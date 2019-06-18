<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master"
    CodeBehind="frmEscolherPerito.aspx.vb" Inherits="Interface.frmEscolherPerito"
    Title="Nomeacao" EnableSessionState="True" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">

    <form id="form1" runat="server" visible="True">
    <div style="width: 520px" class="corpo-form">
        <div style="width:520px;text-align: center">
            <cc1:DGTECGridView ID="GrdPeritos" runat="server" Width="518px" AllowPaging="True" OnPageIndexChanging="GrdPeritos_PageIndexChanging"
                BorderStyle="Solid" AutoGenerateColumns="false" PageSize="15">
                <Columns>
                    <asp:ButtonField CommandName="Selecionar" Text="Selecionar" ItemStyle-Width="20px" />
                    <asp:ButtonField CommandName="Curriculum" Text="Curriculum" ItemStyle-Width="30px" />
                    <asp:ButtonField CommandName="Foto" Text="Foto" ItemStyle-Width="25px" />
                    <asp:BoundField HeaderText="ID" DataField="ID_PF" ItemStyle-Width="20px" />
                    <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-Width="150px" />
                    <asp:BoundField HeaderText="Sem Laudo " DataField="Proc_Sem_Laudo" ItemStyle-Width="60px" />
                    <asp:BoundField HeaderText="Aceitos" DataField="Proc_Aceitos" ItemStyle-Width="40px" />
                </Columns>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BorderStyle="Solid" />
                <RowStyle CssClass="conteudo-grid" HorizontalAlign="Left"></RowStyle>
                <HeaderStyle CssClass="titulo-grid"></HeaderStyle>
            </cc1:DGTECGridView>
            <br />
            <asp:TextBox ID="txtID_PF" runat="server" Visible="False"></asp:TextBox>
            <br />
        </div>
        <div style="width: 520px; text-align:right;" > 
            <cc1:DGTECButton ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
        </div>
    </div>
    </form>
</asp:Content>
