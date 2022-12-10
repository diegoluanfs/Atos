

$(document).ready(function () {
    $('.btnView').on('click', function () {
        if ($('.btnView').hasClass('return')) {
            $('.map-controls').removeClass('d-none');
            $('.btnReport').removeClass('d-none');
            $('.btnView').removeClass('return');
            $('.btnView').html('View Map');
            $('.btnView').addClass('d-none');
        } else {
            $('.map-controls').addClass('d-none');
            $('.btnReport').addClass('d-none');
            $('.btnView').addClass('return');
            $('.btnView').html('Return');
            $('.btnView').addClass('d-none');
            if ($('.main-map').hasClass('d-none')) {
                $('.main-map').removeClass('d-none');
                $('.map-controls').removeClass('d-none');
                $('.main-report').removeClass('d-none');
                $('.main-report').addClass('d-none');
            }
        }
    });

});
