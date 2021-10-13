using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace vitual_machine_online_manager.Function
{
    public class SaveFile
    {
        public static void CreateFolderData()
        {
            string pathData = Path.Combine(Directory.GetCurrentDirectory(), "data");
            if (!new DirectoryInfo(pathData).Exists)
            {
                Directory.CreateDirectory(pathData);
            }
        }

        public static string CreateFolderNameVm(string nameVm)
        {
            string pathNameVm = Path.Combine(Directory.GetCurrentDirectory(), "data", nameVm);
            if (!new DirectoryInfo(pathNameVm).Exists)
            {
                Directory.CreateDirectory(pathNameVm);
            }
            return pathNameVm;
        }

        public static String SaveImageBase64(String base64, string nameVm)
        {
            //try
            //{
            CreateFolderData();
            String pathNameVm = CreateFolderNameVm(nameVm);
            DateTime timeVietnamese = DateTime.Now.ToUniversalTime().AddHours(7);
            Int32 unixTimestamp = GetUnixTimestamp(timeVietnamese);
            String pathFile = Path.Combine(pathNameVm, "dmm" + unixTimestamp.ToString() + ".jpg");
            MessageBox.Show(pathFile);
            MessageBox.Show(base64);
            try
            {
                SaveBase64ToImage(base64, pathFile);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return unixTimestamp + ".jpg";
            //}
            //catch { }
            //return null;
        }

        private static Image SaveBase64ToImage(string base64,String pathFile)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                MessageBox.Show("img.ToString()");
                image.Save(/*pathFile*/"concac.jpg",ImageFormat.Jpeg);
            }
            //image.Dispose();
            return image;
        }

        public static Int32 GetUnixTimestamp(DateTime unixTimeStamp)
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

    }
}
