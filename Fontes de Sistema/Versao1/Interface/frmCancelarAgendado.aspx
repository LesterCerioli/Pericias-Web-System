<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master" CodeBehind="frmCancelarAgendado.aspx.vb" Inherits="Interface.frmCancelarAgendado" 
    title="Untitled Page" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div>
     <table class="corpo-form">
    <tr align="center">
    <th align="center">
                   REMARCAÇÃO E CANCELAMENTO DE PERÍCIA ACIDENTÁRIA</th>
    </tr>
    <table>
    <tr>
    <td>
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
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" id="Td2">
                    <cc1:DGTECLabel ID="DGTECLabel1" runat="server">
                        Marcações das Partes do Processo</cc1:DGTECLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" id="Td3">
                    <cc1:DGTECGridView ID="GrdProcPartes" runat="server" Width="516px" 
                        HorizontalAlign="Center" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px" 
                        CellPadding="3">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                        <asp:CommandField HeaderText="Selecione " SelectText="Exibir" 
                            ShowSelectButton="True"></asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle HorizontalAlign="Left" BackColor="#669999" 
                        Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </cc1:DGTECGridView>
                </td>
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
                <td align="center" id="Td1">
                    <cc1:DGTECLabel ID="DGTECLabel2" runat="server">
                        Marcações da Parte Selecionada</cc1:DGTECLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" id="TblAcidente1">
                    <cc1:DGTECGridView ID="GrdPericiasParte" runat="server" Width="516px" 
                        HorizontalAlign="Center" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="Inset" BorderWidth="1px" 
                        CellPadding="3">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                        <asp:CommandField HeaderText="Selecione " SelectText="Exibir" 
                            ShowSelectButton="True"></asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle HorizontalAlign="Left" BackColor="#669999" 
                        Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </cc1:DGTECGridView>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td>
                
                    <cc1:DGTECRadioButtonList ID="RdSit" runat="server" Width="388px" 
                        AutoPostBack="True" RepeatDirection="Horizontal">
                        <asp:ListItem Value="OptMed">Médido do Trabalho</asp:ListItem>
                        <asp:ListItem Selected="True" Value="OptEngSegTrab">Engenheiro de Segurança no 
                        trabalho.</asp:ListItem>
                    </cc1:DGTECRadioButtonList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" width="50">
                    Perito </td>
                <td align="center">
                    <cc1:DGTECDropDownList ID="CboPerito" runat="server" AutoPostBack="True"
                        Width="450px" Height="17px" Enabled="False">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
        </table>

        <table border="0" cellpadding="10" width="520">
            <tr>
                <td rowspan="4" width="181">
                    <cc1:DGTECCalendar ID="DGTECCalendar1" runat="server" Visible="False">
                    </cc1:DGTECCalendar>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;<cc1:DGTECLabel ID="lblHoraDisp" runat="server" Visible="False">Horários 
                        Disponíveis na data selecionada&nbsp;</cc1:DGTECLabel>
                </td>
                <td>
                    <cc1:DGTECDropDownList ID="DGTECDropDownList2" runat="server" Width="120px" 
                        Height="17px" Visible="False">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    Data da Perícia
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
                    Hora da Perícia</td>
                <td align="center">
                    <cc1:DGTECTextBox ID="DGTECTextBox3" runat="server" Width="70px"></cc1:DGTECTextBox>
                </td>
                <td>
                    <cc1:DGTECButton ID="BtnIntimar" runat="server" Text="Intimar" Width="120px" 
                        Height="17px" />
                </td>
            </tr>
        </table>
             <table border="0" cellpadding="10" width="520">
            <tr>
                <td>
                    <cc1:DGTECButton ID="BtnRemarcar" runat="server" Text="Remarcar" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <cc1:DGTECButton ID="BtnCancelar" runat="server" Text="Cancelar" />
                </td>
            </tr>
        </table>
    </td>
    </tr>
    </table>
    </div>
    </form>
</asp:Content>

