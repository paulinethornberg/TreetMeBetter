$(document).ready(function () {
    initialize();
    checkIsLoggedIn();
});

$("#oldpassword").focusout(function () {
    $.post("/User/CheckPassword", { "postContent": document.getElementById("oldpassword").value }, function (validCheck) {
        if (validCheck) {
            $("#oldPasswordDiv").removeClass("has-feedback has-error")
            $("#errorCheck").addClass("none");
            $("#oldPasswordDiv").addClass("has-feedback has-success")
            $("#validCheck").removeClass("none");
        }
        else {
            $("#oldPasswordDiv").removeClass("has-feedback has-success")
            $("#validCheck").addClass("none");
            $("#oldPasswordDiv").addClass("has-feedback has-error")
            $("#errorCheck").removeClass("none");
        }
    })
});

$("#deletebtn").click(function () {
    ConfirmDelete();
    if (ConfirmDelete())
        $.post("/User/DeleteUser", function (deleteSuccess) {
            if (deleteSuccess)
                console.log("DELETED")
            else (deleteSuccess)
                console.log("STILL THERE MOAHAHAHAH")
        })
});

function ConfirmDelete() {
    var result = confirm("Are you sure you want to delete your account?");
    if (result)
        return true;
    else
        return false; 
    };


$("#updatebtn").click(function () {
    $.post("/User/UpdateUser", { "Username": document.getElementById("usernameReg").value, "CurrentPassword": document.getElementById("oldpassword").value, "Password": document.getElementById("confirmPassword").value, "Email": document.getElementById("emailReg").value }, function (result) {
        if (result === false)
            $("#message").text("Update failed, please try again");
        else {
            $("#alertMessage").removeClass("none");
            document.getElementById('alertMessage').innerHTML = 'Update executed';

        }
        //stäng rutan
        //Ändra massa greher till inloggat och sånt Gott och blandat.
        console.log(result);

    });
});
