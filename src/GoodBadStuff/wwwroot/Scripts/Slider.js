// Future implementation of feed

//(function worker() {
//    $.get({
//        url: '/home/GetLatestInput',
//        success: function (data) {
//            var apiJson = JSON.parse(data);
//            for (var i = 0; i < apiJson.length; i++) {
//                $("#slider" + i).children("#fromSlider").html(apiJson[i].FromAddress);
//                $('#slider').children("#toSlider").html(apiJson[i].ToAddress);
//                $('#slider').children("#co2").html(apiJson[i].Co2);

//                switch (apiJson[0].Transport) {
//                    case 'DRIVING':
//                        $("#transport").addClass('fa-car');
//                        $("#transport1").addClass('fa-car');
//                    case 'WALKING':
//                        $('#transport').addClass('fa-blind');
//                        $('#transport1').addClass('fa-blind');

//                    case 'MOTORCYCLE':
//                        $('#transport').addClass('fa-motorcycle');
//                        $('#transport1').addClass('fa-motorcycle');

//                    case 'BICYCLING':
//                        $('#transport').addClass('fa-bicycle');
//                        $('#transport1').addClass('fa-bicycle');

//                    case 'BUS':
//                        $('#transport').addClass('fa-bus');
//                        $('#transport1').addClass('fa-bus');

//                    case 'TRAIN':
//                        $('#transport').addClass('fa-train');
//                        $('#transport1').addClass('fa-train');

//                }

//            }
//        },
//        complete: function () {
//            // Schedule the next request when the current one's complete
//            setTimeout(worker, 5000);
//        }
//    });
//})();