using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using WITU.Core.Model;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using WITU.API.UserManagement;
using WITU.API.UserManagement.Identity;
using WITU.API.UserManagement.Store;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace WITU.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly ICoreRepository _coreRepo;

        private readonly IAccountRepository _accountRepo;

        private readonly IOauthRepository _oauthRepo;

        public ApplicationUserManager UserManager { get; private set; }

       
        public SimpleAuthorizationServerProvider(ICoreRepository coreRepo, IAccountRepository accountRepo, IOauthRepository oauthRepo)
            : this(new ApplicationUserManager(new UserStore(accountRepo, coreRepo)))
        {
            _coreRepo = coreRepo;
            _accountRepo = accountRepo;
            _oauthRepo = oauthRepo;
        }

        public SimpleAuthorizationServerProvider(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                //context.Validated();
                context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            
                client = _oauthRepo.FindClient(context.ClientId);
            

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != WITU.API.Helper.Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";
            try
            {
                context.OwinContext.Response.Headers.Remove("Access-Control-Allow-Origin");
            }
            catch (Exception exception)
            {

              
            }
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            
           
                IdentityUser user = await UserManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                if (user.UserType == 2)
                {
                context.SetError("invalid_grant", "Access denied to students");
                return;
                }
            if (user.UserType == 3)
            {
                context.SetError("invalid_grant", "Access denied to students");
                return;
            }
            //Find the staff member
                var userNative = _coreRepo.Get<User>(user.Id);
                Instructor staff = _accountRepo.GetUserInstructor(userNative);
            
            

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //Add the staff id as the NameIdentifier Claim for the logged in staff member
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, staff.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, staff.Person.EmailAddress));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, staff.Person.TelephoneContact));
            identity.AddClaim(new Claim("sub", context.UserName));
            

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    { 
                        "userName", context.UserName
                    }
                    ,
                    { 
                        "staffId", staff.Id.ToString()
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            //remove claims and add new ones if necessary
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}