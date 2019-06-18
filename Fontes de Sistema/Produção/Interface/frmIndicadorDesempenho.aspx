<%@ Page Title="Indicador de Desempenho" Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.Master"
    CodeBehind="frmIndicadorDesempenho.aspx.vb" Inherits="Interface.frmIndicadorDesempenho" %>

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
                        INDICADOR DE DESEMPENHO
                    </th>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
                        <div style="height: 130px; width: 650px; padding-left: 10px;">
                            <br />
                            <table>
                                <tr style="height: 30px">
                                    <td>
                                        <asp:Label ID="lblDataProtocoloInicial" runat="server" Text="Data Protocolo Inicial:"></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtDataProtocoloInicial" tipoCampo="data"
                                            runat="server" Height="19px" Width="170px"></cc1:DGTECTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDataProtocoloFinal" runat="server" Text="Data Protocolo Final:"></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtDataProtocoloFinal" tipoCampo="data" AutoPostBack="true"
                                            runat="server" Height="19px" Width="170px"></cc1:DGTECTextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        <table>
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
            <asp:Panel ID="pnlMedia" runat="server" Visible="false">
                <asp:Label ID="lblTexto1" Text="Indicador de desempenho: " runat="server"></asp:Label>
                <asp:Label ID="lblMedia" Text="0 " ForeColor="Red" runat="server"></asp:Label>
                <asp:Label ID="lblTexto2" Text=" dias(s) para protocolização do ofício de ajuda de custo, em média." runat="server"></asp:Label>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtDataProtocoloFinal" EventName="TextChanged" />
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
