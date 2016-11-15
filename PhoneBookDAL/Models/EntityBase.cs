using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	public abstract class EntityBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "ID")]
		public int Id { get; set; }

		[Required]
		[Display(Name = "Created at")]
		public DateTime? CreatedAt { get; set; }

		[Required]
		[Display(Name = "Updated at")]
		public DateTime? UpdatedAt { get; set; }
	}
}
