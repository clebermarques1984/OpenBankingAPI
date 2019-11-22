using MediatR;
using OBAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Application.Bus
{
	public class InMemoryBus : IMediatorHandler
	{
		private readonly IMediator mediator;

		public InMemoryBus(IMediator mediator)
		{
			this.mediator = mediator;
		}

		public async Task<Result> SendCommand<T>(IRequest<T>  request, CancellationToken cancellationToken) where T : Result
		{
			return await mediator.Send(request, CancellationToken.None);
		}

		public Task RaiseEvent<T>(T notification, CancellationToken cancellationToken) where T : Event
		{
			// could save the event at the eventSource repository

			return mediator.Publish(notification, cancellationToken);
		}
	}
}
