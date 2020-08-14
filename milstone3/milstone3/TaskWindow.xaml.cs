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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        TaskInput taskInput;
        public TaskWindow()
        {
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (taskInput.edit())
            {
                MessageBox.Show("Task Edited Succefully");
                Window2 window2 = new Window2();
                window2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("can't edit task");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (taskInput.delete())
            {
                MessageBox.Show("Task was deleted succesfuly");
                Window2 window2 = new Window2();
                window2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("can't delete task");
            }
        }

        private void MoveState_Click(object sender, RoutedEventArgs e)
        {
            if (taskInput.changeState())
            {
                MessageBox.Show("Task was moved forward");
                Window2 window2 = new Window2();
                window2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("can't change state for the task");
            }
        }
    }
}
