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

namespace LycuteEbookManagement.Search
{
    /// <summary>
    /// Interaction logic for SearchResult.xaml
    /// </summary>
    public partial class SearchResult : UserControl
    {
        BookLib bus_book = new BookLib();
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
            viewer.BeginStoryboard((Storyboard)this.Resources["slideRightToLeft"]);
        }
        public void closeSide() {
            viewer.ItemsSource = null;
            //viewer.BeginStoryboard((Storyboard)this.Resources["slideLeftToRight"]);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Book> _book = bus_book.ShowAll();
            listview_Result.DataContext = _book;
            closeBookProperties();
        }

        private void listview_Result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book value = (Book)listview_Result.SelectedValue;
            if (value != null)
            {
                showBookProperties();
                setData(value);
            }
        }
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
            lbl_Title.Content = pBook.bok_Title;
            lbl_Author.Content = "";
            lbl_Edition.Content = "";
            lbl_Publisher.Content = "";
            lbl_Volume.Content = pBook.bok_Volume;
            lbl_Year.Content = pBook.bok_Year;
            if (pBook.bok_ImageURl != null && pBook.bok_ImageURl != "")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(pBook.bok_ImageURl, UriKind.Absolute);
                bi.EndInit();
                img_Cover.Source = bi;
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            showEditor();
        }
    }
}
