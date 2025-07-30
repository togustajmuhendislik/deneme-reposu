<%@ Page Title="" Language="C#" MasterPageFile="~/Sekreter/MainSekreter.Master" AutoEventWireup="true" CodeBehind="AnasayfaSekreter.aspx.cs" Inherits="RandevuSistemi.Sekreter.AnasayfaSekreter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
        }

        #calendar {
            max-width: 800px;
            margin: 40px auto;
        }
    </style>

    <script>
        window.onload = function () {
            BindTakvim();
        }

        function BindTakvim() {
            var request = $.ajax({
                type: 'POST',
                url: 'AnasayfaSekreter.aspx/BindServerCalendar', // Burayı kendi sayfa adınla güncelledik!
                contentType: 'application/json; charset=utf-8',
                cache: false,
                dataType: "json"
            });

            request.done(function (h) {
                data = h.d;
                Liste = JSON.parse(data);

                $('#calendar').fullCalendar('destroy');

                $('#calendar').fullCalendar({
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,agendaWeek,agendaDay'
                    },
                    defaultView: 'month',
                    defaultDate: Liste.defaultDate,
                    locale: 'tr',
                    editable: false,
                    selectable: true,
                    minTime: "09:00:00",
                    maxTime: "24:00:00",
                    firstDay: Liste.firstDay,
                    events: Liste.events,
                    eventOverlap: false,
                    dayClick: function (date) {
                        var sDate = new Date(date);
                        var bDate = new Date(sDate.getTime() + (60 * 60000));
                        alert(sDate.toLocaleDateString("tr-TR") + ' ' + sDate.toLocaleTimeString("tr-TR"));
                        alert(bDate.toLocaleDateString("tr-TR") + ' ' + bDate.toLocaleTimeString("tr-TR"));
                    }
                });
            });

            request.fail(function (jqXHR, textStatus) {
                hata1 = jqXHR;
                hata2 = textStatus;

                alert(hata1 + " --- " + hata2);
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="text-align: center;">FullCalendar Basit Örnek</h2>

    <div id="calendar"></div>
</asp:Content>