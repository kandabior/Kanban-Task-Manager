
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
using milstone3;
using milstone3.PresentationLayer.DataContexts;
using milstone3.PresentationLayer.XamlWindows;

namespace milstone3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserInput userInput;
        string boardId;
        public MainWindow()
        {
            UserController.initiateProg();
            InitializeComponent();
            

            this.userInput= new UserInput();

            this.DataContext = this.userInput;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            {
                Window1 registerWindow = new Window1();
                registerWindow.Show();
                this.Close();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginF();
        }

        private void LoginF()
        {
            if (UserController.login(userInput.UserName, userInput.PWD))
            {
                chooseBoard win2 = new chooseBoard(userInput.UserName);
                win2.Show();
                this.Close();
            }
            else
            {
                this.userInput.UserName = "";
                this.userInput.PWD = "";
                MessageBox.Show("User Name Or Password Incorrect");
            }
        }

        private void EnterClicked1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Login_Click(sender, e);
                e.Handled = true;
            }
        }

        private void EnterClicked2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Login_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
