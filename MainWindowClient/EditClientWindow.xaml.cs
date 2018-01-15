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
using Bank.Entities;
using System.Text.RegularExpressions;


namespace Bank.MainWindow
{
    /// <summary>
    /// Interaction logic for EditClientWindow.xaml
    /// </summary>
    ///
    public partial class EditClientWindow : Window
    {
        List<TextBox> textBoxes;
        private Client editClient;
        public EditClientWindow()
        {
            InitializeComponent();
            textBoxes = new List<TextBox>();
            foreach (TextBox tb in EditClientWindowElements.Children.OfType<TextBox>())
            {
                textBoxes.Add(tb);
            }
        }
        public string FirstNameProperty
        {
            get { return FirstNameBox.Text; }
        }
        public string LastNameProperty
        {
            get { return LastNameBox.Text; }
        }
        public string EmailProperty
        {
            get { return EmailBox.Text; }
        }
        public long PeselProperty
        {
            get { return Int64.Parse(PeselBox.Text); }
        }
        public string CountryProperty
        {
            get { return CountryBox.Text; }
        }
        public string CityProperty
        {
            get { return CityBox.Text; }
        }
        public string PostalCodeProperty
        {
            get { return PostalCodeBox.Text; }
        }
        public string StreetProperty
        {
            get { return StreetBox.Text; }
        }
        public string BuildingNumberProperty
        {
            get { return BuildingNumberBox.Text; }
        }
        public string ApartamentNumberProperty
        {
            get { return ApartamentNumberBox.Text; }
        }

        public void LoadClientToEdit(Client client)
        {
            this.editClient = client;
            FirstNameBox.Text = editClient.FirstName;
            LastNameBox.Text = editClient.LastName;
            EmailBox.Text = editClient.Email;
            PeselBox.Text = editClient.Pesel;
            CountryBox.Text = editClient.Address.Country;
            CityBox.Text = editClient.Address.City;
            PostalCodeBox.Text = editClient.Address.PostalCode;
            StreetBox.Text = editClient.Address.Street;
            BuildingNumberBox.Text = editClient.Address.BuildingNr;
            ApartamentNumberBox.Text = editClient.Address.AppartmentNr;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
            Close();

        }
    }
}
