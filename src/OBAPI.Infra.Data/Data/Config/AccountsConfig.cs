using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OBAPI.Domain;
using OBAPI.Domain.Entities;

namespace OBAPI.Infra.Data.Config
{
	public class AccountsConfig : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			var converter = new EnumToStringConverter<Category>();
			builder.Property(a => a.Category)
				.HasConversion(converter);
		}
	}
}
