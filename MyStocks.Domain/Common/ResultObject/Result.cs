using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace MyStocks.Domain.Common.ResultObject;

public class Result
{
    private readonly List<Error> _Errors = new List<Error>();
    public IReadOnlyList<Error> Errors => _Errors;
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get => !IsSuccess; }

    private Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    #region Create
    public static Result Create()
    {
        return new Result(true);
    }

    public static Result Create(Error error)
    {
        var result = new Result(false);
        result.AddError(error);
        return result;
    }
    public static Result Create(List<Error> errors)
    {
        var result = new Result(false);
        result.AddError(errors);
        return result;
    }
    #endregion

    public void AddError(Error error)
    {
        IsSuccess = false;
        _Errors.Add(error);
    }

    public void AddError(List<Error> errors)
    {
        IsSuccess = false;
        _Errors.AddRange(errors);
    }

    public void AddError(string key, string message)
    {
        var error = Error.Create(key, message);
        IsSuccess = false;
        _Errors.Add(error);
    }


    public static implicit operator Result(bool value)
    {
        return new Result(value);
    }

    public static implicit operator Result(Error error)
    {
        var result = new Result(false);
        result.AddError(error);

        return result;
    }

    public static implicit operator bool(Result result) 
    {
        return result.IsSuccess;
    }

    public static implicit operator Result(List<Error> errors)
    {
        var result = new Result(false);
            result.AddError(errors);
        return result;
    }
}
