

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

        console.log($("#occurrence option:selected").val());
        console.log($('#occurrence-description').val());

        $('#btnPosition').on('click', function () {
            let latitude = marker.getPosition().lat();
            let longitude = marker.getPosition().lng();
            let option = opt;
            let description = desc;


            Swal.fire({
                title: "<i>Os dados informados foram:</i>",
                html: "Option: " + opt + "<br>Description: " + desc + "<br>Latitude: " + latitude + "<br>Longitude: " + longitude,
                confirmButtonText: "V <u>redu</u>",
            });

            $.ajax({
                url: "https://localhost:44335/helpbuy/getbystatus",
                type: 'post',
                data: {
                    latitude: "Maria Fernanda",
                    longitude: '3500'
                },
                beforeSend: function () {
                    $("#resultado").html("ENVIANDO...");
                }
            }).done(function (msg) {
                    $("#resultado").html(msg);
                })
                .fail(function (jqXHR, textStatus, msg) {
                    alert(msg);
                });

        });
    });
}

window.initMap = initMap;




