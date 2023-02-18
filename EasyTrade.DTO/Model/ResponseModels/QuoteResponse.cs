namespace EasyTrade.Service.Model.ResponseModels;

public class QuoteResponse
{
    public string Date { get; set; }
    public Info Info { get; set; }
    public Query Query { get; set; }
    public decimal Result { get; set; }
    public bool Success { get; set; }
}


public class Info
{
    public decimal Rate { get; set; }
    public int Timestamp { get; set; }
}

public class Query
{
    public int Amount { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}

