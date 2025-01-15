using ClosedXML.Excel;
using System;
namespace UnivLabProj5 {
    class Program {
        static void Main(string[] args) {
            // Path to the Excel file
            /*
            string filePath = @"C:\Users\Dmitry\source\repos\UnivLabProj5\UnivLabProj5\LR5-var14.xlsx";

            Console.WriteLine("Attempting to open workbook...");
            using(var workbook = new XLWorkbook(filePath)) {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();

                foreach(var row in rows) {
                    for(int col = 1; col <= 8; col++) {
                        var cellValue = row.Cell(col).GetValue<string>();
                        Console.Write($"{cellValue}\t");
                    }
                    Console.WriteLine();
                }
            }
            Exdb ex = new Exdb();
            ex.log_init();
            ex.log("Read a workbook");
            */

            /*
            string filePath = @"C:\Users\Dmitry\source\repos\UnivLabProj5\UnivLabProj5\LR5-var14.xlsx";
            Exdb ex = new Exdb(filePath);
            ex.read_db();
            ex.out_db();
            */
            //Genres genre = new Genres(0, "Horror");
            //Console.WriteLine(genre.ToString());

            /*
            string filePath = @"C:\Users\Dmitry\source\repos\UnivLabProj5\UnivLabProj5\LR5-var14.xlsx";
            Exdb ex = new Exdb(filePath);
            ex.read_db(Tables.genres);
            ex.out_db(Tables.genres);
            */
            string filePath = @"C:\Users\Dmitry\source\repos\UnivLabProj5\UnivLabProj5\LR5-var14.xlsx";
            Menu menu = new Menu(filePath);
            menu.activate();

        }
    }
}
