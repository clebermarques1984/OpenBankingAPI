using OBAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.Commands.Base
{
	public class PostingNotificationBase : Event
	{
		public int IdPosting { get; set; }
		public int AccountNumber { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
	}
}
