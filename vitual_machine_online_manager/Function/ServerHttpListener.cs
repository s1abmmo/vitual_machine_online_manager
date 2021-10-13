using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using vitual_machine_online_manager.Model;
using System.Threading;
using System.Windows;
using System.IO;

namespace vitual_machine_online_manager.Function
{
    public class ServerHttpListener
    {
        private static readonly ServerHttpListener Self = new ServerHttpListener();
        private Thread thread;
        private ServerHttpListener() { }
        public static ServerHttpListener Instance()
        {
            return Self;
        }
        public void Run(string[] prefixes, Action<ClientData> callBack)
        {
            if (thread == null)
            {
                thread = new Thread(() => { HttpListening(prefixes, callBack); });
                thread.Start();
            }
        }

        public static void Dispose()
        {
            if (Self.thread != null)
            {
                try
                {
                    Self.thread.Abort();
                }
                catch { }
            }
        }

        private static void HttpListening(string[] prefixes, Action<ClientData> callBack)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            System.IO.Stream output;
            while (true)
            {
                listener.Start();
                Console.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                using (var reader = new StreamReader(request.InputStream,
                                     request.ContentEncoding))
                {
                    string text = reader.ReadToEnd();
                    MessageBox.Show(text);
                }

                MessageBox.Show("new request");
                MessageBox.Show(request.QueryString.Count.ToString());

                try
                {
                    String vmName = request.QueryString["vmName"];
                    String? imageBase64 = request.QueryString["imageBase64"];
                    String? clipboard = request.QueryString["clipboard"];
                    callBack(new ClientData(vmName: vmName, imageBase64: imageBase64, clipboard: clipboard));
                }
                catch { }

                //request.QueryString.Count;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = "OK";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
            }
            // You must close the output stream.
            output.Close();
            listener.Stop();
        }
    }
}
