namespace AEspejo.FreightQuotes.Application.Exceptions;

/// <summary>
/// Thrown when a request conflicts with the current state of a resource
/// (e.g. attempting to create a duplicate). Maps to HTTP 409 Conflict.
/// </summary>
public class ConflictException(string message) : Exception(message)
{
}
