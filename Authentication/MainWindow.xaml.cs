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

namespace Authentication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAutorizate_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text.Length == 0)
                MessageBox.Show("Заполните поле логина");
            else if (pbPass.Password.Length == 0)
                MessageBox.Show("Заполните поле пароля");
            else
            {
                if (tbLogin.Text == "adm")
                {
                    if (pbPass.Password == "adm")
                        ShowAuthorization();
                    else
                        MessageBox.Show("Пользователь с таким логиным и паролем не найден");
                }
                else
                    MessageBox.Show("Пользователь с таким логиным не найден");
            }
        }

        private void ShowAuthorization()
        {

        }
    }
}
