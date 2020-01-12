using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pegelstaende
{
    public class CollectInformationTagliamento
    {
        public List<(string Date, string Level)> Pegel;
        public CollectInformationTagliamento()
        {
            Pegel = getLevelInformation();
        }
        private List<(string Date, string Level)> getLevelInformation()
        {
            string url = "https://www.protezionecivile.fvg.it/it/stampa-dati?station_id=707&sensor_id=WATER_LEVEL";
            List<(string Date, string Level)> ListofLevels = new List<(string,string)>();
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);
            try
            {
                HtmlNodeCollection hmnodes = doc.DocumentNode.SelectNodes(
                   "/table/tbody/tr/td");
                for (int i = 0;i<hmnodes.Count; i = i+2)
                {
                    ListofLevels.Add((hmnodes[i].InnerText, hmnodes[i+1].InnerText));
                }
                return ListofLevels;
            }
            catch (Exception e) 
            {
                ListofLevels.Add((e.Message, ""));
                return ListofLevels; 
            }
        }
    }
}
