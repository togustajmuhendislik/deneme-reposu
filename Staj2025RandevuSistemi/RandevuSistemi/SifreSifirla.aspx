<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SifreSifirla.aspx.cs" Inherits="RandevuSistemi.SifreSifirla" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kayıt Ol</title>
    <meta charset="utf-8" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Roboto', sans-serif;
            background: linear-gradient(135deg, #74ebd5, #acb6e5);
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .register-container {
            background-color: #fff;
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
            width: 400px;
        }

        .register-container h2 {
            text-align: center;
            margin-bottom: 30px;
            color: #333;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            color: #333;
        }

        .form-group input {
            width: 100%;
            padding: 10px;
            border-radius: 8px;
            border: 1px solid #ccc;
            font-size: 16px;
        }

        .form-group input:focus {
            outline: none;
            border-color: #74ebd5;
        }

        .submit-btn {
            background-color: #4fc3f7;
            color: white;
            padding: 12px;
            border: none;
            width: 100%;
            border-radius: 8px;
            font-size: 16px;
            cursor: pointer;
        }

        .submit-btn:hover {
            background-color: #0288d1;
        }

        .result {
            margin-top: 15px;
            color: green;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-container">
            <h2>Kayıt Ol</h2>
            <div class="form-group">
                <label for="txtAd">Ad Soyad:</label>
                <asp:TextBox ID="txtAd" runat="server" CssClass="form-control" placeholder="Adınızı giriniz" />
            </div>
            <div class="form-group">
                <label for="txtEmail">E-posta:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="E-posta adresiniz" />
            </div>
            <div class="form-group">
                <label for="txtSifre">Şifre:</label>
                <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifrenizi giriniz" />
            </div>
            <asp:Button ID="btnKayit" runat="server" Text="Kayıt Ol" CssClass="submit-btn" OnClick="btnKayit_Click" />
            <asp:Label ID="lblSonuc" runat="server" CssClass="result"></asp:Label>
        </div>
    </form>
</body>
</html>