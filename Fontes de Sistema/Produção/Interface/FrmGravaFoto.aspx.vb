Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET
Imports System.IO

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

        ExcluiArquivosPasta()
        
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

    Private Sub ExcluiArquivosPasta()
        Try
            Dim log As New StringBuilder

            For Each arquivo As String In IO.Directory.GetFiles(Server.MapPath("Fotos"))
                Try
                    If IO.File.GetCreationTime(arquivo) < Now.AddMinutes(-5) Then
                        IO.File.Delete(arquivo)
                    End If

                Catch ex As Exception
                    log.AppendLine(arquivo & " - " & ex.Message)
                End Try
            Next

            If log.Length > 0 Then
                IO.File.AppendAllText(Server.MapPath("Fotos/" & Format(Now, "dd-MM-yyyy HH-mm-ss") & "-Log.txt"), log.ToString)
            End If
        Catch

        End Try
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Try
            Dim Bal As New BALPERITO(GetUsuario)
            Dim CodigoPerito As String
            Dim caminho As String = String.Empty
            'IDGED_FOTO
            'IDGED_CV
            CodigoPerito = Session("CodigoPerito").ToString

            caminho = hfCaminhoFoto.Value
            If CodigoPerito <> "" Then
                If FotoPerito.ImageUrl = "" Then
                    'MsgErro("Informe a foto do perito.", "Erro")
                    lblErro.Text = "Informe a foto do perito."
                    Exit Sub
                End If
                Dim fs As New IO.FileInfo(caminho)
                If fs.Length = 0 Then
                    'MsgErro("O arquivo informado não é válido.", "Erro")
                    lblErro.Text = "O arquivo informado não é válido."
                    Exit Sub
                End If
                'f_ID_PF = Convert.ToInt64(Session("ID").ToString)
                Dim ambiente As Integer = CInt(ConfigurationManager.AppSettings("AMBIENTE"))
                Obj = New DGTECGEDAR
                If ambiente = 3 Then
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Producao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                Else
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                End If
                IDDoc = Obj.Armazena(caminho)
                'FotoPeritoMaq.ImageUrl = Server.MapPath("Imagem/Foto.jpg")
                Obj.Finaliza()
                'Else
                'MsgErro("O Perito ainda não foi gravado. Repita a operação apóa a gravação")
                'GRAVAR ID DO GED na Tabela Perito
                Bal.GravarFoto(CodigoPerito, IDDoc) '- Somente se o perito estiver cadastrado na tabela perito
                'MsgErro("Gravado com Sucesso", "Sucesso")
                lblErro.ForeColor = Drawing.Color.Green
                lblErro.Text = "Gravado com Sucesso"

            End If
            ExibirFoto()
        Catch ex As Exception
            lblErro.Text = ex.Message
        End Try
        

    End Sub

    Protected Sub BtnCarrega_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCarrega.Click
        Try
            Dim codigoPerito As Long = CLng(Session("CodigoPerito"))
            Dim nomeArquivo As String
            Dim caminhoArquivo As String

            nomeArquivo = String.Format("Foto-Perito-{0}", Now.Ticks.ToString)
            caminhoArquivo = Server.MapPath(String.Format("Fotos/{0}.jpg", nomeArquivo))

            FileUpload.SaveAs(caminhoArquivo)
            hfCaminhoFoto.Value = caminhoArquivo
            FotoPerito.ImageUrl = String.Format("Fotos/{0}.jpg", nomeArquivo)
        Catch ex As Exception
            lblErro.Text = ex.Message
        End Try


    End Sub
    Private Sub ExibirFoto()
        Try
            'DGTECGEDAR Class
            'DGTECGEDARDOTNET Namespace
            Dim Bal As New BALPERITO(GetUsuario)
            Dim Ent As EntPERITO
            Dim CodigoPerito As String
            Dim TipoPessoa As Integer

            'IDGED_FOTO
            'IDGED_CV
            CodigoPerito = Session("CodigoPerito").ToString

            TipoPessoa = CInt(Session("TipoPessoa"))
            Ent = Bal.ExibirDadosEnt("CODIGO", CodigoPerito, "N", 0, TipoPessoa)
            If Not Ent Is Nothing Then
                IDDoc = Ent.IDGED_Foto
            Else
                IDDoc = ""
            End If
            If IDDoc <> "" Then
                Dim ambiente As Integer = CInt(ConfigurationManager.AppSettings("AMBIENTE"))
                Obj = New DGTECGEDAR
                If ambiente = 3 Then
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Producao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                Else
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                End If

                'Obj.Recupera(IDDoc, Server.MapPath(String.Format("Fotos/{0}.jpg", nomeArquivo)))
                FotoArq.ImageUrl = Obj.MontaURLCacheWeb(IDDoc)
                Obj.Finaliza()
            End If
        Catch ex As Exception
            lblErro.Text = ex.Message
        End Try
        
    End Sub

End Class