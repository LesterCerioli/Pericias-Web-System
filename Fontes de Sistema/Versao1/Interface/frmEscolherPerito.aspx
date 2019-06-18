<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master"
    CodeBehind="frmEscolherPerito.aspx.vb" Inherits="Interface.frmEscolherPerito"
    Title="Nomeacao" EnableSessionState="True" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">

    <form id="form1" runat="server" visible="True">
    <div style="width: 520px" class="corpo-form">
        <div style="width:520px;text-align: center">
            <cc1:DGTECGridView ID="GrdPeritos" runat="server" Width="518px" 
                AllowPaging="True" OnPageIndexChanging="GrdPeritos_PageIndexChanging"
                BorderStyle="Solid" AutoGenerateColumns="False" PageSize="15" 
                EnableModelValidation="True">
                <Columns>
                    <asp:ButtonField CommandName="Selecionar" Text="OK" 
                        ItemStyle-Width="20px" >
                    <ControlStyle BorderStyle="Dotted" BorderWidth="1px" />
                    <HeaderStyle BorderStyle="Dotted" BorderWidth="1px" />
<ItemStyle Width="20px" BorderStyle="Dotted" BorderWidth="1px"></ItemStyle>
                    </asp:ButtonField>
                    <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-Width="150px" >
                    <ControlStyle BorderWidth="1px" />
                    <HeaderStyle BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" />
<ItemStyle Width="150px" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:ButtonField CommandName="Curriculum" Text="CV" 
                        ItemStyle-Width="30px" >
                    <HeaderStyle BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" />
<ItemStyle Width="30px" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="Foto" Text="Foto" ItemStyle-Width="25px" >
                    <HeaderStyle BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" />
<ItemStyle Width="25px" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonField>
                    <asp:BoundField HeaderText="ID" DataField="ID_PF" ItemStyle-Width="20px" >
                    <ControlStyle Width="20px" />
                    <FooterStyle Width="20px" />
                    <HeaderStyle Width="20px" />
<ItemStyle Width="20px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Perícias em Andamento" DataField="Proc_Sem_Laudo" 
                        ItemStyle-Width="60px" >
                    <ControlStyle Width="50px" />
                    <HeaderStyle BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" />
<ItemStyle Width="60px" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Nomeacoes Aprovadas" DataField="Proc_Aceitos" 
                        ItemStyle-Width="40px" >
                    <ControlStyle Width="50px" />
                    <HeaderStyle BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" />
<ItemStyle Width="60px" BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Proc_Recusados" HeaderText="Nomeacoes Recusados">
                    <ControlStyle Width="50px" />
                    <HeaderStyle BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Dotted" BorderWidth="1px" HorizontalAlign="Center" 
                        Width="60px" />
                    </asp:BoundField>
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
