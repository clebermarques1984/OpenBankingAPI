using OBAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace OBAPI.Domain.Specifications
{
	public class StatementFilterSpecification : BaseSpecification<AccountPosting>
	{
		public StatementFilterSpecification(int idAccount
											,DateTime fromDate
											,DateTime toDate) : base(p => p.AccountID == idAccount &&
																	p.Date >= fromDate &&
																	p.Date <= toDate)
		{
		}
	}
}
