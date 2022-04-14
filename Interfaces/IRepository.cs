namespace LoggingAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        IEnumerable<T> FindAll();
    }
}
