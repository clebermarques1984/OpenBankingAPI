using OBAPI.Domain.Entities;
using OBAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Domain.Interfaces
{
	public interface IAccountPostingRead
	{
		Task<List<AccountPosting>> GetStatements(int idAccount, DateTime fromDate, DateTime toDate);

		Task<AccountPosting> GetBalance(int idAccount, DateTime toDate);
	}
}
