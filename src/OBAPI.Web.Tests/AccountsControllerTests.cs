using System;
using Xunit;
using Xunit.Abstractions;

namespace OBAPI.Web.Tests
{
	public class AccountsControllerTests
	{
		private readonly ITestOutputHelper output;

		public AccountsControllerTests(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public void Should_Return_OK()
		{

		}

		private void Log(string msg)
			=> output.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}\t{msg}");

	}
}
