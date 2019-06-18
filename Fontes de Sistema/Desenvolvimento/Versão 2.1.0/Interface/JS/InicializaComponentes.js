//JavaScript Document
//adiciona mascara de cnpj

$(document).ready(function () {
    InicializaFormatacoesMascarasValidacoes();
});


function InicializaFormatacoesMascarasValidacoes() {
    var tipo;
    timeId = 0;

    $(":input").each(function () {
        tipo = $(this).attr("tipoCampo");
        if (tipo == "cpf") {
            MascaraCPF(this);
        }
        if (tipo == "cnpj") {
            MascaraCNPJ(this);
        }
        if (tipo == "data") {
            MascaraData(this);
        }
        if (tipo == "moeda") {
            MascaraMoeda(this);
        }
        if (tipo == "cpf_cnpj") {
            $(this).keypress(function (e) {
                mascaraMutuario(this, cpfCnpj);
            });

            $(this).blur(function (e) {
                clearTimeout(timeId);
                return true;
            });
        }
        if (tipo == "processo_completo") {
            $(this).keypress(function (e) {
                mascaraMutuario(this, retornaNumeroProcessoFormatado);
            });

            $(this).blur(function (e) {
                clearTimeout(timeId);
                return true;
            });
        }
        if (tipo == "processo") {
            MascaraProcessoEspecifico(this);
        }

        LimitarTamanhosCaixaDeTexto(this);
    });

    $("select").each(function () {
        FormataCombos(this);
    });

    $("#btnInserirDocumento").click(function () {
        $("#openModal").css("opacity", "1");
        $("#openModal").css("pointer-events", "auto");
    });

    $("#btnCloseDoc").click(function () {
        $("#openModal").css("opacity", "0");
        $("#openModal").css("pointer-events", "none");
    });

    CarregaScripts();

}

function LimitarTamanhosCaixaDeTexto(campo) {
    var limiteCaracteres = $(campo).attr('MaximoCaracteres');
    if ((limiteCaracteres != undefined) && (limiteCaracteres > 0)) {
        $(campo).charCounter({ limit: limiteCaracteres });
    }
}

function MascaraProcessoEspecifico(campo) {
    var tipoProcesso = $(campo).attr("tipoProcesso");

    switch (tipoProcesso) {
        case "ADM":
            $(campo).mask("9999-999999");
            break;
        case "ANT":
            $(campo).mask("9999.999.999999-9");
            break;
        case "CNJ":
            $(campo).mask("9999999-99.9999.9.99.9999");
            break;
        case "OUTROS":
            $(campo).mask("9999.999.99999");
            break;
        default:
            return false;
    }
    
}

function MascaraMoeda(campo) {
    $(campo).maskMoney({ prefix: 'R$ ',
        showSymbol: true,
        thousands: '.',
        decimal: ',',
        symbolStay: true,
        allowNegative: false
    });
}

function MascaraCPF(campo) {
    $(campo).mask("999.999.999-99");

}

function MascaraCNPJ(campo) {
    $(campo).mask("99.999.999/9999-99");
}

function MascaraData(campo) {
    $(campo).mask("99/99/9999");

    $(campo).datepicker({
        changeMonth: true,
        changeYear: true,
        buttonImage: "Imagens/calendar_24x24.png",
        buttonImageOnly: true,
        showOn: "both",
        dateFormat: "dd/mm/yy",
        monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
        dayNames: ["Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado"],
        dayNamesMin: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
        dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"]
    });

    var tipoData = $(campo).attr("tipoData");

    if (tipoData == "data_nascimento") {
        $(campo).datepicker("option", "maxDate", "-1d");

        $(campo).change(function () {
            var dataDigitada = $(this).val();
            dataDigitada = new Date(dataDigitada.split("/")[2], dataDigitada.split("/")[1] - 1, dataDigitada.split("/")[0]);
            var dataAtual = $("input[id*='txtDataAtual']").val();
            dataAtual = new Date(dataAtual.split("/")[2], dataAtual.split("/")[1] - 1, dataAtual.split("/")[0]);

            if (dataDigitada >= dataAtual) {
                Mensagem("erro", "Erro", "A Data de nascimento não pode ser maior ou igual a data de hoje.");
                $(this).val("");
            }
        });
    } else {
        $(campo).datepicker("option", "gotoCurrent", "true");
    }

    //gotoCurrent: true,
}

function FormataCombos(campo) {
    $(campo).attr('data-placeholder', 'Selecione...');

    $(campo).chosen({
        no_results_text: "Nenhum registro encontrado!",
        allow_single_deselect: true
    });

}

// Função para carregar os scripts individuais de cada página.
function CarregaScripts() {
    var tituloPagina = document.title;

    switch (tituloPagina) {
        case "Cadastrar Perito":
            FuncoesCadastroPerito(tituloPagina);
            break;
        case "Anotações":
            FuncoesAnotacaoPerito(tituloPagina);
            break;
        case "Peritos para Nomeação":
            FuncoesPeritoParaNomeacao(tituloPagina);
            break;
        case "Situação Cadastral":
            FuncoesSituacaoCadastral(tituloPagina);
            break;
        case "Cadastro de Informações em Lote":
            FuncoesTelaLoteInfoPag(tituloPagina);
            break;
        case "Consulta de Pagamentos Realizados":
            FuncoesAnotacaoPerito(tituloPagina);
            break;
        default:
            return false;
    }
}

function FuncoesCadastroPerito(pagina) {
    SelecionaTipoPessoa(pagina);
}

function FuncoesAnotacaoPerito(pagina) {
    SelecionaTipoPessoa(pagina);
}

function FuncoesPeritoParaNomeacao(pagina) {
    SelecionaTipoPessoa(pagina);
}

function FuncoesSituacaoCadastral(pagina) {
    SelecionaTipoPessoa(pagina);
}

function FuncoesTelaLoteInfoPag(pagina) {
    SelecionaTipoPessoa(pagina);
}

function HabilitaComponentesPessoa() {
    var pessoa = $("select[id*='ddlTipoPessoa']").find(":selected").val();
    var txtCpf = $("input[id*='txtCPF']");
    var txtCnpj = $("input[id*='txtCNPJ']");
    var txtNasc = $("input[id*='txtDt_Nasc']");
    var linhaPJ = $("tr[id*='linhaPessoaJuridica']");

    switch (pessoa) {
        case "1":
            $(txtCpf).attr('disabled', false);
            $(txtCpf).attr('habilitado', true);
            $(txtCnpj).attr('disabled', true);
            $(txtCnpj).attr('habilitado', false);
            $(txtNasc).attr('disabled', false);

            $(txtCnpj).val("");

            //Desabilita preenchimento de nome fantasia.
            $(linhaPJ).hide();
            break;
        case "2":
            $(txtCpf).attr('disabled', true);
            $(txtCpf).attr('habilitado', false);
            $(txtCnpj).attr('disabled', false);
            $(txtCnpj).attr('habilitado', true);
            $(txtNasc).attr('disabled', true);

            $(txtCpf).val("");
            $(txtNasc).val("");

            //Habilita preenchimento de nome fantasia.
            $(linhaPJ).show();
            break;
        default:
            $(txtCpf).attr('disabled', true);
            $(txtCpf).attr('habilitado', false);
            $(txtCnpj).attr('disabled', true);
            $(txtCnpj).attr('habilitado', false);
            $(txtNasc).attr('disabled', true);

            $(txtNasc).val("");
            $(txtCnpj).val("");
            $(txtCpf).val("");
    }
}

function HabilitaComponentesBusca() {
    var pessoa = $("select[id*='ddlTipoPessoa']").find(":selected").val();
    var txtCpf = $("input[id*='txtCPF']");
    var txtCnpj = $("input[id*='txtCNPJ']");
    var linhaPJ = $("tr[id*='linhaPessoaJuridica']");

    switch (pessoa) {
        case "1":
            $(txtCpf).attr('disabled', false);
            $(txtCpf).attr('habilitado', true);
            $(txtCnpj).attr('disabled', true);
            $(txtCnpj).attr('habilitado', false);

            $(txtCnpj).val("");

            //Desabilita preenchimento de nome fantasia.
            $(linhaPJ).hide();
            break;
        case "2":
            $(txtCpf).attr('disabled', true);
            $(txtCpf).attr('habilitado', false);
            $(txtCnpj).attr('disabled', false);
            $(txtCnpj).attr('habilitado', true);

            $(txtCpf).val("");

            //Habilita preenchimento de nome fantasia.
            $(linhaPJ).show();
            break;
        default:
            $(txtCpf).attr('disabled', true);
            $(txtCpf).attr('habilitado', false);
            $(txtCnpj).attr('disabled', true);
            $(txtCnpj).attr('habilitado', false);

            $(txtCnpj).val("");
            $(txtCpf).val("");
    }
}

function HabilitaBotaoGravarTelaDeInfoLote() {
    var relacao = $("select[id*='ddlRelacaoPagamento']").find(":selected").val();
    var btnGravar = $("input[id*='BtnGravar']");
    if (relacao != undefined) {
        if (relacao != null) {
            if (relacao != "Selecione uma relação de pagamento...") {
                if (relacao != "0") {
                    $(btnGravar).removeAttr("disabled");
                } else {
                    $(btnGravar).attr("disabled", "disabled");
                }
            } else {
                $(btnGravar).attr("disabled", "disabled");
            }
        } else {
            $(btnGravar).attr("disabled", "disabled");
        }
    } else {
        $(btnGravar).attr("disabled", "disabled");
    }
}

function SelecionaTipoPessoa(pagina) {
    var ddlTipoPessoa = $("select[id*='ddlTipoPessoa']");
    var ddlRelacaoPagamento = $("select[id*='ddlRelacaoPagamento']")

    if (pagina == "Cadastrar Perito") {
        HabilitaComponentesPessoa();
    };

    if (pagina == "Anotações" || pagina == "Peritos para Nomeação" || pagina == "Situação Cadastral" || pagina == "Consulta de Pagamentos Realizados") {
        HabilitaComponentesBusca();
    };

    if (pagina == "Cadastro de Informações em Lote") {
        HabilitaBotaoGravarTelaDeInfoLote();
    };

    $(ddlTipoPessoa).change(function () {
        if (pagina == "Cadastrar Perito") {
            HabilitaComponentesPessoa();
        };

        if (pagina == "Anotações" || pagina == "Peritos para Nomeação" || pagina == "Situação Cadastral" || pagina == "Consulta de Pagamentos Realizados") {
            HabilitaComponentesBusca();
        };

    });

    $(ddlRelacaoPagamento).change(function () {
        if (pagina == "Cadastro de Informações em Lote") {
            HabilitaBotaoGravarTelaDeInfoLote();
        };
    });
}

function Mensagem(tipo, titulo, mensagem) {
    switch (tipo) {
        case "erro":
            alertify.alert(titulo, mensagem);
            break;
        default:
            alertify.alert(titulo, mensagem);
    }
}

function AbrirJanelaPopUp(pagina, parametros) {
    var p = parametros.split(",");

    for (i = 0; i < p.length; i++) {
        if (i == 0) {
            pagina = pagina + "?" + p[i];
        } else {
            pagina = pagina + "&" + p[i];
        }
    }

    window.open(pagina,
                "janela",
                "width=1000, height=700, top=100, left=110");
}

function AbrirPopUp(pagina, largura, altura, parametros, msg) {
    var p = parametros.split(",");

    for (i = 0; i < p.length; i++) {
        if (i == 0) {
            pagina = pagina + "?" + p[i];
        } else {
            pagina = pagina + "&" + p[i];
        }
    }

    window.open(pagina,
                "janela",
                "width=" + largura + ",height=" + altura + ",top=100, left=110");

    if (msg == "Sim") {
        if (confirm("Deseja realmente incluir a ajuda de custo?")) {
            __doPostBack('btnSim', '');
        } else {
            __doPostBack('btnNao', '');
        }
    }
}

function Expandir_Retrair_Detalhes_Grid(codigo, nomeControleImagem) {
    var imgCollapsable = $("img[id*='" + nomeControleImagem + "']");
    var estado = $(imgCollapsable).attr("estado");
    var linhaAnotacoes = $("tr[pai*='lnTbl" + codigo + "']");

    if (estado == "retraido") {
        $(imgCollapsable).attr("src", "Imagens/iconmonstr-arrow-70-24.png");
        $(imgCollapsable).attr("estado", "expandido");
        $(linhaAnotacoes).css("display", "");
    } else {
        $(imgCollapsable).attr("src", "Imagens/iconmonstr-arrow-69-24.png");
        $(imgCollapsable).attr("estado", "retraido");
        $(linhaAnotacoes).css("display", "none");
    }
}

function Expandir_Retrair_Detalhes_Geral(div, nomeControleImagem) {
    var divDados = $("div[id='" + div + "']");
    var imgCollapsable = $("img[id='" + nomeControleImagem + "']");
    var estado = $(imgCollapsable).attr("estado");

    if (estado == "retraido") {
        $(imgCollapsable).attr("src", "Imagens/iconmonstr-arrow-70-24.png");
        $(imgCollapsable).attr("estado", "expandido");
        $(divDados).css("display", "");
    } else {
        $(imgCollapsable).attr("src", "Imagens/iconmonstr-arrow-69-24.png");
        $(imgCollapsable).attr("estado", "retraido");
        $(divDados).css("display", "none");
    }
}

function Mudar_Imagem(nomeControleImagem, nomeControle) {
    var imgCollapsable = $("img[id*='" + nomeControleImagem + "']");
    var estado = $(imgCollapsable).attr("estado");

    if (estado == "retraido") {
        $(imgCollapsable).attr("src", "Imagens/iconmonstr-arrow-70-24.png");
        $(imgCollapsable).attr("estado", "expandido");
    } else {
        $(imgCollapsable).attr("src", "Imagens/iconmonstr-arrow-69-24.png");
        $(imgCollapsable).attr("estado", "retraido");
    }

    Retrair_Expandir_Texto_Componente(nomeControle);
}

function Retrair_Expandir_Texto_Componente(nomeControle) {
    var controle = $("p[id*='" + nomeControle + "']");
    var estado = $(controle).attr("estado");

    if (estado == "retraido") {
        $(controle).css("text-overflow", "clip");
        $(controle).css("white-space", "normal");
        $(controle).attr("estado", "expandido");
    } else {
        $(controle).css("text-overflow", "ellipsis");
        $(controle).css("white-space", "nowrap");
        $(controle).attr("estado", "retraido");
    }
}


/************
COPIADOS DA INTERNET 
***********/

/************
MASCARA DE CPF/CNPJ NO MESMO CAMPO
***********/

function mascaraMutuario(o, f) {
    v_obj = o;
    v_fun = f;
    /*setTimeout('execmascara()', 1);*/
    timeId = setTimeout('execmascara()', 1);
}

function execmascara() {
    v_obj.value = v_fun(v_obj.value);
}

function cpfCnpj(v) {

    //Remove tudo o que não é dígito
    v = v.replace(/\D/g, "");

    if (v.length <= 11) { //CPF

        //Coloca um ponto entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um ponto entre o terceiro e o quarto dígitos
        //de novo (para o segundo bloco de números)
        v = v.replace(/(\d{3})(\d)/, "$1.$2");

        //Coloca um hífen entre o terceiro e o quarto dígitos
        v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");

    } else { //CNPJ

        //Coloca ponto entre o segundo e o terceiro dígitos
        v = v.replace(/^(\d{2})(\d)/, "$1.$2");

        //Coloca ponto entre o quinto e o sexto dígitos
        v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");

        //Coloca uma barra entre o oitavo e o nono dígitos
        v = v.replace(/\.(\d{3})(\d)/, ".$1/$2");

        //Coloca um hífen depois do bloco de quatro dígitos
        v = v.replace(/(\d{4})(\d)/, "$1-$2");

    }

    return v;

}

function retornaNumeroProcessoFormatado(v) {
    v = v.replace(/\D/g, "");

    if (v.length <= 10) {
        v = v.replace(/^(\d{4})(\d)/, "$1-$2");
    } else if (v.length > 10 && v.length <= 12) {
        v = v.replace(/^(\d{4})(\d{3})(\d{5})$/, "$1.$2.$3");
    } else if (v.length > 12 && v.length <= 14) {
        v = v.replace(/^(\d{4})(\d{3})(\d{6})(\d{1})$/, "$1.$2.$3-$4");
    } else {
        v = v.replace(/^(\d{7})(\d{2})(\d{4})(\d{1})(\d{2})(\d{4})$/, "$1-$2.$3.$4.$5.$6");
    }

    return v;
}



//function ValidaEmail(email){
//    var exclude = /[^@-.w]|^[_@.-]|[._-]{2}|[@.]{2}|(@)[^@]*1/;
//    var check = /@[w-]+./;
//    var checkend = /.[a-zA-Z]{2,3}$/;
//    if( ((email.search(exclude) != -1) || (email.search(check)) == -1) || (email.search(checkend) == -1) ){
//        return false;
//    }
//    else {
//        return true;
//    }
//}


//function InicializaCampo(tipo, id) {
//    var obj = document.getElementById(id);

//    if (tipo == "cpf") {
//        MascaraCPF(obj);
//        ValidarCPF(obj);
//    }

//    if (tipo == "cnpj") {
//        MascaraCNPJ(obj);
//        ValidarCNPJ(obj);
//    }

//    if (tipo == "data") {
//        MascaraData(obj);
//        ValidaData(obj);
//    }

//    if (tipo == "cep") {
//        MascaraCep(obj);
//        ValidaCep(obj);
//    }

//    if (tipo == "telefone") {
//        MascaraTelefone(obj);
//        ValidaTelefone(obj);
//    }
//}

//function MascaraCNPJ(cnpj) {
//    if (mascaraInteiro(cnpj) == false) {
//        event.returnValue = false;
//    }
//    return formataCampo(cnpj, '00.000.000/0000-00', event);
//}

////adiciona mascara de cep
//function MascaraCep(cep) {
//    if (mascaraInteiro(cep) == false) {
//        event.returnValue = false;
//    }
//    return formataCampo(cep, '00.000-000', event);
//}

////adiciona mascara de data
//function MascaraData(data) {
//    if (mascaraInteiro(data) == false) {
//        event.returnValue = false;
//    }
//    return formataCampo(data, '00/00/0000', event);
//}

////adiciona mascara ao telefone
//function MascaraTelefone(tel) {
//    if (mascaraInteiro(tel) == false) {
//        event.returnValue = false;
//    }
//    return formataCampo(tel, '(00) 0000-0000', event);
//}

////adiciona mascara ao CPF
//function MascaraCPF(cpf) {
//    if (mascaraInteiro(cpf) == false) {
//        event.returnValue = false;
//    }
//    return formataCampo(cpf, '000.000.000-00', event);
//}

////valida telefone
//function ValidaTelefone(tel) {
//    exp = /\(\d{2}\)\ \d{4}\-\d{4}/
//    if (!exp.test(tel.value))
//        alert('Numero de Telefone Invalido!');
//}

////valida CEP
//function ValidaCep(cep) {
//    exp = /\d{2}\.\d{3}\-\d{3}/
//    if (!exp.test(cep.value))
//        alert('Numero de Cep Invalido!');
//}

////valida data
//function ValidaData(data) {
//    exp = /\d{2}\/\d{2}\/\d{4}/
//    if (!exp.test(data.value))
//        alert('Data Invalida!');
//}

////valida o CPF digitado
//function ValidarCPF(Objcpf) {
//    var cpf = document.getElementById(Objcpf).value;

//    if (cpf != undefined && cpf != null && cpf != ""){
//        exp = /\.|\-/g
//        cpf = cpf.toString().replace(exp, "");
//        var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
//        var soma1 = 0, soma2 = 0;
//        var vlr = 11;

//        for (i = 0; i < 9; i++) {
//            soma1 += eval(cpf.charAt(i) * (vlr - 1));
//            soma2 += eval(cpf.charAt(i) * vlr);
//            vlr--;
//        }
//        soma1 = (11 - (soma1 % 11)) % 10;  //(((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
//        soma2 += soma1 * 2; //(((soma2 + (2 * soma1)) * 10) % 11);
//        soma2 = (11 - (soma2 % 11)) % 10

//        var digitoGerado = eval(soma1.toString() + soma2.toString());
//        if (digitoGerado != digitoDigitado)
//            alert('CPF Invalido!');
//    }
//}

////valida numero inteiro com mascara
//function mascaraInteiro() {
//    if (event.keyCode < 48 || event.keyCode > 57) {
//        event.returnValue = false;
//        return false;
//    }
//    return true;
//}

////valida o CNPJ digitado
//function ValidarCNPJ(ObjCnpj) {
//    var cnpj = document.getElementById(ObjCnpj).value;

//    if (cnpj != undefined && cnpj != null && cnpj != "") {

//        var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
//        var dig1 = new Number;
//        var dig2 = new Number;

//        exp = /\.|\-|\//g
//        cnpj = cnpj.toString().replace(exp, "");
//        var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

//        for (i = 0; i < valida.length; i++) {
//            dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
//            dig2 += cnpj.charAt(i) * valida[i];
//        }
//        dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
//        dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

//        if (((dig1 * 10) + dig2) != digito)
//            alert('CNPJ Invalido!');

//    }
//}

////formata de forma generica os campos
//function formataCampo(campo, Mascara, evento) {
//    var boleanoMascara;

//    var Digitato = evento.keyCode;
//    exp = /\-|\.|\/|\(|\)|  /g
//    campoSoNumeros = campo.value.toString().replace(exp, "");

//    var posicaoCampo = 0;
//    var NovoValorCampo = "";
//    var TamanhoMascara = campoSoNumeros.length; ;

//    if (Digitato != 8) { // backspace 
//        for (i = 0; i <= TamanhoMascara; i++) {
//            boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
//                                || (Mascara.charAt(i) == "/"))
//            boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
//                                || (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
//            if (boleanoMascara) {
//                NovoValorCampo += Mascara.charAt(i);
//                TamanhoMascara++;
//            } else {
//                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
//                posicaoCampo++;
//            }
//        }
//        campo.value = NovoValorCampo;
//        return true;
//    } else {
//        return true;
//    }
//} 