<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PERMasterPagePerito.Master"
    CodeBehind="frmCadastrarPerito.aspx.vb" Inherits="Interface.frmCadastrarPerito"
    Title="Cadastrar Perito" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePartialRendering="True">
    </asp:ScriptManager>
    <table class="corpo-form" width="100%">
        <tr>
            <td>
                <table class="corpo-form" style="width: 524px;" align="center">
                    <tr>
                        <th align="center">
                            DADOS PESSOAIS
                        </th>
                    </tr>
                </table>
                <table border="0" cellpadding="10" width="519px">
                    <tr>
                        <td style="text-align: center;">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        Pesquisa por nome falhou? Use o CPF
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CPF
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtCPF" runat="server" AutoPostBack="True" Height="17px" MaxLength="11"></cc1:DGTECTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <cc1:DGTECLabel ID="lblValidaCPF" runat="server" ForeColor="Red"></cc1:DGTECLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="60">
                            <table>
                                <tr>
                                    <td>
                                        Nome
                                    </td>
                                    <td>
                                        <cc1:DGTECTextBox ID="txtNome" runat="server" AutoPostBack="True" Width="220px" Height="17px"></cc1:DGTECTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nomes Semelhantes
                                    </td>
                                    <td>
                                        <cc1:DGTECDropDownList ID="CboPerito" runat="server" AutoPostBack="True" Height="19px"
                                            Width="220px">
                                        </cc1:DGTECDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<cc1:DGTECLabel ID="lblValidaNome" runat="server" ForeColor="Red"></cc1:DGTECLabel>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        
                                        <cc1:DGTECTextBox ID="txtID_PF" runat="server" Height="17px" Width="10px" Visible="False"
                                            Enabled="False"></cc1:DGTECTextBox>
                                        <cc1:DGTECTextBox ID="txtCod_Perito" runat="server" Visible="False" Enabled="False"
                                            Height="17px" Width="10px"></cc1:DGTECTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc1:DGTECButton ID="BtnSair" runat="server" Text="Sair" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="10" width="519px">
                    <tr>
                       <td align="center" width="100">
                            Data de Nascimento
                        </td>
                        <td>
                            <asp:TextBox ID="txtDt_Nasc" runat="server" Height="17px" Width="70px"></asp:TextBox>
                        </td>
                        <td><cc1:DGTECLabel ID="lblExcluido" runat="server" Font-Bold="True" ForeColor="#FF3300"></cc1:DGTECLabel></td>
                    </tr>
                </table>
                <table border="0" cellpadding="10" width="519px">
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center" style="width: 100px">
                            <cc1:DGTECButton ID="BtnGravaFoto" runat="server" Text="Gravar Foto" Width="80px" />
                        </td>
                        <td align="center" style="width: 100px">
                            <cc1:DGTECButton ID="BtnExibirFoto" runat="server" Text="Visualizar Foto" Width="80px" />
                        </td>
                        <td align="center" style="width: 100px">
                            <cc1:DGTECButton ID="BtnGravaCurriculum" runat="server" Text="Gravar CV" Width="80px" />
                        </td>
                        <td align="center" style="width: 100px">
                            <cc1:DGTECButton ID="BtnExibirCurriculum" runat="server" Text="Visualizar CV" Width="80px" />
                        </td>
                        <td>
                            <%-- <cc1:DGTECButton ID="BtnSair" runat="server" Text="Sair" Width="80px" />--%>
                            <cc1:DGTECButton ID="BtnChklist" runat="server" Text="ChkList" Width="80px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center" style="width: 100px">
                            <cc1:DGTECButton ID="BtnEndRes" runat="server" Text="End. Res." Width="80px" />
                        </td>
                        <td style="width: 100px">
                            <cc1:DGTECButton ID="BtnEndCom" runat="server" Text="End. Com." Width="80px" />
                        </td>
                        <td style="width: 100px">
                            <cc1:DGTECButton ID="BtnTel" runat="server" Text="Telefones" Width="80px" />
                        </td>
                        <td align="center" style="width: 100px">
                            <cc1:DGTECButton ID="BtnProfissao" runat="server" Text="Profissão" Width="80px" />
                        </td>
                        <td align="center">
                            <cc1:DGTECButton ID="BtnInf" runat="server" Text="Inf. Adicionais" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="10" width="519px">
                    <tr>
                        <td>
                            <cc1:DGTECPanel ID="PanelEndRes" runat="server" Visible="False" Enabled="False" CssClass="caixa-form"
                                Width="517px">
                                <table border="0" cellpadding="10" width="517px">
                                    <tr>
                                        <th align="center">
                                            ENDEREÇO RESIDENCIAL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <cc1:DGTECButton ID="BtnNovoEndRes" runat="server" Text="Novo End. Res." />
                                        </th>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="30">
                                            CEP
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="TxtCEP" runat="server" Height="17px" Width="70px" MaxLength="8"
                                                AutoPostBack="True"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td align="left" style="width: 30px">
                                            UF
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboUF" runat="server" AutoPostBack="True" Height="19px"
                                                Width="40px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                        <td align="center">
                                            Cidade
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboCidade" runat="server" AutoPostBack="True" Height="19px"
                                                Width="392px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="30">
                                            Bairro
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboBairro" runat="server" Height="19px" Width="240px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                        <td>
                                            Tipo Logr.
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboTip_Logr" runat="server" Height="19px" Width="153px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="30">
                                            Logr
                                        </td>
                                        <td style="width: 276px">
                                            <cc1:DGTECTextBox ID="txtNome_Logr" runat="server" Height="17px" Width="275px"></cc1:DGTECTextBox>
                                        </td>
                                        <td style="width: 14px">
                                            Nº
                                        </td>
                                        <td width="40">
                                            <cc1:DGTECTextBox ID="txtNum_Logr" runat="server" Height="17px" Width="40px"></cc1:DGTECTextBox>
                                        </td>
                                        <td style="width: 62px">
                                            Compl. Logr.
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="txtCompl_Logr" runat="server" Height="17px" Width="70px"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </cc1:DGTECPanel>
                            <cc1:DGTECPanel ID="PanelTel" runat="server" Visible="False" Enabled="False" CssClass="caixa-form"
                                Width="517px">
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <th align="center">
                                            TELEFONES
                                        </th>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <cc1:DGTECGridView ID="GrdTel" runat="server" AutoGenerateColumns="false" EmptyDataText="Não há registro"
                                                Width="400px" PageSize="4" AllowPaging="true" OnPageIndexChanging="GrdTel_PageIndexChanging">
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
                                                                CausesValidation="false" CommandName="btnGrdAlterar_Command" CommandArgument='<%#  eval("ID_PF")&","&  eval("seq_tel")%>'
                                                                Text="Alterar" OnCommand="btnGrdAlterar_Command" ToolTip="Alterar telefone">
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Excluir">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirTel" runat="server"
                                                                CausesValidation="false" CommandName="btnExcluirTel_Command" CommandArgument='<%# eval("ID_PF")&","&  eval("seq_tel") %>'
                                                                Text="Excluir" OnCommand="btnExcluirTel_Command" ToolTip="Excluir telefone" OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir o telefone?')" %>>
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            </cc1:DGTECGridView>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="100">
                                            Tipo de Telefone
                                        </td>
                                        <td style="width: 84px">
                                            <cc1:DGTECDropDownList ID="CboTip_Tel" runat="server" Width="90px" AutoPostBack="True">
                                                <asp:ListItem Value="1">Residencial</asp:ListItem>
                                                <asp:ListItem Value="2">Profissional</asp:ListItem>
                                                <asp:ListItem Value="4">Celular</asp:ListItem>
                                            </cc1:DGTECDropDownList>
                                        </td>
                                        <td width="27">
                                            Tel : (
                                        </td>
                                        <td width="31">
                                            <cc1:DGTECTextBox ID="txtDDD" runat="server" Height="17px" Width="32px"></cc1:DGTECTextBox>
                                        </td>
                                        <td style="width: 4px">
                                            )
                                        </td>
                                        <td style="width: 129px">
                                            <cc1:DGTECTextBox ID="TxtTel" runat="server" Height="17px" AutoPostBack="True"></cc1:DGTECTextBox>
                                        </td>
                                        <td width="37px">
                                            <cc1:DGTECLabel ID="lblRamal" runat="server" Text="Ramal" />
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="TxtRamal" runat="server" Height="17px" Width="60px" AutoPostBack="True" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr align="right">
                                        <td>
                                            <cc1:DGTECButton ID="btnGravarTel" runat="server" Text="Gravar Telefone" />
                                        </td>
                                    </tr>
                                </table>
                            </cc1:DGTECPanel>
                            <cc1:DGTECPanel ID="PanelEndCom" runat="server" Visible="False" Enabled="False" CssClass="caixa-form"
                                Width="517px">
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <th align="center">
                                            ENDEREÇO COMERCIAL
                                            <cc1:DGTECButton ID="BtnNovoEndCom" runat="server" Text="Novo End. Com." />
                                        </th>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="30">
                                            CEP
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="TxtCEP1" runat="server" Height="17px" Width="70px" MaxLength="8"
                                                AutoPostBack="True"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td align="left" style="width: 30px">
                                            UF
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboUF1" runat="server" AutoPostBack="True" Height="19px"
                                                Width="40px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                        <td align="center">
                                            Cidade
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboCidade1" runat="server" AutoPostBack="True" Height="19px"
                                                Width="392px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="30">
                                            Bairro
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboBairro1" runat="server" Height="19px" Width="240px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                        <td>
                                            Ttipo Logr.
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboTip_Logr1" runat="server" Height="17px" Width="150px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="30">
                                            Logr.
                                        </td>
                                        <td style="width: 272px">
                                            <cc1:DGTECTextBox ID="txtNome_Logr1" runat="server" Height="17px" Width="275px"></cc1:DGTECTextBox>
                                        </td>
                                        <td>
                                            Nº
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="txtNum_Logr1" runat="server" Height="17px" Width="40px"></cc1:DGTECTextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Compl. Logr.
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="txtCompl_Logr1" runat="server" Height="17px" Width="70px"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </cc1:DGTECPanel>
                            <cc1:DGTECPanel ID="PanelInf" runat="server" Visible="False" Enabled="False" CssClass="caixa-form"
                                Width="517px">
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <th align="center">
                                            INFORMAÇÕES ADICIONAIS
                                        </th>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td align="center" style="width: 108px">
                                            Situação do Perito
                                        </td>
                                        <td>
                                            <cc1:DGTECRadioButtonList ID="RdAtivo" runat="server" Width="97px" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                                                <asp:ListItem Value="0">Inativo</asp:ListItem>
                                            </cc1:DGTECRadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td style="text-align: center">
                                            Nurs
                                            <br />
                                            de
                                            <br />
                                            Atuação
                                        </td>
                                        <td id="Btn" width="455" height="40">
                                            <cc1:DGTECButton ID="BtnNur1" runat="server" Text="1º NUR" TabIndex="1" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur2" runat="server" Text="2º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur3" runat="server" Text="3º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur4" runat="server" Text="4º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur5" runat="server" Text="5º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur6" runat="server" Text="6º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur7" runat="server" Text="7º NUR" Width="60px" />
                                            <br />
                                            <cc1:DGTECButton ID="BtnNur8" runat="server" Text="8º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur9" runat="server" Text="9º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur10" runat="server" Text="10º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur11" runat="server" Text="11º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur12" runat="server" Text="12º NUR" Width="60px" />
                                            <cc1:DGTECButton ID="BtnNur13" runat="server" Text="13º NUR" Width="60px" />
                                        </td>
                                    </tr>
                                </table>
                                 <table border="0">
                                                <tr>
                                                    <td align="center" style="width: 509px">
                                                        <cc1:DGTECButton ID="btnNurMarcarTodos" runat="server" 
                                                            Text="Marcar Todos Nur" />
                                                        <cc1:DGTECButton ID="btnNurDesmarcarTodos" runat="server" 
                                                            Text="Desmarcar Todos Nur" />
                                                    </td>
                                                </tr>
                                            </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="30" align="center">
                                            Email
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="ValidarEmail" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="Email Inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            <br />
                                            <cc1:DGTECTextBox ID="txtEmail" runat="server" Height="17px" Width="175px"></cc1:DGTECTextBox>
                                        </td>
                                        <td width="85">
                                            Email Alternativo
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="ValidarEmail1" runat="server" ControlToValidate="txtEmail1"
                                                ErrorMessage="Email Inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            <br />
                                            <cc1:DGTECTextBox ID="txtEmail1" runat="server" Height="17px" Width="175px"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td>
                                            &nbsp; Cadastro
                                        </td>
                                        <td>
                                            Origem&nbsp;
                                        </td>
                                        <td>
                                            <cc1:DGTECRadioButtonList ID="RdIndic" runat="server">
                                                <asp:ListItem Selected="True" Value="OptDIPEJ">DIPEJ</asp:ListItem>
                                                <asp:ListItem Value="OptJuiz">Juiz</asp:ListItem>
                                            </cc1:DGTECRadioButtonList>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp; Data &nbsp;
                                        </td>
                                        <td>
                                            <uc2:CtlData1 ID="txtData_Cadastramento" runat="server" />
                                        </td>
                                        <td align="center">
                                            Situação
                                        </td>
                                        <td>
                                            <cc1:DGTECRadioButtonList ID="RdSit" runat="server" Width="97px">
                                                <asp:ListItem Value="OptOK">OK</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="OptPendente">Pendente</asp:ListItem>
                                            </cc1:DGTECRadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="80">
                                            Banco
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboBanco" runat="server" Height="19px" Width="350px">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td style="width: 73px">
                                            Agência&nbsp;
                                        </td>
                                        <td width="80">
                                            <cc1:DGTECTextBox ID="TxtNum_Agencia" runat="server" Height="17px" Width="80px" MaxLength="6"></cc1:DGTECTextBox>
                                        </td>
                                        <td style="width: 129px">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Nome da Agência&nbsp;
                                        </td>
                                        <td width="100">
                                            <cc1:DGTECTextBox ID="txtNome_Agencia" runat="server" Height="17px" Width="185px"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="80">
                                            &nbsp;Conta Corrente&nbsp;
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="txtNum_Conta_Corrente" runat="server" Height="17px" MaxLength="10"></cc1:DGTECTextBox>
                                            &nbsp;
                                            <cc1:DGTECButton ID="BtnDadosBancarios" runat="server" Text="Inserir dados bancários"
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <td width="80">
                                            Falta Entregar
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="txtFalta_Entregar" runat="server" Height="35px" Width="420px"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </cc1:DGTECPanel>
                            <cc1:DGTECPanel ID="PanelChklist" runat="server" Visible="False" Enabled="False"
                                CssClass="caixa-form" Width="517px">
                                <table border="0" cellpadding="10" width="519px">
                                    <tr>
                                        <th align="center">
                                            MARQUE OS DOCUMENTOS ENTREGUES A DIPEJ
                                        </th>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="10" width="519px">
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
                            <cc1:DGTECPanel ID="pnlProfissao" runat="server" Visible="False" Enabled="False"
                                CssClass="caixa-form" Width="100%">
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
                                                EmptyDataText="Não há registro" PageSize="4" AllowPaging="true" OnPageIndexChanging="GrdProfissao_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="descr_profissao" HeaderText="Profissão" />
                                                    <asp:BoundField DataField="descr_especialidade" HeaderText="Especialidade" />
                                                    <asp:BoundField DataField="sigla_per" HeaderText="Sigla" />
                                                    <asp:BoundField DataField="uf" HeaderText="UF" />
                                                    <asp:BoundField DataField="NUM_REG" HeaderText="Num. registro" />
                                                    <asp:TemplateField HeaderText="Alterar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="imagens\bt_editar.gif" ID="btnGrdAlterarProf" runat="server"
                                                                CausesValidation="false" CommandName="btnGrdAlterarProf_Command" CommandArgument='<%#  eval("ID_PF")&","&  eval("cod_profissao")&","&  eval("cod_especialidade")&","&  eval("sigla_per")&","&  eval("uf")&","&  eval("NUM_REG")&","&  eval("COD_ORGAO_PER")%>'
                                                                Text="Alterar" OnCommand="btnGrdAlterarProf_Command" ToolTip="Alterar profissão"
                                                                Width="30px"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Excluir">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirProf" runat="server"
                                                                CausesValidation="false" CommandName="btnExcluirProf_Command" CommandArgument='<%# eval("ID_PF")&","&  eval("cod_profissao")&","&  eval("cod_especialidade") %>'
                                                                Text="Excluir" OnCommand="btnExcluirProf_Command" ToolTip="Excluir profissão"
                                                                Width="30px" OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir a profissão?')" %>>
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
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboProfissao" runat="server" Height="19px" Width="200px"
                                                AutoPostBack="True">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                        <td>
                                            Especialidade
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" Height="19px" Width="200px" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="46">
                                            Órgão
                                        </td>
                                        <td width="60">
                                            <cc1:DGTECDropDownList ID="CboOrgao_Per" runat="server" Width="340px" Height="19px"
                                                AutoPostBack="True">
                                            </cc1:DGTECDropDownList>
                                        </td>
                                        <td width="30" align="right">
                                            Sigla
                                        </td>
                                        <td>
                                            <cc1:DGTECLabel ID="lblSiglaProf" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td width="175px">
                                            Número de conselho profissional
                                        </td>
                                        <td style="width: 60px">
                                            <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Height="17px" Width="70px" AutoPostBack="True"></cc1:DGTECTextBox>
                                        </td>
                                        <td width="77px" align="right">
                                            UF
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="cboUFProf" runat="server" Width="60px" />
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:DGTECLabel ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" />
                        </td>
                    </tr>
                </table>
    </td> </tr> </table>
    </form>
</asp:Content>
