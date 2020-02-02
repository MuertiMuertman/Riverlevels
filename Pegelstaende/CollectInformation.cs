using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Pegelstaende
{
    public interface ICollectInformation
    {
        public List<(DateTime Date, double Level)> Pegel { get; }
        public string Flussname { get; }
    }
    public class CollectInformationTagliamento : ICollectInformation
    {
        public List<(DateTime Date, double Level)> Pegel { get; }
        public string Flussname { get { return "Tagliamento"; } }
        public CollectInformationTagliamento()
        {
            Pegel = getLevelInformation();
        }
        private List<(DateTime Date, double Level)> getLevelInformation()
        {
            string url = "https://www.protezionecivile.fvg.it/it/stampa-dati?station_id=707&sensor_id=WATER_LEVEL";
            List<(DateTime Date, double Level)> ListofLevels = new List<(DateTime, double)>();
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);
            try
            {
                HtmlNodeCollection hmnodes = doc.DocumentNode.SelectNodes(
                   "/table/tbody/tr/td");
                for (int i = 0;i<hmnodes.Count; i = i+2)
                {
                    string strLevel = hmnodes[i + 1].InnerText;
                    strLevel = strLevel.Remove(strLevel.Length - 1).Replace(".",",");
                    double parsedLevel = double.Parse(strLevel,NumberStyles.Float,
                        CultureInfo.CreateSpecificCulture("de-DE"));
                    ListofLevels.Add((DateTime.Parse(hmnodes[i].InnerText), parsedLevel));
                }
                return ListofLevels;
            }
            catch (Exception e) 
            {
                ListofLevels.Add((DateTime.Now, -1));
                Console.WriteLine($"{e.Message} {e.StackTrace}");
                return ListofLevels; 
            }
        }
    }
    public class CollectInformationPiave : ICollectInformation
    {
        public List<(DateTime Date, double Level)> Pegel { get; }

        public string Flussname { get { return "Piave"; } }
        public CollectInformationPiave()
        {
            Pegel = getLevelInformation();
        }
        private List<(DateTime Date, double Level)> getLevelInformation()
        {
            string url = "https://www.arpa.veneto.it/temi-ambientali/acqua/datiacqua/dati_idrometeo_stazione.php?id=309&sr=1&idsr=300005790";
            List<(DateTime Date, double Level)> ListofLevels = new List<(DateTime, double)>();
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);
            try
            {
                HtmlNodeCollection hmnodes = doc.DocumentNode.SelectNodes(
                   "//*[@id='datiidrometeo']/table/tr/td");
                for (int i = 0; i < hmnodes.Count; i = i + 3)
                {
                    string strLevel = hmnodes[i + 2].InnerText;
                    strLevel = strLevel.Replace(".", ",");
                    double parsedLevel = double.Parse(strLevel, NumberStyles.Float,
                        CultureInfo.CreateSpecificCulture("de-DE"));
                    ListofLevels.Add
                        ((DateTime.Parse($"{hmnodes[i].InnerText} {hmnodes[i+1].InnerText}",
                            System.Globalization.CultureInfo.CreateSpecificCulture("de-DE")),
                        parsedLevel));
                }
                ListofLevels.Reverse();
                return ListofLevels;
            }
            catch (Exception e)
            {
                ListofLevels.Add((DateTime.Now, -1));
                Console.WriteLine($"{e.Message} {e.StackTrace}");
                return ListofLevels;
            }
        }
    }
}
