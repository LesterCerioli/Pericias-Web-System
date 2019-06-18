<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmAnotacaoDCP.aspx.vb"
    Inherits="Interface.frmAnotacaoDCP" Title="Anotação" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="http://www.tjrj.jus.br/css/lay-out.css" rel="stylesheet" type="text/css" />
    <link href="http://www.tjrj.jus.br/css/form.css" rel="stylesheet" type="text/css" />
</head>
<body topmargin="0" leftmargin="0" rightMargin="0">
    <form id="form1" runat="server">
    <table class="corpo-form" style="height: 357px; width: 506px">
        <tr >
            <th >
                <center>ANOTAÇÃO</center> 
            </th>
        </tr>
        <tr>
            <td>
                <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="281px" Width="506px" 
                    TextMode="MultiLine"></cc1:DGTECTextBox>
            </td>
        </tr>
        <tr align="center">
            <td>
                <cc1:DGTECButton ID="BtnEnviar" runat="server" Text="Enviar para DIPEJ" CssClass="Button-padrao" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
