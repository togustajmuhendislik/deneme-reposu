using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using RandevuSistemi.App_Code;
namespace RandevuSistemi.App_Code
{
    public class MainClass
    {
        protected dbPdrMerEntities db;
        protected SqlConnection con;
        protected static string WEB_con_str;
        protected string MailUserName;
        protected string MailSifre;
        protected string DogrulamaUrl;

        public MainClass()
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["Con_Web"];
            WEB_con_str = mySetting.ConnectionString;
            MailUserName = WebConfigurationManager.AppSettings["MailUserName"];
            MailSifre = WebConfigurationManager.AppSettings["MailSifre"];
            DogrulamaUrl = WebConfigurationManager.AppSettings["DogrulamaUrl"];
        }
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

       
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



    }
    public class RandevuListesiClass
    {
        public string DanisanAdiSoyadi { get; set; }
        public string UzmanAdiSoyadi { get; set; }
        public string RandevuTarihSaatiStr  { get; set; }
        public DateTime RandevuTarihSaati { get; set; }
        public string RandevuOrtami { get; set; }
        public string RandevuSil { get; set; }
        public string RandevuDuzenle { get; set; }

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

    public class SessionDataClass
    {
        public Guid UserId { get; set; }
        public string AdiSoyadi { get; set; }
        public string KullaniciTipi { get; set; }
        public string Mesaj { get; set; }

    }
    

    public class SelectJsonClass
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }
    }


    public class LoggerClass
    {
        public static void ErrorLog(Guid? UserId, string Metot, string IP, string LogDeger)
        {
            string LogTuru = "Error";
            SistemLog Logdata;

            using (dbPdrMerEntities db = new dbPdrMerEntities())
            {
                Logdata = new SistemLog();
                Logdata.Metod = Metot;
                Logdata.LogDeger = LogDeger;
                Logdata.LogTipi = LogTuru;
                Logdata.LogDate = DateTime.Now;
                Logdata.UserId = UserId;
                Logdata.IP = IP;
                db.SistemLog.Add(Logdata);
                db.SaveChanges();
            }
        }

        public static void InfoLog(Guid? UserId, string Metot, string IP, string LogDeger)
        {
            string LogTuru = "Info";
            SistemLog Logdata;
            using (dbPdrMerEntities db = new dbPdrMerEntities())
            {
                Logdata = new SistemLog();
                Logdata.Metod = Metot;
                Logdata.LogDeger = LogDeger;
                Logdata.LogTipi = LogTuru;
                Logdata.LogDate = DateTime.Now;
                Logdata.UserId = UserId;
                Logdata.IP = IP;
                db.SistemLog.Add(Logdata);
                db.SaveChanges();
            }
        }

    }


}