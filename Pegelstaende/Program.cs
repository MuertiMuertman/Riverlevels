using System;
using System.Net;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace Pegelstaende
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ICollectInformation> Fluesse = new List<ICollectInformation>();
            Fluesse.Add(new CollectInformationTagliamento());
            Fluesse.Add(new CollectInformationPiave());
            Boolean WriteSuccess;
            foreach(ICollectInformation Fluss in Fluesse)
            {
                WriteSuccess = SaveDataToFile.WriteData
                    (Fluss.Pegel,
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/Pegelstaende/{Fluss.Flussname}.txt");
                Console.WriteLine(WriteSuccess);
            }
        }
    }
}
