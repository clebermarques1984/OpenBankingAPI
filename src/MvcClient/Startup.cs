using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MvcClient
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = "Cookies";
				options.DefaultChallengeScheme = "oidc";
			})
				.AddCookie("Cookies")
				.AddOpenIdConnect("oidc", options =>
				{
					options.Authority = "http://localhost:5000";
					options.RequireHttpsMetadata = false;

					options.ClientId = "mvc";
					options.ClientSecret = "secret";
					options.ResponseType = "code";

					options.SaveTokens = true;
					options.GetClaimsFromUserInfoEndpoint = true;

					options.Scope.Add("openid");
					options.Scope.Add("profile");
					options.Scope.Add("email");
					options.Scope.Add("account");
					options.Scope.Add("OBAPI");
					options.Scope.Add("offline_access");
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseRouting();
			
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute()
					.RequireAuthorization();
			});
		}
	}
}
