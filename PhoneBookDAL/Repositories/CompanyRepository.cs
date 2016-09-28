using System.Data.Entity;
using System.Threading.Tasks;

using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
	public class CompanyRepository : BaseRepository<Company>
	{
		public CompanyRepository()
		{
			Table = Context.Companies;
		}

		public override int Delete(int id)
		{
			Context.Entry(
				new Company()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			Context.Entry(
				new Company()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChangesAsync();
		}
	}
}
