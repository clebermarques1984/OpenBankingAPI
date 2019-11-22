using OBAPI.Domain.Entities;
using OBAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

		public async Task<Account> GetById(int id)
		{
			return await GetByIdAsync(id);
		}
	}
}
