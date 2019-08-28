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
        $('#VisaDate').attr({ 'max': '30', 'min': '1','data_val_length': 'Sorry only 25 characters allowed for ProductName'});
       
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
