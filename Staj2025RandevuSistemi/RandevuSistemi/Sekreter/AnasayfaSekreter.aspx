<%@ Page Title="" Language="C#" MasterPageFile="~/Sekreter/MainSekreter.Master" AutoEventWireup="true" CodeBehind="AnasayfaSekreter.aspx.cs" Inherits="RandevuSistemi.Sekreter.AnasayfaSekreter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.css" rel="stylesheet" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/locale/tr.js"></script>
    <style>
        body {
            font-family: Arial, sans-serif;
        }

        #calendar {
            max-width: 5800px;
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
                    defaultView: 'agendaWeek',
                    defaultDate: Liste.defaultDate,
                    locale: 'tr',
                    editable: false,
                    selectable: true,
                    minTime: "09:00:00",
                    maxTime: "16:00:00",
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
    

    <div id="calendar"></div>
</asp:Content>