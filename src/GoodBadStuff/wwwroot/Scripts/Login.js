$(document).ready(function () {
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
    $("#closeBTN").click(function () {
        $('#myModal').modal('hide');
    });
});
