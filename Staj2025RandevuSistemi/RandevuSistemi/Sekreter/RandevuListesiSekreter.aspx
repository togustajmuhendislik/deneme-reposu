<%@ Page Title="" Language="C#" MasterPageFile="~/Sekreter/MainSekreter.Master" AutoEventWireup="true" CodeBehind="RandevuListesiSekreter.aspx.cs" Inherits="RandevuSistemi.Sekreter.RandevuListesiSekreter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables çekirdek JS dosyası -->
    <script type="text/javascript" src="/Template/global_assets/js/plugins/tables/DataTableV2/1.12.1.jquery.dataTables.min.js"></script>
    <!-- DataTables DateTime eklentisi (moment.js ile çalışır) -->
    <script type="text/javascript" src="/Template/global_assets/js/plugins/tables/DataTableV2/1.10.19.datetime-moment.js"></script>

    <!-- Master Page'de yüklendiği varsayılan diğer scriptler ve app.js -->

    <script type="text/javascript">
        // Geçici ShowLoading ve Bildirim fonksiyonları (kendi app.js'nizdeki gerçek implementasyonlarla değiştirmeniz önerilir)


        $(document).ready(function () {
            // DataTable ve Moment.js'nin yüklendiğinden emin olun


            RandevuListesi();
        });

        function RandevuListesi() {
            var request = $.ajax({
                type: 'POST',
                url: 'RandevuListesiSekreter.aspx/RandevuListesi', // WebMethod URL'si
                contentType: 'application/json; charset=utf-8',
                cache: false,
                dataType: "json"
            });

            request.done(function (h) {
                var data = h.d;
                var Liste = JSON.parse(data);
                ShowLoading(false);

                $('#tblHatirlatici').DataTable({
                    destroy: true,
                    autoWidth: false,
                    order: [2, 'desc'],
                    dom: '<"datatable-header"fl><"datatable-scroll"t><"datatable-footer"ip>',
                    language: {
                        search: '<span>Tabloda Ara:</span> _INPUT_',
                        searchPlaceholder: 'Aranacak Veri',
                        lengthMenu: '<span>Göster:</span> _MENU_',
                        paginate: { 'first': 'İlk', 'last': 'Son', 'next': $('html').attr('dir') == 'rtl' ? '&larr;' : '&rarr;', 'previous': $('html').attr('dir') == 'rtl' ? '&rarr;' : '&larr;' }
                    },
                    data: Liste,
                    columns: [
                        { data: "DanisanAdiSoyadi", title: "Ad Soyad" },
                        { data: "UzmanAdiSoyadi", title: "Uzman " },
                        {
                            data: "RandevuTarihSaati", title: "Tarih", type: 'datetime', render: DataTable.render.datetime('DD/MM/YYYY')
                        },
                        { data: "RandevuOrtami", title: "Randevu Ortamı " },
                        { data: "RandevuDuzenle", title: "" },
                        { data: "RandevuSil", title: "   " }
                    ]
                });
            });

            request.fail(function (jqXHR, textStatus) {
                var hata1 = jqXHR;
                var hata2 = textStatus;
                ShowLoading(false);
                Bildirim("0", hata1 + " --- " + hata2);
                console.log(jqXHR);
            });
        }

        function RandevuSil(RandevuId) {

            // ilgili randevu silmek için ajax calıştır
            // sonuc nul ilse başarılı mesajı ver
            RandevuListesi();
            $('#RandevuSilModal').modal({ backdrop: 'static', keyboard: false }, 'show');

        }

        function RandevuDuzenle(RandevuId) {
            // ilgili randevu bılgılerı ajax cekılecek
            // popup dolduralacak
            // sonra show edilecek
            // hata varsa sonuc null ise popup açma
        }

    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">
        <div class="card-header header-elements-inline">
            <h5 class="card-title">Günlük Hatırlatıcı</h5>
        </div>
        <div class="card-body">
            <div class="content">
            </div>
        </div>
        <div class="content">
            <div class="form-group">
            </div>
            <div class="form-group row">
                <div class="col-lg-12">
                    <table class="table" id="tblHatirlatici">
                    </table>
                </div>
            </div>
        </div>
    </div>
    


     <div id="RandevuSilModal" class="modal fade" tabindex="-1">
     <div class="modal-dialog modal-lg">
         <div class="modal-content">
             <div class="modal-header bg-danger-700">
                 <h5 class="modal-title text-white"><b>Parametre Ekle / Güncelle</b></h5>
                 <button type="button" class="close" data-dismiss="modal">&times;</button>
             </div>
             <div class="modal-body ">
                 <div class="form-group row">
                     <div class="col-lg-2">
                         <b runat="server" id="lblYargiParamTuru"></b>
                     </div>

                 </div>

                 <div class="form-group row">
                     <div class="col-lg-2">
                         <b>Parametre Adı</b>
                     </div>
                     <div class="col-lg-10">
                         <select id="drpYargiBirim" runat="server" class="form-control select-search" onchange="SelChangeYargiBirim()" multiple="false">
                         </select>
                     </div>
                 </div>
                 <div class="form-group row">
                     <div class="col-lg-2">
                         <b>Parametre Adı</b>
                     </div>
                     <div class="col-lg-10">
                         <input id="txtParametreAdi" runat="server" type="text" class="form-control " value="">
                     </div>
                 </div>


             </div>
             <div class="modal-footer">
                 <button type="button" class="btn bg-teal-400" onclick="YargiParametreEkleGuncelle()"><i class="icon-database-add">&nbsp;</i>Kaydet / Güncelle</button>
                 <button type="button" class="btn bg-danger-400" data-dismiss="modal" onclick="YargiParametreEkleSil()">Sil</button>
                 <button type="button" class="btn bg-grey-400" data-dismiss="modal">İşlemi İptal Et</button>
             </div>
         </div>
     </div>
 </div>

</asp:Content>
