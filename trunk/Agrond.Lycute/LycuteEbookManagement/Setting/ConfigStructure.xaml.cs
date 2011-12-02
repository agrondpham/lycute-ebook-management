using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Windows.Media.Animation;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace LycuteEbookManagement.Setting
{
    /// <summary>
    /// Interaction logic for ChangeStructure.xaml
    /// </summary>
    public partial class ChangeStructure : UserControl
    {
        public ChangeStructure()
        {
            InitializeComponent();
        }
        public static string _strStructure;
        public void changeTextStrutureStore(string strStructure)
        {
            _strStructure = strStructure;
        }
        private void btn_ChoiceStructure_Click(object sender, RoutedEventArgs e)
        {

            Common.AlertDiag alert = new Common.AlertDiag();
            alert._strAlertNote="This change can be lead to the reload of application. \n It can get the time for change setting";
            alert.ShowInTaskbar = false;
            alert.WindowStyle = WindowStyle.ToolWindow;
            alert.ShowDialog();
           

            Setting.StructureStore configStructureDiag = new Setting.StructureStore();
            configStructureDiag.ShowInTaskbar = false;
            configStructureDiag.WindowStyle = WindowStyle.ToolWindow;
            configStructureDiag.ShowDialog();

            if (_strStructure != "" && _strStructure != null)
            txb_Structure.Text = _strStructure;
        }
    }
}
