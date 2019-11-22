using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Domain.ViewModels
{
	public class CreditViewModel
	{
		public int IdAccount { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
	}
}
