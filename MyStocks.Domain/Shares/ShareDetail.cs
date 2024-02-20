using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Exceptions;
using MyStocks.Domain.Primitives;
using MyStocks.Domain.Shares.Exceptions.SharesDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares;

public class ShareDetail : Entity
{
    public Guid ShareId { get; private set; }
    //todo: nav property se não ignorar esse json, chega a uma referência infita ao retornar da APi de get de share. dando erro
    [JsonIgnore]
    public Share Share { get; private set; }
    public string? Note { get; private set; }
    public decimal Quantity { get; private set; }
    public Currency Price { get; private set; }
    public OperationType OperandType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    //Compatibility with EfCore
    private ShareDetail() : base(Guid.NewGuid())
    { }
    

    private ShareDetail(Guid id, Guid shareId, decimal quantity, Currency price, OperationType operationType, string? note) : base(id)
    {
        ShareId = shareId;
        Quantity = quantity;
        Price = price;
        OperandType = operationType;
        Note = note;
        CreatedAt = DateTime.Now;
    }

    public static ShareDetail Create(Guid shareId, decimal quantity, Currency price, string operationTypeCode, string? note)

    {
        if (shareId == Guid.Empty)
            throw new InvalidLengthShareDetailCodeException(nameof(shareId),new NullReferenceException());

        if (quantity <= 0)
            throw new InvalidQuantityShareDetailException(nameof(quantity));

        if (note!.Length > Constants.MAX_SHAREDETAILNOTE_LENGTH)
            throw new InvalidNoteShareDetailException(nameof(note),Constants.MAX_SHAREDETAILNOTE_LENGTH);

        OperationType parsedOperationType;
        if (!Enum.TryParse<OperationType>(operationTypeCode, out parsedOperationType))
            throw new InvalidOperationTypeShareDetailException(nameof(operationTypeCode));


        return new ShareDetail(Guid.NewGuid(), shareId, quantity, price, parsedOperationType, note);


    }
}