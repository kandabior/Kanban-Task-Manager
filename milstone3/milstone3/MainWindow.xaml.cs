
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


namespace milstone3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserInput userInput;
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
            if (this.userInput.login())
            {
                Window2 win2 = new Window2();
                win2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User Name Or Password Incorrect");
            }


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        
    }
}
