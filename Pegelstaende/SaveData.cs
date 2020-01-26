using System;
using System.Collections.Generic;
using System.Text;

namespace Pegelstaende
{
    public static class SaveDataToFile
    {
        public  static Boolean WriteData(List<(DateTime Date, string Level)> Pegel, string filepath)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{filepath}\\pegel.txt", true))
                { 
                    foreach ((DateTime Date, string Level) tplPegel in Pegel)
                    {
                        file.Write($"{tplPegel.Date};{tplPegel.Level}\n");
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
