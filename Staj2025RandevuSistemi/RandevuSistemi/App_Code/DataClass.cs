using RandevuSistemi.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RandevuSistemi.App_Code
{
    public class DataClass:MainClass
    {
       
        public List<RandevuListesiClass> RandevuListesi(string IP, Guid? IslemYapanUser)
        {
            List<RandevuListesiClass> sonuc = new List<RandevuListesiClass>();
            string Query = @"SELECT 
                        danisan.KullaniciAdiSoyadi AS DANISAN, 
                        uzman.KullaniciAdiSoyadi AS Uzman, 
                        r.RandevuTarihSaati as TarihSaat, 
                        ortam.ParametreDeger as RandevuOrtami,
                        r.RandevuId
                     FROM Randevular r
                     INNER JOIN Kullanicilar uzman ON uzman.KullaniciGuidId=r.UzmanGuidId
					 inner join Kullanicilar danisan on danisan.KullaniciGuidId = r.DanisanGuidId
					 inner join Parametreler ortam on ortam.ParametreAdi = r.RandevuOrtami ";
            SqlCommand cmd;
            DataTable dt = new DataTable();
            string sil = "<a href=\"javascript:RandevuSil('{0}')\">Sil</a>";
            string duzenle = "<a href=\"javascript:RandevuDuzenle('{0})'\">Düzenle</a>";
           
            try
            {
                using (con = new SqlConnection(WEB_con_str))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (cmd = new SqlCommand(Query, con))
                    {
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(dt);
                        }
                    }
                }

                foreach (DataRow item in dt.Rows)
                {
                    sonuc.Add(new RandevuListesiClass
                    {
                        DanisanAdiSoyadi = item["DANISAN"].ToString(),
                        UzmanAdiSoyadi = item["Uzman"].ToString(),
                        RandevuTarihSaatiStr = item["TarihSaat"].ToString(),
                        RandevuTarihSaati = Convert.ToDateTime (item["TarihSaat"].ToString()),
                        RandevuOrtami = item["RandevuOrtami"].ToString(),
                        RandevuDuzenle=string.Format(duzenle, item["RandevuId"].ToString()),
                        RandevuSil = string.Format(sil, item["RandevuId"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                LoggerClass.ErrorLog(IslemYapanUser, "RandevuListesi", IP, ex.ToString());
                sonuc = null;
            }
            return sonuc;
        }


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


        public List<SelectJsonClass> ParametreGetir(string ParametreGrup,string BosStr,string Ip,Guid? UserId)
        {
            List<SelectJsonClass> sonuc = null;
            string Metod = "ParametreGetir";

            SelectJsonClass tmp = null;
            List<SelectJsonClass> tmpList = null;
            try
            {
                sonuc = new List<SelectJsonClass>();
                if (BosStr!=null)
                {
                    tmp = new SelectJsonClass();
                    tmp.text = BosStr;
                    tmp.id = "-1";
                    sonuc.Add(tmp);
                }

                using (db=new dbPdrMerEntities())
                {
                    tmpList = db.Parametreler.Where(a => a.ParametreGrup == ParametreGrup).Select(a=>  new SelectJsonClass {id=a.ParametreAdi,text=a.ParametreDeger }).OrderBy(a=> a.text).ToList() ;
                }
                if (tmpList!=null)
                {
                    sonuc.AddRange(tmpList);
                }
                

            }
            catch (Exception ex)
            {

                LoggerClass.ErrorLog(UserId,Metod,Ip,ex.ToString());
            }

            return sonuc;
        }


        public SessionDataClass Login(string KullaniciEpostasi, string Sifre, string Ip, Guid? UserId)
        {
            SessionDataClass sonuc = null;
            string Metod = "Login";
            Kullanicilar data;
            string SifreCozulmus;
            AESSinif ans = new AESSinif();
            try
            {
 
                
                using (db = new dbPdrMerEntities())
                {
                    data = db.Kullanicilar.Where(a => a.KullaniciEpostasi == KullaniciEpostasi).FirstOrDefault() ;
                    if (data!=null)
                    {
                        SifreCozulmus = ans.AESsifreCoz(data.KullaniciParolasi);
                        if (SifreCozulmus== Sifre)
                        {
                            sonuc = new SessionDataClass();
                            sonuc.KullaniciTipi = data.KullaniciTipi;
                            sonuc.AdiSoyadi = data.KullaniciAdiSoyadi;
                            sonuc.UserId = data.KullaniciGuidId.Value;
                           
                        }
                    }
                }
                


            }
            catch (Exception ex)
            {

                LoggerClass.ErrorLog(UserId, Metod, Ip, ex.ToString());
            }

            return sonuc;
        }

    }
}