namespace System;

public interface IDateTime
{
    static abstract DateTime Now { get; }

    static abstract DateTime UtcNow { get; }

    static abstract void SetOverride(Func<DateTime> now, Func<DateTime> utcNow);

    static abstract void ResetOverride();
}
