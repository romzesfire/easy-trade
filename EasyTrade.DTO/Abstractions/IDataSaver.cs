namespace EasyTrade.DTO.Abstractions;

public interface IDataSaver
{
    public void Save();

    public void SaveAsync();
}