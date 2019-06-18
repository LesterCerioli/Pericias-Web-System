<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master"
    CodeBehind="frmAgendaAcidentaria.aspx.vb" Inherits="Interface.frmAgendaAcidentaria" title="Agenda Acidentaria"%>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div>
     <table class="corpo-form">
    <tr align="center">
    <th align="center">
                   PERITOS (SISTEMA DE PERÍCIAS)
    </th>
    </tr>
    <tr>
    <td>
    <!--
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center">
                    PERITOS (SISTEMA DE PERÍCIAS)
                </td>
            </tr>
        </table>
     -->
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" width="100">
                    Nº Processo DCP 
                </td>
                <td align="center" style="width: 87px">
                    <cc1:DGTECTextBox ID="txtNum_Processo" runat="server" Width="100px" Height="17px"></cc1:DGTECTextBox>
                </td>
                <td align="center" width="100">
                    Nº Processo CNJ
                </td>
                <td align="center" style="width: 87px">
                    <cc1:DGTECTextBox ID="txtNum_CNJ" runat="server" Width="100px" Height="17px"></cc1:DGTECTextBox>
                </td>
            </tr>
            <tr>
            <td colspan="4">
            
                <cc1:DGTECGridView ID="GrdProcPerito" runat="server" 
                    Width="516px" HorizontalAlign="Center"
                                        OnPageIndexChanging="GrdProcPerito_PageIndexChanging" EmptyDataText="NÃO HÁ REGISTRO"
                                        AutoGenerateColumns="False" PageSize="5" AllowPaging="True" 
                                        EnableModelValidation="True">
                                        <Columns>
                                            <asp:CommandField SelectText="Exibir" ShowSelectButton="True" 
                                                ItemStyle-Width="25px" >
                                            <ItemStyle Width="25px" />
                                            </asp:CommandField>
                                            <asp:BoundField HeaderText="ID" DataField="ID_PF" ItemStyle-Width="30px" >
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-Width="150px" >
                                            <ItemStyle Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Nomeação" DataField="Data_Nomeacao" 
                                                ItemStyle-Width="30px" />
                                            <asp:BoundField HeaderText="Profissão" DataField="prof"  
                                                ItemStyle-Width="100px" >
                                            <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Especialidade" DataField="Espec" 
                                                ItemStyle-Width="50px" >
                                            <ItemStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Aceite" DataField="Aceite" ItemStyle-Width="30px"  
                                                Visible="false">
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="J.G." DataField="JG" ItemStyle-Width="10px" 
                                                Visible="false" >
                                            <ItemStyle Width="10px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Prazo" HeaderText="Prazo" ItemStyle-Width="20px" 
                                                Visible="false">
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
               <table border="0" cellpadding="10" width="520">
            <tr>
                <td width="80">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    Especialidade
                </td>
                <td>
                    <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" Width="425px"
                        Height="17px">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" width="50">
                    Perito
                </td>
                <td align="center">
                    <cc1:DGTECDropDownList ID="CboPerito" runat="server" AutoPostBack="True"
                        Width="450px" Height="17px">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" id="TblAcidente1">
                    PERÍCIA ACIDENTÁRIA&nbsp;                 </td>
            </tr>
        </table>
                <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center">
                    Parte                 </td>
                <td style="width: 87px" align="center">
                    <asp:DropDownList ID="CboParte" runat="server" Width="460px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center">
                    Próxima data disponível                 </td>
                <td style="width: 87px" align="center">
                    <uc2:CtlData1 ID="CtlData11" runat="server" />
                </td>
                <td align="center">
                    Data disponíveis subsequentes
                </td>
                <td align="center">
                    <cc1:DGTECDropDownList ID="CboDataDisponiveis" runat="server" Width="70px">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td rowspan="4" width="181">
                    <cc1:DGTECCalendar ID="DGTECCalendar1" runat="server">
                    </cc1:DGTECCalendar>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    Horários Disponíveis na data selecionada&nbsp;
                </td>
                <td>
                    <cc1:DGTECDropDownList ID="DGTECDropDownList2" runat="server" Width="120px" 
                        Height="17px">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    Data Selecionada
                </td>
                <td align="center" height="17">
                    <uc2:CtlData1 ID="CtlData12" runat="server" />
                </td>
                <td height="20" width="121">
                    <cc1:DGTECButton ID="BtnGravar" runat="server" Height="17px" Text="Gravar" 
                        Width="120px" />
                </td>
            </tr>
            <tr>
                <td height="20" align="center">
                    Hora Selecionada                 </td>
                <td align="center">
                    <cc1:DGTECTextBox ID="DGTECTextBox3" runat="server" Width="70px"></cc1:DGTECTextBox>
                </td>
                <td>
                    <cc1:DGTECButton ID="BtnIntimar" runat="server" Text="Intimar" Width="120px" 
                        Height="17px" />
                </td>
            </tr>
        </table>
        <table>
           <tr>
              <td height="17" width="100" valign="top">
                Data de Cancelamento :
              </td>
                <td align="center" height="17" style="width: 77px" valign="top">
                    <uc2:CtlData1 ID="DataCancel" runat="server" />
                </td>
              <td height="17" style="width: 115px" align="left" valign="top">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   Motivo :
                </td>
                <td height="17" align="center" style="width: 218px">
                    <cc1:DGTECTextBox ID="txtMotivo" runat="server" Width="190px" Height="80px"></cc1:DGTECTextBox>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td style="width: 155px">
                    &nbsp;</td>
                <td style="width: 69px">
                    <cc1:DGTECButton ID="BtnEmailNomeacao" runat="server" Text="Reenviar Email" />
                </td>
                <td style="width: 100px">
                    &nbsp;
                    <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Height="20px" 
                        Width="80px" />
                </td>
                <td style="width: 87px">
                    &nbsp;
                </td>
            </tr>
        </table>
         </td>
    </tr>
    </table>
    </div>
    </form>
</asp:Content>
