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
namespace Authentication
{
    public partial class MainWindow : Window
    {
        public static int countCode; // Количество введеных чисел
        public static bool codeWrite; // Проверка на правильность введенного числа
        int countTime; // Счётчик времени для повторного ввода
        DispatcherTimer disTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            disTimer.Interval = new TimeSpan(0, 0, 1);
            disTimer.Tick += new EventHandler(Timer);
        }
        private void BtnAutorizate_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнения полей
            if (tbLogin.Text.Length == 0)
                MessageBox.Show("Заполните поле логина");
            else if (pbPass.Password.Length == 0)
                MessageBox.Show("Заполните поле пароля");
            else // Попытка входа
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
        private void Timer(object sender, EventArgs e) // Таймер
        {
            if (countTime == 0) // Если не осталось времени, то разрешаем ввод
            {
                BtnAutorizate.Visibility = Visibility.Visible; // Включаем отображение кнопки
                disTimer.Stop();
                tbLogin.IsEnabled = true;
                pbPass.IsEnabled = true;
                tbNewCode.Visibility = Visibility.Collapsed; // Скрываем таймер
            }
            else
                tbNewCode.Text = "Получить новый код можно будет через " + countTime + " секунд";
            countTime--;
        }
        private void Authorization() // После успешной авторизации
        {
            codeWrite = false;
            Random r = new Random();
            int code = r.Next(0, 100000);
            MessageBox.Show("Код для входа: " + code.ToString("D5") + "\nЗапомните его"); // Вывод сообщения с кодом
            Code CodeT = new Code(code.ToString("D5"));
            CodeT.ShowDialog(); // Запускаем окно с вводом проверочного кода
            if (codeWrite == true) // Если код введён верно
            {
                codeWrite = false;
                Captcha captcha = new Captcha();
                captcha.ShowDialog(); // Запускаем окно с капчей
                if (codeWrite == true) // Если капча введена верно
                    MessageBox.Show("Успешная авторизация!");
                else // Если капча введена неверно
                {
                    MessageBox.Show("Текст введён не верно! Попробуйте ещё раз!");
                    Captcha captchaReplay = new Captcha();
                    captchaReplay.ShowDialog();
                    // Вторая попатка на ввод
                    if (codeWrite == true) // Если успех
                        MessageBox.Show("Успешная авторизация!");
                    else // Если неверный ввод
                    {
                        MessageBox.Show("Вы не подтвердили, что вы не робот. Вход не удачен");
                        tbLogin.Text = "";
                        pbPass.Password = "";
                    }
                }
            }
            else // Если код введён неверно или не полностью
            {
                if (countCode == 5) // Вывод сообщения о том что введённый код неверный
                    MessageBox.Show("Неверный код");
                // Запрещаем ввод в поля и скрываем кнопку
                BtnAutorizate.Visibility = Visibility.Collapsed;
                tbLogin.IsEnabled = false;
                pbPass.IsEnabled = false;
                // Включаем таймер на 60 секунд
                countTime = 60;
                tbNewCode.Text = "Получить новый код можно будет через " + countTime + " секунд";
                tbNewCode.Visibility = Visibility.Visible;
                disTimer.Start();
            }
        }
    }
}