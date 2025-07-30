using RandevuSistemi.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandevuSistemi.Sekreter
{
    public partial class AnasayfaSekreter : System.Web.UI.Page
    {
        [WebMethod]
        public static string BindServerCalendar()
        {
            // Burada takvim etkinlik verilerini oluşturacaksınız.
            // Bu kısım, veritabanından veri çekme, randevuları listeleme vb. işlemleri yapacağınız yerdir.
            // Örnek olarak statik verilerle dolduralım:

            var events = new List<object>();

            // Örnek etkinlikler
            events.Add(new { title = "Doktor Randevusu - Ayşe Yılmaz", start = DateTime.Now.AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss"), end = DateTime.Now.AddHours(3).ToString("yyyy-MM-ddTHH:mm:ss"), color = "#4CAF50" }); // Yeşil
            events.Add(new { title = "Toplantı - Personel", start = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddT09:00:00"), end = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddT10:30:00"), color = "#FFC107" }); // Sarı
            events.Add(new { title = "Kontrol - Mehmet Can", start = DateTime.Now.AddDays(-2).AddHours(4).ToString("yyyy-MM-ddTHH:mm:ss"), end = DateTime.Now.AddDays(-2).AddHours(5).ToString("yyyy-MM-ddTHH:mm:ss"), color = "#2196F3" }); // Mavi
            events.Add(new { title = "Acil Durum", start = DateTime.Now.AddDays(3).ToString("yyyy-MM-ddT11:00:00"), end = DateTime.Now.AddDays(3).ToString("yyyy-MM-ddT12:00:00"), color = "#F44336" }); // Kırmızı

            var calendarData = new
            {
                defaultDate = DateTime.Now.ToString("yyyy-MM-dd"), // Takvimin varsayılan olarak hangi tarihte açılacağını belirtir
                firstDay = 1, // Haftanın başlangıç günü (0: Pazar, 1: Pazartesi)
                events = events // Oluşturduğumuz etkinlik listesi
            };

            // JSON formatına dönüştür ve geri döndür
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(calendarData);
        }

    }
}