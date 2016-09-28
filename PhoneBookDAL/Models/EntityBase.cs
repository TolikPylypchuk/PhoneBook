using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	public class EntityBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Timestamp]
		[Required]
		public byte[] CreatedAt { get; set; }

		[Timestamp]
		[Required]
		public byte[] UpdatedAt { get; set; }
	}
}
