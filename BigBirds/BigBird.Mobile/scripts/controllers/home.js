var HomeController = {};
var estadoAtual = "assunto";

(function (ns) {

    ns.bindEventos = function (data) {
        MensagemController.hide();

        $("#container").load("home.html", function () {
            $(this).trigger("create");
            $.mobile.silentScroll(0);
            $("#txtTexto").click();
            $("#btnEnviar").click(enviar);
            $("#txtTexto").focusin();
            $("#btnAcessibilidadeOn").click(function () {
                $("#btnAcessibilidadeOn").hide();
                $("#btnAcessibilidadeOff").show();
            });

            $("#btnAcessibilidadeOff").click(function () {
                $("#btnAcessibilidadeOff").hide();
                $("#btnAcessibilidadeOn").show();
            })
        });
    }

    voz = function (texto) {
        TTS
       .speak({
           text: texto,
           locale: 'pt-BR',
           rate: 0.75
       }, function () {
       }, function (reason) {
           alert(reason);
       });
    }

    enviar = function () {
        $(".digitando").show();
        var templateUser = $("#templateUser").html();
        var mensagem = $("#txtTexto").val();
        $("#txtTexto").val("");
        templateUser = templateUser.replace("{mensagem}", mensagem);
        $("#formHome").append(templateUser);
        var resposta = "";

        if (estadoAtual == "assunto") {
            resposta = respostaAssunto(mensagem);
            if (resposta == false) {
                resposta = "Desculpe mas não entendi muito bem esse assunto, alguns assuntos que conheço bem: Saúde, Educação, Transporte... Digite um novo assunto por favor";
            }
            else {
                estadoAtual = "problema";
            }
            exibirResposta(resposta);

        }

        else if (estadoAtual == "problema") {
            resposta = respostaProblema(mensagem);
            if (resposta == false) {
                resposta = "Desculpe mas não sei bem como resolver esse problema, mas vou encaminhar para um dos funcionários da ouvidoria.";
                exibirResposta(resposta);

                setTimeout(function () {
                    abrirProtocolo(mensagem);
                    estadoAtual = "fim";
                }, 5000);
            }
            else {
                estadoAtual = "fim";
                exibirResposta(resposta);
            }
        }

        else if (estadoAtual == "fim") {
            mensagem = mensagem.toLowerCase();
            if (mensagem == "não" || mensagem == "nao") {
                resposta = "OK, estou aqui para o que precisar :)";
                estadoAtual = "recomecar";
            }
            else {
                resposta = "Certo, seu problema é relacionado com que assunto? Ex: Saúde, Educação, Serviços, Transporte Público...";
                estadoAtual = "assunto";
            }
            exibirResposta(resposta);

        }
        else if (estadoAtual == "recomecar") {
            estadoAtual = "assunto";
            resposta = "Olá, seu problema é relacionado com que assunto? Ex: Saúde, Educação, Serviços, Transporte Público...";
            exibirResposta(resposta);

        }

    }

    abrirProtocolo = function (textoReclamacao) {
        var data = {
            "manifestacao": {
                "Assunto": {
                    "Id": 535,
                    "Descricao": "Conduta do motorista de ônibus",
                    "Ativo": false
                },
                "Cidadao": {
                    "CPF": "281.315.663-90"
                },
                "RegiaoAdministrativa": {
                    "Id": 1,
                    "Descricao": "RA I Brasília",
                    "Ativo": true
                },
                "LocalDoFato": {
                    "X": -15.8300029,
                    "Y": -47.92990180000001,
                    "Descricao": "W3 Sul  Via Urbana Via W3 Sul Via W3 Sul"
                },
                "Relato": textoReclamacao,
                "Classificacao": {
                    "Id": 2,
                    "Indice": 5,
                    "Descricao": "Reclamação",
                    "PermitirAnonimato": true,
                    "TextoExplicativo": textoReclamacao,
                    "Icone": "lnr lnr-thumbs-down text-warning",
                    "AcessoExterno": null
                }
            },
            "manifestacaoPorAtendente": false,
            "manifestacaoVinculada": null
        };
        $.POST('http://www.ouv.df.gov.br//Home/CadastrarManifestacao', data, function (response) {
            var manifestacaoId = response.Object.manifestacaoId;
            data = { "manifestacaoId": manifestacaoId };
            $.POST('http://www.ouv.df.gov.br/Home/BuscarProtocolo', data, function (protocolo) {
                exibirResposta("Tudo certo, enviei sua reclamação para um dos meus colegas e eles pediram pra te informar o protocolo número " + protocolo + ", mas não se preocupe que te avisarei quando eles tiverem uma resposta.");
                setTimeout(function () {
                    exibirResposta("Algo mais que posso te ajudar?");
                }, 14000);
            });
        });
    }

    exibirResposta = function (mensagem) {
        $(".digitando").hide();
        var templateBot = $("#templateBot").html();
        templateBot = templateBot.replace("{mensagem}", mensagem);
        $("#formHome").append(templateBot);
        $('#formHome').animate({ scrollTop: 9999 }, 'slow');
        if ($("#btnAcessibilidadeOn").is(":visible")) {
            voz(mensagem);
        }
    }

    respostaAssunto = function (assunto) {
        assunto = assunto.toLowerCase();
        if (assunto == "saúde" || assunto == "saude" || assunto == "transporte publico" || assunto == "transporte" || assunto == "educação")
            return "Certo, me conte agora um pouco mais sobre o seu problema."
        else {
            return false;
        }
    }

    respostaProblema = function (problema) {
        problema = problema.toLowerCase();
        var url = "http://desktop-r8fmlht/orbot/api/v1/chat/answers";

        if (problema == "minha mala foi extraviada")
            return "Entendi, eu te recomendo tentar abrir um chamado na ouvidoria da cia aerea, Algo mais em que posso te ajudar?";
        else {
            return false;
        };

        //$.POST(url, { content: problema }, function (response) {
        //    if (response != "false")
        //        return "Entendi, eu te recomendo " + response + ", Algo mais em que posso te ajudar?";
        //    else {
        //        return false;
        //    };
        //});

    }

})(HomeController);