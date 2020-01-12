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
            Console.WriteLine(Taglia.Pegel[1].Level);
        }
    }
}
