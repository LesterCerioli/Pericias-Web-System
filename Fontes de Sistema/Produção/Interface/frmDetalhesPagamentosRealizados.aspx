<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDetalhesPagamentosRealizados.aspx.vb"
    Inherits="Interface.frmDetalhesPagamentosRealizados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalhes Pagamentos Realizados</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlPagina" runat="server" Style="font-size: medium; margin-left: 5px;
        padding: 2px 2px 2px 2px; height: 600px; width: 750px; overflow: auto;">
        <div>
            <span style="font-weight: bold;">Dados do Perito</span>
            <hr width="100%" />
            <br />
            <table cellspacing="8">
                <tr>
                    <td style="font-weight: bold">
                        Nome:
                    </td>
                    <td>
                        <asp:Label ID="lblNomePerito" runat="server" Text="Não informado"></asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        CPF/CNPJ:
                    </td>
                    <td>
                        <asp:Label ID="lblCpfCnpj" runat="server" Text="Não informado"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold">
                        Profissão:
                    </td>
                    <td>
                        <asp:Label ID="lblProfissao" runat="server" Text="Não informado"></asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        Especialidade:
                    </td>
                    <td>
                        <asp:Label ID="lblEspecialidade" runat="server" Text="Não informado"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold">
                        Nº Registro:
                    </td>
                    <td>
                        <asp:Label ID="lblNumReg" runat="server" Text="Não informado"></asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        Sigla:
                    </td>
                    <td>
                        <asp:Label ID="lblSigla" runat="server" Text="Não informado"></asp:Label>
                    </td>
                    <td style="font-weight: bold">
                        UF:
                    </td>
                    <td>
                        <asp:Label ID="lblUF" runat="server" Text="Não informado"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <span style="font-weight: bold;">Dados do Pagamento</span>
            <hr width="100%" />
            <br />
            <table cellpadding="2" cellspacing="10">
                <tr>
                    <td style="font-weight: bold">Ofício</td>
                    <td><asp:Label ID="lblOficio" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Data Recebimento:</td>
                    <td><asp:Label ID="lblDataRec" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Relação Pgto:</td>
                    <td><asp:Label ID="lblRelPgto" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Nº Processo:</td>
                    <td><asp:Label ID="lblProcesso" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Processo Pgto:</td>
                    <td><asp:Label ID="lblProcPgto" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Data Protocolo:</td>
                    <td><asp:Label ID="lblDataProt" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Data Pgto:</td>
                    <td><asp:Label ID="lblDataPgto" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Valor:</td>
                    <td><asp:Label ID="lblValor" runat="server" Text="Não informado"></asp:Label></td>
                </tr>
            </table>
            <br />
            <div style="height: 30px;">
                <asp:Button ID="btnSair" Text="Sair" Width="100px" runat="server" OnClientClick="window.close(); return false;"
            Style="float: right; margin-right: 30px;" />
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
