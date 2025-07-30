using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace RandevuSistemi.App_Code
{
    public class MainClass
    {
        public static string ConvertJsonData(object data)
        {
            return new JavaScriptSerializer().Serialize(data);
        }

        public static string DateTimeToStringCalendarFormat(DateTime data)
        {
            string Sonuc = "";
            Sonuc = data.ToString("yyyy-MM-ddTHH:mm:ss");
            return Sonuc;
        }

        public class FullCalendarJsonDataClass
        {
            public FullCalendarJsonDataClass()
            {
                events = new List<FullCalendarJsonEventClass>();
            }
            public string defaultDate { get; set; }
            public string firstDay { get; set; }

            public List<FullCalendarJsonEventClass> events { get; set; }
        }

        public class FullCalendarJsonEventClass
        {
            public string id { get; set; }
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string color { get; set; }
            public bool overlap { get; set; }
            public FullCalendarJsonEventDataClass data { get; set; }

        }

        public class FullCalendarJsonEventDataClass
        {
            public string DerslikTakvimId { get; set; }
            public string SinavId { get; set; }
            public string SinavAdi { get; set; }
        }

    }
}