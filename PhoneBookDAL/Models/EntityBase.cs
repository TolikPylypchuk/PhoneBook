using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	public partial class EntityBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[UIHint("Created at")]
		public DateTime CreatedAt { get; set; }

		[Required]
		[Timestamp]
		[UIHint("Updated at")]
		public byte[] UpdatedAt { get; set; }
	}
}
