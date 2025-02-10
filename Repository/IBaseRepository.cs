using airtext_api.Models;

namespace airtext_api.Repository;

public interface IBaseRepository<T> where T : BaseEntity
{
	Task<T> AddAsync(T entity);
	//Task<T> Update(T entity);
	//Task<T> Delete(T entity);

	Task<T> GetAsync(Guid Id);
	//Task<List<T>> GetAll();
}