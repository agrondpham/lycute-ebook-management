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
            if (_book != null)
            {
                load();
            }
            //ucComBoxYear.setText("1990");
        }
        BookLib bus_book = new BookLib();
        private void load() {
            ObservableCollection<Author> _authors = bus_book.ShowAuthorByBookID(_book.bok_ID);
            string strAuthor="";
            foreach (var a in _authors) {
                strAuthor = a.ath_Name + ";" + strAuthor;
            }
            //Author author=_book[0].Authors.;
            txtAuthor.Text = strAuthor;
            txtISBN.Text = _book.bok_ISBN;
            txtTitle.Text = _book.bok_Title;
            txtEdition.Text = _book.bok_Edition.ToString();
            txtPublisher.Text = "publisher";
            ucComBoxVolume.setText(_book.bok_Volume.ToString());
            ucComBoxYear.setText(_book.bok_Year.ToString());
            rankComponent1.setText(Rank.RankNumber(_book.bok_Rank));
            SetPic(_book.bok_ImageURl);

        }

        private void btnLoadPicture_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Multiselect = false;
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                SetPic(dlg.FileName);
            }
        }
        private void SetPic(string pStrPicURI) {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(pStrPicURI, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            img_Cover.Source = bi;
        }
    }
}
