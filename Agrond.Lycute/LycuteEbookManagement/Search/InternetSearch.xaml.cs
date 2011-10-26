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
using System.Collections.ObjectModel;
using Agrond.Lycute.DAO;

namespace LycuteEbookManagement.Search
{
    /// <summary>
    /// Interaction logic for InternetSearch.xaml
    /// </summary>
    public partial class InternetSearch : UserControl
    {
        Book SelectedBook = new Book();
        BookLib booklib = new BookLib();
        public static string _keyword;
        public static bool _IsISBN;
        MainWindow m;
        public InternetSearch()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
            FindData();
        }
        private void FindData() {
            ObservableCollection<Book> _book = null;
            _book = booklib.GetInformation(_keyword, _IsISBN);
            listview_Result.DataContext = _book;
        }
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            m.loadMain(new Ebook.Editor());
        }

        private void btn_getInfo_Click(object sender, RoutedEventArgs e)
        {
            Ebook.Editor._book = SelectedBook;
            Search.CoverAndReview._isbn = SelectedBook.bok_ISBN;
            m.loadMain(new Search.CoverAndReview());
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBook = (Book)listview_Result.SelectedValue;
            //Ebook.Editor._book = SelectedBook;
        }
    }
}
