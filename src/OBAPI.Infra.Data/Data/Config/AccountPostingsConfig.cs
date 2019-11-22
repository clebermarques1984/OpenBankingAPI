using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OBAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OBAPI.Infra.Data.Config
{
	public class AccountPostingsConfig : IEntityTypeConfiguration<AccountPosting>
	{
		public void Configure(EntityTypeBuilder<AccountPosting> builder)
		{
			builder.Property(a => a.Amount)
				.HasColumnType("decimal (18, 2)");
		}
	}
}
