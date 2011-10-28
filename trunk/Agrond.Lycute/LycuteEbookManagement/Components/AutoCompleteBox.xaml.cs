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
using System.ComponentModel;

namespace LycuteEbookManagement.Components
{
    /// <summary>
    /// Interaction logic for AutoCompleteBox.xaml
    /// </summary>
    public partial class AutoCompleteBox : UserControl
    {
        public AutoCompleteBox()
        {
            InitializeComponent();

        }

        #region Variable
        public static List<string> _ListData { get; set; }
        #endregion

        #region set get
        //this function not used now
        public string Text { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public void SetData(string pStrData) {
            if (pStrData != "")
            {
                string[] str = pStrData.Split(';');
                List<string> list = str.ToList<string>();
                _ListData = list;
                ICollectionView filtedView = CollectionViewSource.GetDefaultView(_ListData);
                listBox1.DataContext = filtedView;
                listBox1.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region event
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

            string[] strArrayAuthor = textBox1.Text.Split(';');
            FilterList(strArrayAuthor[strArrayAuthor.Count() - 1]);

        }
        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            listBox1.Visibility = Visibility.Hidden;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string listboxItem = (string)listBox1.SelectedItem;
            string data = listboxItem;
            textBox1.Text = textBox1.Text + data + ";";
            listBox1.Visibility = Visibility.Hidden;
        }
        #endregion

        #region function
        public void FilterList(string pKeyword) {
            listBox1.Items.Filter = p =>
            {
                string path = p as string;
                return path.ToLower().Contains(pKeyword.ToLower());
            };
            if (listBox1.Items.Count == 0)
                listBox1.Visibility = Visibility.Hidden;
            else
                listBox1.Visibility = Visibility.Visible;
        }
        #endregion


    }
}
