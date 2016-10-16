using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using PhoneBook.DAL.EF;
using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
	public abstract class BaseRepository<T> : IRepository<T>
		where T : EntityBase, new()
	{
		protected DbSet<T> table;

		public PhoneBookContext Context { get; } =
			new PhoneBookContext();

		public IEnumerable<T> LocalData => this.table.Local;

		public int Add(T entity)
		{
			this.table.Add(entity);
			return this.Context.SaveChanges();
		}

		public Task<int> AddAsync(T entity)
		{
			this.table.Add(entity);
			return this.Context.SaveChangesAsync();
		}

		public int AddRange(IEnumerable<T> entities)
		{
			this.table.AddRange(entities);
			return this.Context.SaveChanges();
		}

		public Task<int> AddRangeAsync(IEnumerable<T> entities)
		{
			this.table.AddRange(entities);
			return this.Context.SaveChangesAsync();
		}

		public int Update(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Modified;
			return this.Context.SaveChanges();
		}

		public Task<int> UpdateAsync(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Modified;
			return this.Context.SaveChangesAsync();
		}

		public abstract int Delete(int id);
		public abstract Task<int> DeleteAsync(int id);

		public int Delete(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Deleted;
			return this.Context.SaveChanges();
		}

		public Task<int> DeleteAsync(T entity)
		{
			this.Context.Entry(entity).State = EntityState.Deleted;
			return this.Context.SaveChangesAsync();
		}

		public T GetById(int id)
		{
			return this.table.Find(id);
		}

		public Task<T> GetByIdAsync(int id)
		{
			return this.table.FindAsync(id);
		}

		public IQueryable<T> GetAll()
		{
			return this.table;
		}
	}
}
