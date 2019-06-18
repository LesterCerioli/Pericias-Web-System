<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPageExt.Master" CodeBehind="frmDeclaraRec.aspx.vb" Inherits="Interface.frmDeclaraRec" title="Declaração do Requerente" %>
      <%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">

    <form id="FrmAutorizaRec"  runat="server">
    <div>
        <table style="width:100%;" class="corpo-form">
             <tr>
                <td class="style8">

                    <img alt="" src="Imagens/Brasao_oficial.GIF" style="width: 69px; height: 78px" /> </td>
                        <td class="style3"><span class="style7">TRIBUNAL DE JUSTIÇA DO ESTADO DO RIO DE 
                            JANEIRO <br class="style7" />   
                        <span class="style7">DIRETORIA GERAL DE APOIO AOS ÒRGÃOS JURIDICIONAIS</span><br                          class="style7" />
                        <span class="style7">DEPARTAMENTO DE INSTRUÇÃO PROCESSUAL</span><br class="style7" />
                        <span class="style7">DIVISÃO DE PERÍCIAS JUDICIAS  </span><span class="style6"><b>
                   
                      <br />
                            </b>
                    </span>&nbsp;</td>
            </tr>
            <tr>
                <td class="style3" colspan = "2">
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <i><b>DECLARAÇÃO DO REQUERENTE</b></i><br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style5" colspan = "2">
                    <br />
                    Declara para devidos fins não ter impedimento profissional ou ético que o impeça 
                    de atuar como perito nomeado pelo Juíz em determinada demanda judicial e nem 
                    exerce atividade laboral neste Tribunal.<br />
                    Declara, ainda, serem verdadeiras as informações prestadas e documentos 
                    apresentados.&nbsp;&nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Rio de Janeiro,                     
                    <cc1:DGTECTextBox ID="DGTECTextBox3" runat="server" 
                        Width="24px" BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                    &nbsp; de
                   <cc1:DGTECTextBox ID="DGTECTextBox4" runat="server" Width="74px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                    &nbsp;de
                    <cc1:DGTECTextBox ID="DGTECTextBox5" runat="server" Width="39px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    --------------------------------------------------<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Assinatura do Requerente</i></td>

            </tr>

        </table>

    </div>

    </form>

</asp:Content>
