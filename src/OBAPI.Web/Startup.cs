using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OBAPI.Infra.Data;
using OBAPI.Infra.IoC;
using OBAPI.Web.Extensions;
using OBAPI.Web.OAuth;
using System.Reflection;

namespace OBAPI.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");

			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			// Add framework services.
			services.AddDbContext<OBAPIContext>(options => options.UseInMemoryDatabase("Memory"));
			
			//services.AddDbContext<OBAPIContext>(options => 
			//	options.UseSqlServer(connectionString, sql => 
			//		sql.MigrationsAssembly(migrationsAssembly)));

			services.AddControllers();

			services.AddAuthentication("Bearer")
		   .AddJwtBearer("Bearer", options =>
		   {
			   options.Authority = "http://localhost:5000";
			   options.RequireHttpsMetadata = false;

			   options.Audience = "OBAPI";
		   });

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Open Banking API", Version = "v1" });
			});

			services.AddAutoMapperSetup();

			// Adding MediatR for Domain Events and Notifications
			services.AddMediatR(typeof(Startup));

			// .NET Native DI Abstraction
			RegisterServices(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "OBAPI V1");
			});

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static void RegisterServices(IServiceCollection services)
		{
			// Adding dependencies from another layers (isolated from Presentation)
			NativeInjectorBootStrapper.RegisterServices(services);
		}
	}
}
