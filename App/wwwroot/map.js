

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
                var _latitude = marker.getPosition().lat();
                var _longitude = marker.getPosition().lng();
                let option = opt;
                let description = desc;

                var occurrence = JSON.stringify({
                    latitude: _latitude + "",
                    longitude: _longitude + "",
                    occurrenceType: $('#occurrence').val(),
                    occurrenceDescription: $('#occurrence-description').val()
                });

                console.log(occurrence);

                $.ajax({
                    url: 'https://localhost:7280/Map/create',
                    type: 'POST',
                    headers: { 'APIKey': localStorage.getItem('l'), 'APIVersion': '1.0' },
                    contentType: 'application/json',
                    async: false,
                    processData: false,
                    crossDomain: true,
                    data: occurrence,
                    success: function (data) {
                        console.log(data);
                    },
                    error: function (ex) {
                        console.log(ex);
                    }
                });
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

window.initMap = initMap;




