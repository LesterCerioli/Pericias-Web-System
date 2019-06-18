<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master"
    CodeBehind="frmPeritoDCP.aspx.vb" Inherits="Interface.frmPeritoDCP" Title="Untitled Page" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div>
        
    <table>
    <tr>
    <th align="left" bgcolor="#EFE6D6">
                    <br />
                    NOMEAÇÃO DE PERITOS
                    <br />
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
        <table border="0" cellpadding="10" width="520" class="">
            <tr>
            <td>
                &nbsp;
            <br />
              <cc1:DGTECGridView ID="GrdProcPerito" runat="server" Width="516px" 
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
               <br />    
            </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520" class="corpo-form">
            <tr>
                <td align="center" style="width: 130px">
                    Nº Processo Antigo 
                </td>
                <td align="center" style="width: 101px">
                    <cc1:DGTECTextBox ID="txtNum_Processo" runat="server" Width="100px" 
                        Height="17px" AutoPostBack="True" MaxLength="14">
                        </cc1:DGTECTextBox>
                </td>
                <td align="center" style="width: 124px">
                    Nº Processo Novo (CNJ)                 
                </td>
                <td align="left">
                    <cc1:DGTECTextBox ID="txtNum_CNJ1" runat="server" Width="85px" Height="17px" 
                        AutoPostBack="True" MaxLength="15"></cc1:DGTECTextBox>
                    .8.19.
                    <cc1:DGTECTextBox ID="txtNum_CNJ2" runat="server" Width="28px" Height="17px" 
                        AutoPostBack="True" MaxLength="4"></cc1:DGTECTextBox>
                      &nbsp;  
                    <cc1:DGTECTextBox ID="txtNum_CNJ" runat="server" 
                        AutoPostBack="True" Visible="False" Width="5px">
                    </cc1:DGTECTextBox>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
            <td>
                &nbsp;</td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td width="80">
                    Profissao      </td>
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
                    Especialidade      </td>
                <td>
                    <cc1:DGTECDropDownList ID="CboEspecialidade" runat="server" AutoPostBack="True" Width="400px"
                        Height="20px">
                    </cc1:DGTECDropDownList>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="left" width="80">
                    Perito
                </td>
                <td align="left">
                    <cc1:DGTECTextBox ID="txtNome_Perito" runat="server" Enabled="False" 
                        Height="20px" Width="400px"></cc1:DGTECTextBox>
                    &nbsp;<cc1:DGTECTextBox ID="txtID_PF" runat="server" Visible="False" Width="10px"></cc1:DGTECTextBox>
                    <br />
                    <br />
                    &nbsp;<cc1:DGTECButton ID="BtnPerito" runat="server" Text="Selecionar Perito" 
                        Width="125px" Height="21px" />
                             
                         &nbsp;&nbsp;&nbsp; &nbsp;
                    <cc1:DGTECButton ID="BtnCurriculum" runat="server" 
                             Text="Curriculum" Height="21px" Width="100px" />
                         &nbsp;&nbsp;&nbsp; &nbsp;
                    <cc1:DGTECButton ID="BtnVerFoto" runat="server" 
                    Text="Foto" Height="21px" Width="50px" />
                    <br />
                    &nbsp;
                    <cc1:DGTECLabel ID="LblAnotacao" runat="server" Width="512px" Font-Size="Small" 
                    Visible="False">Anotações</cc1:DGTECLabel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="10" width="520">
              <tr>
                <td align="center">
                    <cc1:DGTECTextBox ID="txtAnotacao" runat="server" Height="200px" Width="420px" 
                        Visible="False" Enabled="False" TextMode="MultiLine"></cc1:DGTECTextBox>
                        <br />
                        <br />
                </td>
                </tr>
         </table>
         <table border="0" cellpadding="10" width="520">
         <tr>
         <td>
                    &nbsp;&nbsp; &nbsp;&nbsp;
                    <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Height="17px" />
                    &nbsp; &nbsp;&nbsp;&nbsp;
                    <cc1:DGTECButton ID="BtnNovo" runat="server" Height="17px" Text="Limpar" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <cc1:DGTECButton ID="BtnEmailNomeacao" runat="server" 
                    Text="Reenviar Email de Nomeação" Height="17px" Width="150px" />
                         &nbsp; &nbsp;&nbsp;
                    <cc1:DGTECButton ID="BtnAnotacao" runat="server" 
                        Height="17px" TextMode="multiline" Text = "Enviar Anotações à DIPEJ"
                        Width="158px" />
         </td>
         </tr>
         </table>
         <table border="0" cellpadding="10" width="520">
            <tr>
            <td>
                Data da Aceitação :<cc1:DGTECLabel ID="lblData_Aceitacao" runat="server"></cc1:DGTECLabel>
            </td>
            <td>
                Data da Negação :<cc1:DGTECLabel ID="lblData_Negacao" runat="server"></cc1:DGTECLabel>
            </td>
            </tr>
         </table>
         <table border="0" cellpadding="10" width="520">
            <tr>
                <td width="200"> Data da Nomeação :                     <cc1:DGTECLabel ID="lblData_Nomeacao" runat="server" Enabled="False"></cc1:DGTECLabel>
                </td>
                <td>
                    <cc1:DGTECCheckBox ID="ChkJustGrat" runat="server" Text=" Justiça Gratuita" />
                </td>

                </tr>
          </table>
                    <table border="0" cellpadding="10" width="520">
                <tr>
                <td>Email : <cc1:DGTECLabel ID="lblEmail" runat="server" Enabled="False" 
                             Width="180px"></cc1:DGTECLabel>
                     </td>
                                     <td width="285">
                                         Email Alternativo :
                    <cc1:DGTECLabel ID="lblEmail1" runat="server" Width="160px"></cc1:DGTECLabel>
                                    </td>
                </tr>
          </table>
          <table border="0" cellpadding="10" width="520">
                <tr>
                <td > Prazo para realização da perícia após a data de recebimento dos autos:
                <cc1:DGTECTextBox ID="txtPrazo" runat="server" Width="30px">
                </cc1:DGTECTextBox>
                    &nbsp;dias
                </td>
                </tr>
          </table>
          <table border="0" cellpadding="10" width="520">
            <tr>

                <td style="width: 285px">
                    <cc1:DGTECCheckBox ID="chkLaudoLiberado" runat="server" Height="17px" 
                        Text=" Laudo Aceito" AutoPostBack="True" Width="127px" />
                    Data&nbsp; :                     <cc1:DGTECLabel ID="lblData_Liberacao" runat="server" Enabled="False"></cc1:DGTECLabel>
                </td>
            </tr>
          </table>
          <table border="0" cellpadding="10" width="520">
            <tr>
                <td align="center" width="150">Data de Cadastramento na DIPEJ</td>
                <td width="50">
                    <cc1:DGTECLabel ID="lblData_Cadastramento" runat="server" Enabled="False" 
                        Height="17px" Width="52px">__ /__ /__
                    </cc1:DGTECLabel>
                </td>
                <td align="center" width="120">Quantidade de Processo Aceitos</td>
                <td width="35">
                    &nbsp;<cc1:DGTECTextBox ID="txtQteAceitos" runat="server" Width="30px" 
                        Enabled="False"></cc1:DGTECTextBox></td>
                <td align="center" width="110">Quantidade de Processo Pendentes</td>
                <td width="40">
                    &nbsp;<cc1:DGTECTextBox ID="txtQtePendentes" runat="server" Width="30px" 
                        Enabled="False"></cc1:DGTECTextBox></td>
            </tr>
           </table>
           </td>
           </tr>
        </table>
    </div>
    </form>
</asp:Content>
