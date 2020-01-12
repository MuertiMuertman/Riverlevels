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
            Console.WriteLine(Taglia.Pegel[3].Date>Taglia.Pegel[2].Date);
        }
    }
}
