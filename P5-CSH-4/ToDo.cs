using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5_CSH_4 {

    // Nachfragen, weshalb die class "public" sein soll um in MainWindow als Rückgabetyp genutzt zu werden, siehe "GeneriereToDo"
    public class ToDo {
        public static List<ToDo> toDos = new List<ToDo>();
        //public static ObservableCollection<ToDo> toDos = new ObservableCollection<ToDo>();
        public int Id { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public bool Completed { get; set; }
        public ToDo() {
            toDos.Add(this);
        }
    }
}
