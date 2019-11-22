using System;

namespace OBAPI.Domain.ViewModels
{
	public class StatementViewModel
	{
		public int IdAccount { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
	}
}
