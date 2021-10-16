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
using System.Web;
using System.Collections.Specialized;
using System.Net.Http;

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
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            HttpListener listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            System.IO.Stream output;
            while (true)
            {
                listener.Start();
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                //MessageBox.Show(request.QueryString.Count.ToString());

                string input = null;
                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    input = reader.ReadToEnd();
                }
                NameValueCollection coll = HttpUtility.ParseQueryString(input);

                try
                {
                    String vmName = coll["vmName"];
                    String? clipboard = coll["clipboard"];
                    callBack(new ClientData(vmName: vmName, clipboard: clipboard));
                }
                catch { }

                try
                {
                    String vmName = request.QueryString["vmName"];
                    //String? imageBase64 = request.QueryString["imageBase64"];
                    String? clipboard = request.QueryString["clipboard"];
                    callBack(new ClientData(vmName: vmName, clipboard: clipboard));
                }
                catch { }

                try
                {
                    HttpListenerResponse response = context.Response;
                    string responseString = "OK";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                }
                catch { }
            }
            output.Close();
            listener.Stop();
        }
    }
}
