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
using System.Xml;
using System.Windows.Media.Animation;
using Agrond.Lycute.Bus;
using System.Collections.ObjectModel;
using Agrond.Lycute.DAO;
using System.Windows.Threading;
using System.Diagnostics;

namespace LycuteEbookManagement.Search
{
    /// <summary>
    /// Interaction logic for SearchResult.xaml
    /// </summary>
    public partial class SearchResult : UserControl
    {
        #region variable
        
        Book _bookValue;
        string _strAuthor="";
        string _strTitle = "";
        string _strFileType = "pdf";
        //int _intBookSelectedID;
        public static string _strKeyword;
        bool IsPropertiesAreaShown = false;
        MainWindow m;
        BookLib booklib = new BookLib();
        
        #endregion

        #region constructor
        public SearchResult()
        {
            InitializeComponent();
            if (_strKeyword != null) {
                loadData(_strKeyword);
                tbx_Search.Text = _strKeyword;
            }
            this.Loaded += new RoutedEventHandler(loadParent);
        }
        #endregion

        #region Event
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            loadData(tbx_Search.Text);
            closeBookProperties();
        }

        private void listview_Result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _bookValue= (Book)listview_Result.SelectedValue;
            //_intBookSelectedID = _bookValue.bok_ID;
            if (_bookValue != null)
            {
                showBookProperties();
                setData(_bookValue);
            }
        }
        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            closeBookProperties();
            LycuteEbookManagement.Ebook.Editor._book = _bookValue;
            LycuteEbookManagement.Ebook.Editor.IsAddnewMode = false;
            m.loadMain(new Ebook.Editor());

        }
        private void btn_Detail_Click(object sender, RoutedEventArgs e)
        {
            closeBookProperties();
            LycuteEbookManagement.Ebook.Detail._book = _bookValue;
            m.loadMain(new Ebook.Detail());
        }
        private void btn_Read_Click(object sender, RoutedEventArgs e)
        {
            string url = NameCreater.GetFileURL(NameCreater.GetFirstAuthor(_strAuthor), _strTitle, _strFileType);
            Process.Start(url);
        }
        private void btn_CloseProperties_MouseDown(object sender, MouseButtonEventArgs e)
        {
            closeBookProperties();
        }

        #endregion

        #region function
        private void loadData(string pStrKeyword) {
            ObservableCollection<Book> _book = booklib.Search(pStrKeyword);
            listview_Result.DataContext = _book;
        }
        /// <summary>
        /// show Properties windown
        /// </summary>
        private void showBookProperties() {

            if (IsPropertiesAreaShown == false)
            {
                Storyboard HidePropertiesArea =
                    this.TryFindResource("OnShowPropertiesArea") as Storyboard;
                if (HidePropertiesArea != null)
                    HidePropertiesArea.Begin(properties);
            }
        }
        private void closeBookProperties() {
            if (IsPropertiesAreaShown == false)
            {
                Storyboard HidePropertiesArea =
                    this.TryFindResource("OnHidePropertiesArea") as Storyboard;
                if (HidePropertiesArea != null)
                    HidePropertiesArea.Begin(properties);
            }
        }
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        private void setData(Book pBook)
        {
            string strAuthor = AuthorLib.ToString(pBook.Authors);
            lbl_Author.Content = strAuthor;

            lbl_Title.Content = pBook.bok_Title;
            lbl_Edition.Content = pBook.bok_Edition;
            lbl_Publisher.Content = pBook.Publisher.pbl_Name;
            lbl_Volume.Content = pBook.bok_Volume;
            lbl_Year.Content = pBook.bok_Year;
            //pass variable;
            _strFileType = pBook.bok_Location;
            _strAuthor = strAuthor;
            _strTitle = pBook.bok_Title;
            if (pBook.bok_ImageURl != null && pBook.bok_ImageURl != "")
            {
                try
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(pBook.bok_ImageURl, UriKind.RelativeOrAbsolute);
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                    img_Cover.Source = bi;
                }
                catch (Exception e){
                    ///Error
                    //load default image when you do not find the image
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri("pack://application:,,,/Images/no_picture_available.png", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    img_Cover.Source = bi;
                    img_Cover.Stretch = Stretch.Uniform;
                    pBook.bok_ImageURl = "pack://application:,,,/Images/no_picture_available.png";
                }
            }
        }
        #endregion




    }
}
