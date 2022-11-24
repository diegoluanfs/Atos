

$(document).ready(function () {
    $('#btnMain').on('click', function () {
        if($('.main-report').hasClass('d-none'))
        {
            $('.main-report').removeClass('d-none');
        }
        else
        {
            $('.main-report').addClass('d-none');
        }
    });
    $('#btnMain').on('click', function () {
        if ($('.main-map').hasClass('d-none'))
        {
            $('.main-map').removeClass('d-none');
        }
        else {
            $('.main-map').addClass('d-none');
        }
    });

});
