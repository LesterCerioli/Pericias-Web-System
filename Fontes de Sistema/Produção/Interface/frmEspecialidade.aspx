<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master"
    CodeBehind="frmEspecialidade.aspx.vb" Inherits="Interface.frmEspecialidade" Title="Especialidades" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <table class="corpo-form" width="100%">
        <tr>
            <th>
                CADASTRO DA ESPECIALIDADE
            </th>
        </tr>
    </table>
    <table class="corpo-form" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                Profissão:
            </td>
            <td height="20">
                <cc1:DGTECDropDownList ID="CboProfissao" runat="server" AutoPostBack="True" 
                    Width="350px" Height="18px" />
            </td>
        </tr>
        <tr>
            <td height="20">
                Especialidade:
            </td>
            <td>
                <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" 
                    Width="350px" Height="18px" />
            </td>
        </tr>
        <tr>
            <td height="20">
                Descrição da Especialidade:
            </td>
            <td>
                <cc1:DGTECTextBox ID="txtDescr_Especialidade" runat="server" Width="350px"></cc1:DGTECTextBox>
            </td>
        </tr>
    </table>
    <table class="corpo-form" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td height="23">
                <cc1:DGTECLabel ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" />
            </td>
        </tr>
        <tr align="center">
            <td>
                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="60px" />&nbsp;<cc1:DGTECButton
                    ID="BtnExcluir" runat="server" Text="Excluir" Width="60px" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
