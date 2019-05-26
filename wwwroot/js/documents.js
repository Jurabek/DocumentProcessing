$(function(){
    $('#applicantType input[type="radio"]').click(function(){
        if ($(this).is(':checked'))
        {
            var existingApplicantGroup = $("#existingApplicantGroup");
            var newApplicantGroup = $("#newApplicantGroup")
            var value = $(this).val();
            
            if(value === "Existing") {
                newApplicantGroup.hide();
                existingApplicantGroup.show()
            }
            else if(value === "New") {
                newApplicantGroup.show();
                existingApplicantGroup.hide();
            }
        }
    });
});