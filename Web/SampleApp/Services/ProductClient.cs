using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Newtonsoft.Json;
using SimpleCommerce.Core.Domain;
using SimpleCommerce.Core.Dto;

namespace SimpleCommerce.Services
{


    public static class ProductAPI
    {
        public static string Produtcts => $"api/products";
    }

    public class ProductClient : IProductClient
    {
        private string _baseUri;
        private IRestClient _client;

        public ProductClient(string baseUri)
        {
            _baseUri = baseUri;
            _client = new RestClient(_baseUri);
        }

       
        public List<ProductDto> GetAllProducts(string access_token)
        {
            var request = new RestSharp.RestRequest(ProductAPI.Produtcts)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
             
            request.AddHeader("Authorization", $"Bearer {access_token}");
            IRestResponse response = _client.Get(request);
            var apiResponse = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            
            if (apiResponse.Success)
            {
                var list = JsonConvert.DeserializeObject<List<ProductDto>>(apiResponse.Result.ToString());
                return list;
            }
            return new List<ProductDto>();
        }
    }
}
