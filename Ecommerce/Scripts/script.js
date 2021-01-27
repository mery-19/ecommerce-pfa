

$(document).ready(function () {
    $('.sideMenuToggler').on('click', function () {
        $('.wrapper').toggleClass('active');

        console.log("clicked");

    });

    var adjustSidebar = function () {
        $('.sidebar').slimScroll({
            height: document.documentElement.clientHeight - $('.navbar').outerHeight()
        });

    };

    adjustSidebar();

    $(window).resize(function () {
        adjustSidebar();
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

    window.$('.table').DataTable(frensh);
    window.$('.table-no-searh ').DataTable({
        searching: false, "lengthChange": false, bInfo: false, "language": {
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
        } });


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

    $(function () {
        $("a.delete-link-produit").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr = confirm('are you sure you want to delete this?');
            if (checkstr == true) {
                $.ajax({
                    url: '/Produits/Delete/' + $(".delete-link-produit").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.replace("https://localhost:44352/Produits/Index");
                    }
                });
            }
        }
        );
    });

    $(function () {
        $("a.delete-link-categorie").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr = confirm('are you sure you want to delete this?');
            if (checkstr == true) {
                $.ajax({
                    url: '/Categories/Delete/' + $(".delete-link-categorie").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.replace("https://localhost:44352/Categories/Index");
                    }
                });
            }
        }
        );
    });

    $(function () {
        $("a.delete-link-user").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr = confirm('are you sure you want to delete this?');
            if (checkstr == true) {
                $.ajax({
                    url: '/ApplicationUsers/Delete/' + $(".delete-link-user").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.replace("https://localhost:44352/ApplicationUsers/Index");
                    }
                });
            }
        }
        );
    });

    $(function () {
        $("a.delete-link-promo").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr = confirm('are you sure you want to delete this?');
            if (checkstr == true) {
                $.ajax({
                    url: '/Promotions/Delete/' + $(".delete-link-promo").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        window.location.replace("https://localhost:44352/Promotions/Index");
                    }
                });
            }
        }
        );
    });


    $(function () {
        $('#Produits').selectpicker();
       
    });


   /* $(function () {
        $(".modal_promo").click(function () {
            var my = $(this).data('id');
            console.log(my)
            $.ajax({
                url: '/Produits',
                type: "GET",
                success: function (res) {
                    console.log(res);
                }
            });
        }
        );
    });*/

    $(function () {
        $("#create").click(function (e) {
            e.preventDefault();
            var obj = [],
                items = '';
            /*  var tab = $('.produit_multi option:selected');
              for (i = 0; i < tab.length; i++){
                  console.log($('.produit_multi option:selected')[i].outerHTML);
        }*/
            $('.produit_multi option:selected').each(function (i) {
                obj.push($(this).val());
                console.log($(this).val());
            });

            taux =  $("input[name=taux_promotion]").val();
            date = $("input[name=date_expiration]").val();
            libele = $("input[name=libele]").val();
            description = $("input[name=description]").val();
            data = {
                taux_promotion:taux,
                date_expiration:date,
                libele,
                description,
               obj
            }
            // send select ittems to controlelr
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url:"/Promotions/CreateTest",
                data: JSON.stringify(data),
                success: function (data) {
                    console.log(data);

                    if (data) {
                        window.location.href = "https://localhost:44352/Promotions/Index";
                    }
                    else {
                        alert("Probléme d'envoyer les informations.");

                    }
                    
                },
                error: function (errorThrown) {
                    alert(errorThrown);
                }
            });

        });
    })



    /* --START-- On quantity change*/
    $("#qty").change(function () {
        console.log($("#qty option:selected").text());
        var qty = $("#qty option:selected").text();
        var id_produit = $("#Produit_id").val();
        data = {
            qty, id_produit
        }
        console.log(data);
        $.ajax({
            url: '/ProduitDetails/addProduit',
            type: "POST",
            data: data,
            success: function (res) {
                console.log(res);
                if (res.success) {
                    alert(res.responseText);
                } else {
                    alert(res.responseText);

                }
            }
        });
    });
    /* --END-- On quantity change*/
       
});