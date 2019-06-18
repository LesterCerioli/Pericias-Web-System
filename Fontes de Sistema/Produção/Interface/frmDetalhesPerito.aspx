<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDetalhesPerito.aspx.vb"
    Inherits="Interface.frmDetalhesPerito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalhes do Perito para Nomeação</title>
    <link href="CSS/Alertfy/themes/default.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Alertfy/alertify.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Chosen/prism.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Chosen/chosen.min.css" rel="stylesheet" type="text/css" />
    <link href="CSS/Chosen/chosen.css" rel="stylesheet" type="text/css" />
    <link href="CSS/UI/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="CSS/W3CSS/w3.css" rel="stylesheet" type="text/css" />--%>
    <link href="CSS/W3CSS/w3.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Jquery/jquery-3.0.0.min.js" type="text/javascript"></script>
    <script src="JS/Chosen/chosen.jquery.min.js" type="text/javascript"></script>
    <script src="JS/UI/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="JS/Alertfy/alertify.min.js" type="text/javascript"></script>
    <script src="JS/MaskedInput/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="JS/Charcounter/charcounter.js" type="text/javascript"></script>
    <script src="JS/InicializaComponentes.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlPagina" Style="margin-left: 10px; overflow: auto; height: 700px;">
        <div id="divDadosPessoais">
            <span style="font-weight: bold; color: #8B8888;">Dados do Perito</span>
            <hr width="100%" />
            <br />
            <div id="dadosPessoais">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblTituloNomePerito" Style="font-weight: bold;" runat="server" Text="Nome: "></asp:Label>
                                        <asp:Label ID="lblNomePerito" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTituloCpfCnpj" Style="font-weight: bold;" runat="server" Text="CPF/CNPJ: "></asp:Label>
                                        <asp:Label ID="lblCpfCnpj" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTituloStatusPerito" Style="font-weight: bold;" runat="server" Text="Status: "></asp:Label>
                                        <asp:Label ID="lblStatusPerito" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <span style="font-weight: bold; color: #8B8888;">Anotações do Perito</span>
                            <hr width="100%" />
                            <table id="tblAnotacoes" cellpadding="5" cellspacing="5" runat="server">
                            </table>
                        </td>
                        <td style="width: 120px;">
                            <asp:Image ID="imgFotoPerito" runat="server" Height="160px" Width="120px" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div id="divDadosProfissao">
            <img alt="" src="Imagens/iconmonstr-arrow-70-24.png" estado="expandido" id="imgDadosProfissao"
                onclick="Expandir_Retrair_Detalhes_Geral('profissoes', 'imgDadosProfissao')" />&nbsp;
            <span style="font-weight: bold; color: #8B8888;">Profissões e Especialidades</span>
            <hr width="87%" align="left" />
            <div id="profissoes">
                <table id="tblProfissoes" runat="server" style="width: 400px;">
                </table>
            </div>
        </div>
        <br />
        <div id="divContatos">
            <img alt="" src="Imagens/iconmonstr-arrow-70-24.png" estado="expandido" id="imgContatos"
                onclick="Expandir_Retrair_Detalhes_Geral('contatos', 'imgContatos')" />&nbsp;
            <span style="font-weight: bold; color: #8B8888;">Contatos</span>
            <hr width="87%" align="left" />
            <div id="contatos">
                <table id="tblEndereco" style="width: 400px;">
                    <tr>
                        <td>
                            <asp:Image ID="Image2" ImageUrl="~/Imagens/icone email 24-24.png" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloEmailPrincipal" Text="Principal: " runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlEmailPrincipal" Text="" runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image1" ImageUrl="~/Imagens/icone email 24-24.png" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloEmailAnternativo" Text="Alternativo: " runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlEmailAlternativo" Text="" runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="grdTelefone" EmptyDataText="Não há registro" runat="server" AutoGenerateColumns="false"
                    Width="400px">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/icone telefone 24-24.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DESC_TIP_TEL" />
                        <asp:BoundField DataField="DDD" />
                        <asp:BoundField DataField="NUM_TEL" />
                    </Columns>
                </asp:GridView>
                <br />
                <span style="font-weight: bold;">Endereço Residencial:</span>
                <table style="width: 800px;">
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloLogrResidencial" runat="server" Text="Logradouro: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblLogrResidencial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloNumeroResidencial" runat="server" Text="Nº: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNumeroResidencial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloComplementoResidencial" runat="server" Text="Complemento: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblComplementoResidencial" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloBairroResidencial" runat="server" Text="Bairro: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBairroResidencial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloCEPResidencial" runat="server" Text="CEP: "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblCEPResidencial" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloCidadeResidencial" runat="server" Text="Cidade: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCidadeResidencial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloUFResidencial" runat="server" Text="UF: "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblUFResidencial" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <span style="font-weight: bold;">Endereço Comercial:</span>
                <table style="width: 800px;">
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloLogrComercial" runat="server" Text="Logradouro: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblLogrComercial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloNumeroComercial" runat="server" Text="Nº: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNumeroComercial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloComplementoComercial" runat="server" Text="Complemento: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblComplementoComercial" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloBairroComercial" runat="server" Text="Bairro: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBairroComercial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloCEPComercial" runat="server" Text="CEP: "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblCEPComercial" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloCidadeComercial" runat="server" Text="Cidade: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCidadeComercial" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloUFComercial" runat="server" Text="UF: "></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblUFComercial" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div id="divInfoComplementar">
            <img alt="" src="Imagens/iconmonstr-arrow-70-24.png" estado="expandido" id="imgInfoComplementar"
                onclick="Expandir_Retrair_Detalhes_Geral('informacoesComplementares', 'imgInfoComplementar')" />&nbsp;
            <span style="font-weight: bold; color: #8B8888;">Informações Complementares</span>
            <hr width="87%" align="left" />
            <div id="informacoesComplementares">
                <table style="width: 800px;">
                    <tr>
                        <td>
                            <asp:Label ID="lblTituloPeritoJuizo" Style="font-weight: bold;" runat="server" Text="Perito do Juízo: "></asp:Label>
                            <asp:Label ID="lblPeritoJuizo" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloDataCadastro" runat="server" Style="font-weight: bold;" Text="Data Cadastro: "></asp:Label>
                            <asp:Label ID="lblDataCadastro" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTituloDataMigração" runat="server" Style="font-weight: bold;" Text="Data Migração: "></asp:Label>
                            <asp:Label ID="lblDataMigração" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div id="divNurs">
            <span style="font-weight: bold; color: #8B8888;">NURs de Atuação</span>
            <hr width="100%" />
            <table id="tblNursComarcas" runat="server" style="width: 600px;">
            </table>
        </div>
        <br />
        <div id="divDocumentos">
            <img alt="" src="Imagens/iconmonstr-arrow-70-24.png" estado="expandido" id="imgDocumentos"
                onclick="Expandir_Retrair_Detalhes_Geral('documentos', 'imgDocumentos')" />&nbsp;<span
                    style="font-weight: bold; color: #8B8888;">Documentos</span>
            <hr width="100%" />
            <div id="documentos">
                <asp:GridView ID="grdDoc" EmptyDataText="Não há registro" runat="server" AutoGenerateColumns="false"
                    Width="80%" PageSize="4" AllowPaging="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink Target="_blank" ID="linkDocumento" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NomeArquivo" Visible="true" />
                        <asp:BoundField DataField="GedId" Visible="true" />
                    </Columns>
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
        <br />
        <asp:Button ID="Button1" Text="Sair" Width="100px" runat="server" OnClientClick="window.close(); return false;"
            Style="float: right; margin-right: 30px; margin-top: -30px;" /><br />
    </asp:Panel>
    </form>
</body>
</html>
