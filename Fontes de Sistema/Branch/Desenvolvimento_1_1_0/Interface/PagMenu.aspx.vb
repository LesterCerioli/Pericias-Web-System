Imports System.Web
Imports System.Collections.Generic
Imports BAL
Imports Entidade
Imports WebServicePadrao
Imports SegurancaWeb.servicoSEG
'Imports UCEntidade

Partial Public Class PagMenu
    Inherits System.Web.UI.Page

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
    Public Sub carregaInformacoesUsuario()
        Dim dsSeg As New System.Data.DataSet
        Dim estr As SegurancaWeb.ServicoCofre.EstUsuario

        estr = New SegurancaWeb.ServicoCofre.EstUsuario
        estr = Session("PERICIAS")
        Session("codOrgaoUsu") = Session("PERICIAS_CODORGAO")
        Session("Usuario") = estr.codUsu
        Session("CPFUsu") = estr.CPF
        Session("NomeUsuCPF") = estr.nome
        Session("NomeUsuMatr") = estr.matricula
        Session("UsuMatr") = estr.matricula
        Session("IdFunc") = estr.idFunc
        Session("Origem") = estr.origemUsuario
        Session("JANELAS") = estr.autorizacoes

    End Sub



End Class