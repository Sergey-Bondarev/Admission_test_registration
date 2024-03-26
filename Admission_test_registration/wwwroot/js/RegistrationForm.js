
function populateExamScheduleElementOptions(universityId, examScheduleElementId, shedule) {
    var examScheduleElements = shedule;

    $('#' + examScheduleElementId).empty();

    var filteredExamScheduleElements = examScheduleElements.filter(function (elem) {
        return elem.universityId == universityId;
    });

    $.each(filteredExamScheduleElements, function (index, examScheduleElement) {
        $('#' + examScheduleElementId).append($('<option>', {
            value: examScheduleElement.examScheduleElementId,
            text: examScheduleElement.examTime.substring(0, examScheduleElement.examTime.lastIndexOf(":"))
        }));
    });
}

function setupSelect(universityId, examScheduleElementId, button, shedule) {
    $('#' + examScheduleElementId).prop('disabled', true);
    $('#' + universityId).change(function () {
        var selectedUniversityId = $(this).val();
        if (selectedUniversityId) {
            $('#' + examScheduleElementId).prop('disabled', false);
            $('#' + button).prop('disabled', false);
            populateExamScheduleElementOptions(selectedUniversityId, examScheduleElementId, shedule);
        } else {
            $('#' + examScheduleElementId).prop('disabled', true).empty();
            $('#' + button).prop('disabled', true);
        }
    });
    $('#' + examScheduleElementId).change(function () {
        var selectedValue = $(this).val();
        if (!selectedValue) {
            $('#' + examScheduleElementId).prop('disabled', true);
        }
    });
}
