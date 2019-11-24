using OBAPI.Domain.Entities;
using OBAPI.Domain.Interfaces;
using OBAPI.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Infra.Data.Repository
{
	public class AccountRepository : Repository<Account>, IAccountRead
	{
		public AccountRepository(OBAPIContext context) : base(context)
		{
		}

		public async Task<Account> GetByCustomerIdAndNumber(int customerId, int accountNumber)
		{
			var spec = new AccountFilterSpecification(customerId, accountNumber);
			return (await ListAsync(spec)).SingleOrDefault();
		}
	}
}
