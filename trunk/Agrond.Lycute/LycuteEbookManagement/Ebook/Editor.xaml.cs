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
using System.Collections.ObjectModel;
using Agrond.Lycute.DAO;
using Agrond.Lycute.Bus;

namespace LycuteEbookManagement.Ebook
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public static Book _book { set; get; }
        public Editor()
        {
            InitializeComponent();
            load();
            ucComBoxYear.setText("1990");
        }
        BookLib bus_book = new BookLib();
        private void load() {
            ObservableCollection<Author> _authors = bus_book.ShowAuthorByBookID(1);
            string strAuthor="";
            foreach (var a in _authors) {
                strAuthor = a.ath_Name + ";" + strAuthor;
            }
            //Author author=_book[0].Authors.;
            txtAuthor.Text = strAuthor;
            txtISBN.Text = _book.bok_ISBN;
            txtTitle.Text = _book.bok_Title;
            rankComponent1.setText(1);
        }
    }
}
