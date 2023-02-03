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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Authentication
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        public static string text;
        public Captcha()
        {
            InitializeComponent();
            Random random = new Random();
            int countL = random.Next(6, 12); // Случайной количество линий
            for (int i = 0; i < countL; i++)
            {
                SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))); // Случайный цвет линий
                Line l = new Line()
                {
                    X1 = random.Next((int)CvField.Width),
                    Y1 = random.Next((int)CvField.Height),
                    X2 = random.Next((int)CvField.Width),
                    Y2 = random.Next((int)CvField.Height),
                    Stroke = brush,
                };
                CvField.Children.Add(l);
            }
            int countText = random.Next(7, 11); // Случайное количество символов в строке
            text = "";
            for (int i = 0; i < countText; i++)
            {
                int j = random.Next(2); // Случайный выбор символа на число или букву
                if (j == 0)
                    text = text + random.Next(9).ToString();
                else
                {
                    int l = random.Next(2); // Случайный выбор на заглавную или строчную
                    if (l == 0)
                        text = text + (char)random.Next('A', 'Z' + 1);
                    else
                        text = text + (char)random.Next('a', 'z' + 1);
                }
            }
            int Start = 0; // Начало отрезка
            int Finish = 0; // Конец отрезка
            int h = (int)CvField.Width / text.Length; // Шаг
            for (int i = 0; i < text.Length; i++) // Заполнение символами
            {
                if (i == 0)
                    Finish += h;
                else
                {
                    Start = Finish;
                    Finish += h;
                }
                int height = random.Next((int)CvField.Height);
                int width = random.Next(Start, Finish);
                if (height > 170)
                    height -= 30;
                if (width > 590)
                    Finish -= 10;
                int j = random.Next(3); // Выбор стиля символа
                if (j == 0)
                {
                    int fontSize = random.Next(16, 33);
                    TextBlock txt = new TextBlock()
                    {
                        Text = text[i].ToString(),
                        FontWeight = FontWeights.Bold,
                        Padding = new Thickness(width, height, 0, 0),
                        FontSize = fontSize,
                    };
                    CvField.Children.Add(txt);
                }
                else if (j == 1)
                {
                    int fontSize = random.Next(16, 33);
                    TextBlock txt = new TextBlock()
                    {
                        Text = text[i].ToString(),
                        FontStyle = FontStyles.Italic,
                        Padding = new Thickness(width, height, 0, 0),
                        FontSize = fontSize,
                    };
                    CvField.Children.Add(txt);
                }
                else if (j == 2)
                {
                    int fontSize = random.Next(16, 33);
                    TextBlock txt = new TextBlock()
                    {
                        Text = text[i].ToString(),
                        FontWeight = FontWeights.Bold,
                        FontStyle = FontStyles.Italic,
                        Padding = new Thickness(width, height, 0, 0),
                        FontSize = fontSize,
                    };
                    CvField.Children.Add(txt);
                }
            }
        }
        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            if (tbInputField.Text == text)
            {
                MainWindow.codeWrite = true;
                this.Close();
            }
            else
                this.Close();
        }
    }
}