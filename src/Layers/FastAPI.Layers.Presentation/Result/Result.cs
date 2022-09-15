namespace FastAPI.Layers.Presentation.Result;

using FastAPI.Layers.Application.Response;

internal class Result
{
	public Result(string message)
	{
		this.Message = message;
	}

    public string Message { get; init; }

    public static Result Success(string message) 
        => new (message);

    public static DataResult<TData> Success<TData>(string message, TData data) 
        => new(message, data);

    public static ErrorResult Fail(string message, IEnumerable<AppError> errors) 
        => new(message, errors);
}

internal sealed class ErrorResult : Result
{
	public ErrorResult(string message, IEnumerable<AppError>? errors)
		: base(message)
	{
		this.Errors = errors ?? new List<AppError>();
	}

    public IEnumerable<AppError> Errors { get; init; }
}

internal sealed class DataResult<TData> : Result
{
    public DataResult(string message, TData data)
        : base(message)
    {
        this.Data = data;
    }

    public TData Data { get; init; }
}
