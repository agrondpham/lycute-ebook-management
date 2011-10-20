using System;
using System.Collections.Generic;
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
using System.Xml;

namespace LycuteEbookManagement
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Home : UserControl
	{
        //Ma m = new MainWindow();
        Window parentWindow = null;
        MainWindow m;
		public Home()
		{
			this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
		}
        private void loadParent(object sender,RoutedEventArgs e) {
            parentWindow = Window.GetWindow(this);
            m = (MainWindow)parentWindow;
        }
        private void btn_ManagerPanel(object sender, MouseButtonEventArgs e)
        {
            m.loadMain(new Ebook._3DWall());
        }
        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            Search.SearchResult._strKeyword = tbx_Search.Text;
            m.loadMain(new Search.SearchResult());

        }

        private void btn_SettingPanel(object sender, MouseButtonEventArgs e)
        {
            m.loadMain(new Setting.SettingPanel());
        }

        private void btn_AddEbook(object sender, MouseButtonEventArgs e)
        {
            LycuteEbookManagement.Ebook.Editor._book = null;
            m.loadMain(new Ebook.Editor());
        }
	}
}