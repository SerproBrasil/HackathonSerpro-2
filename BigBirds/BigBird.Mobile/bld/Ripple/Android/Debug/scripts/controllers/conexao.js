var ConexaoController = {};

(function (ns) {
    ns.possuiConexao = function () {
        var networkState = navigator.connection.type;
        if (networkState == Connection.NONE) {
            return false;

        } else {
            return true;
        }
    }
})(ConexaoController);