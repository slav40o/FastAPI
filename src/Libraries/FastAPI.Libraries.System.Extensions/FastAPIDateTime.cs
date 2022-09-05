namespace System;

public class FastAPIDateTime : IDateTime
{
    private static readonly AsyncLocal<Func<DateTime>?> overrideNowFunc = new();
    private static readonly AsyncLocal<Func<DateTime>?> overrideUtcNowFunc = new();

    public static DateTime Now => overrideNowFunc.Value != null ? overrideNowFunc.Value() : DateTime.Now;

    public static DateTime UtcNow => overrideUtcNowFunc.Value != null ? overrideUtcNowFunc.Value() : DateTime.Now;

    public static void ResetOverride()
    {
        overrideNowFunc.Value = null;
        overrideUtcNowFunc.Value = null;
    }

    /*
     * Example:
     *  FastAPIDateTime.SetOverride(d => new DateTime(2000, 1, 1), d => new DateTime(2000, 1, 1))
     *  FastAPIDateTime.Now();
     *  FastAPIDateTime.ResetToNormal();
     *  FastAPIDateTime.Now();
     */
    public static void SetOverride(Func<DateTime> now, Func<DateTime> utcNow)
    {
        overrideNowFunc.Value = now;
        overrideUtcNowFunc.Value = utcNow;
    }
}
