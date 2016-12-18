var MensagemController = {};

(function (ns) {
    ns.showAlerta = function (mensagem) {
        $('#msgBoxTxt').text(mensagem);
        $('#msgBox').show();
    }
    ns.hide = function() {
        $('#msgBox').hide();
    }
})(MensagemController);