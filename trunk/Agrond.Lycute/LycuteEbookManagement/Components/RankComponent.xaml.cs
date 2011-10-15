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

namespace LycuteEbookManagement.Components
{
    /// <summary>
    /// Interaction logic for RankComponent.xaml
    /// </summary>
    public partial class RankComponent : UserControl
    {
        private static int _intRank;
        public int getText()
        {
            return _intRank = Convert.ToInt32(textBox1.Text);

        }
        public void setText(int value)
        {
            _intRank = value;
            textBox1.Text = _intRank.ToString();
            changeRank(_intRank);
        }
        public RankComponent()
        {
            InitializeComponent();
        }
        private void btn_Minus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _intRank = Convert.ToInt32(textBox1.Text);
            if (_intRank >1)
            {
                _intRank -= 1;
                changeRank(_intRank);
                textBox1.Text = _intRank.ToString();
            }
        }
        private void btn_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _intRank = Convert.ToInt32(textBox1.Text);
            if (_intRank < 5)
            {
                _intRank += 1;
                changeRank(_intRank);
                textBox1.Text = _intRank.ToString();
            }
        }
        private void changeRank(int intRank) {
            switch (intRank)
            {
                case 1:
                    setpic("/LycuteEbookManagement;component/Images/Components/1star.png");
                    break;
                case 2:
                    setpic("/LycuteEbookManagement;component/Images/Components/2stars.png");
                    break;
                case 3:
                    setpic("/LycuteEbookManagement;component/Images/Components/3stars.png");
                    break;
                case 4:
                    setpic("/LycuteEbookManagement;component/Images/Components/4stars.png");
                    break;
                case 5:
                    setpic("/LycuteEbookManagement;component/Images/Components/5stars.png");
                    break;

            }
        }
        private void setpic(string url) {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            image1.Source = bi;
        }
    }
}
