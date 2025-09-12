namespace CRM.Application.Interfaces.Services;

public interface IService<T> where T : class
{
	public Task<T> GetAsync(int id);
	public Task<IEnumerable<T>> GetAllAsync();
	public Task<bool> CreateAsync(T entity);
}
