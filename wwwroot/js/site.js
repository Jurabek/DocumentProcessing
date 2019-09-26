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
 
    if ($purpose == "Тасдиқи даъват") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("required", "true");
        $('#VisaTypeId').attr("required", "true");
        $('#VisaDateTypeId').attr("required", "true");
        $('#VisaDate').attr("required", "true");
        $("#addScnt").css("display", "block");
        $('#VisaId').removeAttr("disabled");
        $('#VisaTypeId').removeAttr("disabled");
        $('#VisaDateTypeId').removeAttr("disabled");
        $('#VisaDate').removeAttr("disabled");
    } else if ($purpose == "Бақайдгирӣ") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else if ($purpose == "Хуруҷӣ") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else if ($purpose == "Таъйид") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else if ($purpose == "Апостилгузорӣ") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else if ($purpose == "eVisa") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("required", "true");
        $('#VisaTypeId').attr("required", "true");
        $('#VisaDateTypeId').attr("required", "true");
        $('#VisaDate').attr("required", "true");
        $("#addScnt").css("display", "block");
        $('#VisaId').removeAttr("disabled");
        $('#VisaTypeId').removeAttr("disabled");
        $('#VisaDateTypeId').removeAttr("disabled");
        $('#VisaDate').removeAttr("disabled");
    } else if ($purpose == "Тамдиди раводид") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("required", "true");
        $('#VisaTypeId').attr("required", "true");
        $('#VisaDateTypeId').attr("required", "true");
        $('#VisaDate').attr("required", "true");
        $("#addScnt").css("display", "block");
        $('#VisaId').removeAttr("disabled");
        $('#VisaTypeId').removeAttr("disabled");
        $('#VisaDateTypeId').removeAttr("disabled");
        $('#VisaDate').removeAttr("disabled");
    } else if ($purpose == "Роудспот") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else if ($purpose == "Иҷозати ГБАО") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else if ($purpose == " Сафари хизматӣ ба ҶХХ") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else if ($purpose == "Шиноснома") {
        $('#Appointment_Character option[value=1]').attr('selected', 'selected');
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    } else {
        
        $('#VisaId').attr("disabled", "true");
        $('#VisaTypeId').attr("disabled", "true");
        $('#VisaDateTypeId').attr("disabled", "true");
        $('#VisaDate').attr("disabled", "true");
        $("#addScnt").css("display", "none");
    }


});

var $purpose = $("#PurposeId option:selected").text();

if ($purpose == "Тасдиқи даъват") {
    $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    $('#VisaId').attr("required", "true");
    $('#VisaTypeId').attr("required", "true");
    $('#VisaDateTypeId').attr("required", "true");
    $('#VisaDate').attr("required", "true");
    $("#addScnt").css("display", "block");
    $('#VisaId').removeAttr("disabled");
    $('#VisaTypeId').removeAttr("disabled");
    $('#VisaDateTypeId').removeAttr("disabled");
    $('#VisaDate').removeAttr("disabled");
} else if ($purpose == "eVisa") {
    $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    $('#VisaId').attr("required", "true");
    $('#VisaTypeId').attr("required", "true");
    $('#VisaDateTypeId').attr("required", "true");
    $('#VisaDate').attr("required", "true");
    $("#addScnt").css("display", "block");
    $('#VisaId').removeAttr("disabled");
    $('#VisaTypeId').removeAttr("disabled");
    $('#VisaDateTypeId').removeAttr("disabled");
    $('#VisaDate').removeAttr("disabled");
} else if ($purpose == "Тамдиди раводид") {
    $('#Appointment_Character option[value=1]').attr('selected', 'selected');
    $('#VisaId').attr("required", "true");
    $('#VisaTypeId').attr("required", "true");
    $('#VisaDateTypeId').attr("required", "true");
    $('#VisaDate').attr("required", "true");
    $("#addScnt").css("display", "block");
    $('#VisaId').removeAttr("disabled");
    $('#VisaTypeId').removeAttr("disabled");
    $('#VisaDateTypeId').removeAttr("disabled");
    $('#VisaDate').removeAttr("disabled");
}  else {

    $('#VisaId').attr("disabled", "true");
    $('#VisaTypeId').attr("disabled", "true");
    $('#VisaDateTypeId').attr("disabled", "true");
    $('#VisaDate').attr("disabled", "true");
    $("#addScnt").css("display", "none");
}

$('#saveButton').click(function () {
    if (document.getElementById("file").files.length == 0) {
        console.log("no files selected");
        $(".displayNone").css("display", "block");
    } else {
        $(".displayNone").css("display", "none");
    }
});

$('#file').change(function () {
    if (document.getElementById("file").files.length == 0) {
        console.log("no files selected");
        $(".displayNone").css("display", "block");
    } else {
        $(".displayNone").css("display", "none");
    }
});





$textChange = 'Ҳадди ниҳоии рақами иҷозатшуда барои Рӯз то 30 ва барои Моҳ то 32';

$('select[name=VisaDateTypeId]').change(function () {
    var $VisaDateType = $("#VisaDateTypeId option:selected").text();

    if ($VisaDateType == "Рӯз") {
        $('#VisaDate').attr({ 'max': '30', 'min': '1', 'data_val_length': 'Sorry only 25 characters allowed for ProductName' });

        $textChange = 'Ҳадди ниҳоии рақами иҷозатшуда барои Рӯз аз 1 то 30';
    } else if ($VisaDateType == "Моҳ") {

        $('#VisaDate').attr({ 'max': '32', 'min': '1', 'data_val_length': 'Sorry only 25 characters allowed for ProductName' });
        $textChange = 'Ҳадди ниҳоии рақами иҷозатшуда барои Моҳ аз 1 то 32';
    }
});

$('#VisaDate').change(function () {
    setTimeout(function () {

        $('#textChange').text($textChange);
    }, 300);

});

// expot to excell
function exportToExcell() {

    // внешний скрипт для форматирование даты
    Date.prototype.format = function (mask, utc) {
        return dateFormat(this, mask, utc);
    };
    const now = new Date();
    const date = now.format("dd mm yyyy");
    const content = document.getElementById('table');
    const file = new FileReader();
    const mimiType = 'application/x-msexcell;charset=utf-8';
    const blob = new Blob([unescape(content.outerHTML)], { type: mimiType });
    file.readAsDataURL(blob);
    file.onload = (e) => {
        // window.open(file.result.toString(), '_blank'); // Cкачивание без название файла

        var a = document.createElement("a"); // создание тега а
        a.href = file.result; //  Файл в формате base64 присвоение к href
        a.download = "Экспорт " + date + ".xls"; // Название файла
        document.body.appendChild(a); // добавление в тело документа тега а
        a.click(); // имитация клика
        setTimeout(function () {
            document.body.removeChild(a); // удаление тега а из тело документа
        }, 0);
    };


    
}
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};
 const now = new Date();
 $date = now.format("dd mm yyyy");
/* Defaults */

 $("table").tableExport({
    headings: true,                    // (Boolean), display table headings (th/td elements) in the <thead>
    footers: true,                     // (Boolean), display table footers (th/td elements) in the <tfoot>
    formats: ["xlsx"],    // (String[]), filetypes for the export
    fileName: 'Экспорт 123',                    // (id, String), filename for the downloaded file
    bootstrap: true,                   // (Boolean), style buttons using bootstrap
    position: "bottom",                 // (top, bottom), position of the caption element relative to table
    ignoreRows: null,                  // (Number, Number[]), row indices to exclude from the exported file(s)
    ignoreCols: [5, 6, 8, 9],                  // (Number, Number[]), column indices to exclude from the exported file(s)
    ignoreCSS: "td.todelete",  // (selector, selector[]), selector(s) to exclude from the exported file(s)
    emptyCSS: ".tableexport-empty",    // (selector, selector[]), selector(s) to replace cells with an empty string in the exported file(s)
    trimWhitespace: true              // (Boolean), remove all leading/trailing newlines, spaces, and tabs from cell text in the exported file(s)
});


$(".xlsx").html('<i class="fas fa-file-excel"></i>');