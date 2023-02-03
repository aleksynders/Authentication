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
using System.Windows.Threading;

namespace Authentication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///
    


//
//██╗░░░░░░█████╗░░██████╗░██╗███╗░░██╗██╗  ░█████╗░██████╗░███╗░░░███╗
//██║░░░░░██╔══██╗██╔════╝░██║████╗░██║╚═╝  ██╔══██╗██╔══██╗████╗░████║
//██║░░░░░██║░░██║██║░░██╗░██║██╔██╗██║░░░  ███████║██║░░██║██╔████╔██║
//██║░░░░░██║░░██║██║░░╚██╗██║██║╚████║░░░  ██╔══██║██║░░██║██║╚██╔╝██║
//███████╗╚█████╔╝╚██████╔╝██║██║░╚███║██╗  ██║░░██║██████╔╝██║░╚═╝░██║
//╚══════╝░╚════╝░░╚═════╝░╚═╝╚═╝░░╚══╝╚═╝  ╚═╝░░╚═╝╚═════╝░╚═╝░░░░░╚═╝

//██████╗░░█████╗░░██████╗░██████╗░██╗░░░░░░░██╗░█████╗░██████╗░██████╗░██╗  ░█████╗░██████╗░███╗░░░███╗
//██╔══██╗██╔══██╗██╔════╝██╔════╝░██║░░██╗░░██║██╔══██╗██╔══██╗██╔══██╗╚═╝  ██╔══██╗██╔══██╗████╗░████║
//██████╔╝███████║╚█████╗░╚█████╗░░╚██╗████╗██╔╝██║░░██║██████╔╝██║░░██║░░░  ███████║██║░░██║██╔████╔██║
//██╔═══╝░██╔══██║░╚═══██╗░╚═══██╗░░████╔═████║░██║░░██║██╔══██╗██║░░██║░░░  ██╔══██║██║░░██║██║╚██╔╝██║
//██║░░░░░██║░░██║██████╔╝██████╔╝░░╚██╔╝░╚██╔╝░╚█████╔╝██║░░██║██████╔╝██╗  ██║░░██║██████╔╝██║░╚═╝░██║
//╚═╝░░░░░╚═╝░░╚═╝╚═════╝░╚═════╝░░░░╚═╝░░░╚═╝░░░╚════╝░╚═╝░░╚═╝╚═════╝░╚═╝  ╚═╝░░╚═╝╚═════╝░╚═╝░░░░░╚═╝
//


    public partial class MainWindow : Window
    {

        public static int countCode;
        public static bool codeWrite;
        int countTime;
        DispatcherTimer disTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            disTimer.Interval = new TimeSpan(0, 0, 1);
            disTimer.Tick += new EventHandler(Timer);
        }

        private void BtnAutorizate_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text.Length == 0)
                MessageBox.Show("Заполните поле логина");
            else if (pbPass.Password.Length == 0)
                MessageBox.Show("Заполните поле пароля");
            else
            {
                if (tbLogin.Text == "ADM")
                {
                    if (pbPass.Password == "ADM")
                        Authorization();
                    else
                        MessageBox.Show("Пароль не верный");
                }
                else
                    MessageBox.Show("Пользователь с таким логиным не найден");
            }
        }

        private void Timer(object sender, EventArgs e)
        {
            if (countTime == 0)
            {
                BtnAutorizate.Visibility = Visibility.Visible;
                disTimer.Stop();
                tbLogin.IsEnabled = true;
                pbPass.IsEnabled = true;
                tbNewCode.Visibility = Visibility.Collapsed;
            }
            else
                tbNewCode.Text = "Получить новый код можно будет через " + countTime + " секунд";
            countTime--;
        }

        private void Authorization()
        {
            codeWrite = false;
            Random r = new Random();
            int code = r.Next(0, 100000);
            MessageBox.Show("Код для входа: " + code.ToString("D5") + "\nЗапомните его");
            Code CodeT = new Code(code.ToString("D5"));
            CodeT.ShowDialog();
            if (codeWrite == true)
            {

            }
            else 
            {
                if (countCode == 5)
                    MessageBox.Show("Неверный код");
                BtnAutorizate.Visibility = Visibility.Collapsed;
                tbLogin.IsEnabled = false;
                pbPass.IsEnabled = false;
                countTime = 60;
                tbNewCode.Text = "Получить новый код можно будет через " + countTime + " секунд";
                tbNewCode.Visibility = Visibility.Visible;
                disTimer.Start();
            }
        }
    }
}
