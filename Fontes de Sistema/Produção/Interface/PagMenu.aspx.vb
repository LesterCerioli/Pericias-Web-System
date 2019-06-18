Imports System.Web
Imports System.Collections.Generic
Imports BAL
Imports Entidade
Imports WebServicePadrao
Imports SegurancaWeb.servicoSEG
'Imports UCEntidade

Partial Public Class PagMenu
    Inherits BasePage

    Private Sub PagMenu_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        'P ->Perito
        'S ->Serventia
        'D -> DIPEJ
        'I -> DGTEC
        'ID das <LI> da MasterPage -> auxiliares, dipej, nomeacao,principal, perito
        'UsuarioAutorizado = VerificaTipoUsu0
        '-------------------------------------
        Dim UsuarioAutorizado As String
        carregaInformacoesUsuario()
        Session("EDITA_Auxiliares") = "0"
        Session("EDITA_Perito") = "0"
        Session("EDITA_Nomecao") = "0"
        Session("EDITA_DIPEJ") = "0"
        Session("UsuarioAutorizado") = ""
        UsuarioAutorizado = ""
        If Not Session("JANELAS") Is Nothing Then
            For Each estAut In Session("JANELAS")
                If UCase(estAut.janela) = "FRMPERITODCP.ASPX" Then
                    If estAut.indAutorizado = "S" Then
                        Session("EDITA_NOMEACAO") = "1"
                        UsuarioAutorizado = "S"
                        Session("UsuarioAutorizado") = UsuarioAutorizado
                        '   Response.Redirect("Principal.aspx")
                    End If
                    'If estAut.objFunc = "NOMEACAO" Then
                    '    If estAut.indAutorizado = "S" Then
                    '        Session("EDITA_NOMEACAO") = "1"
                    '        UsuarioAutorizado = "S"
                    '        Session("UsuarioAutorizado") = UsuarioAutorizado
                    '        Response.Redirect("Principal.aspx")
                    '    End If
                    'End If
                End If
                If UCase(estAut.janela) = "FRMACEITACAO.ASPX" Then
                    If estAut.indAutorizado = "S" Then
                        Session("EDITA_PERITO") = "1"
                        UsuarioAutorizado = "P"
                    End If
                End If
                If UCase(estAut.janela) = "FRMESPECIALIDADE.ASPX" Then
                    If estAut.indAutorizado = "S" Then
                        Session("EDITA_AUXILIARES") = "1"
                        Session("EDITA_DIPEJ") = "1"
                        UsuarioAutorizado = "ID"
                        Session("UsuarioAutorizado") = UsuarioAutorizado
                        ' Response.Redirect("Principal.aspx")
                    End If
                End If
            Next
        End If
        Session("UsuarioAutorizado") = UsuarioAutorizado
        Response.Redirect("Principal.aspx")

    End Sub
   



End Class