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
        #region variable
        public static Book _book { set; get; }
        BookLib bus_book = new BookLib();
        private string _imageSource = "";
        MainWindow m;
        #endregion
        
        #region Constructor
        public Editor()
        {
            InitializeComponent();
            if (_book != null)
            {
                load();
            }
            else { 
                SetAuthor("");
                SetPublisher("");
            }
            this.Loaded += new RoutedEventHandler(loadParent);

            //ucComBoxYear.setText("1990");
        }
        #endregion

        #region event
        private void btnLoadPicture_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Multiselect = false;
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                SetPic(dlg.FileName);
                _imageSource = dlg.FileName;
            }
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string strAuthor=autoCompleteBox_Author.GetText();
            TextRange textRange = new TextRange(txtReview.Document.ContentStart, txtReview.Document.ContentEnd);
            
            Book editedBook = AddData(_book);
            bus_book.Edit(editedBook,strAuthor,_imageSource);
            //save review
            bus_book.EditReview(LycuteApplication.GetLocationString()+ NameCreater.CreateLocation(NameCreater.AuthorStringToList(strAuthor)[0], editedBook.bok_Title, "review.xml"), textRange.Text);
            m.loadMain(new Home());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            m.loadMain(new Home());
        }
        #endregion

        #region function
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }

        private void load() {
            //string strAuthor = bus_book.ConvertAuthorObservableToString(bus_book.ShowAuthor(_book.bok_ID));
            string strAuthor = bus_book.ConvertAuthorObservableToString(_book.Authors);
            //txtAuthor.Text = strAuthor;
            txtISBN.Text = _book.bok_ISBN;
            txtTitle.Text = _book.bok_Title;
            txtEdition.Text = _book.bok_Edition.ToString();
            //txtPublisher.Text = _book.Publisher.pbl_Name;
            ucComBoxVolume.setText(_book.bok_Volume.ToString());
            ucComBoxYear.setText(_book.bok_Year.ToString());
            rankComponent1.setText(Rank.RankNumber(_book.bok_Rank));
            txtReview.AppendText(_book.bok_Review);
            SetPic(_book.bok_ImageURl);
            SetAuthor(strAuthor);
            SetPublisher(_book.Publisher.pbl_Name);
        }
        private Book AddData(Book pbook) {
            pbook.bok_ISBN = txtISBN.Text;
            pbook.bok_Title = txtTitle.Text;
            pbook.bok_Edition= Convert.ToInt32(txtEdition.Text);
            pbook.bok_Volume=Convert.ToInt32(ucComBoxVolume.getText());
            pbook.bok_Year=Convert.ToInt32(ucComBoxYear.getText());
            pbook.bok_Rank=rankComponent1.getText().ToString();
            // lost publisher
            return pbook;
        }
        
        private void SetPic(string pStrPicURI) {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(pStrPicURI, UriKind.RelativeOrAbsolute);
            //this code load image to cach for delete image.
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            img_Cover.Source = bi;
        }
        private void SetAuthor(string pStrAuthor) { 
            //get list from database
            autoCompleteBox_Author.SetText(pStrAuthor);
            string strAuthor = bus_book.ConvertAuthorObservableToString(bus_book.ShowAuthor(0));
            autoCompleteBox_Author.SetData(strAuthor);
        }
        private void SetPublisher(string pStrPublisher)
        {
            //get list from database           
            autoCompleteBox_Publisher.SetText(pStrPublisher);
            //string strPublisher = bus_book.ConvertPublisherObservableToString(bus_book.ShowAuthor(0));
            autoCompleteBox_Publisher.SetData(pStrPublisher);
        }

        /// <summary>
        /// same function on SearchResult
        /// </summary>
        /// <param name="pAuthors"></param>
        /// <returns></returns>

        #endregion

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
