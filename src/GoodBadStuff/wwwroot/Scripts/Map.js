$(document).ready(function () {
    initialize();
});

    var geocoder;
    var map;
    var fromResult;
    var toResult;
    var directionsService;
    var directionsDisplay;
    var transit;
    var from;
    var to;

    function onClick() {
        from = document.getElementById('from').value;
        to = document.getElementById('to').value;



        //  Do from-call
        var fromPromise = doGeoCall(from).then(function (result) {
            fromResult = result[0].geometry.location;
        }, function (err) {
            alert('Geocode for from-call was not successful for the following reason: ' + err);
        });

        // Do to-call
        var toPromise = doGeoCall(to).then(function (result) {
            toResult = result[0].geometry.location;
        }, function (err) {
            alert('Geocode for to-call was not successful for the following reason: ' + err);
        });

        fromPromise.then(function () {
            toPromise.then(function () {
                console.log(fromResult.lat() + " " + fromResult.lng());
                console.log(toResult.lat() + " " + toResult.lng());
                drawRoute();
                getCarbon();
            });
        });
    }

    // Send and get JSON from Carbon-API
    function getCarbon() {
        $.post("/home/GetCarbonData", { "FromLat": fromResult.lat(), "FromLng": fromResult.lng(), "ToLat": toResult.lat(), "ToLng": toResult.lng() }, function (result) {
            console.log(result);
            document.getElementById('postCarbon').innerHTML = result.emissions[5].totalCo2;
            document.getElementById('fromAddress').innerHTML = from;
            document.getElementById('toAddress').innerHTML = to;
            

        });
    }

    function drawRoute() {
        // Todo: putsa på koden så denna lösning blir snyggare
        type = document.querySelector('input[name="type"]:checked').value;
        var request;

        if (type === 'BUS' || type === 'TRAIN') {
            request = {
                origin: fromResult,
                destination: toResult,
                travelMode: 'TRANSIT',
                transitOptions: {
                    modes: [type]
                }
            }
        }
        else
            request = {
                origin: fromResult,
                destination: toResult,
                travelMode: type
            };
        directionsService.route(request, function (result, status) {
            if (status == 'OK') {
                directionsDisplay.setDirections(result);
            }
        });

    }
    function initialize() {
        geocoder = new google.maps.Geocoder();
        var latlng = new google.maps.LatLng(58.397, 18.644);
        var mapOptions = {
            zoom: 8,
            center: latlng
        }
        directionsDisplay = new google.maps.DirectionsRenderer();
        directionsService = new google.maps.DirectionsService();
        map = new google.maps.Map(document.getElementById('map'), mapOptions);
        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            draggable: true,
            title: "Drag me!"
        });
        directionsDisplay.setMap(map);
    }

    function doGeoCall(address) {
        return new Promise(function (resolve, reject) {
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == 'OK') {
                    resolve(results);
                }
                else {
                    console.log("failed");
                    reject(status);
                }
            });
        });
    }
