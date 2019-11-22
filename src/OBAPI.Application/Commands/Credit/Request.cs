using MediatR;
using OBAPI.Application.Commands.Base;
using OBAPI.Application.Domain;
using OBAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.Commands.Credit
{
	public class Request : PostingRequestBase, IRequest<Result>
	{
		public override void Validate()
		{
			if (Amount <= 0)
				AddNotification(nameof(Amount), $"{nameof(Amount)} must be positive");
		}
	}
}
