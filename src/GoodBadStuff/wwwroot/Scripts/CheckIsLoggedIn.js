// check if user is logged in 
function checkIsLoggedIn() {
    $.get("/user/CheckIsLoggedIn", function (loggedIn) {
        if (loggedIn) {
            //Create navbar buttons
            console.log(loggedIn);
            $("#MyTravelsButton").removeClass('none');
            $("#MyAccountButton").removeClass('none');
            $("#logOutButton").removeClass('none');
            $("#saveBtnDiv").removeClass('none');
            $("#loggBtn").addClass('none');
        }
        else {
            console.log("Utloggad");
            $("#MyTravelsButton").addClass('none');
            $("#MyAccountButton").addClass('none');
            $("#logOutButton").addClass('none');
            $("#saveBtnDiv").addClass('none');
            $("#loggBtn").removeClass('none');
        }
    });
}
$(document).ready(function () {
    checkIsLoggedIn();
})
