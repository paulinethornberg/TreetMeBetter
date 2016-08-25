$(document).ready(function () {

    //LOGIN JS
    $("#login-submit").click(function () {
        $.post("/User/Login", { "Username": document.getElementById("username").value, "Password": document.getElementById("password").value }, function (result) {
            if (result === false)
                $("#messageLabel").text("Login failed, please try again");
            else
                $('#myModal').modal('hide');
            //stäng rutan
            //Ändra massa greher till inloggat och sånt Gott och blandat.
            console.log(result);
        });
    });

    //REGISTER JS
    $("#register-submit").click(function () {
        $("#messageLabel").empty();
        $.post("/User/Create", { "Username": $("#usernameReg").val(), "Email": $("#emailReg").val(), "Password": $("#passwordReg").val() }, function (regResult) {
            if (regResult === false)
                $("#messageLabel").text("Registration failed, please try again");
            else {
                $("#register-form").html("<h1 style='color:green; text-align:center;'>Registration Successful!</h1>");
                LoggedIn();
            }

            console.log(regResult);

        });

    });

    //CLOSE MODAL
    $("#closeBTN").click(function () {
        $('#myModal').modal('hide');
    });

//INPUT VALIDATER
$("usernameReg")
  .focusout(function () {
      $("#usernameReg").addClass("");
  })

function LoggedIn() {
    $("#navBtns").html(" <li><a href='#'><span id='btn1' class='glyphicon glyphicon-user'></span> Sign Up</a></li>");
}

});
