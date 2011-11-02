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
using Agrond.Option;
using Agrond.Plus;

namespace LycuteEbookManagement.Setting
{
    /// <summary>
    /// Interaction logic for ConfigLocation.xaml
    /// </summary>
    public partial class ConfigLocation : UserControl
    {
        MainWindow m;
        public ConfigLocation()
        {
            InitializeComponent();
            tbx_Store.Text= LycuteApplication.GetLocationString();
            this.Loaded += new RoutedEventHandler(loadParent);
        }
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        private void btn_BrowserFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.ShowDialog();
            if (dlg.SelectedPath != "")
            tbx_Store.Text = dlg.SelectedPath;
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            StoreLocation store = new StoreLocation();
            LycuteApplication.SetLocation(tbx_Store.Text);
            store.CheckDatabase(tbx_Store.Text);
            DBHelper.ConfigDatabase();
            m.loadMain(new Home());
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            m.loadMain(new Home());
        }
    }
}
