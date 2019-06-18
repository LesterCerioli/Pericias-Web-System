Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
''Imports log4net
Imports App = System.Configuration.ConfigurationManager

Partial Public Class frmAnotacaoDCP
    Inherits BasePage

    ''Dim logger As log4net.ILog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''logger = log4net.LogManager.GetLogger("LogInFile")
        ''logger.Debug("PreencherEspecialidade()")
        PreencherTipo_Anotacao()
        'logger.Debug("Acesso ao envio de anotação ...")
        txtCPF.Text = Request.QueryString("m_CPF")
        txtAnotacao.Text = "CPF : " & txtCPF.Text & Chr(13)

    End Sub

    'cboTipo_anotacao

    Private Sub PreencherTipo_Anotacao()
        ''logger.Debug("PreencherTipo_Anotacao ...")
        Dim bal As New BalTipoAnotacao(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        cboTipo_anotacao.Items.Clear()
        cboTipo_anotacao.DataTextField = "Descr_Tipo_Anotacao"
        cboTipo_anotacao.DataValueField = "Cod_Tipo_Anotacao"
        cboTipo_anotacao.DataSource = dsfila.Tables(0)
        cboTipo_anotacao.DataBind()
        cboTipo_anotacao.Items.Insert(0, "Selecione o Tipo da Anotacao")
        cboTipo_anotacao.SelectedIndex = 0

    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click

        ''logger.Debug("BtnEnviar_Click ...")

        If txtAnotacao.Text = "" Then
            MsgErro("Informar a anotação.")
            Exit Sub
        End If

        Dim Bal As New BalEmail(GetUsuario)
        Dim BalUsu As New BALUSUARIO(GetUsuario)
        Dim BalOrg As New BALORGAO(GetUsuario)
        Dim p_Html As String
        Dim p_To As String
        Dim p_Cc As String
        Dim p_From_Nome As String
        Dim p_From_Address As String
        Dim p_Subject As String
        Dim p_Smtp_Hostname As String
        Dim p_Smtp_Portnum As String
        Dim mm_Serventia As String
        Dim mm_Usuario As String
        Dim DssServentia As DataSet
        Dim BalComarcaMail As New BALCOMARCA(GetUsuario)
        Dim rssServentia As DataRow
        Dim m_Email As String = String.Empty
        mm_Serventia = ""
        mm_Usuario = ""
        'Serventia
        ''logger.Debug("BalComarcaMail.ExibirDadosServentia(" & GetUsuario.CodOrg & ")")
        DssServentia = BalComarcaMail.ExibirDadosServentia(GetUsuario.CodOrg)
        If DssServentia.Tables(0).Rows.Count > 0 Then
            rssServentia = DssServentia.Tables(0).Rows(0)
            If Not rssServentia("Descr_Serventia").ToString = Nothing Then
                mm_Serventia = rssServentia("Descr_Serventia").ToString '  + "/" + rssServentia("Descr_Comarca").ToString
            End If
        End If
        'Usuario
        'Usuario = GetUsuario.UsuarioSO
        ''logger.Debug("BalUsu.ExibirNomeUsuario(" & GetUsuario.Login & ")")
        mm_Usuario = BalUsu.ExibirNomeUsuario(GetUsuario.Login)

        'p_Html = " <html> <body> Nome do Perito: " + Session("Nome").ToString + " </br>  Data: " + Today.ToShortDateString + " </br>  </br> </br>" + txtAnotacao.Text + " </br> " + " Serventia : " + mm_Serventia + " </br>" + " Usuário : " + mm_Usuario + " </body> </html>"

        p_Html = " <html> <body> Nome do Perito: " + Session("Nome").ToString + "</br> CPF : " + txtCPF.Text + " </br> Tipo de Anotação : " + cboTipo_anotacao.Text + " </br> " +
                 " <Table> <tr> <td> Data: " + Today.ToShortDateString + " </td> </tr> <tr> <td> " + txtAnotacao.Text + " </td> </tr> <tr> <td> " + " Serventia : " + mm_Serventia + " </td> </tr> <tr> <td> " + " Usuário : " + mm_Usuario + " </td> </tr> </table> </body> </html>"

        'DESTINO (DIPEJ)
        p_From_Nome = mm_Serventia
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
        If UCase(GetUsuario.Login) = "DANIELAFMO" Then
            m_Email = "danielafmo@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "MYLENAROCHA" Then
            m_Email = "Mylenarocha@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "MYLENAROCHA" Then
            m_Email = "Mylenarocha@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "CRISTIANESOUSA" Then
            m_Email = "cristianesousa@tjrj.jus.br"
        End If
        ''logger.Debug("Corpo do email: " & p_Html)
        ''logger.Debug("p_To: " & m_Email)
        p_To = m_Email
        p_Cc = ""
        'p_From_Nome = App.AppSettings("FromAddressNomeacao").ToString
        'ORIGEM EMAIL SERVENTIA
        p_Subject = "Anotações"
        p_Smtp_Hostname = App.AppSettings("SmtpHostname").ToString
        'p_Smtp_Hostname = "mail.tjrj.jus.br"
        p_Smtp_Portnum = App.AppSettings("SmtpPortnum").ToString
        'p_Smtp_Portnum = "25"
        ''logger.Debug("BalOrg.ExibirEmailServentia(" & GetUsuario.Login & ")")
        'p_From_Address = "santo@tjrj.jus.br"
        p_From_Address = BalOrg.ExibirEmailServentia(GetUsuario.CodOrg)
        If p_From_Address = "" Then
            MsgErro("Envio cancelado. Esta serventia não possui email cadastrado, favor contactar (21)3133-9100")
            Exit Sub
        End If
        If m_Email = "" Then
            p_To = App.AppSettings("ToAddressNomeacao").ToString
        End If
        ''logger.Debug("Bal.EnviarEmail(p_To, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)")
        Bal.EnviarEmail(p_To, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        ''logger.Debug("Email enviado.")
        MsgErro("E-mail foi enviado com sucesso.")
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.close();", True)

    End Sub

End Class
