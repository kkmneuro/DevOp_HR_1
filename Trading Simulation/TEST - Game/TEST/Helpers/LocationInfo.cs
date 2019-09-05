using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Xml;
using System.Net;

namespace TEST.Helpers
{

    class GeoIpData
    {
       public GeoIpData()
        {
            KeyValue = new Dictionary<string, string>();
        }
        public Dictionary<string, string> KeyValue;
    }


    class LocationInfo
    {

        public string IP { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Text { get; set; }
    }

    class Location
    {



        public static LocationInfo getLocationInfo()
        {
            LocationInfo infor = new LocationInfo();
            try
            {
                GeoIpData gd = GetMy();
                infor.IP =  gd.KeyValue["IP"];
                infor.Zip = gd.KeyValue["ZipCode"];
                infor.Country = gd.KeyValue["CountryName"];
                infor.City = gd.KeyValue["City"];
                infor.Text = gd.KeyValue["Text"];
               

            }
            catch (Exception a)
            {
                infor.IP = getIp();
            }
            
            

            return infor;
        }


        static public GeoIpData GetMy()
        {
            string url = "http://freegeoip.net/xml/";
            WebClient wc = new WebClient();
            wc.Proxy = null;
            MemoryStream ms = new MemoryStream(wc.DownloadData(url));
         //   XmlTextReader rdr = new XmlTextReader(url);
            XmlDocument doc = new XmlDocument();
            ms.Position = 0;
            doc.Load(ms);
            ms.Dispose();
            GeoIpData retval = new GeoIpData();
            retval.KeyValue.Add("Text", doc.OuterXml);
            foreach (XmlElement el in doc.SelectSingleNode("/Response").ChildNodes)
            {
                retval.KeyValue.Add(el.Name, el.InnerText);
            }
            //retval.KeyValue.Add("Text", doc.InnerText);
            return retval;
        }



        public static string getIp()
        {
            try
            {
                string externalip = new WebClient().DownloadString("http://icanhazip.com");
                return externalip;
            }
            catch (Exception e)
            {
                try
                {
                    string ipas = new WebClient().DownloadString("http://bot.whatismyipaddress.com/");
                    return ipas;
                }
                catch(Exception ee)
                {
                    string pubIsp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                    return pubIsp;
                }
            }
            
        }
    }
}
