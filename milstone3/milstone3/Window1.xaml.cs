using milstone3.PresentationLayer.DataContexts;
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

namespace milstone3
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        UserInput userInput;
        public Window1()
        {
            InitializeComponent();
            this.userInput = new UserInput();

            this.DataContext = this.userInput;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (this.userInput.Register())
            {
                MessageBox.Show("User Name Register Succefully");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid User Name Or Password");
            }
        }
    }
}
