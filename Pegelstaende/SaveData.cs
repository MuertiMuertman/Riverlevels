using System;
using System.Collections.Generic;
using System.Text;

namespace Pegelstaende
{
    public static class SaveDataToFile
    {
        public  static Boolean WriteData(List<(DateTime Date, string Level)> Pegel, string filename)
        {
            try
            {
                Console.WriteLine(filename);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{filename}", true))
                { 
                    foreach ((DateTime Date, string Level) in Pegel)
                    {
                        file.Write($"{Date.ToString("dd.MM.yyyy HH:mm:ss")};{Level}\n");
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
