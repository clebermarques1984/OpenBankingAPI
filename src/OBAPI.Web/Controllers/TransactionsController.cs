using System.Linq;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OBAPI.Domain;
using OBAPI.Domain.Interfaces;
using OBAPI.Domain.ViewModels;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace OBAPI.Web.Controllers
{
	[Authorize]
	//[Route("api/[controller]")]
	public class TransactionsController	: Controller
	{
		private readonly IAccountAppServices accountAppServices;
		private readonly ClaimsPrincipal caller;

		public TransactionsController(IAccountAppServices accountAppServices, IHttpContextAccessor httpContextAccessor)
		{
			caller = httpContextAccessor.HttpContext.User;
			this.accountAppServices = accountAppServices;
		}

		[HttpGet]
		[Route("api/extrato")]
		public async Task<IActionResult> GetHistory(DateTime from, DateTime to)
		{
			if (!caller.Identity.IsAuthenticated) return BadRequest("User not authenticated");

			// retrieve the user info
			var userId = int.Parse(caller.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
			var statement = new StatementViewModel {  IdAccount = userId, FromDate = from, ToDate = to };

			var result = await accountAppServices.GetStatements(statement, CancellationToken.None);

			return ValidationHandler(statement, result);
		}

		[HttpPost]
		[Route("api/saque")]
		public async Task<IActionResult> PostDebit(string description, decimal amount)
		{
			if (!caller.Identity.IsAuthenticated) return BadRequest("User not authenticated");
			
			// retrieve the user info
			var userId = int.Parse(caller.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
			var debit = new DebitViewModel { IdAccount = userId, Amount = amount, Description = description };

			var result = await accountAppServices.AddDebit(debit, CancellationToken.None);

			return ValidationHandler(debit, result);
		}

		[HttpPost]
		[Route("api/deposito")]
		public async Task<IActionResult> PostCredit(string description, decimal amount)
		{
			if (!caller.Identity.IsAuthenticated) return BadRequest("User not authenticated");

			// retrieve the user info
			var userId = int.Parse(caller.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
			var credit = new CreditViewModel { IdAccount = userId, Amount = amount, Description = description };

			var result = await accountAppServices.AddCredit(credit, CancellationToken.None);

			return ValidationHandler(credit, result);
		}

		private IActionResult ValidationHandler<TCommand>(TCommand command, Result result)
		{
			if (!result.HasValidation) return Ok(result);

			return BadRequest(result);
		}
	}
}
