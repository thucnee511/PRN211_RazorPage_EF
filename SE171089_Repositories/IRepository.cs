namespace SE171089_Repositories
{
    public interface IRepository<T>
    {
        Task<T?> Add(T obj);
        Task<T?> Update(T obj);
        Task<T?> Delete(int id);
        Task<T?> GetOne(int id);
        Task<List<T>> GetList();
    }
}
