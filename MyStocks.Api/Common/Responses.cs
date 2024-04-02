using Microsoft.AspNetCore.Mvc;
using MyStocks.Domain.Common;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MyStocks.Api.Common;

public class Responses: ProblemDetails
{
    
    private List<Error> _Errors = new();
    [JsonPropertyOrder(5)]
    public IReadOnlyList<Error> Errors { get => _Errors; }


    private Responses(List<Error> error, HttpContext context)
    {
        Title = "request cannot be processed";
        Status = 400;
        Detail = error.Count() > 1? "One or more errors has ocourred.": "An error has occurred.";
        _Errors.AddRange(error);
        Instance = context.Request.Path;
    }
    public static ObjectResult ErrorResponse(HttpContext context, Error error)
    {
        var errorList = new List<Error>() { error };
        var res = new Responses(errorList, context);

        return new ObjectResult(res);
    }

    public static ObjectResult ErrorResponse(HttpContext context, List<Error> error)
    {
        var res = new Responses(error, context);
        return new ObjectResult(res);
    }
}
