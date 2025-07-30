using RandevuSistemi.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandevuSistemi
{
    public partial class Login : System.Web.UI.Page
    {
        // Yeni eklenen kontrolleri tanımlayalım
        protected TextBox txtSifreUnutEmail;
        protected TextBox txtKayitAdSoyad;
        protected TextBox txtKayitEmail;
        protected TextBox txtKayitSifre;
        protected TextBox txtKayitSifreTekrar;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Eğer bir modaldan bir PostBack olduysa ve mesaj varsa, modalı tekrar açmak için
            // JavaScript kodu gönderilir. Bu, kullanıcının bir hata veya başarı mesajı aldıktan sonra
            //// modalın kapanmasını engeller.
            //if (IsPostBack)
            //{
            //    if (Session["OpenModal"] != null && hidMesaj.Value.Contains("|"))
            //    {
            //        string modalToOpen = Session["OpenModal"].ToString();
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "openModalScript", $"$(document).ready(function() {{ $('#{modalToOpen}Modal').modal('show'); }});", true);
            //        Session["OpenModal"] = null; // Modalı açtıktan sonra session'ı temizle
            //    }
            //}

            SessionDataClass data = new SessionDataClass();
            data.AdiSoyadi = "";
            data.UserId =Guid.Parse("b21844fd-a0e1-4312-bf7c-0ce3fa27149c");
            Session["oturum"] = data;
            Response.Redirect("Sekreter/AnasayfaSekreter.aspx");

        }
        protected void btnGiris_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string sifre = txtPassword.Text.Trim();

            // Basit kullanıcı doğrulama örneği
            if (email == "admin@admin.com" && sifre == "1234")
            {
                Session["KullaniciEmail"] = email;
                Response.Redirect("AnaSayfa.aspx");
            }
            else
            {
                // Hatalı giriş bildirimi
                hidMesaj.Value = "0|E-posta veya şifre hatalı.";
            }
        }

        //protected void btnGiris_Click(object sender, EventArgs e)
        //{
        //    string email = txtEmail.Text.Trim();
        //    string sifre = txtPassword.Text.Trim();

        //    // Örnek kontrol
        //    if (email == "admin@admin.com" && sifre == "1234")
        //    {
        //        Session["KullaniciEmail"] = email;
        //        Response.Redirect("AnaSayfa.aspx");
        //    }
        //    else
        //    {
        //        hidMesaj.Value = "0|E-posta veya şifre hatalı.";
        //    }
        //}

        //protected void btnSifreGonder_Click(object sender, EventArgs e)
        //{
        //    // Bu metot, Şifremi Unuttum modalı içindeki butonun Click olayıdır.
        //    string email = txtSifreUnutEmail.Text.Trim();

        //    if (string.IsNullOrEmpty(email))
        //    {
        //        hidMesaj.Value = "0|Lütfen e-posta adresinizi girin.";
        //        Session["OpenModal"] = "forgotPassword"; // Modalın açık kalması için session'a bilgi kaydet
        //        return;
        //    }

        //    // Şifre sıfırlama işlemi yapılabilir (veritabanı kontrolü, mail gönderme vb.)
        //    // Örneğin, burada e-posta gönderimi simüle edilebilir.
        //    // DataClass dc = new DataClass();
        //    // bool result = dc.SifreSifirla(email); // Varsayımsal bir metod çağrısı

        //    // Başarılı olursa
        //    hidMesaj.Value = "1|Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";
        //    // Başarısız olursa
        //    // hidMesaj.Value = "0|E-posta adresiniz sistemimizde kayıtlı değil.";

        //    // Modalı kapatmak istemiyorsanız aşağıdaki satırı yorum satırı yapmayın.
        //    // İşlem başarılı olduğunda modalın otomatik kapanmasını istiyorsanız bu satırı kaldırmayın.
        //    // Eğer modal otomatik kapanırsa ve kullanıcı mesajı göremezse,
        //    // hidMesaj'ı sadece modal açıkken göstermek daha iyi bir yaklaşımdır.
        //    // Ancak şu anki yapıda modal kapandıktan sonra Noty bildirimi görünür.
        //}

        //protected void btnKayitOl_Click(object sender, EventArgs e)
        //{
        //    // Bu metot, Kayıt Ol modalı içindeki butonun Click olayıdır.
        //    string adSoyad = txtKayitAdSoyad.Text.Trim();
        //    string email = txtKayitEmail.Text.Trim();
        //    string sifre = txtKayitSifre.Text.Trim();
        //    string sifreTekrar = txtKayitSifreTekrar.Text.Trim();

        //    if (string.IsNullOrEmpty(adSoyad) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(sifreTekrar))
        //    {
        //        hidMesaj.Value = "0|Lütfen tüm alanları doldurun.";
        //        Session["OpenModal"] = "register"; // Modalın açık kalması için session'a bilgi kaydet
        //        return;
        //    }

        //    if (sifre != sifreTekrar)
        //    {
        //        hidMesaj.Value = "0|Şifreler uyuşmuyor.";
        //        Session["OpenModal"] = "register"; // Modalın açık kalması için session'a bilgi kaydet
        //        return;
        //    }

        // Kayıt işlemlerini burada yapabilirsiniz (veritabanına kullanıcı ekleme vb.)
        // Örn: DataClass'tan bir metot çağırabilirsiniz.
        // string result = UyelikTamamla(adSoyad, null, email, null, email, sifre); // Örnek çağrı

        // Başarılı olursa
        //hidMesaj.Value = "1|Kaydınız başarıyla oluşturuldu! Şimdi giriş yapabilirsiniz.";
        // Başarısız olursa
        // hidMesaj.Value = "0|Kayıt sırasında bir hata oluştu. Lütfen tekrar deneyin.";

        // İşlem başarılı olduğunda modalın otomatik kapanmasını istiyorsanız bu satırı kaldırmayın.
        // Eğer modal otomatik kapanırsa ve kullanıcı mesajı göremezse,
        // hidMesaj'ı sadece modal açıkken göstermek daha iyi bir yaklaşımdır.
        // Ancak şu anki yapıda modal kapandıktan sonra Noty bildirimi görünür.
    }

        // UyelikTamamla metodu örneğiniz, eğer kullanılacaksa App_Code içindeki DataClass'ta olmalı
        // public static string UyelikTamamla(string AdSoyad, string Tel, string Mail, string EgitimTipi, string KullaniciAdi, string Sifre)
        // {
        //     string IP = MainClass.GetIPAddress();
        //     string jsonData = null;
        //     DataClass dc = new DataClass();
        //     jsonData = MainClass.ConvertJsonData(dc.YeniKayit(AdSoyad, Tel, Mail, EgitimTipi, KullaniciAdi, Sifre, IP, null));
        //     return jsonData;
        // }
    }