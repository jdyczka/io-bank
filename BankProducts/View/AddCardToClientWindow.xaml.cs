using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BankProducts.View
{
    public partial class AddCardToClientWindow : Window
    {
        private List<TextBox> textBoxes;

        public AddCardToClientWindow()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            foreach (TextBox tb in CardData.Children.OfType<TextBox>())
            {
                textBoxes.Add(tb);
            }
        }

        public void AddCardConfirm_Click(object sender, RoutedEventArgs e)
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
            if (PINText.Text.Length != 4)
            {
                isDataCorrect = false;
                wrongDataMessage += " Pin nie sklada sie z 4 cyfr.";
            }
            if (isDataCorrect == true)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show(wrongDataMessage, "Błąd danych");
            }
        }

        public void CancelAddingCard_Click(object sender, RoutedEventArgs e)
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
