using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hextobin_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Hextobin : Window
    {
        List<CheckBox> checkBoxes = new List<CheckBox>();

        int numberOfByte = 5;
        public Hextobin()
        {
            InitializeComponent();
            Initialize();

        }
        private void Initialize()
        {
            
                PnlCheck.Children.Clear();
           
                int j = 16;
           
                for (int i = 0; i < 16; i++)

                {
                    CheckBox checkBox = new CheckBox
                    {
                        Name = "bit" + i,
                        IsChecked = false,
                        Content = "bit" + j
                    };
                    j--;
                    checkBox.Checked += CheckBox_Checked;
                    checkBox.Unchecked += CheckBox_Checked;
                    checkBoxes.Add(checkBox);
                    PnlCheck.Children.Add(checkBox);
                
            }
            txt_hex.Text = "000000";
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Handle(sender as CheckBox);
        }


        void Handle(CheckBox checkBox)
        {
            bool flag = checkBox.IsChecked.Value;
            string name = checkBox.Name;
            string strIndex = name.Replace("bit", "");
            int index = Int32.Parse(strIndex);

            char newChar = '0';

            if (flag == true)
                newChar = '1';
            else
                newChar = '0';

            if (Utils.OnlyHexInString(txt_hex.Text))
            {
                byte[] hexByte = Utils.StringToByteArray(txt_hex.Text);

                Dictionary<string, string> outputList = Utils.ByteArrayToBinary(hexByte);
                for (int i = 1; i <= outputList.Count; i++)
                {
                    string v = outputList["Byte" + i];
                    string temp = v;


                    StringBuilder sb = new StringBuilder(temp);
                    sb[index] = newChar;
                    temp = sb.ToString();

                    StringBuilder sb1 = new StringBuilder(Utils.BinaryStringToHexString(temp));

                    txt_hex.Text = sb1.ToString();
                }
            }
        }

        private void TextChanged()
        {
            if (Utils.OnlyHexInString(txt_hex.Text))
            {
                byte[] hexByte = Utils.StringToByteArray(txt_hex.Text);

                Dictionary<string, string> outputList = Utils.ByteArrayToBinary(hexByte);
                for (int i = 0; i < outputList.Count; i++)
                {
                    string s1 = outputList["Byte" + i];
                    string str = s1;
                    int index = 0;
                    foreach (var s in str)
                    {
                        var selectedchk = checkBoxes.Find(s => s.Name == "bit" + index);
                        if (selectedchk != null)
                            setValue(selectedchk, s);

                        index++;
                    }
                }  
            }
        }
        private void setValue(CheckBox checkBox, char val)
        {
            if (val == '1')
                checkBox.IsChecked = true;
            else
                checkBox.IsChecked = false;
        }



        private void txt_hex_Keyup(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TextChanged();
        }



        private void btn_Click(object sender, RoutedEventArgs e)
        {
            TextChanged();
        }
    }
}
