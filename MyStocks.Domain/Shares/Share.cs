using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Exceptions;
using MyStocks.Domain.Primitives;
using MyStocks.Domain.Shares.Exceptions;
using MyStocks.Domain.Shares.Exceptions.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares
{
    public class Share:Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public  ShareTypes ShareType { get; private set; }
        public Currency TotalValueInvested { get; private set; }
        public decimal TotalShares { get; private set; }
        public Currency AveragePrice { get; private set; }
        public List<ShareDetail> SharesDetails { get; private set; } = new List<ShareDetail>();
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

            return new Share(Guid.NewGuid(), code, name, description, shareType, currencyType);
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
            SharesDetails.Add(shareDetail);

            CalculateAveragePrice(shareDetail,false);
            CalculateTotals(shareDetail, false);
        }

        public void RemoveShareDetail(ShareDetail shareDetail)
        {
            //TODO: O sharedetail e Share estão dependente um do outro fortemente. verificar a
            //criação de um domain service para controlar o uso dos dois
            SharesDetails.Remove(shareDetail);

            CalculateAveragePrice(shareDetail, true);
            CalculateTotals(shareDetail, true);

        }

        public void UpdateShareDetail(ShareDetail oldshareDetail, ShareDetail newShareDetail)
        {
            if (oldshareDetail.Id != newShareDetail.Id)
                throw new InvalidOperationException("you cannot update share detail with a new share detail");

            CalculateAveragePrice(oldshareDetail, true);
            CalculateTotals(oldshareDetail, true);

            CalculateAveragePrice(newShareDetail, false);
            CalculateTotals(newShareDetail, false);

        }


        private void CalculateAveragePrice(ShareDetail shareDetail, bool isRemove)
        {

            if (shareDetail.OperationType == OperationType.Buy && !isRemove ||
                shareDetail.OperationType == OperationType.Sell && isRemove)
            {
                decimal price = (TotalShares * AveragePrice.Value + shareDetail.Price.Value * shareDetail.Quantity) /
                                                (TotalShares + shareDetail.Quantity);

                AveragePrice = Currency.Create(AveragePrice.CurrencyType, price);
            }
            else
            {
                decimal price = (TotalShares * AveragePrice.Value - shareDetail.Price.Value * shareDetail.Quantity) /
                                                 (TotalShares - shareDetail.Quantity);

                AveragePrice = Currency.Create(AveragePrice.CurrencyType, price);
            }


        }

        private void CalculateTotals(ShareDetail shareDetail, bool isRemove)
        {
            if (shareDetail.OperationType == OperationType.Buy && !isRemove ||
                shareDetail.OperationType == OperationType.Sell && isRemove)
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
    }
}
