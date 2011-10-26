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
        public CoverAndReview()
        {
            InitializeComponent();
            loadCover();
            loadRevew();
        }
        private void loadCover() {
            string imageURL = booklib.GetInternetCover(_isbn);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(imageURL, UriKind.RelativeOrAbsolute);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            img_Cover.Source = bi;
        }
        private void loadRevew() {
            string review = booklib.GetInternetReview(_isbn);
            txtReview.AppendText(review);
        }
    }
}
