using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using vitual_machine_online_manager.Model;
using System.Text.Json;
using Newtonsoft.Json;
using System.Dynamic;

namespace vitual_machine_online_manager.Function
{
    public class SaveFile
    {
        public static List<VitualMachine> LoadConfigs()
        {
            List<VitualMachine> listVitualMachine = new List<VitualMachine>();
            try
            {
                String[] configs = File.ReadAllLines(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "configs"));

                foreach (var config in configs)
                {
                    var vM = JsonConvert.DeserializeObject<dynamic>(config);
                    var listClipboard = JsonConvert.DeserializeObject<List<dynamic>>(Convert.ToString(vM.listClipboard));

                    List<ClipboardStore> listClipboardStore = new List<ClipboardStore>();
                    foreach (dynamic clipboard in listClipboard)
                    {
                        listClipboardStore.Add(new ClipboardStore(content: Convert.ToString(clipboard.content), timeUploaded: Convert.ToDateTime(clipboard.timeUploaded)));
                    }
                    VitualMachine newVm = new VitualMachine(Convert.ToString(vM.name));
                    newVm.loadFromStorage(lastTimePing: Convert.ToDateTime(vM.lastTimePing), listClipboard: listClipboardStore);
                    listVitualMachine.Add(newVm);
                }
            }
            catch { }
            return listVitualMachine;
        }

        public static void SaveConfigs(List<VitualMachine> listVitualMachine)
        {
            try
            {
                List<String> encode = new List<String>();
                foreach (var vm in listVitualMachine)
                {
                    dynamic flexible = new ExpandoObject();
                    flexible.name = vm.name;
                    flexible.lastTimePing = vm.lastTimePing.ToString();
                    flexible.clipboardContent = vm.listClipboard;
                    encode.Add(JsonConvert.SerializeObject(vm));
                }
                File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), "configs"), encode);
            }
            catch { }
        }

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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return unixTimestamp + ".jpg";
            //}
            //catch { }
            //return null;
        }

        private static Image SaveBase64ToImage(string base64, String pathFile)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                MessageBox.Show("img.ToString()");
                image.Save(/*pathFile*/"concac.jpg", ImageFormat.Jpeg);
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
