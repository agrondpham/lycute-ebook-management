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
        public static Book _Internetbook { set; get; }
        public static bool _IsReload { get; set; }
        private static string _OldAuthor { get; set; }
        private static string _OldTitle { get; set; }
        private static string _OldEbookFile { get; set; }
        BookLib booklib = new BookLib();
        public static Boolean IsAddnewMode{get;set;}
        //private static string _BookID = "";
        MainWindow m;
        private static string _oldDirectory="";
        //bool IsInternetDataOpen;
        bool IsISBN;
        #endregion
        
        #region Constructor
        public Editor()
        {
            InitializeComponent();
            if (IsAddnewMode)
            {
                fileLocation1.Visibility = Visibility.Visible;
                SetAuthor("");
                SetPublisher("");
            }
            else
            {
                fileLocation1.Visibility = Visibility.Hidden;
                Load(_book);
            }
            this.Loaded += new RoutedEventHandler(loadParent);
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
                //_imageSource = dlg.FileName;
            }
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string strAuthor = autoCompleteBox_Author.Text;
            TextRange textRange = new TextRange(txtReview.Document.ContentStart, txtReview.Document.ContentEnd);
            Book editedBook = AddData(_book);
            if (IsAddnewMode)
            {
                
                //booklib.Add(editedBook, strAuthor, textRange.Text);
            }
            else
            {
                string oldDri = CreateOldDirectory(_OldAuthor, _OldTitle);
                booklib.Edit(editedBook, strAuthor, oldDri, textRange.Text,_OldEbookFile);
            } m.loadMain(new Home());
            //Save Author
            //Save Publisher
        }
        private void btn_getInfo_Click(object sender, RoutedEventArgs e)
        {
            //setdata to pass
            //_BookID = _book.bok_ID.ToString();
            //_oldDirectory = LycuteApplication.GetLocationString() + "\\" + NameCreater.CreateLocation(ConvertData.ToList(ConvertData.ToString(pBook.Authors))[0], NameCreater.CreateName(pBook.bok_Title));
            _OldEbookFile = _book.bok_Location;
            Search.InternetSearch._IsISBN = IsISBN;
            Search.InternetSearch._keyword = GetDataToSearch();
            Search.InternetSearch._bookID = _book.bok_ID.ToString();
            m.loadMain(new Search.InternetSearch()) ;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            m.loadMain(new Home());
            //ResetData();
        }
        #endregion

        #region function
        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }

        private void Load(Book pBook) {
            //string strAuthor = booklib.ConvertAuthorObservableToString(booklib.ShowAuthor(_book.bok_ID));
            //_oldDirectory = LycuteApplication.GetLocationString() + "\\" + NameCreater.CreateLocation(ConvertData.ToList(ConvertData.ToString(pBook.Authors))[0], NameCreater.CreateName(pBook.bok_Title));
            string strAuthor="AgrondPham;";
            if (pBook.Authors != null)
            {
                strAuthor = ConvertData.ToString(pBook.Authors);
                autoCompleteBox_Author.Text= strAuthor;
                SetAuthor(strAuthor);
            }
            if (pBook.Publisher != null)
            {
                autoCompleteBox_Publisher.Text=pBook.Publisher.pbl_Name;
                SetPublisher(pBook.Publisher.pbl_Name);
            }

            txtISBN.Text = pBook.bok_ISBN;
            txtTitle.Text = pBook.bok_Title;
            txtEdition.Text = pBook.bok_Edition.ToString();

            ucComBoxVolume.setText(pBook.bok_Volume.ToString());
            ucComBoxYear.setText(pBook.bok_Year.ToString());
            rankComponent1.setText(Rank.RankNumber(pBook.bok_Rank));

            txtReview.AppendText(pBook.bok_Review);
            if (pBook.bok_ImageURl != ""&& pBook.bok_ImageURl!=null)
                SetPic(pBook.bok_ImageURl);
            else
                SetPic("pack://application:,,,/Images/no_picture_available.png");
            //set old for edit
            if (!_IsReload)
            {
                _OldAuthor = ConvertData.ToList(strAuthor)[0];
                _OldTitle = pBook.bok_Title;
                _OldEbookFile = pBook.bok_Location;
            }
        
        }

        private Book AddData(Book pbook) {
            pbook.bok_ImageURl = img_Cover.Source.ToString();
            pbook.bok_ID = Convert.ToInt32(pbook.bok_ID);
            pbook.bok_ISBN = txtISBN.Text;
            pbook.bok_Title = txtTitle.Text;
            //error if textbox has not data
            pbook.bok_Edition= Convert.ToInt32(txtEdition.Text);
            pbook.bok_Volume=Convert.ToInt32(ucComBoxVolume.getText());
            //
            //error not content location of file
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
            string strAuthor = ConvertData.ToString(booklib.ShowAuthor());
            autoCompleteBox_Author.SetData(strAuthor);
        }
        private void SetPublisher(string pStrPublisher)
        {
            //get list from database           
            string strPublisher = ConvertData.ToString(booklib.ShowPublisher());
            autoCompleteBox_Publisher.SetData(strPublisher);
        }
        private string GetDataToSearch() {
            string keyword = "";
            if (txtISBN.Text != "")
            {
                keyword = txtISBN.Text;
                IsISBN = true;
            }
            else { 
                keyword=txtTitle.Text;
                keyword = keyword + " " + autoCompleteBox_Author.Text;
                keyword = keyword + " " + txtEdition.Text;
                IsISBN = false;
            }
            return keyword;
        }
        private string CreateOldDirectory(string oldAuhthor, string oldTitle) {
            string oldDirectory = LycuteApplication.GetLocationString() + "\\" + NameCreater.CreateLocation(oldAuhthor, NameCreater.CreateName(oldTitle));
            return oldDirectory;
        }
        #endregion
    }
}
