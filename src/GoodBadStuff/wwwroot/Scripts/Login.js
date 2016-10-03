$(document).ready(function () {

    //LOGIN JS
    $("#login-submit").click(function () {
        $.post("/User/Login", { "Username": document.getElementById("username").value, "Password": document.getElementById("password").value }, function (result) {
            if (result === false)
                $("#messageLabel").text("Login failed, please try again");
            else {
                $('#myModal').modal('hide');
                checkIsLoggedIn();
            }
            console.log(result);

        });
    });

    //Log Out
    $("#logOutButton").click(function () {
        $.post("/User/Logout", function () {
            checkIsLoggedIn();
        });
    });

    //REGISTER JS with validation
    $('#formVal').bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            usernameReg: {
                validators: {
                    stringLength: {
                        min: 2,
                    },
                    notEmpty: {
                        message: 'Please supply your first name'
                    }
                }
            },

            emailReg: {
                validators: {
                    notEmpty: {
                        message: 'Please supply your email address'
                    },
                    emailAddress: {
                        message: 'Please supply a valid email address'
                    }
                }
            },
            passwordReg: {
                validators: {
                    stringLength: {
                        min: 6,
                        message: 'Password length must be at least 6 characters'
                    },
                    notEmpty: {
                        message: 'Please supply your first name'
                    },
                }
            },

            confirmPassword: {
                validators: {

                    notEmpty: {
                        message: 'Passwords doesnt match'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Passwords doesnt match'
                    },
                    identical: {
                        field: 'passwordReg',
                        message: 'Passwords doesnt match'
                    }
                }
            }

        }
    })
               .on('success.form.bv', function (e) {
                   $('#success_message').slideDown({ opacity: "show" }, "slow") 
                   //$('#registerForm').data('bootstrapValidator').resetForm();

                   // Prevent form submission
                   e.preventDefault();
                   // Use Ajax to submit form data
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


});
