namespace System;

public sealed class FastAPIDateTime : IDateTime
{
    private static readonly AsyncLocal<Func<DateTimeOffset>?> overrideNowFunc = new();
    private static readonly AsyncLocal<Func<DateTimeOffset>?> overrideUtcNowFunc = new();

    public static DateTimeOffset Now => overrideNowFunc.Value != null ? overrideNowFunc.Value() : DateTimeOffset.Now;

    public static DateTimeOffset UtcNow => overrideUtcNowFunc.Value != null ? overrideUtcNowFunc.Value() : DateTimeOffset.UtcNow;

    public static void ResetOverride()
    {
        overrideNowFunc.Value = null;
        overrideUtcNowFunc.Value = null;
    }

    public static void SetOverride(Func<DateTimeOffset> now, Func<DateTimeOffset> utcNow)
    {
        overrideNowFunc.Value = now;
        overrideUtcNowFunc.Value = utcNow;
    }
}
