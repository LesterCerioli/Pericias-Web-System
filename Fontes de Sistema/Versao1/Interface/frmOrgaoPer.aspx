<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.master"
    CodeBehind="frmOrgaoPer.aspx.vb" Inherits="Interface.frmOrgaoPer" Title="Orgão do Perito" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <table class="corpo-form" width="100%">
        <tr>
            <th>
                CADASTRO DO
                ÓRGÃO PROFISSIONAL</th>
        </tr>
    </table>
    <table class="corpo-form" width="100%" border="0" cellpadding="5" cellspacing="0">
        <tr>
            <td style="width: 79px">
                Órgão:
            </td>
            <td>
                <cc1:DGTECDropDownList ID="CboOrgaoPer" runat="server" AutoPostBack="True" Width="400px"
                    Height="18px" />
            </td>
        </tr>
        <tr>
            <td style="width: 79px">
                Nome do Órgão:
            </td>
            <td>
                <cc1:DGTECTextBox ID="txtDescr_Orgao_Per" runat="server" Width="400px"></cc1:DGTECTextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 45px">
                Sigla:
            </td>
            <td width="70">
                <cc1:DGTECTextBox ID="txtSigla_Orgao_Per" runat="server" Width="57px"></cc1:DGTECTextBox>
            </td>
        </tr>
    </table>
    <table class="corpo-form" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="25" height="25">
                <cc1:DGTECLabel ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" />
            </td>
        </tr>
        <tr align="center">
            <td>
                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="70px"></cc1:DGTECButton>
                &nbsp;&nbsp;
                <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="70px" OnClientClick="javascrit:return window.confirm('Tem certeza que deseja excluir o órgão profissional?');"></cc1:DGTECButton>
                &nbsp;&nbsp;
                <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="70px" />  
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
