//var baseUrl = 'http://fenacon.agenciamagine.com.br/WebServices';
var baseUrl = 'http://192.168.1.5/FurandoFila.Web.Services/';

//sends a json request to an ASMX or WCF service configured to reply to JSON requests.
(function ($) {
    var tries = 0; //IE9 seems to error out the first ajax call sometimes... will retry up to 5 times

    $.msajax = function (url, data, onSuccess, onError) {
        return $.ajax({
            'type': "POST"
          , 'url': url
          , 'contentType': "application/json"
          , 'dataType': "json"
          , 'data': typeof data == "string" ? data : JSON.stringify(data || {})
          , beforeSend: function (jqXHR) {
              $.mobile.loading("show", {
                  disabled: true
              });
              jqXHR.setRequestHeader("X-MicrosoftAjax", "Delta=true");
          }
          , 'complete': function (jqXHR, textStatus) {
              $.mobile.loading("hide");
              handleResponse(jqXHR, textStatus, onSuccess, onError, function () {
                  setTimeout(function () {
                      $.msajax(url, data, onSuccess, onError);
                  }, 100 * tries); //try again
              });
          }
        });
    }

    $.msajax.defaultErrorMessage = "Error retreiving data.";


    function logError(err, errorHandler, jqXHR) {
        tries = 0; //reset counter - handling error response

        //normalize error message
        if (typeof err == "string") err = { 'Message': err };

        if (console && console.debug && console.dir) {
            console.debug("ERROR processing jQuery.msajax request.");
            console.dir({ 'details': { 'error': err, 'jqXHR': jqXHR } });
        }

        try {
            if (errorHandler) {
                errorHandler(err, jqXHR);
            } else {
                MensagemController.erro("Error retrieving data. " + jqXHR.statusText + " - " + jqXHR.responseText);
            }
        } catch (e) { }
        return;
    }


    function handleResponse(jqXHR, textStatus, onSuccess, onError, onRetry) {
        var ret = null;
        var reterr = null;
        try {
            //error from jqXHR
            if (textStatus == "error") {
                var errmsg = $.msajax.defaultErrorMessage || "Error retreiving data.";

                //check for error response from the server
                if (jqXHR.status >= 300 && jqXHR.status < 600) {
                    return logError(jqXHR.statusText || msg, onError, jqXHR);
                }

                if (tries++ < 5) return onRetry();

                return logError(msg, onError, jqXHR);
            }

            //not an error response, reset try counter
            tries = 0;

            //check for a redirect from server (usually authentication token expiration).
            if (jqXHR.responseText.indexOf("|pageRedirect||") > 0) {
                location.href = decodeURIComponent(jqXHR.responseText.split("|pageRedirect||")[1].split("|")[0]).split('?')[0];
                return;
            }

            //parse response using ajax enabled parser (if available)
            ret = ((JSON && JSON.parseAjax) || $.parseJSON)(jqXHR.responseText);

            //invalid response
            if (!ret) throw jqXHR.responseText;

            // d property wrap as of .Net 3.5
            if (ret.d) ret = ret.d;

            //has an error
            reterr = (ret && (ret.error || ret.Error)) || null; //specifically returned an "error"

            if (ret && ret.ExceptionType) { //Microsoft Webservice Exception Response
                reterr = ret
            }

        } catch (err) {
            reterr = {
                'Message': $.msajax.defaultErrorMessage || "Error retreiving data."
              , 'debug': err
            }
        }

        //perform final logic outside try/catch, was catching error in onSuccess/onError callbacks
        if (reterr) {
            MensagemController.showAlerta("Ops, ocorreu um erro ao se comunicar com o servidor.");

            logError(reterr, onError, jqXHR);
            return;
        }

        MensagemController.hide();
        onSuccess(ret, jqXHR);
    }

}(jQuery));