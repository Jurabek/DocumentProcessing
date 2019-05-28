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

    $("#saveButton").click(function () {
        startUpdatingProgressIndicator();
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
