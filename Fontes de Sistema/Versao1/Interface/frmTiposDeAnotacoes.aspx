<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master" 
CodeBehind="frmTiposDeAnotacoes.aspx.vb" Inherits="Interface.frmTiposDeAnotacoes" Title="Tipos de Anotações" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <table class="corpo-form" width="100%">
        <tr>
            <th>
                CADASTRO DO
                TIPO DE ANOTAÇÃO</th>
        </tr>
    </table>
    <table class="corpo-form" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                Tipos de Anotações:
            </td>
            <td>
                <cc1:DGTECDropDownList ID="CboTipo_Anotacao" runat="server" AutoPostBack="True" 
                    Width="350px" Height="18px">
                </cc1:DGTECDropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Descrição:
            </td>
            <td>
                <cc1:DGTECTextBox ID="txtDescr_Tipo_Anotacao" runat="server" Width="350px"></cc1:DGTECTextBox>
            </td>
        </tr>
    </table>
    <table class="corpo-form" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td height="23"><cc1:DGTECLabel ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true"/></td>
        </tr>
        <tr align="center">
            <td xml:lang="70">
                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="70px" />
                &nbsp;&nbsp;
                <cc1:DGTECButton ID="BtnExcluir" Enabled="false" runat="server" Text="Excluir" Width="70px"  OnClick="BtnExcluir_Click" OnClientClick="javascrit:return window.confirm('Tem certeza que deseja excluir o tipo de anotação?');" />
                &nbsp;&nbsp;
                <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="70px" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
