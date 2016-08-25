$(document).ready(function () {

    //LOGIN JS
    $("#login-submit").click(function () {
        $.post("/User/Login", { "Username": document.getElementById("username").value, "Password": document.getElementById("password").value }, function (result) {
            if (result === false)
                $("#messageLabel").innerHTML = "Login failed, please try again";
            else
                $("#messageLabel").innerHTML = "Login mkt tng, please try again";
            //Ändra massa greher till inloggat och sånt Gott och blandat.
            console.log(result);
        });
    });

    //REGISTER JS
    $("#register-submit").click(function () {
        $.post("/User/Create", { "Username": $("#usernameReg").val(), "Email": $("#emailReg").val(), "Password": $("#passwordReg").val()}, function(regResult) {
            console.log(regResult);
        });
    });

    //CLOSE MODAL
    $("#closeBTN").click(function () {
        $('#myModal').modal('hide');
    });
});


