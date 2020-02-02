using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Pegelstaende
{
    public static class SaveDataToFile
    {
        public  static Boolean WriteData(List<(DateTime Date, double Level)> Pegel, string filename)
        {
            try
            {
                Console.WriteLine(filename);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{filename}", true))
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
    }
}
