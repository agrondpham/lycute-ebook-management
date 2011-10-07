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
    /// Interaction logic for ComboBox.xaml
    /// </summary>
    public partial class ComboBox : UserControl
    {
        public ComboBox()
        {
            InitializeComponent();
        }
        private static string strTextBoxValue = "";
        public string getText() {
            return strTextBoxValue = textBox1.Text;

        }
        public void setText(string value) {
            strTextBoxValue = value;
            textBox1.Text = strTextBoxValue;
        }
        private void textBox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //textBox1.IsReadOnly = true;
        }

        private void btn_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(textBox1.Text);
                value += 1;
                textBox1.Text = value.ToString();
            }
            catch (Exception ex) { }
        }

        private void btn_Minus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(textBox1.Text);
                if (value > 0)
                {
                    value -= 1;
                }
                textBox1.Text = value.ToString();
            }
            catch (Exception ex) { }
        }
    }
}
