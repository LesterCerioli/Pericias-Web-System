<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master"
    CodeBehind="frmAnotacoes.aspx.vb" Inherits="Interface.frmAnotacoes" Title="Anotações" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div style="width: 524px">
        <table class="corpo-form">
    <tr>
    <th align="center">
                    DADOS PESSOAIS
    </th>
    </tr>
    <tr>
    <td>
    <!--
    <table border="0" cellpadding="10" width="520">
        <tr>
            <td align="center">
                DADOS PESSOAIS
            </td>
        </tr>
    </table>
    !-->
    <table border="0" cellpadding="10" width="520">
        <tr>
            <td style="text-align: center;">
                CPF&nbsp;
            </td>
            <td width="100" align="center">
                &nbsp;<br />
                <cc1:DGTECTextBox ID="txtCPF" runat="server" AutoPostBack="True" Height="17px"></cc1:DGTECTextBox>
                <br />
                <asp:RequiredFieldValidator ID="ValidarCPF" runat="server" ControlToValidate="txtCPF"
                    ErrorMessage="Preencher CPF">Preencher CPF</asp:RequiredFieldValidator>
            </td>
            <td width="60">
                Nome<br />
                <br />
                <cc1:DGTECLabel ID="lblNomesSemelhantes" runat="server">Nomes Semelhantes</cc1:DGTECLabel>
                &nbsp;</td>
            <td width="290">
                <cc1:DGTECTextBox ID="txtNome" runat="server" AutoPostBack="True" Width="220px" Height="17px"></cc1:DGTECTextBox>
                &nbsp;&nbsp;
                <cc1:DGTECTextBox ID="txtID_PF" runat="server" Height="17px" Width="10px" Visible="False"></cc1:DGTECTextBox>
                <br />
                <asp:RequiredFieldValidator ID="ValidarNome" runat="server" ControlToValidate="txtNome"
                    ErrorMessage="Preencher o Nome">Preencher o Nome</asp:RequiredFieldValidator>
                <br />
                <cc1:DGTECDropDownList ID="CboPerito" runat="server" AutoPostBack="True" Height="17px"
                    Width="220px">
                </cc1:DGTECDropDownList>
                &nbsp;&nbsp;
                <cc1:DGTECTextBox ID="txtCod_Perito" runat="server" Visible="False" Height="17px"
                    Width="10px"></cc1:DGTECTextBox>
                <br />
            </td>
            <td>
                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="50px" />
                <br />
                <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="50px" />
                <br />
                <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="50px" />
                <br />
                <cc1:DGTECButton ID="BtnSair" runat="server" Text="Sair" Width="50px" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="10" width="520">
        <tr>
            <td width="40">
                Orgão             </td>
            <td width="60">
                <cc1:DGTECDropDownList ID="CboOrgao_Per" runat="server" Width="200px" AutoPostBack="True"
                    Height="17px">
                </cc1:DGTECDropDownList>
            </td>
            <td width="70">
                Número Reg.
            </td>
            <td width="60">
                <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Height="17px" Width="70px"></cc1:DGTECTextBox>
            </td>
            <td>
                Data Anotação
            </td>
            <td>
               
                <cc1:DGTECLabel ID="lblData_Anotacao" runat="server"></cc1:DGTECLabel>
               
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="10" width="520">
        <tr>
            <td style="width: 100px">
                Tipo de Anotação : 
                </td>
                <td colspan="2" width="250">
                <cc1:DGTECDropDownList ID="cboTipo_anotacao" runat="server" Height="18px" 
                    Width="180px">
                </cc1:DGTECDropDownList>
            </td>
            <td width="100">
                <cc1:DGTECButton ID="BtnNova" runat="server" Text="Nova Anotação" 
                    Width="100px" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="80px" Width="488px" 
                    TextMode="MultiLine"></cc1:DGTECTextBox>
            </td>
        </tr>
        <tr>
        <td colspan="4" width="520">
                <cc1:DGTECGridView ID="GrdAnotacoes" runat="server" Width="410px">
                    <Columns>
                        <asp:CommandField SelectText="Exibir" ShowSelectButton="True" />
                    </Columns>
            </cc1:DGTECGridView>
            </td>
        </tr>
    </table>
    </td>
    </tr>
    </table>
    </div>
    </form>
</asp:Content>
