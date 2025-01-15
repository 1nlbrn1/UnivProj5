using System;
namespace UnivLabProj5 {
    class Movies {
        int id;
        string name;
        int id_filmmaker;
        int release_date;
        int id_genre;
        int duration;
        int budget;
        int box_office;
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
        public int ID_filmmaker {
            get {
                return id_filmmaker;
            }
            set {
                id_filmmaker = value;
            }
        }
        public int Release_date {
            get {
                return release_date;
            }
            set {
                release_date = value;
            }
        }
        public int ID_genre {
            get {
                return id_genre;
            }
            set {
                id_genre = value;
            }
        }
        public int Duration {
            get {
                return duration;
            }
            set {
                duration = value;
            }
        }
        public int Budget {
            get {
                return budget;
            }
            set {
                budget = value;
            }
        }
        public int Box_office {
            get {
                return box_office;
            }
            set {
                box_office = value;
            }
        }
        //public Movies() { }
        public Movies(int ID, string name, int ID_filmmaker, int release_date, int ID_genre, int duration, int budget, int box_office) {
            this.id = ID;
            this.name = name;
            this.id_filmmaker = ID_filmmaker;
            this.release_date = release_date;
            this.id_genre = ID_genre;
            this.duration = duration;
            this.budget = budget;
            this.box_office = box_office;
        }
        public static void shapka() {
            Console.WriteLine("Фильмы:");
            Console.WriteLine(
            $"{ "ID",-5}" +
            //$"{ "Название фильма",-25}" +
            $"{ "Название фильма",-90}" +
            $"{ "ID режиссёра",-15}" +
            $"{ "Год выхода",-12}" +
            $"{ "ID жанра",-10}" +
            $"{ "Продолжительность",-20}" +
            $"{ "Бюджет",-15}" +
            $"{ "Сборы",-10}");

            Console.WriteLine(new string('-', 200)); // Separator line
            Console.WriteLine();
        }
        public new string ToString() {
            return 
       $"{id,-5}" +
        //$"{(name.Length > 25 ? name.Substring(0, 22) + "..." : name),-25}" +
       $"{name,-90}" +
       $"{id_filmmaker,-15}" +
       $"{release_date,-12}" +
       $"{id_genre,-10}" +
       $"{duration,-20}" +
       $"{budget,-15}" +
       $"{box_office,-10}";
        }
    }
}
