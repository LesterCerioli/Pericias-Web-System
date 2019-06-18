<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master"
    CodeBehind="frmAnotacoes.aspx.vb" Inherits="Interface.frmAnotacoes" Title="Anotações" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div style="width: 524px">
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
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            CPF
                                        </td>
                                        <td>
                                            <cc1:DGTECTextBox ID="txtCPF" runat="server" AutoPostBack="True" Height="17px"></cc1:DGTECTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <cc1:DGTECLabel ID="lblValidaCPF" runat="server" ForeColor="Red" Text="Preencher CPF" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="260">
                                <table width="300">
                                    <tr>
                                        <td style="width: 49px">
                                            Nome
                                        </td>
                                        <td width="200">
                                            <cc1:DGTECTextBox ID="txtNome" runat="server" AutoPostBack="True" Width="220px" Height="17px"></cc1:DGTECTextBox>
                                            <br />
                                            <cc1:DGTECLabel ID="lblValidaNome" runat="server" ForeColor="Red" Text="Preencher o Nome" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 49px">
                                            <cc1:DGTECLabel ID="lblNomesSemelhantes" runat="server">Nomes Semelhantes</cc1:DGTECLabel>
                                        </td>
                                        <td>
                                            <cc1:DGTECDropDownList ID="CboPerito" runat="server" AutoPostBack="True" Height="17px"
                                                Width="220px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 49px">
                                            <cc1:DGTECTextBox ID="txtID_PF" runat="server" Height="17px" Width="10px" Visible="False" />
                                            <cc1:DGTECTextBox ID="txtCod_Perito" runat="server" Visible="False" Height="17px"
                                                Width="10px" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="70px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="70px"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="70px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc1:DGTECButton ID="BtnSair" runat="server" Text="Sair" Width="70px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520" class="corpo-form">
            <tr>
                <td width="40">
                    Orgão
                </td>
                <td width="60">
                    <cc1:DGTECDropDownList ID="CboOrgao_Per" runat="server" Width="200px" AutoPostBack="True"
                        Height="17px">
                    </cc1:DGTECDropDownList>
                </td>
                <td width="70">
                    Número Reg.
                </td>
                <td width="60">
                    <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Height="17px" Width="70px"></cc1:DGTECTextBox>
                </td>
                <td>
                    Data Anotação
                </td>
                <td>
                    <cc1:DGTECLabel ID="lblData_Anotacao" runat="server"></cc1:DGTECLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520" class="corpo-form">
            <tr>
                <td style="width: 100px">
                    Tipo de Anotação :
                </td>
                <td colspan="2" width="250">
                    <cc1:DGTECDropDownList ID="cboTipo_anotacao" runat="server" Height="18px" Width="180px">
                    </cc1:DGTECDropDownList>
                </td>
                <td width="100">
                    <cc1:DGTECButton ID="BtnNova" runat="server" Text="Nova Anotação" Width="100px" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="80px" Width="488px" TextMode="MultiLine"></cc1:DGTECTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" width="520">
                    <br />
                    <cc1:DGTECGridView ID="GrdAnotacoes" runat="server" Width="268px" EmptyDataText="NÃO HÁ REGISTROS"
                        AutoGenerateColumns="False">
                        <Columns>
                            <%--<asp:CommandField HeaderText="Visualizar" SelectText="Exibir" ShowSelectButton="True"
                                Visible="false">
                                <ItemStyle Width="60px" />
                            </asp:CommandField>--%>
                            <%--<asp:BoundField HeaderText="Nome" DataField="Nome">
                                <ItemStyle Width="140px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ID" DataField="ID_PF">
                                <ItemStyle Width="70px"></ItemStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="Data" DataField="Dt_Anot" DataFormatString="{0:Y}">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                           
                            <asp:BoundField HeaderText="Tipo" DataField="Tipo">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Cod_tipo_anotacao" Visible="false" />--%>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                   <asp:ImageButton ImageUrl="imagens\img_lupa.gif" ID="btnGrdVisualizar" runat="server"  CausesValidation="false" CommandName="btnGrdVisualizar_Command"
                                    CommandArgument='<%# eval("Cod_tipo_anotacao")&","& eval("Dt_Anot")&","& eval("ID_PF") %>' Text="Visualizar"
                                    OnCommand="btnGrdVisualizar_Command"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="conteudo-grid"></RowStyle>
                        <HeaderStyle CssClass="titulo-grid"></HeaderStyle>
                    </cc1:DGTECGridView>
                </td>
            </tr>
        </table>
        <%-- </td> </tr> </table>--%>
    </div>
    </form>
</asp:Content>
