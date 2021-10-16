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
using System.Windows.Shapes;
using System.Windows.Markup;
using vitual_machine_online_manager.Model;

namespace vitual_machine_online_manager.View
{
    /// <summary>
    /// Interaction logic for Clipboard.xaml
    /// </summary>
    public partial class Clipboard : Window
    {
        public Clipboard(List<ClipboardStore> listClipboardStore)
        {
            InitializeComponent();
            foreach (var clipboardStore in listClipboardStore)
            {
                ItemCLipboard itemClipboard = new ItemCLipboard(clipboardStore);
                main.Children.Add(itemClipboard);
            }
        }
    }
}
