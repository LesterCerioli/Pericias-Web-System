<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master" CodeBehind="FrmOficioSubstituicao.aspx.vb" Inherits="Interface.FrmOficioSubstituicao" 
    title="Oficio de Substituição" %>
<%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div>
    <br />
    <br />
    Ofício de Substiuição de Perito em estudo<br />
        <br />
&nbsp;<cc1:DGTECLabel ID="LblNome" runat="server" Visible="False"></cc1:DGTECLabel><br />
    &nbsp;<cc1:DGTECLabel ID="LblProcesso" runat="server" Visible="False"></cc1:DGTECLabel>
    <br />
    <br />
    <br />
    </div>
    </form>
</asp:Content>
