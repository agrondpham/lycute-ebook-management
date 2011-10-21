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
using Agrond.Lycute.DAO;
using Agrond.Lycute.Bus;
using System.Diagnostics;

namespace LycuteEbookManagement.Ebook
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : UserControl
    {
        #region Variable
        Agrond.Lycute.Bus.BookLib booklib=new Agrond.Lycute.Bus.BookLib();
        public static Book _book{ set; get; }
        MainWindow m;
        #endregion

        #region construction
        public Detail()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
            LoadData();
        }
        #endregion
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        private void LoadData() {
            txt_Author.Text=booklib.ConvertAuthorObservableToString(_book.Authors);;
            txt_Edition.Text=_book.bok_Edition.ToString();
            txt_ISBN.Text=_book.bok_ISBN;
            txt_Language.Text=_book.Language.lng_Name;
            txt_Publisher.Text=_book.Publisher.pbl_Name;
            txt_Year.Text = _book.bok_Year.ToString();
            txt_Review.AppendText(_book.bok_Review);
            txt_Tag.Text=booklib.ConvertTagObservableToString(_book.Tags);
            txt_Title.Text=_book.bok_Title;
            txt_Volume.Text=_book.bok_Volume.ToString();

            SetPic(img_Rank, _book.bok_Rank);
            SetPic(img_Cover, _book.bok_ImageURl);
        }
        private void SetPic(Image img,string pStrPicURI)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(pStrPicURI, UriKind.RelativeOrAbsolute);
            //this code load image to cach for delete image.
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            img.Source = bi;
        }
        #region event
        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            //closeBookProperties();
            LycuteEbookManagement.Ebook.Editor._book = _book;
            m.loadMain(new Ebook.Editor());
        }

        private void btn_Read_Click(object sender, RoutedEventArgs e)
        {
            string url = NameCreater.GetFileURL(NameCreater.GetFirstAuthor(_strAuthor), _strTitle, _strFileType);
            Process.Start(url);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
