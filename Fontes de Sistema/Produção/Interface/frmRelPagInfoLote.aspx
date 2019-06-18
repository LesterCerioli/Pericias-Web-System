<%@ Page Title="Cadastro de Informações em Lote" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/LayoutPrincipal.Master" CodeBehind="frmRelPagInfoLote.aspx.vb"
    Inherits="Interface.frmRelPagInfoLote" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
                        INFORMAÇÕES DE PAGAMENTO - EM LOTE
                    </th>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
                        <div style="height: 130px;">
                            <br />
                            <table cellpadding="10" cellspacing="10">
                                <tr style="height: 30px">
                                    <td>
                                        <asp:Label ID="lblNomeRelacaoPagamento" runat="server" Text="Relação de Pagamento:"></asp:Label>&nbsp;
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlRelacaoPagamento" runat="server" Height="19px" Width="180px" />
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td>
                                        <asp:Label ID="lblProcessoPagamento" runat="server" Text="Processo de Pagamento:"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <cc1:DGTECTextBox ID="txtProcessoPagamento" tipoCampo="processo_completo" MaxLength="11"
                                            runat="server" Enabled="true" habilitado="true" Height="19px"
                                            Width="140px"></cc1:DGTECTextBox>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td>
                                        <asp:Label ID="lblDataProtocolo" runat="server" Text="Data Protocolo:"></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtDataProtocolo" runat="server" Enabled="true" habilitado="true"
                                            Height="19px" tipoCampo="data" Width="140px"></cc1:DGTECTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDataPagamento" runat="server" Text="Data Pagamento:"></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtDataPagamento" tipoCampo="data" runat="server" Enabled="true"
                                            habilitado="true" Height="19px" Width="140px"></cc1:DGTECTextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                    </td>
                    <td>
                        <table>
                            <tr style="height: 50px">
                                <td>
                                    <cc1:DGTECButton Height="30px" Width="80px" ID="BtnGravar" Enabled="false" runat="server"
                                        Text="Gravar" />
                                </td>
                            </tr>
                            <tr style="height: 50px">
                                <td>
                                    <cc1:DGTECButton Height="30px" Width="80px" ID="BtnLimpar" runat="server" Text="Limpar" />
                                </td>
                            </tr>
                            <tr style="height: 50px">
                                <td>
                                    <cc1:DGTECButton Height="30px" Width="80px" ID="BtnSair" runat="server" Text="Sair" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGravar" EventName="Click" />
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
