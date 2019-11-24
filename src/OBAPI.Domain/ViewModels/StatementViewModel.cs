using System;

namespace OBAPI.Domain.ViewModels
{
	public class StatementViewModel
	{
		public int UserId { get; set; }
		public int AccountNumber { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
	}
}
