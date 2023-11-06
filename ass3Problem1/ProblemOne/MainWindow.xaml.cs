using ProblemOne.Models;
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

namespace ProblemOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CalculateFees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double balance = double.Parse(balanceTextBox.Text);
                int numChecks = int.Parse(numChecksTextBox.Text);
                bankCharges bc = new bankCharges(balance, numChecks);
                double fees = bc.calculateFees();
                resultLabel.Content = $"Total fee for this month: ${fees:F2}";
            }
            catch
            {
                resultLabel.Content = "Incorrect entered number";
            }
        }
    }
}
