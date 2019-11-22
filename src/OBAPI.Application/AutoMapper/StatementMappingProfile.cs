using AutoMapper;
using OBAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.AutoMapper
{
	public class StatementViewModelToDomainMappingProfile : Profile
	{
		public StatementViewModelToDomainMappingProfile()
		{
			CreateMap<StatementViewModel, Commands.Statement.Request>();
		}
	}

	public class StatementDomainToViewModelMappingProfile : Profile
	{
		public StatementDomainToViewModelMappingProfile()
		{
			CreateMap<Commands.Statement.Request, StatementViewModel>();
		}
	}
}
