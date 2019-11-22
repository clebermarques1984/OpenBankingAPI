using Microsoft.Extensions.DependencyInjection;
using OBAPI.Application.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace OBAPI.Web.Extensions
{
	public static class AutoMapperSetup
	{
		public static void AddAutoMapperSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddAutoMapper();

			// Registering Mappings automatically only works if the 
			// Automapper Profile classes are in ASP.NET project
			AutoMapperConfig.RegisterMappings();
		}
	}
}
