<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master"
    CodeBehind="frmAceitacao.aspx.vb" Inherits="Interface.frmAceitacao" EnableEventValidation="false" EnableSessionState="True" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div>
        <table class="corpo-form">
            <tr>
                <th align="center">
                    ACEITAÇÃO / RECUSA DA NOMEAÇÃO
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
                                Nome:
                                <cc1:DGTECLabel ID="lblNome" runat="server" />
                                <cc1:DGTECTextBox ID="txtID_PF" runat="server" Height="17px" Width="10px" Visible="False"></cc1:DGTECTextBox>
                                <cc1:DGTECTextBox ID="txtID_Nomeacao" runat="server" Height="17px" Width="10px" Visible="False"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td>
                                <cc1:DGTECGridView ID="GrdProcessos" runat="server" AutoGenerateColumns="False" EmptyDataText="Não há registro"
                                    Width="510px" AllowPaging="True" OnPageIndexChanging="GrdProcessos_PageIndexChanging"
                                    OnRowCommand="GrdProcessos_RowCommand" EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Exibir" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl="imagens\img_lupa.gif" ID="btnGrdExibir" runat="server"
                                                    CausesValidation="False" CommandName="btnGrdExibir_Command" CommandArgument='<%# eval("Num_CNJ")&","& eval("Nomeacao") &","&  eval("CodProfissao") &","&  eval("CodEspecialidade") &","&  eval("linha") &","&  eval("Aceite") &","&  eval("Negacao") %>'
                                                    Text="Exibir" OnCommand="btnGrdExibir_Command" 
                                                    ToolTip="Visualizar dados da nomeação.">
                                                </asp:ImageButton>
                                            </ItemTemplate>

<ItemStyle Width="40px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="linha" Visible="false" />
                                        <asp:BoundField DataField="Num_CNJ" HeaderText="Num. CNJ" 
                                            ItemStyle-Width="150px" >
<ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nomeacao" HeaderText="Nomeação" DataFormatString="{0:dd/MM/yyyy}"
                                            ItemStyle-Width="80px" >
<ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Aceite" HeaderText="Aceite" DataFormatString="{0:dd/MM/yyyy}"
                                            ItemStyle-Width="80px" >
<ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Negacao" HeaderText="Negação" DataFormatString="{0:dd/MM/yyyy}"
                                            ItemStyle-Width="80px" Visible="False" >
<ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DescProfissao" HeaderText="Profissão" Visible="false" />
                                        <asp:BoundField DataField="DescEspecialidade" HeaderText="Especialidade" 
                                            ItemStyle-Width="100px" Visible="False" >
<ItemStyle Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CodEspecialidade" Visible="false" />
                                        <asp:BoundField DataField="data_inicio" HeaderText="Início" 
                                            DataFormatString="{0:dd/MM/yyyy}">
                                        <ControlStyle ForeColor="#FF3300" />
                                        <ItemStyle ForeColor="#FF3300" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CodProfissao" Visible="false" />
                                        <%--<asp:BoundField DataField="Liberacao" HeaderText="Liberação" DataFormatString="{0:dd/MM/yyyy}"  Visible="false"/>
                                        <asp:BoundField DataField="JG" HeaderText="JG" Visible="false" />--%>
                                    </Columns>
                                    <SelectedRowStyle HorizontalAlign="Left" Font-Bold="True" />
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
                            <td colspan="2">
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
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" width="520">
                        <tr>
                            <td style="width: 112px">
                                Profissão:
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtProfissao" runat="server" Enabled="False" Width="200px" />
                            </td>
                            <td style="width: 98px">
                                Data Nomeação
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtData_Nomeacao" runat="server" Width="70px" Enabled="False"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 112px">
                                Especialidade:
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtEspecialidade" runat="server" Enabled="False" Width="200px" />
                            </td>
                            <%--<td style="width: 98px">
                                Data Liberação:
                            </td>
                            <td>
                                <cc1:DGTECLabel ID="lblDataLiberacao" runat="server" />
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 112px">
                                Conselho Profissional:
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtOrgaoProfissional" runat="server" Width="200px" ReadOnly="True"></cc1:DGTECTextBox>
                            </td>
                            <td style="width: 98px">
                                Número do registro:
                            </td>
                            <td>
                                <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Width="70px" Enabled="False"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <cc1:DGTECLinkButton ID="lnkVisualizaProcesso" runat="server" Visible="false">
                                        Visualizar Processo.
                                </cc1:DGTECLinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <cc1:DGTECLabel ID="lblMsg" Font-Bold="true" runat="server"></cc1:DGTECLabel>
                            </td>
                        </tr>
                        <tr>
                        <td style="width: 71px"></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Prazo fixado para a realização da perícia:
                                <cc1:DGTECLabel ID="lblPrazo" runat="server"></cc1:DGTECLabel>
                                &nbsp;dias.
                            </td>
                        </tr>
                        
                        
                            <td colspan="2">
                                <font color="red">
                                    <p>
                                        A ausência da recusa expressa no prazo determinado pelo Art. 146 do CPC implica
                                        aceitação tácita do encargo.</p>
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <cc1:DGTECLabel ID="lblSegAceitacao" Font-Bold="True" runat="server"></cc1:DGTECLabel>
                                <br />
                                <asp:CheckBox ID="ChkComp_Deleg" runat="server" 
                                    Text="Competência delegada conforme CF/88, art. 109, §3º e art. 112" 
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 71px">
                                <asp:RadioButtonList ID="RdoAceitacao" runat="server" AutoPostBack="True" 
                                    Enabled="False" Width="90px">
                                    <asp:ListItem Value="Aceita">Aceitar</asp:ListItem>
                                    <asp:ListItem Value="Recusa">Recusar</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left">
                                <!-- <p>
                                    Caso não seja feito a recusa num prazo de 5 dias, após a intimação, existe a presunção
                                    da aceitação(Art.146 CPC), portanto será considerada como aceita.
                                </p> -->
                                <table>
                                    <tr>
                                        <td style="245: ;">



                                <cc1:DGTECLabel ID="lblMsgTacita" Font-Bold="True" runat="server" 
                                    ForeColor="#0033CC" Visible="False" Width="100px">Aceitação Tácita</cc1:DGTECLabel>



                                &nbsp;<cc1:DGTECLabel ID="lblMsgHon" Font-Bold="True" runat="server">Honorários:</cc1:DGTECLabel>
                            &nbsp; <cc1:DGTECTextBox ID="txtHonorarios" runat="server" Enabled="False" Width="40px"
                                                Visible="False"></cc1:DGTECTextBox>
                                <cc1:DGTECLabel ID="lblRefHon" Font-Bold="True" runat="server"> UFIRs</cc1:DGTECLabel>
                                        </td>
                                        <%--<td style="width: 85px">
                                            &nbsp;Justiça Gratuita?
                                        </td>
                                        <td style="width: 20px">
                                            <cc1:DGTECLabel ID="lblJusticaGratuita" runat="server" />
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                <cc1:DGTECLabel ID="lblHonorarioJuiz" Font-Bold="True" runat="server">Honorários fixados pelo Juízo :</cc1:DGTECLabel>
                            &nbsp;<cc1:DGTECTextBox ID="txtHonorarioJuiz" ReadOnly="true" runat="server" Width="40px" 
                                                Enabled="False"></cc1:DGTECTextBox>
                                <cc1:DGTECLabel ID="lblRefHonJuiz" Font-Bold="True" runat="server"> UFIRs</cc1:DGTECLabel>
                                        </td>
                                        <td>
                                <cc1:DGTECLabel ID="lblInterdicao" Font-Bold="True" runat="server" 
                                    ForeColor="#0033CC" Visible="False" Width="133px">Interdição Psiquiátrica</cc1:DGTECLabel>
                                            <br />
                                <cc1:DGTECLabel ID="lblMsgJG" Font-Bold="True" runat="server" 
                                    ForeColor="#0033CC" Visible="False" Width="100px">Justiça Gratuita</cc1:DGTECLabel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td style="width: 204px">
                                &nbsp;
                                <cc1:DGTECLabel ID="lblRecusa" runat="server" Visible="False">Justificativa da Recusa</cc1:DGTECLabel>
                                <br />
                                <asp:TextBox ID="TxtMotivo" runat="server" Height="50px" Visible="False" Width="500px"
                                    TextMode="MultiLine" onkeypress="Javascript:return textboxMultilineMaxNumber(event,this,255);"
                                    MaxLength="255" ToolTip="Limite de 255 caracteres"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="10" width="520">
                        <tr>
                            <td align="center">
                                <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="80px" />
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
