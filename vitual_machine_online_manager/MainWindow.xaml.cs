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

        }
    }
}
