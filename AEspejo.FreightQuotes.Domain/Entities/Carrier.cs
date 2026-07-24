namespace AEspejo.FreightQuotes.Domain.Entities
{
    public class Carrier : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Scac { get; set; } = string.Empty;
        public bool IsMockMode { get; set; } = true;
        public bool IsActive { get; set; }

        public ICollection<CarrierSetting> Settings { get; set; } = [];
    }
}
