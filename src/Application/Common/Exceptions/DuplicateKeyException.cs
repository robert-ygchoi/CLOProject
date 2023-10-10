namespace Application.Common.Exceptions;

public class DuplicateKeyException : Exception
{
    public string Key { get; init; }

    public DuplicateKeyException(string key) : base("One entity insert failure have occurred")
    {
        Key = key;
    }
}
