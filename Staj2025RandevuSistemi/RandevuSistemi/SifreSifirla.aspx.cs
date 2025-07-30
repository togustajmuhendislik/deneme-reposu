using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandevuSistemi
{
    public partial class SifreSifirla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnKayit_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            string email = txtEmail.Text;
            string sifre = txtSifre.Text;

            // Basit bir örnek kayıt işlemi (veritabanı bağlantısı yok)
            if (!string.IsNullOrEmpty(ad) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(sifre))
            {
                lblSonuc.Text = "Kayıt başarılı! Hoş geldiniz, " + ad + ".";
            }
            else
            {
                lblSonuc.Text = "Lütfen tüm alanları doldurunuz!";
                lblSonuc.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}