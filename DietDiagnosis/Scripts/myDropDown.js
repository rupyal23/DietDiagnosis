$(function () {
    $('#myDropDown').change(function () {
        var value = $(this).val();
        var firstVal = $('#myDropdown option:first-child').attr("selected", "selected");
        if (value == firstVal) {
            $('#txt').show();
        } else {
            $('#txt').hide();
        }
    });
});

