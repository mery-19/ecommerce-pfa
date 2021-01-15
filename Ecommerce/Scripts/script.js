

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


/*    var click = function () {
        var demo = $("#demo");
        $('button[data-toggle="modal"]').click(function (e) {
            var url = $(this).data('url');
            console.log(url);
            $.get(url).done(function (data) {
                console.log(data);

                demo.html(data);
                demo.find('.modal').modal('show');
            })

        })
    };

    
    click();*/

    var frensh = {
        "language": {
            "sProcessing": "Traitement en cours ...",
            "sLengthMenu": "Afficher _MENU_ lignes",
            "sZeroRecords": "Aucun résultat trouvé",
            "sEmptyTable": "Aucune donnée disponible",
            "sInfo": "Lignes _START_ à _END_ sur _TOTAL_",
            "sInfoEmpty": "Aucune ligne affichée",
            "sInfoFiltered": "(Filtrer un maximum de_MAX_)",
            "sInfoPostFix": "",
            "sSearch": "Chercher:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Chargement...",
            "oPaginate": {
                "sFirst": "Premier", "sLast": "Dernier", "sNext": "Suivant", "sPrevious": "Précédent"
            },
            "oAria": {
                "sSortAscending": ": Trier par ordre croissant", "sSortDescending": ": Trier par ordre décroissant"
            }
        }
    }

    window.$('#categorie').DataTable(frensh);
    window.$('#produitTable').DataTable(frensh);

});


// for save category without image
$(function () {
    $("#SaveCategory").click(function () {
        console.log("clicked");
        if ($("#categoryImage").val() == "") {
            $("#err").fadeIn();
            $("#view-err").text("l'image de catégorie requis.");
            return false; //for not submit informations to server 
        }
    });
});