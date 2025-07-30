<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RandevuSistemi.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Randevu Sistemi Giriş</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />

    <link href="https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900" rel="stylesheet" />
    <link href="Template/global_assets/css/icons/icomoon/styles.min.css" rel="stylesheet" />
    <link href="Template/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Template/assets/css/bootstrap_limitless.min.css" rel="stylesheet" />
    <link href="Template/assets/css/layout.min.css" rel="stylesheet" />
    <link href="Template/assets/css/components.min.css" rel="stylesheet" />
    <link href="Template/assets/css/colors.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.css" rel="stylesheet" />
    <link href="Template/assets/css/noty.css" rel="stylesheet" />

    <script src="Template/global_assets/js/main/jquery.min.js"></script>
    <script src="Template/global_assets/js/main/bootstrap.bundle.min.js"></script>
    <script src="Template/global_assets/js/plugins/loaders/blockui.min.js"></script>
    <script src="Template/global_assets/js/plugins/notifications/noty.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/locale/tr.js"></script>
    <script src="Template/assets/js/app.js"></script>

    <style>
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
            font-family: 'Roboto', sans-serif;
        }

        .page-content {
            display: flex;
            min-height: 100vh;
        }

        .content-wrapper {
            flex-grow: 1;
            overflow-y: auto;
            padding: 20px;
            position: relative;
            z-index: 0;
            color: #fff;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            text-align: center;
        }

        .content-wrapper::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-image: url('Template/global_assets/images/togü_kampüs.jpg');
            background-size: cover;
            background-position: center;
            filter: brightness(0.4) blur(3px);
            z-index: -1;
        }

        .right-panel {
            width: 450px;
            background-color: #ffffff;
            border-left: 1px solid #e0e0e0;
            padding: 40px;
            box-sizing: border-box;
            overflow-y: auto;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            box-shadow: -5px 0 15px rgba(0,0,0,0.05);
        }

        .right-panel .panel-logo {
            max-width: 200px;
            margin-bottom: 30px;
        }

        .right-panel h4 {
            color: #333;
            margin-bottom: 25px;
            font-weight: 500;
        }

        .right-panel .form-group {
            margin-bottom: 20px;
            width: 100%;
        }

        .right-panel .btn-primary {
            width: 100%;
            padding: 12px;
        }

        @media (max-width: 991.98px) {
            .page-content {
                flex-direction: column;
            }

            .right-panel {
                width: 100%;
                border-left: none;
                border-top: 1px solid #e0e0e0;
                padding: 30px;
            }
        }
    </style>

    <script>
        function Bildirim(bildirimtipi, Text) {
            Noty.overrideDefaults({
                theme: 'limitless',
                layout: 'topRight',
                timeout: 2500,
                progressBar: true,
                closeWith: ['click', 'button']
            });

            let headerText = '';
            switch (bildirimtipi) {
                case '0': headerText = 'Hata!'; break;
                case '1': headerText = 'Başarılı!'; break;
                case '2': headerText = 'Uyarı!'; break;
                default: headerText = 'Mesaj'; break;
            }

            new Noty({
                type: (bildirimtipi === '0' ? 'error' : bildirimtipi === '1' ? 'success' : 'info'),
                text: `<b>${headerText}</b><br>${Text}`
            }).show();
        }

        $(document).ready(function () {
            var hid = document.getElementById('<%= hidMesaj.ClientID %>');
            if (hid && hid.value !== "") {
                let parts = hid.value.split('|');
                if (parts.length === 2) {
                    Bildirim(parts[0], parts[1]);
                } else {
                    Bildirim("2", hid.value);
                }
                hid.value = "";
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-content">
            <div class="content-wrapper">
                <h2 style="color:white;">Randevu Sistemi Giriş Ekranı</h2>
                <p style="color:white;">
                    Sisteme giriş yapmak için sağdaki paneli kullanınız.
                </p>
            </div>

            <div class="right-panel">
                <img src="Template/global_assets/images/pdr_logo.png" class="panel-logo" />

                <h4>Hesabınıza Giriş Yapın</h4>

                <div class="form-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="E-posta Adresiniz" TextMode="Email" />
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Şifreniz" TextMode="Password" />
                </div>

                <div class="form-group mb-3">
                    <asp:Button ID="btnGiris" runat="server" CssClass="btn btn-primary" Text="Giriş Yap" OnClick="btnGiris_Click" />
                </div>

                <div class="text-center">
                    <a href="SifremiUnuttum.aspx">Şifremi Unuttum?</a>
                    <span class="mx-2">|</span>
                    <a href="KayitOl.aspx">Kayıt Ol</a>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hidMesaj" runat="server" />
    </form>
</body>
</html>
