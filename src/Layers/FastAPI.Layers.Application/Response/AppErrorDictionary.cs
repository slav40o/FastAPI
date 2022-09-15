namespace FastAPI.Layers.Application.Response;

/// <summary>
/// Dictionary holding result errors.
/// </summary>
public sealed class AppErrorDictionary
{
    private readonly IDictionary<string, AppError> errors;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppErrorDictionary"/> class.
    /// </summary>
    public AppErrorDictionary()
    {
        this.errors = new Dictionary<string, AppError>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppErrorDictionary"/> class.
    /// </summary>
    /// <param name="initialError">Initial result error to be added to the dictionary.</param>
    public AppErrorDictionary(AppError initialError)
        : this()
    {
        this.errors.Add(initialError.Key, initialError);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppErrorDictionary"/> class.
    /// </summary>
    /// <param name="failures">Initial list with result errors.</param>
    public AppErrorDictionary(IEnumerable<AppError> failures)
        : this()
    {
        if (failures is null || !failures.Any())
        {
            return;
        }

        foreach (var failure in failures)
        {
            // Check for result error duplication
            if (this.errors.ContainsKey(failure.Key))
            {
                this.errors[failure.Key].AddErrorMessages(failure.ErrorMessages);
                continue;
            }

            this.errors.Add(failure.Key, failure);
        }
    }

    /// <summary>
    /// Adds new result error to the dictionary.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="message">Error message.</param>
    public void Add(string key, string message)
    {
        if (!this.errors.ContainsKey(key))
        {
            this.errors.Add(key, new AppError(key, message));
        }

        this.errors[key].AddErrorMessage(message);
    }

    /// <summary>
    /// Adds new result error to the dictionary.
    /// </summary>
    /// <param name="error">Error data.</param>
    public void Add(AppError error)
    {
        if (!this.errors.ContainsKey(error.Key))
        {
            this.errors.Add(error.Key, error);
            return;
        }

        this.errors[error.Key].AddErrorMessages(error.ErrorMessages);
    }

    /// <summary>
    /// Appends a collection of errors to the dictionary.
    /// </summary>
    /// <param name="errors">Error list.</param>
    public void AddRange(IEnumerable<AppError> errors)
    {
        foreach (var error in errors)
        {
            this.Add(error);
        }
    }

    /// <summary>
    /// true if dictionary has any error items.
    /// </summary>
    public bool Any()
    {
        return this.errors.Any();
    }

    /// <summary>
    /// Gets flattened collection of result errors.
    /// </summary>
    /// <returns>Result errors collection.</returns>
    public IReadOnlyCollection<AppError> AsReadOnlyCollection()
        => this.errors.Select(e => e.Value).ToArray();
}