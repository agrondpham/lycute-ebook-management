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
        public FileLocation()
        {
            InitializeComponent();
        }
        //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        private void btn_GetFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Multiselect = false;
            dlg.ShowDialog();
            if (dlg.FileName != "")
                txtFileLocation.Text = dlg.FileName;
        }
    }
}
