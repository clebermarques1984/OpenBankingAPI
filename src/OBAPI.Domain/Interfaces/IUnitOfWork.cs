using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		bool Commit();
	}
}
