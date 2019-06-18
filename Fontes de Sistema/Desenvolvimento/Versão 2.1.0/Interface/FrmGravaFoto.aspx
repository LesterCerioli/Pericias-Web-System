<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmGravaFoto.aspx.vb" Inherits="Interface.frmGravaFoto" EnableSessionState="True" title="Gravar foto"%>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Gravar Foto</title>
    <link href="http://www.tjrj.jus.br/css/lay-out.css" rel="stylesheet" type="text/css" />
    <link href="http://www.tjrj.jus.br/css/form.css" rel="stylesheet" type="text/css" />
    <script src="JS/Jquery/jquery-3.0.0.min.js" type="text/javascript"></script>
    <script src="JS/Alertfy/alertify.min.js" type="text/javascript"></script>
    <script src="JS/Chosen/chosen.jquery.min.js" type="text/javascript"></script>
    <script src="JS/MaskedInput/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="JS/UI/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="JS/InicializaComponentes.js" type="text/javascript"></script>
</head>
<body topmargin="0" leftmargin="0" rightMargin="0">
    <form id="form1" runat="server" visible="True" >
    <asp:HiddenField runat="server" ID="hfCaminhoFoto" />
    <div style="text-align: center; height: 342px; width: 600px;">
        <br />
        &nbsp;<br />
        <asp:Image ID="FotoArq" runat="server" Height="160px" Width="120px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Image ID="FotoPerito" runat="server" Height="160px" Width="120px" />
        <br />
        <asp:Label ID="LblFotoArq" runat="server" Text="Foto Arquivada"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnGravar" runat="server" Text="Gravar Foto" />
        <br />
        <br />
        <cc1:DGTECFileUpload ID="FileUpload" runat="server" Width="431px" />
        <br />
        <br />
        <asp:Button ID="BtnCarrega" runat="server" Text="Enviar" />
        <br />
        <br />
        <asp:Label ID="lblErro" runat=server Text="" ForeColor="Red"></asp:Label>
    </div>
   </form>
</body>
</html>
