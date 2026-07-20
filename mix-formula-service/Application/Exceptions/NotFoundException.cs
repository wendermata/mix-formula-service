namespace Application.Exceptions;

public sealed class NotFoundException(string resourceName, object key)
    : Exception($"{resourceName} with id '{key}' was not found.")
{
    public string ResourceName { get; } = resourceName;
    public object Key { get; } = key;
}
