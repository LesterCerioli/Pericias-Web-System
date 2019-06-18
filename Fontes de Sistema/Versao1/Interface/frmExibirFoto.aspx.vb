Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET
Imports App = System.Configuration

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
        Dim f_ID_PF As String
        Dim strImagem As String = String.Empty

        'IDGED_FOTO
        'IDGED_CV

        If Not IsPostBack Then
            Obj = New DGTECGEDAR
            f_ID_PF = Session("ID").ToString
            Ent = Bal.ExibirDadosEnt("ID", f_ID_PF, "S")
            If f_ID_PF = "" Then
                MsgErro("Identificador não encontrado")
                Exit Sub
            End If
            If Not Ent Is Nothing Then
                IDDoc = Ent.IDGED_Foto
            Else
                IDDoc = ""
            End If
            If IDDoc <> "" Then
                'FOS - Código foi modificado porque a aplicação foi implantanda gravando os arquivos no GED de homologação
                'Enquanto os arquivos não são migrados, a busca primeiro é feita em homologação e se não encontrar, deverá procurar em produção 
                If App.ConfigurationManager.AppSettings("AMBIENTE") = "3" Then
                    'Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                    'strImagem = Obj.Recupera(IDDoc, Server.MapPath("Imagens/FotoPerito.jpg"))
                    'Foto.ImageUrl = "Imagens/FotoPerito.jpg"
                    'Obj.Finaliza()
                    'If strImagem = "" Then
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Producao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                    Obj.Recupera(IDDoc, Server.MapPath("Imagens/FotoPerito.jpg"))
                    Foto.ImageUrl = "Imagens/FotoPerito.jpg"
                    Obj.Finaliza()
                    'End If
                ElseIf App.ConfigurationManager.AppSettings("AMBIENTE") = "2" Then
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                    strImagem = Obj.Recupera(IDDoc, Server.MapPath("Imagens/FotoPerito.jpg"))
                    Foto.ImageUrl = "Imagens/FotoPerito.jpg"
                    Obj.Finaliza()
                Else
                    Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao4, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                    strImagem = Obj.Recupera(IDDoc, Server.MapPath("Imagens/FotoPerito.jpg"))
                    Foto.ImageUrl = "Imagens/FotoPerito.jpg"
                    Obj.Finaliza()
                End If

            Else
                MsgErro("O Perito não possui foto!")
            End If
        End If
    End Sub

End Class