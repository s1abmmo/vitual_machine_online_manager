using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Collections.Generic;
using TextCopy;
using System.IO;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            //var captureBmp = new Bitmap(1920, 1024, PixelFormat.Format32bppArgb);
            //using (var captureGraphic = Graphics.FromImage(captureBmp))
            //{
            //    captureGraphic.CopyFromScreen(0, 0, 0, 0, captureBmp.Size);
            //    System.IO.MemoryStream ms = new MemoryStream();
            //    captureBmp.Save(ms, ImageFormat.Jpeg);
            //    byte[] img = ms.ToArray();
            //    String base64 = Convert.ToBase64String(img);
            //    Console.WriteLine(base64);
            //}

            String clipboard = new TextCopy.Clipboard().GetText();

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

            Task task=SendRequest.POSTRequest(url, values);
            task.Wait();
            //Console.ReadKey();

        }
        public static String ConvertImageToBase64(Image img)
        {
            using (Image image = img)
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

    }
}
