<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPageExt.Master" CodeBehind="frmAutorizaRec.aspx.vb" Inherits="Interface.frmAutorizaRec" title="Autorização do Requerente" %>
      <%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">

  <style type="text/css">
        .style3
        {
            text-align: left;
        }
        .style4
        {
            text-align: left;
            width: 62px;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            font-size: medium;
            font-weight: bold;
        }
        .style7
        {
            font-size: small;
            font-weight: bold;
        }
      .style8
      {
          text-align: left;
          height: 133px;
      }
    </style>
  <form id="FrmAutoriza"  runat="server">
    <div>
        <table style="width:100%;" class="corpo-form">
            <tr>
                <td class="style4">

                    <img alt="" src="Imagens/Brasao_oficial.GIF" style="width: 69px; height: 78px" /></td>
                        <td class="style3"><span class="style7">TRIBUNAL DE JUSTIÇA DO ESTADO DO RIO DE 
                            JANEIRO</span><br 
                                class="style7" />
                            <span class="style7">DIRETORIA GERAL DE APOIO AOS ÒRGÃOS JURIDICIONAIS</span><br 
                                class="style7" />
                            <span class="style7">DEPARTAMENTO DE INSTRUÇÃO PROCESSUAL</span><br 
                                class="style7" />
                            <span class="style7">DIVISÃO DE PERÍCIAS JUDICIAS 
                    </span><span class="style6"><b>
                      <br />
                            </b>
                    </span>&nbsp;</td>
            </tr>
            <tr>
                <td class="style8" colspan = "2">
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <i><b>AUTORIZAÇÃO DO REQUERENTE</b></i><br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style5" colspan = "2">
                    <br />
                    <br />
                    Autorizo
                    <cc1:dgtectextbox ID="DGTECTextBox6" runat="server" Width="287px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:dgtectextbox>
                    &nbsp;, Identidade,
                    <cc1:dgtectextbox ID="DGTECTextBox2" runat="server" Width="83px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:dgtectextbox>
                    , 
                    <br />
                                        CPF
                    <cc1:dgtectextbox ID="DGTECTextBox7" runat="server" Width="117px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:dgtectextbox>
                    &nbsp;a fazer carga dos autos em que eu for nomeado como perito.         
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Rio de Janeiro,                     
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
                    <br />
                   <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    --------------------------------------------------<i><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; Assinatura do Requerente</i></td>

            </tr>

        </table>

    </div>

    </form>
</asp:Content>
