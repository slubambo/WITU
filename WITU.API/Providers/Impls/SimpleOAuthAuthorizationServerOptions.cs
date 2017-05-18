using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.API.Providers.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;

namespace WITU.API.Providers.Impls
{
    public class SimpleOAuthAuthorizationServerOptions: IOAuthAuthorizationServerOptions
    {
        private readonly IOAuthAuthorizationServerProvider _provider;
        private readonly IAuthenticationTokenProvider _tokenProvider;

        public SimpleOAuthAuthorizationServerOptions(
            IOAuthAuthorizationServerProvider oAuthAuthorizationServerProvider,
            IAuthenticationTokenProvider tokenProvider)
        {
            _provider = oAuthAuthorizationServerProvider;
            _tokenProvider = tokenProvider;
        }

        public OAuthAuthorizationServerOptions GetOptions()
        {
            return new OAuthAuthorizationServerOptions()
            {
//#if DEBUG
                AllowInsecureHttp = true,
//#endif
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = _provider,
                RefreshTokenProvider = _tokenProvider
            };
        }
    }
}