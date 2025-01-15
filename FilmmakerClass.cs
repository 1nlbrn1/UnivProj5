using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivLabProj5 {
    class Filmmakers {
        int id;
        string name;
        string country;
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
        public string Country{
            get {
                return country;
            }
            set {
                country = value;
            }
        }
        public Filmmakers() { }
        public Filmmakers(int ID, string name, string country) {
            this.id = ID;
            this.name = name;
            this.country = country;
        }
        public static void shapka() {
            Console.WriteLine("Режиссёры:");
            Console.WriteLine(
            $"{ "ID Режиссёра",-15}" +
            $"{ "Имя режиссёра",-25}" +
            $"{ "Страна происхождения",-10}"
            );

            Console.WriteLine(new string('-', 100)); // Separator line
            Console.WriteLine();
        }
        public new string ToString() {
            return
      $"{id,-15}" +
      $"{name,-25}" +
      $"{country,-10}";
        }
    }
}
