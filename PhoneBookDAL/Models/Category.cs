using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("Categories")]
	public partial class Category : EntityBase
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public virtual ICollection<Company> Companies { get; set; } =
			new HashSet<Company>();
	}
}
