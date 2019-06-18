<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmExibirFoto.aspx.vb" Inherits="Interface.frmExibirFoto" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Exibir Foto</title>
    <style type="text/css">
        #form1
        {
            width: 465px;
        }
        #FrmExibirFoto
        {
            width: 316px;
            height: 437px;
        }
    </style>
</head>
<body>
    <form id="FrmExibirFoto" runat="server" visible="True">
        <asp:Image ID="Foto" runat="server" Height="400px" Width="300px" />
        <br />
        <br />
   </form>
</body>
</html>
