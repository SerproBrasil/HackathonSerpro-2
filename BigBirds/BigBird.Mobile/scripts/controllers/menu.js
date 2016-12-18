var MenuController = {};

(function (ns) {
    ns.ultimaPagina = null;
    ns.paginaAtual = '';

    ns.bindEventos = function () {
        WizardController.bindEventos();
        //HomeController.bindEventos();
    }

})(MenuController);

