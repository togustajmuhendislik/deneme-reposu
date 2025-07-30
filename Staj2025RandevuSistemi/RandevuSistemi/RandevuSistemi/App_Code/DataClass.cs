using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandevuSistemi.App_Code
{
    public class DataClass:MainClass
    {
        public FullCalendarJsonDataClass OrnekTakvimGetirAylik(string AyYil, string IP, string UserId)
        {
            // AyYil formatı: "yyyy-MM" örn: "2025-07"
            string Metod = "";
            FullCalendarJsonDataClass sonuc = null;

            try
            {
                var yilAy = AyYil.Split('-');
                int yil = int.Parse(yilAy[0]);
                int ay = int.Parse(yilAy[1]);
                DateTime bas = new DateTime(yil, ay, 1);
                DateTime bit = bas.AddMonths(1).AddDays(-1);

                sonuc = new FullCalendarJsonDataClass();
                sonuc.defaultDate = bas.ToString("yyyy-MM-dd");
                sonuc.firstDay = ((int)bas.DayOfWeek).ToString();
                sonuc.events = new List<FullCalendarJsonEventClass>();

                // Örnek: Her güne bir etkinlik ekle
                for (DateTime dt = bas; dt <= bit; dt = dt.AddDays(1))
                {
                    var tmp = new FullCalendarJsonEventClass();
                    tmp.title = $"Etkinlik {dt.Day}";
                    tmp.start = MainClass.DateTimeToStringCalendarFormat(dt);
                    tmp.end = MainClass.DateTimeToStringCalendarFormat(dt);
                    tmp.color = "#dd813e";
                    tmp.overlap = false;
                    sonuc.events.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                sonuc = null;
                // logger class ile log gir
            }
            return sonuc;
        }
    }
}