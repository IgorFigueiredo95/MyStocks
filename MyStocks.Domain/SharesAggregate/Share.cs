using MyStocks.Domain.Common.Abstractions;
using MyStocks.Domain.Common.Primitives;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Exceptions;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.Primitives;
using MyStocks.Domain.Shares.Exceptions;
using MyStocks.Domain.Shares.Exceptions.Shares;
using MyStocks.Domain.Shares.Exceptions.SharesDetail;
using MyStocks.Domain.SharesAggregate.DomainEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.SharesAggregate;

//Aggregate Root => raiz da agregação share.
//garante a consistência do aggregate
//Agregates filhos "ShareDetail" são controlados/persistidos por aqui. para garantir a consistência.
//https://martinfowler.com/bliki/DDD_Aggregate.html
//https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/net-core-microservice-domain-model
public class Share : Entity, IAggregateRoot, IHasOwner
{ 
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public  ShareTypes ShareType { get; private set; }
    public Currency TotalValueInvested { get; private set; }
    public decimal TotalShares { get; private set; }
    public Currency AveragePrice { get; private set; }

    private List<ShareDetail> _shareDetails = new List<ShareDetail>();
    public IReadOnlyCollection<ShareDetail> ShareDetails { get => _shareDetails; }
    public Guid OwnerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    //ef compatibility
    private Share():base(Guid.Empty){}

    private Share(Guid id, string code, string name, string? description, ShareTypes shareType, CurrencyTypes currencyType) : base(id)
    {
        Code = code;
        Name = name;
        Description = description;
        ShareType = shareType;
        CreatedAt = DateTime.Now;
        TotalValueInvested = Currency.Create(currencyType, 0);
        AveragePrice = Currency.Create(currencyType, 0);
    }

    public static Share Create(string code, string name, string? description, ShareTypes shareType, CurrencyTypes currencyType)
    {
        if (code.Length > Constants.MAX_CODE_LENGTH)
            throw new InvalidLengthShareCodeException(nameof(code), 1, Constants.MAX_CODE_LENGTH);

        if (name.Length > Constants.MAX_SHARENAME_LENGTH)
            throw new InvalidLengthShareNameException(nameof(name), Constants.MAX_SHARENAME_LENGTH);

        if (name.Length == 0)
            throw new NameCannotBeEmptyShareException(nameof(name), new ArgumentNullException(nameof(name)));

        if (description?.Length > Constants.MAX_SHAREDESCRIPTION_LENGTH)
            throw new InvalidLengthShareDescriptionException(nameof(description), Constants.MAX_SHAREDESCRIPTION_LENGTH);

        if (currencyType is null)
            throw new CurrencyTypeCannotBeEmptyShareException(nameof(currencyType), new ArgumentNullException(nameof(currencyType)));

        Share share = new Share(Guid.NewGuid(), code, name, description, shareType, currencyType);

        share.AddDomainEvent(new ShareCreated(share.Id));

        return share;
    }

    public Share Update(string? name, string? description, ShareTypes? shareTypes)
    {
        if (name?.Length > Constants.MAX_SHARENAME_LENGTH)
            throw new InvalidLengthShareNameException(nameof(name), 1, Constants.MAX_CODE_LENGTH);

        if (description?.Length > Constants.MAX_SHAREDESCRIPTION_LENGTH)
            throw new InvalidLengthShareDescriptionException(nameof(description), Constants.MAX_SHAREDESCRIPTION_LENGTH);

        Name = name ?? Name;
        Description = description ?? Description;
        ShareType = shareTypes ?? ShareType;

        UpdatedAt = DateTime.UtcNow;

        return this;
    }

    public void AddShareDetail(ShareDetail shareDetail)
    {

        if (shareDetail.OperationType == OperationType.Sell &&
        !HasEnoughBalanceToSell(shareDetail.Quantity, shareDetail.Price.Value))
            throw new InvalidOperationException("You do not have enough balance to sell this amount.");

        CalculateAveragePrice(shareDetail,false);
        CalculateTotals(shareDetail, false);

        _shareDetails.Add(shareDetail);
    }

    public void UpdateShareDetail(ShareDetail oldshareDetail, string? note, decimal? quantity, Currency? price)
    {
        // 1º remove valores do sharedetail dos agregados totais 
        // 2º atualiza sharedetail
        //3º atualiza valro dos agregados totais com valor do sharedetail atualizado.
        if (oldshareDetail.ShareId != Id || oldshareDetail.ShareId == Guid.Empty)
            throw new InvalidOperationException("This Share Detail does not belong to this Share!");
        
        CalculateAveragePrice(oldshareDetail, true);
        CalculateTotals(oldshareDetail, true);

        var updatedShareDetail = oldshareDetail.Update(note, quantity, price);

        if (updatedShareDetail.OperationType == OperationType.Sell &&
            !HasEnoughBalanceToSell(updatedShareDetail.Quantity, updatedShareDetail.Price.Value))
            throw new InvalidOperationException("You do not have enough balance to sell this amount.");

        CalculateAveragePrice(updatedShareDetail, false);
        CalculateTotals(updatedShareDetail, false);

    }

    public void RemoveShareDetail(ShareDetail shareDetail)
    {
        if (!_shareDetails.Any(s => s.Id == shareDetail.Id))
            throw new InvalidOperationException("you cannot  remove a shareDetail that is not associated in this share");

        _shareDetails.Remove(shareDetail);

        CalculateAveragePrice(shareDetail, true);
        CalculateTotals(shareDetail, true);

    }

    private void CalculateAveragePrice(ShareDetail shareDetail, bool isShareDetailRemove)
    {

        if (shareDetail.OperationType == OperationType.Buy && !isShareDetailRemove ||
            shareDetail.OperationType == OperationType.Sell && isShareDetailRemove)
        {
            decimal price = (TotalShares * AveragePrice.Value + shareDetail.Price.Value * shareDetail.Quantity) /
                                            (TotalShares + shareDetail.Quantity);

            AveragePrice = Currency.Create(AveragePrice.CurrencyType, price);
        }
        else
        {
            if(TotalShares - shareDetail.Quantity == 0)
            {
                AveragePrice = Currency.Create(AveragePrice.CurrencyType, 0);
                return;
            }

            decimal price = (TotalShares * AveragePrice.Value - shareDetail.Price.Value * shareDetail.Quantity) /
                                             (TotalShares - shareDetail.Quantity);

            AveragePrice = Currency.Create(AveragePrice.CurrencyType, price);
        }


    }

    private void CalculateTotals(ShareDetail shareDetail, bool IsShareDetailRemove)
    {
        if (shareDetail.OperationType == OperationType.Buy && !IsShareDetailRemove ||
            shareDetail.OperationType == OperationType.Sell && IsShareDetailRemove)
        {
            TotalValueInvested = Currency.Create(TotalValueInvested.CurrencyType,
               TotalValueInvested.Value + (shareDetail.Price.Value * shareDetail.Quantity)
               );

            TotalShares += shareDetail.Quantity;
        }
        else
        {
            TotalValueInvested = Currency.Create(TotalValueInvested.CurrencyType,
            TotalValueInvested.Value - shareDetail.Price.Value * shareDetail.Quantity);

            TotalShares -= shareDetail.Quantity;
        }
    }


    private bool HasEnoughBalanceToSell(decimal quantityToSell, decimal value)
    {
        if (quantityToSell > TotalShares || 
            (quantityToSell * value) > TotalValueInvested.Value)
            return false;

        return true;
    }

}
