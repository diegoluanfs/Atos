
var latitude, longitude;

if ('geolocation' in navigator) {
    navigator.geolocation.getCurrentPosition(
        function (position) {
            latitude = position.coords.latitude;
            longitude = position.coords.longitude;
        },
        function (error) {
            console.log(error)
        }
    )
}

let map;

function initMap() {
    //map = new google.maps.Map(document.getElementById("map"), {
    //    center: { lat: latitude, lng: longitude },
    //    zoom: 8,
    //});

    var myLatlng = new google.maps.LatLng(latitude, longitude);
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 12,
        center: { lat: latitude, lng: longitude },
        mapTypeId: "terrain",
    });


    // Create a <script> tag and set the USGS URL as the source.
    const script = document.createElement("script");

    // This example uses a local copy of the GeoJSON stored at
    // http://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/2.5_week.geojsonp
    script.src =
        "https://developers.google.com/maps/documentation/javascript/examples/json/earthquake_GeoJSONP.js";
    document.getElementsByTagName("head")[0].appendChild(script);



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

function GetMarkers() {
    $.ajax({
        url: 'https://localhost:7280/map/getall',
        type: 'GET',
        headers: { 'APIKey': '', 'APIVersion': '1.0' },
        contentType: 'application/json',
        async: false,
        processData: false,
        crossDomain: true,
        data: {},
        success: function (resp) {
            markers = resp.data;
        },
        error: function (ex) {
            //console.log(ex);
        }
    });
}





// Loop through the results array and place a marker for each
// set of coordinates.
const eqfeed_callback = function (results) {
    GetMarkers();
    $(markers).each(function (index, element) {
        const latLng = new google.maps.LatLng(element.latitude, element.longitude);
        new google.maps.Marker({
            position: latLng,
            map: map,
        });
    });
};

window.initMap = initMap;
window.eqfeed_callback = eqfeed_callback;
