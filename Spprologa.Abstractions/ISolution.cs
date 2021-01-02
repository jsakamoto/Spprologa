namespace Spprologa
{
    public interface ISolution
    {
        object? this[string varName] { get; }

        string? ToString();
    }
}