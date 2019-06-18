<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPageExt.Master"
    CodeBehind="frmReqInscCadPerDipej.aspx.vb" Inherits="Interface.frmReqInscCadPerDipej"
    Title="Requerimento de Inscrição" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <div style="width: 550px">
    <form id="FrmAutoriza" runat="server">

        <table style="width: 540px;" border="0" class="corpo-form">
                    <tr>
                    <td align="center">
                    <img alt="" src="Imagens/Brasao_oficial.GIF" style="width: 69px; height: 78px" />&nbsp;&nbsp;&nbsp;&nbsp; 
                    </td>
                    <td colspan="2" width="600">
                        TRIBUNAL DE JUSTIÇA DO ESTADO DO RIO DE JANEIRO<br />
                        DIRETORIA GERAL DE APOIO AOS ÒRGÃOS JURIDICIONAIS<br>
                        DEPARTAMENTO DE INSTRUÇÃO PROCESSUAL<br>
                        DIVISÃO DE PERÍCIAS JUDICIAS</td>
                       </tr>
                       </table>
                    <table align="left" style="height: 100px;" width="540" class="corpo-form">
                        <tr style="width: 600px">
                            <td align="center" 
                                style="border-style: hidden; border-width: 1px; width: 139px;" width="200">
                                Foto     </td>
                            <td width="200">
                            </td>
                            <td align="center" style="border-style: double; border-width: 1px;" width="200">
                                &nbsp;Recebimento<br />
                                <br />
                                ----/----/----<br />
                                <br />
                                <br />
                                -------------------------------<br />
                                <br />
                                Nome/Matricula
                            </td>
                        </tr>
                    </table>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                    <br />
                        <table style="width: 540px;" border="0" class="corpo-form">
                            <tr>
                                <td>
                                    <i>REQUEREMENTO DE INSCRIÇÃO NO CADASTRO DE PERICIAS NA DIPEJ</i>
                                </td>
                            </tr>
                        </table>
            <table style="width: 540px;" border="0" class="corpo-form">
                <tr>
                    <td style="width: 186px">
                        REQUERENTE:
                    </td>
                    <td class="style14" width="420">
                        <asp:TextBox ID="TextBox1" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 186px">
                        Nº REGISTRO NO DCP:                     </td>
                    <td class="style12">
                        &amp;&nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="420px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style9" colspan="2" width="540">
                        Sr. Diretor, o perito acima qualificado vem, respeitosamente nos termos do Aviso 
                        TJ nº 24 de 2009, requerer seu cadastramento e/ou atualização de seu cadastro já 
                        existente.r                         <br />
                        <br />
                        Rio de Janeiro,
                        <cc1:DGTECTextBox ID="DGTECTextBox3" runat="server" Width="24px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        &nbsp;de
                        <cc1:DGTECTextBox ID="DGTECTextBox4" runat="server" Width="74px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        &nbsp;de
                        <cc1:DGTECTextBox ID="DGTECTextBox5" runat="server" Width="39px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        <br />
                        <br />
                        <br />
                        &nbsp;--------------------------------------------------<br />
                        <i>&nbsp;Assinatura do Requerente<br />
                        </i>
                    </td>
                </tr>
            </table>
            <table style="width: 540;" border="0" width="540" align="left" class="corpo-form">
                <tr>
                    <td align="center" colspan="2">
                        PARA USO DO DIPEJ
                    </td>
                </tr>
                <tr style="width: 600px">
                    <td class="style18" align="left">
                        Senhor diretor,<br />
                        &nbsp; Sugiro o cadastramento e/ou atualização do cadastro&nbsp;&nbsp;&nbsp;
                        <br />
                        &nbsp; já existente, na forma supra descrita, vez que
                        <br />
                        &nbsp; atendido os requisitos do Aviso 24/2009<br />
                        <br />
                        <br />
                        Rio de Janeiro,
                        <cc1:DGTECTextBox ID="DGTECTextBox6" runat="server" Width="24px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        &nbsp;de
                        <cc1:DGTECTextBox ID="DGTECTextBox7" runat="server" Width="74px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        &nbsp;de
                        <cc1:DGTECTextBox ID="DGTECTextBox8" runat="server" Width="39px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        <br />
                        <br />
                        ----------------------------------------------<br />
                             <i>Assinatura do Requerente <i/>
                            <br />
                        </i>
                    </td>
                    <td>
                        De acordo, cadastra-se.<br />
                        <br />
                        Rio de Janeiro, &nbsp;<cc1:DGTECTextBox ID="DGTECTextBox12" runat="server" Width="29px"
                            BorderColor="#F2F2F2" BorderStyle="Solid"></cc1:DGTECTextBox>de
                        <cc1:DGTECTextBox ID="DGTECTextBox10" runat="server" Width="56px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        &nbsp;de
                        <cc1:DGTECTextBox ID="DGTECTextBox11" runat="server" Width="39px" BorderColor="#F2F2F2"
                            BorderStyle="Solid"></cc1:DGTECTextBox>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        -----------------------------------------<br />
                             <i>Assinatura do Requerente<br />
                            <br />
                        </i>
                    </td>
                </tr>
        </table>

    </form>
        </div>
</asp:Content>
