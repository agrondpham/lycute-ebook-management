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
using Agrond.Lycute.DAO;

namespace LycuteEbookManagement.Ebook
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : UserControl
    {
        public Detail()
        {
            InitializeComponent();
        }
        private void LoadData(Book pBook) { 
            //txt_Author.Text=pBook.;
            txt_Edition.Text=pBook.bok_Edition.ToString();
            txt_ISBN.Text=pBook.bok_ISBN;
            txt_Language.Text=pBook.Language.lng_Name;
            txt_Publisher.Text=pBook.Publisher.pbl_Name;
            //txt_Review.=;
            //txt_Tag.Text=;
            txt_Title.Text=pBook.bok_Title;
            txt_Volume.Text=pBook.bok_Volume.ToString();
            //img_Cover=;
            //img_Rank=;
        }
    }
}
