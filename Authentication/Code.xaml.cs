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
using System.Windows.Threading;

namespace Authentication
{
    /// <summary>
    /// Логика взаимодействия для Code.xaml
    /// </summary>
    public partial class Code : Window
    {

        string code;
        int time = 10;
        DispatcherTimer disTimer = new DispatcherTimer();

        public Code(string code)
        {
            InitializeComponent();
            this.code = code;
            disTimer.Interval = new TimeSpan(0, 0, 1);
            disTimer.Tick += new EventHandler(Timer);
            disTimer.Start();
            MainWindow.countCode = 0;
        }

        private void Timer(object sender, EventArgs e)
        {
            if (time == 0)
                this.Close();
            else
                tbRemainingTime.Text = "Оставшееся время: " + time + " секунд";
            time--;
        }

        private void tbCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbCode.Text == code)
            {
                MainWindow.codeWrite = true;
                this.Close();
            }
            MainWindow.countCode = tbCode.Text.Length;
        }
    }
}
