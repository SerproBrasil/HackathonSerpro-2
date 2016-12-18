var WizardController = {};

(function (ns) {
    ns.bindEventos = function () {
        $("#container").load("wizard.html", function () {
            $(this).trigger("create");
            $.mobile.silentScroll(0);
            $("#btnIniciar").click(HomeController.bindEventos);
            $("#btnLigarAcessibilidade").click(ligarAcessibilidade);
        });
    }

    ligarAcessibilidade = function () {
        $("#btnAcessibilidadeOff").hide();
        $("#btnAcessibilidadeOn").show();
        $("#lblAcessibilidadeLigada").show();
        $("#lblLigarAcessibilidade").hide();

        TTS
      .speak({
            text: 'Eu sou o O erre Bot, e vou te ajudar a encontrar e abrir reclamações nos orgãos governamentais. Podemos começar?',
          locale: 'pt-BR',
          rate: 0.75
      }, function () {
      }, function (reason) {
          alert(reason);
      });
    }

})(WizardController);