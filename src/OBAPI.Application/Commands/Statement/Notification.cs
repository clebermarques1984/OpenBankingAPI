using MediatR;
using OBAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.Commands.Statement
{
	public class Notification : Event
	{
		public int AccountID { get; set; }
		public int StatementCount { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		
	}
}
