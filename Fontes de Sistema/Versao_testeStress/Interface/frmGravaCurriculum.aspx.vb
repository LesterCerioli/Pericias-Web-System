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
                Obj = New DGTECGEDAR
                Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                IDDoc = Obj.Armazena(Server.MapPath("Imagens/CVArq.pdf"))
                'm_URL = Obj.MontaURLCacheWeb(IDDoc)
                Obj.Finaliza()
                'GRAVAR ID DO GED na Tabela Perito
                Bal.GravarCurriculum(f_ID_PF, IDDoc) '- Somente se o perito estiver cadastrado na tabela perito
            Catch ex As Exception
                Throw ex
            Finally
                MsgErro("Gravado com Sucesso")
                Session("CV") = "S"
            End Try

        End If
        'ExibirCurriculum()

    End Sub

    'Protected Sub BtnCarrega_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCarrega.Click
    'FileUpload.SaveAs(Server.MapPath("Imagens/CVArq.pdf"))
    'HyperCVAtual.ImageUrl = "Imagens/CVArq.pdf"
    'End Sub
    'Private Sub ExibirCurriculum()
    '    'DGTECGEDAR Class
    '    'DGTECGEDARDOTNET Namespace
    '    Dim Bal As New BALPERITO(GetUsuario)
    '    Dim Ent As EntPERITO
    '    Dim f_ID_PF As String

    '    'IDGED_FOTO
    '    'IDGED_CV
    '    f_ID_PF = Session("ID").ToString
    '    Ent = Bal.ExibirDadosEnt("ID", f_ID_PF)
    '    IDDoc = Ent.IDGED_Foto

    '    If IDDoc <> "" Then
    '        Obj = New DGTECGEDAR
    '        Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
    '        Obj.Recupera(IDDoc, Server.MapPath("Imagens/CVArq.pdf"))
    '        'HyperCVArq.ImageUrl = "Imagens/CVArq.pdf"
    '        Obj.Finaliza()
    '    End If
    'End Sub

End Class