namespace AEspejo.FreightQuotes.Shared.Dtos.Carrier;

public record CarrierResponse(
    long Id,
    string Name = "",
    string Scac = "",
    bool IsActive = false,
    bool IsMockMode = true
);

