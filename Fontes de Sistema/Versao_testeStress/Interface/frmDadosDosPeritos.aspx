<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master" CodeBehind="frmDadosDosPeritos.aspx.vb" Inherits="Interface.frmDadosDosPeritos" title="Dados do Perito"  %>
    
      <%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">


    <form id="FrmAutoriza"  runat="server">
    <div>
        <table>
            <tr>
                <td class="style4">

                    <img alt="" src="Imagens/Brasao_oficial.GIF" style="width: 69px; height: 78px" /></td>
                        <td class="style12"><span class="style7">TRIBUNAL DE JUSTIÇA DO ESTADO DO RIO DE 
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
                <td class="style3" colspan = "2">
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <i><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DADOS DO PERITO</b></i><br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style5" colspan = "2">
                    <table border="0" frame="box" style="width:100%;">
                        <tr>
                            <td class="style9">
                                Requerente<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox9" runat="server" Width="561px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                            <td class="style10">
                                Nº Registro do DCP<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox13" runat="server" Width="144px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" height="35">
                                Endereço<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox10" runat="server" Width="561px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                            <td height="35" class="style11">
                                CPF<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox14" runat="server" Width="144px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" height="35">
                                Endereço Trabalho<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox11" runat="server" Width="561px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                            <td height="35" class="style11">
                                Tel: Trabalho<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox15" runat="server" Width="144px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" height="35">
                                E-mail<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox12" runat="server" Width="561px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                            <td height="35" class="style11">
                                Tel: Residência<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox16" runat="server" Width="144px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" height="50">
                                Dados da Conta Corrente<br />
                                <br />
                                Banco:
                   <cc1:DGTECTextBox ID="DGTECTextBox6" runat="server" Width="34px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                Agência nº:
                   <cc1:DGTECTextBox ID="DGTECTextBox7" runat="server" Width="33px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                &nbsp;Conta Corrente nº:
                   <cc1:DGTECTextBox ID="DGTECTextBox8" runat="server" Width="70px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                                <br />
                            </td>
                            <td height="35" class="style11">
                                Tel: Celular<br />
                                <br />
                   <cc1:DGTECTextBox ID="DGTECTextBox17" runat="server" Width="144px" 
                        BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Rio de Janeiro,                     
                    <cc1:DGTECTextBox ID="DGTECTextBox3" runat="server" 
                        Width="24px" BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>
                    &nbsp;de
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
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    --------------------------------------------------<br />

                    <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Assinatura do 
                    Requerente</i></td>

            </tr>

        </table>

    </div>

    </form>

</asp:Content>
