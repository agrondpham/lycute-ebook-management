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
using System.Windows.Shapes;

namespace LycuteEbookManagement.Setting
{
    /// <summary>
    /// Interaction logic for StructureStore.xaml
    /// </summary>
    public partial class StructureStore : Window
    {
        public StructureStore()
        {
            InitializeComponent();
        }
        public string strStructure = "";
        Setting.ChangeStructure changeStrt = new ChangeStructure();
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //changeStrt.clearViewer();
            //this.Visibility = Visibility.Hidden;
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            string strStructure="";
            if (rdb_AE.IsChecked == true)
                strStructure = "@{Athour}/{Title}";
            else if (rdb_EA.IsChecked == true)
                strStructure = "@{Title}/{Author}";
            else if (rdb_E.IsChecked == true)
                strStructure = "@{Title}";
            else strStructure = "Are you fucking me?";
            changeStrt.changeTextStrutureStore(strStructure);
            //changeStrt.txb_Structure.DataContext = strStructure;
            //settingPanel._strStructure = strStructure;
            //this.Visibility = Visibility.Hidden;
            this.Close();
        }
    }
}
