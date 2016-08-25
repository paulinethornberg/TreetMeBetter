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
        
        $.post("/User/Create", { "Username": $("#usernameReg").val(), "Email": $("#emailReg").val(), "Password": $("#passwordReg").val()}, function(regResult) {
            if (regResult === false)
                $("#messageLabel").text("Registration failed, please try again");
            else
                $("#messageLabel").text("Registration succeeded, you are logged in!");
            $("#empty").html("<h1 style='color:green'>Registration Successful!</h1>"

                // VIKTOR FIXA DETTA!!

                  <div class="row">
                                <div class="col-xs-5">
                                    <a href="#" class="active" id="login-form-link">Login</a>
                                </div>
                                <div class="col-xs-5">
                                    <a href="#" id="register-form-link">Register</a>
                                </div>
                                <div class="col-xs-2">
                                    <i class="fa fa-times fa-2x" aria-hidden="true" id="closeBTN"></i>
                                </div>
                            </div>


                );
            console.log(regResult);

        });

    });

    //CLOSE MODAL
    $("#closeBTN").click(function () {
        $('#myModal').modal('hide');
    });
});

 

