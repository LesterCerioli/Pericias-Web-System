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
     !-->
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
                <td width="80">
                    Especialidade
                </td>
                <td>
                    <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" Width="400px"
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
                    &nbsp;
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
             <table border="0" cellpadding="10" width="520">
            <tr>
                <td>
                    <cc1:DGTECCheckBox ID="chkNomeado" runat="server" Height="17px" Text="Nomeado" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <cc1:DGTECButton ID="BtnEmailNomeacao" runat="server" Text="Reenviar Email" />
                </td>
                <td style="width: 87px">
                    &nbsp;</td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td>
                    <cc1:DGTECCheckBox ID="chkLaudoLiberado" runat="server" Height="17px" Text="Laudo Liberado" />
                </td>
                <td style="width: 87px">
                    &nbsp;</td>
            </tr>
        </table>
         </td>
    </tr>
    </table>
    </div>
    </form>
</asp:Content>
