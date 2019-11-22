using MediatR;
using OBAPI.Application.Domain;
using OBAPI.Domain;
using System;

namespace OBAPI.Application.Commands.Statement
{
	public class Request : Validatable, IRequest<Result>
	{
		public int IdAccount { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

		public override void Validate()
		{
			if (ToDate.CompareTo(FromDate) < 0)
				AddNotification(nameof(ToDate), $"{nameof(FromDate)} must be later than {nameof(ToDate)}");
		}
	}
}
