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
using vitual_machine_online_manager.View;

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

            listVitualMachine=SaveFile.LoadConfigs();

            listView.ItemsSource = listVitualMachine;

            Loop.Instance().Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    listView.Items.Refresh();
                });
            });

            String[] prefix = { "http://*:8889/" };
            ServerHttpListener.Instance().Run(prefix, (a) =>
            {
                if (a.vmName == "" || a.vmName == null)
                    return;

                int index = listVitualMachine.FindIndex(0, listVitualMachine.Count, x => x.name == a.vmName);
                //Check exist in list
                if (index == -1)
                {
                    listVitualMachine.Add(new VitualMachine(name: a.vmName, createByClient: true));

                    index = listVitualMachine.FindIndex(0, listVitualMachine.Count, x => x.name == a.vmName);

                    listVitualMachine[index].updateData(new ClipboardStore(a.clipboard, DateTime.Now));

                    this.Dispatcher.Invoke(() =>
                    {
                        listView.Items.Refresh();
                    });

                }
                else
                {
                    listVitualMachine[index].updateData(new ClipboardStore(a.clipboard, DateTime.Now));

                    this.Dispatcher.Invoke(() =>
                    {
                        listView.Items.Refresh();
                    });
                }
                SaveFile.SaveConfigs(listVitualMachine);
            });
        }

        public static List<VitualMachine> listVitualMachine = new List<VitualMachine>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            StackPanel s = button.Parent as StackPanel;
            String id = s.Uid;
            var vm= listVitualMachine.Find(e => e.name == id);
            vitual_machine_online_manager.View.Clipboard clipboard = new vitual_machine_online_manager.View.Clipboard(vm.listClipboard);
            clipboard.Show();
        }
    }

    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not DateTime)
                return "WAIT";

            TimeSpan duration = DateTime.Now.ToUniversalTime() - (DateTime)value;

            if (duration < Configs.Instance().distanceMaxTime)
                return "OK";

            return "ERROR";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not DateTime)
                return "---";

            TimeSpan duration = DateTime.Now.ToUniversalTime().AddHours(7) - (DateTime)value;
            return duration.ToString(@"dd\.hh\:mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

}
