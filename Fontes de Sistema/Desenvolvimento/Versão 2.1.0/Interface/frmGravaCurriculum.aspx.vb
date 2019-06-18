Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET

Partial Public Class FrmGravaCurriculum
    Inherits BasePage
    Public Obj As DGTECGEDAR
    Public IDDoc As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'ExibirCurriculum()
        End If

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Dim Bal As New BALPERITO(GetUsuario)
        Dim f_ID_PF As String
        Dim Extensao As String

        If FileUpload.FileName = "" Then
            MsgErro("Gravação rejeitada. Seleção do arquivo no formato PDF é obrigatório.")
            Exit Sub
        End If

        Extensao = Mid(FileUpload.FileName, Len(FileUpload.FileName) - 2, 3)

        If UCase(Extensao) <> "PDF" Then
            MsgErro("Gravação rejeitada. Somente serão aceitos arquivos de extensão PDF")
            Exit Sub
        End If
        FileUpload.SaveAs(Server.MapPath("Imagens/CVArq.pdf"))
        'IDGED_FOTO
        'IDGED_CV
        f_ID_PF = Session("ID").ToString
        If f_ID_PF <> "" Then
            Try
                Dim ambiente As Integer = CInt(ConfigurationManager.AppSettings("AMBIENTE"))
                Obj = New DGTECGEDAR
                If ambiente = 3 Then
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Producao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                Else
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                End If
                IDDoc = Obj.Armazena(Server.MapPath("Imagens/CVArq.pdf"))
                'm_URL = Obj.MontaURLCacheWeb(IDDoc)
                Obj.Finaliza()
                'GRAVAR ID DO GED na Tabela Perito
                Bal.GravarCurriculum(f_ID_PF, IDDoc) '- Somente se o perito estiver cadastrado na tabela perito
            Catch ex As Exception
                Throw ex
            Finally
                MsgErro("Gravado com Sucesso", "Sucesso")
                Session("CV") = "S"
            End Try

        End If
        'ExibirCurriculum()

    End Sub

End Class