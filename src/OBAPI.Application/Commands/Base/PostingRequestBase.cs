using OBAPI.Application.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.Commands.Base
{
	public class PostingRequestBase : Validatable
	{
		public int UserId { get; set; }
		public int AccountNumber { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }

		public override void Validate() {}
	}
}
