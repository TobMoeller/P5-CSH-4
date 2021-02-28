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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P5_CSH_4 {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        /*
            Praktische Übung GUI

            - DB-Tabelle für Aufgaben:
            ID(eindeutig und fortlaufend) (int, autoincrement, Primary Key, Unique)
            Kurzbezeichnung(varchar 30)
            Aufgabenbeschreibung(max. 100 Zeichen) (varchar 100)
            Erledigt(bool)

            *********************************************************************

            Oberfläche:
            Menüfunktionen für Neu, Ändern, Löschen, Anzeige einzelner, Anzeige aller Aufgaben und getrennt nach erledigt oder nicht erledigt
            Statusanzeige in Statusbar/Label(Anzahl aller Datensätze, Rest nach Gutdünken)

            1. Entwurf der Struktur und der DB-Tabelle
	            - XAML
	            - Klassen
	            - DB-Tabelle

            - Welche Elemente brauche ich?
            - Wie ist die Reihenfolge dieser?
            - Was sollen die Elemente handeln?

            1a.Aufbau der SQL-Befehle 

            2. Codieren der Klassen und der GUI-Funktionalität
        */

        public MainWindow() {
            InitializeComponent();
        }


        /// <summary>
        /// Datenbank verbinden
        /// </summary>
        private void BtDbStarten_Click(object sender, RoutedEventArgs e) {
            DbPropertys dbPropertysWindow = new DbPropertys();
            dbPropertysWindow.Show();
            dbPropertysWindow.Closing += TbAktualisieren;
        }
        /*
         *  Methode abonniert Window Closing Event der LoginPopUps
         *  Gibt die Verbindungs-Statusmeldung in der Status Textbox aus
         */
        private void TbAktualisieren(object sender, System.ComponentModel.CancelEventArgs e) {
            TbStatus.Text = Database.database?.Connected == true ? 
                $"Verbunden mit: \n{Database.database.Server} - {Database.database.Db}"
                : "Keine Verbindung";
        }


        /// <summary>
        /// Suche entsprechend Eingabemaske
        /// (SELECT mit WHERE Einschränkung)
        /// </summary>
        private void BtSingle_Click(object sender, RoutedEventArgs e) {
            string suchTerm = "", suchFeld = "";

            foreach (object element in SpInput.Children) { // Iteriert über alle Element in SpInput
                // Alle TextBoxen
                if (element is TextBox) {

                    if ((element as TextBox).Text != "" && suchTerm == "") { // Die erste nicht leere Textbox bestimmt den Suchterm
                        
                        suchTerm = (element as TextBox).Text;
                        suchFeld = SuchFeld((element as TextBox).Name);

                        (element as TextBox).BorderThickness = new Thickness(3); // Die Suchterm Textbox wird markiert
                    } else {
                        (element as TextBox).BorderThickness = new Thickness(1); // Allen anderen wird Markierung entzogen
                    }

                    // Alle ComboBoxen
                } else if (element is ComboBox) {

                    if ((element as ComboBox).SelectedItem != CbEmpty && suchTerm == "") {

                        suchTerm = (element as ComboBox).SelectedItem == CbErledigt ? "true" : "false";
                        suchFeld = "completed";

                        (element as ComboBox).BorderThickness = new Thickness(3);
                    } else {
                        (element as ComboBox).BorderThickness = new Thickness(1);
                    }

                }

            }

            // (Spielerei zum Testen der generischen Select Methode (wandelt "true" in bool um))
            // Anschließend: Aufruf Datenbank Klasse
            if (suchFeld == "completed" && (suchTerm.ToLower() == "true" || suchTerm.ToLower() == "false")) {
                TbStatus.Text = Database.database?.Select(suchFeld, (suchTerm.ToLower() == "true" ? true : false)).returnString;
            } else {
                TbStatus.Text = Database.database?.Select(suchFeld, suchTerm).returnString;
            }

            LvData.ItemsSource = null;
            LvData.ItemsSource = ToDo.toDos;
        }

        /*
         * Methode die den Controlnamen durch den passenden SQL Spaltennamen ersetzt
         */
        private string SuchFeld(string name) {
            switch (name) {
                case "TbID":
                    return "id";
                case "TbShortDesc":
                    return "short_description";
                case "TbLongDesc":
                    return "long_description";
                case "TbCompleted":
                    return "completed";
                default:
                    return "";
            }
        }


        /// <summary>
        /// Eingabemaske leeren
        /// </summary>
        private void BtClearText_Click(object sender, RoutedEventArgs e) {
            foreach (object element in SpInput.Children) {

                if (element is TextBox) {
                    (element as TextBox).Text = "";
                    (element as TextBox).BorderThickness = new Thickness(1);

                } else if (element is ComboBox) {
                    (element as ComboBox).SelectedItem = CbEmpty;
                }

            }
        }

        /// <summary>
        /// Alle anzeigen
        /// </summary>
        private void BtAll_Click(object sender, RoutedEventArgs e) {
            TbStatus.Text = Database.database?.SelectAll().returnString;

            // Update Listview, ggf. nochmal recherchieren
            // https://stackoverflow.com/questions/20996288/wpf-listview-changing-itemssource-does-not-change-listview
            // https://www.wpf-tutorial.com/listview-control/listview-with-gridview/
            LvData.ItemsSource = null;
            LvData.ItemsSource = ToDo.toDos;
        }


        /// <summary>
        /// Neuen Eintrag hinzufügen
        /// </summary>
        private void BtNew_Click(object sender, RoutedEventArgs e) {
            ToDo.toDos.Clear();

            // Generiere ToDo aus Eingabe Maske OHNE erforderlicher ID
            ToDo toDo = GeneriereToDo(false);

            // Aufruf Datenbank Klasse
            TbStatus.Text = Database.database.Insert(toDo).returnString;

            // Update Listview
            LvData.ItemsSource = null;
            LvData.ItemsSource = ToDo.toDos;
        }


        /// <summary>
        /// Ausgewähltes ELement (ID ist erforderlich) in Datenbank ändern
        /// </summary>
        private void BtChange_Click(object sender, RoutedEventArgs e) {
            ToDo.toDos.Clear();

            // Generiere ToDo aus Eingabe Maske MIT erforderlicher ID
            ToDo toDo = GeneriereToDo(true);            

            // Aufruf Datenbank Klasse
            TbStatus.Text = Database.database.Update(toDo).returnString;

            // Update Listview
            LvData.ItemsSource = null;
            LvData.ItemsSource = ToDo.toDos;
        }


        /// <summary>
        /// Löscht das ausgewählte Element (ID erforderlich)
        /// </summary>
        private void BtDelete_Click(object sender, RoutedEventArgs e) {
            ToDo.toDos.Clear();

            // Generiere ToDo aus Eingabe Maske MIT erforderlicher ID
            ToDo toDo = GeneriereToDo(true);

            // Aufruf Datenbank Klasse
            TbStatus.Text = Database.database.Delete(toDo).returnString;

            // Update Listview
            LvData.ItemsSource = null;
            LvData.ItemsSource = ToDo.toDos;
        }


        /// <summary>
        /// Generiert ein neues ToDo Objekt aus der Eingabemaske und gibt dieses zurück.
        /// </summary>
        /// <param name="IdRequired">Gibt an, ob eine ID zwingend erforderlich ist</param>
        /// <returns></returns>
        public ToDo GeneriereToDo(bool IdRequired) {
            // Neues ToDo generieren
            ToDo toDo = new ToDo() { ShortDesc = TbShortDesc.Text, LongDesc = TbLongDesc.Text };

            // Prüfen ob ID gesetzt werden soll
            if (TbID.Text != "") {
                int tempInt;
                Int32.TryParse(TbID.Text, out tempInt);
                toDo.Id = tempInt;
            } else if (IdRequired) {
                MessageBox.Show("Bitte die zu ändernde ID eingeben!");
            }

            // Completed Combobox in bool umwandeln
            if (CbCompleted.SelectedItem == CbErledigt) toDo.Completed = true;
            else toDo.Completed = false;

            return toDo;
        }


        /// <summary>
        /// Sortierung nach nicht erledigt und erledigt
        /// (unfertig)
        /// </summary>
        private void BtSortDone_Click(object sender, RoutedEventArgs e) {
            //ToDo.toDos.Sort((x, y) => (x.Completed == true ? 1 : -1));
            ToDo.toDos.Sort((x, y) => {
                int result = 0;
                result += (x.Completed == true ? 500 : 0);
                result += (x.Id > y.Id ? 1 : -1);
                //result += x.Id;
                //result -= y.Id;
                return result;
            });

            // Update Listview
            LvData.ItemsSource = null;
            LvData.ItemsSource = ToDo.toDos;
        }


        /// <summary>
        /// Bei Fensterschließen wird Datenbankverbindung geschlossen
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Database.database?.Disconnect();
        }


        /// <summary>
        /// Fügt das ausgewählte Element der Liste in die Auswahlmaske ein
        /// </summary>
        private void LvData_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (LvData.SelectedItem is ToDo) {
                ToDo temp = (LvData.SelectedItem as ToDo);

                TbID.Text = temp.Id.ToString();
                TbShortDesc.Text = temp.ShortDesc;
                TbLongDesc.Text = temp.LongDesc;

                if (temp.Completed == true) CbCompleted.SelectedItem = CbErledigt;
                else CbCompleted.SelectedItem = CbOffen;

            }
        }

    }
}
