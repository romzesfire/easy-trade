namespace EasyTrade.DTO.Model;


public class Currency
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string IsoCode { get; set; }

    public Currency(uint id, string name, string iso)
    {
        Id = id;
        Name = name;
        IsoCode = iso;
    }
}