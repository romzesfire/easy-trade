namespace EasyTrade.DTO.Abstractions;

public interface IRepository<TEnt, in TId> where TEnt : class
{
    IEnumerable<TEnt> GetAll();
    public (IEnumerable<TEnt>, int) GetLimited(int limit, int offset);
    TEnt Get(TId id);
}