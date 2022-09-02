namespace FastAPI.Layers.Application.Response;

public class AppResponse
{
    protected internal readonly AppErrorDictionary errorsDictionary;

    public AppResponse()
    {
        this.errorsDictionary = new ();
    }

    public string Message { get; protected internal set; } = default!;

    public bool Succeded { get; protected internal set; }

    public ResponseFailures? FailureType { get; protected internal set; }

    public bool HasError => this.errorsDictionary.Any();

    public IReadOnlyCollection<AppError> Errors => this.errorsDictionary.AsReadOnlyCollection();

    private AppResponse AsSuccess()
    {
        this.Succeded = true;
        return this;
    }

    private AppResponse WithErrors(AppError[] errors)
    {
        this.errorsDictionary.AddRange(errors);
        return this;
    }

    private AppResponse WithFailureType(ResponseFailures failure)
    {
        this.FailureType = failure;
        return this;
    }

    private AppResponse WithMessage(string message)
    {
        this.Message = message;
        return this;
    }

    private AppResponse WithGlobalError(string errorMessage)
    {
        this.errorsDictionary.Add(AppError.GeneralError(errorMessage));
        return this;
    }

    public static AppResponse Success(string message)
        => New().AsSuccess().WithMessage(message);

    public static AppResponse<T> Success<T>(string message, T data)
        => AppResponse<T>.New<T>().AsSuccess().WithMessage(message).WithData(data);

    public static AppResponse NotFound(string message)
        => Fail(message, ResponseFailures.NotFound);

    public static AppResponse<T> NotFound<T>(string message)
        => AppResponse<T>.Fail<T>(message, ResponseFailures.NotFound);

    public static AppResponse ValidationFail(string message, params AppError[] validationErrors)
        => Fail(message, ResponseFailures.ValidationFail, validationErrors);

    public static AppResponse<T> ValidationFail<T>(string message, params AppError[] validationErrors)
        => AppResponse<T>.Fail<T>(message, ResponseFailures.ValidationFail, validationErrors);

    public static AppResponse Unauthorized(string message)
        => Fail(message, ResponseFailures.Unauthorized);

    public static AppResponse<T> Unauthorized<T>(string message)
        => AppResponse<T>.Fail<T>(message, ResponseFailures.Unauthorized);

    private static AppResponse New()
        => new();

    private static AppResponse Fail(string message, ResponseFailures failure)
        => New().WithMessage(message).WithFailureType(failure).WithGlobalError(message);

    private static AppResponse Fail(string message, ResponseFailures failure, params AppError[] errors)
        => New().WithMessage(message).WithGlobalError(message).WithFailureType(failure).WithErrors(errors);
}

public class AppResponse<TData> : AppResponse
{
    public TData? Data { get; private set; } = default!;

    internal AppResponse<TData> WithData(TData data)
    {
        this.Data = data;
        return this;
    }

    internal AppResponse<TData> AsSuccess()
    {
        this.Succeded = true;
        return this;
    }

    internal AppResponse<TData> WithErrors(AppError[] errors)
    {
        this.errorsDictionary.AddRange(errors);
        return this;
    }

    internal AppResponse<TData> WithFailureType(ResponseFailures failure)
    {
        this.FailureType = failure;
        return this;
    }

    internal AppResponse<TData> WithMessage(string message)
    {
        this.Message = message;
        return this;
    }

    internal AppResponse<TData> WithGlobalError(string errorMessage)
    {
        this.errorsDictionary.Add(AppError.GeneralError(errorMessage));
        return this;
    }

    public static new AppResponse<T> Success<T>(string message, T data)
        => New<T>().AsSuccess().WithMessage(message).WithData(data);

    public static new AppResponse<T> NotFound<T>(string message)
        => Fail<T>(message, ResponseFailures.NotFound);

    public static new AppResponse<T> ValidationFail<T>(string message, params AppError[] validationErrors)
        => Fail<T>(message, ResponseFailures.ValidationFail, validationErrors);

    public static new AppResponse<T> Unauthorized<T>(string message)
        => Fail<T>(message, ResponseFailures.Unauthorized);

    internal static AppResponse<T> New<T>()
        => new();

    internal static AppResponse<T> Fail<T>(string message, ResponseFailures failure)
        => New<T>().WithMessage(message).WithFailureType(failure).WithGlobalError(message);

    internal static AppResponse<T> Fail<T>(string message, ResponseFailures failure, params AppError[] errors)
        => New<T>().WithMessage(message).WithGlobalError(message).WithFailureType(failure).WithErrors(errors);
}
