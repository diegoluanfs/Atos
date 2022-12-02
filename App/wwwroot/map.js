

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

        $('.btnReport').on('click', function () {
            var _latitude = marker.getPosition().lat();
            var _longitude = marker.getPosition().lng();
            let option = opt;
            let description = desc;

            console.log(JSON.stringify({
                latitude: _latitude,
                longitude: _longitude
            }));

            $.ajax({
                url: 'https://localhost:7280/Map/create',
                type: 'POST',
                headers: { 'APIKey': localStorage.getItem('l'), 'APIVersion': '1.0' },
                contentType: 'application/json',
                async: false,
                processData: false,
                crossDomain: true,
                data: function (d) {
                    return JSON.stringify({
                        latitude: _latitude,
                        longitude: _longitude
                    });
                },
                success: function (data) {
                    //console.log(data);
                },
                error: function (ex) {
                    //console.log(ex);
                }
            });


            //Swal.fire({
            //    title: "<i>Os dados informados foram:</i>",
            //    html: "Option: " + opt + "<br>Description: " + desc + "<br>Latitude: " + latitude + "<br>Longitude: " + longitude,
            //    confirmButtonText: "V <u>redu</u>",
            //});

        });
    });
}

window.initMap = initMap;




