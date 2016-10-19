using System;

namespace PhoneBook.DAL.Models
{
	public partial class Address
	{
		public override string ToString()
		{
			return $"{this.Street}, {this.Number}" +
				(this.Apartment == 0 ? $"/{this.Apartment}" : String.Empty) +
				$", {this.City}, {this.Country}, {this.PostalCode}";
		}
	}
}
