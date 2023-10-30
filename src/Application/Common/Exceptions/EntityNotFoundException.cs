namespace Application.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string id) : base($"`{id}` Not Found")
    {
        Id = id;
    }

    public string Id { get; }
}
