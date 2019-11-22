using OBAPI.Domain.Entities;
using OBAPI.Domain.Specifications;

namespace OBAPI.Infra.Data.Tests
{
	public class TestBankWithItemsSpecification : BaseSpecification<Bank>
	{
		public TestBankWithItemsSpecification(int BankID) : base(b => b.ID == BankID)
		{
			AddInclude(b => b.Branches);
		}
	}
}
