using MediatR;
using OBAPI.Application.Bus;
using OBAPI.Domain;
using OBAPI.Domain.Entities;
using OBAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Application.Commands.Statement
{
	public class Handler : IRequestHandler<Request, Result>
	{
		private readonly IMediatorHandler mediator;
		private readonly IAccountPostingRead db;
		private readonly IAccountRead read;

		public Handler(IMediatorHandler mediator, IAccountPostingRead db, IAccountRead read)
		{
			this.mediator = mediator;
			this.db = db;
			this.read = read;
		}

		public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
		{
			try
			{
				var account = await read.GetByCustomerIdAndNumber(request.UserId, request.AccountNumber);

				if (account == null)
					throw new Exception("Account not found");

				var postings = await db.GetStatements(account.ID, request.FromDate, request.ToDate);

				var balance = await db.GetBalance(account.ID, request.FromDate.AddDays(-1));

				postings.Add(balance);

				var result = postings.AsQueryable().OrderBy(p => p.Date).ToList();

				await mediator.RaiseEvent(new Notification
				{
					AccountNumber = request.AccountNumber,
					StatementCount = result.Count,
					FromDate = request.FromDate,
					ToDate = request.ToDate
				}, cancellationToken);

				return new Result<List<AccountPosting>>(result);
			}
			catch (Exception ex)
			{
				var result = new Result();
				result.AddValidation(ex.Message);
				return result;
			}
		}
	}
}
