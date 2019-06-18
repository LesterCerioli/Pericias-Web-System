<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PERMasterPagePerito.Master" CodeBehind="frmProfissao.aspx.vb" Inherits="Interface.frmProfissao" 
    title="Profissão" %>
  <%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">

    <form id="form1" runat="server">
    &nbsp;<table class="corpo-form">
        <tr>
            <td style="border-style: groove; width: 505px">
                <br />
                Profissao:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:DGTECDropDownList ID="CboProfissao" runat="server" AutoPostBack="True" Width="250px">
                </cc1:DGTECDropDownList>
                <br />
                <br />
                Descrição da Profissao:&nbsp;&nbsp;&nbsp; &nbsp;
                <cc1:DGTECTextBox ID="txtDescr_Profissao" runat="server" Width="250px"></cc1:DGTECTextBox>
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

