<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/LayoutPrincipal.master" CodeBehind="Principal.aspx.vb" Inherits="Interface.Principal" 
    title="Principal" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">


<!--
<script type="text/javascript">
var vSiglaSistema = 'PERICIAS';
</script>
<script type="text/javascript" src="http://webserver.tjrj.jus.br/portaldesistemas/integracaoPortalSistemas.js"></script>
-->
    <form id="form1" runat="server">
    <div>
  <%--  <table border="0" cellpadding="0" width="520" class="corpo-form">
      <tr>
      <td align="center">
          <cc1:DGTECLabel ID="lblPrincipal" runat="server" Font-Bold="true">
              Processos com nomeação rejeita pelo perito.
          </cc1:DGTECLabel>
      </td>
   
      </tr>
  </table>--%>
      <table border="0" cellpadding="0" width="520">
      <tr>
      <td align="center">
          <cc1:DGTECLabel ID="lblPrincipal" runat="server" Font-Bold="True">
              Processos com nomeação rejeitada pelo perito.
          </cc1:DGTECLabel>
      </td>
      <%--<th>
        Processos Com Nomeação Rejeita Pelo Perito
      </th>--%>
      </tr>
      <tr>
      <td >
          <cc1:DGTECGridView ID="GrdNomeados" runat="server" EmptyDataText="NÃO HÁ REGISTROS" Width="520px"
               AutoGenerateColumns="False" OnPageIndexChanging="GrdNomeado_PageIndexChanging" PageSize="10" AllowPaging="true" >
              <Columns>
                  <asp:CommandField SelectText="Substituição" 
                      ShowSelectButton="True" ButtonType="Image" 
                      SelectImageUrl="~/Imagens/Substituição.JPG" ShowCancelButton="False" ItemStyle-Width="30px" >
                  </asp:CommandField>
                 <%-- <asp:TemplateField >
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="Imagens\Substituição.JPG" ID="btnSubstituir" runat="server" CausesValidation="false" Width="30px"
                        CommandName="btnSusbtituir_Command" OnCommand="btnSusbtituir_Command" CommandArgument='<%# eval("PROCESSO") & "," & eval("NOME")%>' 
                        ToolTip="Substituir perito" Text="Substituição" />
                    </ItemTemplate>
                  </asp:TemplateField>--%>
                  <asp:BoundField DataField="PROCESSO" HeaderText="Processo" ItemStyle-Width="140px" />
                  <asp:BoundField DataField="NOME" HeaderText="Perito" ItemStyle-Width="115px" />
                  <asp:BoundField DataField="DATA_NOMEACAO" HeaderText="Nomeação" ItemStyle-Width="40px" />
                  <%--<asp:BoundField DataField="DATA_NEGACAO" HeaderText="Negação" ItemStyle-Width="40px" />--%>
                  <asp:BoundField DataField="TELEFONE1" HeaderText="Tel. 1" ItemStyle-Width="55px" />
                  <asp:BoundField DataField="Telefone2" HeaderText="Tel. 2" ItemStyle-Width="50px" />
              </Columns>
             <%--<PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle CssClass="conteudo-grid"></RowStyle>
                 <HeaderStyle CssClass="titulo-grid"></HeaderStyle>--%>
          </cc1:DGTECGridView>

      </td>
      </tr>
  </table>

  </div>
</form>
</asp:Content>
