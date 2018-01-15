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
using Bank.DataAccess.Repositories;
using Bank.Entities;
using Bank.MainWindow;

namespace BankProducts.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainProductWindow : Window
    {
        ClientRepository repository = null;

        private Client clientDetails;

        public Client ClientDetailsProperty { get => clientDetails; set => clientDetails = value; }

        public MainProductWindow()
        {
            InitializeComponent();
            //LoadDetailsClient();
        }

        public void SetRepository(ClientRepository repository)
        {
            this.repository = repository;
        }

        public void LoadDetailsClient()
        {
            FirstName.Content = clientDetails.FirstName;
            LastName.Content = clientDetails.LastName;
            Email.Content = clientDetails.Email;
            Pesel.Content = clientDetails.Pesel;
        }

        private void CancelProduct_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = false; po polaczeniu z modulem zarzadzania
            Close();
        }

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccountToClientWindow addAccountToClientWindow = new AddAccountToClientWindow();       
            if(addAccountToClientWindow.ShowDialog() == true)
            {

            }
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            AddCardToClientWindow addCardToClientWindow = new AddCardToClientWindow();
            if(addCardToClientWindow.ShowDialog() == true)
            {

            }
        }

        private void AddLoan_Click(object sender, RoutedEventArgs e)
        {
            AddLoanToClientWindow addLoanToClientWindow = new AddLoanToClientWindow();
            if (addLoanToClientWindow.ShowDialog() == true)
            {

            }
        }
    }
}
