$(function () {
    $('#myDropDown').change(function () {
        var value = $(this).val();
        var fistVal = $('#myDropdown option:first-child').attr("selected", "selected");
        if (value == fistVal) {
            $('#txt').show();
        } else {
            $('#txt').hide();
        }
    });
});

