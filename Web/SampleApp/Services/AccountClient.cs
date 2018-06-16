using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Newtonsoft.Json;
using SimpleCommerce.Core.Domain;
using SimpleCommerce.Web.Models;

namespace SimpleCommerce.Services
{
    public static class AccountAPI
    {
        public static string SignIn => $"/Token/auth";
    }

    public class AccountClient : IAccountClient
    {
     
        private string _baseUri;
        private IRestClient _client;
        public AccountClient(string baseUri)
        {
            _baseUri = baseUri;
            _client = new RestClient(_baseUri);
        }


        public SignResult SignIn(SignInViewModel user)
        {
            var request = new RestSharp.RestRequest(AccountAPI.SignIn)
            {
                //JsonSerializer = new NewtonsoftJsonSerializer()
            };
            request.AddQueryParameter("userId", user.UserId);
            request.AddQueryParameter("password", user.Password);

            IRestResponse response = _client.Get(request);
            var apiResponse = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            var res = new SignResult()
            {
                Succeed = apiResponse.Success,
                ErrorMessage = apiResponse.Error
            };

            if (apiResponse.Success)
            {
                var authResult = JsonConvert.DeserializeObject<AuthenticateResultModel>(apiResponse.Result.ToString());
                res.User = new User()
                {
                    UserId = authResult.UserId
                };

                res.AccessToken = authResult.AccessToken;
            }
            return res;
        }
    }
}
