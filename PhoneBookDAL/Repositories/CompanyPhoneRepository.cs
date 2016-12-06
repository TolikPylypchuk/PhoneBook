using System.Data.Entity;
using System.Threading.Tasks;

using PhoneBook.DAL.EF;
using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
	public class CompanyPhoneRepository : BaseRepository<CompanyPhone>
	{
		public CompanyPhoneRepository(PhoneBookContext context)
			: base(context)
		{
			table = Context.CompanyPhones;
		}

		public override int Delete(int id)
		{
			Context.Entry(
				new CompanyPhone()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			Context.Entry(
				new CompanyPhone()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChangesAsync();
		}
	}
}
