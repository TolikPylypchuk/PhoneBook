using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("Reviews")]
	public partial class Review : EntityBase
	{
		[Required]
		[StringLength(300)]
		public string Description { get; set; }

		[Required]
		public int Rating { get; set; }

		[Required]
		[Display(Name = "User ID")]
		public int UserId { get; set; }

		[Required]
		[Display(Name = "Company ID")]
		public int CompanyId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }

		[ForeignKey("CompanyId")]
		public virtual Company Company { get; set; }
	}
}
