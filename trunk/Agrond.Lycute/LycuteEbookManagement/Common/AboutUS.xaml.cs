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

namespace LycuteEbookManagement.Common
{
    /// <summary>
    /// Interaction logic for AboutUS.xaml
    /// </summary>
    public partial class AboutUS : UserControl
    {
        MainWindow m;
        public AboutUS()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
        }
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            m.loadMain(new Home());
        }
    }
}
