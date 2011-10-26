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
using System.Windows.Shapes;
using Agrond.Lycute.Bus;
using System.Collections.ObjectModel;
using Agrond.Lycute.DAO;

namespace LycuteEbookManagement.BookSearch
{
    /// <summary>
    /// Interaction logic for FindBookInformation.xaml
    /// </summary>
    public partial class FindBookInformation : Window
    {
        public FindBookInformation()
        {
            InitializeComponent();
        }

        private void btn_SearchInfo_Click(object sender, RoutedEventArgs e)
        {
            BookLib booklib = new BookLib();
            ObservableCollection<Book> _book = booklib.GetInformation("Computer network",false);
            //Binding bind = new Binding();
            listView1.DataContext = _book;
        }
    }
}
