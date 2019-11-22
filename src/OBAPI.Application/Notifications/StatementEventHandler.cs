using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Statement = OBAPI.Application.Commands.Statement;

namespace OBAPI.Application.Notifications
{
	public class StatementEventHandler : INotificationHandler<Statement.Notification>
	{
		public Task Handle(Statement.Notification notification, CancellationToken cancellationToken)
		{
			// Save the notification at the Azure blob storage

			return Task.CompletedTask;
		}
	}
}
