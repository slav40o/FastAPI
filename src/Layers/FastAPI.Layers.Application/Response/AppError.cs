namespace FastAPI.Layers.Application.Response;

/// <summary>
/// Holds error value used in <see cref="AppResponse" and <see cref="AppResponse{TData}"/>/>.
/// </summary>
public sealed class AppError
{
    /// <summary>
    /// Key for storing all general(key-less) errors.
    /// </summary>
    public static readonly string GeneralErrorKey = string.Empty;

    private readonly ICollection<string> errors;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppError"/> class.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="error">Error message.</param>
    public AppError(string key, string error)
        : this(key, new List<string>() { error })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppError"/> class.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="errors">List of message errors.</param>
    public AppError(string key, IEnumerable<string> errors)
    {
        this.Key = key;
        this.errors = errors.ToList();
    }

    /// <summary>
    /// Gets error key.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Gets list of error messages.
    /// </summary>
    public IReadOnlyCollection<string> ErrorMessages => this.errors.ToList();

    /// <summary>
    /// Creates result error with empty string key and one general error message.
    /// </summary>
    /// <param name="message">General error message.</param>
    /// <returns>Created general result error instance.</returns>
    public static AppError GeneralError(string message)
        => new(GeneralErrorKey, message);

    /// <summary>
    /// Add additional error message.
    /// </summary>
    /// <param name="message">Error message.</param>
    public void AddErrorMessage(string message)
    {
        this.errors.Add(message);
    }

    /// <summary>
    /// Add additional error messages.
    /// </summary>
    /// <param name="messages">Collection of error message.</param>
    public void AddErrorMessages(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            this.AddErrorMessage(message);
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{this.Key} - {this.errors.Count} errors.";
    }
}
