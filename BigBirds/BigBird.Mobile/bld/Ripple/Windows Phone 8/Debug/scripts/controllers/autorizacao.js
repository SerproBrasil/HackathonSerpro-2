var AutorizacaoController = {};

(function (ns) {
    var textoPauta = null;
    ns.validar = function () {
        if (!ConexaoController.possuiConexao()) {
            alert("Dispositivo sem conexão de dados, por favor se conecte a internet e tente novamente.");
            navigator.app.exitApp();
        }
        else {
            var uuid = device.uuid;
            var url = baseUrl + "/Autorizacao.asmx/ValidarApresentacaoProdutor";
            $.msajax(url, { imei: uuid }, ns.finalizaValidacao, null);
        }
    },
    ns.finalizaValidacao = function (data) {
        if (!data.estaValido) {
            alert(data.erro);
        }
        else {
            $("#container").load("venda.html", function () {
                $(this).trigger("create");
                $.mobile.silentScroll(0);
                $("#evento").text(data.apresentacao);
                VendaController.carregar();
            });
        }
    }
})(AutorizacaoController);