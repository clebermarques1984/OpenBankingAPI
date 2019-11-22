using OBAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Domain.Interfaces
{
	public interface IAccountRead
	{
		Task<Account> GetById(int id);
	}
}
