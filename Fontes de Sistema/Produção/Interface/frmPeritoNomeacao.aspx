<%@ Page Title="Peritos para Nomeação" Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.Master"
    CodeBehind="frmPeritoNomeacao.aspx.vb" Inherits="Interface.frmPeritoNomeacao" %>

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
                        PERITOS PARA NOMEAÇÃO
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
                    <td>
                        <asp:Label ID="lblNomeParecido" runat="server" Text="Nomes Semelhantes:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlNomeSemelhante" Enabled="false" AutoPostBack="true"
                            runat="server" Height="19px" Width="250px">
                        </cc1:DGTECDropDownList>
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
                        <asp:Label ID="lblProfissao" runat="server" Text="Profissão:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlProfissao" runat="server" AutoPostBack="true" Height="19px"
                            Width="250px">
                        </cc1:DGTECDropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblEspecialidade" runat="server" Text="Especialidade:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlEspecialidade" runat="server" AutoPostBack="true" Height="19px"
                            Width="250px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
                <tr style="height: 50px">
                    <td>
                        <asp:Label ID="lblNur" runat="server" Text="NUR:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlNur" runat="server" AutoPostBack="true" Height="19px"
                            Width="250px">
                        </cc1:DGTECDropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblComarca" runat="server" Text="Comarca:"></asp:Label>
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="ddlComarca" runat="server" AutoPostBack="true" Height="19px"
                            Width="250px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table id="tblPeritos" runat="server" width="100%" visible="false">
            </table>
            <%--<br />
            <cc1:DGTECGridView ID="grdPeritos" runat="server" Width="100%" AutoGenerateColumns="false"
                RowStyle-BackColor="#DAD4D4" RowStyle-Height="20px" RowStyle-BorderColor="White"
                RowStyle-BorderWidth="2" OnRowDataBound="grdPeritos_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Panel ID="pnlPerito" runat="server">
                                <asp:Image runat="server" Style="margin-left: 5px;" ID="imgCollapsible" ImageUrl="~/Imagens/iconmonstr-arrow-70-24.png" />
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NOME" />
                    <asp:BoundField DataField="CPF_CNPJ" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink Target="_blank" Text="Detalhes do perito" ID="hlDetalhesPerito" runat="server"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="COD_PERITO" Visible="true" ItemStyle-Width="1px" />
                </Columns>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Panel ID="pnlAnotacoes" runat="server">
                                <cc1:DGTECGridView ID="grdAnotacoes" runat="server" Width="100%" AutoGenerateColumns="false"
                                    EmptyDataText="Não há anotações cadastradas pare este perito.">
                                    <Columns>
                                        <asp:BoundField DataField="DESCR_ANOTACAO" />
                                    </Columns>
                                </cc1:DGTECGridView>
                            </asp:Panel>
                            <cc2:CollapsiblePanelExtender ID="cpe1" runat="server" CollapsedSize="0" Collapsed="true"
                                ExpandControlID="pnlPerito" CollapseControlID="pnlPerito" AutoCollapse="false"
                                AutoExpand="false" ScrollContents="false" ImageControlID="imgCollapsible" ExpandedImage="~/Imagens/iconmonstr-arrow-69-24.png"
                                TargetControlID="pnlAnotacoes" CollapsedImage="~/Imagens/iconmonstr-arrow-70-24.png"
                                ExpandDirection="Horizontal">
                            </cc2:CollapsiblePanelExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:DGTECGridView>--%>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtCPF" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCNPJ" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlNomeSemelhante" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlProfissao" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlEspecialidade" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlNur" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlComarca" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSair" EventName="Click" />
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
