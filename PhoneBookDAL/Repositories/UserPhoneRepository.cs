using System.Data.Entity;
using System.Threading.Tasks;

using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
	public class UserPhoneRepository : BaseRepository<UserPhone>
	{
		public UserPhoneRepository()
		{
			table = Context.UserPhones;
		}

		public override int Delete(int id)
		{
			Context.Entry(
				new UserPhone()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			Context.Entry(
				new UserPhone()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChangesAsync();
		}
	}
}
