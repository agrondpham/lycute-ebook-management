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
using System.Windows.Media.Animation;
using Agrond.ObjectLib;
using Agrond.DataAccess;
using Agrond.Validation;
using Agrond.Plus;
using Agrond.Option;

namespace LycuteEbookManagement.Ebook
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {   BookLib booklib = new BookLib();
        AuthorLib authorlib = new AuthorLib();
        PublisherLib publisherlib = new PublisherLib();

        #region variable
        public static Book _book { set; get; }
        public static Book _Internetbook { set; get; }
        public static bool _IsReload { get; set; }
        private static string _OldAuthor { get; set; }
        private static string _OldTitle { get; set; }
        private static string _OldEbookFile { get; set; }

        public static Boolean IsAddnewMode{get;set;}
        public static string filelocation{get;set;}
        //private static string _BookID = "";
        MainWindow m;
        //bool IsInternetDataOpen;
        bool IsISBN;
        #endregion
        
        #region Constructor
        public Editor()
        {
            InitializeComponent();
            if (!_IsReload&&IsAddnewMode)
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
            if (DataIsValidated())
            {
                string strAuthor = ValidationLib.DefaultData(autoCompleteBox_Author.Text);
                TextRange textRange = new TextRange(txtReview.Document.ContentStart, txtReview.Document.ContentEnd);
                Book editedBook;
                if (IsAddnewMode)
                {
                    if (fileLocation1.txtFileLocation.Text != "" || filelocation != "")
                    {

                        string filesource = "";
                        if (fileLocation1.txtFileLocation.Text != "")
                            filesource = fileLocation1.txtFileLocation.Text;
                        else
                            filesource = filelocation;
                        editedBook = AddData(new Book());
                        string[] arrayFileSource = fileLocation1.txtFileLocation.Text.Split('\\');
                        _OldEbookFile = arrayFileSource[arrayFileSource.Count() - 1];
                        booklib.Add(editedBook, strAuthor, textRange.Text, filesource, _OldEbookFile);
                    }
                }
                else
                {
                    editedBook = AddData(_book);
                    string oldDri = CreateOldDirectory(_OldAuthor, _OldTitle);
                    booklib.Edit(editedBook, strAuthor, oldDri, textRange.Text, _OldEbookFile);
                } m.loadMain(new Home());
                //Save Author
                //Save Publisher
            }
        }
        private void btn_getInfo_Click(object sender, RoutedEventArgs e)
        {
            //Keep the old name of ebook
            if (_book != null)
            {
                _OldEbookFile = _book.bok_Location;
                Search.InternetSearch._bookID = _book.bok_ID.ToString();
            }
            //setdata to pass
            Search.InternetSearch._keyword = GetDataToSearch();
            Search.InternetSearch._IsISBN = IsISBN;
            
            if (fileLocation1.txtFileLocation.Text != "")
                filelocation = fileLocation1.txtFileLocation.Text;
            
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
            string strAuthor="AgrondPham;";
            if (pBook.Authors != null)
            {
                strAuthor = AuthorLib.ToString(pBook.Authors);
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

            ucComBoxVolume.Text=pBook.bok_Volume.ToString();
            ucComBoxYear.Text=pBook.bok_Year.ToString();
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
            
            if (img_Cover.Source.ToString() != "pack://application:,,,/LycuteEbookManagement;component/Images/no_picture_available.png")
            pbook.bok_ImageURl = img_Cover.Source.ToString();
            pbook.bok_ID = pbook.bok_ID;
            
            pbook.bok_ISBN = txtISBN.Text;
            pbook.bok_Title = txtTitle.Text;
            //error if textbox has not data
            pbook.bok_Edition= Convert.ToInt32(txtEdition.Text);
            pbook.bok_Volume=Convert.ToInt32(ucComBoxVolume.Text);
            pbook.bok_Year=Convert.ToInt32(ucComBoxYear.Text);
            
            pbook.bok_Rank=rankComponent1.getText().ToString();
            
            // publisher
            if (autoCompleteBox_Publisher.Text == "" || autoCompleteBox_Publisher.Text.ToLower() == "unknow")
            {
                pbook.Publisher = publisherlib.Default();
            }
            else
            {
                pbook.Publisher = new Publisher { pbl_ID = "0", pbl_Name = autoCompleteBox_Publisher.Text };
            }

            //author
            if (autoCompleteBox_Author.Text == "" || autoCompleteBox_Author.Text.ToLower() == "unknow;")
            {
                ObservableCollection<Author> listAu = new ObservableCollection<Author>(pbook.Authors);
                listAu.Add(authorlib.Default());
            }
            else {
                string[] arrayAuthor = autoCompleteBox_Author.Text.Split(';');
                ObservableCollection<Author> listAu = new ObservableCollection<Author>(pbook.Authors);
                pbook.Authors.Clear();
                foreach (string strAu in arrayAuthor)
                {
                    
                    pbook.Authors.Add(new Author { ath_ID="0",ath_Name=strAu});
                }
            }
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
            string strAuthor = AuthorLib.ToString(authorlib.ShowAuthor());
            autoCompleteBox_Author.SetData(strAuthor);
        }
        private void SetPublisher(string pStrPublisher)
        {
            //get list from database           
            string strPublisher = PublisherLib.ToString(publisherlib.ShowPublisher());
            autoCompleteBox_Publisher.SetData(strPublisher);
            autoCompleteBox_Publisher.IsClearData = true;
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
            string oldDirectory = LycuteApplication.GetLocationString() + "\\" + Naming.CreateLocation(oldAuhthor, Naming.CreateName(oldTitle));
            return oldDirectory;
        }
        private bool DataIsValidated() {
            if (ValidationLib.IsNum(txtEdition.Text)==true
                && ValidationLib.IsNum(ucComBoxVolume.Text)==true
                && ValidationLib.IsNum(ucComBoxYear.Text)==true
                && ValidationLib.IsNull(txtTitle.Text)==false
                )
            {
                return true;
            }
            else
            {//alert

                DetectErrors();
                return false;
            }


        
        }
        private void DetectErrors() {
            if (ValidationLib.IsNull(txtTitle.Text) == true) {
                txtTitle.Background = Brushes.Red;
            }
            if (ValidationLib.IsNum(txtEdition.Text) == false) {
                txtEdition.Bground = Brushes.Red;
            }
            if (ValidationLib.IsNum(ucComBoxVolume.Text) == false) {
                ucComBoxVolume.Bground = Brushes.Red;
            }
            if (ValidationLib.IsNum(ucComBoxYear.Text) == false) {
                ucComBoxYear.Bground = Brushes.Red;
            }
        
        }
        #endregion
    }
}
