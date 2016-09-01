$(document).ready(function () {
    initialize();
    checkIsLoggedIn();
});

var geocoder;
var map;
var fromResult;
var toResult;
var directionsService;
var directionsDisplay;
var type;
var travelInfoId

$('#addTravelBtn').click(function () {
    $.post("/home/SaveCarbonData", { "Id": travelInfoId }, function (resultMessage) {
        console.log(resultMessage);
        AlertSaveMessage();
    });
});

// Alertmessage for saving a travel
function AlertSaveMessage() {
    $("#success-alert").removeClass('none');
    $("#success-alert").alert();
    $("#success-alert").fadeTo(2000, 500).slideUp(500, function () {
        $("#success-alert").slideUp(500);
    });
}

// check if used is logged in 
function checkIsLoggedIn() {
    $.get("/user/CheckIsLoggedIn", function (loggedIn) {
        if (loggedIn) {
            //Create navbar buttons
            console.log(loggedIn);
            $("#MyTravelsButton").removeClass('none');
            $("#MyAccountButton").removeClass('none');
            $("#logOutButton").removeClass('none');
            $("#saveBtnDiv").removeClass('none');
            $("#loggBtn").addClass('none');
        }
        else {
            console.log("Utloggad");
            $("#MyTravelsButton").addClass('none');
            $("#MyAccountButton").addClass('none');
            $("#logOutButton").addClass('none');
            $("#saveBtnDiv").addClass('none');
            $("#loggBtn").removeClass('none');
        }
    });
}



$('#goBtn').click(function () {
    var from = document.getElementById('from').value;
    var to = document.getElementById('to').value;
    $('#error').html('');
    if (type == null) {
        $('#error').html('Oops... something went wrong');
    }

    //  Do from-call
    var fromPromise = doGeoCall(from).then(function (result) {
        fromResult = result[0].geometry.location;
    }, function (err) {
        $('#error').html('Oops... something went wrong');
    });

    // Do to-call
    var toPromise = doGeoCall(to).then(function (result) {
        toResult = result[0].geometry.location;
    }, function (err) {
        $('#error').html('Oops... something went wrong');
    });
    fromPromise.then(function () {
        toPromise.then(function () {
            drawRoute();
            getCarbon(from, to);
        });
    });
});

// CONVERT TO TREES

function treeConverter(co2) {
    $("#treeDiv").empty();
    if (co2 >= 20000) {
        var numberOfBigTrees = Math.round(co2 / 20000);
        if ((numberOfBigTrees * 20000) - co2 >= 4000) {
            numberOfBigTrees = numberOfBigTrees - 1;
            co2 = co2 - (numberOfBigTrees * 20000);
            var numberOfSmallTrees = Math.round(co2 / 5000);
        }
        else if ((numberOfBigTrees * 20000) - co2 <= -4000) {
            co2 = co2 - (numberOfBigTrees * 20000);
            var numberOfSmallTrees = Math.round(co2 / 5000);
        }
    }
    else if (co2 < 20000) {
        var numberOfSmallTrees = Math.round(co2 % 20000 / 5000);
    }

    for (var i = 0; i < numberOfBigTrees; i++) {
        $("<span style='color:green;' class='glyphicon glyphicon-tree-deciduous glyphicon-large'></span>").appendTo("#treeDiv");
    }

    for (var i = 0; i < numberOfSmallTrees; i++) {
        $("<span style='color:green;' class='glyphicon glyphicon-tree-deciduous glyphicon-small'></span>").appendTo("#treeDiv");
    }
    $("body").animate({ scrollTop: $('body').prop("scrollHeight") }, 1500);
}

// SEND AND GET CARBON FROM API

function getCarbon(from, to) {
    $.post("/home/GetCarbonData", { "FromLat": fromResult.lat(), "FromLng": fromResult.lng(), "ToLat": toResult.lat(), "ToLng": toResult.lng(), "FromAddress": document.getElementById('from').value, "ToAddress": to, "Transport": type }, function (result) {

        var apiJsonString = result.apiJson;
        var apiJson = JSON.parse(apiJsonString);
        travelInfoId = result.travelInfoId;

        console.log(travelInfoId);

        switch (type) {
            case 'BICYCLING':
                setHTML(0, 'fa-bicycle', apiJson);
                break;
            case 'WALKING':
                setHTML(1, 'fa-blind', apiJson);
                break;
            case 'TRAIN':
                setHTML(2, 'fa-train', apiJson);
                break;
            case 'BUS':
                setHTML(3, 'fa-bus', apiJson);
                break;
            case 'DRIVING':
                setHTML(7, 'fa-car', apiJson);
                break;
            case 'MOTORCYCLE':
                setHTML(4, 'fa-motorcycle', apiJson);
                break;
        }
        document.getElementById('fromAddress').innerHTML = from;
        document.getElementById('toAddress').innerHTML = to;
        $("#searchContainer").removeClass('none');
        $("#saveOrNew").removeClass('none');
        $("#goBtn").addClass('none');
    });
}

$('#MakeNewTravelBtn').click(function () {
    $("#goBtn").removeClass('none');
    $("#saveOrNew").addClass('none');
    $("#searchContainer").addClass('none');
});

$('.type').click(function () {
    $(".type").removeClass('fa-cog');
    type = $(this).attr('value');
    $(this).addClass('fa-cog');
});

//SEARCH AND SEND VEHICLE TYPE
function drawRoute() {
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
    else if (type === 'MOTORCYCLE') {
        request = {
            origin: fromResult,
            destination: toResult,
            travelMode: 'DRIVING'
        };
    }
    else {
        request = {
            origin: fromResult,
            destination: toResult,
            travelMode: type
        };
    }
    directionsService.route(request, function (result, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(result);
        }
    });
}

function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(59.334591, 18.063240);

    var mapOptions = {
        zoom: 7,
        center: latlng,
        scrollwheel: false,
        draggable: false
    }
    directionsDisplay = new google.maps.DirectionsRenderer();
    directionsService = new google.maps.DirectionsService();
    map = new google.maps.Map(document.getElementById('map'), mapOptions);
    directionsDisplay.setMap(map);

    var fromInput = document.getElementById('from');
    var toInput = document.getElementById('to');
    var defaultBounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(-90, -180));
    var autoCompleteOptions = {
        types: ['geocode'],
        bounds: defaultBounds
    };
    fromAutoComplete = new google.maps.places.Autocomplete(fromInput, autoCompleteOptions);
    toAutoComplete = new google.maps.places.Autocomplete(toInput, autoCompleteOptions);
}

function doGeoCall(address) {
    return new Promise(function (resolve, reject) {
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                resolve(results);
            }
            else {
                reject(status);
            }
        });
    });
}

function setHTML(position, type, result) {
    var distance = result.emissions[position].routedDistance;
    if (position == 7 && distance > 2000000) {
        document.getElementById('postCarbon').innerHTML = Math.round(distance / 5556) + ' ' + 'Kg CO2';
        treeConverter(distance / 5, 5556);
    }
    else {
        document.getElementById('postCarbon').innerHTML = Math.round(result.emissions[position].totalCo2 / 1000) + ' ' + 'Kg CO2';
        treeConverter(result.emissions[position].totalCo2);
    }
        $('#vehicleIcon').attr('class', 'fa ' + type + ' fa-2x');
        $('#vehicleIcon2').attr('class', 'fa ' + type + ' fa-2x');
}
