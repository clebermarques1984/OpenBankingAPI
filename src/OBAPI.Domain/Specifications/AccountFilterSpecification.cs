using OBAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OBAPI.Domain.Specifications
{
	public sealed class AccountFilterSpecification : BaseSpecification<Account>
	{
		public AccountFilterSpecification(int customerId, int accountNumber)
			: base(a => a.CustomerID == customerId && a.Number == accountNumber)
		{
		}
	}
}
