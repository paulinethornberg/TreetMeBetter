
//Login with enter
document.getElementById("login-form").addEventListener("keypress", function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById("login-submit").click();
    }
});

//Register with enter
document.getElementById("register-form").addEventListener("keypress", function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById("registerSubmit").click();
    }
});