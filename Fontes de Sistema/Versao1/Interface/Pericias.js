// JScript File
function Atualiza(nomeControle, valor) {
    document.getElementById(nomeControle).value = valor;
}

function AtribuiValor(nomeControle) {
    var valor = document.getElementById('upLoad').value;

    alert(nomeControle);

    document.getElementById(nomeControle).valor;
}

function ExibeAjudaTabela(nomeBLL, nomeColCod, nomeColDes, nomeControle, nomeTabela) {
    var altura = 480;
    var largura = 520;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;
    var pagina = ''

    /*if (typeof Magistrado == 'undefined')
    {
    Magistrado = 'F';
    pagina = 'frmAjudaTabela.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela + '&Magistrado' + Magistrado;
    }
    else {
    Magistrado = 'V';
    */
    pagina = 'frmAjudaTabela.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela;
    /*   } */

    if (window.showModalDialog) {
        window.showModalDialog(pagina, window, 'dialogHeight: ' + altura + 'px; dialogwidth: ' + largura + 'px; center:yes; status:no; scroll:no;');
    }
    else {
        window.open(pagina, window, 'scrollbars=no,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
    }
}

function ExibeAjudaTabelaTipoMovimento(nomeBLL, nomeColCod, nomeColDes, nomeControle, nomeTabela, nomeMetodo) {
    var altura = 480;
    var largura = 520;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;
    var pagina = ''

    pagina = 'frmAjudaTabela.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela + '&nomeMetodo=' + nomeMetodo;

    if (window.showModalDialog) {
        window.showModalDialog(pagina, window, 'dialogHeight: ' + altura + 'px; dialogwidth: ' + largura + 'px; center:yes; status:no; scroll:no;');
    }
    else {
        window.open(pagina, window, 'scrollbars=no,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
    }
}

function ExibeAjudaTabelaOrgaoHierarquica(nomeBLL, nomeColCod, nomeColDes, nomeControle, nomeTabela, metodo, codOrgao) {
    var altura = 480;
    var largura = 520;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;
    var pagina = ''

    pagina = 'frmAjudaTabelaOrgaoHierarquica.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela + '&nomeMetodo=' + metodo + '&codOrgao=' + codOrgao;

    if (window.showModalDialog) {
        window.showModalDialog(pagina, window, 'dialogHeight: ' + altura + 'px; dialogwidth: ' + largura + 'px; center:yes; status:no; scroll:no;');
    }
    else {
        window.open(pagina, window, 'scrollbars=no,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
    }
}

function ExibeAjudaTabelaPorParametro(nomeBLL, nomeColCod, nomeColDes, nomeControle, nomeTabela, nomeMetodo, paramCol, paramVal) {
    var altura = 480;
    var largura = 520;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;
    var pagina = ''

    pagina = 'frmAjudaTabela.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela + '&nomeMetodo=' + nomeMetodo + '&paramCol=' + paramCol + '&paramVal=' + paramVal;

    if (window.showModalDialog) {
        window.showModalDialog(pagina, window, 'dialogHeight: ' + altura + 'px; dialogwidth: ' + largura + 'px; center:yes; status:no; scroll:no;');
    }
    else {
        window.open(pagina, window, 'scrollbars=no,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
    }
}

function ExibeAjudaTabelaFunc(nomeBLL, nomeColCod, nomeColDes, nomeControle, nomeTabela, Magistrado) {
    var altura = 480;
    var largura = 520;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;
    var pagina = ''


    if (typeof Magistrado == 'undefined') {
        Magistrado = 'F';
        //   pagina = 'frmAjudaTabelaFunc.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela + '&Magistrado' + Magistrado;
    }
    // else {
    //   Magistrado = 'V';
    //  pagina = 'frmAjudaTabelaFunc.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela + '&Magistrado' + Magistrado;
    //   }

    pagina = 'frmAjudaTabelaFunc.aspx?nomeBLL=' + nomeBLL + '&nomeColCod=' + nomeColCod + '&nomeColDes=' + nomeColDes + '&nomeControle=' + nomeControle + '&nomeTabela=' + nomeTabela + '&Magistrado=' + Magistrado;

    if (window.showModalDialog) {
        window.showModalDialog(pagina, window, 'dialogHeight: ' + altura + 'px; dialogwidth: ' + largura + 'px; center:yes; status:no; scroll:no;');
    }
    else {
        window.open(pagina, window, 'scrollbars=no,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
    }
}


function GEDView(nomeControle) {
    var altura = 600;
    var largura = 800;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;
    var url = document.getElementById(nomeControle).value;

    if (url != '') {
        window.open(url, 'Janela', 'scrollbars=no,directories=no,menubar=no,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
    }
}

function ExibeGed(nomeControleProt, nomeControleGED) {
    var altura = 210;
    var largura = 580;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;

    var numProt = document.getElementById(nomeControleProt).value;

    var pagina = 'frmGEDDocumento.aspx?numProt=' + numProt + '&nomeControle=' + nomeControleGED;

    window.open(pagina, 'Janela', 'scrollbars=no,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
}
/*
function ExibeJanela(url) {
    var altura = 600;
    var largura = 800;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;

    //  window.open(url,'Janela','directories=no,menubar=no,modal=yes,toolbar=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo ); //

    if (window.showModalDialog) {
        window.showModalDialog(url, window, 'dialogHeight: ' + altura + 'px; dialogwidth: ' + largura + 'px; center:yes; ');
    } else {
        window.open(url, window, 'directories=no,menubar=no,modal=yes,toolbar=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
    }

}
*/
function ExibeJanelaRelatorio(url) {
    var altura = 600;
    var largura = 800;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;

    window.open(url, 'Janela', 'directories=no,menubar=no,toolbar=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
}

function ExibePersonagem(nomeControle) {
    // var altura = 250;
    var altura = 600;
    var largura = 700;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;

    var numProt = document.getElementById(nomeControle).value;

    var pagina = 'frmCadastroDadosIniciaisPers.aspx?numProt=' + numProt;

    if (numProt != '') {
        if (window.showModalDialog) {
            window.showModalDialog(pagina, window, 'dialogHeight: ' + altura + 'px; dialogwidth: ' + largura + 'px; center:yes; status:no; scroll:no;');
            //window.open(pagina,'Janela','directories=no,menubar=no,modal=yes,toolbar=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo );
        } else {
            window.open(pagina, window, 'scrollbars=no,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
        }
    }
}

function ExibeVogais() {
    // var altura = 250;
    var altura = 600;
    var largura = 700;
    var esquerda = (screen.width - largura) / 2;
    var topo = (screen.height - altura) / 2;

    var pagina = 'frmVogais.aspx';

    window.open(pagina, 'teste', 'scrollbars=yes,directories=no,menubar=no,resizable=no,modal=yes,toolbar=no,status=no,width=' + largura + ',height=' + altura + ',left=' + esquerda + ',top=' + topo);
}

// Funções para Trabalhar com Check Box nas DataGridViews

function ChangeCheckBoxState(id, checkState) {
    var cb = document.getElementById(id);
    if (cb != null)
        cb.checked = checkState;
}

function ChangeAllCheckBoxStates(checkState) {
    // Toggles through all of the checkboxes defined in the CheckBoxIDs array
    // and updates their value to the checkState input parameter
    if (CheckBoxIDs != null) {
        for (var i = 0; i < CheckBoxIDs.length; i++)
            ChangeCheckBoxState(CheckBoxIDs[i], checkState);
    }
}

function CheckHeader(id) {
    var cb = document.getElementById(id);
    if (cb != null) {
        ChangeAllCheckBoxStates(cb.checked);
    }
}

//function formataCPF(nomeControle, e) {
//      var code;
//      if (!e) var e = window.event;
//      
//      if (e.keyCode) code = e.keyCode;
//      else if (e.which) code = e.which;
//      
//      var character = String.fromCharCode(code);

//      if(code==8) {
//      }    
//      else {           
//        if(code > 47 && code < 58) { 
//            var conteudo;
//            conteudo = document.getElementById(nomeControle).value;
//            if(conteudo.length==3) {
//                conteudo = conteudo + '.';
//                document.getElementById(nomeControle).value = conteudo;
//            }
//            else if(conteudo.length==7) {
//                conteudo = conteudo + '.';
//                document.getElementById(nomeControle).value = conteudo;  
//            }
//            else if(conteudo.length==11) {
//                conteudo = conteudo + '-';
//                document.getElementById(nomeControle).value = conteudo;  
//            } 
//        }
//        else {
//          event.keyCode = 0;
//        }            
//     }
//}

function formataNumero(nomeControle, e) {
    var code;
    if (!e) var e = window.event;
    if (e.keyCode) {
        code = e.keyCode;
    }
    else if (e.which) {
        code = e.which;
    }

    var character = String.fromCharCode(code);

    if (code == 8) {
    }
    else {
        if (code > 47 && code < 58) {
            var conteudo;
            conteudo = document.getElementById(nomeControle).value;
        }
        else {
            event.keyCode = 0;
        }
    }
}

//function formataNumero(nomeControle, e) {
//    var code;

//    if (!e) var e = window.event;
//    if (e.keyCode) {
//        code = e.keyCode;
//    }
//    else if (e.which) {
//        code = e.which;
//    }

//    //var character = String.fromCharCode(code);

//    if ((e.keyCode == 9) || ((e.keyCode == 10) || (e.keyCode = 13))) {
//        alert(e.keyCode);
//    }
//    else {
//        if (code > 47 && code < 58) {
//            var conteudo;
//            conteudo = document.getElementById(nomeControle).value;
//        }
//        else {
//            event.keyCode = 0;
//        }
//    }
//}

/*
function formataProtocolo(nomeControle) {
    // Função utilizada no controle ctlProtocolo
    var valor = "";
    var tamanho;
    var zeros = "0000000";
    var hoje = new Date();

    valor = document.getElementById(nomeControle).value;

    tamanho = valor.length;

    if (tamanho > 0) {
        if (tamanho < 5) {
            valor = hoje.getFullYear() + zeros.substring(1, zeros.length - valor.length) + valor;
        }
        if (tamanho < 11) {
            valor = valor.substring(0, 4) + zeros.substring(0, zeros.length - (valor.length - 4)) + valor.substring(4, valor.length);
        }
    }

    document.getElementById(nomeControle).value = valor;
}
*/

/*function validacpf(Controle,NomeControle){ */
 
function validacpf(NomeControle) {
var i; 
/*var Contr = '<%='+Controle+'.ClientID %>'*/
if (NomeControle=='') 
   {
   return true;
   }
   if (isNaN(NomeControle))
      {
   return true;
   }
var s = document.getElementById(NomeControle).value;
/* s = Controle  */
var c = s.substr(0,9); 

var dv = s.substr(9,2); 

var d1 = 0; 
if (s != '') {
for (i = 0; i < 9; i++) 
{ 
d1 += c.charAt(i)*(10-i); 
} 
if (d1 == 0){ 
alert("CPF Invalido") 
document.getElementById(NomeControle).value = ''
//document.getElementById("<%=txtCPF.ClientID%>").value = ''
return false; 
} 
d1 = 11 - (d1 % 11); 
if (d1 > 9) d1 = 0; 
if (dv.charAt(0) != d1) 
{ 
alert("CPF Invalido") 
document.getElementById(NomeControle).value = ''
//document.getElementById("<%=txtCPF.ClientID%>").value = ''
return false; 
} 
d1 *= 2; 
for (i = 0; i < 9; i++) 
{ 
d1 += c.charAt(i)*(11-i); 
} 
d1 = 11 - (d1 % 11); 
if (d1 > 9) d1 = 0; 
if (dv.charAt(1) != d1) 
{ 
alert("CPF Invalido") 
document.getElementById(NomeControle).value = ''
//document.getElementById("<%=txtCPF.ClientID%>").value = ''
return false; 
}  
{
if (s=='00000000000' || s=='11111111111' ||  s=='22222222222' ||  s=='33333333333' ||  s=='44444444444' ||  s=='55555555555' ||  s=='66666666666' ||  s=='77777777777' ||  s=='88888888888' ||  s=='99999999999')
{
alert("CPF Invalido") 
document.getElementById(NomeControle).value = ''
//document.getElementById(NomeControle).value=''
return false;
}
return true; 
} 
}
return true;
}

function VerificaBranco(Controle){ 
  
var i; 
s = Controle 
if (s = '') 
{ 
alert("Preencher o Campo") 
return false; 
} 
{
return true; 
} 
}

//-----------------------------------------------------
//Funcao: MascaraMoeda
//Sinopse: Mascara de preenchimento de moeda
//Parametro:
//   objTextBox : Objeto (TextBox)
//   SeparadorMilesimo : Caracter separador de milésimos
//   SeparadorDecimal : Caracter separador de decimais
//   e : Evento
//Retorno: Booleano
//Autor: Gabriel Fróes - www.codigofonte.com.br
//-----------------------------------------------------
function MascaraMoeda(objTextBox, SeparadorMilesimo, SeparadorDecimal, e) {
    var selected_text = "";
    if (document.selection) { selected_text = document.selection.createRange().text; }
    else { selected_text = objTextBox.value.substring(objTextBox.selectionStart, objTextBox.selectionEnd); }
    if (selected_text) {
        // alert('tem selecao');
        objTextBox.value = objTextBox.value.replace(selected_text, "");
        objTextBox.focus();
        objTextBox.select();
    }

    if (objTextBox.value.length > 15) {
        alert('Tamanho máximo de valor excedido.');
        return false;
    }
    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13) return true;
    key = String.fromCharCode(whichCode); // Valor para o código da Chave
    if (strCheck.indexOf(key) == -1) return false; // Chave inválida
    len = objTextBox.value.length;
    for (i = 0; i < len; i++)
        if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
    aux = '';
    for (; i < len; i++)
        if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1) aux += objTextBox.value.charAt(i);
    aux += key;
    len = aux.length;
    if (len == 0) objTextBox.value = '';
    if (len == 1) objTextBox.value = '0' + SeparadorDecimal + '0' + aux;
    if (len == 2) objTextBox.value = '0' + SeparadorDecimal + aux;
    if (len > 2) {
        aux2 = '';
        for (j = 0, i = len - 3; i >= 0; i--) {
            if (j == 3) {
                aux2 += SeparadorMilesimo;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }
        objTextBox.value = '';
        len2 = aux2.length;
        for (i = len2 - 1; i >= 0; i--)
            objTextBox.value += aux2.charAt(i);
        objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
    }
    return false;
}


function Ambiente() {
    var Mensagem;
    Mensagem = window.location.hostname;
    //   alert(Mensagem); 
    document.getElementById('lblAmbiente').innerText = null;
    if (Mensagem == 'localhost')
        (
     document.getElementById('lblAmbiente').innerText = 'Desenvolvimento'
   );
    return true
}
//-------------------------------------------------------------------------
/*
function IsDate(NomeControle) {
    var s;
    //s = document.ctlData.value; 
    s = document.getElementById(nomeControle).value;
    var Dia = s.substr(1, 2);
    var Mes = s.substr(3, 2);
    var Ano = s.trim(substr(5, 4));
    var d1 = new date();
    alert("Dia" + Dia + " - Mes " + Mes + " - Ano ")
    var minhaData = new Date(s);
    var RETORNO = true
    RETORNO = iif(Mes == NaN, false, true);
    if (RETORNO = false) {
        alert("Data Invalida 1");
        return false
    }
    RETORNO = iif(number(Mes) < 1 | Number(Mes) > 12, false, true);
    if (RETORNO = false) {
        alert("Data Invalida 2");
        return false
    }
    RETORNO = iif(number(Dia) < 1 | Number(Dia) > 31, false, true);
    if (RETORNO = false) {
        alert("Data Invalida 3");
        return false
    }
    RETORNO = iif(Ano == NaN, false, true);
    if (RETORNO = false) {
        alert("Data Invalida 4");
        return false
    }
    d1 = Dia + "/" + Mes + "/" + Ano;
    alert("Data:" & d1)
    if (d1 = NaN) return false;
    return true
}
*/
//-------------------------------------------------------------------------


/*
//function formataCPF(nomeControle, e) {
//      var code;
//      if (!e) var e = window.event;
//      
//      if (e.keyCode) code = e.keyCode;
//      else if (e.which) code = e.which;
//      
//      var character = String.fromCharCode(code);

//      if(code==8) {
//      }    
//      else {           
//        if(code > 47 && code < 58) { 
//            var conteudo;
//            conteudo = document.getElementById(nomeControle).value;
//            if(conteudo.length==3) {
//                conteudo = conteudo + '.';
//                document.getElementById(nomeControle).value = conteudo;
//            }
//            else if(conteudo.length==7) {
//                conteudo = conteudo + '.';
//                document.getElementById(nomeControle).value = conteudo;  
//            }
//            else if(conteudo.length==11) {
//                conteudo = conteudo + '-';
//                document.getElementById(nomeControle).value = conteudo;  
//            } 
//        }
//        else {
//          event.keyCode = 0;
//        }            
//     }
//}
*/
//evento onkeyup
function maskCPF(CPF) {
var evt = window.event;
kcode=evt.keyCode;
if (kcode == 8) return;
if (CPF.value.length == 3) { CPF.value = CPF.value + '.'; }
if (CPF.value.length == 7) { CPF.value = CPF.value + '.'; }
if (CPF.value.length == 11) { CPF.value = CPF.value + '-'; }
}

// evento onblur
function formataCPF(CPF)
{
with (CPF)
{
value = value.substr(0, 3) + '.' +
value.substr(3, 3) + '.' +
value.substr(6, 3) + '-' +
value.substr(9, 2);
}
}
function retiraFormatacao(CPF)
{
with (CPF)
{
value = value.replace (".","");
value = value.replace (".","");
value = value.replace ("-","");
value = value.replace ("/","");
}
}

    function printDiv1(NomeDIV) 
{ 
  var divToPrint=document.getElementById(NomeDiv); 
  var newWin=window.open('','Print-Window','width=500,height=600'); 
  newWin.document.open(); 
  newWin.document.write('<html><body onload="window.print()">'+divToPrint.innerHTML+'</body></html>'); 
  newWin.document.close(); 
  setTimeout(function(){newWin.close();},10); 
}

function printdiv(printpage)
{
var headstr = "<html><head><title></title></head><body>";
var footstr = "</body>";
var newstr = document.all.item(printpage).innerHTML;
var oldstr = document.body.innerHTML;
document.body.innerHTML = headstr+newstr+footstr;
window.print(); 
document.body.innerHTML = oldstr;
return false;
} 
function printPartOfPage(elementId)
{
 var printContent = document.getElementById(elementId);
 var windowUrl = 'about:blank';
 var uniqueName = new Date();
 var windowName = 'Print' + uniqueName.getTime();
 var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');

 printWindow.document.write(printContent.innerHTML);
 printWindow.document.close();
 printWindow.focus();
 printWindow.print();
 printWindow.close();

 /*location.reload(document.location.href); */
 /*location.reload(true)*/
 document.getElementById("loading99").style.visibility='hidden'; 

}


/* Controle de Data.
Public Sub TxtCtlData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCtlData.TextChanged
        'Public Event TitleChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim DataTeste As String
        Dim DataHoje As Date
        Dim DiaHoje As String
        Dim MesHoje As String
        Dim AnoHoje As String
        'Formato Final dd/mm/aaaa
        DataTeste = txtCtlData.Text
        DataHoje = Today()
        If DataTeste.Length = 5 Then
            DataTeste = DataTeste + "/" + (DataHoje.Year).ToString
        End If
        DiaHoje = (DataHoje.Day).ToString
        MesHoje = (DataHoje.Month).ToString
        If Len(DiaHoje) = 1 Then
            DiaHoje = "0" + DiaHoje
        End If
        If Len(MesHoje) = 1 Then
            MesHoje = "0" + MesHoje
        End If
        AnoHoje = (DataHoje.Year).ToString
        If UCase(txtCtlData.Text) = "H" Then
            txtCtlData.Text = DiaHoje + "/" + MesHoje + "/" + AnoHoje
            Exit Sub
        End If
        Select Case Len(DataTeste)
            Case 1
                DataTeste = "0" + DataTeste + "/" + MesHoje + "/" + AnoHoje
            Case 2
                DataTeste = DataTeste + "/" + MesHoje + "/" + AnoHoje
            Case 3
                DataTeste = "0" + Mid(DataTeste, 1, 1) + "/" + Mid(DataTeste, 2, 2) + "/" + AnoHoje
            Case 4
                DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 3, 2) + "/" + AnoHoje
            Case 5
                If Val(Mid(DataTeste, 4, 2)) < 50 Then
                    DataTeste = "0" + Mid(DataTeste, 1, 2) + "/0" + Mid(DataTeste, 2, 2) + "/20" + Mid(DataTeste, 4, 2)
                Else
                    DataTeste = "0" + Mid(DataTeste, 1, 2) + "/0" + Mid(DataTeste, 2, 2) + "/19" + Mid(DataTeste, 4, 2)
                End If
            Case 6
                If Val(Mid(DataTeste, 5, 2)) < 50 Then
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 3, 2) + "/20" + Mid(DataTeste, 5, 2)
                Else
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 3, 2) + "/19" + Mid(DataTeste, 5, 2)
                End If
            Case 8
                If Val(Mid(DataTeste, 7, 2)) < 50 Then
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 4, 2) + "/20" + Mid(DataTeste, 7, 2)
                Else
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 4, 2) + "/19" + Mid(DataTeste, 7, 2)
                End If
        End Select
        txtCtlData.Text = DataTeste
        Try
            If txtCtlData.Text <> "" Then
                If Not IsDate(CDate(txtCtlData.Text)) Then
                    MsgErroData()
                    txtCtlData.Text = ""
                    Exit Sub
                End If
            End If
        Catch
            MsgErroData()
            txtCtlData.Text = ""
            Exit Sub
        End Try
        txtCtlData.Text = FormatarData(txtCtlData.Text)
        RaiseEvent TextChanged(sender, e)
        'RaiseEvent TextChanged()
    End Sub
    Public Property Data() As String
        Get
            If Not (txtCtlData.Text) Is Nothing Then
                Return txtCtlData.Text
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            txtCtlData.Text = value
        End Set
    End Property

    Function FormatarData(ByVal NewDate As String) As String

        Dim Dia As String
        Dim Mes As String
        Dim Ano As String
        Dia = Mid(NewDate, 1, 2)
        Mes = Mid(NewDate, 4, 2)
        Ano = Trim(Mid(NewDate, 7, 4))
        If Mes.Length = 1 Then
            Mes = "0" + Mes
        End If
        If Dia.Length = 1 Then
            Dia = "0" + Dia
        End If
        If Ano.Length = 2 Then
            If Val(Ano) > 30 Then
                Ano = "19" + Ano
            Else
                Ano = "20" + Ano
            End If
        End If
        Return (Dia + "/" + Mes + "/" + Ano)
    End Function
    


    Public Sub MsgErroData()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "erro", "alert('Data Inválida');", true)
    End Sub

*/

function FormatarProc(NomeControle)
{
with (NomeControle)
var Conteudo = "";
Conteudo = document.getElementById(NomeControle).value;
{
if (Conteudo.length<14)
Conteudo = document.getElementById(NomeControle).value.substr(0, 4) + '.' +
document.getElementById(NomeControle).value.substr(4, 3) + '.' +
document.getElementById(NomeControle).value.substr(7, 6) + '-' +
document.getElementById(NomeControle).value.substr(13, 1);
document.getElementById(NomeControle).value=Conteudo;
return m_Num_Proc;
}
}

function retiraFormataProc(m_Num_Proc)
{
with (m_Num_Proc)
{
m_Num_Proc = m_Num_Proc.replace (".","");
m_Num_Proc = m_Num_Proc.replace (".","");
m_Num_Proc = m_Num_Proc.replace ("-","");
m_Num_Proc = m_Num_Proc.replace ("/","");
return m_Num_Proc;
}
}

function validarProc(m_Num_Proc)
{
with (m_Num_Proc_CNJ)
{
if (!isNaN(m_Num_Proc_CNJ) ^ m_Num_Proc_CNJ.length>0)
{
alert("Somente serão aceitos valores numéricos!")
window.focus(this)
 
};
{
if ((m_Num_Proc_CNJ.length != 14) ^ (m_Num_Proc_CNJ.length != 0))
{ 
alert("Número de Processo Inválido(js)!")
return false;
};
{
return true;
}
}
}
}
/* 0000017-85.2009.8.19.9000 */

/*
function FormatarProcCNJ(m_Num_Proc_CNJ)
{
with (m_Num_Proc_CNJ)
{
value = value.substr(0, 7) + '-' +
value.substr(7, 2) + '.' +
value.substr(9, 4) + '.' +
value.substr(13, 1) + '.' +
value.substr(14, 2) + '.' +
value.substr(16, 4)
return value;
}
}
*/

function FormatarProcCNJ1(NomeControle)
{
with (NomeControle)
var Conteudo = "";
Conteudo = document.getElementById(NomeControle).value;
{
if (Conteudo.length<14)
{
if (Conteudo.length==12)
{
    document.getElementById(NomeControle).value = '0' + document.getElementById(NomeControle).value
};
if (Conteudo.length==11)
{
    document.getElementById(NomeControle).value = '00' + document.getElementById(NomeControle).value
};
if (Conteudo.length==10)
{
    document.getElementById(NomeControle).value = '000' + document.getElementById(NomeControle).value
};
if (Conteudo.length==9)
{
    document.getElementById(NomeControle).value = '0000' + document.getElementById(NomeControle).value
};
if (Conteudo.length==8)
{
    document.getElementById(NomeControle).value = '00000' + document.getElementById(NomeControle).value
};
if (Conteudo.length==7)
{
    document.getElementById(NomeControle).value = '000000' + document.getElementById(NomeControle).value
};
Conteudo = document.getElementById(NomeControle).value.substr(0, 7) + '-' +
document.getElementById(NomeControle).value.substr(7, 2) + '.' +
document.getElementById(NomeControle).value.substr(9, 4) ;
document.getElementById(NomeControle).value = Conteudo;
}; // fim do primeiro if
return Conteudo;
}
}

function validarProcCNJ(m_Num_Proc_CNJ)
{
with (m_Num_Proc_CNJ)
{
if (!isNaN(m_Num_Proc_CNJ) ^ m_Num_Proc_CNJ.length>0)
{
alert("Somente serão aceitos valores numéricos!")
window.focus(this)
 
};
{
if ((m_Num_Proc_CNJ.length != 20) ^ (m_Num_Proc_CNJ.length != 0))
{ 
alert("Número de Processo Inválido(js)!")
return false;
};
{
return true;
}
}
}
}

/*
Confirmar
result = window.confirm("Deseja exibir o dia e a hora atuais?");
if (result) {
	window.alert(new Date());
}

Tempo em excesso
var comando = "alert('Já passou muito tempo!');"
intervalo = window.setTimeout(comando, 5000);


Elemento da tela
oElement = document.createElement("<input name='Titulo'>");
alert(document.getElementsByName("Titulo").length);


Cookies
if (navigator.cookieEnabled)
	alert("Seu navegador aceita cookies clientes");
else
	alert("Seu navegador não aceita cookies clientes");
Resolução
alert("Sua resolução horizontal é " + screen.width + " pixels.");


alert("Após clicar em OK, você será redirecionado.");
location.assign("http://www.fomezero.org.br"); 
ou
alert("Após clicar em OK, você será redirecionado.");
location.reload("http://www.fomezero.org.br");


Recarregar a página
{location.reload(true);}

Javascript Versus HTML

<input type="text" onkeypress="return pergunta();">
<script language="javascript">
	function pergunta() {
	if (confirm("Deseja realmente inserir o caractere digitado?"))
			return true;
	else
			return false;
	}
</script>

Caixa Alta
<input type="text" onkeypress="return caixaAlta();">
<script language="javascript">
	function caixaAlta() {
		codigoLetraDigitada = window.event.keyCode; 
		letraDigitada = String.fromCharCode(codigoLetraDigitada);
		letraMaiuscula = letraDigitada.toUpperCase();
		codigoLetraMaiuscula = letraMaiuscula.charCodeAt(0);
		window.event.keyCode = codigoLetraMaiuscula;
	}
</script>

*/


/*
 function validardata(NomeControle){ 
     var datateste = document.getElementById(NomeControle).value;
     var DataHoje = "";
     var DiaHoje = "";
     var MesHoje = "";
     var AnoHoje = "";
     var DataFinal = "";
    
   
     if (datateste.length == 10 && datateste.substring(3,2) == '/' && datateste.substring(6,5) == '/') 
         {
           if (IsDate(datateste))
           {
           if (isNaN(datateste.substring(1, 0)) || isNaN(datateste.substring(2, 1)) || datateste.substring(3, 2)  != '/' || isNaN(datateste.substring(4, 3)) || isNaN(datateste.substring(5, 4)) || datateste.substring(6, 5) != '/'  || isNaN(datateste.substring(7, 6)) || isNaN(datateste.substring(8, 7)) || isNaN(datateste.substring(9, 8)) || isNaN(datateste.substring(10, 9))) 
               {
               alert("Existem caracteres não aceitos num campo data. Data Inválida..");
               return false
               } 
           document.getElementById(NomeControle).value = datateste.toString();
           return true
           }
           else
           {
               alert('A Data ' + datateste.toString() + ' é inválida..');
               document.getElementById(NomeControle).value = ' ';
           return false
           }
         }
          
     //Formato Final dd/mm/aaaa
     var Data1 = new Date();
       
     DiaHoje = Data1.getDate();
     MesHoje = Data1.getMonth()+1;
     AnoHoje = Data1.getYear();
     if (DiaHoje.toString().length == 1)
        {
        (DiaHoje = '0' + DiaHoje)
        }
     if (MesHoje.toString().length == 1)
        {
        (MesHoje = '0' + MesHoje)
         }
     DataHoje = DiaHoje + '/' + MesHoje + '/' + AnoHoje;
       
     if (datateste.substring(0, 1) == 'H' || datateste.substring(0, 1) == 'h')
        {
         document.getElementById(NomeControle).value = DataHoje;
         return true;
        }

     if (datateste.length == 5  && datateste.substring(3, 2) == '/')
        {
        (datateste = datateste + '/' + AnoHoje)
        }


        switch (datateste.length) 
        {
        case 1:
            {
            (datateste = '0' + datateste + '/' + MesHoje + '/' + AnoHoje);
            break;
            }
        case 2:
            {
            (datateste = datateste + '/' + MesHoje + '/' + AnoHoje);
            break;
            }
        case 3:
            {
            alert('A Data ' + datateste.toString() + ' é inválida...');
            break;
            }
        case 4:
            {
            (datateste = datateste.substring(0, 2) + '/' + datateste.substring(4, 2) + '/' + AnoHoje);
             break;
            }
        case 5:
            {
                if (datateste.substring(3, 2) !== '/') 
                  {
                    alert('A Data ' + datateste.toString() + ' é inválida....');
                    return false;
                  };
                if (number(datateste.substring(6, 4)) < 20) 
                    {
                    (datateste = datateste.subtring(1, 2) + '/' + datateste.subtring(2, 2) + '/20' + datateste.subtring(4, 2))
                    break;    
                    }
                else
                    {
                    (datateste = datateste.subtring(1, 2) + '/' + datateste.subtring(2, 2) + '/19' + datateste.subtring(4, 2))
                    break;    
                    }
             }
        case 6:
            {
                if (datateste.substring(1, 0) == '/' || datateste.substring(2, 1) == '/' || datateste.substring(3, 2) == '/' || datateste.substring(4, 3) == '/' || datateste.substring(5, 4) == '/' || datateste.substring(6, 5) == '/')
                  {
                    alert('A Data ' + datateste.toString() +  ' não é aceita..');
                    return false
                  };
                if (Number(datateste.substring(6, 4)) < 20)
                  {
                   (datateste = datateste.substring(0, 2) + '/' + datateste.substring(4,2) + '/20' + datateste.substring(6, 4))
                   break;
                  }   
                else
                  {
                   (datateste = datateste.substring(0, 2) + '/' + datateste.substring(4,2) + '/19' + datateste.substring(6, 4))
                   break;
                  }
            }                    
        case 8:
            {
              if (datateste.substring(3, 2) == '/' && datateste.substring(6, 5) == '/') 
                {
                if (Number(datateste.substring(8, 6)) < 20)
                   {
                   (datateste = datateste.substring(0, 2) + '/' + datateste.substring(5, 3) + '/20' + datateste.substring(8, 6))
                   }
                else
                   {
                   (datateste = datateste.substring(0, 2) + '/' + datateste.substring(5, 3) + '/19' + datateste.substring(8, 6))
                   } 
                 }
               else
                   {
                    datateste = datateste.substring(0, 2) + '/' + datateste.substring(4, 2) + '/' + datateste.substring(8, 4)
                   }
               break;
            }
        }        

        if (datateste.length != 10) 
           {return false}
          
        if (IsDate(datateste))
           {
           if (isNaN(datateste.substring(1, 0)) || isNaN(datateste.substring(2, 1)) || datateste.substring(3, 2)  != '/' || isNaN(datateste.substring(4, 3)) || isNaN(datateste.substring(5, 4)) || datateste.substring(6, 5) != '/'  || isNaN(datateste.substring(7, 6)) || isNaN(datateste.substring(8, 7)) || isNaN(datateste.substring(9, 8)) || isNaN(datateste.substring(10, 9))) 
               {
               alert("Existem caracteres não aceitos no campo data. Data Inválida...");
               return false
               } 
           document.getElementById(NomeControle).value = datateste.toString();
           return true
           }
           else
           {
           alert('A Data ' + datateste.toString() + ' é inválida.....');
           return false
           }
}
*/

function validardata(NomeControle) {
    var datateste = document.getElementById(NomeControle).value;
    var DataHoje = "";
    var DiaHoje = "";
    var MesHoje = "";
    var AnoHoje = "";
    var DataFinal = "";

    if (datateste == '  /  /    ')
    { return true }
    if (datateste == '  /  /  ')
    { return true }
    if (datateste == '__/__/____')
    { return true }

    if (datateste.length == 10 && datateste.substring(3, 2) == '/' && datateste.substring(6, 5) == '/') {
        if (IsDate(datateste)) {
            if (isNaN(datateste.substring(1, 0)) || isNaN(datateste.substring(2, 1)) || datateste.substring(3, 2) != '/' || isNaN(datateste.substring(4, 3)) || isNaN(datateste.substring(5, 4)) || datateste.substring(6, 5) != '/' || isNaN(datateste.substring(7, 6)) || isNaN(datateste.substring(8, 7)) || isNaN(datateste.substring(9, 8)) || isNaN(datateste.substring(10, 9))) {
                alert("Existem caracteres não aceitos num campo data. Data Inválida..");
                return false
            }
            document.getElementById(NomeControle).value = datateste.toString();
            return true
        }
        else {
            alert('A Data ' + datateste.toString() + ' é inválida..');
            document.getElementById(NomeControle).value = ' ';
            return false
        }
    }

    //Formato Final dd/mm/aaaa
    var Data1 = new Date();

    DiaHoje = Data1.getDate();
    MesHoje = Data1.getMonth() + 1;
    AnoHoje = Data1.getYear();
    if (DiaHoje.toString().length == 1) {
        (DiaHoje = '0' + DiaHoje)
    }
    if (MesHoje.toString().length == 1) {
        (MesHoje = '0' + MesHoje)
    }
    DataHoje = DiaHoje + '/' + MesHoje + '/' + AnoHoje;

    if (datateste.substring(0, 1) == 'H' || datateste.substring(0, 1) == 'h') {
        document.getElementById(NomeControle).value = DataHoje;
        return true;
    }

    if (datateste.length == 5 && datateste.substring(3, 2) == '/') {
        (datateste = datateste + '/' + AnoHoje)
    }


    switch (datateste.length) {
        case 1:
            {
                (datateste = '0' + datateste + '/' + MesHoje + '/' + AnoHoje);
                break;
            }
        case 2:
            {
                (datateste = datateste + '/' + MesHoje + '/' + AnoHoje);
                break;
            }
        case 3:
            {
                alert('A Data ' + datateste.toString() + ' é inválida...');
                break;
            }
        case 4:
            {
                (datateste = datateste.substring(0, 2) + '/' + datateste.substring(4, 2) + '/' + AnoHoje);
                break;
            }
        case 5:
            {
                if (datateste.substring(3, 2) !== '/') {
                    alert('A Data ' + datateste.toString() + ' é inválida....');
                    return false;
                };
                if (number(datateste.substring(6, 4)) < 20) {
                    (datateste = datateste.subtring(1, 2) + '/' + datateste.subtring(2, 2) + '/20' + datateste.subtring(4, 2))
                    break;
                }
                else {
                    (datateste = datateste.subtring(1, 2) + '/' + datateste.subtring(2, 2) + '/19' + datateste.subtring(4, 2))
                    break;
                }
            }
        case 6:
            {
                if (datateste.substring(1, 0) == '/' || datateste.substring(2, 1) == '/' || datateste.substring(3, 2) == '/' || datateste.substring(4, 3) == '/' || datateste.substring(5, 4) == '/' || datateste.substring(6, 5) == '/') {
                    alert('A Data ' + datateste.toString() + ' não é aceita..');
                    return false
                };
                if (Number(datateste.substring(6, 4)) < 20) {
                    (datateste = datateste.substring(0, 2) + '/' + datateste.substring(4, 2) + '/20' + datateste.substring(6, 4))
                    break;
                }
                else {
                    (datateste = datateste.substring(0, 2) + '/' + datateste.substring(4, 2) + '/19' + datateste.substring(6, 4))
                    break;
                }
            }
        case 8:
            {
                if (datateste.substring(3, 2) == '/' && datateste.substring(6, 5) == '/') {
                    if (Number(datateste.substring(8, 6)) < 20) {
                        (datateste = datateste.substring(0, 2) + '/' + datateste.substring(5, 3) + '/20' + datateste.substring(8, 6))
                    }
                    else {
                        (datateste = datateste.substring(0, 2) + '/' + datateste.substring(5, 3) + '/19' + datateste.substring(8, 6))
                    }
                }
                else {
                    datateste = datateste.substring(0, 2) + '/' + datateste.substring(4, 2) + '/' + datateste.substring(8, 4)
                }
                break;
            }
    }

    if (datateste.length != 10)
    { return false }

    if (IsDate(datateste)) {
        if (isNaN(datateste.substring(1, 0)) || isNaN(datateste.substring(2, 1)) || datateste.substring(3, 2) != '/' || isNaN(datateste.substring(4, 3)) || isNaN(datateste.substring(5, 4)) || datateste.substring(6, 5) != '/' || isNaN(datateste.substring(7, 6)) || isNaN(datateste.substring(8, 7)) || isNaN(datateste.substring(9, 8)) || isNaN(datateste.substring(10, 9))) {
            alert("Existem caracteres não aceitos no campo data. Data Inválida...");
            return false
        }
        document.getElementById(NomeControle).value = datateste.toString();
        return true
    }
    else {
        alert('A Data ' + datateste.toString() + ' é inválida.....');
        return false
    }
}

function IsDate(DataNew) {

    var Dia = DataNew.substring(2, 0);
    var Mes = DataNew.substring(5, 3);
    var Ano = DataNew.substring(10, 6);
    if (isNaN(Mes))
       {
        return false
        }
     if (Number(Mes) < 1 || Number(Mes) > 12)
       {
        return false
        }
    if (Number(Dia) < 1 || Number(Dia) > 31)
       {
        return false
        }
    if (Number(Dia) > 29 && Number(Mes) == 2)
       {
        return false
        }
    if (Number(Dia) > 30 && Number(Mes) == 4)
       {
        return false
        }
    if (Number(Dia) > 30 && Number(Mes) == 6)
       {
        return false
        }
    if (Number(Dia) > 30 && Number(Mes) == 9)
       {
        return false
        }
    if (Number(Dia) > 30 && Number(Mes) == 11)
       {
        return false
        }
    if (Number(Ano)%4 !== 0 && Number(Mes) == 2 && Number(Dia) > 28)
       {
        return false
        }
    if (isNaN(Ano))
       {
        return false
        }
    else
       {   
       return true
       }
}

        
/*
function FormatarData(m_Data) {
    // Função utilizada no controle ctlProtocolo
    // var valor = document.getElementById(nomeControle).value;
    var Dia = m_Data.substring(0, 2);
    var Mes = m_Data.substring(5, 2);
    var Ano = m_Data.substring(8, 4);
    
        if (Mes.length == 1)
            (Mes = "0" + Mes)
        if (Dia.length == 1)
            (Dia = "0" + Dia)
        if (Ano.length == 2)
           {
            if (number(Ano) > 30)
                {
                (Ano = "19" + Ano);
                }
            else
                {
                (Ano = "20" + Ano);
                }
            }
        m_Data = (Dia + '/' + Mes + '/' + Ano);
        
        return m_Data
        }
*/

  /*              
        switch (datateste.length) 
        {
        case 1:
            {
            (datateste = '0' + datateste + '/' + MesHoje + '/' + AnoHoje);
            break;
            }
        case 2:
            {
            (datateste = datateste + '/' + MesHoje + '/' + AnoHoje);
            break;
            }
        case 3:
            {
            alert('Data Inválida');
            break;
            }
        case 4:
            {
            (datateste = datateste.substring(0, 2) + '/' + datateste.substring(4, 2) + '/' + AnoHoje);
             break;
            }
        case 5:
            {
                if (datateste.tostring.substring(3, 1) != '/') 
                {
                if (number(datateste.substr(4, 2)) < 20) 
                    {
                    (datateste = '0' + datateste.subtr(1, 2) + '/0' + datateste.subtr(2, 2) + '/20' + datateste.subtr(4, 2))
                    }
                else
                    {
                    (datateste = '0' + datateste.subtr(1, 2) + '/0' + datateste.subtr(2, 2) + '/19' + datateste.subtr(4, 2))
                    }
                }
             break;
             }
        case 6:
            {
                {
                if (Number(datateste.subr(5, 2)) < 20)
                   (datateste = datateste.subtring(0, 2) + '/' + datateste.subtring(4,2) + '/20' + datateste.subtring(5, 2))
                else
                   (datateste = datateste.subtring(0, 2) + '/' + datateste.subtring(4,2) + '/19' + datateste.subtr(5, 2))
                }
                break;
            }                    
        case 8:
            {
              if (datateste.tostring.substring(2, 1) == '/' && datateste.tostring.substring(5, 1) == '/') 
                {
                if (Number(datateste.subtr(7, 2)) < 20)
                   {
                   (datateste = datateste.subtring(0, 2) + '/' + datateste.subtring(3, 2) + '/20' + datateste.subtring(6, 2))
                   }
                else
                   {
                   (datateste = datateste.subtring(0, 2) + '/' + datateste.substring(3, 2) + '/19' + datateste.subtring(6, 2))
                   } 
                 }
               else
                   {
                    datateste = datateste.subtring(0, 2) + '/' + datateste.substring(3, 2) + '/' + datateste.subtring(5, 4)
                    }
               break;
            }
        }        
        // var DataFinal = new Date(); 
 
        // DataFinal = datateste.
        //DataFinal = FormatarData(datateste);
        if (datateste.length != 10) 
           {return false}
          
        //document.getElementById(nomeControle).value = DataFinal;
        document.getElementById(NomeControle).value = datateste.toString();
        //document.getElementById("<%=txtDt_Nasc.ClientID%>").value = '01/01/1900' 
        //datateste.toString()  /* DataFinal.format('d/m/Y').toString() */
        // return true

        // DataHoje = Data1.getDate() + "/" + (Data1.getMonth()+1) + "/" + Data1.getFullYear();
        //AnoHoje = (DataHoje.Year).ToString


function changeMe(NomeControle)
{
    var aForm;
    var Retorno;
    //changeMe('" & txtNome_Perito.ClientID & "');"
    //aForm = FrmPeritoDCP.aspx.elements;
    var myObject = new Object();
    myObject.txtNome_Perito = 'JULIO';
    Retorno = '';
    //aForm.txtNome_Perito.value;
	// The object "myObject" is sent to the modal window.
	//Retorno = window.showModalDialog('frmEscolherPerito.aspx', NomeControle, 'dialogWidth:50; dialogHeight:10; status:0; help:0');
    //Retorno = window.showModalDialeog('frmEscolherPerito.aspx', myObject, 'dialogWidth:50; dialogHeight:10; status:0; help:0');
    //myObject.txtNome_Perito 
    Retorno = window.showModalDialog('frmEscolherPerito.aspx', Retorno, 'dialogWidth:500px; dialogHeight:250px; center:yes; status:0; help:0');
	//var answer = window.showModalDialog("subDoc.html",argsVariable, "dialogWidth:300px; dialogHeight:200px; center:yes");
	//document.getElementById(NomeControle).value = Retorno;
	//document.getElementById(NomeControle).value = window.dialogArguments;
	document.getElementById(NomeControle).value = 'teste' 
	//document.getElementById(ctl00$Tela$txtNome_Perito.ClientID).value = 'teste' 
	//closeMe;
	//document.getElementById("<%=txtNome_Perito.ClientID%>").value = 'teste';
	//var LblDate1 = document.getElementById('<%=lblSelectedDate.ClientID%>');
	//document.getElementById('txtNome_Perito').innerText = 'teste';
	//myObject.txtNome_Perito
	//myObject.dialogArguments;
	
	//vVar =  myObject.dialogArguments

	return true;
}

function closeMe(Valor)
{
	window.returnValue = Valor;
	event.returnValue = false;
	window.close();
}

function textboxMultilineMaxNumber(e, txt, MaxLen) {
    var count;
    count = txt.value.length;
    if (e.keyCode == 8) {
        count = count - 1;
        //alert(txt.value.length);
    }

    if (count > (MaxLen - 1)) {
        alert("Limite de " + MaxLen + " caracteres.");
        return false;
    }
}

function somenteNumeros(obj, ev) {
    ek = ev.keyCode;
    if (ek != 8 && ek != 37 && ek != 38 && ek != 39 && ek != 40 && ek != 16 && ek != 46) {
        if (!(ek >= 48 && ek <= 57) && !(ek >= 96 && ek <= 105)) {
            var aux = obj.value;
            aux = aux.replace(/\D/g, "");
            obj.value = aux;
        }
    }
}

/*
<script language="JavaScript" for="window" event="onLoad">
	Name.value = window.dialogArguments;
</script>
<script language="JavaScript">
function closeMe()
{
	window.returnValue = '558085';
	event.returnValue = false;
	window.close();
}
</script>
*/