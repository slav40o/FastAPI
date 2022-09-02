namespace FastAPI.Layers.Presentation.Result;

using FastAPI.Layers.Application.Response;

using Microsoft.AspNetCore.Http;

internal static class ResultExtensions
{
    public static async Task<IResult> ToIResult(this Task<AppResponse> response)
    {
        var result = await response;

        if (result.Succeded)
        {
            return Results.Ok(Result.Success(result.Message));
        }

        if (result.FailureType == ResponseFailures.ValidationFail)
        {
            return Results.BadRequest(Result.Fail(result.Message, result.Errors));
        }

        if (result.FailureType == ResponseFailures.NotFound)
        {
            return Results.NotFound(result.Message);
        }

        if (result.FailureType == ResponseFailures.Unauthorized)
        {
            return Results.Unauthorized();
        }

        return Results.Problem(result.Message);
    }

    public static async Task<IResult> ToIResult<TData>(this Task<AppResponse<TData>> response)
    {
        var result = await response;

        if (result.Succeded)
        {
            return Results.Ok(Result.Success(result.Message, result.Data));
        }

        if (result.FailureType == ResponseFailures.ValidationFail)
        {
            return Results.BadRequest(Result.Fail(result.Message, result.Errors));
        }

        if (result.FailureType == ResponseFailures.NotFound)
        {
            return Results.NotFound(result.Message);
        }

        if (result.FailureType == ResponseFailures.Unauthorized)
        {
            return Results.Unauthorized();
        }

        return Results.Problem(result.Message);
    }
}
