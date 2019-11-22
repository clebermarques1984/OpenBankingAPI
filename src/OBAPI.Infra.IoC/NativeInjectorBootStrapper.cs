using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OBAPI.Application.Bus;
using OBAPI.Application.Services;
using OBAPI.Domain;
using OBAPI.Domain.Interfaces;
using Statement = OBAPI.Application.Commands.Statement;
using Debit = OBAPI.Application.Commands.Debit;
using Credit = OBAPI.Application.Commands.Credit;
using Notifications = OBAPI.Application.Notifications;
using System;
using OBAPI.Infra.Data.Repository;
using OBAPI.Application.Pipelines;
using OBAPI.Infra.Data.UoW;

namespace OBAPI.Infra.IoC
{
	public class NativeInjectorBootStrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			
			
			// ASP.NET HttpContext dependency
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// Domain Bus (Mediator)
			services.AddScoped<IMediatorHandler, InMemoryBus>();
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MeasureTime<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommand<,>));

			// Application
			services.AddScoped<IAccountAppServices, AccountAppServices>();

			// Domain - Events
			services.AddScoped<INotificationHandler<Statement.Notification>, Notifications.StatementEventHandler>();

			// Domain - Commands
			services.AddScoped<IRequestHandler<Statement.Request, Domain.Result>, Statement.Handler>();
			services.AddScoped<IRequestHandler<Debit.Request, Domain.Result>, Debit.Handler>();
			services.AddScoped<IRequestHandler<Credit.Request, Domain.Result>, Credit.Handler>();

			//Infra - Data
			services.AddScoped<IAccountPostingRead, AccountPostingRepository>();
			services.AddScoped<IAccountPostingWrite, AccountPostingRepository>();
			services.AddScoped<IAccountRead, AccountRepository>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
