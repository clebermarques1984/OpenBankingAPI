using MediatR;
using OBAPI.Domain;
using OBAPI.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Application.Commands.Credit
{
	public class Handler : IRequestHandler<Request, Result>
	{
		private readonly IMediatorHandler mediator;
		private readonly IAccountPostingWrite db;
		private readonly IAccountRead read;
		private readonly IUnitOfWork uow;

		public Handler(IMediatorHandler mediator, IAccountPostingWrite db, IAccountRead read, IUnitOfWork uow)
		{
			this.mediator = mediator;
			this.db = db;
			this.read = read;
			this.uow = uow;
		}

		public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
		{
			try
			{
				var account = await read.GetById(request.IdAccount);

				if (account == null)
					throw new Exception("Account not found");

				var posting = await db.AddPosting(request.IdAccount, request.Amount, request.Description);

				uow.Commit();

				await mediator.RaiseEvent(new Notification
				{
					IdPosting = posting.ID,
					IdAccount = posting.AccountID,
					Amount = posting.Amount,
					Description = posting.Description,
					Date = posting.Date
				}, cancellationToken);

				return Result.Ok;
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
