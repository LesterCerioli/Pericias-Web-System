<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDesativarPerDCP.aspx.vb" Inherits="Interface.frmDesativarPerDCP" EnableSessionState="True" title="Desatvivar Perito"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>

<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server" visible="True">
    <div style="text-align: center">
    
        <div style="text-align: center">
    
        Selecione o(s) Peritos que serão desativados no Sistema Comarca<asp:GridView 
                ID="GrdDesativar" runat="server" Width="467px" CellPadding="1" 
            ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
                Height="20px" EnableModelValidation="True" >
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText="Selecione">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Chk" runat="server" 
                            Checked='<%# IIF(EVAL("Marcado") = 1,True,False) %>' />
                        <cc1:DGTECLabel ID="lbl_Cod_Comarca" runat="server" 
                            Visible="False" Height="19px" Text='<%# eval("Cod_Perito") %>' 
                            Width="30px"></cc1:DGTECLabel>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <cc1:DGTECLabel ID="lbl_Nome" runat="server" Text='<%# eval("Nome") %>' 
                            Visible="False"></cc1:DGTECLabel>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" 
                        HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:BoundField DataField="Nome" HeaderText="Nome" >
                    <ControlStyle BorderStyle="Solid" Height="20px" Width="500px" />
                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                        HorizontalAlign="Left" VerticalAlign="Middle" Width="400px" />
                </asp:BoundField>
                <asp:BoundField DataField="Cod_Perito" HeaderText="Código do Perito" 
                    ConvertEmptyStringToNull="False" ReadOnly="True" >
                    <ControlStyle BorderColor="Black" BorderStyle="Solid" Height="20px" 
                        Width="100px" />
                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                        HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Num_Reg" HeaderText="RG" />
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
            <cc1:DGTECButton ID="BtnMarcar" runat="server" Text="Marcar Todas" />
            <cc1:DGTECButton ID="BtnDesmarcar" runat="server" Text="Desmarcar Todas" />
    <cc1:DGTECButton ID="BtnConfirmar" runat="server" Text="Confirmar" />
    
  
            <asp:Button ID="Button1" runat="server" Text="Button" />
    
  
    </div>
    
   
    </div>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </form>
</body>
</html>

