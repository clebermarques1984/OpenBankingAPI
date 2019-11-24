using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OBAPI.Domain.Entities;
using System.Reflection;

namespace OBAPI.Infra.Data
{
	public class 
		OBAPIContext : DbContext
	{
		public static readonly LoggerFactory _myLoggerFactory =
			new LoggerFactory(new[] {new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()});

		public DbSet<Bank> Banks { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountPosting> AccountPostings { get; set; }

		public OBAPIContext(DbContextOptions<OBAPIContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLoggerFactory(_myLoggerFactory);
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			builder.Entity<Bank>().HasData(new Bank { 
				ID = 1,
				Number = 123,
				Name = "OBAPI Bank"
			});

			builder.Entity<Branch>().HasData(new Branch {
				ID = 1,
				Number = 1000,
				BankID = 1,
				Name = "Brach One"
			},
			new Branch {
				ID = 2,
				Number = 2000,
				BankID = 1,
				Name = "Brach Two"
			});

			builder.Entity<Customer>().HasData(new Customer
			{
				ID = 1,
				Code = "32791181130",
				Name = "Alice Smith",
				BrachID = 2
			},
			new Customer
			{
				ID = 2,
				Code = "22691181130",
				Name = "Bob Smith",
				BrachID = 2
			});

			builder.Entity<Account>().HasData(new Account
			{
				ID = 1,
				Category = Domain.Category.Checking,
				Number = 818181,
				CustomerID = 1
			},
			new Account
			{
				ID = 2,
				Category = Domain.Category.Checking,
				Number = 616161,
				CustomerID = 2
			});

			builder.Entity<AccountPosting>().HasData(new AccountPosting
			{
				ID = 1,
				AccountID = 1,
				Date = System.DateTime.Now.AddDays(-10),
				Description = "Deposito em caixa",
				Amount = 1521
			},
			new AccountPosting {
				ID = 2,
				AccountID = 1,
				Date = System.DateTime.Now.AddDays(-3),
				Description = "Cobrança de Taxa",
				Amount = -11
			});

		}
	}
}
