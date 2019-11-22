using AutoMapper;
using OBAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Application.AutoMapper
{
	public class DebitViewModelToDomainMappingProfile : Profile
	{
		public DebitViewModelToDomainMappingProfile()
		{
			CreateMap<DebitViewModel, Commands.Debit.Request>();
		}
	}

	public class DebitDomainToViewModelMappingProfile : Profile
	{
		public DebitDomainToViewModelMappingProfile()
		{
			CreateMap<Commands.Debit.Request, DebitViewModel>();
		}
	}
}
