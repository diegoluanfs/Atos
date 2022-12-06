

$(document).ready(function () {
    $('.btnReport').on('click', function () {
        if ($('.main-map').hasClass('d-none')) {
            $('.main-map').removeClass('d-none');
            $('.map-controls').removeClass('d-none');
            $('.main-report').removeClass('d-none');
            $('.main-report').addClass('d-none');
        }
    });
});
