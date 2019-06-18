Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET
Imports App = System.Configuration

Partial Public Class FrmGravaFoto
    Inherits BasePage
    Public Obj As DGTECGEDAR
    Public IDDoc As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'GED
        'AbortaTransacao Abortar uma transação desfazendo-se todas as operações realizadas em seu escopo.  
        'Armazena Método de armazenamento de documentos  
        'ConverteParaRecordset Converte um DataTable para Recordset  
        'Exclui Exclui de forma física ou lógica uma versão de um documento previamente armazenado no GED.  
        'Finaliza Finaliza sessão do GED. Após a finalização, para retomar a sessão com chamada de métodos do GED deve-se proceder com a execução do método Inicializa.  
        'FinalizaTransacao Finaliza uma transação no GED realizando desfecho com sucesso de todas as operações ocorridas dentro da transação.  
        'FinalizaTransacaoDistribuida Overloaded.  
        'GaranteAcesso Garante acesso a um documento para um sistema  
        'Inicializa Inicializa o objeto GED de forma análoga ao método construtor com os mesmos parâmetros deste método. Util para aplicações que utilizem a interface COM deste objeto e só tem acesso ao método construtor sem parâmetros. Por exemplo: aplicações escritas em Visual Basic 6.  
        'IniciaTransacao Inicia uma transação no GED permitindo o desfecho com sucesso ou abortamento de todas as operações ocorridas dentro da transação.  
        'MontaURLCacheWeb Retorna URL completa para download pelo navegador de internet do documento cujo ID foi passado como parâmetro.  
        'ObtemHistorico Retorna um DataTable com o histórico de acessos a um documento ou a uma de suas versões.  
        'ObtemHistoricoRS Retorna um ADODB.Recorset com o histórico de acessos a um documento ou a uma de suas versões.  
        'ObtemMetadados Retorna um DataTable com os metadados de uma ou todas as versões de um documento armazenado no GED.  
        'ObtemMetadadosRS Retorna um ADODB.Recordset com os metadados de uma ou todas as versões de um documento armazenado no GED.  
        'ObtemTipoServIdx Obtem o tipo do servidor de indexação a partir do código de armazenamento de um documento  
        'Recupera Recupera Documento armazenado previamente no GED  
        'RestauraExclusao Restaura documento excluído logicamente.  
        'RevogaAcesso Revoga acesso a um documento de um sistema  
        'Substitui Substitui documento armazenado previamente no GED com versionamento - a versão anterior pode ser recuperada do banco sem versionamento - a versão anterior não pode ser recuperada do banco  
        'Versao Retorna a versão do componente GEDAR  


        If Not IsPostBack Then
            ExibirFoto()
        End If
        ''DGTECGEDAR Class
        ''DGTECGEDARDOTNET Namespace
        'Dim Bal As New BALPERITO(GetUsuario)
        'Dim Ent As EntPERITO
        'Dim f_ID_PF As String

        ''IDGED_FOTO
        ''IDGED_CV
        ''If Not IsPostBack Then
        'f_ID_PF = Session("ID").ToString
        'Ent = Bal.ExibirDadosEnt("ID", f_ID_PF)
        'IDDoc = Ent.IDGED_Foto

        'If IDDoc <> "" Then
        '    Obj = New DGTECGEDAR
        '    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
        '    Obj.Recupera(IDDoc, Server.MapPath("Imagens/FotoArq.jpg"))
        '    FotoArq.ImageUrl = "Imagens/FotoArq.jpg"
        '    Obj.Finaliza()
        '    'Else
        '    'MsgErro("O Perito não possui foto!")
        'End If
        ''End If
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Dim Bal As New BALPERITO(GetUsuario)
        Dim f_ID_PF As String
        Dim caminho As String = Server.MapPath("Imagens/Foto.jpg")
        Dim TipoServIndexGED As New DGTECGEDARDOTNET.DGTECGEDAR.TipoServidorIndexacao
        'IDGED_FOTO
        'IDGED_CV
        f_ID_PF = Session("ID").ToString
        If f_ID_PF <> "" Then
            If FotoPerito.ImageUrl = "" Then
                MsgErro("Informe a foto do perito.")
                Exit Sub
            End If
            Dim fs As New IO.FileInfo(caminho)
            If fs.Length = 0 Then
                MsgErro("O arquivo informado não é válido.")
                Exit Sub
            End If
            'f_ID_PF = Convert.ToInt64(Session("ID").ToString)
            Obj = New DGTECGEDAR
            If App.ConfigurationManager.AppSettings("AMBIENTE") = "3" Then
                TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Producao2
            ElseIf App.ConfigurationManager.AppSettings("AMBIENTE") = "2" Then
                TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Homologacao2
            Else
                TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Homologacao4
            End If
            'Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
            Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, TipoServIndexGED, DGTECGEDAR.TipoServidorWebService.Automatico, False)
            IDDoc = Obj.Armazena(caminho)
            'FotoPeritoMaq.ImageUrl = Server.MapPath("Imagem/Foto.jpg")
            Obj.Finaliza()
            'Else
            'MsgErro("O Perito ainda não foi gravado. Repita a operação apóa a gravação")
            'GRAVAR ID DO GED na Tabela Perito
            Bal.GravarFoto(f_ID_PF, IDDoc) '- Somente se o perito estiver cadastrado na tabela perito
            MsgErro("Gravado com Sucesso")
        End If
        ExibirFoto()

    End Sub

    Protected Sub BtnCarrega_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCarrega.Click
        FileUpload.SaveAs(Server.MapPath("Imagens/Foto.jpg"))  ' ("C:\Foto1.jpg")  ' & FileUpload.FileName)
        'FotoPerito.ImageUrl = FileUpload.PostedFile.FileName
        'FotoPerito.ImageUrl = Server.MapPath("Imagens/Foto1.jpg")
        FotoPerito.ImageUrl = "Imagens/Foto.jpg"
    End Sub
    Private Sub ExibirFoto()
        'DGTECGEDAR Class
        'DGTECGEDARDOTNET Namespace
        Dim Bal As New BALPERITO(GetUsuario)
        Dim Ent As EntPERITO
        Dim f_ID_PF As String
        Dim TipoServIndexGED As New DGTECGEDARDOTNET.DGTECGEDAR.TipoServidorIndexacao

        'IDGED_FOTO
        'IDGED_CV
        f_ID_PF = Session("ID").ToString
        Ent = Bal.ExibirDadosEnt("ID", f_ID_PF, "N")
        If Not Ent Is Nothing Then
            IDDoc = Ent.IDGED_Foto
        Else
            IDDoc = ""
        End If
        If IDDoc <> "" Then
            Obj = New DGTECGEDAR
            'If App.ConfigurationManager.AppSettings("AMBIENTE") = "3" Then
            '    TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Producao2
            'Else
            '    TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Homologacao2
            'End If
            If App.ConfigurationManager.AppSettings("AMBIENTE") = "3" Then
                TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Producao2
            ElseIf App.ConfigurationManager.AppSettings("AMBIENTE") = "2" Then
                TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Homologacao2
            Else
                TipoServIndexGED = DGTECGEDAR.TipoServidorIndexacao.Homologacao4
            End If
            Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, TipoServIndexGED, DGTECGEDAR.TipoServidorWebService.Automatico, False)
            Obj.Recupera(IDDoc, Server.MapPath("Imagens/FotoArq.jpg"))
            FotoArq.ImageUrl = "Imagens/FotoArq.jpg"
            Obj.Finaliza()
        End If
    End Sub

End Class