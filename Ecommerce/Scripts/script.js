

$(document).ready(function () {
    $('.sideMenuToggler').on('click', function () {
        $('.wrapper').toggleClass('active');

        console.log("clicked");

    });

    var adjustSidebar = function () {
        $('.sidebar').slimScroll({
            height: document.documentElement.clientHeight - $('.navbar').outerHeight()
        });

        console.log(document.documentElement.clientHeight - $('.navbar').outerHeight());
    };

    adjustSidebar();

    $(window).resize(function () {
        adjustSidebar();
        console.log("resize");
    });



});
