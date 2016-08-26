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
var type;

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

// FIXA TILL DENNA TREE CONVERTER FUNKTION

function treeConverter(co2) {
    $("#treeDiv").empty();

    var numberOfBigTrees = Math.round(co2 / 20000);
    var numberOfSmallTrees = Math.round(co2 % 20000 / 5000);

    for (var i = 0; i < numberOfBigTrees; i++) {
        $("<span style='color:green;' class='glyphicon glyphicon-tree-deciduous glyphicon-large'></span>").appendTo("#treeDiv");
    }

    for (var i = 0; i < numberOfSmallTrees; i++) {
        $("<span style='color:green;' class='glyphicon glyphicon-tree-deciduous glyphicon-small'></span>").appendTo("#treeDiv");
    }
}

// Send and get JSON from Carbon-API
function getCarbon() {
    $.post("/home/GetCarbonData", { "FromLat": fromResult.lat(), "FromLng": fromResult.lng(), "ToLat": toResult.lat(), "ToLng": toResult.lng(), "FromAddress": document.getElementById('from').value, "ToAddress": to }, function (result) {
        switch (type) {
            case 'BICYCLING':
                document.getElementById('postCarbon').innerHTML = Math.round(result.emissions[0].totalCo2 / 1000) + ' ' + 'Kg CO2';
                treeConverter(result.emissions[0].totalCo2);
                $('#vehicleIcon').attr('class', 'fa fa-bicycle fa-2x');
                $('#vehicleIcon2').attr('class', 'fa fa-bicycle fa-2x');
                break;
            case 'WALKING':
                document.getElementById('postCarbon').innerHTML = Math.round(result.emissions[1].totalCo2 / 1000) + ' ' + 'Kg CO2';
                treeConverter(result.emissions[1].totalCo2);
                $('#vehicleIcon').attr('class', 'fa fa-blind fa-2x');
                $('#vehicleIcon2').attr('class', 'fa fa-blind fa-2x');
                break;
            case 'TRAIN':
                document.getElementById('postCarbon').innerHTML = Math.round(result.emissions[2].totalCo2/1000) + ' ' + 'Kg CO2';
                treeConverter(result.emissions[2].totalCo2);
                $('#vehicleIcon').attr('class', 'fa fa-train fa-2x');
                $('#vehicleIcon2').attr('class', 'fa fa-train fa-2x');
                break;
            case 'BUS':
                document.getElementById('postCarbon').innerHTML = Math.round(result.emissions[3].totalCo2 / 1000) + ' ' + 'Kg CO2';
                $('#vehicleIcon').attr('class', 'fa fa-bus fa-2x');
                $('#vehicleIcon2').attr('class', 'fa fa-bus fa-2x');
                treeConverter(result.emissions[3].totalCo2);
                break;
            case 'DRIVING':
                if (document.querySelector('input[id="MOTORCYCLE"]:checked')) {
                    document.getElementById('postCarbon').innerHTML = Math.round(result.emissions[4].totalCo2 / 1000) + ' ' + 'Kg CO2';
                    treeConverter(result.emissions[4].totalCo2);
                    $('#vehicleIcon').attr('class', 'fa fa-motorcycle fa-2x');
                    $('#vehicleIcon2').attr('class', 'fa fa-motorcycle fa-2x');
                }
                else {
                    document.getElementById('postCarbon').innerHTML = Math.round(result.emissions[5].totalCo2 / 1000) + ' ' + 'Kg CO2';
                    treeConverter(result.emissions[5].totalCo2);
                    $('#vehicleIcon').attr('class', 'fa fa-car fa-2x');
                    $('#vehicleIcon2').attr('class', 'fa fa-car fa-2x');
                }
                break;
            case 'AIRCRAFT':
                document.getElementById('postCarbon').innerHTML = result.emissions[9].totalCo2;
                treeConverter(result.emissions[9].totalCo2);
                $('#vehicleIcon').attr('class', 'fa fa-plane fa-2x');
                $('#vehicleIcon2').attr('class', 'fa fa-plane fa-2x');
                break;

        }
        document.getElementById('fromAddress').innerHTML = from;
        document.getElementById('toAddress').innerHTML = to;
        $("#searchContainer").removeClass('none');
    });
}
//Search and send vehicle type
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
        if (status === 'OK') {
            directionsDisplay.setDirections(result);
        }
    });

}
function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(59.1946, 18.47);
    var mapOptions = {
        zoom: 7,
        center: latlng
    }
    directionsDisplay = new google.maps.DirectionsRenderer();
    directionsService = new google.maps.DirectionsService();
    map = new google.maps.Map(document.getElementById('map'), mapOptions);
    //var marker = new google.maps.Marker({
    //    position: latlng,
    //    map: map,
    //    draggable: true,
    //    title: "Drag me!"
    //});
    directionsDisplay.setMap(map);
}

function doGeoCall(address) {
    return new Promise(function (resolve, reject) {
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                resolve(results);
            }
            else {
                console.log("failed");
                reject(status);
            }
        });
    });
}
