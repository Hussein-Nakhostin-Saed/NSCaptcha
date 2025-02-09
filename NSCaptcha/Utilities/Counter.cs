namespace NSCaptcha.Utilities;

/// <summary>
/// A sealed class representing a thread-safe, singleton counter.
/// This class provides a single, globally accessible instance of a counter that can be incremented
/// using the ++ operator.  It utilizes a lazy initialization pattern to ensure thread safety
/// and efficient instantiation.
/// </summary>
internal sealed class Counter
{
    private static readonly Lazy<Counter> _instance = new Lazy<Counter>(() => new Counter());
    private int _count;

    /// <summary>
    /// Gets the singleton instance of the <see cref="Counter"/> class.
    /// </summary>
    public static Counter Instance => _instance.Value;

    /// <summary>
    /// Gets the current value of the counter.
    /// </summary>
    public int Count => _count;

    /// <summary>
    /// Increments the counter by one.  This method allows the use of the ++ operator directly
    /// on the <see cref="Counter.Instance"/> (e.g., `Counter.Instance++;`).
    /// </summary>
    /// <param name="counter">The <see cref="Counter"/> instance to increment (this parameter is not actually used,
    /// but is required for the operator overload).</param>
    /// <returns>The incremented <see cref="Counter.Instance"/>.</returns>
    public static Counter operator ++(Counter counter)
    {
        Instance._count++;
        return Instance;
    }

    /// <summary>
    /// Resets the counter to zero.
    /// </summary>
    internal void Reset() => _count = 0;
}