using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using PhoneBook.DAL.EF;
using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
	public abstract class BaseRepository<T> : IRepository<T>
		where T : EntityBase
	{
		public PhoneBookContext Context { get; } =
			new PhoneBookContext();

		protected DbSet<T> Table;

		public int Add(T entity)
		{
			Table.Add(entity);
			return Context.SaveChanges();
		}

		public Task<int> AddAsync(T entity)
		{
			Table.Add(entity);
			return Context.SaveChangesAsync();
		}

		public int AddRange(IList<T> entities)
		{
			Table.AddRange(entities);
			return Context.SaveChanges();
		}

		public Task<int> AddRangeAsync(IList<T> entities)
		{
			Table.AddRange(entities);
			return Context.SaveChangesAsync();
		}

		public int Update(T entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			return Context.SaveChanges();
		}

		public Task<int> UpdateAsync(T entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			return Context.SaveChangesAsync();
		}

		public abstract int Delete(int id);
		public abstract Task<int> DeleteAsync(int id);

		public int Delete(T entity)
		{
			Context.Entry(entity).State = EntityState.Deleted;
			return Context.SaveChanges();
		}

		public Task<int> DeleteAsync(T entity)
		{
			Context.Entry(entity).State = EntityState.Deleted;
			return Context.SaveChangesAsync();
		}

		public T GetById(int id)
		{
			return Table.Find(id);
		}

		public Task<T> GetByIdAsync(int id)
		{
			return Table.FindAsync(id);
		}

		public IQueryable<T> GetAll()
		{
			return Table;
		}
	}
}
