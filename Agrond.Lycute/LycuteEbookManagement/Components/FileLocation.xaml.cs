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

namespace LycuteEbookManagement.Components
{
    /// <summary>
    /// Interaction logic for FileLocation.xaml
    /// </summary>
    public partial class FileLocation : UserControl
    {
        MainWindow m;
        public FileLocation()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
        }
        //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        private void btn_GetFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Multiselect = false;
            //dlg.Filter = "";
            dlg.ShowDialog();
            if (dlg.FileName != "")
                txtFileLocation.Content = dlg.FileName;
        }
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (txtFileLocation.Content.ToString() != "")
            {
                this.Visibility = Visibility.Hidden;
            }
            else {
                Common.AlertDiag alert = new Common.AlertDiag();
                alert._strAlertNote = "Insert e-book location";
                alert.ShowInTaskbar = false;
                alert.WindowStyle = WindowStyle.ToolWindow;
                alert.CancelButton = Visibility.Hidden;
                alert.ShowDialog();
            
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            txtFileLocation.Content = "";
            //this.Visibility = Visibility.Hidden;
            m.loadMain(new Home());
        }
    }
}
