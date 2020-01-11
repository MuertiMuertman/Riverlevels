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
            string url = "https://www.protezionecivile.fvg.it/it/stampa-dati?station_id=707&sensor_id=WATER_LEVEL";
            Console.WriteLine(htmlagtest(url));
            //Console.WriteLine(getWebsite(url));
        }

        static string getWebsite(string url)
        {
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
            }
        }

        static string htmlagtest(string url)
        {
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);
            try
            {
                return doc.DocumentNode.SelectNodes(
                   "/table/tbody/tr/td[1]")[1].InnerText;
            }
            catch(Exception e) { return e.Message; }
           // / html / body / table / tbody / tr[1]
        }
    }
}
