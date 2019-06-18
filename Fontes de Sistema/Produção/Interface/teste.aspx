<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="teste.aspx.vb" Inherits="Interface.teste" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>Portal de Magistrados e Servidores | Sistema de Auxílio Educação
        
    </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <!--Arquivos Genéricos -->
    <script language="javascript" type="text/javascript" src="http://www.tjrj.jus.br/consultas/redirecionador/js/integracaoPortal.js"></script>
    <link rel="stylesheet" type="text/css" href="http://portaltj.tjrj.jus.br/web/guest/importacao-portal/-/importacao-portal/css" />
    <script type="text/javascript" src="http://portaltj.tjrj.jus.br/web/guest/importacao-portal/-/importacao-portal/script"></script>
    <script language="javascript" type="text/javascript" src="http://www.tjrj.jus.br/js_funcoes/max_min_font_size2.js"></script> 
   
   
    <!-- jQuery-->    
    
    <link href="CSS/estilo_pericias.css" rel="stylesheet" type="text/css" />

        
</head>
<body>
    
    <div id="loading" class="progressoAtualizacao" style="display: none">        
        <img id="imgLoading" src="Imagens/loading6.gif" alt="" />        
    </div>

    <div id="tudo">   
        <!-- Topo Padrao -->
       
        <!-- /Topo Padrao -->
    </div>
    <div id="navigation" class="welcome">
        <div id="navegador-horiz-limpo">
            <span class="welcome-message">
                Seja Bem-Vindo
                <label id="lblNomeUsuario"></label>
            </span>
            <label id="lblAmbiente" class="welcome-enviroment" runat="server"></label>
        </div>
    </div>
    <div id="container">
         <!-- Breadcrumb -->
        <div id="bread-crumb" class="breadCrumb module">
            <ul class="breadcrumbs lfr-component">
                <li class="first"><span><a href="#">Home</a></span></li>
                <li><span><a href="#">Portal de Magistrados e Servidores</a></span></li>
                <li><span><a href="Index.aspx">Sistema de Auxílio Educação</a></span></li>
                
            </ul>
        </div>

        <!-- Menu Lateral -->
            <div id="navegador" class="naveg-vertical navegador">
                <div class='titulo'>
                    <div class="conteudo">
                        <a href="Index.aspx">Portal de Magistrados e Servidores</a></div>
                </div>                
                <ul class="layouts level-1" id="listaMenu">
                    <li class="layouts level-1" id="L1" runat="server">
                        <div class="conteudo">
                            <a href="frmConsultarAuxEduc.aspx">Cadastrar Auxílio Educação</a><a class="icone"></a></div>
                    </li>
                    <li class="layouts level-1" id="L2" runat="server">
                        <div class="conteudo">
                            <a href="frmConsultarAuxEducAdm.aspx">Cadastrar Auxílio Educação - DEAPS</a><a class="icone"></a></div>
                    </li>                                                        
                </ul>      
                <div id="divComple">
            
                </div>         
            </div>
            
            <!-- /Menu Lateral -->

            <!-- Barra de Acessibilidade -->
            <div id="content-barra" style="clear: none;">
                <a href="javascript:decreaseFontSize();">
                    <img src="http://www.tjrj.jus.br/imagens/ico-font-menos.gif" alt="diminuir fonte" /></a>
                <a href="javascript:defaultFontSize();">
                    <img src="http://www.tjrj.jus.br/imagens/ico-font-default.gif" alt="normalizar fonte" /></a>
                <a href="javascript:increaseFontSize();">
                    <img src="http://www.tjrj.jus.br/imagens/ico-font-mais.gif" alt="aumentar fonte" /></a>
            </div>
            <!-- /Barra de Acessibilidade -->

            <div id="content">
                <div id="conteudo">
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                </div>
            </div>
           
    </div>    
    <!-- Fim da div#container -->
    <!-- Início DIV do Rodapé do Sistema -->
    
    <!-- Fim DIV do Rodapé do Sistema -->
</body>
</html>
