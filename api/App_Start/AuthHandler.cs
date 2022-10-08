using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace api.App_Start
{
    public class AuthHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                  CancellationToken cancellationToken)
        {
            HttpResponseMessage errorResponse = null;


            if (!request.RequestUri.ToString().Contains("/api/Account"))
            {
                try
                {
                    IEnumerable<string> authHeaderValues;
                    request.Headers.TryGetValues("Auth", out authHeaderValues);


                    if (authHeaderValues == null)
                        return Task.FromResult(request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized")); // cross fingers

                    var bearerToken = authHeaderValues.ElementAt(0);
                    var token = bearerToken;

                    //var secret = ConfigurationManager.AppSettings.Get("jwtKey");
                    var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

                    Thread.CurrentPrincipal = ValidateToken(
                        token,
                        secret,
                        true
                    );

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = Thread.CurrentPrincipal;
                    }
                    //Get the current claims principal
                    var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

                    // Get the claims values

                    var clientId = identity.Claims.Where(c => c.Type == ClaimTypes.Hash)
                        .Select(c => c.Value).SingleOrDefault();

                    

                    //request.Headers.Add("SessionId", clientId);
                }
                catch (SignatureVerificationException ex)
                {
                    errorResponse = request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
                }
                catch (Exception ex)
                {
                    errorResponse = request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }

            }
            return errorResponse != null
                ? Task.FromResult(errorResponse)
                : base.SendAsync(request, cancellationToken);
        }

        private static ClaimsPrincipal ValidateToken(string token, string secret, bool checkExpiration)
        {
            var jsonSerializer = new JavaScriptSerializer();

            //token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6InJvbnkwMTcxMjY1MTQxMkBnbWFpbC5jb20iLCJ1c2VySWQiOiIzZTY3NWY3My1kNWViLTQ0NDYtYWNkNi0zNmE2OGFlMDU2N2IiLCJyb2xlIjoyLCJleHAiOjE1MzI3Mjc2NTIuMH0.8mTr3WRrIN4_cd5F4mVpAVWOZi0KGhgtP0BgVaa2C9Y";

            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
           
            var json = decoder.Decode(token, secret, verify: true);

            //var payloadJson = JsonWebToken.Decode(token, secret);
            var payloadData = jsonSerializer.Deserialize<Dictionary<string, object>>(json);


            object o;
            if (payloadData != null && (checkExpiration && payloadData.TryGetValue("exp", out o)))
            {
                double d;

                if (o is IConvertible)
                {
                    d = ((IConvertible)o).ToDouble(null);
                }
                else
                {
                    d = 0d;
                }

                //var validTo = DateTime.FromOADate(d);
                ////var validTo = FromUnixTime(long.Parse(exp.ToString()));
                //if (DateTime.Compare(validTo, DateTime.UtcNow) <= 0)
                //{
                //    throw new Exception(
                //        string.Format("Token is expired. Expiration: '{0}'. Current: '{1}'", validTo, DateTime.UtcNow));
                //}
            }

            var subject = new ClaimsIdentity("Federation", ClaimTypes.Name, ClaimTypes.Role);

            var claims = new List<Claim>();

            
            return new ClaimsPrincipal(subject);
        }
    }
}