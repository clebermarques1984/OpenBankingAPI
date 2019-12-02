using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MvcClient.ViewModel
{
	[DataContract()]
	public class ExtratoViewModel
	{
		[DataMember()]
		public bool HasValidation { get; set; }

		[DataMember()]
		public List<string> Validations { get; set; }

		[DataMember()]
		public List<Postings> Data { get; private set; }
	}

	[DataContract()]
	public class Postings
	{
		[DataMember()]
		public int Id { get; set; }

		[DataMember()]
		public string Description { get; set; }

		[DataMember()]
		public DateTime Date { get; set; }

		[DataMember()]
		public decimal Amount { get; set; }

		[DataMember()]
		public int AccountID { get; set; }

		public string FormatDate
		{ 
			get => Date.ToString("dd/MM/yyyy");
		}

	}
}
