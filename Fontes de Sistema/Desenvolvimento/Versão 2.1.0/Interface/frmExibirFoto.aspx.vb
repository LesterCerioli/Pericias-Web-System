Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET
Imports System.IO

Partial Public Class FrmExibirFoto
    Inherits BasePage
    Public Obj As DGTECGEDAR
    Public IDDoc As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim Bal As New BALPERITO(GetUsuario)
        'Dim Ent As EntPERITO
        'Dim f_ID_PF As String
        'Dim NomeArq As String
        ''IDGED_FOTO
        ''IDGED_CV
        'f_ID_PF = Session("ID").ToString
        'Ent = Bal.ExibirDadosEnt("ID", f_ID_PF)
        'IDDoc = Ent.IDGED_Foto

        'If IDDoc <> "" Then
        '    NomeArq = ""
        '    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
        '    Obj.Recupera(IDDoc, "C:\FotoPerito.jpg")
        '    FotoPerito.ImageUrl = "C:\FotoPerito.jpg"
        '    Obj.Finaliza()
        'Else
        '    MsgErro("O Perito não possui foto!")
        'End If
        Dim Bal As New BALPERITO(GetUsuario)
        Dim Ent As EntPERITO
        Dim CodigoPerito As String
        Dim TipoPessoa As Integer
        Dim nomeArquivo As String
        'IDGED_FOTO
        'IDGED_CV

        'IDGED_FOTO
        'IDGED_CV
        If Not IsPostBack Then
            Obj = New DGTECGEDAR
            CodigoPerito = Session("CodigoPerito").ToString

            TipoPessoa = CInt(Session("TipoPessoa"))
            Ent = Bal.ExibirDadosEnt("CODIGO", CodigoPerito, "S", 0, TipoPessoa)
            If CodigoPerito = "" Then
                MsgErro("Identificador não encontrado", "erro")
                Exit Sub
            End If
            If Not Ent Is Nothing Then
                IDDoc = Ent.IDGED_Foto
            Else
                IDDoc = ""
            End If
            If IDDoc <> "" Then
                Dim ambiente As Integer = CInt(ConfigurationManager.AppSettings("AMBIENTE"))
                If ambiente = 3 Then
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Producao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                Else
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                End If
                'Obj.Recupera(IDDoc, Server.MapPath(String.Format("Fotos/{0}.jpg", nomeArquivo)))
                Foto.ImageUrl = Obj.MontaURLCacheWeb(IDDoc)
                Obj.Finaliza()
            Else
                'MsgErro("O Perito não possui foto!", "erro")
                lblErro.Text = "O Perito não possui foto!"
            End If
        End If
    End Sub

End Class