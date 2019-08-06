// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#dateFor').datepicker({
    language: 'tg'
});

$('#dateFrom').datepicker({
    language: 'tg'
});

$('select[name=PurposeId]').change(function () {  // ищеть в документе 
    var $purpose = $("#PurposeId option:selected").text();
 
    if ($purpose == "Тасдики даъват") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    } else if ($purpose == "Бакайдгирӣ") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    } else if ($purpose == "Хуручи") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    } else if ($purpose == "Таъйид") {
        $('#Appointment_Character option[value=2]').attr('selected', 'selected');
    } else if ($purpose == "Апостилгузори") {
        $('#Appointment_Character option[value=2]').attr('selected', 'selected');
    } else if ($purpose == "eVisa") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    } else if ($purpose == "Тамдиди раводид") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    } else if ($purpose == "Роудспот") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    } else if ($purpose == "Иҷозати ГБАО") {
        $('#Appointment_Character option[value=3]').attr('selected', 'selected');
    } else if ($purpose == " Сафари хидматӣ ба ҶХХ") {
        $('#Appointment_Character option[value=3]').attr('selected', 'selected');
    }
    
     
});





// paging with Jquery
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};
const now = new Date();
const date = now.format("dd mm yyyy");
$(document).ready(function () {
    $('#table').DataTable({
        autoFill: true,
        "pagingType": "full_numbers",
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "ҳамма"]],
        "language": {
            "paginate": {
                "first": '<i class="fa fa-arrow-circle-left" aria-hidden="true"></i>',
                "last": '<i class="fa fa-arrow-circle-right" aria-hidden="true"></i>',
                "previous": '<i class="fa fa-arrow-left" aria-hidden="true"></i>',
                "next": '<i class="fa fa-arrow-right" aria-hidden="true"></i>',
            },
            "search": "Ҷустуҷӯ:",
            "info": 'Намоиши _START_ то _END_ аз _TOTAL_ ҳуҷҷатҳо',
            "lengthMenu": "Намоиши _MENU_ ҳуҷҷатҳо",
            

        }
    });
});

var table = $("table").tableExport({
    headings: true,                    // (Boolean), display table headings (th/td elements) in the <thead>
    footers: true,                     // (Boolean), display table footers (th/td elements) in the <tfoot>
    formats: ["xlsx"],    // (String[]), filetypes for the export
    fileName: "Экспорт " + date,                    // (id, String), filename for the downloaded file
    bootstrap: false,                   // (Boolean), style buttons using bootstrap
    position: "bottom",                 // (top, bottom), position of the caption element relative to table
    ignoreRows: true,                  // (Number, Number[]), row indices to exclude from the exported file(s)
    ignoreCols: null,                  // (Number, Number[]), column indices to exclude from the exported file(s)
    ignoreCSS: ".todelete",  // (selector, selector[]), selector(s) to exclude from the exported file(s)
    trimWhitespace: true,             // (Boolean), remove all leading/trailing newlines, spaces, and tabs from cell text in the exported file(s)
    buttons: false
});

$('#export').click(function () {
    table.export("xls");
});

