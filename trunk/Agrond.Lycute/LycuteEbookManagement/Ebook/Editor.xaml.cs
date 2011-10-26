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
using System.Windows.Media.Animation;

namespace LycuteEbookManagement.Ebook
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        #region variable
        public static Book _book { set; get; }
        BookLib booklib = new BookLib();
        private string _imageSource = "";
        MainWindow m;
        //bool IsInternetDataOpen;
        bool IsISBN;
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
            booklib.Edit(editedBook,strAuthor,_imageSource);
            //save review
            booklib.EditReview(LycuteApplication.GetLocationString()+ NameCreater.CreateLocation(NameCreater.AuthorStringToList(strAuthor)[0], editedBook.bok_Title, "review.xml"), textRange.Text);
            m.loadMain(new Home());
        }
        private void btn_getInfo_Click(object sender, RoutedEventArgs e)
        {

                Search.InternetSearch._IsISBN = IsISBN;
                Search.InternetSearch._keyword = GetDataToSearch();
                m.loadMain(new Search.InternetSearch()) ;
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
            //string strAuthor = booklib.ConvertAuthorObservableToString(booklib.ShowAuthor(_book.bok_ID));
            if (_book.Authors != null)
            {
                string strAuthor = booklib.ConvertAuthorObservableToString(_book.Authors);
                autoCompleteBox_Author.SetText(strAuthor);
                SetAuthor(strAuthor);
            }
            if (_book.Publisher != null)
            {
                autoCompleteBox_Publisher.SetText(_book.Publisher.pbl_Name);
                SetPublisher(_book.Publisher.pbl_Name);
            }

            txtISBN.Text = _book.bok_ISBN;
            txtTitle.Text = _book.bok_Title;
            txtEdition.Text = _book.bok_Edition.ToString();

            ucComBoxVolume.setText(_book.bok_Volume.ToString());
            ucComBoxYear.setText(_book.bok_Year.ToString());

            rankComponent1.setText(Rank.RankNumber(_book.bok_Rank));

            txtReview.AppendText(_book.bok_Review);
            if (_book.bok_ImageURl != ""&& _book.bok_ImageURl!=null)
                SetPic(_book.bok_ImageURl);
            else
                SetPic("pack://application:,,,/Images/no_picture_available.png");
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
            string strAuthor = booklib.ConvertAuthorObservableToString(booklib.ShowAuthor());
            autoCompleteBox_Author.SetData(strAuthor);
        }
        private void SetPublisher(string pStrPublisher)
        {
            //get list from database           
            string strPublisher = booklib.ConvertPublisherObservableToString(booklib.ShowPublisher());
            autoCompleteBox_Publisher.SetData(strPublisher);
        }
        //private void closeMenu()
        //{
        //    if (IsInternetDataOpen)
        //    {
        //        Storyboard HideMenuArea =
        //        this.TryFindResource("OnHideMenuArea") as Storyboard;
        //        if (HideMenuArea != null)
        //            HideMenuArea.Begin(InternetData);
        //        IsInternetDataOpen = false;
        //    }
        //}
        //private void openMenu()
        //{
        //    //if (IsInternetDataOpen == false)
        //    //{
        //        //internetSearch1.Visibility = Visibility.Visible;
        //        Storyboard ShowMenuArea =
        //        this.TryFindResource("OnShowMenuArea") as Storyboard;
        //        if (ShowMenuArea != null)
        //            ShowMenuArea.Begin(InternetData);
        //    //    IsInternetDataOpen= true;
        //    //}

        //}
        private string GetDataToSearch() {
            string keyword = "";
            if (txtISBN.Text != "")
            {
                keyword = txtISBN.Text;
                IsISBN = true;
            }
            else { 
                keyword=txtTitle.Text;
                keyword = keyword + " " + autoCompleteBox_Author.GetText();
                keyword = keyword + " " + txtEdition.Text;
                IsISBN = false;
            }
            return keyword;
        }
        #endregion
    }
}
