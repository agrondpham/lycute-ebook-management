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
using System.Xml;
using System.Windows.Media.Animation;

namespace LycuteEbookManagement.Setting
{
    /// <summary>
    /// Interaction logic for SettingPanel.xaml
    /// </summary>
    public partial class SettingPanel : UserControl
    {
        public SettingPanel()
        {
            InitializeComponent();
        }
        //Change slide
        public void chanceSlide(string pData)
        {
            string data = pData;
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);
            XmlElement root = (XmlElement)xdoc.ChildNodes[0];
            XmlNodeList xnl = root.SelectNodes("Page");
            viewer.ItemsSource = xnl;
            viewer.BeginStoryboard((Storyboard)Application.Current.Resources["slideRightToLeft"]);
        }

        private void btn_SetLocation_Click(object sender, RoutedEventArgs e)
        {
            string strSearchResult = @"<root>
                <Page Source='ConfigLocation.xaml'/>
            </root>";
            chanceSlide(strSearchResult);
        }

        private void btn_SetStructure_Click(object sender, RoutedEventArgs e)
        {
            string strSearchResult = @"<root>
                <Page Source='ConfigStructure.xaml'/>
            </root>";
            chanceSlide(strSearchResult);
        }
    }
}
