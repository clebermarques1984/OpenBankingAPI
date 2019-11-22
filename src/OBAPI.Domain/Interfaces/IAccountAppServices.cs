using OBAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Domain.Interfaces
{
	public interface IAccountAppServices
	{
		Task<Result> GetStatements(StatementViewModel statement, CancellationToken cancellationToken = default);
		Task<Result> AddDebit(DebitViewModel debit, CancellationToken cancellationToken = default);
		Task<Result> AddCredit(CreditViewModel credit, CancellationToken cancellationToken = default);
	}
}
