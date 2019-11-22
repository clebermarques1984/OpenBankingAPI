using OBAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient.ViewModel
{
	public class ExtratoViewModel
	{
		public bool hasValidation { get; set; }
		public List<string> validations { get; set; }
		public List<Postings> data { get; private set; }

	}

	public class Postings
	{
		public int id { get; set; }
		public string description { get; set; }
		public DateTime date { get; set; }
		public decimal amount { get; set; }
		public int accountID { get; set; }
	}
}
