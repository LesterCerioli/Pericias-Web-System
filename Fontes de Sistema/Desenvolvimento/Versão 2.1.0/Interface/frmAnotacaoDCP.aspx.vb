Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports log4net
Imports App = System.Configuration.ConfigurationManager

Partial Public Class frmAnotacaoDCP
    Inherits BasePage

    Dim logger As log4net.ILog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        logger = log4net.LogManager.GetLogger("LogInFile")
        logger.Debug("Acesso ao envio de anotação ...")

    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click

        logger.Debug("BtnEnviar_Click ...")

        If txtAnotacao.Text = "" Then
            MsgErro("Informar a anotação.")
            Exit Sub
        End If

        Dim Bal As New BalEmail(GetUsuario)
        Dim p_Html As String
        Dim p_To As String
        Dim p_Cc As String
        Dim p_From_Nome As String
        Dim p_From_Address As String
        Dim p_Subject As String
        Dim p_Smtp_Hostname As String
        Dim p_Smtp_Portnum As String
        Dim m_Email As String = String.Empty
        'Servetia e Usuário
        p_Html = " <html> <body> Nome do Perito: " + Session("Nome").ToString + " </br>  Data: " + Today.ToShortDateString + " </br>  </br> </br>" + txtAnotacao.Text + " </body> </html>"
        'DESTINO (DIPEJ)
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
        If UCase(GetUsuario.Login) = "ANAPEREIRA" Then
            m_Email = "anapereira@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "FRANCIANASANTOS" Then
            m_Email = "francianasantos@tjrj.jus.br"
        End If

        logger.Debug("Corpo do email: " & p_Html)
        logger.Debug("p_To: " & m_Email)
        p_To = m_Email
        p_Cc = ""
        'p_From_Nome = "DGJUR/DEINP/DIPEJ"
        p_From_Nome = App.AppSettings("FromAddressNomeacao").ToString
        'ORIGEM EMAIL SERVENTIA

        p_From_Address = "santo@tjrj.jus.br"

        p_Subject = "Anotações"

        p_Smtp_Hostname = App.AppSettings("SmtpHostname").ToString
        'p_Smtp_Hostname = "mail.tjrj.jus.br"

        p_Smtp_Portnum = App.AppSettings("SmtpPortnum").ToString
        'p_Smtp_Portnum = "25"

        logger.Debug("Bal.EnviarEmail(p_To, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)")
        Bal.EnviarEmail(p_To, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        logger.Debug("Email enviado.")
        MsgErro("E-mail foi enviado com sucesso.", "Sucesso")
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.close();", True)


    End Sub
End Class