<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master"
    CodeBehind="frmAceitacao.aspx.vb" Inherits="Interface.frmAceitacao" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div>
        <table class="corpo-form">
            <tr>
                <th align="center">
                    ACEITAÇÃO / RECUSA DA PERÍCIA
                </th>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td style="width: 189px">
                                CPF:
                                <cc1:DGTECLabel ID="lblCPF" runat="server" />
                            </td>
                            <td>
                                Nome: <cc1:DGTECLabel ID="lblNome" runat="server" />
                                 <cc1:DGTECTextBox ID="txtID_PF" runat="server" Height="17px" Width="10px" Visible="False"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td>
                                <cc1:DGTECGridView ID="GrdProcessos" runat="server" AutoGenerateColumns="false" EmptyDataText="Não há registro"
                                  Width="510px" PageSize="10" AllowPaging="true" OnPageIndexChanging="GrdProcessos_PageIndexChanging">
                                    <Columns>
                                        <asp:CommandField SelectText="Exibir" ShowSelectButton="True" />
                                        <asp:BoundField DataField="Num_CNJ" HeaderText="Num. CNJ" />
                                        <asp:BoundField DataField="Nomeacao" HeaderText="Nomeação" />
                                        <asp:BoundField DataField="Aceite" HeaderText="Aceite" />
                                        <asp:BoundField DataField="Negacao" HeaderText="Negação" />
                                        <asp:BoundField DataField="Liberacao" HeaderText="Liberação" />
                                        <asp:BoundField DataField="JG" HeaderText="JG" />
                                    </Columns>
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                </cc1:DGTECGridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td style="width: 100px">
                                Nº Processo Antigo
                            </td>
                            <td colspan="2" width="110">
                                <asp:TextBox ID="txtNum_Processo" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td width="120">
                                Nº Processo Novo (CNJ)
                            </td>
                            <td width="180">
                                <cc1:DGTECTextBox ID="txtNum_CNJ" runat="server" Enabled="False" Width="170px"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td style="width: 100px">
                                
                            </td>
                            <td colspan="2" width="150">
                                <%--<asp:TextBox ID="TxtTipoProc" runat="server" Width="100px" Enabled="False" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="4">
                                <cc1:DGTECLinkButton ID="lnkVisualizaProcesso" runat="server" Visible="false">Visualiza 
                processo.</cc1:DGTECLinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" width="520">
                        <tr>
                            <td style="width: 70px">
                                Orgão:
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtOrgaoProfissional" runat="server" Width="250px" Enabled="false"></cc1:DGTECTextBox>
                            </td>
                            <td style="width: 98px">
                                Número do registro:
                            </td>
                            <td>
                              <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Width="70px" Enabled="False"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 70px">
                                Profissão:
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtProfissao" runat="server" Enabled="False" Width="200px" />
                            </td>
                            <td style="width: 98px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 70px">
                                Especialidade:
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtEspecialidade" runat="server" Enabled="False" Width="200px" />
                            </td>
                            <td style="width: 98px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 70px">
                                Honorários :
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtHonorarios" runat="server" Enabled="False" Width="50px"
                                    Visible="False"></cc1:DGTECTextBox>(UFIRs)
                            </td>
                            <td style="width: 98px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Prazo para realização da perícia após a data de recebimento dos autos é de
                                <cc1:DGTECLabel ID="lblPrazo" runat="server"></cc1:DGTECLabel>
                                &nbsp;dias.
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 82px">
                                <asp:RadioButtonList ID="RdoAceitacao" runat="server" AutoPostBack="True" Enabled="False">
                                    <asp:ListItem Value="Aceita">Aceita</asp:ListItem>
                                    <asp:ListItem>Recusa</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left">
                                <p>
                                    Caso não seja feito a recusa num prazo de 5 dias, após a intimação, existe a presunção
                                    da aceitação.(Art.146 CPC), portanto será considera como aceita.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td style="width: 204px">
                                &nbsp;
                                <cc1:DGTECLabel ID="lblRecusa" runat="server" Visible="False">Motivo da Recusa</cc1:DGTECLabel>
                                <br />
                                <asp:TextBox ID="TxtMotivo" runat="server" Enabled="False" Height="80px" Visible="False"
                                    Width="500px" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td align="center">
                                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="80px" />
                              <%--  <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="80px" />--%>
                                <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="80px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</asp:Content>
