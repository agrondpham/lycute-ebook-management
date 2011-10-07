using System;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Windows.Media.Animation;
namespace LycuteEbookManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private static int oldSelectedIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            Agrond.Lycute.Bus.StoreLocation store=new Agrond.Lycute.Bus.StoreLocation();
            string strLocation = Agrond.Lycute.Bus.Application.GetLocationString();
            if (store.CheckDatabase(strLocation)==false)
            { 
                //alert direct wrong
                //choice new location
                //update databse
                
            }

        }

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    var findBookInfo = new BookSearch.FindBookInformation();
        //    findBookInfo.ShowDialog();
        //}
        protected override void  OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.None; 
                this.Topmost = true;
                //btn_minimize.Visibility = Visibility.Visible;
            }
            base.OnStateChanged(e);
        }

        private void btn_minimize_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.ThreeDBorderWindow;
            this.Height = 500;
            this.Width = 1000;
        }

        private void btn_ManagerPanel(object sender, MouseButtonEventArgs e)
        {
            //Common.Window1 alert = new Common.Window1();
            //alert.ShowActivated = false;
            //alert.ShowInTaskbar = false;
            //alert.ShowDialog();
            ////MainWindow mn = new MainWindow();
            //alert.Owner = this;



            string strSearchResult = @"<root>
                <Page Source='Ebook/3DWall.xaml'/>
            </root>";
            chanceSlide(strSearchResult);
        }
        public void chanceSlide(string pData){
            string data = pData;
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);
            XmlElement root = (XmlElement)xdoc.ChildNodes[0];
            XmlNodeList xnl = root.SelectNodes("Page");
            viewer.ItemsSource = xnl;
            viewer.BeginStoryboard((Storyboard)this.Resources["slideRightToLeft"]);
        }
        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string strSearchResult=@"<root>
                <Page Source='Search/SearchResult.xaml'/>
            </root>";
            chanceSlide(strSearchResult);
        }
        public void moveout()
        {
            viewer.ItemsSource = null;
            viewer.BeginStoryboard((Storyboard)this.Resources["slideRightToLeft"]);
        }

        private void btn_back(object sender, MouseButtonEventArgs e)
        {
            moveout();
        }

        private void btn_SettingPanel(object sender, MouseButtonEventArgs e)
        {
            //Common.Window1 alert = new Common.Window1();
            //alert.ShowActivated = false;
            //alert.ShowInTaskbar = false;
            //alert.ShowDialog();
            ////MainWindow mn = new MainWindow();
            //alert.Owner = this;



            string strSearchResult = @"<root>
                <Page Source='Setting/SettingPanel.xaml'/>
            </root>";
            chanceSlide(strSearchResult);
        }

   }
}
