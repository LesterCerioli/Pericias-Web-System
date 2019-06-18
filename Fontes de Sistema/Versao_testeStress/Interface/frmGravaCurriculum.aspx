<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmGravaCurriculum.aspx.vb" Inherits="Interface.frmGravaCurriculum" title="Grava Curriculum"%>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Gravar Curriculum Vitae</title>
</head>
<body>
    <form id="form1" runat="server" visible="True">

    <div style="height: 150px; width: 300px; background-color: #FFFFCC;">
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <cc1:DGTECFileUpload ID="FileUpload" runat="server" Width="244px" />
        <br />
        <br />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnGravar" runat="server" Text="Gravar Curriculum" />
        &nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;
        <br />
        <br />
    </div>
   </form>
</body>
</html>
