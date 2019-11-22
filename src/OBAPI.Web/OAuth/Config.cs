using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace OBAPI.Web.OAuth
{
	public static class Config
	{
		public static IEnumerable<ApiResource> Apis =>
			new List<ApiResource>
			{
				new ApiResource("OBAPI", "Open Banking API")
			};

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
				{
					ClientId = "client",

					AllowedGrantTypes = GrantTypes.ClientCredentials,

					ClientSecrets = { new Secret("secret".Sha256())},

					AllowedScopes = { "History", "Transactions" }
				}
			};
	}
}
