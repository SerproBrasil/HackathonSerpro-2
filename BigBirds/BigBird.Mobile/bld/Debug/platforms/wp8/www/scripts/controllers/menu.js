var MenuController = {};

(function (ns) {
    ns.ultimaPagina = null;
    ns.paginaAtual = '';

    ns.bindEventos = function () {
        $("#menuVenda").click(ns.exibeVenda);
        $("#menuLista").click(ns.exibeLista);

    },
    
    ns.exibeVenda = function () {
        MensagemController.hide();
        AutorizacaoController.validar();
    }
    ns.exibeLista = function () {
        MensagemController.hide();
        ListaController.carregar();
    }

})(MenuController);

