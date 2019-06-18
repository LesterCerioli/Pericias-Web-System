<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.master" CodeBehind="frmOrgaoPer.aspx.vb" Inherits="Interface.frmOrgaoPer" title="Orgão do Perito" %>
<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    
    &nbsp;<table class="corpo-form">
        <tr>
            <td style="border-style: groove; width: 480px">
                <br />
                Orgão:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DGTECDropDownList ID="CboOrgaoPer" runat="server" AutoPostBack="True" Width="170px">
                </cc1:DGTECDropDownList>
                <br />
                <br />
                Nome do Órgão:&nbsp;&nbsp;&nbsp;
                <cc1:DGTECTextBox ID="txtDescr_Orgao_Per" runat="server" Width="167px"></cc1:DGTECTextBox>
                <br />
                <br />
                UF:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DGTECDropDownList ID="CboUf" runat="server" AutoPostBack="True" 
                    Width="64px" Height="16px">
                </cc1:DGTECDropDownList>
                &nbsp;&nbsp;
                <br />
                <br />
                Sigla:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DGTECTextBox ID="txtSigla_Orgao_Per" runat="server" Width="57px"></cc1:DGTECTextBox>
                <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                &nbsp;&nbsp;
                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="70px"></cc1:DGTECButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="64px"></cc1:DGTECButton>
                <br />
                <br />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
