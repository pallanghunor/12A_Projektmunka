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

namespace _12A_Projektmunka
{
    /// <summary>
    /// Interaction logic for ErrorMessageBox.xaml
    /// </summary>
    public partial class ErrorMessageBox : Window
    {
        public ErrorMessageBox(string errorMessage)
        {
            InitializeComponent();
            MessageTxtBlock.Text = errorMessage;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
