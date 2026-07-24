namespace AEspejo.FreightQuotes.Shared.Dtos.Carrier;

public record SaveCarrierRequest(
    string Name = "",
    string Scac = "",
    bool IsActive = false,
    bool IsMockMode = true
);
