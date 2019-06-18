<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.master" CodeBehind="frmConsProcPer.aspx.vb" Inherits="Interface.frmConsProcPer" 
    title="Untitled Page" %>
       
    <%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<%@ Register src="Controles/CtlData1.ascx" tagname="CtlData1" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
        <table class="corpo-form">
    <tr>
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
                    <cc1:DGTECTextBox ID="txtNum_Processo" runat="server" Width="100px" 
                        Height="17px" AutoPostBack="True"></cc1:DGTECTextBox>
                </td>
                <td align="center" width="100">
                    Nº Processo CNJ
                </td>
                <td align="center" style="width: 87px">
                    <cc1:DGTECTextBox ID="txtNum_CNJ" runat="server" Width="100px" Height="17px" 
                        AutoPostBack="True"></cc1:DGTECTextBox>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
            <td>
                <cc1:DGTECGridView ID="GrdProcPerito" runat="server">
                    <Columns>
                        <asp:CommandField HeaderText="Selecione" SelectText="Exibir" 
                            ShowSelectButton="True"></asp:CommandField>
                    </Columns>
                </cc1:DGTECGridView>
                
               <table border="0" cellpadding="10" width="520">
            <tr>
                <td>
               <table border="0" cellpadding="10" width="520">
            <tr>
                <td width="80">
                    Profissao      </td>
                <td>
                    <cc1:DGTECDropDownList ID="CboProfissao" runat="server" AutoPostBack="True" Width="400px"
                        Height="17px" Enabled="False">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
                        <tr>
                <td width="80">
                    Especialidade      </td>
                <td>
                    <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" Width="400px"
                        Height="17px" Enabled="False">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
        </table>
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
                        Width="450px" Height="17px" Enabled="False">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
        </table>
           <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" width="200">
                    <cc1:DGTECLabel ID="LblAnotacao" runat="server" Width="200px" Font-Size="Small" 
                        Visible="False">Anotações</cc1:DGTECLabel>
                </td>
                <td style="width: 370px" align="center">
                    &nbsp;</td>
            </tr>
              <tr>
                <td align="center" colspan="2">
                    <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="200px" Width="420px" 
                        Visible="False" Enabled="False" TextMode="MultiLine"></cc1:DGTECTextBox>
                </td>
                </tr>
                </table>
                     <table border="0" cellpadding="10" width="520">
                     <tr>
                     <td>
                         &nbsp;&nbsp;&nbsp;&nbsp;
                         <cc1:DGTECButton ID="BtnNovo" runat="server" Height="17px" Text="Limpar" />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                     </td>
                     </tr>
                                          </table>
                      <table border="0" cellpadding="10" width="520">
            <tr>
                <td width="285"> Data da Nomeação :                    <cc1:DGTECLabel ID="lblData_Nomeacao" runat="server" Enabled="False">
                    </cc1:DGTECLabel>
                </td>
                <td>Email : <cc1:DGTECLabel ID="lblEmail" runat="server" Enabled="False" 
                             Width="180px">
                         </cc1:DGTECLabel>
                     </td>
                </tr>
                     </table>
             <table border="0" cellpadding="10" width="520">
            <tr>
                <td width="285">
                    Email Alternativo :
                    <cc1:DGTECLabel ID="lblEmail1" runat="server" Width="160px">
                    </cc1:DGTECLabel>
                                    </td>
                <td style="width: 285px">
                    <cc1:DGTECCheckBox ID="chkLaudoLiberado" runat="server" Height="17px" 
                        Text="Laudo Liberado" AutoPostBack="True" Width="95px" Enabled="False" />
                    &nbsp;- Data&nbsp;
                    <cc1:DGTECLabel ID="lblData_Liberacao" runat="server" Enabled="False">
                    </cc1:DGTECLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td>Data de Cadastramento na DIPEJ/td&gt;                 <td>
                    <cc1:DGTECLabel ID="lblData_Cadastramento" runat="server" Enabled="False">
                    </cc1:DGTECLabel>
                </td>
                <td>Quantidade de Processo Aceitos</td>
                <td width="60">
                    &nbsp;<cc1:DGTECTextBox ID="txtQteAceitos" runat="server" Width="30px" 
                        Enabled="False"></cc1:DGTECTextBox></td>
                <td>Quantidade de Processo Pendentes</td>
                <td width="60">
                    &nbsp;<cc1:DGTECTextBox ID="txtQtePendentes" runat="server" Width="30px" 
                        Enabled="False"></cc1:DGTECTextBox></td>
            </tr>
        </table>
                
            </td>
            </tr>
            </table>
               </td>
    </tr>
    </table>
    </form>
</asp:Content>
