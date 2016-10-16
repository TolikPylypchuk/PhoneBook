using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.DAL.Repositories
{
	public interface IRepository<T>
	{
		IEnumerable<T> LocalData { get; }

		int Add(T entity);
		Task<int> AddAsync(T entity);

		int AddRange(IEnumerable<T> entities);
		Task<int> AddRangeAsync(IEnumerable<T> enitities);

		int Update(T entity);
		Task<int> UpdateAsync(T entity);

		int Delete(int id);
		Task<int> DeleteAsync(int id);

		int Delete(T entity);
		Task<int> DeleteAsync(T entity);

		T GetById(int id);
		Task<T> GetByIdAsync(int id);

		IQueryable<T> GetAll();
	}
}
