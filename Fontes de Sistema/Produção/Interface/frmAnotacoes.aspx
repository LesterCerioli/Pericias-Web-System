<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master"
    CodeBehind="frmAnotacoes.aspx.vb" Inherits="Interface.frmAnotacoes" Title="Anotações" %>

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
                        ANOTAÇÕES DO PERITO
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
                                    <cc1:DGTECButton Height="30px" Width="100px" ID="BtnGravar" runat="server" Text="Gravar" />
                                </td>
                            </tr>
                            <tr style="height: 50px">
                                <td>
                                    <cc1:DGTECButton Height="30px" Width="100px" ID="BtnExcluir" runat="server" Text="Excluir" />
                                </td>
                            </tr>
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
                    <td colspan="3">
                        <cc1:DGTECTextBox ID="txtNome" MaxLength="100" AutoPostBack="true" runat="server"
                            Height="19px" Width="300px"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr runat="server" id="linhaPessoaJuridica" style="height: 50px; display: none;">
                    <td>
                        <asp:Label ID="lblNomePJ" runat="server" Text="Nome Fantasia:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <cc1:DGTECTextBox ID="txtNomeFantasia" MaxLength="1000" AutoPostBack="true" runat="server"
                            Height="19px" Width="300px"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr style="height: 50px">
                    <td>
                        <asp:Label ID="lblNomeParecido" runat="server" Text="Nomes Semelhantes:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <cc1:DGTECDropDownList ID="CboPerito" Enabled="false" AutoPostBack="true" runat="server" Height="19px"
                            Width="300px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 50px">
                        <asp:Label ID="lblDataNascimento" runat="server" Text="Data de Nascimento:"></asp:Label>
                    </td>
                    <td style="height: 50px">
                        <asp:TextBox ID="txtDt_Nasc" Enabled="false" tipoCampo="data" runat="server" Height="19px"
                            Width="100px">
                        </asp:TextBox>
                    </td>
                    <td style="height: 50px">
                        <asp:Label ID="lblStatusPerito" runat="server" Text="Status Perito:"></asp:Label>
                    </td>
                    <td style="height: 50px">
                        <asp:DropDownList ID="ddlStatusPerito" Enabled="false" runat="server" Height="19px" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 50px">
                        Data Anotação:
                    </td>
                    <td style="height: 50px">
                        <cc1:DGTECLabel ID="lblData_Anotacao" runat="server"></cc1:DGTECLabel>
                    </td>
                </tr>
                <tr>
                    <td style="height: 50px">
                        Tipo de Anotação:
                    </td>
                    <td style="height: 50px">
                        <cc1:DGTECDropDownList ID="cboTipo_anotacao" runat="server" Height="18px" Width="180px">
                        </cc1:DGTECDropDownList>
                    </td>
                    <td style="height: 110px">
                        <cc1:DGTECButton ID="BtnNova" runat="server" Text="Nova Anotação" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <cc1:DGTECTextBox ID="txtAnotacao" runat="server" MaximoCaracteres="1000" MaxLength="1000" Height="80px" Width="650px" TextMode="MultiLine"></cc1:DGTECTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" width="520">
                        <br />
                        <cc1:DGTECGridView ID="GrdAnotacoes" runat="server" Width="268px" EmptyDataText="NÃO HÁ REGISTROS"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField HeaderText="Data" DataField="DT_ANOT" DataFormatString="{0:Y}">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Tipo" DataField="Tipo">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ImageUrl="imagens\img_lupa.gif" ID="btnGrdVisualizar" runat="server"
                                            CausesValidation="false" CommandName="btnGrdVisualizar_Command" CommandArgument='<%# eval("Cod_tipo_anotacao")&","& eval("DT_ANOT")&","& eval("COD_PERITO") %>'
                                            Text="Visualizar" OnCommand="btnGrdVisualizar_Command"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="conteudo-grid"></RowStyle>
                            <HeaderStyle CssClass="titulo-grid"></HeaderStyle>
                        </cc1:DGTECGridView>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="txtCodPerito" runat="server" Value="" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtCPF" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCNPJ" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtNome" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtNomeFantasia" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboPerito" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="BtnGravar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnLimpar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnSair" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnExcluir" EventName="Click" />
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
