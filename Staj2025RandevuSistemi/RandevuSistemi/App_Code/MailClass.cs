using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using RandevuSistemi.App_Code;

namespace RandevuSistemi.App_Code
{
    public class MailClass:MainClass
    {
        public void DogrulamaMailiGonder(string MailAdresi,string Baslik,string Icerik,string IP, Guid? UserId)
        {
            string Metod = "DogrulamaMailiGonder";
            try
            {
                // SMTP Sunucu Bilgileri
                string smtpServer = "ramazan.erken0421@gop.edu.tr";
                int smtpPort = 587; // SSL için 465
                string smtpUser = MailUserName;
                string smtpPass = MailSifre;

                // Gönderici ve Alıcı Bilgileri
                string fromEmail = smtpUser;
                string toEmail = MailAdresi; // Alıcının e-posta adresi
                string subject = Baslik;
                string body = Icerik;

                // SMTP İstemcisi Oluştur
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = false, // SSL Etkin
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                // Mail Mesajı Oluştur
                MailMessage mailMessage = new MailMessage
                {
                    //Priority=MailPriority.High,
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // HTML formatında mail göndermek için true yap
                };

                mailMessage.To.Add(toEmail); // Alıcı ekle

                // Mail Gönder
                smtpClient.Send(mailMessage);
                Console.WriteLine("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                LoggerClass.ErrorLog(UserId, Metod, IP, ex.ToString());
            }
        }

         
    }
}