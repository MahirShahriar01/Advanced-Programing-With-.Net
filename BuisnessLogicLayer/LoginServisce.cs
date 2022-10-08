using BuisnessLogicLayer.Model;
using DataAccessLayer;
using DataAccessLayer.Data;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer
{
    public class LoginService
    {
        ProfileRepo profileRepo;
        public LoginService()
        {
            profileRepo = new ProfileRepo();
        }
        private string CreateToken(SessionInformation sessionInformation)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiry = Math.Round((DateTime.UtcNow.AddHours(3) - unixEpoch).TotalSeconds);
            var issuedAt = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);
            var notBefore = Math.Round((DateTime.UtcNow.AddMonths(6) - unixEpoch).TotalSeconds);
            var payload = new Dictionary<string, object>
            {
                {"Email", sessionInformation.Email},
                {"SessionId", sessionInformation.SessionId},
                {"exp", expiry}
            };

            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
        }

        public LoginModel LoginCredentialVerification(LoginModel model)
        {
            LoginModel response = new LoginModel();
            Profile profile = profileRepo.GetByEmail(model.Email);

            if(profile !=null && profile.Password == model.Password)
            {
                var sessionInformation = new SessionInformation();
                sessionInformation.Email = profile.Email;
                response.AuthToken = CreateToken(sessionInformation);
                response.IsOperationSuccessful = true;
                return response;
            }
            else
            {
                response.Message = "User not found.";
                return response;
            }
           
        }
    }

    public class SessionInformation
    {
        public string Email { get;  set; }
        public string SessionId { get;  set; }
    }
}
