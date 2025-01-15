using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
namespace UnivLabProj5 {
    public enum Tables {
        movies,
        filmmakers,
        genres
    }
    public enum Ext_ids {
        filmmakers_id,
        genres_id
    }
    class Exdb {
        string path;
        string logpath;
        string db;
        List<Movies> movies;
        List<Filmmakers> filmmakers;
        List<Genres> genres;
        public Exdb(string path) {
            this.path = path;
            logpath = "";
            db = "";
            movies = new List<Movies>();
            filmmakers = new List<Filmmakers>();
            genres = new List<Genres>();
            log_init();

        }
        public void log_init() {
            Console.WriteLine("Store Logs into the new file? y/n");
            string inp = Console.ReadLine();
            if(inp != "y" && inp != "n") {
                Console.WriteLine("Input error.");
                log_init();
                return;
            }

            string prepath = @"..\..\..\";
            if(inp == "y") {
                string dt = "log" + DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss").Replace(":", "-") + ".txt";
                prepath = Path.Combine(prepath, dt);
                try {
                    // Ensure the file is created and properly closed
                    using(FileStream fs = File.Create(prepath)) {
                        // File created and closed immediately
                    }
                    Console.WriteLine($"Created new log file: {prepath}");
                    logpath = prepath;
                } catch(Exception e) {
                    Console.WriteLine($"Error creating file: {e.Message}");
                    log_init();
                }
                return;
            }

            if(inp == "n") {
                try {
                    string[] files = Directory.GetFiles(prepath, "log*.txt", SearchOption.TopDirectoryOnly);
                    if(files.Length > 0) {
                        string latestFile = files
                            .OrderByDescending(file => {
                                // Extract the date-time part from the file name
                                string fileName = Path.GetFileNameWithoutExtension(file);
                                string dateTimePart = fileName.Substring(3); // Start after "log"
                                return DateTime.ParseExact(dateTimePart, "dd.MM.yyyy HH-mm-ss", CultureInfo.InvariantCulture);
                            })
                            .FirstOrDefault();

                        Console.WriteLine($"Logging into: {latestFile}");
                        logpath = latestFile;
                    } else {
                        Console.WriteLine("Log file not found!");
                        log_init();
                        return;
                    }
                } catch(Exception e) {
                    Console.WriteLine($"Error accessing log files: {e.Message}");
                    log_init();
                }
                return;
            }
        }
        public void log(string inp) {
            try {
                using(var writer = File.AppendText(logpath)) {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + inp);
                }
            } catch(Exception e) {
                Console.WriteLine($"Error writing to log file: {e.Message}");
            }
        }
        bool IsID(string value, bool is_silent) {
            if(int.TryParse(value, out _)) {
                if(int.Parse(value) >= 0) {
                    return true;
                }
            }
            if(!is_silent) {
                Console.WriteLine("Not an ID");
            }
            return false;
        }
        bool IsAccNum(string value, int minval = 0, int maxval= int.MaxValue) {
            if(int.TryParse(value, out _)) {
                if((int.Parse(value) >= minval) && (int.Parse(value) <= maxval)) {
                    return true;
                }
                Console.WriteLine($"Value: \"{value}\" is too big or too small");

            }
            Console.WriteLine($"Value: \"{value}\" is not parseable to int.");
            return false;
        }
        public bool IsExistingID(string value, Tables t, bool expectations, bool is_silent = false) {
            if(IsID(value, is_silent)) {
                if(t == Tables.movies) {
                    foreach(Movies movie in movies) {
                        int id = int.Parse(value);
                        if(id == movie.ID) {
                            if(!expectations && !is_silent) {
                                Console.WriteLine($"ID {id} already exists");
                            }
                            return true;
                        }
                    }
                }
                if(t == Tables.filmmakers) {
                    foreach(Filmmakers filmmaker in filmmakers) {
                        int id = int.Parse(value);
                        if(id == filmmaker.ID) {
                            if(!expectations && !is_silent) {
                                Console.WriteLine($"ID {id} already exists");
                            }
                            return true;
                        }
                    }
                }
                if(t == Tables.genres) {
                    foreach(Genres genre in genres) {
                        int id = int.Parse(value);
                        if(id == genre.ID) {
                            if(!expectations && !is_silent) {
                                Console.WriteLine($"ID {id} already exists");
                            }
                            return true;
                        }
                    }
                }
            }
            if(expectations && !is_silent) {
                Console.WriteLine($"ID {value} does not exist");
            }
            return false;
        }
        /*
        public void read_db_dumb() {
            Console.WriteLine("Attempting to open workbook...");
            using(var workbook = new XLWorkbook(path)) {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();
                db = "";
                foreach(var row in rows) {
                    for(int col = 1; col <= 8; col++) {
                        var cellValue = row.Cell(col).GetValue<string>();
                        db += ($"{cellValue}\t");
                    }
                    db += "\n";
                }
            }
            log("Read a workbook");
        }
        public void out_db_dumb() {
            Console.WriteLine(db);
            log("Out workbook");
        }
        */
        public void read_db(Tables t) {
            using(var workbook = new XLWorkbook(path)) {
                if(t == Tables.movies) {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RangeUsed().RowsUsed();
                    db = "";
                    bool isfirst = true;
                    foreach(var row in rows) {
                        if(isfirst) { isfirst = false; continue; }
                        var id = row.Cell(1).GetValue<int>();
                        //Console.WriteLine("HELLO>"+id +"HELLO<");
                        var name = row.Cell(2).GetValue<string>();
                        var id_filmmaker = row.Cell(3).GetValue<int>();
                        var release_date = row.Cell(4).GetValue<int>();
                        var id_genre = row.Cell(5).GetValue<int>();
                        var duration = row.Cell(6).GetValue<int>();
                        var budget = row.Cell(7).GetValue<int>();
                        var box_office = row.Cell(8).GetValue<int>();
                        Movies m = new Movies(id, name, id_filmmaker, release_date, id_genre, duration, budget, box_office);
                        movies.Add(m);
                        //row.Cell(1).GetValue<string>();

                        //for(int col = 1; col <= 8; col++) {

                        //var cellValue = row.Cell(col).GetValue<string>();

                        //db += ($"{cellValue}\t");
                        // }
                        //db += "\n";
                    }
                    log("Read a workbook: movies");
                }
                if(t == Tables.filmmakers) {
                    var worksheet = workbook.Worksheet(2);
                    var rows = worksheet.RangeUsed().RowsUsed();
                    db = "";
                    bool isfirst = true;
                    foreach(var row in rows) {
                        if(isfirst) { isfirst = false; continue; }
                        var id = row.Cell(1).GetValue<int>();
                        var name = row.Cell(2).GetValue<string>();
                        var country = row.Cell(3).GetValue<string>();
                        Filmmakers f = new Filmmakers(id, name, country);
                        filmmakers.Add(f);
                    }
                    log("Read a workbook: filmmakers");
                }
                if(t == Tables.genres) {
                    var worksheet = workbook.Worksheet(3);
                    var rows = worksheet.RangeUsed().RowsUsed();
                    db = "";
                    bool isfirst = true;
                    foreach(var row in rows) {
                        if(isfirst) { isfirst = false; continue; }
                        var id = row.Cell(1).GetValue<int>();
                        var name = row.Cell(2).GetValue<string>();
                        Genres g = new Genres(id, name);
                        genres.Add(g);
                    }
                    log("Read a workbook: genres");
                }
            }
        }
        public void out_db(Tables t) {
            Console.WriteLine();
            if(t == Tables.movies) {
                Movies.shapka();
                foreach(Movies movie in movies) {
                    Console.WriteLine(movie.ToString());
                }
                log("Out database: movies");
            }
            if(t == Tables.filmmakers) {
                Filmmakers.shapka();
                foreach(Filmmakers filmmaker in filmmakers) {
                    Console.WriteLine(filmmaker.ToString());
                }
                log("Out database: filmmakers");
            }
            if(t == Tables.genres) {
                Genres.shapka();
                foreach(Genres genre in genres) {
                    Console.WriteLine(genre.ToString());
                }
                log("Out database: genres");
            }
            Console.WriteLine();
        }
        public void out_db<T>(List<T> table) {
            Console.WriteLine();

            if(table is List<Movies> tmovies) {
                Movies.shapka();
                foreach(Movies movie in tmovies) {
                    Console.WriteLine(movie.ToString());
                }
            }
            if(table is List<Filmmakers> tfilmmakers) {
                Filmmakers.shapka();
                foreach(Filmmakers filmmaker in tfilmmakers) {
                    Console.WriteLine(filmmaker.ToString());
                }
            }
            if(table is List<Genres> tgenres) {
                Genres.shapka();
                foreach(Genres genre in tgenres) {
                    Console.WriteLine(genre.ToString());
                }
            }

            Console.WriteLine();
        }

        public void remove_by_id(string ids, Tables t, bool is_silent) {
            if(!IsExistingID(ids, t, true, is_silent)) {
                return;
            }
            int id = int.Parse(ids);
            if(t == Tables.movies) {
                for(int i = movies.Count - 1; i >= 0; --i) {
                    if(id == movies[i].ID) {
                        movies.RemoveAt(i);
                        log($"Removed entry with ID: {id} from movies");
                    }
                }
            }
            if(t == Tables.genres) {
                for(int i = genres.Count - 1; i >= 0; --i) {
                    if(id == genres[i].ID) {
                        remove_by_ext_id(genres[i].ID, Ext_ids.genres_id);
                        genres.RemoveAt(i);
                        log($"Removed entry with ID: {id} from genres");
                    }
                }
            }
            if(t == Tables.filmmakers) {
                for(int i = filmmakers.Count - 1; i >= 0; --i) {
                    if(id == filmmakers[i].ID) {
                        remove_by_ext_id(filmmakers[i].ID, Ext_ids.filmmakers_id);
                        filmmakers.RemoveAt(i);
                        log($"Removed entry with ID: {id} from filmmakers");
                    }
                }
            }
        }
        public void remove_by_ext_id(int id, Ext_ids eid) {
            if(eid == Ext_ids.filmmakers_id) {
                for(int i = movies.Count - 1; i >= 0; --i) {
                    if(id == movies[i].ID_filmmaker) {
                        movies.RemoveAt(i);
                        log($"Removed entry with ID: {id} from movies");
                    }
                }
            }
            if(eid == Ext_ids.genres_id) {
                for(int i = movies.Count - 1; i >= 0; --i) {
                    if(id == movies[i].ID_genre) {
                        movies.RemoveAt(i);
                        log($"Removed entry with ID: {id} from movies");
                    }
                }
            }
        }
        public void add(Tables t) {
            if(t == Tables.filmmakers) {
                Console.WriteLine("Input: {ID режиссёра} {Имя режиссёра} {Страна происхождения}");
                string[] inp = Console.ReadLine().Split(" ");
                if(inp.Length != 3) {
                    Console.WriteLine("Incorrect amount of arguments");
                    return;
                }
                if(!IsExistingID(inp[0], t, false, false)) {
                    Filmmakers filmmaker = new Filmmakers(int.Parse(inp[0]), inp[1], inp[2]);
                    filmmakers.Add(filmmaker);
                    log($"Added entry with ID: {inp[0]} to filmmakers");
                }

            }
            if(t == Tables.genres) {
                Console.WriteLine("Input: {ID жанра} {Жанр}");
                string[] inp = Console.ReadLine().Split(" ");
                if(inp.Length != 2) {
                    Console.WriteLine("Incorrect amount of arguments");
                    return;
                }
                if(!IsExistingID(inp[0], t, false, false)) {
                    Genres genre = new Genres(int.Parse(inp[0]), inp[1]);
                    genres.Add(genre);
                    log($"Added entry with ID: {inp[0]} to genres");
                }

            }
            if(t == Tables.movies) {
                Console.WriteLine("Input: {ID} {Название фильма} {ID режиссёра} {Год Выхода} {ID жанра} {Продолжительность} {Бюджет} {Cборы}");
                string[] inp = Console.ReadLine().Split(" ");
                if(inp.Length != 8) {
                    Console.WriteLine("Incorrect amount of arguments");
                    return;
                }
                if(!IsExistingID(inp[0], t, false, false) && IsExistingID(inp[2], Tables.filmmakers, true, false) && IsExistingID(inp[4], Tables.genres, true, false) &&
                    IsAccNum(inp[3], 0 ,int.MaxValue) && IsAccNum(inp[5], 0,int.MaxValue)  && IsAccNum(inp[6], 0, int.MaxValue) && IsAccNum(inp[7], 0, int.MaxValue)) {
                    Movies movie = new Movies(int.Parse(inp[0]), inp[1], int.Parse(inp[2]), int.Parse(inp[3]), int.Parse(inp[4]), int.Parse(inp[5]), int.Parse(inp[6]), int.Parse(inp[7]));
                    movies.Add(movie);
                    log($"Added entry with ID: {inp[0]} to movies");
                }

            }
        }
        public void edit(Tables t) {

            if(t == Tables.genres) {
                Console.WriteLine("Input: {ID} (target_fieldname) (value)");
                Console.WriteLine("(target_fieldname) - [ID_жанра/Жанр]");
                string[] inp = Console.ReadLine().Split(" ");
                if(inp.Length != 3) {
                    Console.WriteLine("Incorrect amount of arguments");
                    return;
                }
                if(IsExistingID(inp[0], t, true, false)) {
                    int id = int.Parse(inp[0]);
                    for(int i = genres.Count - 1; i >= 0; --i) {
                        if(id == genres[i].ID) {
                            if(inp[1] == "ID_жанра") {
                                if(!IsExistingID(inp[2], t, false, false)) {
                                    genres[i].ID = int.Parse(inp[2]);
                                    log($"Edited entry with ID: {id} from genres: ID changed from {id} to {inp[2]}");
                                }
                                return;
                            }
                            if(inp[1] == "Жанр") {
                                log($"Edited entry with ID: {id} from genres: Name changed from {genres[i].Name} to {inp[2]}");
                                genres[i].Name = inp[2];
                                
                                return;
                            }
                            Console.WriteLine("Incorrect target_fieldname");
                        }
                    }
                }
            }
            if(t == Tables.filmmakers) {
                Console.WriteLine("Input: {ID} (target_fieldname) (value)");
                Console.WriteLine("(target_fieldname) - [ID_режиссёра/Имя_режиссёра/Страна_происхождения]");
                string[] inp = Console.ReadLine().Split(" ");
                if(inp.Length != 3) {
                    Console.WriteLine("Incorrect amount of arguments");
                    return;
                }
                if(IsExistingID(inp[0], t, true, false)) {
                    int id = int.Parse(inp[0]);
                    for(int i = filmmakers.Count - 1; i >= 0; --i) {
                        if(id == filmmakers[i].ID) {
                            if(inp[1] == "ID_Режиссёра") {
                                if(!IsExistingID(inp[2], t, false, false)) {
                                    log($"Edited entry with ID: {id} from filmmakers: ID changed from {id} to {inp[2]}");
                                    filmmakers[i].ID = int.Parse(inp[2]);
                                }
                                return;
                            }
                            if(inp[1] == "Имя_режиссёра") {
                                log($"Edited entry with ID: {id} from filmmakers: Name changed from {filmmakers[i].Name} to {inp[2]}");
                                filmmakers[i].Name = inp[2];
                                return;
                            }
                            if(inp[1] == "Страна_происхождения") {
                                log($"Edited entry with ID: {id} from filmmakers: Country changed from {filmmakers[i].Country} to {inp[2]}");
                                filmmakers[i].Country = inp[2];
                                return;
                            }
                            Console.WriteLine("Incorrect target_fieldname");
                        }
                    }
                }
            }
            if(t == Tables.movies) {
                Console.WriteLine("Input: {ID} (target_fieldname) (value)");
                Console.WriteLine("(target_fieldname) - [ID/Название_фильма/Год_выхода/ID_жанра/Продолжительность/Бюджет/Сборы]");
                string[] inp = Console.ReadLine().Split(" ");
                if(inp.Length != 3) {
                    Console.WriteLine("Incorrect amount of arguments");
                    return;
                }
                if(IsExistingID(inp[0], t, true, false)) {
                    int id = int.Parse(inp[0]);
                    for(int i = movies.Count - 1; i >= 0; --i) {
                        if(id == movies[i].ID) {
                            if(inp[1] == "ID") {
                                if(!IsExistingID(inp[2], t, false, false)) {
                                    log($"Edited entry with ID: {id} from movies: ID changed from {id} to {inp[2]}");
                                    movies[i].ID = int.Parse(inp[2]);
                                }
                                return;
                            }
                            if(inp[1] == "Название_фильма") {
                                log($"Edited entry with ID: {id} from movies: Name changed from {movies[i].Name} to {inp[2]}");
                                movies[i].Name = inp[2];
                                return;
                            }
                            if(inp[1] == "Год_выхода") {
                                if(IsAccNum(inp[2])){
                                    log($"Edited entry with ID: {id} from movies: Release_date changed from {movies[i].Release_date} to {inp[2]}");
                                    movies[i].Release_date = int.Parse(inp[2]); ;
                                }
                                return;
                            }
                            if(inp[1] == "ID_жанра") {
                                log($"Edited entry with ID: {id} from movies: ID_genre changed from {movies[i].ID_genre} to {inp[2]}");
                                movies[i].ID_genre = int.Parse(inp[2]); ;
                                return;
                            }
                            if(inp[1] == "Продолжительность") {
                                if(IsAccNum(inp[2])) {
                                    log($"Edited entry with ID: {id} from movies: Duration changed from {movies[i].Duration} to {inp[2]}");
                                    movies[i].Duration = int.Parse(inp[2]); ;
                                }
                                return;
                            }
                            if(inp[1] == "Бюджет") {
                                if(IsAccNum(inp[2])) {
                                    log($"Edited entry with ID: {id} from movies: Budget changed from {movies[i].Budget} to {inp[2]}");
                                    movies[i].Budget = int.Parse(inp[2]);
                                }
                                return;
                            }
                            if(inp[1] == "Сборы") {
                                if(IsAccNum(inp[2])) {
                                    log($"Edited entry with ID: {id} from movies: Box_office changed from {movies[i].Box_office} to {inp[2]}");
                                    movies[i].Box_office = int.Parse(inp[2]);// DO TO: Handle big numbers that are converted from string into int. +
                                                                             // Logger to everything +
                                                                             // 4 Queries +
                                                                             // Add comments everywhere !->poh
                                }
                                return;
                            }
                            Console.WriteLine("Incorrect target_fieldname");
                        }
                    }
                }
            }
        }
        public void query1() {//Выведите режисёров из Великобритании
            var fm = from f in filmmakers
                     where f.Country == "Великобритания"
                     select f;
            Console.WriteLine("Result:");
            out_db(fm.ToList());
            log("Query 1 is complete!");
        }
        public void query2() { //Определите долю (в процентах) фильмов, снятых в СССР, среди всех фильмов, снятых с 1920 года по 
            //1960 год(включительно) с бюджетом меньше $1000000.В ответ запишите только целую часть числа.
            var mov = from m in movies
                    where (m.Release_date >= 1920) && (m.Release_date <= 1960) && (m.Budget < 1000000)
                    select m;
            //mov = mov.ToList();
            //Console.WriteLine(mov.Count());
            var ussrf = from f in filmmakers
                    where f.Country == "СССР"
                    select f.ID;
            //Console.WriteLine(ussrf.Count());
            var res = from m in mov
                    where ussrf.ToList().Contains(m.ID_filmmaker)
                    select m;
            //Console.WriteLine(res.Count());
            if(mov.Count() == 0) {
                Console.WriteLine("Result: 0");
                return;
            }
            Console.WriteLine($"Result: {(int)(100.0*res.Count()/mov.Count())}");
            log("Query 2 is complete!");

        }
        public void query3() {//Выведите все фильмы СССР и России в жанре мелодрама
            var ussrnrus = from f in filmmakers
                           where (f.Country == "СССР") || (f.Country == "Россия")
                           select f.ID;
            var melodid = from g in genres
                          where g.Name == "Мелодрама"
                          select g.ID;
            var res = from m in movies
                      where (melodid.ToList().Contains(m.ID_genre)) && (ussrnrus.ToList().Contains(m.ID_filmmaker))
                      select m;
            Console.WriteLine("Result:");
                out_db(res.ToList());
            log("Query 3 is complete!");
        }
        public void query4() {// Сколько фильмов в жанре ужас было снято Стэнли Кубриком и Ларс Фон Триером
            var fm = from f in filmmakers
                     where (f.Name == "Стэнли Кубрик") || (f.Name == "Ларс Фон Триер")
                     select f.ID;
            var horid = from g in genres
                        where g.Name == "Ужас"
                        select g.ID;
            var res = from m in movies
                      where (fm.ToList().Contains(m.ID_filmmaker)) && (horid.ToList().Contains(m.ID_genre))
                      select m;
            Console.WriteLine($"Result: {res.Count()}");
            log("Query 4 is complete!");
        }
    }
}
