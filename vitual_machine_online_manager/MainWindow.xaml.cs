using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vitual_machine_online_manager.Model;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using vitual_machine_online_manager.Function;

namespace vitual_machine_online_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var cc = new VitualMachine(name: "DMM");
            string jsonString = JsonSerializer.Serialize(cc);

            MessageBox.Show(jsonString);

            String[] prefix = { "http://localhost:9999/" };
            Server.HttpListening(prefix, (a) => {
                int index=listVitualMachine.FindIndex(0, listVitualMachine.Count, x => x.name == a.vmName);
                if (index == -1)
                {
                    listVitualMachine.Add(new VitualMachine(name: a.vmName));
                    //listVitualMachine[listVitualMachine.Count-1]
                }
            });
        }

        private static List<VitualMachine> listVitualMachine=new List<VitualMachine>();


    }
}
