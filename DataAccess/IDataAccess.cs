namespace Study.Parking.DataAccess
{
    public interface IDataAccess<T>
    {
        Task<List<T>> Get();
        Task<T> Get(string Id);
        Task Post(T model);
        Task<T> Put(string Id, T model);
        Task Delete(string Id);
    }
}