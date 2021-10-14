using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Collections.Generic;
using TextCopy;
using System.IO;
using System.Text;
using System.IO;
using System.Web;
using System.Collections.Specialized;

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
                System.IO.MemoryStream ms = new MemoryStream();
                captureBmp.Save(ms, ImageFormat.Jpeg);
                byte[] byteresized= Resize2Max50Kbytes(ms.ToArray());
                byte[] img = ms.ToArray();

                String base64 = Convert.ToBase64String(byteresized);
                Console.WriteLine(base64);

                String clipboard = new TextCopy.Clipboard().GetText();

                Console.WriteLine(clipboard);

            //    var values = new Dictionary<string, string>{
            //    {  "vmName", "1" },
            //    { "imageBase64", base64},
            //        { "clipboard", clipboard }
            //};

                SendRequest.POSTRequest("http://localhost:9999", img);

            }

            Console.ReadKey();

        }
        public static byte[] Resize2Max50Kbytes(byte[] byteImageIn)
        {
            byte[] currentByteImageArray = byteImageIn;
            double scale = 1f;

            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);

            while (currentByteImageArray.Length > 35000)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.05f;
            }

            return currentByteImageArray;
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
