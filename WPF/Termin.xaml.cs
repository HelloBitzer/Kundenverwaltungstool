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
    /// Interaktionslogik für Termin.xaml
    /// </summary>
    public partial class Termin : Window
    {
        public ObservableCollection<TerminDto> terminListe { get; set; } = new ObservableCollection<TerminDto>();

        public int index;
        private Client client = null;
        private static string url = @"http://localhost:44328";

        public Termin()
        {
        }

        public Termin(System.Collections.ObjectModel.ObservableCollection<TerminDto> terminListe, int index)
        {
            InitializeComponent();

            this.terminListe = terminListe;
            this.index = index;

            //Enum-Liste
            CB_grund.ItemsSource = Enum.GetValues(typeof(Termingrund));

            //Prüfen ob anlegen...

            if (index == -1)
            {
                //anlegen
                this.Title = "Termin anlegen";
                BT_erstellen.Content = "Hinzufügen";

                DP_beginn.SelectedDate = DateTime.Now;
                TB_Beginn_std.Text = DateTime.Now.Hour.ToString();
                TB_Beginn_min.Text = 0.ToString();

                DP_ende.SelectedDate = DateTime.Now;
                TB_Ende_std.Text = DateTime.Now.Hour.ToString();
                TB_ende_min.Text = "30";

                //CB_grund.SelectedItem = terminListe[index].Termingrund;
                TB_bemerkung.Text = string.Empty;
            }


            //...oder ändern, wenn termin ausgewählt, daten von Textbox laden
            else
            {
                //ändern
                this.Title = "Termin ändern";
                BT_erstellen.Content = "Ändern";

                //ändern, Felder mit Daten aus Liste füllen

                DP_beginn.SelectedDate = terminListe[index].Start.DateTime;
                TB_Beginn_std.Text = terminListe[index].Start.Hour.ToString();
                TB_Beginn_min.Text = terminListe[index].Start.Minute.ToString();

                DP_ende.SelectedDate = terminListe[index].Ende?.DateTime;
                TB_Ende_std.Text = terminListe[index].Ende?.Hour.ToString() ?? string.Empty;
                TB_ende_min.Text = terminListe[index].Ende?.Minute.ToString() ?? string.Empty;

                //CB_grund.SelectedItem = terminListe[index].Termingrund;
                TB_bemerkung.Text = terminListe[index].Bemerkung;
            }
        }

        public int SelectedIndex { get; }


        private void BT_erstellen_OnClick(object sender, RoutedEventArgs e)
        {
            bool close = true;

            //Terminfelder Start generieren
            int selectedHour = int.Parse(TB_Beginn_std.Text);
            int selectedMin = int.Parse(TB_Beginn_min.Text);
            var selectedStartDate = DP_beginn.SelectedDate.Value;
            var start = new DateTime(selectedStartDate.Year, selectedStartDate.Month, selectedStartDate.Day, selectedHour, selectedMin, 0);

            //Termin Felder Ende in "end" generieren
            int selectedhourend = int.Parse(TB_Ende_std.Text);
            int selectedminend = int.Parse(TB_ende_min.Text);
            var selectedend = DP_ende.SelectedDate.Value;
            var end = new DateTime(selectedend.Year, selectedend.Month, selectedend.Day, selectedhourend, selectedminend, 0);


            if (index == -1)
            {
                var termin = new TerminDto()
                {
                    Start = new DateTimeOffset(start),
                    Ende = new DateTimeOffset(end),
                    Bemerkung = TB_bemerkung.Text,
                    //Termingrund = termin.Termingrund
                };

                client = new Client(url);
                var erg = client.PostTerminAsync(termin).Result;

                if (erg.TerminId > 0)
                {
                    terminListe.Add(erg);
                    MessageBox.Show("Speichern erfolgreich");

                    this.Close();
                }
            }

            else
            {
                try
                {   //Termin Felder Start in "start" generieren

                    // zusammenbauen des objektes person

                    terminListe[index].Start = new DateTimeOffset(start);
                    terminListe[index].Ende = new DateTimeOffset(end);
                    terminListe[index].Bemerkung = TB_bemerkung.Text;

                    // client
                    client = new Client(url);

                    var t = terminListe[index];

                    var termin = new TerminDto()
                    {
                        TerminId = t.TerminId,
                        Start = t.Start,
                        Ende = t.Ende,
                        Bemerkung = t.Bemerkung,
                        //Termingrund = termin.Termingrund
                    };

                    // 
                    client.PutTerminAsync(termin.TerminId, termin);


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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client = new Client(url);
        }
    }
}
