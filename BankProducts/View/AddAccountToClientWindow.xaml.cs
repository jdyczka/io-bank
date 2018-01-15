using Bank.Entities;
using Bank.Entities.Enums;
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

namespace BankProducts.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddAccountToClientWindow.xaml
    /// </summary>
    public partial class AddAccountToClientWindow : Window
    {
        private List<String> listaWalut = new List<String>();
        private List<String> typyKont = new List<String>();
        
        public AddAccountToClientWindow()
        {
            InitializeComponent();
            listaWalut.Add("Polski złoty");
            listaWalut.Add("Euro");
            listaWalut.Add("Dolar amerykański");
            WalutaText.ItemsSource = listaWalut;

            typyKont.Add("Regularne");
            typyKont.Add("Złote");
            typyKont.Add("Platynowe");
            TypText.ItemsSource = typyKont;
        }

        private void AddAccountConfirm_Click(object sender, RoutedEventArgs e)
        {
            float rate = float.Parse(OprocentowanieText.Text);
            Currency currency;
            AccountType accountType;
            if ((string)WalutaText.SelectedItem == "Polski złoty")
            {
                currency = Currency.PLN;
            }
            else if ((string)WalutaText.SelectedItem == "Euro")
            {
                currency = Currency.EUR;
            }
            else
                currency = Currency.USD;

            if ((string)TypText.SelectedItem == "Regularne")
            {
                accountType = AccountType.Regular;
            }
            else if ((string)TypText.SelectedItem == "Złote")
            {
                accountType = AccountType.Gold;
            }
            else
                accountType = AccountType.Platinum;

            Account account = new Account(NazwaKontaText.Text, currency, accountType, rate);
            DialogResult = true;
            Close();
        }

        private void CancelAddingAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz anulować?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DialogResult = false;
                Close();
            }         
        }
    }
}
