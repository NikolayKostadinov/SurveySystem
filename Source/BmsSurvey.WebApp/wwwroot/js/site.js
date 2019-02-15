// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getAntiforgeryToken() {
    return { "__RequestVerificationToken": $('input[name=__RequestVerificationToken]').val() };
}

function getAntiforgeryTokenAndPass() {
    var result = getAntiforgeryToken();
    result["Password"] = "111111";
    return result;
}
