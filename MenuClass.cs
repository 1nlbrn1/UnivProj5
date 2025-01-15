using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivLabProj5 {
    class Menu {
        Exdb ex;
        //public Menu() { }
        public Menu(string path) {
            ex = new Exdb(path);
        }
        string menu_str() {
            return
                "Command List:\n"+
                "READ [movies/filmmakers/genres/all]\n" +
                "OUT [movies/filmmakers/genres]\n" +
                "REM [movies/filmmakers/genres] [{ID}]\n" +
                "ADD [movies/filmmakers/genres]\n" +
                "EDIT [movies/filmmakers/genres]\n" +
                "QUERY [1-4]\n"
                ;
        }
        public void activate() {
            Console.WriteLine(menu_str());
            string inp;
            while(true) {
                inp = Console.ReadLine();
                processinp(inp);
            }
        }
        void processinp(string inp) {
            bool isvalid_arg_0 = false;
            string[] inpspl = inp.Split(" ");
            if(inpspl[0] == "HELP" && inpspl.Length==1) {
                isvalid_arg_0 = true;
                Console.WriteLine(menu_str());
            }
            if(inpspl[0] == "READ" && inpspl.Length == 2) {
                isvalid_arg_0 = true;
                if(inpspl[1] == "movies") {
                    ex.read_db(Tables.movies);
                    return;
                }
                if(inpspl[1] == "filmmakers") {
                    ex.read_db(Tables.filmmakers);
                    return;
                }
                if(inpspl[1] == "genres") {
                    ex.read_db(Tables.genres);
                    return;
                }
                if(inpspl[1] == "all") {
                    ex.read_db(Tables.movies);
                    ex.read_db(Tables.filmmakers);
                    ex.read_db(Tables.genres);
                    return;
                }
                Console.WriteLine("Error in READ command: READ [movies/filmmakers/genres]");

            }
            if(inpspl[0] == "OUT" && inpspl.Length == 2) {
                isvalid_arg_0 = true;
                if(inpspl[1] == "movies") {
                    ex.out_db(Tables.movies);
                    return;
                }
                if(inpspl[1] == "filmmakers") {
                    ex.out_db(Tables.filmmakers);
                    return;
                }
                if(inpspl[1] == "genres") {
                    ex.out_db(Tables.genres);
                    return;
                }
                Console.WriteLine("Error in OUT command: OUT [movies/filmmakers/genres]");
            }
            if(inpspl[0] == "REM" && inpspl.Length == 3) {
                isvalid_arg_0 = true;
                bool isvalid_arg_1 = false;
                if(inpspl[1] == "movies") {
                    isvalid_arg_1 = true;
                    ex.remove_by_id(inpspl[2], Tables.movies, false);
                }
                if(inpspl[1] == "filmmakers") {
                    isvalid_arg_1 = true;
                    ex.remove_by_id(inpspl[2], Tables.filmmakers, false);
                }
                if(inpspl[1] == "genres") {
                    isvalid_arg_1 = true;
                    ex.remove_by_id(inpspl[2], Tables.genres, false);
                }
                if(!isvalid_arg_1) {
                    Console.WriteLine("Error in REM command: REM [movies/filmmakers/genres] [{ID}]");
                }
            }
            if(inpspl[0] == "ADD" && inpspl.Length == 2) {
                isvalid_arg_0 = true;
                bool isvalid_arg_1 = false;
                if(inpspl[1] == "movies") {
                    isvalid_arg_1 = true;
                    ex.add(Tables.movies);
                }
                if(inpspl[1] == "filmmakers") {
                    isvalid_arg_1 = true;
                    ex.add(Tables.filmmakers);
                }
                if(inpspl[1] == "genres") {
                    isvalid_arg_1 = true;
                    ex.add(Tables.genres);
                }
                if(!isvalid_arg_1) {
                    Console.WriteLine("Error in ADD command: ADD [movies/filmmakers/genres]");
                }
            }
            if(inpspl[0] == "EDIT" && inpspl.Length == 2) {
                isvalid_arg_0 = true;
                bool isvalid_arg_1 = false;
                if(inpspl[1] == "movies") {
                    isvalid_arg_1 = true;
                    ex.edit(Tables.movies);
                }
                if(inpspl[1] == "filmmakers") {
                    isvalid_arg_1 = true;
                    ex.edit(Tables.filmmakers);
                }
                if(inpspl[1] == "genres") {
                    isvalid_arg_1 = true;
                    ex.edit(Tables.genres);
                }
                if(!isvalid_arg_1) {
                    Console.WriteLine("Error in ADD command: ADD [movies/filmmakers/genres]");
                }
            }
            if(inpspl[0] == "QUERY" && inpspl.Length == 2) {
                isvalid_arg_0 = true;
                if(inpspl[1] == "1") {
                    ex.query1();
                    return;
                }
                if(inpspl[1] == "2") {
                    ex.query2();
                    return;
                }
                if(inpspl[1] == "3") {
                    ex.query3();
                    return;
                }
                if(inpspl[1] == "4") {
                    ex.query4();
                    return;
                }
                Console.WriteLine("Error in QUERY command: QUERY [1-4]");

            }
            if(!isvalid_arg_0) {
                Console.WriteLine("Invalid command");
            }
        }
    }
}
