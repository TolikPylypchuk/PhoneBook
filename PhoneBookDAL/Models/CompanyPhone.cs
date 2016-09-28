using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("CompanyPhones")]
	public partial class CompanyPhone : EntityBase
	{
		[Required]
		[Phone]
		[StringLength(15)]
		public string Number { get; set; }

		[Required]
		public int CompanyId { get; set; }

		[ForeignKey("CompanyId")]
		public virtual Company Company { get; set; }
	}
}
