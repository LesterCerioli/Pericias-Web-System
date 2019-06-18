<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmAnotacaoDCP.aspx.vb" Inherits="Interface.frmAnotacaoDCP" title="Anotação"%>

<%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <p align="center" style="font-size: x-large; width: 399px;">
        <br />
        Anotação
    </p>
    <form id="form1" runat="server">
    <p style="height: 402px; width: 402px">
        <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="400px" 
            Width="400px" TextMode="MultiLine"></cc1:DGTECTextBox>
    </p>
    <p align="center" style="height: 23px; width: 400px">
        <cc1:DGTECButton ID="BtnEnviar" runat="server" Text="Enviar para DIPEJ" />
    </p>
    </form>
</body>
</html>
