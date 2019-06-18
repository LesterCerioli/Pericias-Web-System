<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPagePerito.master" CodeBehind="Principal.aspx.vb" Inherits="Interface.Principal" 
    title="Untitled Page" %>

<%@ Register Assembly="ClienteWebPadrao" Namespace="ClienteWebPadrao" TagPrefix="cc1" %>
<%@ Register Src="Controles/CtlData1.ascx" TagName="CtlData1" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="10" width="520">
      <tr>
      <td align="center">
          <cc1:DGTECLabel ID="lblPrincipal" runat="server">
              Processos com Peritos Nomeados aguardando Resposta
          </cc1:DGTECLabel>
      </td>
      </tr>
  </table>
      <table border="0" cellpadding="10" width="520">
      <tr>
      <td align="center">

          <cc1:DGTECGridView ID="GrdNomeados" runat="server" 
              DataSourceID="DGTECSqlDataSource1" AutoGenerateColumns="False" 
              Width="490px" AllowPaging="True" AllowSorting="True">
              <Columns>
                  <asp:CommandField HeaderText="Substituição" SelectText="Substituição" 
                      ShowSelectButton="True" ButtonType="Image" 
                      SelectImageUrl="~/Imagens/Substituição.JPG" ShowCancelButton="False" >
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:CommandField>
                  <asp:BoundField DataField="PROCESSO" HeaderText="Processo" 
                      SortExpression="PROCESSO" />
                  <asp:BoundField DataField="NOME" HeaderText="Perito" SortExpression="NOME" />
                  <asp:BoundField DataField="DATA_NOMEACAO" HeaderText="Dt Nomeação" 
                      SortExpression="DATA_NOMEACAO" />
                  <asp:BoundField DataField="TELEFONE1" HeaderText="Telefone" 
                      SortExpression="TELEFONE" />
                  <asp:BoundField DataField="Telefone2" HeaderText="Telefone Alt" 
                      SortExpression="Telefone2" />
              </Columns>
          </cc1:DGTECGridView>
          <cc1:DGTECSqlDataSource ID="DGTECSqlDataSource1" runat="server" 
              ConnectionString="<%$ ConnectionStrings:PerConex %>" 
              ProviderName="<%$ ConnectionStrings:PerConex.ProviderName %>" 
              
              
              SelectCommand="SELECT PP.NUM_CNJ AS PROCESSO, PF.NOME, PP.DATA_NOMEACAO, TAB1.TELEFONE1, TAB2.TELEFONE2 FROM (SELECT PFTEL.NUM_TEL AS TELEFONE2, PFTEL.ID_PF FROM UC.PESSOAFISICATELEFONE PFTEL INNER JOIN UC.PESSOAFISICA PF2 ON PFTEL.ID_PF = PF2.ID_PF WHERE (PFTEL.SEQ_TEL = 2)) TAB2 INNER JOIN (SELECT PFTEL.NUM_TEL AS TELEFONE1, PFTEL.ID_PF FROM UC.PESSOAFISICATELEFONE PFTEL INNER JOIN UC.PESSOAFISICA PF1 ON PFTEL.ID_PF = PF1.ID_PF WHERE (PFTEL.SEQ_TEL = 1)) TAB1 ON TAB2.ID_PF = TAB1.ID_PF INNER JOIN UC.PESSOAFISICA PF ON TAB2.ID_PF = PF.ID_PF INNER JOIN PERITOS P ON PF.ID_PF = P.ID_PF INNER JOIN PROCESSO_PERITO PP ON P.ID_PF = PP.ID_PF WHERE (NOT (PP.DATA_NOMEACAO IS NULL)) AND (PP.DATA_ACEITACAO IS NULL) AND (PP.DATA_NEGACAO IS NULL) AND (PP.DATA_NOMEACAO &lt; Sysdate - 7) ORDER BY PP.DATA_NOMEACAO DESC">
          </cc1:DGTECSqlDataSource>

      </td>
      </tr>
  </table>

  </div>
</form>
</asp:Content>
