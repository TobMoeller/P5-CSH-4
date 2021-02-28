# P5-CSH-4
Third programming class during retraining

## What is it about?

- making school notes
- solving school tasks
- sharing and collaborating with class mates
- showcasing some of my Code in C#

### Features

- Windows Presentation Foundation (WPF) App
- Database Connection

### Technologies

- C# WPF-App (.NET Framework)
- MySQL
- Visual Studio
- phpMyAdmin

---

![app UI](https://github.com/TobMoeller/P5-CSH-4/blob/main/UI.jpg?raw=true)

## Small Feature showcase

This was a small project we did during class to play around with what we learned about WPF and database connections. So i went ahead and tried to improve on a few things:

### database

At first i tryed to separate the tasks more clearly and made a `Database` class that was supposed to handle most of the communication part to the database itself.

Then i wanted to make it more dynamic: Every instance of the class should be its own connection to a database, so it could be used to handle multiple database connections at once (For this i also implemented a separate login screen).

```csharp
class Database {
    //public static List<Database> databases = new List<Database>();        // the idea was to store multiple connections here
    public static Database database;                                        // for now i only used one
    public string Server { get; set; }
    public string Db { get; set; }
    public string Uid { get; set; }
    public string Pwd { get; set; }                                         // security would have to be reworked and was ignored for this task
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
}
```

### display

Before i decided on how the class should interact with the rest of the app i had to look on how to display what the queries result in.

For this i went with a listview that gets its data from a databinding to a list of the desired types:

```xml
<ListView x:Name="LvData" Margin="200, 0, 0, 0" ItemsSource="{Binding ToDo.toDos}" SelectionChanged="LvData_SelectionChanged">
    <ListView.View>
        <GridView>
            <GridViewColumn Header="ID" Width="40" DisplayMemberBinding="{Binding Id}" />
            <GridViewColumn Header="Kurzbezeichnung" Width="160" DisplayMemberBinding="{Binding ShortDesc}" />
            <GridViewColumn Header="Aufgabenbeschreibung" Width="320" DisplayMemberBinding="{Binding LongDesc}" />
            <GridViewColumn Header="Erledigt" Width="70" >
                <GridViewColumn.CellTemplate>
                    <DataTemplate>
                        <CheckBox IsChecked="{Binding Completed}" IsEnabled="False" />
                    </DataTemplate>
                </GridViewColumn.CellTemplate>
            </GridViewColumn>
        </GridView>
    </ListView.View>
</ListView>
```

### data

The aforementioned list is part of a class i made that represents the type of data we want to show:

```csharp
public class ToDo {
    public static List<ToDo> toDos = new List<ToDo>();      // static list that holds the data for the Listview
    public int Id { get; set; }
    public string ShortDesc { get; set; }
    public string LongDesc { get; set; }
    public bool Completed { get; set; }
    public ToDo() {
        toDos.Add(this);
    }
}
```

## how it works together

With this basic construct all the database class has to do is manipulate the `toDos` list to change the display of data.

So if the user for example wants to delete an item with a certain ID it works like this:

```csharp
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
```

The button click clears the list at first, then generates a new ToDo object with the following method from the user input mask (this is separated because i use it in a few other methods aswell):

```csharp
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
```
This ToDo finally gets passed to the database's `Delete()` method:

```csharp
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
```

It communicates the command to the database and returns an object containing a boolean if the command was successful and a string that gets displayed inside the status window.

---

## The generic idea

Another idea i had was to make the database's methods as dynamic as possible to handle different types of uses like SELECT queries with different datatypes to search for.
This is not really finalized, but it already works in certain scenarios (here it is used to handle strings and booleans):

```csharp
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
```