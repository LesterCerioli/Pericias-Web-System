<%@ Page Title="Consulta de Pagamentos Realizados" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/LayoutPrincipal.Master" CodeBehind="frmConsultaPagamentosRealizados.aspx.vb"
    Inherits="Interface.frmConsultaPagamentosRealizados" %>

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
                        CONSULTA PAGAMENTOS REALIZADOS
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
                                <tr style="height: 30px">
                                    <td>
                                        <asp:Label ID="lblTipoPessoa" runat="server" Text="Tipo de Pessoa:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoPessoa" runat="server" Height="19px" Width="170px">
                                            <asp:ListItem Text="Física" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Jurídica" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <cc1:DGTECHiddenField ID="txtCodPerito" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td>
                                        <asp:Label ID="lblCPF" runat="server" Text="CPF:"></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtCPF" habilitado="true" Enabled="true" tipoCampo="cpf" runat="server"
                                            AutoPostBack="true" Height="19px" Width="170px"></cc1:DGTECTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCNPJ" runat="server" Text="CNPJ:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCNPJ" habilitado="false" tipoCampo="cnpj" Enabled="false" AutoPostBack="true"
                                            runat="server" Height="19px" Width="170px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        <table>
                            <tr style="height: 50px">
                                <td>
                                    <cc1:DGTECButton Height="30px" Width="80px" ID="BtnImprimir" Enabled="false" runat="server" Text="Imprimir" />
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
            <table>
                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="lblNome" runat="server" Text="Nome:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNome" MaxLength="100" AutoPostBack="true" runat="server"
                            Height="19px" Width="170px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblNomeParecido" runat="server" Text="Nomes Semelhantes:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlNomeSemelhante" Enabled="false" AutoPostBack="true"
                            runat="server" Height="19px" Width="170px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
                <tr runat="server" id="linhaPessoaJuridica" style="height: 30px; display: none;">
                    <td>
                        <asp:Label ID="lblNomePJ" runat="server" Text="Nome Fantasia:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <cc1:DGTECTextBox ID="txtNomeFantasia" MaxLength="1000" AutoPostBack="true" runat="server"
                            Height="19px" Width="170px"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="lblProfissao" runat="server" Text="Profissão:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlProfissao" runat="server" AutoPostBack="true" Height="19px"
                            Width="170px">
                        </cc1:DGTECDropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblEspecialidade" runat="server" Text="Especialidade:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlEspecialidade" runat="server" AutoPostBack="true" Height="19px"
                            Width="170px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="lblOficio" runat="server" Text="Número Ofício:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <cc1:DGTECTextBox ID="txtOficio" MaxLength="10" AutoPostBack="true" runat="server"
                            Height="19px" Width="170px"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNumeroProcesso" runat="server" Text="Número Processo:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNumeroProcesso" tipoCampo="processo_completo" MaxLength="25"
                            AutoPostBack="true" runat="server" Height="19px" Width="170px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        Processos Relacionados
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProcessosRelacionados" Enabled="false" runat="server" Height="19px"
                            Width="170px" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="lblRelacaoPagamento" runat="server" Text="Relação Pagamento:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlRelacaoPagamento" runat="server" AutoPostBack="true"
                            Height="19px" Width="170px">
                        </cc1:DGTECDropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblProcessoPagamento" runat="server" Text="Processo Pagamento:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtProcessoPagamento" tipoCampo="processo_completo"
                            MaxLength="10" AutoPostBack="true" runat="server" Height="19px" Width="170px"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="lblDataProtocoloInicial" runat="server" Text="Data Protocolo Inicial:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtDataProtocoloInicial" tipoCampo="data" AutoPostBack="true"
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
                <tr style="height: 30px">
                    <td>
                        <asp:Label ID="lblDataPagamentoInicial" runat="server" Text="Data Pagamento Inicial:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtDataPagamentoInicial" tipoCampo="data" AutoPostBack="true"
                            runat="server" Height="19px" Width="170px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblDataPagamentoFinal" runat="server" Text="Data Pagamento Final:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtDataPagamentoFinal" tipoCampo="data" AutoPostBack="true"
                            runat="server" Height="19px" Width="170px"></cc1:DGTECTextBox>
                    </td>
                </tr>
            </table>
            <br />
            <cc1:DGTECGridView ID="grdAjudaCusto" CellPadding="20"
                CellSpacing="20" runat="server" AutoGenerateColumns="false" Width="100%" PageSize="5"
                AllowPaging="true" BorderWidth="1" Font-Size="Smaller">
                <Columns>
                    <asp:BoundField DataField="NOME_PERITO" HeaderText="NOME" />
                    <asp:BoundField DataField="PROCESSO" HeaderText="PROCESSO" />
                    <asp:BoundField DataField="OFICIO" HeaderText="OFÍCIO" />
                    <asp:BoundField DataField="PAGAMENTO" HeaderText="DATA PGTO." />
                    <asp:TemplateField HeaderText="VALOR">
                        <ItemTemplate>
                            <asp:Label ID="lblValor" runat="server" Text='<%# String.Format("{0:N}", Eval("VALOR")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DETALHES">
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="imagens\img_lupa.gif" ID="btnVisualizarDetalhes" runat="server"
                                CausesValidation="false" CommandName="btnVisualizarDetalhes_Command" OnCommand="btnVisualizarDetalhes_Command"
                                CommandArgument='<%# eval("ID_AJUDA_CUSTO") %>' Text="Visualizar" ToolTip="Visualizar Detalhes da Ajuda de Custo"
                                Width="30px"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:DGTECGridView>
            <br />
            <asp:Label style="float: right;" ID="lblQntdPags" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtCPF" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCNPJ" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtNome" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtNomeFantasia" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlNomeSemelhante" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlProfissao" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEspecialidade" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtOficio" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtNumeroProcesso" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlRelacaoPagamento" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtProcessoPagamento" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDataProtocoloInicial" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDataProtocoloFinal" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDataPagamentoInicial" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDataPagamentoFinal" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnImprimir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSair" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdAjudaCusto" EventName="PageIndexChanging" />
            <%--<asp:AsyncPostBackTrigger ControlID="grdPeritos" EventName="RowDataBound" />--%>
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
