using System;

namespace OBAPI.Domain.Entities
{
	public class AccountPosting
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public int AccountID { get; set; }
	}
}
