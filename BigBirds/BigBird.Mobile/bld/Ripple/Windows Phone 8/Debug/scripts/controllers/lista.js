var ListaController = {};
var Total = 0;
(function (ns) {
    var textoPauta = null;

    ns.carregar = function () {  
        MensagemController.hide();
        $("#container").load("lista.html", function () {
            $(this).trigger("create");
            $.mobile.silentScroll(0);
            $("#btnCadastrar").click(ns.enviar);
        });
    }

    ns.validarEmail = function (email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    ns.enviar = function () {
        var email = $("#txtEmail").val();
        if (ns.validarEmail(email)) {
            navigator.notification.alert(
'Foi enviado um link para confirmação da presença para o email ' + email ,  // message
function () {
    $("#txtEmail").val("");
},         // callback
'Solicitação Enviada',            // title
'OK'                  // buttonName
);
        }
        else {
            navigator.notification.alert(
    'O email digitado não é válido!',  // message
    {},         // callback
    'Ocorreu um problema',            // title
    'OK'                  // buttonName
);
        }
    }

})(ListaController);