namespace SE171089_Daos
{
    public interface IDao<T>
    {
        Task<List<T>> GetList();
        Task<T?> GetOne(int id);
        Task<T?> Add(T obj);
        Task<T?> Update(T obj);
        Task<T?> Delete(int id);
    }
}