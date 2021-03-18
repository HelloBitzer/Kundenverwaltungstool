using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyNamespace;

namespace WPF
{
    /// <summary>
    /// Interaktionslogik für KundenAnlegenAendern.xaml
    /// </summary>
    public partial class KundenAnlegenAendern : Window
    {
        public ObservableCollection<FirmaDto> kundenListe { get; set; } = new ObservableCollection<FirmaDto>();
        public ObservableCollection<AnsprechpartnerDto> partnerliste { get; set; } = new ObservableCollection<AnsprechpartnerDto>();

        int index;
        private Client client = null;
        private static string url = @"http://localhost:44328";


        public KundenAnlegenAendern()
        {
        }


        public KundenAnlegenAendern(System.Collections.ObjectModel.ObservableCollection<FirmaDto> kundenListe, int index)
        {
            InitializeComponent();
            this.kundenListe = kundenListe;
            this.index = index;


            //Prüfen ob anlegen oder ändern
            if (index == -1)
            {
                //anlegen
                //Fenster für ändern anpassen
                this.Title = "Firma anlegen";
                BT_speichern.Content = "Hinzufügen";

            }
            else
            {
                //ändern
                //Fenster für ändern anpassen
                this.Title = "Firmendaten ändern";
                BT_speichern.Content = "Ändern";

                //Felder mit Firmendaten füllen
                TB_Name.Text = kundenListe[index].Name;
                TB_Strasse.Text = kundenListe[index].Strasse;
                TB_Hausnummer.Text = kundenListe[index].Hausnummer;
                TB_PLZ.Text = kundenListe[index].Plz;
                TB_Ort.Text = kundenListe[index].Ort;

                //Ansprechpartner
                //TB_Titel.Text = kundenListe[index].Titel;
                //TB_Nname.Text = kundenListe[index].Nachname;
                //TB_Vname = kundenListe[index].Vorname;
                //TB_Telefon = kundenListe[index].Telefon;
                //TB_mail = kundenListe[index].Email;
            }
        }

        public int SelectedIndex { get; }


        private void BT_abbrechen_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BT_speichern_OnClick(object sender, RoutedEventArgs e)
        {
            bool close = true;

            //Prüfen ob Ändern oder Anlegen
            if (index == -1)
            {
                //Anlegen
                var firma = new FirmaDto()
                {
                    Name = TB_Name.Text,
                    Strasse = TB_Strasse.Text,
                    Hausnummer = TB_Hausnummer.Text,
                    Plz = TB_PLZ.Text,
                    Ort = TB_Ort.Text,

                    //Ansprechpartner
                    //Titel = TB_Titel.Text,
                    //Nachname = TB_Nname.Text,
                    //Vorname = TB_Vname.Text,
                    //Telefon = TB_Telefon.Text,
                    //Email = TB_mail.Text


                };

                // client
                client = new Client(url);

                var erg = client.PostFirmaAsync(firma).Result;

                if (erg.FirmenId > 0)
                {
                    kundenListe.Add(erg);
                    MessageBox.Show("Speichern erfolgreich");

                    this.Close();
                }
            }
            //Ändern
            else
            {
                try
                {
                    // zusammenbauen des Objektes Firma

                    kundenListe[index].Name = TB_Name.Text;
                    kundenListe[index].Strasse = TB_Strasse.Text;
                    kundenListe[index].Hausnummer = TB_Hausnummer.Text;
                    kundenListe[index].Plz = TB_PLZ.Text;
                    kundenListe[index].Ort = TB_Ort.Text;

                    // zusammenbauen des Objektes Ansprechpartner

                    //kundenListe[index].Titel = TB_Titel.Text;
                    //kundenListe[index].Nachname = TB_Nname.Text;
                    //kundenListe[index].Vorname = TB_Vname.Text;
                    //kundenListe[index].Telefon = TB_Telefon.Text;
                    //kundenListe[index].Email = TB_mail.Text;


                    // client
                    client = new Client(url);

                    var p = kundenListe[index];

                    var firma = new FirmaDto()
                    {
                        FirmenId = p.FirmenId,
                        Name = p.Name,
                        Strasse = p.Strasse,
                        Hausnummer = p.Hausnummer,
                        Plz = p.Plz,
                        Ort = p.Ort
                    };

                    //Ändern
                    client.PutFirmaAsync(firma.FirmenId, firma);

                    if (close)
                    {
                        MessageBox.Show("Speichern erfolgreich");

                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Speichern fehlgeschlagen");
                    }

                    //wenn erfolgreich gespeichert, fenster schließen, ansonsten Fehlermeldung ausgeben und nicht schließen
                    if (close)
                    {
                        MessageBox.Show("Speichern erfolgreich");

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Speichern fehlgeschlagen");
                    }

                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Alle felder müssen ausgefüllt werden!");
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Bitte Grund auswählen!");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Falsches Datumsformat.");
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Falsches Datumsformat.");
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client = new Client(url);
        }
    }
}

