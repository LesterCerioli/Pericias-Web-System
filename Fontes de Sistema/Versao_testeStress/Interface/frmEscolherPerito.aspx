<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.Master" CodeBehind="frmEscolherPerito.aspx.vb" Inherits="Interface.frmEscolherPerito" Title="Nomeacao"  EnableSessionState="True" title="Escolher Peritos"%>

 <%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">

    <script type ="text/javascript" language="JavaScript">

	    txtID_PF.value = window.dialogArguments
        function closeMe()
        {
	    window.returnValue = Name.value
        event.returnValue = false
	    window.close()
	    return true
        };
        <title>Escolher Perito</title>
	</script>
    <form id="form1" runat="server" visible="True">
    <div style="text-align: center">
        <div style="text-align: center; width: 500px; height: 800px;">
            <asp:GridView ID="GrdPeritos" runat="server" Width="482px" CellPadding="1" 
            ForeColor="#333333" GridLines="None" 
                Height="107px" AllowPaging="True" >
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
                <Columns>
                    <asp:ButtonField CommandName="Selecionar" Text="Selecionar" />
                    <asp:ButtonField CommandName="Curriculum" Text="Curriculum" />
                </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" 
                    HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Center" Width="60px" />
                <EditRowStyle BorderStyle="Solid" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
            <br />
            <asp:TextBox ID="txtID_PF" runat="server" Visible="False"></asp:TextBox>
            <br />
    </div>
    </div>
  </form>
</asp:Content>
