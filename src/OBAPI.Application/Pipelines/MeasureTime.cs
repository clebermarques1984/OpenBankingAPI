using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Application.Pipelines
{
	public class MeasureTime<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var stopWatch = Stopwatch.StartNew();
			var result = await next();
			var elapsed = stopWatch.Elapsed;
			Debug.WriteLine($"Elapse time of request {typeof(TRequest).FullName}: {elapsed}ms");
			return result;
		}
	}
}
