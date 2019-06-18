<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="erro.aspx.vb" Inherits="Interface.erro" %>
<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
</script>

<asp>

<html>

<head>

<link href="CSS/body.css" rel="stylesheet" type="text/css" />
<link href="CSS/form.css" rel="stylesheet" type="text/css" />
<link href="CSS/lay-out.css" rel="stylesheet" type="text/css" />
<link href="CSS/textos-img.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            height: 25px;
            width: 744px;
        }
        .style2
        {
            height: 84px;
            width: 744px;
        }
    </style>
</head>

<body>
<div id="tudo-intranet">

<div id="cabecalho" >
<%--  <table width="100%" border="0" cellspacing="5" cellpadding="0" >
   
    <tr >
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
  </table>--%>
</div>
<div id="navegador-horiz3">
    <h1>
        Perícias
    </h1>
</div>
<div id="container">
  
<div id="content100">
<br />
<form id="Form1" runat="server">


<table class="corpo-form">
    
<tr>
            <th class="style1">
                <cc1:DGTECImage ID="imgMensagem" runat="server" ImageUrl="imagens/img_erro.gif" />
                <cc1:DGTECLabel ID="lblTitulo" runat="server"></cc1:DGTECLabel>
            </th>
            </tr>
            <tr>
                <td class="style2">
                    <br />
                    <cc1:DGTECLabel ID="lblMensagem" runat="server" Width="721px" Height="16px"></cc1:DGTECLabel><br />
                    <br />
                    <br />
                    <cc1:DGTECLabel ID="lblDetalhes" runat="server" Font-Bold="True"
                        Width="336px">Detalhe:</cc1:DGTECLabel><br />
                    <cc1:DGTECLabel ID="lblExcecao" runat="server" Width="722px" Height="16px"></cc1:DGTECLabel>
                </td>
            </tr>
            <tr>
                <td  align="center"> 
                    <cc1:DGTECButton id="btnInicio" runat="server" Text="Inicio" /> 
                </td>
            </tr>
</table>


</form>
</div>
<div id="clear">
</div>
</div>
   
<div id="rodape">
        PALACIO DA JUSTICA DO ESTADO DO RIO DE JANEIRO - FORUM CENTRAL<br />
        Av. Erasmo Braga, 115 - Centro / CEP: 20020-903 - Rua Dom Manuel, 29, Centro / CEP:
        20010-090 / Tel.: (0xx21) 3133-2000
</div>
</div> 
</body>
</html>
</asp>
