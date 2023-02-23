namespace EasyTrade.DAL.Model;


public class Operation
{
    public uint Id { get; set; }
    public DateTimeOffset DateTime { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
}