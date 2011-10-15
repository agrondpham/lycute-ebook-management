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
        BookLib bus_book = new BookLib();
        Book _bookValue;
        string _strAuthor="";
        string _strTitle = "";
        string _strFileType = "";
        public SearchResult()
        {
            InitializeComponent();
        }
        //Change slide
        public void chanceSlide(string pData)
        {
            string data = pData;
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);
            XmlElement root = (XmlElement)xdoc.ChildNodes[0];
            XmlNodeList xnl = root.SelectNodes("Page");
            viewer.ItemsSource = xnl;
            viewer.BeginStoryboard((Storyboard)App.Current.Resources["slideRightToLeft"]);
        }
        public void closeSide() {
            viewer.ItemsSource = null;
            //viewer.BeginStoryboard((Storyboard)this.Resources["slideLeftToRight"]);
        }
        #region Event
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Book> _book = bus_book.Search(txt_Search.Text);
            listview_Result.DataContext = _book;
            closeBookProperties();
        }
        private void listview_Result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _bookValue= (Book)listview_Result.SelectedValue;
            if (_bookValue != null)
            {
                showBookProperties();
                setData(_bookValue);
            }
        }
        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            showEditor();
            LycuteEbookManagement.Ebook.Editor._book = _bookValue;
        }
        private void btn_Read_Click(object sender, RoutedEventArgs e)
        {
            string url = NameCreater.GetFileURL(NameCreater.GetFirstAuthor(_strAuthor), _strTitle, _strFileType);
            Process.Start(url);
        }        
        #endregion
        /// <summary>
        /// show Properties windown
        /// </summary>
        private void showBookProperties() {

            properties.Width = 386;
            properties.Height = 680;
        }
        private void closeBookProperties() {
            properties.Width = 0;
            properties.Height = 0;
        }
        private void showEditor() {
            string strSearchResult = @"<root>
                <Page Source='../Ebook/Editor.xaml'/>
            </root>";
            chanceSlide(strSearchResult);
        }
        private void setData(Book pBook)
        {

            ObservableCollection<Author> _authors = bus_book.ShowAuthorByBookID(pBook.bok_ID);
            string strAuthor = "";
            
            foreach (var a in _authors)
            {
                strAuthor = a.ath_Name + ";" + strAuthor;
            }
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

    }
}
