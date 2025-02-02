using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHDotNetCore.ConsoleApp2
{
    public class RestClientExample
    {
        private readonly RestClient _client;
        private readonly string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample(RestClient client)
        {
            _client = client;
        }

        public async Task Read()
        {
            var request = new RestRequest(_postEndpoint, Method.Get);
            var response = await _client.ExecuteAsync(request);
            if(response.IsSuccessStatusCode)
            {
                string jsonString = response.Content!;
                Console.WriteLine(jsonString);
            }
        }
    }
}
