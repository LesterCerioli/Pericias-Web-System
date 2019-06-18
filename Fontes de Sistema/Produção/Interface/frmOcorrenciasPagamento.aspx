<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmOcorrenciasPagamento.aspx.vb"
    Inherits="Interface.frmOcorrenciasPagamento" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ocorrências de Pagamento</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlPagina" runat="server" Style="margin-left: 10px; height: 500px;
        width: 700px; overflow: auto;">
        <div style="height: 400px;">
            <asp:Label ID="lblProcesso" Text="" Style="color: Blue;" runat="server">
            </asp:Label>
            <br />
            <br />
            <cc1:DGTECGridView ID="grdAjudaCustoPerito" EmptyDataText="NÃO HÁ DADOS" CellPadding="3"
                CellSpacing="3" runat="server" AutoGenerateColumns="false" Width="80%" AllowPaging="true">
                <Columns>
                    <asp:BoundField DataField="NOME_PERITO" HeaderText="Perito" />
                    <asp:BoundField DataField="OFICIO" HeaderText="Ofício" />
                    <asp:BoundField DataField="PAGAMENTO" HeaderText="Data Pag." />
                    <asp:TemplateField HeaderText="Valor (R$)">
                        <ItemTemplate>
                            <asp:Label ID="lblValor" runat="server" Text='<%# String.Format("{0:N}", Eval("VALOR")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="VALOR" HeaderText="Valor (R$)" />--%>
                </Columns>
            </cc1:DGTECGridView>
        </div>
        <asp:Button ID="btnSair" Text="Sair" Width="100px" runat="server" OnClientClick="window.close(); return false;"
            Style="float: right; margin-right: 70px; margin-top: 50px;" />
        <br />
    </asp:Panel>
    </form>
</body>
</html>
