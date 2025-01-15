using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivLabProj5 {
    class Genres {
        int id;
        string name;
        public int ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        public Genres() { }
        public Genres(int ID, string name) {
            this.id = ID;
            this.name = name;
        }
        public static void shapka() {
            Console.WriteLine("Жанры:");
            Console.WriteLine(
            $"{ "ID жанра",-15}" +
            $"{ "Жанры",-25}"
            );

            Console.WriteLine(new string('-', 50)); // Separator line
            Console.WriteLine();
        }
        public new string ToString() {
            return
      $"{ID,-15}" +
      $"{name,-25}"
      ;}
    }
}
