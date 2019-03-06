// solving datapicker validation issue
// https://docs.telerik.com/aspnet-mvc/troubleshoot/troubleshooting-validation#globalized-dates-and-numbers-are-not-recognized-as-valid-when-using-the-validator
$(function () {
    $(".k-widget").removeClass("input-validation-error");
});