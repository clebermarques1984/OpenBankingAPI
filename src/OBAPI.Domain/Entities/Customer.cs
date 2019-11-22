using System.Collections.Generic;

namespace OBAPI.Domain.Entities
{
	public class Customer
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public int BrachID { get; set; }

		public Branch Branch { get; set; }

		private readonly List<Customer> _customers = new List<Customer>();
		public IReadOnlyCollection<Customer> CustomerCards => _customers.AsReadOnly();

	}
}
