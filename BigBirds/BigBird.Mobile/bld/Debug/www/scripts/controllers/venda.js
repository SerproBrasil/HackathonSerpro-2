var VendaController = {};
var Total = 0;
(function (ns) {
    var textoPauta = null;

    ns.carregar = function () {
        if (!ConexaoController.possuiConexao()) {
            alert("Dispositivo sem conexão de dados, por favor se conecte a internet e tente novamente.");
            navigator.app.exitApp();
        }
        else {
           var uuid = device.uuid;
            var url = baseUrl + "/ProdutorService.asmx/ObterVendas";
            $.msajax(url, { uuid: uuid }, function (data) {
                ns.exibir(data);

                $('#containerVenda li').click(function () {                    

                });
            }, null);
        }
    },

    ns.exibir = function (data) {
        var li = '<li data-icon="shop" class="liVenda" Venda="{3}" IdVenda="{2}" Quantidade="{4}" Valor="{5}"><a href="#"><h1>{0}</h1><p style="font-size: 11pt">{1}</p></a></li>';
        var html = '';
        if (data.length > 0) {
            html += li.replace("{0}", data[0].Quantidade + " Ingressos Impressos vendidos").replace("{1}", "Total: " + data[0].Total.toFixed(2).replace(".", ","));

            html += li.replace("{0}", data[2].Quantidade + " Ingressos Online vendidos").replace("{1}", "Total: " + data[2].Total.toFixed(2).replace(".", ","));
            html += li.replace("{0}", data[1].Quantidade + " Ingressos Cortesia distribuidos").replace("{1}", "Total: " + data[1].Total.toFixed(2).replace(".", ","));
        }
        else {
            $("#containerVenda").after("<h3 class='content'>Nenhuma Venda cadastrada.</h3>");
        }
        $("#containerVenda").empty();
        $("#containerVenda").append(html);
        $("#containerVenda").listview("refresh");
        $("#containerVenda").trigger("updatelayout");
    }

})(VendaController);