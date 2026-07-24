namespace AEspejo.FreightQuotes.Shared.Dtos.Accessorial;

public record AccessorialResponse(
    long Id, 
    string Name, 
    string Code, 
    long TypeId
);
