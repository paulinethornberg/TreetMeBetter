$(document).ready(function () {
    initialize();
    checkIsLoggedIn();
});

$("#oldpassword").focusout(function () {
    $.post("/User/CheckPassword", { "postContent": document.getElementById("oldpassword").value }, function (validCheck) {
        if (validCheck) {
            $("#oldPasswordDiv").removeClass("has-feedback has-error");
            $("#errorCheck").addClass("none");
            $("#oldPasswordDiv").addClass("has-feedback has-success");
            $("#validCheck").removeClass("none");
        }
        else {
            $("#oldPasswordDiv").removeClass("has-feedback has-success");
            $("#validCheck").addClass("none");
            $("#oldPasswordDiv").addClass("has-feedback has-error");
            $("#errorCheck").removeClass("none");
        }
    });
});

$("#deleteAccountbtn").click(function () {
    $.post("/User/DeleteUser", function (deleteResult) {
        if (deleteResult)
            window.location.href = "/";
        else
            window.location.href = "/User/MyAccount";
    });
});


$("#updatebtn").click(function () {
    $.post("/User/UpdateUser", { "Username": document.getElementById("usernameReg").value, "CurrentPassword": document.getElementById("oldpassword").value, "Password": document.getElementById("confirmPassword").value, "Email": document.getElementById("emailReg").value }, function (result) {
        if (result === false)
            $("#message").text("Update failed, please try again");
        else {
            $("#alertMessage").removeClass("none");
            document.getElementById('alertMessage').innerHTML = 'Update executed';
            //document.getElementById('oldpassword').value = '';
            //document.getElementById('confirmPassword').value = '';
            //document.getElementById('passwordReg').value = '';
            document.getElementById("formVal").reset();
            $("#oldPasswordDiv").removeClass("has-feedback has-success");
            $("#newPassword").removeClass("has-feedback has-success");
            $("#confPassword").removeClass("has-feedback has-success");
            $("#validCheck").addClass("none");
            $("#errorCheck").addClass("none");
            //$("#").addClass("none");
        }
        //stäng rutan
        //Ändra massa greher till inloggat och sånt Gott och blandat.
        console.log(result);

    });
});
