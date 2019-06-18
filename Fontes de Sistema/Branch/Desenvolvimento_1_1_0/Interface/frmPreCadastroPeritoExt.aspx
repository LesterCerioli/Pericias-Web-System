<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPageExt.Master"
    CodeBehind="frmPreCadastroPeritoExt.aspx.vb" Inherits="Interface.frmPreCadastroPeritoExt"
    Title="Cadastro de Peritos"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePartialRendering="True">
    </asp:ScriptManager>
    <div>
        <table border="0" cellpadding="0" width="100%" class="corpo-form">
            <tr>
                <th>
                    DADOS PESSOAIS
                </th>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="upPrincipal" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td>
                                        <table border="0">
                                            <tr>
                                                <td style="width: 33px; height: 23px;">
                                                    CPF:
                                                </td>
                                                <td width="90px" style="height: 23px">
                                                    <cc1:DGTECTextBox ID="txtCPF" runat="server" Height="17px" AutoPostBack="True" Width="90px"></cc1:DGTECTextBox>
                                                    <cc2:MaskedEditExtender ID="mskdTxtCPF" runat="server" Mask="99999999999" MaskType="Number"
                                                        TargetControlID="txtCPF" ClearTextOnInvalid="True">
                                                    </cc2:MaskedEditExtender>
                                                </td>
                                                <td align="center" style="width: 46px; height: 23px;">
                                                    Nome:
                                                </td>
                                                <td style="width: 276px; height: 23px;">
                                                    <cc1:DGTECTextBox ID="txtNome" runat="server" Height="17px" Width="220px"></cc1:DGTECTextBox>
                                                </td>
                                                <td  rowspan="0" style="height: 25px">
                                                    Data de nascimento:
                                                    <asp:TextBox ID="txtDt_Nasc" runat="server" Height="17px" Width="70px"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="mskDtNascimento" runat="server" AcceptNegative="Left"
                                                        AutoComplete="False" ClearTextOnInvalid="True" DisplayMoney="Left" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtDt_Nasc" UserDateFormat="DayMonthYear">
                                                    </cc2:MaskedEditExtender>
                                                </td>
                                            </tr>
                                          <%--  <tr>
                                                <td colspan="4" rowspan="0" style="height: 25px">
                                                    Data de nascimento:
                                                    <asp:TextBox ID="txtDt_Nasc" runat="server" Height="17px" Width="70px"></asp:TextBox>
                                                    <cc2:MaskedEditExtender ID="mskDtNascimento" runat="server" AcceptNegative="Left"
                                                        AutoComplete="False" ClearTextOnInvalid="True" DisplayMoney="Left" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtDt_Nasc" UserDateFormat="DayMonthYear">
                                                    </cc2:MaskedEditExtender>
                                                </td>
                                            </tr>--%>
                                        </table>
                                       </td>
                                    <td>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="80px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="upTab" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc2:TabContainer ID="TabContainer1" runat="server" Width="600px" Height="240px"
                                AutoPostBack="True" ActiveTabIndex="4">
                                <cc2:TabPanel runat="server" HeaderText="Endereço residencial" ID="tbpEndRes" TabIndex="0">
                                    <HeaderTemplate>
                                        Endereço residencial
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="590px" height="240px" border="0" cellpadding="0" class="tab-txt-peq">
                                            <tr valign="top">
                                                <td>
                                                    <br />
                                                    <table width="590px">
                                                        <tr>
                                                            <td width="40">
                                                                CEP:
                                                            </td>
                                                            <td style="width: 96px">
                                                                <cc1:DGTECTextBox ID="txtCEP" runat="server" AutoPostBack="True" Height="17px" MaxLength="8"
                                                                    Width="70px"></cc1:DGTECTextBox><cc2:MaskedEditExtender ID="mskTxtCepResidencial"
                                                                        runat="server" Mask="99999-999" MaskType="Number" TargetControlID="TxtCep" CultureAMPMPlaceholder=""
                                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                        Enabled="True">
                                                                    </cc2:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40">
                                                                UF:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECDropDownList ID="CboUF" runat="server" AutoPostBack="True" Height="19px"
                                                                    Width="40px">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                            <td>
                                                                Cidade:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECDropDownList ID="CboCidade" runat="server" AutoPostBack="True" Height="19px"
                                                                    Width="315px">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr>
                                                            <td width="40">
                                                                Bairro:
                                                            </td>
                                                            <td style="width: 300px">
                                                                <cc1:DGTECDropDownList ID="CboBairro" runat="server" Height="19px" Width="230px">
                                                                </cc1:DGTECDropDownList>
                                                                Tipo Logr.:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECDropDownList ID="CboTip_Logr" runat="server" Height="19px" Width="162px">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr>
                                                            <td width="40" colspan="0" rowspan="0">
                                                                Logr.:
                                                            </td>
                                                            <td style="width: 240px">
                                                                <cc1:DGTECTextBox ID="txtNome_Logr" runat="server" Height="17px" Width="240px"></cc1:DGTECTextBox>
                                                            </td>
                                                            <td>
                                                                Nº:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECTextBox ID="txtNum_Logr" runat="server" Height="17px" Width="40px"></cc1:DGTECTextBox>
                                                            </td>
                                                            <td style="width: 80px">
                                                                Compl. Logr.:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECTextBox ID="txtCompl_Logr" runat="server" Height="17px" Width="80px"></cc1:DGTECTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel ID="tbpEndCom" runat="server" HeaderText="Endereço Comercial" TabIndex="1">
                                    <HeaderTemplate>
                                        Endereço Comercial
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="590px" border="0" height="240px" cellpadding="0" class="tab-txt-peq">
                                            <tr valign="top">
                                                <td>
                                                    <br />
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr>
                                                            <td width="40">
                                                                CEP:
                                                            </td>
                                                            <td style="width: 96px">
                                                                <cc1:DGTECTextBox ID="txtCEP1" runat="server" AutoPostBack="True" Height="17px" Width="70px"></cc1:DGTECTextBox><cc2:MaskedEditExtender
                                                                    ID="mskTxtCepComercial" runat="server" TargetControlID="txtCEP1" Mask="99999-999"
                                                                    MaskType="Number" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40">
                                                                UF:
                                                            </td>
                                                            <td style="width: 40px">
                                                                <cc1:DGTECDropDownList ID="CboUF1" runat="server" AutoPostBack="True" Height="19px"
                                                                    Width="40px">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                            <td>
                                                                Cidade:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECDropDownList ID="CboCidade1" runat="server" AutoPostBack="True" Height="19px"
                                                                    Width="315px">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr>
                                                            <td width="40" colspan="0" rowspan="0">
                                                                Bairro:
                                                            </td>
                                                            <td style="width: 300px">
                                                                <cc1:DGTECDropDownList ID="CboBairro1" runat="server" Height="19px" Width="230px">
                                                                </cc1:DGTECDropDownList>
                                                                Tipo Logr.:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECDropDownList ID="CboTip_Logr1" runat="server" Height="17px" Width="162px">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr>
                                                            <td width="40" colspan="0" rowspan="0">
                                                                Logr.:
                                                            </td>
                                                            <td style="width: 240px">
                                                                <cc1:DGTECTextBox ID="txtNome_Logr1" runat="server" Height="17px" Width="240px"></cc1:DGTECTextBox>
                                                            </td>
                                                            <td>
                                                                Nº:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECTextBox ID="txtNum_Logr1" runat="server" Height="17px" Width="40px"></cc1:DGTECTextBox>
                                                            </td>
                                                            <td style="width: 80px">
                                                                Compl. Logr.
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECTextBox ID="txtCompl_Logr1" runat="server" Height="17px" Width="80px"></cc1:DGTECTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" HeaderText="Telefones" ID="tbpTelefone" TabIndex="2">
                                    <HeaderTemplate>
                                        Telefone
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="590px" height="240px" border="0" cellpadding="0" class="tab-txt-peq">
                                            <tr valign="top">
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="500px">
                                                                <cc1:DGTECGridView ID="GrdTel" runat="server" AutoGenerateColumns="False" EmptyDataText="Não há registro"
                                                                    Width="500px" PageSize="3" AllowPaging="True" OnPageIndexChanging="GrdTel_PageIndexChanging"
                                                                    CssClass="corpo-form">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="desc_tip_tel" HeaderText="Tipo Telefone" />
                                                                        <asp:BoundField DataField="cod_tip_tel" HeaderText="Cod. tipo tel" Visible="False" />
                                                                        <asp:BoundField DataField="ddd" HeaderText="DDD" />
                                                                        <asp:BoundField DataField="Num_tel" HeaderText="Número" />
                                                                        <asp:BoundField DataField="num_ramal" HeaderText="Ramal" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirTel" runat="server"
                                                                                    CausesValidation="false" CommandName="btnExcluirTel_Command" CommandArgument='<%# eval("Num_tel")%>'
                                                                                    Text="Excluir" OnCommand="btnExcluirTel_Command" ToolTip="Excluir telefone" OnClientClick=<%# "javascrit:return window.confirm('Tem certeza que deseja excluir o telefone?')" %>>
                                                                                </asp:ImageButton></ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                </cc1:DGTECGridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="10" width="590px">
                                                        <tr>
                                                            <td width="30">
                                                                Tipo:
                                                            </td>
                                                            <td style="width: 90px">
                                                                <cc1:DGTECDropDownList ID="cboCodTipTel" runat="server" Width="90px" AutoPostBack="True">
                                                                    <asp:ListItem>Selecione</asp:ListItem>
                                                                    <asp:ListItem Value="1">Residencial</asp:ListItem>
                                                                    <asp:ListItem Value="2">Profissional</asp:ListItem>
                                                                    <asp:ListItem Value="4">Celular</asp:ListItem>
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                            <td width="35">
                                                                Tel : (
                                                            </td>
                                                            <td width="23">
                                                                <cc1:DGTECTextBox ID="txtDDD" runat="server" Height="17px" Width="22px" MaxLength="2"></cc1:DGTECTextBox>
                                                                <cc2:MaskedEditExtender ID="mskTxtDDD" runat="server" Mask="99" MaskType="Number"
                                                                    TargetControlID="txtDDD" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                            <td style="width: 4px">
                                                                )
                                                            </td>
                                                            <td style="width: 73px">
                                                                <cc1:DGTECTextBox ID="txtTel" runat="server" Height="17px" Width="70px"></cc1:DGTECTextBox><cc2:MaskedEditExtender
                                                                    ID="msktxtTel" runat="server" TargetControlID="txtTel" MaskType="Number" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" Mask="9999-9999">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                            <td width="40px">
                                                                <cc1:DGTECLabel ID="lblRamal" runat="server" Text="Ramal: " /><cc2:MaskedEditExtender
                                                                    ID="mskTxtRamal" runat="server" TargetControlID="txtRamal" MaskType="Number"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" Mask="9999">
                                                                </cc2:MaskedEditExtender>
                                                            </td>
                                                            <td style="width: 73px">
                                                                <cc1:DGTECTextBox ID="TxtRamal" runat="server" Height="17px" Width="40px" AutoPostBack="True" />
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECButton ID="btnGravarTel" runat="server" Text="Inserir" Width="75px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" HeaderText="Profissão" ID="TbpProfissao" TabIndex="3">
                                    <HeaderTemplate>
                                        Profissão
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="590px" height="240px" class="tab-txt-peq">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr>
                                                            <td>
                                                                <cc1:DGTECGridView ID="GrdProfissao" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                    EmptyDataText="Não há registro" PageSize="2" AllowPaging="True" OnPageIndexChanging="GrdProfissao_PageIndexChanging"
                                                                    CssClass="corpo-form">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="descr_profissao" HeaderText="Profissão" />
                                                                        <asp:BoundField DataField="descr_especialidade" HeaderText="Especialidade" />
                                                                        <asp:BoundField DataField="sigla_per" HeaderText="Sigla" />
                                                                        <asp:BoundField DataField="uf" HeaderText="UF" />
                                                                        <asp:BoundField DataField="NUM_REG" HeaderText="Num. registro" />
                                                                        <asp:TemplateField HeaderText="Excluir">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ImageUrl="imagens\bt_excluir2.gif" ID="btnExcluirProf" runat="server"
                                                                                    CausesValidation="false" CommandName="btnExcluirProf_Command" OnCommand="btnExcluirProf_Command"
                                                                                    CommandArgument='<%# eval("cod_especialidade") %>' Text="Excluir" ToolTip="Excluir profissão"
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
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr class="tab-txt-peq">
                                                            <td  width="90px">
                                                                Profissão:
                                                            </td>
                                                            <td width="185">
                                                                <cc1:DGTECDropDownList ID="CboProfissao" runat="server" Height="19px" Width="185px"
                                                                    AutoPostBack="True">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                            <td>
                                                                Especialidade:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" Height="19px" Width="195px"
                                                                    AutoPostBack="True">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr class="tab-txt-peq">
                                                            <td width="90px">
                                                                Órgão:
                                                            </td>
                                                            <td Width="300px">
                                                                <cc1:DGTECDropDownList ID="CboOrgao_Per" runat="server" Width="290px" Height="19px"
                                                                    AutoPostBack="True">
                                                                </cc1:DGTECDropDownList>
                                                            </td>
                                                            <td Width="35px"  >
                                                                Sigla:
                                                            </td>
                                                            <td align="left" >
                                                                <cc1:DGTECLabel ID="lblSiglaProf" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr class="tab-txt-peq">
                                                            <td width="170px">
                                                                Número do registro:
                                                            </td>
                                                            <td style="width: 84px" width="390px">
                                                                <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Height="17px" Width="70px"></cc1:DGTECTextBox>
                                                            </td>
                                                            <td style="width: 33px" width="590px">
                                                                UF:
                                                            </td>
                                                            <td width="590px">
                                                                <cc1:DGTECDropDownList ID="drpUFProf" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr align="right">
                                                            <td>
                                                                <cc1:DGTECButton ID="btnAtualizarProf" runat="server" Text="Inserir" Width="80px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                                <cc2:TabPanel runat="server" HeaderText="Inf. Adicionais" ID="tbpInfAdicionais" TabIndex="4">
                                    <HeaderTemplate>
                                        Inf. adicionais
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table width="590px" height="240px" border="0" cellpadding="0">
                                            <tr valign="top">
                                                <td >
                                                    <br />
                                                    <table border="0" cellpadding="0" width="590px">
                                                        <tr class="tab-txt-peq">
                                                            <td style="text-align: center" width="60px" >
                                                                Nurs
                                                                <br />
                                                                de
                                                                <br />
                                                                Atuação
                                                            </td>
                                                            <td id="Btn" width="52px" height="40" align="center">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur1" runat="server" Text="1º NUR" TabIndex="1" Width="60px"
                                                                                OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=1',500,550,'Nur1',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur2" runat="server" Text="2º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=2',500,550,'Nur2',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur3" runat="server" Text="3º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=3',500,550,'Nur3',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur4" runat="server" Text="4º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=4',500,550,'Nur4',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur5" runat="server" Text="5º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=5',500,550,'Nur5',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur6" runat="server" Text="6º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=6',500,550,'Nur6',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur7" runat="server" Text="7º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=7',500,550,'Nur7',true);" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur8" runat="server" Text="8º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=8',500,550,'Nur8',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur9" runat="server" Text="9º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=9',500,550,'Nur9',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur10" runat="server" Text="10º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=10',500,550,'Nur10',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur11" runat="server" Text="11º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=11',500,550,'Nur11',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur12" runat="server" Text="12º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=12',500,550,'Nur12',true);" />
                                                                        </td>
                                                                        <td>
                                                                            <cc1:DGTECButton ID="BtnNur13" runat="server" Text="13º NUR" Width="60px" OnClientClick="javascript:AbrePoup('frmComarcaPer.aspx?n=13',500,550,'Nur13',true);" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0">
                                                        <tr>
                                                            <td align="center" style="width: 590px">
                                                                <cc1:DGTECButton ID="btnNurMarcarTodos" runat="server" Text="Marcar Todas Nur" />
                                                                <cc1:DGTECButton ID="btnNurDesmarcarTodos" runat="server" Text="Desmarcar Todas Nur" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellpadding="10" width="590px">
                                                        <tr align="center">
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:RegularExpressionValidator ID="ValidarEmail" runat="server" ControlToValidate="txtEmail"
                                                                    ErrorMessage="Email Inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:RegularExpressionValidator ID="ValidarEmail1" runat="server" ControlToValidate="txtEmail1"
                                                                    ErrorMessage="Email Inválido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr class="tab-txt-peq">
                                                            <td width="30">
                                                                Email:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECTextBox ID="txtEmail" runat="server" Height="17px" Width="175px"></cc1:DGTECTextBox>
                                                            </td>
                                                            <td width="110">
                                                                Email Alternativo:
                                                            </td>
                                                            <td>
                                                                <cc1:DGTECTextBox ID="txtEmail1" runat="server" Height="17px" Width="175px"></cc1:DGTECTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc2:TabPanel>
                            </cc2:TabContainer>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdateProgress ID="uppTab" runat="server" AssociatedUpdatePanelID="upTab" DisplayAfter="20">
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="uppPrincipal" runat="server" AssociatedUpdatePanelID="upPrincipal"
            DisplayAfter="20">
        </asp:UpdateProgress>
    </div>
    </form>
</asp:Content>
