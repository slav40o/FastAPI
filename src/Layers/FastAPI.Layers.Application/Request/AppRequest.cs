namespace FastAPI.Layers.Application.Request;

using FastAPI.Layers.Application.Response;

public abstract record AppRequest : IAppRequest, IHaveValidation
{
    private readonly HashSet<AppError> errors;

    public AppRequest()
    {
        this.errors = new HashSet<AppError>();
    }

    public bool IsValid => this.errors.Count == 0;

    public void AddValidationErrors(params AppError[] errors)
        => Array.ForEach(errors, e => this.errors.Add(e));

    public IReadOnlyCollection<AppError> GetValidationErrors()
        => this.errors.ToList();
}

public abstract record AppRequest<TResponseData> : IAppRequest<TResponseData>, IHaveValidation
{
    private readonly HashSet<AppError> errors;

    public AppRequest()
    {
        this.errors = new HashSet<AppError>();
    }

    public bool IsValid => this.errors.Count == 0;

    public void AddValidationErrors(params AppError[] errors)
        => Array.ForEach(errors, e => this.errors.Add(e));

    public IReadOnlyCollection<AppError> GetValidationErrors()
        => this.errors.ToList();
}
