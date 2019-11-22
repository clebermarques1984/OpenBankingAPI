using System.Collections.Generic;

namespace OBAPI.Domain.Entities
{
	public class Branch
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }
		public int BankID { get; set; }

		public Bank Bank { get; set; }

		private readonly List<Customer> _customers = new List<Customer>();
		public IReadOnlyCollection<Customer> Customers => _customers.AsReadOnly();
	}
}
