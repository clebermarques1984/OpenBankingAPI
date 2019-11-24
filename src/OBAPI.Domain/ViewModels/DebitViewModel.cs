using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Domain.ViewModels
{
	public class DebitViewModel
	{
		public int UserId { get; set; }
		public int AccountNumber { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
	}
}
