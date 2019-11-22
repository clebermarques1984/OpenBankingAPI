using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.AutoMapper
{
	public class AutoMapperConfig

	{

		public static MapperConfiguration RegisterMappings()
		{
			return new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new StatementViewModelToDomainMappingProfile());
				cfg.AddProfile(new StatementDomainToViewModelMappingProfile());
				cfg.AddProfile(new DebitViewModelToDomainMappingProfile());
				cfg.AddProfile(new DebitDomainToViewModelMappingProfile());
				cfg.AddProfile(new CreditViewModelToDomainMappingProfile());
				cfg.AddProfile(new CreditDomainToViewModelMappingProfile());
			});
		}
	}
}
