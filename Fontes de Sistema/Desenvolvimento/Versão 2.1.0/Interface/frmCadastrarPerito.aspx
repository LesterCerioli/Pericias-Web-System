<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.Master"
    CodeBehind="frmCadastrarPerito.aspx.vb" Inherits="Interface.frmCadastrarPerito"
    Title="Cadastrar Perito" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server" method="post">
    <div id="openModal" class="modalDialog">
        <div>
            <a href="#close" title="Close" id="btnCloseDoc" class="close">X</a>
            <div id="bordaTitulo">
                <label>
                    Inserir Documento</label>
            </div>
            <div id="divDocumento">
                <asp:Label Text="Documento" runat="server" ID="lblTituloDocumento"></asp:Label>
                &nbsp;
                <cc1:DGTECFileUpload Style="display: inline-block" ID="txtDocumento" runat="server"
                    Width="300px" />
                <asp:Button ID="btnGravarDocumento" Style="left: 50%; margin-left: -65px; top: 10px;
                    position: relative;" Text="Gravar Documento" Width="130px" runat="server" />
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePartialRendering="True">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <input type="hidden" id="txtDataAtual" runat="server" />
            <cc1:DGTECHiddenField ID="txtCod_Perito" runat="server" />
            <cc1:DGTECHiddenField ID="txtID_PF" runat="server" />
            <table class="corpo-form" style="width: 100%;" align="center">
                <tr>
                    <th align="center">
                        DADOS PESSOAIS
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
                        <asp:DropDownList ID="CboPerito" AutoPostBack="true" runat="server" Height="19px"
                            Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 50px">
                        <asp:Label ID="lblDataNascimento" runat="server" Text="Data de Nascimento:"></asp:Label>
                    </td>
                    <td style="height: 50px">
                        <asp:TextBox ID="txtDt_Nasc" Enabled="true" tipoCampo="data" tipoData="data_nascimento"
                            runat="server" Height="19px" Width="100px">
                        </asp:TextBox>
                    </td>
                    <td style="height: 50px">
                        <asp:Label ID="lblStatusPerito" runat="server" Text="Status Perito:"></asp:Label>
                    </td>
                    <td style="height: 50px">
                        <asp:DropDownList ID="ddlStatusPerito" runat="server" Height="19px" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <cc1:DGTECLabel ID="lblMsg" runat="server" ForeColor="Red"></cc1:DGTECLabel>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr style="height: 30px">
                    <td>
                        <cc1:DGTECButton ID="BtnGravaFoto" runat="server" Text="Gravar Foto" Width="100px" />
                    </td>
                    <td>
                        <cc1:DGTECButton ID="BtnExibirFoto" runat="server" Text="Visualizar Foto" Width="100px" />
                    </td>
                    <td>
                        <cc1:DGTECButton ID="BtnChklist" runat="server" Text="ChkList" Width="100px" />
                    </td>
                    <td>
                        <cc1:DGTECButton ID="BtnEndRes" runat="server" Text="End. Res." Width="100px" />
                    </td>
                    <td>
                        <cc1:DGTECButton ID="BtnEndCom" runat="server" Text="End. Com." Width="100px" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        <cc1:DGTECButton ID="BtnTel" runat="server" Text="Telefones" Width="100px" />
                    </td>
                    <td>
                        <cc1:DGTECButton ID="BtnProfissao" runat="server" Text="Profissão" Width="100px" />
                    </td>
                    <td>
                        <cc1:DGTECButton ID="BtnInf" runat="server" Text="Inf. Adicionais" Width="100px" />
                    </td>
                    <td>
                        <cc1:DGTECButton ID="BtnDocumentos" runat="server" Text="Documentos" Width="100px" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <br />
            <cc1:DGTECPanel ID="PanelEndRes" runat="server" Enabled="False" Width="100%" Visible="false">
                <table class="corpo-form" border="0" cellpadding="10" width="100%">
                    <tr>
                        <th align="center">
                            ENDEREÇO RESIDENCIAL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <cc1:DGTECButton ID="BtnNovoEndRes" runat="server" Text="Novo End. Res." />
                        </th>
                    </tr>
                </table>
                <br />
                <table>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="lblCEP" runat="server" Text="CEP: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="TxtCEP" runat="server" Height="19px" Width="70px" MaxLength="8"
                                AutoPostBack="True"></cc1:DGTECTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="lblUF" runat="server" Text="UF: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboUF" runat="server" AutoPostBack="True" Height="19px"
                                Width="100px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblCidade" runat="server" Text="Cidade: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboCidade" runat="server" AutoPostBack="True" Height="19px"
                                Width="392px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="lblBairro" runat="server" Text="Bairro: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboBairro" runat="server" AutoPostBack="True" Height="19px"
                                Width="100px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblTipoLogradoro" runat="server" Text="Tipo Logr: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboTip_Logr" runat="server" AutoPostBack="True" Height="19px"
                                Width="392px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="lblLogradoro" runat="server" Text="Logr: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="txtNome_Logr" runat="server" Height="19px" Width="100px"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblNumeroLogradoro" runat="server" Text="Nº: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="txtNum_Logr" runat="server" Height="19px" Width="40px"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblComplementoLogradoro" runat="server" Text="Compl. Logr: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="txtCompl_Logr" runat="server" Height="19px" Width="70px"></cc1:DGTECTextBox>
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <br />
            <cc1:DGTECPanel ID="PanelTel" runat="server" Enabled="False" Visible="false" Width="100%">
                <table class="corpo-form" border="0" cellpadding="10" width="80%">
                    <tr>
                        <th align="center">
                            TELEFONES
                        </th>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            <cc1:DGTECGridView ID="GrdTel" runat="server" AutoGenerateColumns="false" EmptyDataText="Não há registro"
                                Width="80%" PageSize="4" AllowPaging="true">
                                <Columns>
                                    <asp:BoundField DataField="seq_tel" Visible="false" />
                                    <asp:BoundField DataField="desc_tip_tel" HeaderText="Tipo Telefone" />
                                    <asp:BoundField DataField="cod_tip_tel" HeaderText="Cod. tipo tel" Visible="false" />
                                    <asp:BoundField DataField="ddd" HeaderText="DDD" />
                                    <asp:BoundField DataField="Num_tel" HeaderText="Número" />
                                    <asp:BoundField DataField="num_ramal" HeaderText="Ramal" />
                                    <asp:TemplateField HeaderText="Alterar">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="imagens\bt_editar.gif" ID="btnGrdAlterar" runat="server"
                                                CausesValidation="false" CommandName="btnGrdAlterar_Command" CommandArgument='<%# eval("ID_PF")&","&  eval("seq_tel")&","& eval("Cod_Perito")&","& eval("tipoPessoa")&","& eval("ID_PJ") %>'
                                                Text="Alterar" ToolTip="Alterar telefone" OnCommand="btnGrdAlterar_Command">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirTel" runat="server"
                                                CausesValidation="false" CommandName="btnExcluirTel_Command" CommandArgument='<%# eval("ID_PF")&","&  eval("seq_tel")&","& eval("Cod_Perito")&","& eval("tipoPessoa")&","& eval("ID_PJ") %>'
                                                Text="Excluir" ToolTip="Excluir telefone" OnCommand="btnExcluirTel_Command" OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir o telefone?')" %>>
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            </cc1:DGTECGridView>
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%">
                    <tr style="height: 30px">
                        <td>
                            Tipo de Telefone:
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboTip_Tel" runat="server" Width="90px" AutoPostBack="True">
                                <asp:ListItem Value="1">Residencial</asp:ListItem>
                                <asp:ListItem Value="2">Profissional</asp:ListItem>
                                <asp:ListItem Value="4">Celular</asp:ListItem>
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                            Tel:
                        </td>
                        <td>
                            <span>(</span><div style="display: inline-block">
                                <cc1:DGTECTextBox ID="txtDDD" runat="server" Height="17px" Width="32px"></cc1:DGTECTextBox></div>
                            <span>)</span>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="TxtTel" runat="server" Height="17px" Width="150px" AutoPostBack="True"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            <cc1:DGTECLabel ID="lblRamal" runat="server" Text="Ramal: " />
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="TxtRamal" runat="server" Height="17px" Width="60px" AutoPostBack="True" />
                        </td>
                    </tr>
                    <tr align="right" style="height: 30px">
                        <td colspan="7">
                            <cc1:DGTECButton ID="btnGravarTel" runat="server" Text="Gravar Telefone" />
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <br />
            <cc1:DGTECPanel ID="PanelEndCom" runat="server" Enabled="False" Width="100%">
                <table class="corpo-form" border="0" cellpadding="10" width="100%">
                    <tr>
                        <th align="center">
                            ENDEREÇO COMERCIAL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <cc1:DGTECButton ID="BtnNovoEndCom" runat="server" Text="Novo End. Com." />
                        </th>
                    </tr>
                </table>
                <br />
                <table>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="CEP: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="TxtCEP1" runat="server" Height="19px" Width="70px" MaxLength="8"
                                AutoPostBack="True"></cc1:DGTECTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="UF: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboUF1" runat="server" AutoPostBack="True" Height="19px"
                                Width="100px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Cidade: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboCidade1" runat="server" AutoPostBack="True" Height="19px"
                                Width="392px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Bairro: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboBairro1" runat="server" AutoPostBack="True" Height="19px"
                                Width="100px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Tipo Logr: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECDropDownList ID="CboTip_Logr1" runat="server" AutoPostBack="True" Height="19px"
                                Width="392px">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Logr: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="txtNome_Logr1" runat="server" Height="19px" Width="100px"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Nº: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="txtNum_Logr1" runat="server" Height="19px" Width="40px"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Compl. Logr: "></asp:Label>
                        </td>
                        <td>
                            <cc1:DGTECTextBox ID="txtCompl_Logr1" runat="server" Height="19px" Width="70px"></cc1:DGTECTextBox>
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <br />
            <cc1:DGTECPanel ID="PanelInf" runat="server" Enabled="False" CssClass="caixa-form"
                Visible="true" Width="100%">
                <table border="0" class="corpo-form" cellpadding="10" width="100%">
                    <tr>
                        <th align="center">
                            INFORMAÇÕES ADICIONAIS
                        </th>
                    </tr>
                </table>
                <table border="0" cellpadding="10" width="100%">
                    <tr>
                        <td style="text-align: center; width: 9%;">
                            Nurs
                            <br />
                            de
                            <br />
                            Atuação
                        </td>
                        <td id="Btn" height="40" style="width: 49%;">
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none;" ID="BtnNur1"
                                runat="server" Text="1º NUR" TabIndex="1" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none;" ID="BtnNur2"
                                runat="server" Text="2º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none;" ID="BtnNur3"
                                runat="server" Text="3º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none;" ID="BtnNur4"
                                runat="server" Text="4º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none;" ID="BtnNur5"
                                runat="server" Text="5º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none;" ID="BtnNur6"
                                runat="server" Text="6º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none;" ID="BtnNur7"
                                runat="server" Text="7º NUR" Width="80px" />&nbsp;
                            <br />
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none; margin-top: 5px;"
                                ID="BtnNur8" runat="server" Text="8º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none; margin-top: 5px;"
                                ID="BtnNur9" runat="server" Text="9º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none; margin-top: 5px;"
                                ID="BtnNur10" runat="server" Text="10º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none; margin-top: 5px;"
                                ID="BtnNur11" runat="server" Text="11º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none; margin-top: 5px;"
                                ID="BtnNur12" runat="server" Text="12º NUR" Width="80px" />&nbsp;
                            <cc1:DGTECLinkButton Style="text-align: center; text-decoration: none; margin-top: 5px;"
                                ID="BtnNur13" runat="server" Text="13º NUR" Width="80px" />&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <table border="0" align="center" width="100%">
                    <tr>
                        <td align="center" style="width: 100%">
                            <cc1:DGTECButton ID="btnNurMarcarTodos" runat="server" Text="Marcar Todos Nur" />
                            &nbsp;
                            <cc1:DGTECButton ID="btnNurDesmarcarTodos" runat="server" Text="Desmarcar Todos Nur" />
                        </td>
                    </tr>
                </table>
                <br />
                <table border="0" cellpadding="10" width="100%">
                    <tr>
                        <td align="left">
                            Email
                        </td>
                        <td style="padding: 5px;">
                            <asp:RegularExpressionValidator ID="ValidarEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Email Inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <br />
                            <cc1:DGTECTextBox ID="txtEmail" runat="server" Height="19px" Width="200px"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            Email Alternativo
                        </td>
                        <td style="padding: 5px;">
                            <asp:RegularExpressionValidator ID="ValidarEmail1" runat="server" ControlToValidate="txtEmail1"
                                ErrorMessage="Email Inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <br />
                            <cc1:DGTECTextBox ID="txtEmail1" runat="server" Height="19px" Width="200px"></cc1:DGTECTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Cadastro
                        </td>
                        <td style="padding: 5px;">
                            <cc1:DGTECTextBox Width="200px" Height="19px" Enabled="false" runat="server" ID="txtData_Cadastramento"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            Data Migração
                        </td>
                        <td style="padding: 5px;">
                            <cc1:DGTECTextBox Width="200px" Height="19px" runat="server" ID="txtData_Migracao"
                                Enabled="false"></cc1:DGTECTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Perito do Juizo?
                        </td>
                        <td style="padding: 5px;">
                            <cc1:DGTECRadioButtonList ID="RdPeritoJuizo" Enabled="false" runat="server" Width="97px">
                                <asp:ListItem Value="liSim">Sim</asp:ListItem>
                                <asp:ListItem Value="liNao">Não</asp:ListItem>
                            </cc1:DGTECRadioButtonList>
                        </td>
                        <td>
                            Situação
                        </td>
                        <td style="padding: 5px;">
                            <cc1:DGTECRadioButtonList ID="RdSit" runat="server" Width="97px">
                                <asp:ListItem Value="OptOK">OK</asp:ListItem>
                                <asp:ListItem Selected="True" Value="OptPendente">Pendente</asp:ListItem>
                            </cc1:DGTECRadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Banco
                        </td>
                        <td style="padding: 5px;" colspan="3">
                            <cc1:DGTECDropDownList ID="CboBanco" runat="server" Height="19px" Width="600px">
                            </cc1:DGTECDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Agência
                        </td>
                        <td style="padding: 5px;">
                            <cc1:DGTECTextBox ID="TxtNum_Agencia" runat="server" Height="17px" Width="80px" MaxLength="6"></cc1:DGTECTextBox>
                        </td>
                        <td>
                            Nome da Agência
                        </td>
                        <td style="padding: 5px;">
                            <cc1:DGTECTextBox ID="txtNome_Agencia" runat="server" Height="19px" Width="185px"></cc1:DGTECTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Conta Corrente
                        </td>
                        <td style="padding: 5px;">
                            <cc1:DGTECTextBox ID="txtNum_Conta_Corrente" Width="300px" runat="server" Height="19px"
                                MaxLength="10"></cc1:DGTECTextBox>
                        </td>
                        <td style="padding: 5px;" colspan="2">
                            <cc1:DGTECButton ID="BtnDadosBancarios" runat="server" Text="Inserir dados bancários"
                                Enabled="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Falta Entregar
                        </td>
                        <td style="padding: 5px;" colspan="3">
                            <cc1:DGTECTextBox ID="txtFalta_Entregar" runat="server" Height="40px" Width="420px"></cc1:DGTECTextBox>
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <br />
            <cc1:DGTECPanel ID="PanelChklist" runat="server" Enabled="False" CssClass="caixa-form"
                Width="100%">
                <table class="corpo-form">
                    <tr>
                        <th>
                            MARQUE OS DOCUMENTOS ENTREGUES A DIPEJ
                        </th>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 277px">
                            <cc1:DGTECCheckBox ID="chkDOCNECCV" runat="server" Text="Curriculum Profissional" />
                            <br />
                            <cc1:DGTECCheckBox ID="chkDOCNECORG" runat="server" Text="Cópia autenticada da carteira do Órgão de Classe" />
                            <br />
                            <cc1:DGTECCheckBox ID="chkDOCNECHAB" runat="server" Text="Cópia de dois laudos Periciais já realizados junto à Justiça Estadual / Federal ou cópia autenticada de certificado de conclusão em curso especializado na preparação de peritos, reconhecido pelo Órgão de Classe Profissional, devidamente autenticada" />
                            <br />
                            <cc1:DGTECCheckBox ID="chkDOCNECFOTO" runat="server" Text="Foto tamanho 3x4 recente" />
                        </td>
                        <td>
                            <cc1:DGTECCheckBox ID="chkDOCNECCPF" runat="server" Text="Cópia do CPF" />
                            <br />
                            <cc1:DGTECCheckBox ID="chkDOCNECRES" runat="server" Text="Comprovante de residência e/ou local onde exerce atividade laborativa" />
                            <br />
                            <cc1:DGTECCheckBox ID="chkDOCNECIMP" runat="server" Text="Declaração de que não consta impedimentos profissional ou ética que o impeça de atuar em determinada demanda judicial e se exerce alguma atividade laboral neste Tribunal" />
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <br />
            <cc1:DGTECPanel ID="pnlProfissao" runat="server" Enabled="False" CssClass="caixa-form"
                Width="100%">
                <table class="corpo-form" width="100%">
                    <tr>
                        <th>
                            PROFISSÃO
                        </th>
                    </tr>
                </table>
                <table class="corpo-form" width="100%">
                    <tr>
                        <td>
                            <cc1:DGTECGridView ID="GrdProfissao" runat="server" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="Não há registro" PageSize="4" AllowPaging="true">
                                <Columns>
                                    <asp:BoundField DataField="descr_profissao" HeaderText="Profissão" />
                                    <asp:BoundField DataField="descr_especialidade" HeaderText="Especialidade" />
                                    <asp:BoundField DataField="sigla_per" HeaderText="Sigla" />
                                    <asp:BoundField DataField="uf" HeaderText="UF" />
                                    <asp:BoundField DataField="NUM_REG" HeaderText="Num. registro" />
                                    <asp:TemplateField HeaderText="Alterar">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="imagens\bt_editar.gif" ID="btnGrdAlterarProf" runat="server"
                                                CausesValidation="false" CommandName="btnGrdAlterarProf_Command" OnCommand="btnGrdAlterarProf_Command"
                                                CommandArgument='<%#  eval("ID_PF")&","&  eval("cod_profissao")&","&  eval("cod_especialidade")&","&  eval("sigla_per")&","&  eval("uf")&","&  eval("NUM_REG")&","&  eval("COD_ORGAO_PER")&","&  eval("COD_PERITO")%>'
                                                Text="Alterar" ToolTip="Alterar profissão" Width="30px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirProf" runat="server"
                                                CausesValidation="false" CommandName="btnExcluirProf_Command" OnCommand="btnExcluirProf_Command"
                                                CommandArgument='<%# eval("ID_PF")&","&  eval("cod_profissao")&","&  eval("cod_especialidade")&","&  eval("COD_PERITO") %>'
                                                Text="Excluir" ToolTip="Excluir profissão" Width="30px" OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir a profissão?')" %>>
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            </cc1:DGTECGridView>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="corpo-form" border="0" cellpadding="10" width="100%">
                    <tr>
                        <td>
                            Profissão
                        </td>
                        <td style="padding: 10px">
                            <cc1:DGTECDropDownList ID="CboProfissao" runat="server" Height="19px" Width="340px"
                                AutoPostBack="True">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td>
                            Especialidade
                        </td>
                        <td style="padding: 10px">
                            <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" Height="19px" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td width="46">
                            Órgão
                        </td>
                        <td width="60" style="padding: 10px">
                            <cc1:DGTECDropDownList ID="CboOrgao_Per" runat="server" Width="340px" Height="19px"
                                AutoPostBack="True">
                            </cc1:DGTECDropDownList>
                        </td>
                        <td width="30" align="left">
                            Sigla
                        </td>
                        <td style="padding: 10px">
                            <cc1:DGTECLabel ID="lblSiglaProf" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td width="200px">
                            Número de conselho profissional
                        </td>
                        <td style="width: 60px; padding: 10px">
                            <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Height="17px" Width="70px" AutoPostBack="True"></cc1:DGTECTextBox>
                        </td>
                        <td width="77px">
                            UF
                        </td>
                        <td style="padding: 10px">
                            <cc1:DGTECDropDownList ID="cboUFProf" runat="server" Width="160px" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="right">
                            <cc1:DGTECButton ID="btnGravarProfissao" runat="server" Text="Gravar profissão" />
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <cc1:DGTECPanel ID="PanelDoc" runat="server" Enabled="false" Visible="false" Width="100%">
                <table class="corpo-form" border="0" cellpadding="10" width="100%">
                    <tr>
                        <th align="center">
                            DOCUMENTOS
                        </th>
                    </tr>
                </table>
                <br />
                <cc1:DGTECGridView ID="grdDoc" EmptyDataText="Não há registro" runat="server" AutoGenerateColumns="false"
                    Width="80%" PageSize="4" AllowPaging="true">
                    <Columns>
                        <asp:BoundField DataField="GedId" Visible="true" HeaderText="Documento" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink Target="_blank" ID="linkDocumento" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NomeArquivo" Visible="true" HeaderText="Excluir" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirDoc" runat="server"
                                    CausesValidation="false" CommandName="btnExcluirDoc_Command" CommandArgument='<%# eval("CodigoPerito")&","& eval("GedId")&","& eval("NomeArquivo") %>'
                                    ToolTip="Excluir telefone" OnCommand="btnExcluirDoc_Command" OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir o documento?')" %>>
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                </cc1:DGTECGridView>
                <table>
                    <tr>
                        <td align="right" style="height: 30px">
                            <button id="btnInserirDocumento" type="button" style="margin-right: 20px;">
                                Inserir Documento</button>
                        </td>
                    </tr>
                </table>
            </cc1:DGTECPanel>
            <%--<div style="display: none">
                
                
                <cc1:DGTECLabel ID="lblValidaNome" runat="server"></cc1:DGTECLabel>
                <cc1:DGTECLabel ID="lblValidaCPF" runat="server"></cc1:DGTECLabel>
                <cc1:DGTECLabel ID="lblExcluido" runat="server"></cc1:DGTECLabel>
                <cc1:DGTECButton ID="BtnExcluir" runat="server" />
                <cc1:DGTECButton ID="BtnGravaCurriculum" runat="server" />
                <cc1:DGTECButton ID="BtnExibirCurriculum" runat="server" />
                <cc1:DGTECRadioButtonList ID="RdAtivo" runat="server">
                    <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                </cc1:DGTECRadioButtonList>
                <cc1:DGTECRadioButtonList ID="RdIndic" runat="server">
                    <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                </cc1:DGTECRadioButtonList>
            </div>--%>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="txtCPF" EventName="TextChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="txtCPF" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCNPJ" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtNome" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtNomeFantasia" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboPerito" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="BtnGravar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnLimpar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnSair" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnGravaFoto" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnExibirFoto" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnChklist" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnEndRes" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnEndCom" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnTel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnProfissao" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnInf" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnDocumentos" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNovoEndRes" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGravarTel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNovoEndCom" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur2" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur3" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur4" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur5" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur6" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur7" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur8" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur9" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur10" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur11" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur12" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNur13" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNurMarcarTodos" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNurDesmarcarTodos" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnDadosBancarios" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGravarProfissao" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppC" runat="server" DisplayAfter="20" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <img alt="" src="loading6.gif" style="left: 50%; top: 50%; width: 90px; height: 90px;
                margin-top: -45px; margin-left: -45px; position: absolute;" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- QUINTA TABELA 
    <table border="0" cellpadding="10" width="100%">
        <tr>
            <td>
                
                
                
            </td>
        </tr>
        <tr>
            <td>
                <cc1:DGTECLabel ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" />
            </td>
        </tr>
    </table>
     QUINTA TABELA --%>
    </form>
</asp:Content>
