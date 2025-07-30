using RandevuSistemi.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandevuSistemi.Sekreter
{
    public partial class RandevuListesiSekreter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           


        }
       
        [System.Web.Services.WebMethod]
        public static string RandevuListesi()
        {
            string IP = MainClass.GetIPAddress();
            string jsonData = null;
            SessionDataClass oturum = (SessionDataClass)HttpContext.Current.Session["oturum"];
            if (oturum != null)
            {
                DataClass dc = new DataClass();
                List<RandevuListesiClass> data = dc.RandevuListesi(IP, oturum.UserId);
                jsonData = MainClass.ConvertJsonData(data);
            }
            else
            {
                jsonData = null;
            }

            return jsonData;
        }
    }
}