// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace OBAPI.IdentityServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> Ids =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				new IdentityResource(
					name: "account",
					displayName: "Checking Account Number",
					claimTypes: new [] { "account_number" }
				),
			};

		public static IEnumerable<ApiResource> Apis =>
			new ApiResource[] 
			{ 
				new ApiResource
				{
					Name = "OBAPI",
					DisplayName = "Open Banking API",
					UserClaims = { "account_number" },
					Scopes = { new Scope("OBAPI") }
					
				}
			};
		
		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
				{
					ClientId = "client",

					AllowedGrantTypes = GrantTypes.ClientCredentials,

					ClientSecrets = { new Secret("secret".Sha256())},

					AllowedScopes = { "OBAPI" }
				},
				// interactive ASP.NET Core MVC client
				new Client
				{
					ClientId = "mvc",
					ClientName = "MVC Client",
					ClientSecrets = { new Secret("secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,
					RequireConsent = true,
					RequirePkce = true,
				
					// where to redirect to after login
					RedirectUris = { "http://localhost:5002/signin-oidc" },

					// where to redirect to after logout
					PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"account",
						"OBAPI",
					},

					AllowOfflineAccess = true
				}
			};

	}
}