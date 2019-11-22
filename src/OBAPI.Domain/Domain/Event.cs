using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Domain
{
	public class Event : INotification
	{
		public DateTime Timestamp { get; private set; }

		protected Event()
		{
			Timestamp = DateTime.Now;
		}
	}
}
