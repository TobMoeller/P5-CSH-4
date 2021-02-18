using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace P5_CSH_4 {
    class Database {
        //public static List<Database> databases = new List<Database>();
        public static Database database;
        public string Server { get; set; }
        public string Db { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public bool Connected { get; set; } = false;
        public ConnectionResult ConRes { get; set; } = new ConnectionResult();
        public MySqlConnection Connection { get; private set; }
        public Database(string server, string db, string uid, string pwd) {
            Server = server;
            Db = db;
            Uid = uid;
            Pwd = pwd;
            Connection = new MySqlConnection($"server={server};uid={uid};pwd={pwd};database={db}");
            database = this;
        }

        /// <summary>
        /// Verbindungsaufbau
        /// </summary>
        /// <returns>ConnectionResult</returns>
        public ConnectionResult Connect() {
            try {
                Connection.Open();

                ConRes.success = true;
                ConRes.returnString = $"Verbindung aufgebaut!";
                Connected = true;
            } catch (Exception e) {
                ConRes.success = false;
                ConRes.returnString = "Verbindung fehlgeschlagen: \n" + e.Message;
            }

            return ConRes;
        }

        /// <summary>
        /// Verbindung schließen
        /// </summary>
        public void Disconnect() {
            Connection?.Close();
        }

        /// <summary>
        /// Methode zum Auslesen des dataReaders und Einfügen in ToDo Liste
        /// </summary>
        /// <param name="dataReader"></param>
        public void ReadData(MySqlDataReader dataReader) {
            while (dataReader.Read()) {
                new ToDo() {
                    Id = (int)dataReader["id"],
                    ShortDesc = (string)dataReader["short_description"],
                    LongDesc = (string)dataReader["long_description"],
                    Completed = (bool)dataReader["completed"]
                };
            }
        }

        /// <summary>
        /// SELECT gesamte Tabelle
        /// </summary>
        /// <returns>ConnectionResult</returns>
        public ConnectionResult SelectAll() {
            try {
                ToDo.toDos.Clear();

                MySqlCommand command = new MySqlCommand("SELECT * FROM todo", Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                ReadData(dataReader);
                dataReader.Close();

                ConRes.success = true;
                ConRes.returnString = "Tabelle todo";
            } catch (Exception e) {
                ConRes.success = false;
                ConRes.returnString = "Fehler: " + e.Message;
            }

            return ConRes;
        }

        /// <summary>
        /// SELECT mit WHERE Eingrenzung
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="suchFeld">Tabellenspalte</param>
        /// <param name="suchTerm">Gesuchter Wert</param>
        /// <returns></returns>
        public ConnectionResult Select<T>(string suchFeld, T suchTerm) {
            try {
                ToDo.toDos.Clear();

                MySqlCommand command;
                if (suchTerm is string) {
                    command = new MySqlCommand($"SELECT * FROM todo WHERE {suchFeld} LIKE \"%{suchTerm}%\"", Connection);
                } else {
                    command = new MySqlCommand($"SELECT * FROM todo WHERE {suchFeld} = @suchTerm", Connection);
                    command.Parameters.AddWithValue("@suchTerm", suchTerm);
                }

                MySqlDataReader dataReader = command?.ExecuteReader();
                ReadData(dataReader);
                dataReader.Close();

                ConRes.success = true;
                ConRes.returnString = $"Alle Einträge mit {suchFeld} = {suchTerm}";
            } catch (Exception e) {
                ConRes.success = false;
                ConRes.returnString = "Fehler: " + e.Message;
            }

            return ConRes;
        }

        /// <summary>
        /// INSERT INTO
        /// </summary>
        /// <param name="toDo">Objekt mit den einzufügenden Werten</param>
        /// <returns></returns>
        public ConnectionResult Insert(ToDo toDo) {
            try {
                // SQL string mit festgesetzter ID oder ohne (auto increment) ID
                string sqlCmd = toDo.Id != 0 ?
                    $"INSERT INTO todo (id, short_description, long_description, completed) VALUES (@id, @sd, @ld, @cd)"
                    : $"INSERT INTO todo (short_description, long_description, completed) VALUES (@sd, @ld, @cd)";
                MySqlCommand command = new MySqlCommand(sqlCmd, Connection);

                if (toDo.Id != 0) command.Parameters.AddWithValue("@id", toDo.Id);
                command.Parameters.AddWithValue("@sd", toDo.ShortDesc);
                command.Parameters.AddWithValue("@ld", toDo.LongDesc);
                command.Parameters.AddWithValue("@cd", toDo.Completed);
                command.ExecuteNonQuery();

                ConRes.success = true;
                ConRes.returnString = "Erfolgreich hinzugefügt";
            } catch (Exception e) {
                ConRes.success = false;
                ConRes.returnString = "Fehler: " + e.Message;
            }

            return ConRes;
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="toDo">Objekt mit den zu ändernden Werten</param>
        /// <returns></returns>
        public ConnectionResult Update(ToDo toDo) {
            try {
                if (toDo.Id == 0) throw new Exception("Keine ID angegeben.");

                string sqlCmd = "UPDATE todo SET short_description = @sd WHERE id = @id;"
                                + "UPDATE todo SET long_description = @ld WHERE id = @id;"
                                + "UPDATE todo SET completed = @cd WHERE id = @id;";
                MySqlCommand command = new MySqlCommand(sqlCmd, Connection);

                command.Parameters.AddWithValue("@id", toDo.Id);
                command.Parameters.AddWithValue("@sd", toDo.ShortDesc);
                command.Parameters.AddWithValue("@ld", toDo.LongDesc);
                command.Parameters.AddWithValue("@cd", toDo.Completed);
                command.ExecuteNonQuery();

                ConRes.success = true;
                ConRes.returnString = "Eintrag erfolgreich geändert";
            } catch (Exception e) {
                ConRes.success = false;
                ConRes.returnString = "Fehler: " + e.Message;
            }
            return ConRes;
        }


        /// <summary>
        /// DELETE FROM
        /// </summary>
        /// <param name="toDo">Zu Löschendes Objekt</param>
        /// <returns></returns>
        public ConnectionResult Delete(ToDo toDo) {
            try {
                if (toDo.Id == 0) throw new Exception("Keine ID angegeben.");

                MySqlCommand command = new MySqlCommand("DELETE FROM todo WHERE id = @id", Connection);

                command.Parameters.AddWithValue("@id", toDo.Id);
                command.ExecuteNonQuery();

                ConRes.success = true;
                ConRes.returnString = "Eintrag erfolgreich gelöscht";
            } catch (Exception e) {
                ConRes.success = false;
                ConRes.returnString = "Fehler: " + e.Message;
            }

            return ConRes;
        }
    }

    /// <summary>
    /// Rückgabe Objekt mit Aussage zu Befehlserfolg und Fehler/Erfolgsmeldung
    /// </summary>
    class ConnectionResult {
        public bool success;
        public string returnString;
    }
}
