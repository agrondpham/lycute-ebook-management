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

namespace LycuteEbookManagement.Common
{
    /// <summary>
    /// Interaction logic for AlertDiag.xaml
    /// </summary>
    public partial class AlertDiag : Window
    {
        private static string _strAlertNote;
        public void setStrAlertNote(string pStrAlertNote)
        {
            _strAlertNote = pStrAlertNote; 
            lbl_Alert.Content = _strAlertNote;
        }
        public AlertDiag()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
