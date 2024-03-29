﻿using System;
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
using Agrond.DataAccess;
using System.Windows.Threading;
using Agrond.ObjectLib;

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
        public static string _bookID;
        MainWindow m;
        private DispatcherTimer loadTimer = new DispatcherTimer();
        public InternetSearch()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(loadParent);
            this.Loaded += new RoutedEventHandler(FindData);
            loadTimer.Interval = TimeSpan.FromSeconds(2);
            loadTimer.IsEnabled = false;
            loadTimer.Tick += loadTimer_Tick;
        }
        private void loadTimer_Tick(object sender, EventArgs e)
        {
            loadingWait1.Visibility = Visibility.Hidden;
            loadTimer.IsEnabled = false;
        }
        private void FindData(object sender, RoutedEventArgs e)
        {
            loadTimer.IsEnabled = true;
            loadingWait1.Visibility = Visibility.Visible;
            ObservableCollection<Book> _book = null;
            _book = booklib.GetInformation(_keyword, _IsISBN);
            listview_Result.DataContext = _book;
            txt_KeywordValue.Text = _keyword;
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
            Search.CoverAndReview._isbn = SelectedBook.bok_ISBN;
            //add real id to book
            SelectedBook.bok_ID = _bookID;
            Search.CoverAndReview._SelectedBook=SelectedBook;
            m.loadMain(new Search.CoverAndReview());
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedBook = (Book)listview_Result.SelectedValue;
            
        }
    }
}
