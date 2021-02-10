$(document).ready(function () {

    $("#top-produit .owl-carousel").owlCarousel({
        loop: false,
        nav: true,
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 3
            },
            1000: {
                items: 5,
            }
        }
        
    });

    $('.owl-nav').removeClass("disabled");
    $('.owl-nav').on('click', function (e) {
        $('.owl-nav').removeClass("disabled");

    });

});