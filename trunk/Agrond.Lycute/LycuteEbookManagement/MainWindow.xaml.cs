using System;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Agrond.Plus;
using Agrond.Option;
namespace LycuteEbookManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variable
        bool IsQuickMenuShow = false;
        bool IsBodyShow = false;
        //private static int oldSelectedIndex = 0;
        public static UIElement _Element=null;
        #endregion
        #region constructor
        public MainWindow()
        {
            InitializeComponent();
            StoreLocation store=new StoreLocation();
            if (!StoreLocation.IsDBConfiged())
            {
                //load config forms
                loadMain(new Setting.ConfigLocation());
            }
            else
            {
                store.CreateDatabase(LycuteOption._RootFolderDrection);
                DBHelper.ConfigDatabase();
                loadMain(_Element);
            }
        }
        protected override void  OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.None; 
                this.Topmost = true;
                //btn_minimize.Visibility = Visibility.Visible;
                closeMenu();
            }
            base.OnStateChanged(e);
        }
        #endregion
        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    var findBookInfo = new BookSearch.FindBookInformation();
        //    findBookInfo.ShowDialog();
        //}
        #region event
        private void btn_minimize_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.ThreeDBorderWindow;
            this.Topmost = false;
            this.Height = 500;
            this.Width = 1000;
            closeMenu();
        }       
        private void btn_home_Click(object sender, MouseButtonEventArgs e)
        {
            loadMain(new Home());
            closeMenu();
        }
        private void btn_CloseQuickMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsQuickMenuShow)
            {
                closeMenu();
            }
            else
            {
                openMenu();
            }
        }
        private void btn_close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        #endregion
        #region function
        //event open and close menu
        private void closeMenu() {
            if (IsQuickMenuShow)
            {
                Storyboard HideMenuArea =
                this.TryFindResource("OnHideMenuArea") as Storyboard;
                if (HideMenuArea != null)
                    HideMenuArea.Begin(quickMenu);
                IsQuickMenuShow = false;
                changeIconCloseMenu("pack://application:,,,/LycuteEbookManagement;component/Images/Components/navigate-down-icon_1.png");
            }
        }
        private void openMenu() {
            if (IsQuickMenuShow==false)
            {
                Storyboard ShowMenuArea =
                this.TryFindResource("OnShowMenuArea") as Storyboard;
                if (ShowMenuArea != null)
                    ShowMenuArea.Begin(quickMenu);
                IsQuickMenuShow = true;
                changeIconCloseMenu("pack://application:,,,/LycuteEbookManagement;component/Images/Components/navigate-up-icon_1.png");
            
            }

        }
        public void loadMain(UIElement pElement) {
            if (pElement == null)
            {
                pElement = new Home();
            }
            closeBody();
            bodyArea.Children.Clear();
            bodyArea.Children.Add(pElement);
            Canvas.SetLeft(pElement, 0);
            Canvas.SetTop(pElement, -120);
            openBody();
        }
        private void closeBody()
        {
            if (IsBodyShow)
            {
                Storyboard HideBodyArea =
                this.TryFindResource("OnHideBodyArea") as Storyboard;
                if (HideBodyArea != null)
                    HideBodyArea.Begin(quickMenu);
                IsBodyShow = false;
            }
        }
        private void openBody()
        {
            if (IsBodyShow == false)
            {
                Storyboard ShowBodyArea =
                this.TryFindResource("OnShowBodyArea") as Storyboard;
                if (ShowBodyArea != null)
                    ShowBodyArea.Begin(quickMenu);
                IsBodyShow = true;
            }

        }
        private void changeIconCloseMenu(string url) {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            btn_CloseQuickMenu.Source = bi;
        }
        #endregion


    }
}
