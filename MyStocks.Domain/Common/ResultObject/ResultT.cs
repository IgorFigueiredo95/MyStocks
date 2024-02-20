using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyStocks.Domain.Common.ResultObject;

public class Result<T>
{
    //Forma de controlar validações de dominio para evitar lançar exceptions para input de dados no dominio.     // see more: https://medium.com/tableless/n%C3%A3o-lance-exceptions-em-seu-dom%C3%ADnio-use-notifications-70b31f7148d3
    //Martin fowler https://martinfowler.com/articles/replaceThrowWithNotification.html

    private readonly List<Error>? _Errors = new List<Error>();
    public IReadOnlyList<Error>? Errors => _Errors;
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get => !IsSuccess;}
    public T? Value { get; private set; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        
    }

    private Result(Error error)
    {
        IsSuccess = false;
        _Errors.Add(error);  
    }

    private Result(List<Error> errors)
    {
        IsSuccess = false;
        _Errors.AddRange(errors);
    }

    #region Add Error
    public void AddError(Error error)
    {
        IsSuccess = false;
        _Errors!.Add(error);
    }

    public void AddError(List<Error> errors)
    {
        IsSuccess = false;
        _Errors!.AddRange(errors);
    }
    #endregion;

    #region changeState
    public void SetAsSuccess()
    {
        IsSuccess = true;
    }

    public void SetAsFailure()
    {
        IsSuccess = false;
    }
    #endregion

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }

    public static implicit operator T(Result<T> result)
    {
        return result.Value;
    }

    public static implicit operator Result<T>(List<Error> errors)
    {
        return new Result<T>(errors);
    }
}
