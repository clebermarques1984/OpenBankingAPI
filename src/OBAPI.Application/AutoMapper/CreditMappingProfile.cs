using AutoMapper;
using OBAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.AutoMapper
{
	public class CreditViewModelToDomainMappingProfile : Profile
	{
		public CreditViewModelToDomainMappingProfile()
		{
			CreateMap<CreditViewModel, Commands.Credit.Request>();
		}
	}

	public class CreditDomainToViewModelMappingProfile : Profile
	{
		public CreditDomainToViewModelMappingProfile()
		{
			CreateMap<Commands.Credit.Request, CreditViewModel>();
		}
	}
}
