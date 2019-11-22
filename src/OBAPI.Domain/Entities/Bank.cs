using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Domain.Entities
{
	public class Bank
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }

		private readonly List<Branch> _branches = new List<Branch>();
		public IReadOnlyCollection<Branch> Branches => _branches.AsReadOnly();
	}
}
