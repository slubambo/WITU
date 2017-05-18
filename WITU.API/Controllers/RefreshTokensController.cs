using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;

namespace WITU.API.Controllers
{
    [System.Web.Http.RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        
        private IOauthRepository _oauthRepo
        {
            get
            {
                return DependencyResolver.Current.GetService<IOauthRepository>();

            }
        }

        public RefreshTokensController()
        {

        }

        /// <summary>
        /// only user named “Admin” to execute the “Get” method and obtain all refresh tokens
        /// </summary>
        /// <returns>List of refresh tokens</returns>
        [System.Web.Http.Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_oauthRepo.GetAllRefreshTokens());
        }

        /// <summary>
        /// only user named “Admin” to execute the “Delete” method and delete a refresh token 
        /// accepts a query string named “tokenId”, this should contain the hashed value of the refresh token
        /// </summary>
        /// <param name="tokenId">hashed value of the refresh token</param>
        /// <returns>Sucess or Failure</returns>
        [System.Web.Http.Authorize(Users = "Admin")]
        [System.Web.Http.Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = _oauthRepo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }
    }
}