<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master"
    CodeBehind="frmPeritoDCP.aspx.vb" Inherits="Interface.frmPeritoDCP" Title="Untitled Page" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div class="corpo-form">
        <table>
            <tr>
                <th align="left" bgcolor="#EFE6D6" style="width: 527px">
                    <br />
                    NOMEAÇÃO DE PERITOS
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
                                <cc1:DGTECTextBox ID="txtNum_CNJ1" runat="server" Width="85px" Height="17px" AutoPostBack="True"
                                    MaxLength="15"></cc1:DGTECTextBox>
                                .8.19.
                                <cc1:DGTECTextBox ID="txtNum_CNJ2" runat="server" Width="28px" Height="17px" AutoPostBack="True"
                                    MaxLength="4"></cc1:DGTECTextBox>
                                &nbsp;
                                <cc1:DGTECTextBox ID="txtNum_CNJ" runat="server" AutoPostBack="True" Visible="False"
                                    Width="5px">
                                </cc1:DGTECTextBox>
                            </td>
                        </tr>
                    </table>
                    <cc1:DGTECPanel ID="pnlPeritos" runat="server" GroupingText="Lista de peritos" Visible="false"
                        Enabled="false">
                        <table width="518">
                            <tr>
                                <td>
                                    <cc1:DGTECGridView ID="GrdProcPerito" runat="server" Width="516px" HorizontalAlign="Center"  OnPageIndexChanging="GrdProcPerito_PageIndexChanging"
                                        EmptyDataText="NÃO HÁ REGISTRO" AutoGenerateColumns="False" PageSize="5" AllowPaging="True" >
                                        <Columns>
                                            <asp:CommandField SelectText="Exibir" ShowSelectButton="True"  ItemStyle-Width="25px" />
                                            <asp:BoundField HeaderText="ID" DataField="ID_PF"  ItemStyle-Width="30px" />
                                            <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-Width="150px" />
                                            <asp:BoundField HeaderText="Profissão" DataField="prof"  ItemStyle-Width="100px" />
                                            <asp:BoundField HeaderText="Espec." DataField="Espec" ItemStyle-Width="50px" />
                                            <asp:BoundField HeaderText="Aceite" DataField="Aceite" ItemStyle-Width="30px"  />
                                            <asp:BoundField HeaderText="J.G." DataField="JG" ItemStyle-Width="10px" />
                                            <asp:BoundField HeaderText="Prazo" DataField="Prazo" ItemStyle-Width="20px" />
                                        </Columns>
                                        <%--<FooterStyle BackColor="White" ForeColor="#000066" />--%>
                                        <%--   <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />--%>
                                        <%--<EmptyDataTemplate>
                                            Selecione
                                        </EmptyDataTemplate>--%>
                                        <SelectedRowStyle HorizontalAlign="Left" Font-Bold="True" />
                                        <PagerStyle HorizontalAlign="Left" />
                                        <RowStyle CssClass="conteudo-grid" BorderStyle="Solid"></RowStyle>
                                        <HeaderStyle CssClass="titulo-grid"></HeaderStyle>
                                    </cc1:DGTECGridView>
                                </td>
                            </tr>
                        </table>
                    </cc1:DGTECPanel>
                    <cc1:DGTECPanel ID="pnlInfNomeacaoPerito" runat="server" 
                        GroupingText="Informações da nomeação do perito" Height="350px">
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
                                <td width="80">
                                    Especialidade
                                </td>
                                <td>
                                    <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" Width="400px"
                                        Height="20px">
                                    </cc1:DGTECDropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Perito:
                                </td>
                                <td>
                                    <cc1:DGTECTextBox ID="txtNome_Perito" runat="server" Enabled="False" Height="20px"
                                        Width="400px"></cc1:DGTECTextBox>
                                    <cc1:DGTECTextBox ID="txtID_PF" runat="server" Visible="False" Width="10px"></cc1:DGTECTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <cc1:DGTECButton ID="BtnPerito" runat="server" Text="Pesquisar Perito" Width="125px"
                                        Height="21px" />
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td align="left">
                                    <cc1:DGTECButton ID="BtnCurriculum" runat="server" Text="Curriculum" Height="21px"
                                        Width="100px" Visible="false" />
                                    <cc1:DGTECButton ID="BtnVerFoto" runat="server" Text="Foto" Height="21px" Width="50px"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td align="left">
                                    <cc1:DGTECLabel ID="LblAnotacao" runat="server" Width="512px" Visible="False" Text="Anotações" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="100px" Width="420px" Visible="False"
                                        Enabled="False" TextMode="MultiLine"></cc1:DGTECTextBox>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td width="200">
                                    Data da Nomeação :
                                    <cc1:DGTECLabel ID="lblData_Nomeacao" runat="server" Enabled="False"></cc1:DGTECLabel>
                                </td>
                                <td>
                                    <cc1:DGTECCheckBox ID="ChkJustGrat" runat="server" Text=" Justiça Gratuita" />
                                </td>                                
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" id="tbPisq" runat="server"  
                                                              visible="true" frame="above">
                                 <tr>
                                        <td>
                                        <cc1:DGTECCheckBox ID="ChkPsiqLocal" runat="server" Height="20px" Text="Perícia em ações de interdição local"
                                        AutoPostBack="True" Width="234px" Visible="False" />

                                        <cc1:DGTECCheckBox ID="chkPsiqAudiencia" runat="server" Height="19px" Text="Perícia em ações de interdição na audiência"
                                        AutoPostBack="True" Width="276px" Visible="False" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                 </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td>
                                    Data da Aceitação :<cc1:DGTECLabel ID="lblData_Aceitacao" runat="server"></cc1:DGTECLabel>
                                </td>
                                <td>
                                    Data da Recusa :<cc1:DGTECLabel ID="lblData_Negacao" runat="server"></cc1:DGTECLabel>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td>
                                    Email :
                                    <cc1:DGTECLabel ID="lblEmail" runat="server" Enabled="False" Width="180px"></cc1:DGTECLabel>
                                </td>
                                <td width="285">
                                    Email Alternativo :
                                    <cc1:DGTECLabel ID="lblEmail1" runat="server" Width="160px"></cc1:DGTECLabel>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td>
                                    Prazo para realização da perícia após a data de recebimento dos autos:
                                    <cc1:DGTECTextBox ID="txtPrazo" runat="server" Width="30px"></cc1:DGTECTextBox>
                                    &nbsp;dias
                                </td>
                            </tr>
                        </table>
                       
                        <table border="0" cellpadding="10" width="520">
                            <tr>
                                <td align="center" width="150">
                                    Dt de Cadastramento na DIPEJ
                                </td>
                                <td width="50">
                                    <cc1:DGTECLabel ID="lblData_Cadastramento" runat="server" Enabled="False" Height="17px"
                                        Width="52px">__ /__ /__
                                    </cc1:DGTECLabel>
                                </td>
                                <td align="center" width="120">
                                    Qte Nomeações Aceitas
                                </td>
                                <td width="35">
                                    &nbsp;<cc1:DGTECTextBox ID="txtQteAceitos" runat="server" Width="30px" Enabled="False"></cc1:DGTECTextBox>
                                </td>
                                <td align="center" width="110">
                                    Qte Perícias.Pendentes
                                </td>
                                <td width="40">
                                    &nbsp;<cc1:DGTECTextBox ID="txtQtePendentes" runat="server" Width="30px" Enabled="False"></cc1:DGTECTextBox>
                                </td>
                            </tr>
                        </table>
                         </cc1:DGTECPanel>
                        </td>
                        </tr>
                        <tr>
                        <td></td>
                        </tr>
                        <tr>
                        <td>
                        <cc1:DGTECPanel ID="pnlPagamento" runat="server" GroupingText="Pgtos" 
                        Width="524px" Height="180px" Visible=false >
                          <table border="0" cellpadding="10" id="tbAceiteLaudo" runat="server"  
                                visible="true" frame="above">
                          <tr>
                            <th align="left" bgcolor="#EFE6D6" style="height: 23px; width: 515px;">
                                AUTORIZAÇÃO DE PAGAMENTO
                            </th>
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
                                        <td>
                                        <cc1:DGTECCheckBox ID="ChkCancelaPgto" runat="server" Height="64px" Text="Cancelo a Autorização de Pagamento da ajuda de custo. A autorização de pagamento poderá ser cacelada en até 7(sete) dias da data de sua autorização. Após este prazo deverá ser contactada a Divisão de Perícias Judiciais para providências (tel 21 3133-1608)"
                                        AutoPostBack="True" Width="400px" Enabled="False" />
                                        </td>
                                        <td align="center" style="width: 100px">
                                            Data Cancelamento<br />
                                            <br />
                                         <cc1:DGTECLabel ID="lblData_Cancelamento" runat="server"></cc1:DGTECLabel>

                                        </td>
                                 </tr>
                        </table>
                    </cc1:DGTECPanel>
                       <table width="520" height="50"  border="0" cellpadding="10" >
                            <tr align="center">
                                <td>
                                    <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Height="30px" 
                                        Width="90px" Enabled="False" />
                                </td>
                                <td>
                                    <cc1:DGTECButton ID="BtnNovo" runat="server" Height="30px" Text="Limpar" 
                                        Width="90px" />
                                </td>
                                <td>
                                    <cc1:DGTECButton ID="BtnEmailNomeacao" runat="server" Text="Reenviar Email de Nomeação"
                                        Height="30px" Width="170px" Enabled="False" />
                                </td>
                                <td >
                                    <cc1:DGTECButton ID="BtnAnotacao" runat="server" Height="30px" TextMode="multiline"
                                        Text="Enviar Anotações à DIPEJ" Width="170px" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</asp:Content>
