namespace EasyTrade.DAL.Model;

public class Balance
{
    public uint Id { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
}