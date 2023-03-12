namespace EasyTrade.DTO.Abstractions;

public interface IRepository<TEnt, in TId> where TEnt : class
{
    Task<IEnumerable<TEnt>> GetAll();
    public (IEnumerable<TEnt>, int) GetLimited(int limit, int offset);
    Task<TEnt> Get(TId id);
}