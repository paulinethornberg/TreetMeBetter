$("#update").click(function () {
    $.post("/User/Update", { "Username": document.getElementById("username").value, "Password": document.getElementById("password").value, "Email": document.getElementById("email").value }, function (result) {
        if (result === false)
            $("#message").text("Update failed, please try again");
        else {
            $("#message").text("Update executed");
        }
        //stäng rutan
        //Ändra massa greher till inloggat och sånt Gott och blandat.
        console.log(result);

    });
});

function GetUserInfo() {
    $.get("/User/GetUserInfo"), function(result){

    }
}