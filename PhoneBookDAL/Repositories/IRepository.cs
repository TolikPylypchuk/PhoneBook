using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.DAL.Repositories
{
	public interface IRepository<T>
	{
		int Add(T entity);
		Task<int> AddAsync(T entity);

		int AddRange(IList<T> entities);
		Task<int> AddRangeAsync(IList<T> enitities);

		int Update(T entity);
		Task<int> UpdateAsync(T entity);

		int Delete(int id, byte[] lastUpdate);
		Task<int> DeleteAsync(int id, byte[] lastUpdate);

		int Delete(T entity);
		Task<int> DeleteAsync(T entity);

		T GetById(int id);
		Task<T> GetByIdAsync(int id);

		IQueryable<T> GetAll();
	}
}
