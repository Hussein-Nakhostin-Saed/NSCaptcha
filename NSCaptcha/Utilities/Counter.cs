namespace NCaptcha.Utilities;

internal sealed class Counter
{
    private static readonly Lazy<Counter> _instance = new Lazy<Counter>(() => new Counter());
    private int _count;

    public static Counter Instance => _instance.Value;
    public int Count => _count;

    public static Counter operator ++(Counter counter)
    {
        Instance._count++;
        return Instance;
    }

    internal void Reset() => _count = 0;
}
