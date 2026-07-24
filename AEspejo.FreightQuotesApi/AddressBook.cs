using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotesApi
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime? LastModifiedUTC { get; set; }
    }

    public class Country : BaseEntity
    {
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;
        public ICollection<State> States { get; set; } = new List<State>();
    }

    public class State : BaseEntity
    {
        public long CountryId { get; set; }
        public Country Country { get; set; } = null!;
        [MaxLength(8)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;
    }

    public class ConstantType : BaseEntity
    {
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;
        public ICollection<Constant> Constants { get; set; } = new List<Constant>();
    }

    public class Constant : BaseEntity
    {
        public long ConstantTypeId { get; set; }
        public ConstantType ConstantType { get; set; } = null!;
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;
    }

    public enum ConstantAddressType
    {
        Billing = 1,
        Origin = 2,
        Destination = 3
    }

    public class PartyAddress : BaseEntity
    {
        [MaxLength(1)]
        public ConstantAddressType Type { get; set; } // O=Origin, D=Destination, B=Billing
        [MaxLength(200)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Address1 { get; set; }
        [MaxLength(200)]
        public string? Address2 { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        public long StateId { get; set; }
        public State State { get; set; } = null!;
        public long CountryId { get; set; }
        public Country Country { get; set; } = null!;
        [MaxLength(15)]
        public string Zip { get; set; } = string.Empty;
    }

    public class Address : BaseEntity
    {
        [MaxLength(200)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Address1 { get; set; }
        [MaxLength(200)]
        public string? Address2 { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        public long StateId { get; set; }
        public State State { get; set; } = null!;
        public long CountryId { get; set; }
        public Country Country { get; set; } = null!;
        [MaxLength(15)]
        public string Zip { get; set; } = string.Empty;
    }


    //public abstract class BaseEntity
    //{
    //    [Key]
    //    public long Id { get; set; }
    //    public DateTime CreatedUTC { get; set; }
    //    public DateTime? LastModifiedUTC { get; set; }
    //}

    //public class Country: BaseEntity
    //{
    //    [MaxLength(200)]
    //    public string Name { get; set; } = string.Empty;
    //    [MaxLength(50)]
    //    public string Code { get; set; } = string.Empty;
    //}

    //public class State : BaseEntity
    //{
    //    public int CountryId { get; set; }
    //    [MaxLength(8)]
    //    public string Name { get; set; } = string.Empty;
    //    [MaxLength(50)]
    //    public string Code { get; set; } = string.Empty;
    //}

    //public class Constant : BaseEntity
    //{
    //    public int ConstantTypeId { get; set; }
    //    [MaxLength(250)]
    //    public string Description { get; set; } = string.Empty;
    //    [MaxLength(50)]
    //    public string Code { get; set; } = string.Empty;
    //}

    //public class ConstantType : BaseEntity
    //{       
    //    [MaxLength(250)]
    //    public string Description { get; set; } = string.Empty;
    //    [MaxLength(50)]
    //    public string Code { get; set; } = string.Empty;
    //}

    //public enum ConstantAddressType
    //{
    //    Billing = 1,
    //    Origin = 2,
    //    Destination = 3
    //}

    //public class PartyAddress : BaseEntity
    //{
    //    [MaxLength(1)]
    //    public ConstantAddressType Type { get; set; } //O=Origin, D=Destination, B=Billing

    //    [MaxLength(200)]
    //    public string? Name { get; set; }

    //    [MaxLength(200)]
    //    public string? Address1 { get; set; }

    //    [MaxLength(200)]
    //    public string? Address2 { get; set; }

    //    [MaxLength(50)]
    //    public string? City { get; set; }

    //    [MaxLength(30)]
    //    public long StateId { get; set; }

    //    [MaxLength(5)]
    //    public long CountryId   { get; set; }

    //    [MaxLength(15)]
    //    public string Zip { get; set; } = string.Empty;
    //}

    //public class Address : BaseEntity
    //{
    //    [MaxLength(200)]
    //    public string? Name { get; set; }

    //    [MaxLength(200)]
    //    public string? Address1 { get; set; }

    //    [MaxLength(200)]
    //    public string? Address2 { get; set; }

    //    [MaxLength(50)]
    //    public string? City { get; set; }

    //    [MaxLength(30)]
    //    public long StateId { get; set; }

    //    [MaxLength(5)]
    //    public long CountryId { get; set; }

    //    [MaxLength(15)]
    //    public string Zip { get; set; } = string.Empty;
    //}
}
