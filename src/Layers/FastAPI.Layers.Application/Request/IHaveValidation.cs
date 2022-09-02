namespace FastAPI.Layers.Application.Request;

using FastAPI.Layers.Application.Response;

public interface IHaveValidation
{
    bool IsValid { get; }

    void AddValidationErrors(params AppError[] errors);

    IReadOnlyCollection<AppError> GetValidationErrors();
}
