using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Domain.Interfaces
{
	public interface IRepository<T> : IDisposable where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task<int> CountAsync(ISpecification<T> spec);
		Task<int> SaveChangesAsync();
	}
}
