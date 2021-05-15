using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaktionslogik für Ansprechpartner.xaml
    /// </summary>
    public partial class Ansprechpartner : Window
    {
        public ObservableCollection<AnsprechpartnerDto> partnerListe { get; set; } = new ObservableCollection<AnsprechpartnerDto>();
        public ObservableCollection<FirmaDto> firmenliste { get; set; } = new ObservableCollection<FirmaDto>();

        int index;
        private Client client = null;
        private static string url = @"http://localhost:44328";

        public Ansprechpartner()
        {
        }

        public Ansprechpartner(System.Collections.ObjectModel.ObservableCollection<AnsprechpartnerDto> partnerListe, System.Collections.ObjectModel.ObservableCollection<FirmaDto> firmenliste, int index)
        {
            InitializeComponent();
            this.partnerListe = partnerListe;
            this.firmenliste = firmenliste;
            this.index = index;


            if (this.firmenliste != null)
            {
                DT_Firmen.ItemsSource = this.firmenliste;


                if (this.index != -1)
                {

                    var firma = this.firmenliste.FirstOrDefault(o => o.FirmenId == partnerListe[this.index].FirmenId);
                    if (firma != null)
                    {
                        DT_Firmen.SelectedItem = firma;
                    }
                }
            }

            //Prüfen ob anlegen oder ändern
            if (index == -1)
            {
                //anlegen
                //Fenster für ändern anpassen
                this.Title = "Ansprechpartner anlegen";
                BT_speichern.Content = "Hinzufügen";

            }
            else
            {
                //ändern
                //Fenster für ändern anpassen
                this.Title = "Ansprechpartner ändern";
                BT_speichern.Content = "Ändern";

                //Ansprechpartner
                TB_Titel.Text = partnerListe[index].Titel;
                TB_Nname.Text = partnerListe[index].Nachname;
                TB_Vname.Text = partnerListe[index].Vorname;
                TB_Telefon.Text = partnerListe[index].Telefon;
                TB_mail.Text = partnerListe[index].Email;
            }
        }

        public int SelectedIndex { get; }


        private void BT_speichern_OnClick(object sender, RoutedEventArgs e)
        {
            bool close = true;
            int firmenID = -1;
            var firma = DT_Firmen.SelectedItem as FirmaDto;
            if (firma != null)
            {
                firmenID = firma.FirmenId;
            }

            //Prüfen ob Ändern oder Anlegen
            if (index == -1)
            {

                //Anlegen
                var ansprechpartner = new AnsprechpartnerDto()
                {
                    //Ansprechpartner
                    Titel = TB_Titel.Text,
                    Nachname = TB_Nname.Text,
                    Vorname = TB_Vname.Text,
                    Telefon = TB_Telefon.Text,
                    Email = TB_mail.Text,
                    FirmenId = firmenID
                };

                // client
                client = new Client(url);

                var erg = client.PostAnsprechpartnerAsync(ansprechpartner).Result;

                if (erg.AnsprechpartnerId > 0)
                {
                    partnerListe.Add(erg);
                    MessageBox.Show("Speichern erfolgreich");

                    this.Close();
                }
            }
            //Ändern
            else
            {
                try
                {
                    // zusammenbauen des Objektes Ansprechpartner

                    partnerListe[index].Titel = TB_Titel.Text;
                    partnerListe[index].Nachname = TB_Nname.Text;
                    partnerListe[index].Vorname = TB_Vname.Text;
                    partnerListe[index].Telefon = TB_Telefon.Text;
                    partnerListe[index].Email = TB_mail.Text;
                    partnerListe[index].FirmenId = firmenID;


                    // client
                    client = new Client(url);

                    var p = partnerListe[index];

                    var ansprechpartner = new AnsprechpartnerDto()
                    {
                        AnsprechpartnerId = p.AnsprechpartnerId,
                        Titel = p.Titel,
                        Nachname = p.Nachname,
                        Vorname = p.Vorname,
                        Telefon = p.Telefon,
                        Email = p.Email,
                        FirmenId = firmenID
                    };

                    //Ändern
                    client.PutAnsprechpartnerAsync(ansprechpartner.AnsprechpartnerId, ansprechpartner);

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

        private void BT_abbrechen_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            //if (this.firmenliste != null)
            //{
            //    DT_Firmen.ItemsSource = this.firmenliste;


            //    if (this.SelectedIndex != -1)
            //    {

            //        var firma = this.firmenliste.FirstOrDefault(o => o.FirmenId == partnerListe[index].FirmenId);
            //        if (firma != null)
            //        {
            //            DT_Firmen.SelectedItem = firma;
            //        }



            //    }


            //}
        }
    }
}
