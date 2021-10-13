using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Collections.Specialized;

namespace client
{
    class SendRequest
    {
        private static readonly HttpClient client = new HttpClient();
        public static async void POSTRequest(String url, Dictionary<string, string> param)
        {
            var content = new FormUrlEncodedContent(param);

            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
        }
    }
}
