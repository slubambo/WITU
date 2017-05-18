using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using WITU.API.Models;
using WITU.API.TOTPHelpers.OTP;
using WITU.API.UserManagement.Identity;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using RestSharp;

namespace WITU.API.Controllers
{
    [RoutePrefix("api/PassCode")]
    public class PassCodeController : ApiController
    {
        private readonly IIdentityMessageService _smService;
        private readonly IOauthRepository _oauthRepo;

        public PassCodeController(IIdentityMessageService smsService, IOauthRepository oauthRepo)
        {
            _smService = smsService;
            _oauthRepo = oauthRepo;
        }

        /// <summary>
        /// Generates and sends passcode to authenticated invigilator via SMS
        /// Please not that the passcode expires after 5 minutes
        /// </summary>
        /// <param name="secret">Information supplied to API</param>
        /// <returns>Success or Failure</returns>
        [Authorize]
        [Route("")]
        public async Task<IHttpActionResult> Get(string secret)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var phone = identity.Claims.Where(c => c.Type == ClaimTypes.MobilePhone)
                   .Select(c => c.Value).SingleOrDefault();
            //TODO: Send SMS to the staff members' number above
            
            TOTP t = new TOTP(secret, 300);
            int pass = t.now();
            IdentityMessage message = new IdentityMessage() {Body = "passcode: "+ pass, Destination = "+256706430315", Subject = "Security Code"};       
            await _smService.SendAsync(message);
            PassBackModel p = new PassBackModel();
            p.message = "sent";
            return Ok(p);
        }

        /// <summary>
        /// Verifies passcode entered by invigilator into the mobile app
        /// </summary>
        /// <param name="p">Passcode entered by invigilator</param>
        /// <returns></returns>
        [Authorize]
        [Route("")]
        public IHttpActionResult Post([FromBody] PassCodeModel p)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            //string userId = User.Identity.GetUserId();
            PassCodeModel n = p;
            
            TOTP t = new TOTP(p.imei, 300);
            bool x = false;
            x = t.verify(n.passcode);
            if (x)
            {
                
                return Ok(new PassBackModel(){message ="Verified"});
            }
            else
            {
                return BadRequest("Invalid passcode");
            }

        }


    }
}
