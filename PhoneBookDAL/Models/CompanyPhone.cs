using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("CompanyPhones")]
	public partial class CompanyPhone : PhoneBase
	{
		[Required]
		[Display(Name = "Company ID")]
		public int CompanyId { get; set; }

		[ForeignKey("CompanyId")]
		public virtual Company Company { get; set; }
	}
}
