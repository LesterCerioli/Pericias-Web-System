<%@ Page Title="Cadastrar Perito" Language="vb" MasterPageFile="~/PERMasterPagePerito.Master"
    CodeBehind="frmCadastrarPerito.aspx.vb" Inherits="Interface.frmCadastrarPerito"
    AutoEventWireup="false" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">

    <form id="frmCadastrarPerito" runat="server" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    <div>
        <table class="corpo-form">
            <tr>
                <th align="center">
                    DADOS PESSOAIS
                </th>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td style="text-align: center;">
                                <br />
                                <br />
                                <br />
                                CPF&nbsp;
                            </td>
                            <td width="100" align="center">
                                &nbsp;Pesquisa por nome falhou? Use o CPF
                                <cc1:DGTECTextBox ID="txtCPF" runat="server" AutoPostBack="True" Height="17px" MaxLength="11"></cc1:DGTECTextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="ValidarCPF" runat="server" ControlToValidate="txtCPF"
                                    ErrorMessage="Preencher CPF">Preencher CPF</asp:RequiredFieldValidator>
                            </td>
                            <td width="60">
                                Nome<br />
                                <br />
                                Nomes Semelhantes
                            </td>
                            <td width="290">
                                <cc1:DGTECTextBox ID="txtNome" runat="server" AutoPostBack="True" Width="220px" Height="17px"></cc1:DGTECTextBox>
                                <cc1:DGTECTextBox ID="txtID_PF" runat="server" Height="17px" Width="10px" Visible="False"></cc1:DGTECTextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="ValidarNome" runat="server" ControlToValidate="txtNome"
                                    ErrorMessage="Preencher o Nome">Preencher o Nome</asp:RequiredFieldValidator>
                                <cc1:DGTECLabel ID="lblExcluido" runat="server"></cc1:DGTECLabel>
                                <br />
                                <cc1:DGTECDropDownList ID="CboPerito" runat="server" AutoPostBack="True" Height="19px"
                                    Width="220px">
                                </cc1:DGTECDropDownList>
                                <cc1:DGTECTextBox ID="txtCod_Perito" runat="server" Visible="False" Height="17px"
                                    Width="10px"></cc1:DGTECTextBox>
                            </td>
                            <td>
                                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="50px" />
                                <br />
                                <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="50px" />
                                <br />
                                <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="50px" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td width="40">
                                Orgão
                            </td>
                            <td width="60">
                                <cc1:DGTECDropDownList ID="CboOrgao_Per" runat="server" Width="200px" Height="19px">
                                </cc1:DGTECDropDownList>
                            </td>
                            <td width="70">
                                Número Reg.
                            </td>
                            <td width="60">
                                <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Height="17px" Width="70px" AutoPostBack="True"></cc1:DGTECTextBox>
                            </td>
                            <td align="center" width="70">
                                Dt. Nasc.
                            </td>
                            <td>
                                <asp:TextBox ID="txtDt_Nasc" runat="server" Height="17px" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr align="center">
                            <td align="center">
                                <cc1:DGTECButton ID="BtnGravaFoto" runat="server" Text="Gravar Foto" Width="80px" />
                            </td>
                            <td align="center">
                                <cc1:DGTECButton ID="BtnExibirFoto" runat="server" Text="Visualizar Foto" Width="80px" />
                            </td>
                            <td align="center" style="width: 101px">
                                <cc1:DGTECButton ID="BtnGravaCurriculum" runat="server" Text="Gravar CV" Width="80px" />
                            </td>
                            <td align="center">
                                <cc1:DGTECButton ID="BtnExibirCurriculum" runat="server" Text="Visualizar CV" Width="99px" />
                            </td>
                            <td>
                                &nbsp;<cc1:DGTECButton ID="BtnSair" runat="server" Text="Sair" Width="80px" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
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
                <td style="width: 100px">
                    <cc1:DGTECButton ID="BtnInf" runat="server" Text="Inf. Adicionais" Width="81px" />
                </td>
                <td align="center">
                    <cc1:DGTECButton ID="BtnChklist" runat="server" Text="ChkList" Width="80px" />
                </td>
            </tr>
        </table>
        <cc1:DGTECPanel ID="PanelEndRes" runat="server" Visible="False" Enabled="False">
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
                        ENDEREÇO RESIDENCIAL
                        <cc1:DGTECButton ID="BtnNovoEndRes" runat="server" Text="Novo End. Res." />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
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
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
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
                            Width="400px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td>
                        Bairro
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="CboBairro" runat="server" Height="19px" Width="240px">
                        </cc1:DGTECDropDownList>
                    </td>
                    <td>
                        Ttipo Logr.
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="CboTip_Logr" runat="server" Height="19px" Width="150px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td>
                        Logr.
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNome_Logr" runat="server" Height="17px" Width="290px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        Nº
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNum_Logr" runat="server" Height="17px" Width="40px"></cc1:DGTECTextBox>
                    </td>
                    <td style="width: 17px">
                        Compl. Logr.
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtCompl_Logr" runat="server" Height="17px" Width="100px"></cc1:DGTECTextBox>
                    </td>
                </tr>
            </table>
        </cc1:DGTECPanel>
        <cc1:DGTECPanel ID="PanelTel" runat="server" Visible="False" Enabled="False">
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
                        TELEFONES
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td width="100">
                        Tipo de Telefone
                    </td>
                    <td style="width: 84px">
                        <cc1:DGTECDropDownList ID="CboTip_Tel" runat="server" Width="90px">
                            <asp:ListItem Value="1">Residencial</asp:ListItem>
                            <asp:ListItem Value="2">Profissional</asp:ListItem>
                            <asp:ListItem Value="5">Celular</asp:ListItem>
                        </cc1:DGTECDropDownList>
                    </td>
                    <td width="27">
                        Tel : (
                    </td>
                    <td width="31">
                        <cc1:DGTECTextBox ID="txtDDD" runat="server" Height="17px" Width="32px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        )
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="TxtTel" runat="server" Height="17px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td width="100">
                        Tipo de Telefone
                    </td>
                    <td style="width: 84px">
                        <cc1:DGTECDropDownList ID="CboTip_Tel1" runat="server" Width="90px">
                            <asp:ListItem Value="1">Residencial</asp:ListItem>
                            <asp:ListItem Value="2" Selected="True">Profissional</asp:ListItem>
                            <asp:ListItem Value="5">Celular</asp:ListItem>
                        </cc1:DGTECDropDownList>
                    </td>
                    <td width="27">
                        Tel : (
                    </td>
                    <td width="31">
                        <asp:TextBox ID="txtDDD1" runat="server" Height="17px" Width="32px"></asp:TextBox>
                    </td>
                    <td>
                        )
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtTel1" runat="server" Height="17px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        Ramal
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="TxtRamal" runat="server" Height="17px" Width="60px">
                        </cc1:DGTECTextBox>
                    </td>
                </tr>
            </table>
        </cc1:DGTECPanel>
        <cc1:DGTECPanel ID="PanelEndCom" runat="server" Visible="False" Enabled="False">
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
                        ENDEREÇO COMERCIAL
                        <cc1:DGTECButton ID="BtnNovoEndCom" runat="server" Text="Novo End. Com." />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
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
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
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
                            Width="400px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td>
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
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td>
                        Logr
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNome_Logr1" runat="server" Height="17px" Width="290px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        Nº
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNum_Logr1" runat="server" Height="17px" Width="40px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        Compl. Logr.
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtCompl_Logr1" runat="server" Height="17px" Width="100px"></cc1:DGTECTextBox>
                    </td>
                </tr>
            </table>
        </cc1:DGTECPanel>
        <cc1:DGTECPanel ID="PanelInf" runat="server" Visible="False" Enabled="False">
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
                        INFORMAÇÕES ADICIONAIS
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
                        Situação do Perito
                    </td>
                    <td>
                        <cc1:DGTECRadioButtonList ID="RdAtivo" runat="server" Width="97px">
                            <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                            <asp:ListItem Value="0">Inativo</asp:ListItem>
                        </cc1:DGTECRadioButtonList>
                    </td>
                    <td>
                        Profissão<br />
                        EspEspecialidade<br />
                        <br />
                        Profissão1<br />
                        EspEspecialidade1
                    </td>
                    <td>
                        <cc1:DGTECDropDownList ID="CboProfissao" runat="server" Height="19px" Width="270px">
                        </cc1:DGTECDropDownList>
                        <br />
                        <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" Height="19px" Width="270px">
                        </cc1:DGTECDropDownList>
                        <br />
                        <br />
                        <cc1:DGTECDropDownList ID="CboProfissao1" runat="server" Height="19px" Width="270px">
                        </cc1:DGTECDropDownList>
                        <br />
                        <cc1:DGTECDropDownList ID="CboEspecialidade1" runat="server" Height="19px" Width="270px">
                        </cc1:DGTECDropDownList>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
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
            <table border="0" cellpadding="10" width="520">
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
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td>
                        Cadastro
                    </td>
                    <td>
                        Origem
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
            <table border="0" cellpadding="10" width="520">
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
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td width="80">
                        Agência
                    </td>
                    <td width="80">
                        <cc1:DGTECTextBox ID="TxtNum_Agencia" runat="server" Height="17px" Width="80px"></cc1:DGTECTextBox>
                    </td>
                    <td>
                        Nome da Agência
                    </td>
                    <td width="100">
                        <cc1:DGTECTextBox ID="txtNome_Agencia" runat="server" Height="17px" Width="200px"></cc1:DGTECTextBox>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td>
                        Conta Corrente
                    </td>
                    <td>
                        <cc1:DGTECTextBox ID="txtNum_Conta_Corrente" runat="server" Height="17px"></cc1:DGTECTextBox>
                        <cc1:DGTECButton ID="BtnDadosBancarios" runat="server" Text="Inserir dados bancários"
                            Visible="False" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
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
        <cc1:DGTECPanel ID="PanelChklist" runat="server" Visible="False" Enabled="False">
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td align="center">
                        MARQUE OS DOCUMENTOS ENTREGUES A DIPEJ
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="10" width="520">
                <tr>
                    <td style="width: 277px">
                        <cc1:DGTECCheckBox ID="chkDOCNECCV" runat="server" Text="Curriculum Profissional" />
                        <br />
                        <cc1:DGTECCheckBox ID="chkDOCNECORG" runat="server" Text="Cópia autenticada da carteira do Órgão de Classe" />
                        <br />
                        <cc1:DGTECCheckBox ID="chkDOCNECHAB" runat="server" Text="Cópia de dois laudos pericias já realizados junto à Justiça Estadual / Federal ou cópia autenticada de certificado de conclusão em curso especializado na preparação de peritos, reconhecido pelo Órgão de Classe Profissional, devidamente autenticada" />
                        <br />
                        <cc1:DGTECCheckBox ID="chkDOCNECFOTO" runat="server" Text="Foto tamanho 3x4 recente" />
                    </td>
                    <td>
                        <cc1:DGTECCheckBox ID="chkDOCNECCPF" runat="server" Text="Cópia do CPF" />
                        <br />
                        <cc1:DGTECCheckBox ID="chkDOCNECRES" runat="server" Text="Comprovante de residência e/ou local onde exerce atividade laborativa" />
                        <br />
                        <cc1:DGTECCheckBox ID="chkDOCNECIMP" runat="server" Text="Declaração de que não consta impedimanto profissional ou ética que o impeça de atuar em determinada demanda judicial e se exerce alguma atividade laboral neste Tribunal" />
                    </td>
                </tr>
            </table>
        </cc1:DGTECPanel>
    </div>
    </form>
</asp:Content>
