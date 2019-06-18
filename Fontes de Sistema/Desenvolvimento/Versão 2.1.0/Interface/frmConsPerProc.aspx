<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master" CodeBehind="frmConsPerProc.aspx.vb" Inherits="Interface.frmConsPerProc" 
  %>
    
    <%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
  <div>
<table class="corpo-form">
    <tr>
    <th align="center">
                    PROCESSO COM ACEITAÇÃO/RECUSA DA PERÍCIA POR PERITO</th>
    </tr>
    <tr>
    <td>
    <table border="0" cellpadding="10" width="520">
            <tr>
                <td style="text-align: center;">
                    CPF&nbsp;                 </td>
                <td width="100" align="center">
                    &nbsp;<br />
                    <cc1:DGTECTextBox ID="txtCPF" runat="server" AutoPostBack="True" Height="17px" 
                        MaxLength="11" Enabled="False"></cc1:DGTECTextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="ValidarCPF" runat="server" ControlToValidate="txtCPF"
                        ErrorMessage="Preencher CPF">Preencher CPF</asp:RequiredFieldValidator>
                </td>
                <td width="60">
                    Nome<br />
                    <br />
                    Nomes Semelhantes
                </td>
                <td width="290">
                    <cc1:DGTECTextBox ID="txtNome" runat="server" AutoPostBack="True" Width="220px" 
                        Height="17px" Enabled="False"></cc1:DGTECTextBox>
                    &nbsp;&nbsp;
                    <cc1:DGTECTextBox ID="txtID_PF" runat="server" Height="17px" Width="10px" Visible="False"></cc1:DGTECTextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="ValidarNome" runat="server" ControlToValidate="txtNome"
                        ErrorMessage="Preencher o Nome">Preencher o Nome</asp:RequiredFieldValidator>
                    <br />
                    <cc1:DGTECDropDownList ID="CboPerito" runat="server" AutoPostBack="True" Height="17px"
                        Width="220px" Enabled="False">
                    </cc1:DGTECDropDownList>
                    &nbsp;&nbsp;
                    <cc1:DGTECTextBox ID="txtCod_Perito" runat="server" Visible="False" Height="17px"
                        Width="10px"></cc1:DGTECTextBox>
                </td>
                <td>
                    <cc1:DGTECButton ID="BtnGravar" runat="server" Text="Gravar" Width="50px" />
                    <br />
                    <cc1:DGTECButton ID="BtnExcluir" runat="server" Text="Excluir" Width="50px" />
                    <br />
                    <cc1:DGTECButton ID="BtnLimpar" runat="server" Text="Limpar" Width="50px" />
                </td>
            </tr>
    </table>
    <table border="0" cellpadding="10" width="520">
            <tr>
                <td width="40">
                    Orgão
                </td>
                <td width="60">
                    <cc1:DGTECDropDownList ID="CboOrgao_Per" runat="server" Width="200px" 
                        AutoPostBack="True" Height="17px" Enabled="False">
                    </cc1:DGTECDropDownList>
                </td>
                <td width="70">
                    Número Reg.                 </td>
                <td width="60">
                    <cc1:DGTECTextBox ID="txtNum_Reg" runat="server" Height="17px" Width="70px" 
                        Enabled="False"></cc1:DGTECTextBox>
                </td>
                <td align="center" width="70">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
    </table>
    <table border="0" cellpadding="10" width="520">
        <tr>
            <td style="width: 100px">
                Nº Processo Antigo</td>
            <td colspan="2" width="110">
                <asp:TextBox ID="txtNum_Processo" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                &nbsp;</td>
            <td width="120">
                Nº Processo Novo (CNJ)
            </td>
            <td width="180">
                <cc1:DGTECTextBox ID="txtNum_CNJ" runat="server" Enabled="False" Width="170px"></cc1:DGTECTextBox>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="10" width="520">
        <tr>
            <td style="width: 100px">
                Tipo de Processo</td>
            <td colspan="2" width="150">
                <asp:TextBox ID="TxtTipoProc" runat="server" Width="100px" Enabled="False">Primeira 
                Instância</asp:TextBox>
                &nbsp;
           </td>
           <td>
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
        <td width="100">
              Prazo para realização da perícia após a data de recebimento dos autos</td>
           <td>
           
               <cc1:DGTECTextBox ID="txtPrazo" runat="server" Width="20px" Enabled="False"></cc1:DGTECTextBox>
               &nbsp;dias
           </td>
           <td>
            <asp:RadioButtonList ID="RdoAceitacao" runat="server" AutoPostBack="True" 
                   Enabled="False">
                <asp:ListItem Value="Aceita">Aceita..</asp:ListItem>
                <asp:ListItem>Recusa</asp:ListItem>
            </asp:RadioButtonList>
            </td>
            <td align="left">
                <p>
                    Caso não seja feito a recusa num prazo de 5 dias, após a intimação, existe a 
                    presunção da aceitação.(Art.146 CPC), portanto será considera como aceita.
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
                                <asp:TextBox ID="TxtMotivo" runat="server" Enabled="False" 
                    Height="80px" Visible="False" Width="180px"></asp:TextBox>
                <br />
                <br />
            </td>
            <td style="width: 316px">
                Honorários :&nbsp;&nbsp;&nbsp;&nbsp; 
                <cc1:DGTECTextBox ID="txtHonorarios" 
                    runat="server" Enabled="False" 
                    Width="50px" Visible="False"></cc1:DGTECTextBox>
                &nbsp;UFIRs<br />
                <br />
                Profissao&nbsp;&nbsp;<br />
                &nbsp;<cc1:DGTECTextBox ID="txtProfissao" runat="server" Enabled="False" 
                    Width="300px"></cc1:DGTECTextBox><br />
                Especialidade&nbsp;<br />
                &nbsp;<cc1:DGTECTextBox ID="txtEspecialidade" runat="server" 
                    Enabled="False" Width="300px"></cc1:DGTECTextBox><br />
            </td>
            </tr>
    </table>
    <table border="0" cellpadding="10" width="520">
            <tr>
            <td>
            
                <cc1:DGTECGridView ID="GrdProcessos" runat="server" Width="513px">
                    <Columns>
                        <asp:CommandField SelectText="Exibir" ShowSelectButton="True" />
                    </Columns>
            </cc1:DGTECGridView>
            
            </td>
            </tr>
    </table>              
    <table border="0" cellpadding="10" width="520">
            <tr>
            <td><cc1:DGTECLinkButton ID="lnkVisualizaProcesso" runat="server" Visible ="false" >
                Visualiza processo.</cc1:DGTECLinkButton></td>
            </tr>
     </table>  
     <table border="0" cellpadding="10" style="height: 156px; width: 520px"> 
        <tr>
        <td colspan="6">
            &nbsp;</td>
        </tr>
     </table>
     </td>
     </tr>
 </table>

    </div>
    
    </form>

 

</asp:Content>
