<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPageExt.Master"
    CodeBehind="frmReqInscCadPerDipej.aspx.vb" Inherits="Interface.frmReqInscCadPerDipej"
    Title="Requerimento de Inscrição" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <div style="width: 550px">
    <br />
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
                                    <i>REQUERIMENTO DE INSCRIÇÃO NO CADASTRO DE PERÍCIAS NA DIPEJ</i>
                                </td>
                            </tr>
                <tr>
                    <td align="center" colspan="2">
                        PARA PREENCHIMENTO PELO REQUERENTE
                    </td>
                </tr>
                        </table>
            <table style="width: 540px;" border="0" class="corpo-form">
                <tr>
                    <td style="width: 186px">
                        REQUERENTE:
                    </td>
                    <td class="style14" width="420">
                        <asp:TextBox ID="TextBox1" runat="server" Width="420px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 186px">
                        Nº REGISTRO NO DCP:                     </td>
                    <td class="style12">
                        <asp:TextBox ID="TextBox2" runat="server" Width="420px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style9" colspan="2" width="540">
                        <br />
                        Sr. Diretor,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                        Venho, respeitosamente, requerer meu cadastramento nos termos da Resolução CM Nº 
                        3/2011, declarando para os devidos fins que não tenho impedimento legal, 
                        profissional ou ético que me impeça de atuar como perito do juízo em determinada 
                        demanda judicial, bem como não exerço atividade laborativa remunerada por este 
                        Tribunal ou empresa por ele contratada. Declaro, ainda, que todas as informações 
                        por mim prestadas e documentos apresentados são verdadeiros, sob pena de 
                        responsabilidade civil,, penal e administrativa.<br />
                        <br />
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
           
    </form>
        </div>
</asp:Content>
