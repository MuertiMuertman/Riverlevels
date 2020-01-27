using System;
using System.Net;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace Pegelstaende
{
    class Program
    {
        static void Main(string[] args)
        {
            CollectInformationTagliamento Taglia = new CollectInformationTagliamento();
            Boolean WriteSuccess;
            WriteSuccess = SaveDataToFile.WriteData
                (Taglia.Pegel, $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/Pegelstaende");
            Console.WriteLine(WriteSuccess);
        }
    }
}
