$(document).ready(function () {

    $('form').submit(function (e) {
        var password = $('#password').val();
        var confirmPassword = $('#confirmPassword').val();
        console.log("Password:", password);
        console.log("Confirm Password:", confirmPassword);

        if (password !== confirmPassword) {
            alert("Password and confirm password do not match.");
            e.preventDefault(); // Prevent form submission
        }
    });
});
