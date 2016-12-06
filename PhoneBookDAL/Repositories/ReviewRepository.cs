using System.Data.Entity;
using System.Threading.Tasks;

using PhoneBook.DAL.EF;
using PhoneBook.DAL.Models;

namespace PhoneBook.DAL.Repositories
{
	public class ReviewRepository : BaseRepository<Review>
	{
		public ReviewRepository(PhoneBookContext context)
			: base(context)
		{
			table = Context.Reviews;
		}

		public override int Delete(int id)
		{
			Context.Entry(
				new Review()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChanges();
		}

		public override Task<int> DeleteAsync(int id)
		{
			Context.Entry(
				new Review()
				{
					Id = id
				}).State = EntityState.Deleted;
			return Context.SaveChangesAsync();
		}
	}
}
