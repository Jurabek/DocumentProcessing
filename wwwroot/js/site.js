// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#dateFor').datepicker({
    language: 'tg'
});

$('#dateFrom').datepicker({
    language: 'tg's
});

$('select[name=PurposeId]').change(function () {  
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


