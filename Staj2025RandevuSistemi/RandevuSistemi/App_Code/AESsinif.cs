using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;



class AESSinif
{
    private const string AES_IV = @"!&+QWSDF!1231266";
    private string aes_anahtar = @"QQsaw!257()!%erq";


    public string AESsifrele(string metin)
    {
        AesCryptoServiceProvider aes_saglayici = new AesCryptoServiceProvider();
        /*
        Şifreleme yöntemi olarak AES şifreleme yöntemini seçiyoruz.
         */

        aes_saglayici.BlockSize = 128;
        /*
        AES bloklar halinde şifreleme yapar. 
        Biz de bloklama yöntemini belirliyoruz.
         */

        aes_saglayici.KeySize = 128;
        /*
        AES şifreleme metodunda anahtar ile şifreleme yapılıyor.
        Anahtar boyutları 128, 192 ve 256 olabilir.
         */

        aes_saglayici.IV = Encoding.UTF8.GetBytes(AES_IV);
        //IV = Initial Vector
        aes_saglayici.Key = Encoding.UTF8.GetBytes(aes_anahtar);
        aes_saglayici.Mode = CipherMode.CBC;
        aes_saglayici.Padding = PaddingMode.PKCS7;

        byte[] kaynak = Encoding.Unicode.GetBytes(metin);
        /*
         Metni byte dizisine çeviriyoruz.
         */
        using (ICryptoTransform sifrele = aes_saglayici.CreateEncryptor())
        {
            byte[] hedef = sifrele.TransformFinalBlock(kaynak, 0, kaynak.Length);
            return Convert.ToBase64String(hedef);
        }
    }


    public string AESsifreCoz(string sifreliMetin)
    {
        AesCryptoServiceProvider aes_saglayici = new AesCryptoServiceProvider();
        aes_saglayici.BlockSize = 128;
        aes_saglayici.KeySize = 128;
        aes_saglayici.IV = Encoding.UTF8.GetBytes(AES_IV);
        aes_saglayici.Key = Encoding.UTF8.GetBytes(aes_anahtar);
        aes_saglayici.Mode = CipherMode.CBC;
        aes_saglayici.Padding = PaddingMode.PKCS7;

        byte[] kaynak = System.Convert.FromBase64String(sifreliMetin);

        using (ICryptoTransform decrypt = aes_saglayici.CreateDecryptor())
        {
            byte[] hedef = decrypt.TransformFinalBlock(kaynak, 0, kaynak.Length);
            return Encoding.Unicode.GetString(hedef);
        }
    }

    public byte[] sifreleme2(byte[] data, string StrKarma)
    {
        Stream stream = new MemoryStream(data);
        Stream streamSifreli = new MemoryStream();
        TripleDESCryptoServiceProvider TDCS = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        // FileStream OkuStream = new FileStream(adres, FileMode.Open, FileAccess.Read);
        //FileStream YazmaStream = new FileStream(yeniadres, FileMode.OpenOrCreate, FileAccess.Write);
        byte[] Karma = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(StrKarma));
        //byte[] Metin_Oku = File.ReadAllBytes(adres);
        md5.Clear();
        TDCS.Key = Karma;
        TDCS.Mode = CipherMode.ECB;
        CryptoStream kriptoStream = new CryptoStream(streamSifreli, TDCS.CreateEncryptor(), CryptoStreamMode.Write);
        kriptoStream.Write(data, 0, data.Length);
        return ReadFully(streamSifreli);
    }
    private static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
    private void sifreleme(string adres, string yeniadres, string StrKarma)
    {
        TripleDESCryptoServiceProvider TDCS = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        FileStream OkuStream = new FileStream(adres, FileMode.Open, FileAccess.Read);
        FileStream YazmaStream = new FileStream(yeniadres, FileMode.OpenOrCreate, FileAccess.Write);
        byte[] Karma = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(StrKarma));
        byte[] Metin_Oku = File.ReadAllBytes(adres);
        md5.Clear();
        TDCS.Key = Karma;
        TDCS.Mode = CipherMode.ECB;
        CryptoStream kriptoStream = new CryptoStream(YazmaStream, TDCS.CreateEncryptor(), CryptoStreamMode.Write);
        int depo;
        long position = 0;
        while (position < OkuStream.Length)
        {
            depo = OkuStream.Read(Metin_Oku, 0, Metin_Oku.Length);
            position += depo;
            kriptoStream.Write(Metin_Oku, 0, depo);
        }
        OkuStream.Close();
        YazmaStream.Close();
    }

    private void sifreCozme(string adres, string yeniadres, string StrKarma)
    {
        TripleDESCryptoServiceProvider TDCS = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        FileStream OkuStream = new FileStream(adres, FileMode.Open, FileAccess.Read);
        FileStream YazmaStream = new FileStream(yeniadres, FileMode.OpenOrCreate, FileAccess.Write);
        byte[] Karma = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(StrKarma));
        byte[] Metin_Oku = File.ReadAllBytes(adres);
        md5.Clear();
        TDCS.Key = Karma;
        TDCS.Mode = CipherMode.ECB;
        CryptoStream kriptoStream = new CryptoStream(YazmaStream, TDCS.CreateDecryptor(), CryptoStreamMode.Write);
        int depo;
        long position = 0;
        while (position < OkuStream.Length)
        {
            depo = OkuStream.Read(Metin_Oku, 0, Metin_Oku.Length);
            position += depo;
            kriptoStream.Write(Metin_Oku, 0, depo);
        }
        OkuStream.Close();
        YazmaStream.Close();
    }

    private void sifreCozme2(string adres, string yeniadres, string StrKarma)
    {
        TripleDESCryptoServiceProvider TDCS = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        FileStream OkuStream = new FileStream(adres, FileMode.Open, FileAccess.Read);
        FileStream YazmaStream = new FileStream(yeniadres, FileMode.OpenOrCreate, FileAccess.Write);
        byte[] Karma = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(StrKarma));
        byte[] Metin_Oku = File.ReadAllBytes(adres);
        md5.Clear();
        TDCS.Key = Karma;
        TDCS.Mode = CipherMode.ECB;
        CryptoStream kriptoStream = new CryptoStream(YazmaStream, TDCS.CreateDecryptor(), CryptoStreamMode.Write);
        int depo;
        long position = 0;
        while (position < OkuStream.Length)
        {
            depo = OkuStream.Read(Metin_Oku, 0, Metin_Oku.Length);
            position += depo;
            kriptoStream.Write(Metin_Oku, 0, depo);
        }
        OkuStream.Close();
        YazmaStream.Close();
    }

}
