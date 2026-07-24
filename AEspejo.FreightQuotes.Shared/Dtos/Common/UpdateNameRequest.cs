namespace AEspejo.FreightQuotes.Shared.Dtos.Common;

/// <summary>
/// Request body for editing only the display Name of a reference/catalog entity.
/// </summary>
public record UpdateNameRequest(string Name);
