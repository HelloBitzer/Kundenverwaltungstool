using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MyNamespace;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Kundenliste erstellen
        //private static string url = @"http://localhost:44328";
        //public ObservableCollection<People> kundenListe { get; set; } = new ObservableCollection<People>();

        //Terminliste erstellen
        //public ObservableCollection<Termins> terminListe { get; set; } = new ObservableCollection<Termins>();

        //private Client client = null;



        public MainWindow()
        {
            InitializeComponent();

            //client = new Client(url);

            ////Kundendaten laden 

            //kundenListe = new ObservableCollection<People>(LoadPeoples());

            //terminListe = new ObservableCollection<Termins>(LoadTermins());

            ////Datengrid an Listen binden
            //DG_Kunden.ItemsSource = kundenListe;
            //DG_Termine.ItemsSource = terminListe;
        }


        //private ICollection<People> LoadPeoples()
        //{
        //    try
        //    {
        //        return client.GetPeopleAllAsync().Result;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show($"Die Kundenliste konnte nicht geladen werden {e.Message}");
        //        return null;
        //    }
        //}

        //private ICollection<Termins> LoadTermins()
        //{
        //    try
        //    {
        //        return client.GetTerminsAllAsync().Result;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show($"Die Terminliste konnte nicht geladen werden {e.Message}");
        //        return null;
        //    }
        //}


        private void BT_KD_Anlegen_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BT_KD_Aendern_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BT_KD_Loeschen_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BT_Neuer_Termin_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BT_Termin_ändern_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BT_Termin_Loeschen_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
