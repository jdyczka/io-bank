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
        private List<TextBox> textBoxes;

        public AddAccountToClientWindow()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            foreach (TextBox tb in AccountData.Children.OfType<TextBox>())
            {
                textBoxes.Add(tb);
            }
        }

        private void AddAccountConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool isDataCorrect = true;
            string wrongDataMessage = "Niepoprawne dane!";
            foreach (TextBox tb in textBoxes)
            {
                if (tb.Text.Length <= 0)
                {
                    isDataCorrect = false;
                    wrongDataMessage += " Pole " + (tb.Name.EndsWith("Box") ? (tb.Name.Substring(0, tb.Name.Length - 3)) : tb.Name) + " jest puste.";
                }
            }
            if (NazwaKontaText.Text.Length > 20)
            {
                isDataCorrect = false;
                wrongDataMessage += " Niepoprawna nazwa";
            }
            if (WalutaText.SelectedIndex == -1)
            {
                isDataCorrect = false;
                wrongDataMessage += " Nie wybrano waluty";
            }
            if (TypText.SelectedIndex == -1)
            {
                isDataCorrect = false;
                wrongDataMessage += " Nie wybrano rodzaju konta.";
            }
            if (OprocentowanieText.Text.Length > 2)
            {
                isDataCorrect = false;
                wrongDataMessage += " Błędne oprocentowanie.";
            }
            if (isDataCorrect == true)
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
                Account account = null;
                account = new Account(NazwaKontaText.Text, currency, accountType, rate);
                Close();
            }
            else
            {
                MessageBox.Show(wrongDataMessage, "Błąd danych");
            }
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
