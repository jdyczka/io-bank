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

namespace Bank.MainWindow
{
    public partial class DetailsClientWindow : Window
    {
        private Client clientDetails;

        public DetailsClientWindow()
        {
            InitializeComponent();

        }

        public Client ClientDetailsProperty
        {
            get { return clientDetails; }
            set { clientDetails = value; }
        }

        private void CloseDetails_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void LoadDetailsClient()
        {
            FirstNameDetails.Content = clientDetails.FirstName;
            LastNameDetails.Content = clientDetails.LastName;
            EmailDetails.Content = clientDetails.Email;
            PeselDetails.Content = clientDetails.Pesel;
            CountryDetails.Content = clientDetails.Address.Country;
            CityDetails.Content = clientDetails.Address.City;
            PostalCodeDetails.Content = clientDetails.Address.PostalCode;
            StreetDetails.Content = clientDetails.Address.Street;
            BuildingNumberDetails.Content = clientDetails.Address.BuildingNr;
            ApartamentNumberDetails.Content = clientDetails.Address.AppartmentNr;
        }

    }
}
