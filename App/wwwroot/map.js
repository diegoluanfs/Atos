

// Initialize and add the map
function initMap() {
    navigator.geolocation.getCurrentPosition(position => {
        const { latitude, longitude } = position.coords;

        //console.log('latitude', position.coords.latitude);
        //console.log('longitude', position.coords.longitude);

        //// The location of Uluru
        //const uluru = { lat: latitude, lng: longitude };

        //// The map, centered at Uluru
        //const map = new google.maps.Map(document.getElementById("map"), {
        //    zoom: 12,
        //    center: uluru,
        //});

        //// The marker, positioned at Uluru
        //const marker = new google.maps.Marker({
        //    position: uluru,
        //    map: map,
        //});

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

        $('#btnPosition').on('click', function () {
            console.log('marker', marker.getPosition());
            console.log('latitude', marker.getPosition().lat());
            console.log('longitude', marker.getPosition().lng());
        });
    });
}

window.initMap = initMap;




