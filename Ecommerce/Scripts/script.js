

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
    window.$('.table-no-searh').DataTable({
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

/*--Start-- on save category with empty image */
$(function () {
    $("#SaveCategory").click(function () {
        console.log("clicked");
        if ($("#categoryImage").val() == "") {
            $("#err").fadeIn();
            $("#view-err").text("l'image de catégorie requis.");
            return false; //for not submit informations to server 
        }
    });
    /*--END-- on save category with empty image */

    /*--START-- on delete item*/
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
        $("a.delete-produit-envie").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();

            console.log(token);
            var checkstr = confirm('are you sure you want to delete this?');
            if (checkstr == true) {
                $.ajax({
                    url: '/Envies/Delete/' + $(this, ".delete-produit-envie").attr('data-delete-id'),
                    type: "POST",
                    data: {
                        __RequestVerificationToken: token,
                    },
                    success: function () {
                        location.reload();
                    }
                });
            }
        }
        );
    });

    /*--Start-- on delete item*/

    $(function () {
        $('#Produits').selectpicker();

    });


    /* --START-- Create promotions with multiple product*/
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

            taux = $("input[name=taux_promotion]").val();
            date = $("input[name=date_expiration]").val();
            libele = $("input[name=libele]").val();
            description = $("input[name=description]").val();
            data = {
                taux_promotion: taux,
                date_expiration: date,
                libele,
                description,
                obj
            }
            // send select ittems to controlelr
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: "/Promotions/CreateTest",
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
    /* --END-- Create promotions with multiple product*/



    /* --START-- On quantity change*/

    //in details page
    $("#qty").change(function () {
        console.log($("#qty option:selected").text());
        var qty = $("#qty option:selected").text();
        var id_produit = $("#id").val();
        postQty(qty, id_produit);
    });

    //in panier
    var dropdowns = $(".panier_qty");
    var id_dropdowns = [];
    for (var i = 0; i < dropdowns.length; i++) {
        console.log(dropdowns[i]);
        console.log(dropdowns[i].id);
        id_dropdowns.push(dropdowns[i].id);
    }
    for (var i = 0; i < id_dropdowns.length; i++) {
        var id = "#" + id_dropdowns[i]
        console.log(id_dropdowns[i]);
        $(id).change(function () {
            var qty = $("option:selected", this).text();
            var id_produit = $(this).attr("idProduit");
            postQty(qty, id_produit);
        })
    }

    //post function
    var postQty = function (qty, id_produit) {
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
                    location.reload();

                } else {
                    alert(res.responseText);

                }
            }
        });


    };
    /* --END-- On quantity change*/

    /* --START-- Delete from panier*/

    $(function () {
        $("a.delete-link-panier").click(function () {

            var token = $("[name='__RequestVerificationToken']").val();
            var id_panier = $(".delete-link-panier").attr('id-panier');
            var id_produit = $(".delete-link-panier").attr('id-produit');
            var data = { id_panier, id_produit, __RequestVerificationToken: token, };
            console.log(data);
            var checkstr = confirm('are you sure you want to delete this?');
            if (checkstr == true) {
                $.ajax({
                    url: '/LignePaniers/DeleleLignePanier',
                    type: "POST",
                    data: data,
                    success: function (res) {
                        console.log(res);
                        if (res.success) {
                            alert(res.responseText);
                            location.reload();

                        } else {
                            alert(res.responseText);

                        }
                    }
                });
            }
        }
        );
    });

    /* --END-- Delete from panier*/

    /* --START-- Mettre au cote*/

    $(function () {
        $("a.envie").click(function () {

            var id_produit = $(this, ".envie").attr('id');
            console.log("id produit: " + id_produit);
            var data = { id_produit };
            console.log(data);
            $.ajax({
                url: '/Envies/Add',
                type: "POST",
                data: data,
                success: function (res) {
                    console.log(res);
                    if (res.success) {
                        alert(res.responseText);
                        location.reload();

                    } else {
                        alert(res.responseText);

                    }
                }
            });
        }
        );
    });

    /* --END-- Mettre au cote*/

    /*--START-- on finaliser commande*/
    $(function () {
        $("#finaliser-commande").click(function (e) {
            console.log("clicked");
            var id_liv = $("input[name='id_mode_livraison']:checked").val();
            var id_pai = $("input[name='id_paiement']:checked").val();
            var address = $("textarea#address").val();
            var phone = $("input[name='phone']").val();
            console.log(address);
            console.log(phone);
            console.log(id_liv);
            if (id_liv == null || id_pai == null || $.trim(address) == "" || $.trim(phone) == "") {
                $("#err").fadeIn();
                $("#view-err").text("Tous les parties sont requis.");
                return false; //for not submit informations to server 
            }
        });
    });
    /*--END-- on finaliser commande */

    /*--START-- close the alert */
    $(".close_err").click(function (e) {
        e.preventDefault();
        $("#err").fadeOut();
        return false; //for not submit informations to server 
    });
    /*--END-- close the alert */


    /*--START-- detaills d'un commande */

    $('#commandes-table tbody').on('click', '.btn-details-commande', function (e) {
        e.preventDefault();
        console.log("clicked");
        var id_commande = $(this).attr("id");
        var total = 0;
        var row = "<tr class='border p-2'><td>Nom</td ><td>image</td><td>Quantité</td><td>Prix total</td></tr >";
        console.log("--------------------------");
        console.log("id_commande: " + id_commande);
        console.log(row);
        console.log("--------------------------");
        $.ajax({
            url: '/Commandes/showProductInCommande',
            type: "POST",
            data: {
                "id": id_commande
            },
            success: function (data) {
                $.each(JSON.parse(data), function (i, v) {

                    console.log("enter 1");
                    row += "<tr class='border'><td>" + v.name + "</td><td>  <img src='/Uploads/Produit_image/" + v.image + "' style='height: 120px; width: 120px; ' alt='produit' class='img-fluid'> </td><td>" + v.qty + "</td><td>" + v.prix.toFixed(2) + " DHs";
                    total += parseFloat(v.prix);
                    console.log("enter 1");
                });

                $(".mesProduits").html(row);
                $(".prix").text(total.toFixed(2) + " DHs");


            }
        });
    });


    /*--END-- detaills d'un commande  */

    /*--START-- filter commandes  */
    /*    $(function () {
            $(".filter-commande").click(function (e) {
                console.log($(this, ".filter-commande").attr('id'));
                var id = $(this, ".filter-commande").attr('id');
                $.ajax({
                    url: '/Commandes/All/' + id,
                    type: "GET",
                    success: function (data) {
    
                    }
    
                });
            });
        });*/
    /*--END-- filter commandes  */


    /*--START-- change commande status  */

    $('#commandes-table tbody').on('click', '#status', function (e) {
        console.log($("#status option:selected").text());
        var status = $($(this), "option:selected").val();
        var id_commande = $(this).attr("id-commande");
        if (status == 2) {
            console.log(status);
            console.log(id_commande);
            var data = { id: id_commande }
            $.ajax({
                url: '/Commandes/Livre',
                type: "POST",
                data: data,
                success: function (res) {
                    if (res.success) {
                        alert(res.responseText);
                        location.reload();

                    } else {
                        alert(res.responseText);

                    }
                }

            });
        }

    });
    /*--END-- change commande status  */

    /*--START-- set notification on 0 after click  */
/*    $('.not-click').click(function (e) {
        $.ajax({
            url: '/Commandes/RestartNot',
            type: "POST",
            success: function (res) {

            }

        });
    });*/
    /*--END-- set notification on 0 after click  */

    /*--START-- On card item click */
    $('#top-produit').on('click', '.name-produit', function (e) {
        var id_produit = $(this).attr("id-produit");
        console.log(id_produit);
        window.location.replace("https://localhost:44352/ProduitDetails/Index/" + id_produit);
    });
    $('.body').on('click', '.name-produit', function (e) {
        var id_produit = $(this).attr("id-produit");
        console.log(id_produit);
        window.location.replace("https://localhost:44352/ProduitDetails/Index/" + id_produit);
    });
    /*--END-- On card item click */

    /*--START-- add comment */
    $('.add-comment').click(function (e) {
        var comment = $('#commmentaire').val();
        if (comment.trim() === "") {
            alert("Le commentaire est VIDE!");
            return false;
        }
        var data = {
            "id_produit": $("#id_produit").val(),
            "commentaire": comment
        };
        console.log(data);
        $.ajax({
            url: '/ProduitDetails/AddComment',
            type: "POST",
            data: data,
            success: function (res) {
                console.log(res.responseText);
                alert(res.responseText);
                if (res.success) {
                    location.reload();
                }
            }
        });
    })
    /*--END-- add comment */
    /*  var fakedata = ['test1', 'test2', 'test3', 'test4', 'ietsanders'];
      $("#txt").autocomplete({ source: fakedata });*/
    $("#txt").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Home/Index",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    console.log(data);
                    return response(data);
                }
            })
        },
        messages: {
            noResults: '',
            results: function (resultsCount) { }
        }
    });

    $('.search-btn').click(function () {

        var txt = $("#txt").val();
        if ($.trim(txt) == "") {
            alert("Le champs de recherche est vide.");
            return false;
        }
    });


});

