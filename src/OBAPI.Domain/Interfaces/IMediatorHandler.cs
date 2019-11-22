using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace OBAPI.Domain
{
	public interface IMediatorHandler
	{
		Task<Result> SendCommand<T>(IRequest<T> request, CancellationToken cancellationToken) where T : Result;
		Task RaiseEvent<T>(T notification, CancellationToken cancellationToken) where T : Event;
	}
}
