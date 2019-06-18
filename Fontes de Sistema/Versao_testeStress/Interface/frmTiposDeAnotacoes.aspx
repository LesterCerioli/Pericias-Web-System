<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master" CodeBehind="frmTiposDeAnotacoes.aspx.vb" Inherits="Interface.frmTiposDeAnotacoes" 
    title="Tipos de Anotações" %>
    
     <%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">

    <form id="form1" runat="server">
    &nbsp;<table class="corpo-form">
        <tr>
            <td style="border-style: groove; width: 505px">
                <br />
                Tipos de Anotações:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DGTECDropDownList ID="CboTipo_Anotacao" runat="server" AutoPostBack="True" 
                    Width="250px">
                </cc1:DGTECDropDownList>
                <br />
                <br />
                Descrição:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DGTECTextBox ID="txtDescr_Tipo_Anotacao" runat="server" Width="250px"></cc1:DGTECTextBox>
                <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <cc1:DGTECButton ID="BtnExcluir" runat="server" 
                    Text="Excluir" />
                                <br />
                <br />
                                </td>
                                
                            </tr>
                            </table>

    </form>

</asp:Content>

