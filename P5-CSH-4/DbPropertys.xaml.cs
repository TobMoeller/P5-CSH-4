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

namespace P5_CSH_4 {
    /// <summary>
    /// Interaktionslogik für DbPropertys.xaml
    /// </summary>
    public partial class DbPropertys : Window {
        public DbPropertys() {
            InitializeComponent();
        }

        /// <summary>
        /// Abbruch Knopf, Schließt Fenster
        /// </summary>
        private void BtCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Eingabe Bestätigen und Datenbankverbindung aufbauen
        /// Schließt Fenster bei Erfolg, Gibt Fehlermeldung bei Fehler
        /// </summary>
        private void BtConfirm_Click(object sender, RoutedEventArgs e) {
            new Database(
                TbServer.Text == "" ? "localhost" : TbServer.Text,
                TbDatabase.Text == "" ? "csh5_todo" : TbDatabase.Text,
                TbUid.Text, TbPwd.Text);

            ConnectionResult temp = Database.database.Connect();

            if (temp.success) this.Close(); // Bei Erfolg Fenster Schließen
            else MessageBox.Show(temp.returnString); // Ansonsten Ausgabe des Fehlers
        }

        /// <summary>
        /// Bestätigt Eingabe durch "Enter"
        /// </summary>
        private void BtEnter(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                BtConfirm_Click(sender, e);
            }
        }
    }
}
