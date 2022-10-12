namespace System;

public interface IDateTime
{
    static abstract DateTimeOffset Now { get; }

    static abstract DateTimeOffset UtcNow { get; }

    static abstract void SetOverride(Func<DateTimeOffset> now, Func<DateTimeOffset> utcNow);

    static abstract void ResetOverride();
}
