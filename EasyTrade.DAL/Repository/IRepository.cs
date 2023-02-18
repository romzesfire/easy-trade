namespace EasyTrade.DTO.Model.Repository;

public interface IRepository<out TEnt, in TId> where TEnt : class
{
    IEnumerable<TEnt> GetAll();
    IEnumerable<TEnt> GetLimited(int limit, int offset);
    TEnt Get(TId id);
}