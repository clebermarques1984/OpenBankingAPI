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

		public Handler(IMediatorHandler mediator, IAccountPostingRead db)
		{
			this.mediator = mediator;
			this.db = db;
		}

		public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
		{
			try
			{
				var postings = await db.GetStatements(request.IdAccount, request.FromDate, request.ToDate);

				var balance = await db.GetBalance(request.IdAccount, request.FromDate.AddDays(-1));

				postings.Add(balance);

				var result = postings.AsQueryable().OrderBy(p => p.Date).ToList();

				await mediator.RaiseEvent(new Notification
				{
					AccountID = request.IdAccount,
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
