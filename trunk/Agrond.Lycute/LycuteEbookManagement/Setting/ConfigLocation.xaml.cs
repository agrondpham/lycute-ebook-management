using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LycuteEbookManagement.Setting
{
    /// <summary>
    /// Interaction logic for ConfigLocation.xaml
    /// </summary>
    public partial class ConfigLocation : UserControl
    {
        public ConfigLocation()
        {
            InitializeComponent();
        }

        private void btn_BrowserFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.ShowDialog();
            if (dlg.SelectedPath != "")
            tbx_Store.Text = dlg.SelectedPath;
        }
    }
}
