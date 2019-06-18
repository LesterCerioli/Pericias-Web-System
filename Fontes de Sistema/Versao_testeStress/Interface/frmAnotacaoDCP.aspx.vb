Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Partial Public Class frmAnotacaoDCP
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Dim Bal As New BalEmail(GetUsuario)
        Dim p_Html As String
        Dim p_To As String
        Dim p_Cc As String
        Dim p_From_Nome As String
        Dim p_From_Address As String
        Dim p_Subject As String
        Dim p_Smtp_Hostname As String
        Dim p_Smtp_Portnum As String
        Dim m_Email As String
        'Servetia e Usuário
        p_Html = " <html> <body>  Nome do Perito " + Session("Nome").ToString + " </br>  Data " + Today.ToString + " </br>  </br> </br>" + txtAnotacao.Text + " </body> </html>"
        'DESTINO (DIPEJ)
        m_Email = "santo@tjrj.jus.br"
        If UCase(GetUsuario.Login) = "SANTO" Then
            m_Email = "santo@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "KELLYCOC" Then
            m_Email = "kellycoc@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "VALERIASUZART" Then
            m_Email = "valeriasuzart@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "CHENRIQUE" Then
            m_Email = "chenrique@tjrj.jus.br"
        End If
        p_To = m_Email
        p_Cc = ""
        p_From_Nome = "DGJUR/DEINP/DIPEJ"
        'ORIGEM EMAIL SERVENTIA
        p_From_Address = "santo@tjrj.jus.br"
        p_Subject = "Anotações"
        p_Smtp_Hostname = "mail.tjrj.jus.br"
        p_Smtp_Portnum = "25"
        Bal.EnviarEmail(p_To, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)


    End Sub
End Class