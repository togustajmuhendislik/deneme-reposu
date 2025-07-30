using RandevuSistemi.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace RandevuSistemi.App_Code
{
    public class DataClass : MainClass
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
                        RandevuTarihSaati = Convert.ToDateTime(item["TarihSaat"].ToString()),
                        RandevuOrtami = item["RandevuOrtami"].ToString(),
                        RandevuDuzenle = string.Format(duzenle, item["RandevuId"].ToString()),
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

        public FullCalendarJsonDataClass UzmanTakvimGetir(Guid userId, string IP)
        {
            string metod = "DataClass.UzmanTakvimGetir";

            try
            {
                using (SqlConnection connection = new SqlConnection(WEB_con_str))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_uzmanTakvim", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FullCalendarJsonEventClass etkinlik = new FullCalendarJsonEventClass();
                                //etkinlik.id = Guid.NewGuid().ToString();
                                etkinlik.id = reader["RandevuId"].ToString();
                                etkinlik.title = reader["Baslik"].ToString();
                                etkinlik.start = MainClass.DateTimeToStringCalendarFormat(Convert.ToDateTime(reader["BaslangicTarihi"]));
                                etkinlik.end = MainClass.DateTimeToStringCalendarFormat(Convert.ToDateTime(reader["BitisTarihi"]));
                                etkinlik.color = reader["Renk"].ToString();
                                etkinlik.allDay = false;
                                etkinlik.source = reader["Kaynak"].ToString();

                                sonuc.events.Add(etkinlik);
                            }
                        }
                    }
                }

                DataTable dt = new DataTable();

                using (con = new SqlConnection(WEB_con_str))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    using (SqlCommand command = new SqlCommand("sp_uzmanTakvim", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataAdapter adp = new SqlDataAdapter(command))
                        {
                            adp.Fill(dt);
                        }
                    }
                }

                //List<FullCalendarJsonEventClass> xxx = dt.AsEnumerable().Select(a => new FullCalendarJsonEventClass { color = a["color"].ToString() }).ToList();

                FullCalendarJsonEventClass tmp;
                List<FullCalendarJsonEventClass> listt = new List<FullCalendarJsonEventClass>();
                foreach (DataRow item in dt.AsEnumerable())
                {
                    tmp = new FullCalendarJsonEventClass();
                    tmp.color = item["color"].ToString();
                    listt.Add(tmp);

                }
                sonuc.events = listt;

                //using (db = new dbPdrMerEntities())
                //{
                //    List<sp_uzmanTakvim_Result> dd = db.sp_uzmanTakvim(userId).ToList();
                //    List< FullCalendarJsonEventClass > sonnn = dd.Select(a => new FullCalendarJsonEventClass {color= a.Renk}).ToList();
                //}

                // FullCalendar'ın varsayılan görünüm ayarları
                sonuc.defaultDate = DateTime.Now.ToString("yyyy-MM-dd");
                sonuc.firstDay = "1"; // Haftanın ilk gününü Pazartesi (1) olarak ayarla.
            }
            catch (Exception ex)
            {
                sonuc = new FullCalendarJsonDataClass();
                System.Diagnostics.Debug.WriteLine("UzmanTakvimGetir Hata: " + ex.Message);
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


        public List<SelectJsonClass> ParametreGetir(string ParametreGrup, string BosStr, string Ip, Guid? UserId)
        {
            List<SelectJsonClass> sonuc = null;
            string Metod = "ParametreGetir";

            SelectJsonClass tmp = null;
            List<SelectJsonClass> tmpList = null;
            try
            {
                sonuc = new List<SelectJsonClass>();
                if (BosStr != null)
                {
                    tmp = new SelectJsonClass();
                    tmp.text = BosStr;
                    tmp.id = "-1";
                    sonuc.Add(tmp);
                }

                using (db = new dbPdrMerEntities())
                {
                    tmpList = db.Parametreler.Where(a => a.ParametreGrup == ParametreGrup).Select(a => new SelectJsonClass { id = a.ParametreAdi, text = a.ParametreDeger }).OrderBy(a => a.text).ToList();
                }
                if (tmpList != null)
                {
                    sonuc.AddRange(tmpList);
                }


            }
            catch (Exception ex)
            {

                LoggerClass.ErrorLog(UserId, Metod, Ip, ex.ToString());
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
                    data = db.Kullanicilar.Where(a => a.KullaniciEpostasi == KullaniciEpostasi).FirstOrDefault();
                    if (data != null)
                    {
                        SifreCozulmus = ans.AESsifreCoz(data.KullaniciParolasi);
                        if (SifreCozulmus == Sifre)
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