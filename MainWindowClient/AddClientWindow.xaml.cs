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
using System.Text.RegularExpressions;
using System.Windows.Shapes;


namespace Bank.MainWindow
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        List<TextBox> textBoxes;
        public AddClientWindow()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            foreach (TextBox tb in AddClientWindowElements.Children.OfType<TextBox>())
            {
                textBoxes.Add(tb);
            }

        }
        
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool isDataCorrect = true;
            string wrongDataMessage = "Niepoprawne dane.";
            foreach (TextBox tb in textBoxes)
            {
                if (tb.Text.Length <= 0)
                {
                    isDataCorrect = false;
                    wrongDataMessage += " Pole " + (tb.Name.EndsWith("Box") ? (tb.Name.Substring(0, tb.Name.Length - 3)) : tb.Name) + " jest puste.";
                }
            }

            if (PostalCodeBox.Text.Length != 6)
            {
                isDataCorrect = false;
                wrongDataMessage += " Kod pocztowy nie składa się z 5 cyfr.";
            }

            if (PeselBox.Text.Length != 11)
            {
                isDataCorrect = false;
                wrongDataMessage += " Numer pesel nie składa się z 11 cyfr.";
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void MessageInputError(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
