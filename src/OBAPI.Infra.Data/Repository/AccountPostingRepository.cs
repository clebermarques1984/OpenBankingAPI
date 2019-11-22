using Microsoft.EntityFrameworkCore;
using OBAPI.Domain.Entities;
using OBAPI.Domain.Interfaces;
using OBAPI.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Infra.Data.Repository
{
	public class AccountPostingRepository : Repository<AccountPosting>, IAccountPostingRead, IAccountPostingWrite
	{
		public AccountPostingRepository(OBAPIContext context) : base(context)
		{
		}

		public async Task<List<AccountPosting>> GetStatements(int idAccount, DateTime fromDate, DateTime toDate)
		{
			var spec = new StatementFilterSpecification(idAccount, fromDate, toDate);
			return (await ListAsync(spec)).ToList();
		}

		public async Task<AccountPosting> GetBalance(int idAccount, DateTime toDate)
		{
			var balance = await DbSet.Where(p => p.AccountID == idAccount && p.Date <= toDate).SumAsync(a => a.Amount);

			var posting = new AccountPosting() {
				ID = 0,
				Amount = balance,
				Date = toDate,
				AccountID = idAccount,
				Description = $"Balance of {toDate.ToShortDateString()}"
			};

			return posting;
		}

		public async Task<AccountPosting> AddPosting(int idAccount, decimal amount, string description)
		{
			var posting = new AccountPosting { AccountID = idAccount, Amount = amount, Description = description, Date = DateTime.UtcNow };
			
			await AddAsync(posting);
			
			return posting;
		}
	}
}
