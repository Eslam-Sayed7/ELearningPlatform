namespace Infrastructure.Data.IServices;

public interface IRedisCachService
{
    T? GetData<T>(string key);
    void SetData<T>(string key, T data);
}