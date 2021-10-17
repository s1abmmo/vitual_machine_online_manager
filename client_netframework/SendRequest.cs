using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace client_netframework
{
    class SendRequest
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task POSTRequest(String url, Dictionary<string, string> values)
        {

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync(); 
            
            Console.WriteLine(responseString);

        }
    }
}
