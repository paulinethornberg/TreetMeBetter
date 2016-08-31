$(document).ready(function () {
    initialize();
    checkIsLoggedIn();
});

$("#oldpassword").focusout(function () {
    $.post("/User/CheckPassword", { "postContent": document.getElementById("oldpassword").value }, function (validCheck) {
        $("#")
        if (validCheck)
            console.log("Yeeeeeeeeeiiii");
        else console.log("oooh noooo");
    })
});



$("#updatebtn").click(function () {
    $.post("/User/UpdateUser", { "Username": document.getElementById("usernameReg").value, "CurrentPassword": document.getElementById("oldpassword").value, "Password": document.getElementById("confirmPassword").value, "Email": document.getElementById("emailReg").value }, function (result) {
        if (result === false)
            $("#message").text("Update failed, please try again");
        else {
            $("#formVal").Empty();
            $("#alertMessage").removeClass("none");
            document.getElementById('alertMessage').innerHTML = 'Update executed';

        }
        //stäng rutan
        //Ändra massa greher till inloggat och sånt Gott och blandat.
        console.log(result);

    });
});
