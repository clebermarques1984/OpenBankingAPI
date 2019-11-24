using System.Collections.Generic;
using System.Linq;

namespace OBAPI.Domain.Entities
{
	public class 
		Account
	{
		public int ID { get; set; }
		public int Number { get; set; }
		public Category Category { get; set; }
		public int CustomerID { get; set; }
		
		public Customer Customer { get; set; }

		private readonly List<AccountPosting> _accountPostings = new List<AccountPosting>();
		public IReadOnlyCollection<AccountPosting> AccountPostings => _accountPostings.AsReadOnly();
	}
}
