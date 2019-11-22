using Microsoft.EntityFrameworkCore;
using OBAPI.Domain.Entities;
using OBAPI.Infra.Data.Repository;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OBAPI.Infra.Data.Tests
{
	public class RepositoryTests
	{
		private DbContextOptions<OBAPIContext> options;
		private readonly ITestOutputHelper output;
		private readonly OBAPIContext context;

		public RepositoryTests(ITestOutputHelper output)
		{
			this.output = output;
			var builder = new DbContextOptionsBuilder<OBAPIContext>()
									.UseInMemoryDatabase(databaseName: "dbTest");
			options = builder.Options;
			context = new OBAPIContext(options);
		}


		[Fact]
		public void Should_Create_New_Context()
		{
			//arrange

			//act

			//assert
			Log($"Assert that object is assignable from {nameof(OBAPIContext)} type");
			Assert.IsAssignableFrom<OBAPIContext>(context);
		}

		[Fact]
		public void Should_Create_New_Repository()
		{
			//arrange

			//act
			using var sut = new Repository<Bank>(context);

			//assert
			Log($"Assert that object is assignable from {nameof(Repository<Bank>)} type");
			Assert.IsAssignableFrom<Repository<Bank>>(sut);
		}

		[Fact]
		public void Should_Add_New_Entity()
		{
			//arrange
			using var sut = new Repository<Bank>(context);
			var bank = new Bank() { Name = "Bank Test" };

			//act
			Task.FromResult(sut.AddAsync(bank));
			var result = sut.SaveChangesAsync().Result;

			//assert
			Log($"Assert that {nameof(bank.ID)} is diferent from 0");
			Assert.True(bank.ID != 0, $"{nameof(bank.ID)} should be diferent from 0");

			Log($"Assert that SaveChangesAsync return is equal to 1");
			Assert.Equal(1, result);
		}

		[Fact]
		public void Should_Remove_Entity()
		{
			//arrange
			using var sut = new Repository<Bank>(context);
			var bank = AddBank(context);

			//act
			sut.Delete(bank);
			Task.FromResult(sut.SaveChangesAsync());
			var result = sut.GetByIdAsync(bank.ID).Result;

			//assert
			Log($"Assert that there is no bank with the current ID");
			Assert.Null(result);
		}

		[Fact]
		public void Shoud_Update_Entity()
		{
			//arrange
			using var sut = new Repository<Bank>(context);
			var bank = AddBank(context);
			var oldName = bank.Name;
			var newName = "new_name";
			bank.Name = newName;

			//act
			sut.Update(bank);
			Task.FromResult(sut.SaveChangesAsync());
			var result = sut.GetByIdAsync(bank.ID).Result;

			//assert
			Log($"Assert that name from {nameof(result)} is diferent from {nameof(oldName)}");
			Assert.NotEqual(oldName, result.Name);
			Log($"Assert that name from {nameof(result)} is equal {nameof(newName)}");
			Assert.Equal(newName, result.Name);
		}

		[Fact]
		public void Should_Return_List()
		{
			//arrange
			using var sut = new Repository<Bank>(context);
			var bank = AddBank(context);

			//act
			var result = sut.ListAllAsync().Result;

			//assert
			Log($"Assert that {nameof(sut.ListAllAsync)} return a list bigger than 0");
			Assert.True(result.Count > 0, $"{nameof(sut.ListAllAsync)} should return a list bigger than 0");

			//arrange
			var spec = new TestBankWithItemsSpecification(bank.ID);

			//act
			result = sut.ListAsync(spec).Result;

			//assert
			Log($"Assert that {nameof(sut.ListAsync)} return a list is equal to 1");
			Assert.True(result.Count == 1, $"{nameof(sut.ListAsync)} should return a list is equal to 1");
		}

		private Bank AddBank(OBAPIContext ctx)
		{
			var bank = new Bank() { Name = "Bank Test" };
			
			ctx.Banks.Add(bank);
			ctx.SaveChanges();

			return bank;
		}

		private void Log(string msg)
			=> output.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}\t{msg}");
	}
}
