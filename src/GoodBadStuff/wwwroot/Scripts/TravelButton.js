$("button").click(function () {
    var buttonValue = $(this).val();
    if (buttonValue === "tableDiv")
        $('#chartDiv').addClass("none");
    else if (buttonValue === "chartDiv")
        $('#tableDiv').addClass("none");

    $('#' + buttonValue).toggleClass("none");
    $('html, body').animate({
        scrollTop: $("#toggleBtn").offset().top
    }, 200);
})