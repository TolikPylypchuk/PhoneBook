﻿using System.Data.Entity;
using System.Threading.Tasks;

using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
	public class UserRepository : BaseRepository<User>
	{
		public UserRepository()
		{
			Table = Context.Users;
		}

		public override int Delete(int id, byte[] lastUpdate)
		{
			Context.Entry(
				new User()
				{
					Id = id,
					UpdatedAt = lastUpdate
				}).State = EntityState.Deleted;
			return Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id, byte[] lastUpdate)
		{
			Context.Entry(
				new User()
				{
					Id = id,
					UpdatedAt = lastUpdate
				}).State = EntityState.Deleted;
			return Context.SaveChangesAsync();
		}
	}
}
