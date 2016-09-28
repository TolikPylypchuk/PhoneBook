using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("Reviews")]
	public class Review : EntityBase
	{
		[Required]
		[StringLength(300)]
		public string Description { get; set; }

		[Required]
		public int Rating { get; set; }

		[Required]
		public int UserId { get; set; }

		[Required]
		public int CompanyId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }

		[ForeignKey("CompanyId")]
		public Company Company { get; set; }
	}
}
