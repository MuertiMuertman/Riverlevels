using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace Pegelstaende
{
    public static class SaveDataToFile
    {
        public  static Boolean WriteData(List<(DateTime Date, double Level)> Pegel, string filename)
        {
            Pegel.AddRange(SaveDataToFile.ReadDatafromFile(filename));
            Pegel.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            Pegel = Pegel.Distinct().ToList();
            try
            {
                Console.WriteLine(filename);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{filename}", false))
                { 
                    foreach ((DateTime Date, double Level) in Pegel)
                    {
                        file.Write(
                            $"{Date.ToString("dd.MM.yyyy HH:mm:ss")};" +
                            $"{Level.ToString(CultureInfo.CreateSpecificCulture("de-DE"))}\n");
                    }    
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }
        private static List<(DateTime Date, double Level)> ReadDatafromFile(string filename)
        {
            var lines = System.IO.File.ReadLines(filename);
            List<(DateTime Date, double Level)> ListeDaten = new List<(DateTime Date, double Level)>();
            string[] linesplit;
       
            foreach(string line in lines)
            {
                linesplit = line.Split(";");
                ListeDaten.Add((DateTime.Parse(linesplit[0], CultureInfo.CreateSpecificCulture("de-DE")),
                                    double.Parse(linesplit[1], CultureInfo.CreateSpecificCulture("de-DE"))));
            }
            return ListeDaten;
        }
    }
}
