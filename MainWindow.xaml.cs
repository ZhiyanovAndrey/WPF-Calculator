using System;
using System.Collections.Generic;
using System.Data;
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

namespace WPF_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement el in numButtons.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += NumButtons; //перенаправление события Click если нажата одна из кнопок
                                                      //в контейнере компоновки с именем numButtons
                }
            }
            foreach (UIElement el in functionButton.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += FunctionButtons; //перенаправление события Click если нажата одна из кнопок
                                                           //в контейнере компоновки с именем FunctionButtons
                }
            }

        }

        private void NumButtons(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = (string)((Button)e.OriginalSource).Content; //запись содержимого Content из перенаправленного события в переменную str

                //замена декоративных символов на стандартные символы для метода Compute()
                if (str.Contains("×"))
                {
                    textBlock.AppendText(str.Replace("×", "*"));
                }
                else if (str.Contains("÷"))
                {
                    textBlock.AppendText(str.Replace("÷", "/"));
                }

                //реализация стандартных функции калькулятора
                else if (str == "C")
                    textBlock.Clear();

                else if (str == "=")
                {
                    string s = textBlock.Text.Replace(",", "."); //замена запятой на точку для метода Compute()
                    textBlock.Clear();
                    textBlock.Text = s;
                    string value = new DataTable().Compute(textBlock.Text, null).ToString();
                    textBlock.Text = value;
                }
                else if (str == "🠴")
                {
                    textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - 1, 1);

                }

                else textBlock.Text += str; //запись содержимого Content в строку если ни одно из услови не выполнено
            }

            //вывести сообщение об исключении
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void FunctionButtons(object sender, RoutedEventArgs e)      //реализация инженерных функции калькулятора
        {
            try
            {
                string str = (string)((Button)e.OriginalSource).Content;

                if (str == "1/x")
                {
                    double x = Convert.ToDouble(textBlock.Text);
                    textBlock.Text = (1 / x).ToString("0.###########");
                }
                else if (str == "√")
                {   
                    double x = Convert.ToDouble(textBlock.Text);
                    textBlock.Text = Math.Sqrt(x).ToString("#,0.##########");
                }
                else if (str == "x²")
                {
                    double x = Convert.ToDouble(textBlock.Text);
                    textBlock.Text = Math.Pow(x, 2).ToString("#,0.##########");
                }
                else if (str == "!")
                {
                    int n = Convert.ToInt32(textBlock.Text);
                    double s = 1;
                    for (int i = 2; i <= n; i++)
                    {
                        s *= i;
                    }
                    textBlock.Text = s.ToString("#,0");
                }

                else textBlock.Text += str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
