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
using Agrond.DataAccess;
using System.Diagnostics;
using Agrond.ObjectLib;
using Agrond.Plus;
using Agrond.Option;

namespace LycuteEbookManagement.Ebook
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : UserControl
    {
        #region Variable
        BookLib booklib=new BookLib();
        FileCopy copy = new FileCopy();
        public static Book _book{ set; get; }
        private string _OldAuthor = "";
        private string _OldTitle = "";
        private string pOldDirectory = "";
        MainWindow m;
        #endregion

        #region construction
        public Detail()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
            LoadData();
        }
        #endregion

        private void loadParent(object sender, RoutedEventArgs e)
        {
            m = (MainWindow)Window.GetWindow(this);
        }
        private void LoadData() {
            txt_Author.Text=AuthorLib.ToString(_book.Authors);
            txt_Edition.Text=_book.bok_Edition.ToString();
            txt_ISBN.Text=_book.bok_ISBN;
            txt_Language.Text=_book.Language.lng_Name;
            txt_Publisher.Text=_book.Publisher.pbl_Name;
            txt_Year.Text = _book.bok_Year.ToString();
            txt_Review.AppendText(_book.bok_Review);
            txt_Tag.Text = ConvertData.ToString(_book.Tags);
            txt_Title.Text=_book.bok_Title;
            txt_Volume.Text=_book.bok_Volume.ToString();

            SetPic(img_Rank, _book.bok_Rank);
            SetPic(img_Cover, _book.bok_ImageURl);

            //set value to create location for e-book
            string strAuthor = AuthorLib.ToString(_book.Authors);
            _OldAuthor = ConvertData.ToList(strAuthor)[0];
            _OldTitle = _book.bok_Title;
            pOldDirectory = CreateOldDirectory(_OldAuthor, _OldTitle);
        }
        private void SetPic(Image img,string pStrPicURI)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(pStrPicURI, UriKind.RelativeOrAbsolute);
            //this code load image to cach for delete image.
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            img.Source = bi;
        }

        #region event
        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            LycuteEbookManagement.Ebook.Editor._book = _book;
            LycuteEbookManagement.Ebook.Editor.IsAddnewMode = false;
            m.loadMain(new Ebook.Editor());
        }

        private void btn_Read_Click(object sender, RoutedEventArgs e)
        {
            string url = Naming.GetFileURL(Naming.GetFirstAuthor(AuthorLib.ToString(_book.Authors)), _book.bok_Title, _book.bok_Location);
            Process.Start(url);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            m.loadMain(new Home());
        }
        #endregion

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            //ask for sure to delete
            Common.AlertDiag alert = new Common.AlertDiag();
            alert._strAlertNote="Do you sure to delete this ebook.\nYou will can NOT restore this ebook for next time";
            alert.ShowInTaskbar = false;
            alert.WindowStyle = WindowStyle.ToolWindow;
            alert.ShowDialog();
            if (alert.result)
            {
                //remove the folder
                copy.Delete(pOldDirectory, "folder");
                //delete data in database
                booklib.Delete(_book.bok_ID);
                Common.AlertDiag alert1 = new Common.AlertDiag();
                alert1._strAlertNote = "E-book is deleted successfull";
                alert1.ShowInTaskbar = false;
                alert1.WindowStyle = WindowStyle.ToolWindow;
                alert1.CancelButton = Visibility.Hidden;
                alert1.ShowDialog();
                m.loadMain(new Home());
            }
        }
        private string CreateOldDirectory(string oldAuhthor, string oldTitle)
        {
            string oldDirectory = LycuteApplication.GetLocationString() + "\\" + Naming.CreateLocation(oldAuhthor, Naming.CreateName(oldTitle));
            return oldDirectory;
        }
    }
}
