<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master"
    CodeBehind="frmPeritoDCP.aspx.vb" Inherits="Interface.frmPeritoDCP" Title="Untitled Page" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div class="corpo-form">
        <table>
            <tr>
                <th align="left" style="width: 527px">
                    <br />
                    NOMEAÇÃO DE PERITOS&nbsp;&nbsp;&nbsp;
                    <cc1:DGTECLabel ID="lblOrgaoUsuario" runat="server" />
                    <br />
                </th>
            </tr>
            <tr>
                <td style="width: 527px;">
                    <table border="0" cellpadding="10" width="520" class="corpo-form">
                        <tr>
                            <td style="width: 113px">
                                Nº Processo Antigo
                            </td>
                            <td align="center" style="width: 101px">
                                <cc1:DGTECTextBox ID="txtNum_Processo" runat="server" Width="100px" Height="17px"
                                    AutoPostBack="True" MaxLength="14">
                                </cc1:DGTECTextBox>
                            </td>
                            <td align="center" style="width: 124px">
                                Nº Processo Novo (CNJ)
                            </td>
                            <td align="left">
                                <cc1:DGTECTextBox ID="txtNum_CNJ1" runat="server" Width="88px" Height="17px" AutoPostBack="True"
                                    MaxLength="15"></cc1:DGTECTextBox>
                                .8.19.
                                <cc1:DGTECTextBox ID="txtNum_CNJ2" runat="server" Width="28px" Height="17px" AutoPostBack="True"
                                    MaxLength="4"></cc1:DGTECTextBox>
                                &nbsp;
                                <cc1:DGTECTextBox ID="txtNum_CNJ" runat="server" AutoPostBack="True" Visible="False"
                                    Width="5px">
                                </cc1:DGTECTextBox>
                                <cc1:DGTECTextBox ID="txtID_Nomeacao" runat="server" AutoPostBack="True" Visible="False"
                                    Width="5px">
                                </cc1:DGTECTextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                      <tr>
                      <td>
                        <cc1:DGTECButton ID="BtnVerCNJPendS" runat="server" Height="21px" 
                              Text="Nomeações Recusadas" Width="120px" />
                        &nbsp;&nbsp;
                        <cc1:DGTECButton ID="BtnVerCNJPend" runat="server" Height="21px" 
                              Text="Nomeações Pendentes"  Width="120px" />
                        &nbsp;
                        &nbsp;<cc1:DGTECLabel ID="LblNumProcExemplo" runat="server" Visible="False">Núm Exemplo Proc :</cc1:DGTECLabel>
                          &nbsp;
                          <cc1:DGTECTextBox ID="txtNumProcExemplo" runat="server" Visible="False"></cc1:DGTECTextBox>
                        </td>
                      </tr>
                    </table>
                      <br />
                    <cc1:DGTECPanel ID="pnlPeritos" runat="server" GroupingText="Lista de Peritos Nomeados" Visible="false"
                        Enabled="false" BorderColor="Black">
                        <table width="518">
                            <tr>
                                <td>
                                    <cc1:DGTECGridView ID="GrdProcPerito" runat="server" Width="516px" HorizontalAlign="Center"
                                        OnPageIndexChanging="GrdProcPerito_PageIndexChanging" EmptyDataText="NÃO HÁ REGISTRO"
                                        AutoGenerateColumns="False" PageSize="5" AllowPaging="True" EnableModelValidation="True">
                                        <Columns>
                                            <asp:CommandField SelectText="Exibir" ShowSelectButton="True" ItemStyle-Width="25px">
                                                <HeaderStyle Height="18px" />
                                                <ItemStyle Width="25px" />
                                            </asp:CommandField>
                                            <asp:BoundField HeaderText="ID" DataField="ID_PF" ItemStyle-Width="30px">
                                                <HeaderStyle Height="18px" />
                                                <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-Width="150px">
                                                <HeaderStyle Height="18px" />
                                                <ItemStyle Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Nomeação" DataField="Data_Nomeacao" 
                                                ItemStyle-Width="30px" DataFormatString="{0:dd/MM/yyyy}" >
                                            <HeaderStyle Height="18px" />
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Profissão" DataField="prof" ItemStyle-Width="100px">
                                                <HeaderStyle Height="18px" />
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Especialidade" DataField="Espec" ItemStyle-Width="50px">
                                                <HeaderStyle Height="18px" />
                                                <ItemStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Aceite" DataField="Aceite" ItemStyle-Width="30px" Visible="false">
                                                <HeaderStyle Height="18px" />
                                                <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="J.G." DataField="JG" ItemStyle-Width="10px" Visible="false">
                                                <ItemStyle Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Prazo" HeaderText="Prazo" ItemStyle-Width="20px" Visible="false">
                                                <ItemStyle Width="20px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <SelectedRowStyle HorizontalAlign="Left" Font-Bold="True" />
                                        <PagerStyle HorizontalAlign="Left" />
                                        <RowStyle CssClass="conteudo-grid" BorderStyle="Solid"></RowStyle>
                                        <HeaderStyle CssClass="titulo-grid"></HeaderStyle>
                                    </cc1:DGTECGridView>
                                </td>
                            </tr>
                        </table>
                    </cc1:DGTECPanel>
                    <cc1:DGTECPanel ID="pnlInfNomeacaoPerito" runat="server" GroupingText="Informações do perito"  >
                      <br />
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td width="80">
                                    Profissao
                                </td>
                                <td>
                                    <cc1:DGTECDropDownList ID="CboProfissao" runat="server" AutoPostBack="True" Width="400px"
                                        Height="20px">
                                    </cc1:DGTECDropDownList>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td width="80" style="height: 18px">
                                    Especialidade
                                </td>
                                <td  colspan="2" style="height: 18px">
                                    <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" Width="400px"
                                        Height="20px">
                                    </cc1:DGTECDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 22px">
                                    Perito:
                                </td>
                                <td colspan="1" style="width: 520; height: 22px;">
                                    <cc1:DGTECTextBox ID="txtNome_Perito" runat="server" Enabled="False" Height="20px"
                                        Width="300px"></cc1:DGTECTextBox>
                                    <cc1:DGTECTextBox ID="txtID_PF" runat="server" Visible="False" Width="10px"></cc1:DGTECTextBox>
                                </td>
                                <td style="height: 22px">
                                    <cc1:DGTECButton ID="BtnPerito" runat="server" Height="17px" Text="Pesquisar Perito" Width="90px" />
                                 </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td width="0">
                                    <cc1:DGTECButton ID="BtnCurriculum" runat="server" Height="21px" Text="Curriculum"
                                        Visible="false" Width="100px" />
                                    <cc1:DGTECButton ID="BtnVerFoto" runat="server" Height="21px" Text="Foto" Visible="false"
                                        Width="50px" />
                                </td>
                                <td>
                                    <cc1:DGTECButton 
                                        ID="BtnNovoPer" runat="server" Height="17px" Text="Novo Perito" 
                                        Width="90px" />
                                </td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td width="200"> Data de Cadastramento na DIPEJ:</td>
                                <td style="width: 66px"> <cc1:DGTECLabel ID="lblData_Cadastramento" runat="server" Enabled="False" Height="17px" /></td>
                            </tr>
                            </table>
                            <table>
                            <tr>
                                <td width="150"> Perícias em andamento:</td>
                                <td width="31">
                                    <cc1:DGTECTextBox ID="txtQtePendentes" runat="server" Enabled="False" 
                                        Width="30px"></cc1:DGTECTextBox>
                                </td>
                                <td width="150"> Nomeações Aprovadas :</td>
                                <td width="31">
                                    <cc1:DGTECTextBox ID="txtQteAceitos" runat="server" Enabled="False" 
                                        Width="30px"></cc1:DGTECTextBox>
                                </td>
                                <td width="150"> &nbsp;&nbsp; Nomeações Recusadas:</td>
                                <td width="31">
                                    <cc1:DGTECTextBox ID="txtQteRecusadas" runat="server" Enabled="False" 
                                        Width="30px"></cc1:DGTECTextBox>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10">
                            <tr>
                                <td align="left" style="width: 553px">
                                    <cc1:DGTECLabel ID="LblAnotacao" runat="server" Width="512px" Visible="False" Text="Anotações" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 520">
                                    <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="100px" 
                                        ReadOnly="True" TextMode="MultiLine" Visible="False" Width="420px"></cc1:DGTECTextBox>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 553px" >
                                    <table>
                                        <tr>
                                            <td>
                                                Email :
                                                <cc1:DGTECLabel ID="lblEmail" runat="server" Enabled="False" Width="180px"></cc1:DGTECLabel>
                                            </td>
                                            <td>
                                                Email Alternativo :
                                                <cc1:DGTECLabel ID="lblEmail1" runat="server" Width="160px"></cc1:DGTECLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        </cc1:DGTECPanel>
                        <cc1:DGTECPanel ID="pnlInformacoesNomeacao" runat="server" GroupingText="Nomeação do Perito" Font-Bold="False" Font-Strikeout="False">
                        <br />
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td class="coluna1Movimento" style="width: 100px">
                                    Nomeação :
                                    <cc1:DGTECLabel ID="lblData_Nomeacao" runat="server" Enabled="False"></cc1:DGTECLabel>
                                </td>
                                <td align="center" style="width: 103px">
                                    Aceitação : <cc1:DGTECLabel ID="lblAceitacao_Tacita" runat="server" 
                                        ForeColor="#FF3300" Visible="False">TÁCITA</cc1:DGTECLabel>
                                    <cc1:DGTECLabel ID="lblData_Aceitacao" runat="server"></cc1:DGTECLabel>
                                </td>
                                <td align="center" class="coluna1Movimento" style="width: 143px">
                                    Aceitação após Hon. Juiz :<cc1:DGTECLabel ID="lblData_Seg_Aceitacao" runat="server"></cc1:DGTECLabel>
                                    &nbsp;<cc1:DGTECLabel ID="lblAceitacao_TacitaPos" runat="server" ForeColor="#FF3300" 
                                        Visible="False">TÁCITA</cc1:DGTECLabel>
                                </td>
                                <td align="center">
                                    Recusa:<cc1:DGTECLabel ID="lblData_Negacao" runat="server"></cc1:DGTECLabel>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 515px">
                                    <cc1:DGTECLabel ID="lblMotivo_Recusa" runat="server" Visible="False">Motivo da Recusa</cc1:DGTECLabel>
                                    <br />
                                    <cc1:DGTECTextBox ID="txtMotivo_Recusa" runat="server" Enabled="False" Height="50px" 
                                        Width="510px" Visible="False"></cc1:DGTECTextBox>
                                </td>
                            </tr>

                        </table>
                        <table>
                            <tr>
                                <td class="coluna1Movimento" style="width: 191px">
                                     <cc1:DGTECLabel ID="lblJG" runat="server" Text="Justiça Gratuita?" />
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RdbJustGrat" runat="server" 
                                    RepeatDirection="Horizontal" AutoPostBack="True" Height="16px" Width="89px">
                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                             </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td class="coluna1Movimento" style="width: 270px">
                                    Prazo fixado para a realização da perícia: 
                                    <cc1:DGTECTextBox ID="txtPrazo" runat="server" Width="30px"></cc1:DGTECTextBox>
                                    &nbsp;dias
                                </td>
                            </tr>
                            <tr>
                                <td class="coluna1Movimento" style="width: 270px">
                                 Honorários apresentados pelo Perito :
                                    <cc1:DGTECTextBox ID="txtHonPer" runat="server" Width="30px" Enabled="False"></cc1:DGTECTextBox>
                                    &nbsp;UFIRs
                                </td>
                            </tr>
                            <tr>
                                <td class="coluna1Movimento" style="width: 270px">
                                    Honorários fixados pelo Juízo :
                                    <cc1:DGTECTextBox ID="txtHonJuiz" runat="server" Width="30px" Enabled="False" 
                                        AutoPostBack="True"></cc1:DGTECTextBox>
                                    &nbsp;UFIRs&nbsp;
                                 </td>
                                 <td>
                                     Data da fixação dos Hon. pelo Juiz:
                                     <cc1:DGTECLabel ID="lblData_Novo_Hon" runat="server"></cc1:DGTECLabel>
                                </td>
                                 </td>
                            </tr>
                            <tr>
                                <td class="coluna1Movimento" style="width: 270px">
                                   <cc1:DGTECCheckBox ID="ChkPerInter" runat="server" 
                                        Text="Perícia para interdição" Visible="False" />
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td style="width: 196px">
                                </td>
                                <td>
                                    <cc1:DGTECButton ID="BtnInicio" runat="server" Height="17px" Text="Iniciar Perícia"
                                        Width="90px" Enabled="False" />
                                    &nbsp; Data de Início da Perícia
                                    <cc1:DGTECLabel ID="lblData_Inicio" runat="server"></cc1:DGTECLabel>
                                </td>
                            </tr>
                        </table>
                    </cc1:DGTECPanel>
                    <cc1:DGTECPanel ID="PanelPagtoPerito" runat="server" GroupingText="Pagamento do Perito"
                        Visible="False" Width="527px">
                        <table border="0" cellpadding="10" id="tbAceiteLaudo" runat="server" visible="false">
                            <tr>
                                <th align="left" style="height: 23px; width: 515px;" colspan="2">
                                    AUTORIZAÇÃO DE PAGAMENTO
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="10" width="520">
                                        <tr>
                                            <td align="left">
                                                &nbsp;Perícia psiquiátrica em  ações de interdição?<asp:RadioButtonList ID="RdInterdicao"
                                                    runat="server">
                                                    <asp:ListItem Value="L">Local</asp:ListItem>
                                                    <asp:ListItem Value="A">Audiência</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td style="height: 34px">
                                                <cc1:DGTECCheckBox ID="chkLaudoLiberado" runat="server" Height="32px" Text="Recebo o laudo pericial apresentado e autorizo o pagamento da ajuda de custo nos termos da Resolução nº 09/2011 do E CM"
                                                    AutoPostBack="True" Width="399px" Enabled="False" />
                                            </td>
                                            <td align="center" style="width: 130px; height: 34px;">
                                                Data Autorizaçao<br />
                                                <br />
                                                <cc1:DGTECLabel ID="lblData_Liberacao" runat="server"></cc1:DGTECLabel>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td style="height: 34px">
                                                <cc1:DGTECCheckBox ID="chkLaudoCancelado" runat="server" Height="61px" Text="Cancelo a Autorização de Pagamento da ajuda de custo. A autorização de pagamento poderá ser cancelada em até 7(sete) dias da data de sua autorização. Após este prazo deverá ser contactada a Divisão de Perícias Judiciais para providências(tel 21 3131-1608) "
                                                    AutoPostBack="True" Width="399px" Enabled="False" />
                                            </td>
                                            <td align="center" style="width: 130px; height: 34px;">
                                                Data Cancelamento<br />
                                                <br />
                                                <cc1:DGTECLabel ID="lblData_Cancelamento" runat="server"></cc1:DGTECLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </cc1:DGTECPanel>
                    <table width="523" border="0" cellpadding="10">
                        <tr align="center">
                            <td>
                                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Height="17px" Width="60px"
                                    Enabled="False" />
                            </td>
                            <td>
                                <cc1:DGTECButton ID="BtnNovo" runat="server" Height="17px" Text="Limpar" 
                                    Width="65px" />
                            </td>
                            <td>
                                &nbsp;</td>

                            <td>
                                <cc1:DGTECButton ID="BtnEmailNomeacao" runat="server" Text="Reenviar Email de Nomeação"
                                    Height="17px" Width="150px" Enabled="False" />
                            </td>

                            <td>
                                <cc1:DGTECButton ID="BtnAnotacao" runat="server" Height="17px" Text="Enviar Anotações à DIPEJ"
                                    Width="158px" Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</asp:Content>
