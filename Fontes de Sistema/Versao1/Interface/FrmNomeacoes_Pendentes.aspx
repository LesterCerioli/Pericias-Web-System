<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmNomeacoes_Pendentes.aspx.vb" Inherits="Interface.FrmNomeacoes_Pendentes" %>

<%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

* { margin: 0; padding: 0; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div style="text-align: center; width: 793px;">
    
            Nomeações Pendentes<br />
                                <cc1:DGTECTextBox ID="txtID_Nomeacao" runat="server" 
                Width="100px" Height="17px"
                                    AutoPostBack="True" MaxLength="14" Visible="False"></cc1:DGTECTextBox>
                            <br />
            &nbsp;<asp:GridView ID="GrdNomPend" runat="server" 
                Width="789px" CellPadding="3" AutoGenerateColumns="False" 
                Height="35px" EnableModelValidation="True" BackColor="White" 
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
            <RowStyle ForeColor="#000066" HorizontalAlign="Center" 
                    VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="Num_CNJ" HeaderText="Número do Processo" >
                    <ControlStyle BorderStyle="Solid" Width="100px" 
                    BorderWidth="1px" />
                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" 
                    HorizontalAlign="Center" Wrap="True" Width="100px" />
                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" 
                        HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" 
                    Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Nome" HeaderText="Nome do Perito" 
                    ConvertEmptyStringToNull="False" ReadOnly="True" >
                    <ControlStyle BorderColor="Black" BorderStyle="Solid" 
                        Width="130px" />
                    <HeaderStyle Font-Size="Small" Wrap="False" Width="130px" />
                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" 
                        HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Small" 
                    Width="130px" Wrap="True" />
                </asp:BoundField>
                <asp:BoundField DataField="Descr_Profissao" HeaderText="Profissao" >
                <ControlStyle Width="70px" />
                <HeaderStyle Font-Size="Small" Width="70px" />
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" 
                    Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="Descr_Especialidade" HeaderText="Especialidade" >
                <ControlStyle Width="100px" />
                <HeaderStyle Font-Size="Small" Width="100px" />
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" 
                    Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="ID_PF" HeaderText="ID_PF">
                <ControlStyle Font-Size="Small" Width="0px" />
                <HeaderStyle Font-Size="Small" Width="0px" />
                <ItemStyle Font-Size="Small" Width="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="Data_Nomeacao" DataFormatString="{0:dd/MM/yyyy}" 
                    HeaderText="Nomeacao">
                <ControlStyle Width="20px" />
                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" 
                    Width="20px" />
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" Font-Size="Small" Width="20px" 
                    Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Data_Aceitacao" DataFormatString="{0:dd/MM/yyyy}" 
                    HeaderText="Aceitação" >
                <HeaderStyle Font-Size="Small" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Data_Novo_Hon" DataFormatString="{0:dd/MM/yyyy}" 
                    HeaderText="Hon. Juiz">
                <HeaderStyle Font-Size="Small" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="Tacita" HeaderText="Tacita" >
                <FooterStyle Font-Size="Small" />
                <ItemStyle Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="ID_Nomeacao" Visible="False" 
                    HeaderText="ID_Nomeacao" />
                <asp:TemplateField HeaderText="Exibir" ItemStyle-Width="40px">
                   <ItemTemplate>
                     <asp:ImageButton ImageUrl="imagens\img_lupa.gif" ID="btnGrdNomPend" runat="server"
                    CausesValidation="False" CommandArgument='<%# eval("ID_Nomeacao") %>'
                    Text="ID_Nomeacao" OnCommand="btnGrdNomPend_Command" 
                    ToolTip="Visualizar dados da nomeação.">
                    </asp:ImageButton>
                  </ItemTemplate>
                   <ItemStyle Width="40px"></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Center" Width="60px" />
                <EditRowStyle BorderStyle="Solid" />
        </asp:GridView>
    
  
    </div>
    
   
    </div>
    </form>
</body>
</html>
