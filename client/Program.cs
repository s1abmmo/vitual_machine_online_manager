using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Collections.Generic;
using TextCopy;
using System.IO;
using System.Text;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var captureBmp = new Bitmap(1920, 1024, PixelFormat.Format32bppArgb);
            using (var captureGraphic = Graphics.FromImage(captureBmp))
            {
                captureGraphic.CopyFromScreen(0, 0, 0, 0, captureBmp.Size);
                captureBmp.Save(Path.Combine(Directory.GetCurrentDirectory(),"screen.jpg"), ImageFormat.Jpeg);
            }

            String base64 = ConvertImageToBase64(Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "screen.jpg")));

            Console.WriteLine(base64);

            String clipboard = new TextCopy.Clipboard().GetText();

            Console.WriteLine(clipboard);

            var values = new Dictionary<string, string>{
                {  "vmName", "1" },
                { "imageBase64", base64},
                    { "clipboard", clipboard }
            };

            SendRequest.POSTRequest("http://localhost:9999", values);

            Console.ReadKey();

        }

        public static String ConvertImageToBase64(Image img) {
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
