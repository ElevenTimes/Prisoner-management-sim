// Instance (collection of methods) responsible for saving, loading and creating Json files
public interface IDataService
{
    bool SaveData<T>(string RelativePath, T Data);

    T LoadData<T>(string RelativePath);

    bool CreateNewFile<T>(string RelativePath, T Data);
}
