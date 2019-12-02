using IdentityServer4;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens; 

using System.Reflection;
using IdentityServer4.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using IdentityServer4.EntityFramework.DbContexts;
using System.Linq;
using IdentityServer4.EntityFramework.Mappers;

namespace OBAPI.IdentityServer
{
	public class Startup
	{
		public IWebHostEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public Startup(IWebHostEnvironment environment, IConfiguration configuration)
		{
			Environment = environment;
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			//string connectionString = Configuration.GetConnectionString("DefaultConnection");

			//var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			//var builder = services.AddIdentityServer(options =>
			//{
			//	options.Events.RaiseErrorEvents = true;
			//	options.Events.RaiseInformationEvents = true;
			//	options.Events.RaiseFailureEvents = true;
			//	options.Events.RaiseSuccessEvents = true;
			//	options.UserInteraction.LoginUrl = "/Account/Login";
			//	options.UserInteraction.LogoutUrl = "/Account/Logout";
			//	options.Authentication = new AuthenticationOptions()
			//	{
			//		CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
			//		CookieSlidingExpiration = true
			//	};
			//})
			//.AddConfigurationStore(options =>
			//{
			//	options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
			//})
			//.AddOperationalStore(options =>
			//{
			//	options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
			//	options.EnableTokenCleanup = true;
			//})
			//.AddTestUsers(TestUsers.Users);

			var builder = services.AddIdentityServer()
				.AddInMemoryIdentityResources(Config.Ids)
				.AddInMemoryApiResources(Config.Apis)
				.AddInMemoryClients(Config.Clients)
				.AddTestUsers(TestUsers.Users);

			builder.AddDeveloperSigningCredential();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// this will do the initial DB population
			//bool seed = Configuration.GetSection("Data").GetValue<bool>("Seed");
			//if (seed)
			//{
			//	InitializeDatabase(app);
			//	throw new Exception("Seeding completed. Disable the seed flag in appsettings");
			//}

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}

		private void InitializeDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

				var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
				context.Database.Migrate();
				if (!context.Clients.Any())
				{
					foreach (var client in Config.Clients)
					{
						context.Clients.Add(client.ToEntity());
					}
					context.SaveChanges();
				}

				if (!context.IdentityResources.Any())
				{
					foreach (var resource in Config.Ids)
					{
						context.IdentityResources.Add(resource.ToEntity());
					}
					context.SaveChanges();
				}

				if (!context.ApiResources.Any())
				{
					foreach (var resource in Config.Apis)
					{
						context.ApiResources.Add(resource.ToEntity());
					}
					context.SaveChanges();
				}
			}
		}
	}
}
