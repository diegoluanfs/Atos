

// Initialize and add the map
function initMap() {
    navigator.geolocation.getCurrentPosition(position => {
        const { latitude, longitude } = position.coords;
        var myLatlng = new google.maps.LatLng(latitude, longitude);
        var mapOptions = {
            zoom: 12,
            center: myLatlng
        }
        var map = new google.maps.Map(document.getElementById("map"), mapOptions);

        // Place a draggable marker on the map
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            draggable: true,
            title: "Alert!"
        });

        var opt = $("#occurrence option:selected").val();
        var desc = $('#occurrence-description').val();

        var cont = 0;
        $('.btnReport').on('click', function () {
            cont++;
            if (cont > 1) {

                console.log('opt', opt);
                console.log('desc', desc);

                var _latitude = marker.getPosition().lat();
                var _longitude = marker.getPosition().lng();
                let option = opt;
                let description = desc;

                if (_latitude.value == 0 || _longitude.value == 0) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'You need to mark the position of the occurrence on the map!',
                    });
                }
                else if ($('#occurrence').val() == 0) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Select the type of event!',
                    });
                }
                else {
                    var occurrence = JSON.stringify({
                        latitude: _latitude + "",
                        longitude: _longitude + "",
                        occurrenceType: $('#occurrence').val(),
                        occurrenceDescription: $('#occurrence-description').val()
                    });

                    $.ajax({
                        url: 'https://localhost:7280/Map/create',
                        type: 'POST',
                        headers: { 'APIKey': localStorage.getItem('l'), 'APIVersion': '1.0' },
                        contentType: 'application/json',
                        async: false,
                        processData: false,
                        crossDomain: true,
                        data: occurrence,
                        success: function (resp) {
                            var response = resp.result;
                            if (resp.data.length != 0) {
                                Swal.fire({
                                    position: 'top-end',
                                    icon: 'success',
                                    title: 'Your report has been saved successfully',
                                    showConfirmButton: false,
                                    timer: 5000
                                })
                            }
                        },
                        error: function (ex) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'Something went wrong!',
                                footer: '<a href="">' + ex + '</a>'
                            })
                        }
                    });
                }
            }
        });
    });
}


$.ajax({
    url: 'https://localhost:7280/Utility/getoccurrences',
    type: 'GET',
    headers: { 'APIKey': '', 'APIVersion': '1.0' },
    contentType: 'application/json',
    async: false,
    processData: false,
    crossDomain: true,
    data: {},
    success: function (resp) {
        var selectbox = $('#occurrence');
        $(resp.data).each(function (index, element) {
            selectbox.append('<option value="' + element.id + '">' + element.name + '</option>');
        });
    },
    error: function (ex) {
        //console.log(ex);
    }
});

$('.btnView').on('click', function () {
    if ($('.btnView').hasClass('return')) {
        $('.map-controls').removeClass('d-none');
        $('.btnReport').removeClass('d-none');
        $('.btnView').removeClass('return');
        $('.btnView').html('View');
    } else {
        $('.map-controls').addClass('d-none');
        $('.btnReport').addClass('d-none');
        $('.btnView').addClass('return');
        $('.btnView').html('Return');
    }
})

window.initMap = initMap;




