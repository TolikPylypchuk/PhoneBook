using System;
using System.Data.Entity.Validation;
using System.Linq;

using PhoneBook.DAL.EF;
using PhoneBook.DAL.Models;
using PhoneBook.DAL.Repositories;

namespace PhoneBook.Services
{
	public static class CompanyManager
	{
		public enum CreateResult
		{
			Success,
			NotAllPropertiesSet,
			ValidationError,
			EmailAlreadyOccupied
		}

		public static CreateResult Create(
			Company company,
			PhoneBookContext context)
		{
			if (company == null)
			{
				throw new ArgumentNullException(
					nameof(company),
					"The company cannot be null.");
			}

			try
			{
				IRepository<Company> repo = new CompanyRepository(context);
				var dbCompany = repo.GetAll().FirstOrDefault(
					u => u.Email == company.Email);

				if (dbCompany != null)
				{
					return CreateResult.EmailAlreadyOccupied;
				}

				repo.Add(company);
			} catch (DbEntityValidationException exp)
			{
				return exp.EntityValidationErrors.Any(
					error => error.ValidationErrors.Any(
						e => e.ErrorMessage.Contains("required")))
					? CreateResult.NotAllPropertiesSet
					: CreateResult.ValidationError;
			}

			return CreateResult.Success;
		}
	}
}
