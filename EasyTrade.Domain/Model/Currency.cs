using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTrade.Domain.Model;

public class Currency
{
    public uint Id { get; set; }
    public string Name { get; set; }

    [Key]
    [Required]
    [Column(TypeName = "char(3)")]
    public string IsoCode { get; set; }

    public Currency()
    {
    }

    public Currency(uint id, string name, string iso)
    {
        Id = id;
        Name = name;
        IsoCode = iso;
    }
}