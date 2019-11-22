using Microsoft.EntityFrameworkCore;
using OBAPI.Domain.Interfaces;
using OBAPI.Infra.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OBAPI.Infra.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly OBAPIContext context;
		protected readonly DbSet<T> DbSet;

		public Repository(OBAPIContext context)
		{
			this.context = context;
			DbSet = context.Set<T>();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await DbSet.FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await DbSet.ToListAsync();
		}

		public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}

		public async Task AddAsync(T entity)
		{
			await DbSet.AddAsync(entity);
		}

		public void Update(T entity)
		{
			context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(T entity)
		{
			DbSet.Remove(entity);
		}

		public async Task<int> CountAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).CountAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecification<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(DbSet.AsQueryable(), spec);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await context.SaveChangesAsync();
		}
		public void Dispose()
		{
			context.Dispose();
		}
	}
}
