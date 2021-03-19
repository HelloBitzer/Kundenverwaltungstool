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
        private static string url = @"http://localhost:44328";

        //Listen erstellen
        public ObservableCollection<FirmaDto> kundenListe { get; set; } = new ObservableCollection<FirmaDto>();

        public ObservableCollection<AnsprechpartnerDto> partnerListe { get; set; } = new ObservableCollection<AnsprechpartnerDto>();

        public ObservableCollection<TerminDto> terminListe { get; set; } = new ObservableCollection<TerminDto>();
        
        private Client client = null;


        public MainWindow()
        {
            InitializeComponent();

            client = new Client(url);

            //Kundendaten laden 

            kundenListe = new ObservableCollection<FirmaDto>(LoadFirma());

            terminListe = new ObservableCollection<TerminDto>(LoadTermin());

            partnerListe = new ObservableCollection<AnsprechpartnerDto>(LoadAnsprechpartner());

            ////Datengrid an Listen binden
            DG_Kunden.ItemsSource = kundenListe;
            DG_Termine.ItemsSource = terminListe;
            DG_Ansprechpartner.ItemsSource = partnerListe;

        }

        //Load Methoden

        private ICollection<FirmaDto> LoadFirma()
        {
            try
            {
                return client.GetFirmaAllAsync().Result;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Die Kundenliste konnte nicht geladen werden {e.Message}");
                return null;
            }
        }

        private ICollection<AnsprechpartnerDto> LoadAnsprechpartner()
        {
            try
            {
                return client.GetAnsprechpartnerAllAsync().Result;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Die Ansprechpartner konnten nicht geladen werden {e.Message}");
                return null;
            }
        }


        private ICollection<TerminDto> LoadTermin()
        {
            try
            {
                return (ICollection<TerminDto>)client.GetTerminAsync().Result;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Die Terminliste konnte nicht geladen werden {e.Message}");
                return null;
            }
        }


        private void BT_KD_Anlegen_OnClick(object sender, RoutedEventArgs e)
        {
            client = new Client(url);

            //Fenster erstellen
            KundenAnlegenAendern aaf = new KundenAnlegenAendern(kundenListe, -1); 
            //Fenster öffnen                                 
            aaf.ShowDialog();
        }

        private void BT_KD_Aendern_OnClick(object sender, RoutedEventArgs e)
        {
            //meldung wenn kein Kunde gewählt
            if (DG_Kunden.SelectedIndex < 0)
            {
                MessageBox.Show("Kein Kunde gewählt, Maske zur neuanlage wird geöffnet.");
            }

            //Fenster erstellen
            KundenAnlegenAendern aaf = new KundenAnlegenAendern(kundenListe, DG_Kunden.SelectedIndex);
            //Fenster öffnen
            aaf.ShowDialog();

            //Datengrid Aktualisieren
            DG_Kunden.ItemsSource = null;
            DG_Kunden.ItemsSource = kundenListe;
        }

        // Wenn das fenster geschlossen wurde, die Liste neu lesen und anzeigen
        private void Window_Closed(object sender, EventArgs e)
        {
            client = new Client(url);

            kundenListe = new ObservableCollection<FirmaDto>(LoadFirma());
            terminListe = new ObservableCollection<TerminDto>(LoadTermin());
            partnerListe = new ObservableCollection<AnsprechpartnerDto>(LoadAnsprechpartner());

            //Datengrid an Listen binden
            DG_Kunden.ItemsSource = kundenListe;
            DG_Termine.ItemsSource = terminListe;
            DG_Ansprechpartner.ItemsSource = partnerListe;
        }

        private void BT_KD_Loeschen_OnClick(object sender, RoutedEventArgs e)
        {
            client = new Client(url);

            if (DG_Kunden.SelectedIndex < 0)
            {
                MessageBox.Show("Kein Kunde gewählt");
            }

            //Firma firma = DG_Kunden.SelectedItem as Firma;

           // client.DeleteFirmaAsync(firma.FirmenId);

            //Datengrid Aktualisieren
            DG_Kunden.ItemsSource = null;
            DG_Kunden.ItemsSource = kundenListe;

            kundenListe = new ObservableCollection<FirmaDto>(LoadFirma());

            DG_Kunden.ItemsSource = kundenListe;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client = new Client(url);

            terminListe = new ObservableCollection<TerminDto>(LoadTermin());

            //Datengrid an Listen binden
            DG_Termine.ItemsSource = terminListe;
        }


        private void BT_Neuer_Termin_OnClick(object sender, RoutedEventArgs e)
        {
            client = new Client(url);

            //Fenster erstellen
            Termin time = new Termin(terminListe, -1);
            //Fenster öffnen
            time.ShowDialog();
        }

        private void BT_Termin_ändern_OnClick(object sender, RoutedEventArgs e)
        {
            //meldung wenn kein Kunde gewählt
            if (DG_Termine.SelectedIndex < 0)
            {
                MessageBox.Show("Kein Termin gewählt, Maske zur Neuanlage wird geöffnet.");
            }

            //Fenster erstellen
            Termin time = new Termin(terminListe, DG_Termine.SelectedIndex);
            //Fenster öffnen
            time.ShowDialog();

            //Datengrid Aktualisieren
            DG_Termine.ItemsSource = null;
            DG_Termine.ItemsSource = terminListe;
        }

        private void BT_Termin_Loeschen_OnClick(object sender, RoutedEventArgs e)
        {
            client = new Client(url);

            if (DG_Termine.SelectedIndex < 0)
            {
                MessageBox.Show("Kein Termin gewählt");
            }

            Termin termins = DG_Termine.SelectedItem as Termin;

           // client.DeleteTerminAsync(termins.TerminId);

            //Datengrid Aktualisieren
            DG_Termine.ItemsSource = null;
            DG_Termine.ItemsSource = terminListe;

            terminListe = new ObservableCollection<TerminDto>(LoadTermin());

            DG_Termine.ItemsSource = terminListe;
        }

        private void BT_Ansprechpartner_Anlegen_OnClick(object sender, RoutedEventArgs e)
        {
            client = new Client(url);

            //Fenster erstellen
            Ansprechpartner anf = new Ansprechpartner(partnerListe, -1);
            //Fenster öffnen                                 
            anf.ShowDialog();
        }

        private void BT_Ansprechpartner_Aendern_OnClick(object sender, RoutedEventArgs e)
        {
            //meldung wenn kein Ansprechpartner gewählt
            if (DG_Ansprechpartner.SelectedIndex < 0)
            {
                MessageBox.Show("Kein Ansprechpartner gewählt, Maske zur Neuanlage wird geöffnet.");
            }

            //Fenster erstellen
            Ansprechpartner anf = new Ansprechpartner(partnerListe, DG_Ansprechpartner.SelectedIndex);
            //Fenster öffnen
            anf.ShowDialog();

            //Datengrid Aktualisieren
            DG_Kunden.ItemsSource = null;
            DG_Kunden.ItemsSource = kundenListe;
        }

        private void BT_Ansprechpartner_Loeschen_OnClick(object sender, RoutedEventArgs e)
        {
            client = new Client(url);

            if (DG_Ansprechpartner.SelectedIndex < 0)
            {
                MessageBox.Show("Kein Ansprechpartner gewählt");
            }

            //Datengrid Aktualisieren
            DG_Ansprechpartner.ItemsSource = null;
            DG_Ansprechpartner.ItemsSource = partnerListe;

            partnerListe = new ObservableCollection<AnsprechpartnerDto>(LoadAnsprechpartner());

            DG_Ansprechpartner.ItemsSource = partnerListe;
        }
    }
}
