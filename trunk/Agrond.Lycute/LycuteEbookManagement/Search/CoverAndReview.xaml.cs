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
using Agrond.Lycute.Bus;
using Agrond.Lycute.DAO;
using System.Windows.Threading;

namespace LycuteEbookManagement.Search
{
    /// <summary>
    /// Interaction logic for CoverAndReview.xaml
    /// </summary>
    public partial class CoverAndReview : UserControl
    {
        BookLib booklib = new BookLib();
        public static Book _SelectedBook;
        public static string _isbn;
        private string _imageURL;
        private string _review;
        MainWindow m;
        private DispatcherTimer loadTimer = new DispatcherTimer();
        public CoverAndReview()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
            this.Loaded += new RoutedEventHandler(loadCover);
            this.Loaded += new RoutedEventHandler(loadRevew);
            loadTimer.Interval = TimeSpan.FromSeconds(2);
            loadTimer.IsEnabled = false;
            loadTimer.Tick += loadTimer_Tick;
        }
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        private void loadTimer_Tick(object sender, EventArgs e)
        {
            loadingWait1.Visibility = Visibility.Hidden;
            loadTimer.IsEnabled = false;
        }
        private void loadCover(object sender, RoutedEventArgs e)
        {
            loadingWait1.Visibility = Visibility.Visible;
            loadTimer.IsEnabled = true;
            _imageURL = booklib.GetInternetCover(_isbn);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(_imageURL, UriKind.RelativeOrAbsolute);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            img_Cover.Source = bi;
        }
        private void loadRevew(object sender, RoutedEventArgs e)
        {
            loadingWait1.Visibility = Visibility.Visible;
            loadTimer.IsEnabled = true;
            _review = booklib.GetInternetReview(_isbn);
            txtReview.AppendText(_review);
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            if(cbx_Cover.IsChecked==true)
                _SelectedBook.bok_ImageURl = _imageURL;
            if(cbx_Description.IsChecked==true)
                _SelectedBook.bok_Review = _review;
            Ebook.Editor._book = _SelectedBook;
            Ebook.Editor.IsAddnewMode = true;
            Ebook.Editor._IsReload = true;
            m.loadMain(new Ebook.Editor());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
