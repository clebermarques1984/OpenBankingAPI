using AutoMapper;
using MediatR;
using OBAPI.Application.Bus;
using OBAPI.Domain;
using OBAPI.Domain.Entities;
using OBAPI.Domain.Interfaces;
using OBAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Application.Services
{
	public class AccountAppServices : IAccountAppServices
	{
		private readonly IMediatorHandler mediator;
		private readonly IMapper mapper;

		public AccountAppServices(IMediatorHandler mediator, IMapper mapper)
		{
			this.mediator = mediator;
			this.mapper = mapper;
		}

		public async Task<Result> GetStatements(StatementViewModel statement, CancellationToken cancellationToken = default)
		{
			var request = mapper.Map<Commands.Statement.Request>(statement);
			
			return await mediator.SendCommand(request, cancellationToken);
		}

		public async Task<Result> AddCredit(CreditViewModel credit, CancellationToken cancellationToken = default)
		{
			var request = mapper.Map<Commands.Credit.Request>(credit);

			return await mediator.SendCommand(request, cancellationToken);
		}

		public async Task<Result> AddDebit(DebitViewModel debit, CancellationToken cancellationToken = default)
		{
			var request = mapper.Map<Commands.Debit.Request>(debit);

			return await mediator.SendCommand(request, cancellationToken);
		}
	}
}
