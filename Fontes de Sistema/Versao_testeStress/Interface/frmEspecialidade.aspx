<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PERMasterPagePerito.Master" CodeBehind="frmEspecialidade.aspx.vb" Inherits="Interface.frmEspecialidade" 
    title="Especialidades" %>
  <%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">

    <form id="form1" runat="server">
    &nbsp;<table class="corpo-form">
        <tr>
            <td style="border-style: groove; width: 505px">
                <br />
                Profissão:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;              
                <cc1:DGTECDropDownList ID="CboProfissao" runat="server" AutoPostBack="True" Width="250px">
                </cc1:DGTECDropDownList>
                <br />
                <br />
                Especialidade:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                 <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" Width="250px">
                </cc1:DGTECDropDownList>
                <br />
                <br />
                Descrição da Especialidade:&nbsp;&nbsp;&nbsp; &nbsp;                 <cc1:DGTECTextBox ID="txtDescr_Especialidade" runat="server" Width="250px"></cc1:DGTECTextBox>
                <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    <cc1:DGTECButton ID="BtnExcluir" runat="server" 
                    Text="Excluir" />
                                <br />
                <br />
                                </td>
                                
                            </tr>
                            </table>

    </form>

</asp:Content>

