<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master"
    CodeBehind="frmProfissao.aspx.vb" Inherits="Interface.frmProfissao" Title="Profissão" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <table class="corpo-form" width="100%">
        <tr>
            <th>
                CADASTRO DA PROFISSÃO
            </th>
        </tr>
    </table>
    <table class="corpo-form" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td height="20">
                Profissão:
            </td>
            <td>
                <cc1:DGTECDropDownList ID="CboProfissao" runat="server" AutoPostBack="True" 
                    Width="350px" Height="18px" />
            </td>
        </tr>
        <tr>
            <td height="20">
                Descrição da Profissão:
            </td>
            <td>
                <cc1:DGTECTextBox ID="txtDescr_Profissao" runat="server" Width="350px" />
            </td>
        </tr>
    </table>
    <table class="corpo-form" width="100%" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td height="23">
                <cc1:DGTECLabel ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" />
            </td>
        </tr>
        <tr align="center">
            <td>
                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="60px" />
                &nbsp;
                <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="60px" />
                                &nbsp;
                <cc1:DGTECButton ID="BtnReativar" runat="server" Text="Reativar" Width="60px" 
                    Visible=false/>
            &nbsp;</td>
        </tr>
    </table>
    </form>
</asp:Content>
