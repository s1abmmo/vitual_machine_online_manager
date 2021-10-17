using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vitual_machine_online_manager.Model;
using System.IO;
using Microsoft.Win32;

namespace vitual_machine_online_manager.Function
{
    public class GeneralFunction
    {
        public static String OpenDialog()
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                return fileDialog.FileName;
            }
            return null;
        }
        public static void AddNewList(string selectPath)
        {
            string[] list = File.ReadAllLines(selectPath);
            foreach (string item in list)
            {
                if (item != null || item != "")
                {
                    if (MainWindow.listVitualMachine.Find(e => e.name == item) == null)
                    {
                        MainWindow.listVitualMachine.Add(new VitualMachine(name: item));
                    }
                }
            }
            SaveFile.SaveConfigs(MainWindow.listVitualMachine);
        }
    }
}
