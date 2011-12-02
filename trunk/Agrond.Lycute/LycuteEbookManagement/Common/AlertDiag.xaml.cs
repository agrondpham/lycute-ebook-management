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
        public string _strAlertNote { get { return _strAlertNote; } set { lbl_Alert.Content = value; } }
        public Visibility CancelButton { get { return btnCancel.Visibility; } set { btnCancel.Visibility = value; image3.Visibility = value; } }
        //public void setStrAlertNote(string pStrAlertNote)
        //{
        //    _strAlertNote = pStrAlertNote; 
        //    lbl_Alert.Content = _strAlertNote;
        //}
        public bool result = false;
        //private static bool cancel=true;
        public AlertDiag()
        {
            InitializeComponent();
            //if(cancel==false)
            //{
            //    btnCancel.Visibility=Visibility.Hidden;
            //}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            this.Close();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            result = true;
            this.Close();
        }
    }
}
