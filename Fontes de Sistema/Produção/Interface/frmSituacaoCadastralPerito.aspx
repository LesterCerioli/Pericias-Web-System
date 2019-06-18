<%@ Page Title="Situação Cadastral" Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.Master"
    CodeBehind="frmSituacaoCadastralPerito.aspx.vb" Inherits="Interface.frmSituacaoCadastralPerito" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePartialRendering="True">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table class="corpo-form" style="width: 100%;" align="center">
                <tr>
                    <th align="center">
                        SITUAÇÃO CADASTRAL DO PERITO
                    </th>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
                        <div class="w3-border w3-round-xlarge" style="height: 130px; width: 650px; padding-left: 10px;">
                            <br />
                            <table>
                                <tr style="height: 50px">
                                    <td>
                                        <asp:Label ID="lblTipoPessoa" runat="server" Text="Tipo de Pessoa:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoPessoa" runat="server" Height="19px" Width="200px">
                                            <asp:ListItem Text="Física" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Jurídica" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 50px">
                                    <td>
                                        <asp:Label ID="lblCPF" runat="server" Text="CPF:"></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtCPF" habilitado="true" Enabled="true" tipoCampo="cpf" runat="server"
                                            AutoPostBack="true" Height="19px" Width="200px"></cc1:DGTECTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCNPJ" runat="server" Text="CNPJ:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCNPJ" habilitado="false" tipoCampo="cnpj" Enabled="false" AutoPostBack="true"
                                            runat="server" Height="19px" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        <table>
                            <tr style="height: 50px">
                                <td>
                                    <cc1:DGTECButton Height="30px" Width="100px" ID="BtnLimpar" runat="server" Text="Limpar" />
                                </td>
                            </tr>
                            <tr style="height: 50px">
                                <td>
                                    <cc1:DGTECButton Height="30px" Width="100px" ID="BtnSair" runat="server" Text="Sair" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr style="height: 50px">
                    <td>
                        <asp:Label ID="lblNome" runat="server" Text="Nome:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNome" MaxLength="100" AutoPostBack="true" runat="server"
                            Height="19px" Width="250px"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr runat="server" id="linhaPessoaJuridica" style="height: 50px; display: none;">
                    <td>
                        <asp:Label ID="lblNomePJ" runat="server" Text="Nome Fantasia:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <cc1:DGTECTextBox ID="txtNomeFantasia" MaxLength="1000" AutoPostBack="true" runat="server"
                            Height="19px" Width="250px"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr style="height: 50px">
                    <td>
                        <asp:Label ID="lblNomeParecido" runat="server" Text="Nomes Semelhantes:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlNomeSemelhante" Enabled="false" AutoPostBack="true"
                            runat="server" Height="19px" Width="250px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtCPF" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCNPJ" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlNomeSemelhante" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSair" EventName="Click" />            
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppC" runat="server" DisplayAfter="20" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <img alt="" src="loading6.gif" style="left: 50%; top: 50%; width: 90px; height: 90px;
                margin-top: -45px; margin-left: -45px; position: absolute;" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</asp:Content>
