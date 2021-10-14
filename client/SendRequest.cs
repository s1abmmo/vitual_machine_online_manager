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
        public static async void POSTRequest(String url ,byte[] paramFileBytes)
        {
            //HttpContent fileStreamContent = new StreamContent(paramFileStream);
            HttpContent bytesContent = new ByteArrayContent(paramFileBytes);

            using (var formData = new MultipartFormDataContent())
            {
                //formData.Add(fileStreamContent, "file1", "file1");
                formData.Add(bytesContent, "imageBase64", "file");

                //var content = new FormUrlEncodedContent(param);

                var response = await client.PostAsync(url, formData);

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseString);
            }

        }
    }
}
