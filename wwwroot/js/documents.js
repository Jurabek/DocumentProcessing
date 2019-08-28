$(function () {

    $('#applicantType input[type="radio"]').click(function () {
        if ($(this).is(':checked')) {
            var existingApplicantGroup = $("#existingApplicantGroup");
            var newApplicantGroup = $("#newApplicantGroup")
            var value = $(this).val();

            if (value === "Existing") {
                newApplicantGroup.hide();
                existingApplicantGroup.show()
            } else if (value === "New") {
                newApplicantGroup.show();
                existingApplicantGroup.hide();
            }
        }
    });

    $('#purposeType input[type="radio"]').click(function () {
        if ($(this).is(':checked')) {
            var existingPurposeGroup = $("#existingPurposeGroup");
            var newPurposeGroup = $("#newPurposeGroup")
            var value = $(this).val();

            if (value === "Existing") {
                newPurposeGroup.hide();
                existingPurposeGroup.show()
            } else if (value === "New") {
                newPurposeGroup.show();
                existingPurposeGroup.hide();
            }
        }
    });


    $("#saveButton").click(function () {
        startUpdatingProgressIndicator();
    });

    var selectedPurpose = $('#PurposeId').find(":selected").val();
    var characterOfPurpose = $('#hidden_' + selectedPurpose).val();
    $('#purposeCharacter').val(characterOfPurpose);
    
    $('#PurposeId').on('change', function () {
       var characterOfPurpose = $('#hidden_' + this.value).val();
       $('#purposeCharacter').val(characterOfPurpose);
    });

    var intervalId;

    function startUpdatingProgressIndicator() {
        $("#progress").show();

        intervalId = setInterval(
            function () {
                // We use the POST requests here to avoid caching problems (we could use the GET requests and disable the cache instead)
                $.post(
                    "/Documents/progress",
                    function (progress) {
                        var bar = $("#bar");
                        bar.css({width: progress + "%"});
                        bar.html(progress + "%")
                        bar.attr("aria-valuenow", progress)
                    }
                );
            },
            10
        );
    }
});

function isDeleted(id) {
    var fileElementCheckBox = document.getElementById(id);
    var fileElementLink = document.getElementById("link_" + id);
    
    
    if (fileElementCheckBox.checked) {
        fileElementLink.style.textDecoration = "line-through"
    } 
    else {
        fileElementLink.style.textDecoration = "initial"
    }
}

$(function () {
    var scntDiv = $('#VisaId.ID');
    var i = $('#VisaId.ID p').length + 1;

    $('#addScnt').click(function () {
        $('<p class="col-md-12 row" style="margin-left: 0;"><input style="margin-top: 5px" class="form-control col-md-11" type="text" data-val="true" data-val-required="Id-и Раводид холи аст!" id="VisaId.ID" name="VisaId.ID" value=""> <a href="#" class="remScnt col-md-1" id="remScnt" style="font-size: 2em;"><i class="fa fa-minus-circle" style="color: red;" aria-hidden="true"></i></a></p>').appendTo(scntDiv);
        i++;
        return false;
    });


    $('#VisaId.ID').on('click', '.remScnt', function () {
        if (i > 2) {
            $(this).parents('p').remove();
            i--;
        }
        return false;
    });

});
