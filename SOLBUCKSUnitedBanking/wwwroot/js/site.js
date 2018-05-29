// Write your Javascript code.
$(document).ready(function () {
    var url = window.location;
    $('.nav').find('.active').removeClass('active');
    $('.nav li a').each(function () {
        if (this.href == url.href) {
            $(this).parent().addClass('active');
        }
    }); 
});