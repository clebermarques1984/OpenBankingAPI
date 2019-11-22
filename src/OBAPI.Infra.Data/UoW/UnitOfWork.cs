using OBAPI.Domain.Interfaces;

namespace OBAPI.Infra.Data.UoW
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly OBAPIContext _context;

		public UnitOfWork(OBAPIContext context)
		{
			_context = context;
		}

		public bool Commit()
		{
			return _context.SaveChanges() > 0;
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
