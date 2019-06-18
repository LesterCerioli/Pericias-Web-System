<%@ Page Title="Relação de Pagamentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.Master"
    CodeBehind="frmRelacaoPagamento.aspx.vb" Inherits="Interface.frmRelacaoPagamento" %>

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
                        RELAÇÃO DE PAGAMENTO - AJUDA DE CUSTO
                    </th>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
                        <div style="height: 130px; width: 690px; padding-left: 10px;">
                            <br />
                            <table cellpadding="10">
                                <tr style="height: 30px;">
                                    <td>
                                        <asp:Label ID="lblNomeRelacaoPagamento" runat="server" Text="Relação de Pagamento:"></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtNomeRelacaoPagamento" habilitado="true" Enabled="true" MaxLength="30"
                                            runat="server" AutoPostBack="true" Height="19px" Width="160px"></cc1:DGTECTextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRelacaoPgtoSemelhantes" runat="server" Text="Relações de Pagamento Semelhantes:"></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRelacaoPgtoSemelhantes" AutoPostBack="true" runat="server"
                                            Height="19px" Width="160px" />
                                    </td>
                                </tr>
                                <tr style="height: 30px;">
                                    <td>
                                        <asp:Label ID="lblRelacaoDefinitiva" runat="server" Text="Relação Definitva:"></asp:Label>
                                    </td>
                                    <td>
                                        <cc1:DGTECCheckBox ID="chkRelacaoDefinitiva" Enabled="false" runat="server" />
                                    </td>
                                    <td colspan="2">
                                        <cc1:DGTECHiddenField ID="txtIdRelacaoPagamento" runat="server" />
                                    </td>
                                </tr>
                            </table>
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
                                    <cc1:DGTECButton Height="30px" Width="80px" ID="btnImprimir" Enabled="false" runat="server"
                                        Text="Imprimir" />
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
            <br />
            <cc1:DGTECPanel ID="pnlListaAjudaRelacao" runat="server" Width="100%" Visible="false">
                <cc1:DGTECGridView ID="grdAjudaCustoRelacao" DataKeyNames="ID_RELACAO_PAGAMENTO, ID_AJUDA_CUSTO"
                    EmptyDataText="NÃO HÁ DADOS" runat="server" AutoGenerateColumns="false" Width="100%"
                    PageSize="10" AllowPaging="true" Caption="AJUDAS DE CUSTO ASSOCIADAS À RELAÇÃO DE PAGAMENTO"
                    BorderWidth="1" Font-Size="Smaller">
                    <Columns>
                        <asp:BoundField DataField="NOME_PERITO" HeaderText="Nome" />
                        <asp:BoundField DataField="CPF_CNPJ" HeaderText="CPF/CNPJ" />
                        <asp:BoundField DataField="DESCR_ESPECIALIDADE" HeaderText="Especialidade" />
                        <asp:BoundField DataField="NUMERO_OFICIO" HeaderText="Ofício" />
                        <asp:BoundField DataField="NUMERO_PROCESSO" HeaderText="Processo" />
                        <asp:TemplateField HeaderText="Valor">
                            <ItemTemplate>
                                <asp:Label ID="lblValor" runat="server" Text='<%# String.Format("R$ {0:N}", Eval("VALOR")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="VALOR" HeaderText="Valor" />--%>
                        <asp:TemplateField HeaderText="Alterar">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="imagens\bt_editar.gif" ID="btnAlterarAjuda" runat="server"
                                    CausesValidation="false" CommandName="btnAlterarRel_Command" OnCommand="btnAlterarRel_Command"
                                    CommandArgument='<%# eval("ID_RELACAO_PAGAMENTO")&","& eval("ID_AJUDA_CUSTO") %>'
                                    Text="Alterar" ToolTip="Alterar Ajuda de Custo" Width="30px"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirAjuda" runat="server"
                                    CausesValidation="false" CommandName="btnExcluirRel_Command" OnCommand="btnExcluirRel_Command"
                                    CommandArgument='<%# eval("ID_RELACAO_PAGAMENTO")&","& eval("ID_AJUDA_CUSTO") %>'
                                    OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir?')" %>
                                    Text="Excluir" ToolTip="Excluir Ajuda de Custo" Width="30px"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                </cc1:DGTECGridView>
                <br />
                <table>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnNovaAjudaCusto" Enabled="false" Text="Nova Ajuda de Custo" Width="150px"
                                runat="server" /><br />
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <br />
            <cc1:DGTECPanel ID="pnlPeritoAjudaCusto" runat="server" Width="100%" Visible="false">
                <table class="corpo-form" style="width: 100%;" align="left">
                    <tr>
                        <th align="center">
                            PERITO
                        </th>
                    </tr>
                </table>
                <br />
                <table cellpadding="10">
                    <caption>
                        <br />
                        <tr>
                            <td>
                                Nome
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtNomePerito" runat="server" AutoPostBack="true" Enabled="true"
                                    habilitado="true" Height="19px" MaxLength="1000" Width="175px"></cc1:DGTECTextBox>
                            </td>
                            <td>
                                Nomes Semelhantes
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNomeSemelhante" Enabled="true" runat="server" AutoPostBack="true"
                                    Height="19px" Width="175px">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2">
                                <cc1:DGTECButton ID="btnNovoPerito" runat="server" Enabled="false" Height="30px"
                                    Text="Novo Perito" Width="100px" />
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                CPF/CNPJ
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtCpfCnpj" runat="server" AutoPostBack="true" Enabled="true"
                                    habilitado="true" MaxLength="18" Height="19px" tipoCampo="cpf_cnpj" Width="175px"></cc1:DGTECTextBox>
                            </td>
                            <td colspan="3">
                                <cc1:DGTECHiddenField ID="txtCodPerito" runat="server" />
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                Nº Conselho Profissional
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNumeroConselho" Enabled="true" runat="server" Height="19px"
                                    Width="100px" />
                            </td>
                            <td>
                                Sigla Conselho
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSigla" Enabled="true" runat="server" Height="19px" Width="90px" />
                            </td>
                            <td>
                                UF Conselho
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUF" Enabled="true" runat="server" Height="19px" Width="90px" />
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                Profissão
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProfissao" Enabled="true" runat="server" Height="19px" Width="175px" />
                            </td>
                            <td>
                                Especialidade
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlEspecialidade" Enabled="true" runat="server" Height="19px"
                                    Width="175px" />
                            </td>
                        </tr>
                    </caption>
                </table>
                <br />
                <table class="corpo-form" style="width: 100%;" align="left">
                    <tr>
                        <th align="center">
                            AJUDA DE CUSTO
                        </th>
                    </tr>
                </table>
                <br />
                <table cellpadding="10">
                    <caption>
                        <br />
                        <tr style="height: 30px;">
                            <td>
                                Nº Ofício
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtNumeroOficio" runat="server" Enabled="true" habilitado="true"
                                    Height="19px" MaxLength="10" Width="140px"></cc1:DGTECTextBox>
                            </td>
                            <td>
                                Data Recebimento Ofício
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataRecebimentoOficio" runat="server" Enabled="true" Height="19px"
                                    tipoCampo="data" Width="100px"> </asp:TextBox>
                            </td>
                            <td>
                                <cc1:DGTECButton ID="btnInserirAjudaCusto" runat="server" Enabled="true" Height="30px"
                                    Text="Inserir Ajuda de Custo" Width="150px" />
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                Nº Processo
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtNumeroProcesso" AutoPostBack="true" tipoCampo="processo_completo"
                                    runat="server" Enabled="true" habilitado="true" Height="19px" MaxLength="25"
                                    Width="160px"></cc1:DGTECTextBox>
                            </td>
                            <td>
                                Processos Relacionados
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlProcessosRelacionados" Enabled="false" runat="server" Height="19px"
                                    Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Valor
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtValor" runat="server" Enabled="true" habilitado="true" Height="19px"
                                    tipoCampo="moeda" Width="100px"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                Processo Pagamento
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtProcessoPagamento" tipoCampo="processo_completo" runat="server" Enabled="true" habilitado="true"
                                    Height="19px" MaxLength="10" Width="140px"></cc1:DGTECTextBox>
                            </td>
                            <td>
                                Data Protocolo
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataProtocolo" runat="server" Enabled="true" Height="19px" tipoCampo="data"
                                    Width="100px"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                Data Pagamento
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataPagamento" runat="server" Enabled="true" Height="19px" tipoCampo="data"
                                    Width="100px"> </asp:TextBox>
                            </td>
                            <td>
                                <cc1:DGTECHiddenField ID="txtIdAjudaCusto" runat="server" />
                            </td>
                            <td>
                                <cc1:DGTECHiddenField ID="txtSeqAjudaCusto" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Observação
                            </td>
                            <td colspan="4">
                                <cc1:DGTECTextBox ID="txtObs" runat="server" Height="80px" MaximoCaracteres="100"
                                    MaxLength="100" TextMode="MultiLine" Width="650px"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                    </caption>
                </table>
                <br />
                <cc1:DGTECGridView ID="grdAjudaCustoPerito" EmptyDataText="NÃO HÁ DADOS" CellPadding="20"
                    CellSpacing="20" runat="server" AutoGenerateColumns="false" Width="100%" PageSize="5"
                    AllowPaging="true" BorderWidth="1" Font-Size="Smaller">
                    <Columns>
                        <asp:BoundField DataField="Oficio" HeaderText="N° Ofício" />
                        <asp:BoundField DataField="DataRecebimento" HeaderText="Data Receb." />
                        <asp:BoundField DataField="NumeroProcesso" HeaderText="N° Processo" />
                        <asp:BoundField DataField="ProcessoPagamento" HeaderText="Processo Pgto." />
                        <asp:BoundField DataField="DataProtocolo" HeaderText="Data Protocolo" />
                        <asp:BoundField DataField="DataPagamento" HeaderText="Data Pgto." />
                        <asp:BoundField DataField="Observacao" HeaderText="Observação" />
                        <asp:TemplateField HeaderText="Alterar">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="imagens\bt_editar.gif" ID="btnAlterarAjuda" runat="server"
                                    CausesValidation="false" CommandName="btnAlterarAjuda_Command" OnCommand="btnAlterarAjuda_Command"
                                    CommandArgument='<%# eval("Sequencial")&","& eval("Id")&","& eval("RelacaoPagamento.Id") %>'
                                    Text="Alterar" ToolTip="Alterar Ajuda de Custo" Width="30px"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluir">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirAjuda" runat="server"
                                    CausesValidation="false" CommandName="btnExcluirAjuda_Command" OnCommand="btnExcluirAjuda_Command"
                                    CommandArgument='<%# eval("Sequencial")&","& eval("Id")&","& eval("RelacaoPagamento.Id") %>'
                                    OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir?')" %>
                                    Text="Excluir" ToolTip="Excluir Ajuda de Custo" Width="30px"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:DGTECGridView>
            </cc1:DGTECPanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtNomeRelacaoPagamento" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlRelacaoPgtoSemelhantes" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnGravar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnImprimir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSair" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNovoPerito" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNovaAjudaCusto" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="txtNomePerito" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlNomeSemelhante" EventName="SelectedIndexChanged" />
            <%--<asp:AsyncPostBackTrigger ControlID="txtCpfCnpj" EventName="TextChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnInserirAjudaCusto" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNovoPerito" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdAjudaCustoRelacao" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="grdAjudaCustoPerito" EventName="PageIndexChanging" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnSim" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNao" EventName="Click" />--%>
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
