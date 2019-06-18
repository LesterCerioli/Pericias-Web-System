Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports DGTECGEDARDOTNET

Partial Public Class frmPeritoDCP
    Inherits BasePage
    Dim m_Cod_Profissao As Integer
    Dim m_Cod_Especialidade As Integer
    Dim EscolherPerito As String

    Private Sub frmPeritoDCP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim BalPer As New BALPERITO(GetUsuario)

        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Temporizador", "window.setTimeout(location.reload();,200000))", True)
        If Not IsPostBack Then
            'Session("Escolha") = "N"
            txtNum_CNJ1.Attributes.Add("onblur", "return FormatarProcCNJ1('" & txtNum_CNJ1.ClientID & "');")
            'BtnPerito.Attributes.Add("onclick", "changeMe(Name.innerText)")
            ''BtnPerito.Attributes.Add("onclick", "changeMe('" & txtNome_Perito.ClientID & "');")
            'txtNum_CNJ2.Attributes.Add("onblur", "FormatarProcCNJ2(" & txtNum_CNJ2.ClientID & ".value);")
            'txtNum_CNJ.Attributes.Add("onblur", "ValidarProcCNJ(ctl00$Tela$txtNum_CNJ.value);")
            'txtNum_CNJ.Attributes.Add("onblur", "validarProcCNJ(" & txtNum_CNJ.ClientID & ".value);")
            '''''''''txtNum_Processo.Attributes.Add("onblur", "return FormatarProc(" & txtNum_Processo.ClientID & ".value);")
            'txtNum_Processo.Attributes.Add("onblur", "ValidarProc(" & txtNum_Processo.ClientID & ".value);")
            txtPrazo.Text = "30"
            PreencherPROFISSAO()
            Session("ID") = ""
            txtID_PF.Text = Request.QueryString("ID_PF")
            If txtID_PF.Text <> "" Then
                Session("ID") = txtID_PF.Text
                m_Cod_Profissao = CInt(Session("Cod_Profissao"))
                CboProfissao.SelectedValue = CboProfissao.Items.FindByValue(m_Cod_Profissao.ToString).Value()
                PreencherEspecialidade(m_Cod_Profissao)
                m_Cod_Especialidade = CInt(Session("Cod_Especialidade"))
                If m_Cod_Especialidade <> 0 Then
                    CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByValue(m_Cod_Especialidade.ToString).Value()
                Else
                    CboEspecialidade.Text = "GENÉRICO"
                End If
                txtNum_CNJ1.Text = Session("Num_Processo1").ToString
                txtNum_CNJ2.Text = Session("Num_Processo2").ToString
                ExibirDadosPerito()
                AtualizatxtNum_CNJ()
            End If
            '---------------------
            'EscolherPerito = Session("Escolha").ToString
            'If txtNome_Perito.Text = "" And EscolherPerito = "S" Then MsgErro("Não existe perito para esta especialidade para esta Comarca")
            'm_Cod_Profissao = CInt(Session("Cod_Profissao").ToString)
            'm_Cod_Especialidade = CInt(Session("Cod_Especialidade").ToString)
            'txtNum_CNJ1.Text = Session("Num_Processo1").ToString
            'txtNum_CNJ2.Text = Session("Num_Processo2").ToString
            'AtualizatxtNum_CNJ()
            '---------------------
        Else
            If Not Session("ID") Is Nothing Then
                If Session("ID").ToString <> "" Then
                    txtID_PF.Text = Session("ID").ToString
                End If
                If txtID_PF.Text <> "0" And txtID_PF.Text <> "" Then
                    ExibirDadosPerito()
                    AtualizatxtNum_CNJ()
                End If
            End If
            End If
            '---------------------
            If Session("Escolha") Is Nothing Then
                EscolherPerito = "N"
            Else
                EscolherPerito = Session("Escolha").ToString
            End If
            If txtNome_Perito.Text = "" And EscolherPerito = "S" Then
                If Session("ID") Is Nothing Or Session("ID").ToString = "" Then
                    MsgErro("Não existe perito para esta especialidade para esta Comarca")
                Else
                    txtID_PF.Text = Session("ID").ToString
                    txtNome_Perito.Text = BalPer.Nome_ID(txtID_PF.Text)
                End If
            End If

            If EscolherPerito = "S" Then
                m_Cod_Profissao = CInt(Session("Cod_Profissao").ToString)
                m_Cod_Especialidade = CInt(Session("Cod_Especialidade").ToString)
                txtNum_CNJ1.Text = Session("Num_Processo1").ToString
                txtNum_CNJ2.Text = Session("Num_Processo2").ToString
                AtualizatxtNum_CNJ()
            End If
            '---------------------
            Session("Escolha") = "N"


    End Sub

    'Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

    '    Dim bal As New BALEspecialidade(GetUsuario)
    '    Dim ent As New EntEspecialidade
    '    Dim dsfila As New DataSet
    '    dsfila = bal.ExibirDadosSet(m_Cod_Profissao)
    '    CboEspecialidade.Items.Clear()
    '    CboEspecialidade.DataTextField = "Descr_Especialidade"
    '    CboEspecialidade.DataValueField = "Cod_Especialidade"
    '    CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
    '    CboEspecialidade.DataBind()
    '    CboEspecialidade.Items.Insert(0, "Selecione a Especialidade")
    '    CboEspecialidade.SelectedIndex = 0

    'End Sub
    Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

        Dim bal As New BALEspecialidade(GetUsuario)
        Dim ent As New EntEspecialidade
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_Cod_Profissao)
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataTextField = "Descr_Especialidade"
        CboEspecialidade.DataValueField = "Cod_Especialidade"
        CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
        CboEspecialidade.DataBind()
        CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.SelectedIndex = 0

    End Sub
    Private Sub PreencherPROFISSAO()
        Dim bal As New BALProfissao(GetUsuario)
        Dim ent As New EntProfissao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboProfissao.Items.Clear()
        CboProfissao.DataTextField = "Descr_PROFISSAO"
        CboProfissao.DataValueField = "Cod_PROFISSAO"
        CboProfissao.DataSource = dsfila.Tables(0) '.DefaultView
        CboProfissao.DataBind()
        CboProfissao.Items.Insert(0, "Selecione a PROFISSAO")
        CboProfissao.SelectedIndex = 0

        CboEspecialidade.Items.Clear()

    End Sub
    'Private Sub PreencherPeritos(ByVal m_Cod_Profissao As Integer, ByVal m_Cod_Especialidade As Integer, ByVal Unico As Boolean)

    '    If Not Me.IsPostBack Then
    '        Exit Sub
    '    End If
    '    txtAnotacao.Text = ""
    '    LblAnotacao.Visible = False
    '    txtAnotacao.Visible = False
    '    lblData_Cadastramento.Text = ""
    '    lblData_Liberacao.Text = ""
    '    LblAnotacao.Text = ""
    '    lblEmail.Text = ""
    '    lblEmail1.Text = ""
    '    txtQteAceitos.Text = ""
    '    txtQtePendentes.Text = ""
    '    Dim bal As New BALPERITO(GetUsuario)
    '    Dim dsfila As New DataSet
    '    Dim m_Cod_Orgao As Integer
    '    Dim m_Cod_Comarca As Integer
    '    Dim BalComarca As New BALUSUARIO(GetUsuario)
    '    If m_Cod_Profissao = 0 Then
    '        MsgErro("Selecione uma Profissão")
    '        Exit Sub
    '    End If
    '    'CboPerito.Items.Clear()
    '    m_Cod_Orgao = CInt(Session("PERICIAS_CODORGAO"))
    '    m_Cod_Comarca = BalComarca.ExibirComarca(m_Cod_Orgao)
    '    'Cod_Tip_Sit -> "A" -> UC.PessoaFisicaFuncao.Cod_Tip_Sit = 1 (Ativo)
    '    'P.Situacao_CADASTRO <> P

    '    If Not Unico Then
    '        dsfila = bal.ExibirDadosPerDCP(m_Cod_Profissao, m_Cod_Especialidade, m_Cod_Comarca)
    '        If dsfila.Tables(0).Rows.Count = 0 Then 'dsfila Is Nothing Then
    '            MsgErro("Não existe perito cadastro para esta Comarca para esta especialidade")
    '            Exit Sub
    '        End If
    '    Else
    '        dsfila = bal.ExibirDadosPerDCP(m_Cod_Profissao, m_Cod_Especialidade, m_Cod_Comarca)
    '        If dsfila.Tables(0).Rows.Count = 0 Then 'dsfila Is Nothing Then
    '            MsgErro("Não existe perito cadastro para esta Comarca para esta especialidade")
    '            Exit Sub
    '        End If
    '    End If

    '    'Criar Session com os peritos desta comarca...
    '    'CboPerito.DataTextField = "Nome"
    '    'CboPerito.DataValueField = "ID_PF"
    '    'CboPerito.DataSource = dsfila.Tables(0) '.DefaultView
    '    'CboPerito.DataBind()
    '    'CboPerito.Items.Insert(0, "Selecione um perito")
    '    'CboPerito.SelectedIndex = 0

    'End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Dim Bal As New BalProcesso_Perito(GetUsuario)
        Dim BalEnviarEmail As New BalEmail(GetUsuario)
        Dim p_To As String
        Dim p_Cc As String
        Dim p_From_Nome As String
        Dim p_From_Address As String
        Dim p_Subject As String
        Dim p_Html As String
        Dim p_Smtp_Hostname As String
        Dim p_Smtp_Portnum As String
        Dim EmailHTML As String
        Dim m_Email As String
        Dim m_Email1 As String
        Dim m_PRAZO_ENTREGA As String

        'Verificar se consta como nomeado na tabela Processo_Perito
        ' se sim não grava nada

        'If chkNomeado.Checked And Not m_ChecadoAntes Then
        'If CboPerito.Text <> "Selecione um perito" And CboProfissao.Text <> "Selecione uma Profissão" Then
        If txtNome_Perito.Text <> "" And CboProfissao.Text <> "Selecione uma Profissão" Then
            'EmailHTML = "<html> " & _
            '"<body> " & _
            '"<style type= 'Text/css'> " & _
            '".style4 {font-size: 18px;}.style6 {font-size: 18px;font-weight: bold;}.style7 {font-size: 18px; font-style: italic;} " & _
            '".style5 {font-size: 16px;} " & _
            '"</style> " & _
            '"<p align=center class=style6><strong>PODER JUDICI&Aacute;RIO DO ESTADO DO RIO DE JANEIRO </strong> <br> " & _
            '"<strong>DIRETORIA GERAL DE PER&IacuteCIAS</strong> </P> " & _
            '"<p align=center class=style5>DEINP - Departamento de Pericias/DIPEJ - Divis&atilde;o de Per&iacute;cias Judiciais <br> " & _
            '"Av. Erasmo Braga, 115 - Centro - Sala 604/CEP: 20020-903 - Centro  " & _
            '"Rio de Janeiro/RJ - Tel.: 3133-2284, 3133-3923 e 3133-3536 <br><br></P> " & _
            '"<p align=left class=style6>OF&Iacute;CIO DGJUR/DEINP/DIPEJ N&deg;  </strong></p> "

            'EmailHTML = EmailHTML + " .</p>  <span class=style4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Exmo.  Senhor Juiz,</span> " & _
            '"  <p align='justify' class='style4'>Informamos a nomeação de VSa. como perito para o Processo N&deg : "

            'EmailHTML = EmailHTML + txtNum_Processo.Text + " e Número CNJ " + txtNum_CNJ.Text & _
            '"  <br> Favor entrar em contato com o DEINP para aceitação ou recusa da participação no processo em ep&iacutegrafe. " & _
            '"  <p align='center' class='style4'> Atenciosamente, </p> " & _
            '"  <p align='center'> <span class='style4'><strong>Zulima Alves Moragas  <br> Matr&iacutecula 16341 <br> " & _
            '"  Diretor do Departamento de Per&aacutecias Processual <br> DGJUR/DIPEJ </strong></span></p>  " & _
            '"  <p><br> " & _
            '"  <br> " & _
            '"  Exm&ordm;. Sr(a).<br> " & _
            '"  <em><strong>Dr(a). Juiz(a) de Direito do(a) </strong></em></p> " & _
            '"  <p align=center style='text-align:center'>&nbsp;</p> " & _
            '" </body> " & _
            '"</html>"
            '==========================================================================
            Dim m_Num_Oficio As Integer

            'Gravar Processo_Perito
            Dim dsOficio As DataSet
            'dsOficio = Bal.NumerarOficio(txtNum_CNJ.Text, Convert.ToInt64(CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)))
            dsOficio = Bal.NumerarOficio(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text))

            m_Num_Oficio = CInt(dsOficio.Tables(0).Rows(0).Item(0))

            '"<img alt='Brasao' src='Imagens/Brasao_oficial.GIF' style='width: 69px; height: 78px' />" & _
            '"<img alt='Brasao' src='Imagens/Brasao_oficial.GIF' style='width: 69px; height: 78px' />" & _
            Dim txtBrasao As String
            txtBrasao = ToBase64(ConvertImageFiletoBytes("Imagens/Brasao_oficial.GIF")) 'ToBase64(ByVal data() As Byte) As String
            EmailHTML = "<html> " & _
            "<body> " & _
            "<style type= 'Text/css'> " & _
            ".style4 {font-size: 18px;}.style6 {font-size: 18px;font-weight: bold;}.style7 {font-size: 18px; font-style: italic;} " & _
            ".style5 {font-size: 16px;} " & _
            "</style> " & _
            "<img alt='Brasao' src='data:image/jpeg;base64," & txtBrasao & "'  style='width: 69px; height: 78px' border='0' id='idImagem' /> " & _
            "<p align=center class=style6><strong>TRIBUNAL DE JUSTI&Ccedil;A DO ESTADO DO RIO DE JANEIRO </strong> <br> " & _
            "Diretoria Geral de Apoio aos &Oacute;rg&atilde;os Juridicionais <br> " & _
            "Departamento de Instru&ccedil;&atilde;o PROCESSUAL <br> " & _
            "Divis&atilde;o Per&iacute;cias Judiciais <br> </p>" & _
            "<br><br>" & _
            "<strong> Of&iacute;cio DIPEJ-GAB n&deg " & m_Num_Oficio.ToString & "/" + Year(Now).ToString & _
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Rio de Janeiro, " + Now.ToString

            EmailHTML = EmailHTML + " <br> Processo Judicial n&deg " + txtNum_CNJ.Text + "(" + txtNum_Processo.Text + ")" & _
            "<br><br><br>" & _
            "<p align=left class=style6><strong> Prezado Senhor,</strong></p><br><br> "

            EmailHTML = EmailHTML + "<span class=style4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & _
            "Sirvo-me do presente para intim&aacute;-lo de sua nomea&ccedil&atilde;o pelo Exmo. Sr. Juiz de Direito da </span>"
            '''''''==================
            ' XXX 13ª Vara  XXX da Infância da Comarca de XXXX 
            '''''''''''''''''''''''''''''
            Dim DsProcPer As DataSet
            Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
            Dim rsProcPer As DataRow
            Dim m_Descr_Profissao As String
            Dim m_Descr_Especialidade As String
            Dim m_Descr_Serventia As String
            Dim m_Descr_Comarca As String
            Dim DsServentia As DataSet
            Dim BalComarca As New BALCOMARCA(GetUsuario)
            Dim rsServentia As DataRow
            Dim m_Data_Liberacao As String

            m_PRAZO_ENTREGA = txtPrazo.Text
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
            If CboEspecialidade.SelectedValue = "GENÉRICO" Then
                m_Cod_Especialidade = 0
            Else
                m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            End If
            'Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value), Today.ToShortDateString, "", "", lblData_Liberacao.Text, CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, "", "", m_Num_Oficio, Year(Now), IIf(ChkJustGrat.Checked, "S", "N").ToString)
            'If Trim(lblData_Liberacao.Text) = "__ /__ /__" Then
            'm_Data_Liberacao = ""
            'Else
            m_Data_Liberacao = Trim(lblData_Liberacao.Text)
            'End If
            Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(txtID_PF.Text), Today.ToShortDateString, "", "", m_Data_Liberacao, CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, "", "", m_Num_Oficio, Year(Now), IIf(ChkJustGrat.Checked, "S", "N").ToString)

            DsServentia = BalComarca.ExibirDadosServentia(GetUsuario.CodOrg)
            m_Descr_Serventia = "Serventia..."
            m_Descr_Comarca = "Comarca..."
            If DsServentia.Tables(0).Rows.Count > 0 Then
                rsServentia = DsServentia.Tables(0).Rows(0)
                If Not rsServentia("Descr_Serventia").ToString = Nothing Then
                    m_Descr_Serventia = rsServentia("Descr_Serventia").ToString
                    m_Descr_Comarca = rsServentia("Descr_Comarca").ToString
                End If
            End If
            DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, CInt(txtID_PF.Text))
            m_Descr_Profissao = "Profissão..."
            m_Descr_Especialidade = "Especialidade..."
            If DsProcPer.Tables(0).Rows.Count > 0 Then
                rsProcPer = DsProcPer.Tables(0).Rows(0)
                If Not rsProcPer("Descr_Profissao").ToString = Nothing Then
                    m_Descr_Profissao = rsProcPer("Descr_Profissao").ToString
                End If
                If Not rsProcPer("Especialidade").ToString = Nothing Then
                    m_Descr_Especialidade = rsProcPer("Especialidade").ToString
                End If
            End If
            EmailHTML = EmailHTML + m_Descr_Serventia & " da " & m_Descr_Comarca & ", para atuar como perito daquele respeit&aacute;vel Ju&iacute;zo " & _
            " nos autos do Processo em ep&iacute;grafe, devendo V.Sa. se manifestar quanto aacute; aceita&ccedil;&atilde;o " & _
            " ou recusa do encargo bem como apresentar proposta de honor&aacute;rios, concomitantemente, " & _
            " diretamente àquele ju&iacute;zo;.(ou neste sistema)." & _
            " <br><br> <p align='center' class='style4'> Cordialmente. </p> " & _
            "  <p align='center'> <span class='style4'><strong>MARCIO MARCELO DA SILVA OLIVEIRA <br> Matr&iacute;cula 01/25875 <br> " & _
            "  Diretor da Divis&atilde;o de  de Per&iacute;cias Judiciais  </strong></span></p>  " & _
            "  <p><br> " & _
            "  <br> " & _
            "  Ilmo. Sr. " & txtNome_Perito.Text & "." & _
            "  <br>(" & m_Descr_Profissao & "/" & m_Descr_Especialidade & ")" & _
            " </body> " & _
            "</html>"
            '==========================================================================
            'If UCase(gblLogin) = "CRISTIANESOUSA" Then
            'm_Email = "cristianesousa@tjrj.jus.br"
            'ElseIf UCase(gblLogin) = "SANTO" Then
            'm_Email = "santo@tjrj.jus.br"
            'ElseIf UCase(gblLogin) = "FRANKRIBEIRO" Then
            m_Email = lblEmail.Text
            m_Email1 = lblEmail1.Text
            'teste
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
            If m_Email = "" Then
                MsgErro("Email inválido")
            End If
            'If UCase(GetUsuario.Login) = "santo" Then
            '    m_Email1 = "santo@tjrj.jus.br"
            'End If
            'If UCase(GetUsuario.Login) = "santo" Then
            '    m_Email1 = "santo@tjrj.jus.br"
            'End If
            'GetUsuario.UsuarioSO
            'GetUsuario.Login
            'kellycoc,valeriasuzart,chenrique
            '"frankribeiro@tjrj.jus.br"
            'ElseIf UCase(gblLogin) = "WAGNER" Then
            'm_Email = "wagner@tj.rj.gov.br"
            'End If
            'If Trim(m_Email) = "" Then
            ' MsgBox("O email do Perito " + m_Perito + " não foi encontrado!!!")
            'End If
            p_To = m_Email
            p_Cc = ""
            p_From_Nome = "DGJUR/DEINP/DIPEJ"
            'Trocar para email da DIPEJ
            p_From_Address = "DGJUR/DEINP/DIPEJ"
            'p_From_Address = "santo@tjrj.jus.br"
            'If UCase(GetUsuario.Login) = "SANTO" Then
            p_From_Address = "santo@tjrj.jus.br"
            'End If
            'If UCase(GetUsuario.Login) = "KELLYCOC" Then
            '    p_From_Address = "kellycoc@tjrj.jus.br"
            'End If
            'If UCase(GetUsuario.Login) = "VALERIASUZART" Then
            '    p_From_Address = "valeriasuzart@tjrj.jus.br"
            'End If
            'If UCase(GetUsuario.Login) = "CHENRIQUE" Then
            '    p_From_Address = "chenrique@tjrj.jus.br"
            'End If
            p_Subject = "Indicação do Perito no Processo"
            p_Html = EmailHTML
            p_Smtp_Hostname = "mail.tjrj.jus.br"
            p_Smtp_Portnum = "25"
            BalEnviarEmail.EnviarEmail(p_To, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
            'BalEnviarEmail.EnviarEmail(m_Email1, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
            ''m_PRAZO_ENTREGA = txtPrazo.Text
            ''m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
            ''If CboEspecialidade.SelectedValue = "GENÉRICO" Then
            ''    m_Cod_Especialidade = 0
            ''Else
            ''    m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            ''End If
            ' ''Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value), Today.ToShortDateString, "", "", lblData_Liberacao.Text, CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, "", "", m_Num_Oficio, Year(Now), IIf(ChkJustGrat.Checked, "S", "N").ToString)
            ''Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(txtID_PF.Text), Today.ToShortDateString, "", "", lblData_Liberacao.Text, CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, "", "", m_Num_Oficio, Year(Now), IIf(ChkJustGrat.Checked, "S", "N").ToString)
            PreencherProcPerito(txtNum_CNJ.Text)
            '---------------------
            'ExibirDadosPerito()
            'AtualizatxtNum_CNJ()
            '---------------------
            txtAnotacao.Visible = False
            LblAnotacao.Visible = False
        Else
            MsgErro("Gravação Rejeitada. Selecione um perito")
        End If
    End Sub

    Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged

        m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
        m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        If m_Cod_Profissao = 0 Then
            MsgErro("Selecione uma Profissão")
            Exit Sub
        End If
        '''''''PreencherPeritos(m_Cod_Profissao, m_Cod_Especialidade)

    End Sub

    'Protected Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboPerito.SelectedIndexChanged

    'ExibirDadosPerito()

    'End Sub
    Private Sub ExibirDadosPerito()

        Dim Bal As New BALAnotacao(GetUsuario)
        Dim Ent As New EntAnotacao
        Dim BalPer As New BALPERITO(GetUsuario)
        Dim EntPer As New EntPERITO
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim EntProcPer As New EntProcesso_Perito
        Dim Ds As DataSet
        Dim DsPer As DataSet
        Dim DsProcPer As DataSet
        Dim rsPer As DataRow
        Dim rsProcPer As DataRow
        Dim m_ID_PF As Long
        Dim i As Integer
        Dim m_Justica_Gratuita As String

        'm_ID_PF = CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)
        m_ID_PF = CInt(txtID_PF.Text)

        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text, m_ID_PF)
        'If Not DsProcPer Is Nothing Then
        If DsProcPer.Tables(0).Rows.Count > 0 Then
            rsProcPer = DsProcPer.Tables(0).Rows(0)

            If rsProcPer("Data_Aceitacao") Is Nothing Then
                lblData_Aceitacao.Text = ""
            Else
                lblData_Aceitacao.Text = rsProcPer("Data_Aceitacao").ToString
            End If
            If rsProcPer("Data_Negacao") Is Nothing Then
                lblData_Negacao.Text = ""
            Else
                lblData_Negacao.Text = rsProcPer("Data_Negacao").ToString
            End If
            If Not rsProcPer("Data_Negacao").ToString = Nothing Then
                MsgErro("Perito foi Nomeado para este processo, anteriormente. Recusou o convite em " + lblData_Negacao.Text)
                Exit Sub
            End If
            'lblData_Aceitacao.Text = rsProcPer("Data_Aceitacao").ToString
            'lblData_Negacao.Text = rsProcPer("Data_Negacao").ToString
            lblData_Nomeacao.Text = rsProcPer("Data_Nomeacao").ToString
            If lblData_Nomeacao.Text = "" Then
                lblData_Nomeacao.Text = Today.ToShortDateString
            End If
            lblData_Liberacao.Text = rsProcPer("Data_Liberacao").ToString
            If lblData_Liberacao.Text <> "" Then
                chkLaudoLiberado.Checked = True
            Else
                chkLaudoLiberado.Checked = False
            End If
            m_Justica_Gratuita = DsProcPer.Tables(0).Rows(0).Item(10).ToString
            If m_Justica_Gratuita = "S" Then
                ChkJustGrat.Checked = True
            Else
                ChkJustGrat.Checked = False
            End If
        End If
        'End If
        DsProcPer = Nothing
        Ds = Bal.ExibirAnotPer(m_ID_PF)
        If Ds.Tables(0).Rows.Count > 0 Then
            txtAnotacao.Visible = True
            LblAnotacao.Visible = True
            txtAnotacao.Text = ""
            If Ds.Tables(0).Rows.Count > 0 Then
                i = 0
                For Each rs As DataRow In Ds.Tables(0).Rows
                    txtAnotacao.Text = txtAnotacao.Text + " - " + Ds.Tables(0).Rows(i).Item(3).ToString + Chr(13)  'rs("Descr_Anotacao").ToString
                    i = i + 1
                Next
            End If
        Else
            LblAnotacao.Visible = False
            txtAnotacao.Visible = False
        End If
        Ds = Nothing
        DsPer = BalPer.ExibirDadosSet(m_ID_PF)
        'If Not DsPer Is Nothing Then
        If DsPer.Tables(0).Rows.Count > 0 Then
            rsPer = DsPer.Tables(0).Rows(0)
            lblEmail.Text = rsPer("Email").ToString
            lblData_Cadastramento.Text = rsPer("Data_Cadastramento").ToString
            txtQtePendentes.Text = rsPer("QtePendentes").ToString
            txtQteAceitos.Text = rsPer("QteAceitos").ToString
            If DsPer.Tables(0).Rows.Count > 1 Then
                rsPer = DsPer.Tables(0).Rows(1)
                lblEmail1.Text = rsPer("Email").ToString
            End If
            txtNome_Perito.Text = rsPer("Nome").ToString
        End If
        'End If
        DsPer = Nothing

    End Sub

    Protected Sub txtNum_Processo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_Processo.TextChanged
        Dim Ent As EntPROC_CNJ
        'Dim EntProcPer As EntProcesso_Perito
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim Cod_CNJ_Valido As Boolean

        'txtNum_CNJ.ClientID.validar(false-retornar)
        If Not ValidaNumProc(txtNum_Processo.Text) Then
            MsgErro("Número de Processo Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_CNJ.Text = ""
            Exit Sub
        End If
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ1.Text = "" Then
                txtNum_CNJ1.Text = Mid(Ent.Cod_CNJ, 1, 7) + Mid(Ent.Cod_CNJ, 9, 2) + Mid(Ent.Cod_CNJ, 12, 4)
                txtNum_CNJ2.Text = Mid(Ent.Cod_CNJ, 22, 4)
                txtNum_CNJ.Text = Ent.Cod_CNJ
            End If
            'If txtNum_CNJ.Text = "" Then 
            Cod_CNJ_Valido = ValidaNumCNJ(Ent.Cod_CNJ)
            If Cod_CNJ_Valido Then
                PreencherProcPerito(Ent.Cod_CNJ) '(GrdProcPerito)
                'Ao selecionar ... EntProcPer = BalProcPer.ExibirDadosEnt(m_Cod_CNJ,m_ID_PF)
            Else
                MsgErro("Número de CNJ Inválido")
                txtNum_CNJ1.Text = ""
                txtNum_CNJ2.Text = ""
                txtNum_Processo.Text = ""
            End If
        Else
            MsgErro("Número de Processo/CNJ não localizado!")
        End If
        'Ent.NUM_CNJ = Rss("NUM_CNJ")
        'Ent.ID_PF = Rss("ID_PF ")
        'Ent.DATA_NOMEACAO = Rss("DATA_NOMEACAO")
        'Ent.DATA_NEGACAO = Rss("DATA_NEGACAO")
        'Ent.DATA_ACEITACAO = Rss("DATA_ACEITACAO")
        'Ent.Data_Liberacao = Rss("DATA_LIBERACAO")
        'Ent.SIGLA_NOMEACAO = Rss("SIGLA_NOMEACAO")


    End Sub

    Protected Sub BtnAnotacao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAnotacao.Click
        'Anotações
        'If CboPerito.Text = "" Then
        If txtID_PF.Text = "" Then
            MsgErro("Selecione o Perito")
            Exit Sub
        End If
        'Session("Nome") = CboPerito.Text
        Session("Nome") = txtNome_Perito.Text
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmAnotacaoDCP.aspx', '_blank', 'height=600,width=420,Top=50,left=120,scrollbars=0,toolbar=0,resizable=0,location=0,status=0')", True)
        'The following features are available in most browsers:
        'toolbar=0|1 	Specifies whether to display the toolbar in the new window.
        'location=0|1 	Specifies whether to display the address line in the new window.
        'directories=0|1 	Specifies whether to display the Netscape directory buttons.
        'status=0|1 	Specifies whether to display the browser status bar.
        'menubar=0|1 	Specifies whether to display the browser menu bar.
        'scrollbars=0|1 	Specifies whether the new window should have scrollbars.
        'resizable=0|1 	Specifies whether the new window is resizable.
        'width=pixels 	Specifies the width of the new window.
        'height=pixels 	Specifies the height of the new window.
        'top=pixels 	Specifies the Y coordinate of the top left corner of the new window. (Not supported in version 3 browsers.)
        'left=pixels 	Specifies the X coordinate of the top left corner of the new window. (Not supported in version 3 browsers.) 
    End Sub

    Protected Sub BtnNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovo.Click
        Limpar()
    End Sub
    Private Sub Limpar()
        txtAnotacao.Visible = False
        LblAnotacao.Visible = False
        lblEmail.Text = ""
        lblEmail1.Text = ""
        txtAnotacao.Text = ""
        lblData_Nomeacao.Text = ""
        lblData_Liberacao.Text = ""
        txtNum_CNJ1.Text = ""
        txtNum_CNJ2.Text = ""
        txtNum_CNJ.Text = ""
        txtNum_Processo.Text = ""
        txtQteAceitos.Text = ""
        txtQtePendentes.Text = ""
        txtPrazo.Text = "30"
        txtID_PF.Text = ""
        txtNome_Perito.Text = ""
        lblData_Cadastramento.Text = ""
        lblData_Aceitacao.Text = ""
        lblData_Negacao.Text = ""
        'CboPerito.Items.Clear()
        'CboPerito.Text = Nothing
        'CboPerito.DataSource = Nothing
        'CboPerito.DataBind()
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataSource = Nothing
        CboEspecialidade.DataBind()
        'CboPerito.Text = "Selecione um perito"
        'PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
        CboProfissao.DataSource = Nothing
        CboProfissao.DataBind()
        PreencherPROFISSAO()
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataTextField = "Descr_Especialidade"
        CboEspecialidade.DataValueField = "Cod_Especialidade"
        CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.SelectedIndex = 0
        CboProfissao.SelectedIndex = 0
        'CboPerito.Items.Clear()
        txtNome_Perito.Text = ""
        txtID_PF.Text = ""
        GrdProcPerito.DataSource = Nothing
        GrdProcPerito.DataBind()
        Session("ID") = ""
        Session("Cod_Profissao") = 0
        Session("Cod_Especialidade") = 0
        Session("Num_Processo1") = ""
        Session("Num_Processo2") = ""

    End Sub

    Protected Sub PreencherProcPerito(ByVal Num_CNJ As String)
        Dim Ds As DataSet
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Ds = BalProcPer.ExibirDadosTodos(Num_CNJ)
        GrdProcPerito.DataSource = Ds.Tables(0)
        GrdProcPerito.DataBind()
    End Sub


    Protected Sub BtnEmailNomeacao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEmailNomeacao.Click
        '        Dim Bal As New BalProcesso_Perito(GetUsuario)
        '        Dim BalEnviarEmail As New BalEmail(GetUsuario)
        '        Dim p_To As String
        '        Dim p_Cc As String
        '        Dim p_From_Nome As String
        '        Dim p_From_Address As String
        '        Dim p_Subject As String
        '        Dim p_Html As String
        '        Dim p_Smtp_Hostname As String
        '        Dim p_Smtp_Portnum As String
        '        Dim EmailHTML As String
        '        Dim m_Email As String
        '        Dim m_Email1 As String
        '        '---------------------
        '        Dim m_Num_Oficio As Integer

        '        'Gravar Processo_Perito
        '        Dim dsOficio As DataSet
        '        'dsOficio = Bal.NumerarOficio(txtNum_CNJ.Text, Convert.ToInt64(CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)))
        '        dsOficio = BAL.NumerarOficio(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text))

        '        'Testar
        '        m_Num_Oficio = CInt(dsOficio.Tables(0).Rows(0).Item(0))
        '        '--------------------

        '        EmailHTML = "<html> " & _
        '"<body> " & _
        '"<style type= 'Text/css'> " & _
        '".style4 {font-size: 18px;}.style6 {font-size: 18px;font-weight: bold;}.style7 {font-size: 18px; font-style: italic;} " & _
        '".style5 {font-size: 16px;} " & _
        '"</style> " & _
        '"<p align=center class=style6><strong>PODER JUDICI&Aacute;RIO DO ESTADO DO RIO DE JANEIRO </strong> <br> " & _
        '"<strong>DIRETORIA GERAL DE PER&IacuteCIAS</strong> </P> " & _
        '"<p align=center class=style5>DEINP - Departamento de Pericias/DIPEJ - Divis&atilde;o de Per&iacute;cias Judiciais <br> " & _
        '"Av. Erasmo Braga, 115 - Centro - Sala 604/CEP: 20020-903 - Centro  " & _
        '"Rio de Janeiro/RJ - Tel.: 3133-2284, 3133-3923 e 3133-3536 <br><br></P> " & _
        '"<p align=left class=style6>OF&Iacute;CIO DGJUR/DEINP/DIPEJ N&deg;  </strong></p> " & m_Num_Oficio.ToString & "/" + Year(Now).ToString

        '        EmailHTML = EmailHTML + " .</p>  <span class=style4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Exmo.  Senhor Juiz,</span> " & _
        '        "  <p align='justify' class='style4'>Informamos a nomeação de VSa. como perito para o Processo N&deg : "

        '        EmailHTML = EmailHTML + txtNum_Processo.Text + " e Número CNJ " + txtNum_CNJ.Text & _
        '        "  <br> Favor entrar em contato com o DEINP para aceitação ou recusa da participação no processo em ep&iacutegrafe. " & _
        '        "  <p align='center' class='style4'> Atenciosamente, </p> " & _
        '        "  <p align='center'> <span class='style4'><strong>Zulima Alves Moragas  <br> Matr&iacutecula 16341 <br> " & _
        '        "  Diretor do Departamento de Per&aacutecias Processual <br> DGJUR/DIPEJ </strong></span></p>  " & _
        '        "  <p><br> " & _
        '        "  <br> " & _
        '        "  Exm&ordm;. Sr(a).<br> " & _
        '        "  <em><strong>Dr(a). Juiz(a) de Direito do(a) </strong></em></p> " & _
        '        "  <p align=center style='text-align:center'>&nbsp;</p> " & _
        '        " </body> " & _
        '        "</html>"

        '        m_Email = lblEmail.Text
        '        m_Email1 = lblEmail1.Text
        '        m_Email1 = "santo@tjrj.jus.br"
        '        '"frankribeiro@tjrj.jus.br"
        '        p_To = m_Email
        '        p_Cc = ""
        '        p_From_Nome = "DGJUR/DEINP/DIPEJ"
        '        p_From_Address = "santo@tjrj.jus.br"
        '        p_Subject = "Indicação do Perito no Processo"
        '        p_Html = EmailHTML
        '        p_Smtp_Hostname = "mail.tjrj.jus.br"
        '        p_Smtp_Portnum = "25"
        '        BalEnviarEmail.EnviarEmail(m_Email, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        '        BalEnviarEmail.EnviarEmail(m_Email1, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        Dim Bal As New BalProcesso_Perito(GetUsuario)
        Dim BalEnviarEmail As New BalEmail(GetUsuario)
        Dim p_To As String
        Dim p_Cc As String
        Dim p_From_Nome As String
        Dim p_From_Address As String
        Dim p_Subject As String
        Dim p_Html As String
        Dim p_Smtp_Hostname As String
        Dim p_Smtp_Portnum As String
        Dim EmailHTML As String
        Dim m_Email As String
        Dim m_Email1 As String
        Dim m_PRAZO_ENTREGA As String

        'Verificar se consta como nomeado na tabela Processo_Perito
        ' se sim não grava nada

        'If chkNomeado.Checked And Not m_ChecadoAntes Then
        'If CboPerito.Text <> "Selecione um perito" And CboProfissao.Text <> "Selecione uma Profissão" Then
        'If txtNome_Perito.Text <> "" And CboProfissao.Text <> "Selecione uma Profissão" Then
        'EmailHTML = "<html> " & _
        '"<body> " & _
        '"<style type= 'Text/css'> " & _
        '".style4 {font-size: 18px;}.style6 {font-size: 18px;font-weight: bold;}.style7 {font-size: 18px; font-style: italic;} " & _
        '".style5 {font-size: 16px;} " & _
        '"</style> " & _
        '"<p align=center class=style6><strong>PODER JUDICI&Aacute;RIO DO ESTADO DO RIO DE JANEIRO </strong> <br> " & _
        '"<strong>DIRETORIA GERAL DE PER&IacuteCIAS</strong> </P> " & _
        '"<p align=center class=style5>DEINP - Departamento de Pericias/DIPEJ - Divis&atilde;o de Per&iacute;cias Judiciais <br> " & _
        '"Av. Erasmo Braga, 115 - Centro - Sala 604/CEP: 20020-903 - Centro  " & _
        '"Rio de Janeiro/RJ - Tel.: 3133-2284, 3133-3923 e 3133-3536 <br><br></P> " & _
        '"<p align=left class=style6>OF&Iacute;CIO DGJUR/DEINP/DIPEJ N&deg;  </strong></p> "

        'EmailHTML = EmailHTML + " .</p>  <span class=style4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Exmo.  Senhor Juiz,</span> " & _
        '"  <p align='justify' class='style4'>Informamos a nomeação de VSa. como perito para o Processo N&deg : "

        'EmailHTML = EmailHTML + txtNum_Processo.Text + " e Número CNJ " + txtNum_CNJ.Text & _
        '"  <br> Favor entrar em contato com o DEINP para aceitação ou recusa da participação no processo em ep&iacutegrafe. " & _
        '"  <p align='center' class='style4'> Atenciosamente, </p> " & _
        '"  <p align='center'> <span class='style4'><strong>Zulima Alves Moragas  <br> Matr&iacutecula 16341 <br> " & _
        '"  Diretor do Departamento de Per&aacutecias Processual <br> DGJUR/DIPEJ </strong></span></p>  " & _
        '"  <p><br> " & _
        '"  <br> " & _
        '"  Exm&ordm;. Sr(a).<br> " & _
        '"  <em><strong>Dr(a). Juiz(a) de Direito do(a) </strong></em></p> " & _
        '"  <p align=center style='text-align:center'>&nbsp;</p> " & _
        '" </body> " & _
        '"</html>"
        '==========================================================================
        Dim m_Num_Oficio As String

        'Dim dsOficio As DataSet

        'dsOficio = Bal.NumerarOficio(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text))

        'm_Num_Oficio = CInt(dsOficio.Tables(0).Rows(0).Item(0))

        If txtID_PF.Text = "" Then
            MsgErro("Falha no envio do email. Perito não localizado")
            Exit Sub
        End If
        m_Num_Oficio = Bal.ExibirNumOficio(txtNum_CNJ.Text, txtID_PF.Text)
        '"<img alt='Brasao' src='Imagens/Brasao_oficial.GIF' style='width: 69px; height: 78px' /> " & _
        '"<img alt= 'Brasao' src= " & Chr(34) & "Imagens/Brasao_oficial.GIF" & Chr(34) & " style='width: 69px; height: 78px' /> " & _
        'EmailHTML = "<html> " & _
        '"<body> " & _
        '"<style type= 'Text/css'> " & _
        '".style4 {font-size: 18px;}.style6 {font-size: 18px;font-weight: bold;}.style7 {font-size: 18px; font-style: italic;} " & _
        '".style5 {font-size: 16px;} " & _
        '"</style> " & _
        '"<p align=center class=style6><strong>TRIBUNAL DE JUSTI&Ccedil;A DO ESTADO DO RIO DE JANEIRO </strong> <br> " & _
        Dim txtBrasao As String
        txtBrasao = ToBase64(ConvertImageFiletoBytes("Imagens/Brasao_oficial.GIF")) 'ToBase64(ByVal data() As Byte) As String
        EmailHTML = "<html> " & _
        "<body> " & _
        "<style type= 'Text/css'> " & _
        ".style4 {font-size: 18px;}.style6 {font-size: 18px;font-weight: bold;}.style7 {font-size: 18px; font-style: italic;} " & _
        ".style5 {font-size: 16px;} " & _
        "</style> " & _
        "<img alt='Brasao' src='data:image/jpeg;base64," & txtBrasao & "'  style='width: 69px; height: 78px' border='0' id='idImagem' /> " & _
        "<p align=center class=style6><strong>TRIBUNAL DE JUSTI&Ccedil;A DO ESTADO DO RIO DE JANEIRO </strong> <br> " & _
        "Diretoria Geral de Apoio aos &Oacute;rg&atilde;os Juridicionais <br> " & _
        "Departamento de Instru&ccedil;&atilde;o PROCESSUAL <br> " & _
        "Divis&atilde;o Per&iacute;cias Judiciais <br> </p>" & _
        "<br><br>" & _
        "<strong> Of&iacute;cio DIPEJ-GAB n&deg " & m_Num_Oficio & _
        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Rio de Janeiro, " + Now.ToString

        EmailHTML = EmailHTML + " <br> Processo Judicial n&deg " + txtNum_CNJ.Text + "(" + txtNum_Processo.Text + ")" & _
        "<br><br><br>" & _
        "<p align=left class=style6><strong> Prezado Senhor,</strong></p><br><br> "

        EmailHTML = EmailHTML + "<span class=style4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & _
        "Sirvo-me do presente para intim&aacute;-lo de sua nomea&ccedil&atilde;o pelo Exmo. Sr. Juiz de Direito da </span>"
        '''''''==================
        ' XXX 13ª Vara  XXX da Infância da Comarca de XXXX 
        '''''''''''''''''''''''''''''
        Dim DsProcPer As DataSet
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim rsProcPer As DataRow
        Dim m_Descr_Profissao As String
        Dim m_Descr_Especialidade As String
        Dim m_Descr_Serventia As String
        Dim m_Descr_Comarca As String
        Dim DsServentia As DataSet
        Dim BalComarca As New BALCOMARCA(GetUsuario)
        Dim rsServentia As DataRow
        Dim m_Data_Liberacao As String

        m_PRAZO_ENTREGA = txtPrazo.Text
        m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        If CboEspecialidade.SelectedValue = "GENÉRICO" Then
            m_Cod_Especialidade = 0
        Else
            m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
        End If
        m_Data_Liberacao = Trim(lblData_Liberacao.Text)
        DsServentia = BalComarca.ExibirDadosServentia(GetUsuario.CodOrg)
        m_Descr_Serventia = "Serventia..."
        m_Descr_Comarca = "Comarca..."
        If DsServentia.Tables(0).Rows.Count > 0 Then
            rsServentia = DsServentia.Tables(0).Rows(0)
            If Not rsServentia("Descr_Serventia").ToString = Nothing Then
                m_Descr_Serventia = rsServentia("Descr_Serventia").ToString
                m_Descr_Comarca = rsServentia("Descr_Comarca").ToString
            End If
        End If
        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, CInt(txtID_PF.Text))
        m_Descr_Profissao = "Profissão..."
        m_Descr_Especialidade = "Especialidade..."
        If DsProcPer.Tables(0).Rows.Count > 0 Then
            rsProcPer = DsProcPer.Tables(0).Rows(0)
            If Not rsProcPer("Descr_Profissao").ToString = Nothing Then
                m_Descr_Profissao = rsProcPer("Descr_Profissao").ToString
            End If
            If Not rsProcPer("Especialidade").ToString = Nothing Then
                m_Descr_Especialidade = rsProcPer("Especialidade").ToString
            End If
        End If
        EmailHTML = EmailHTML + m_Descr_Serventia & " da " & m_Descr_Comarca & ", para atuar como perito daquele respeit&aacute;vel Ju&iacute;zo " & _
        " nos autos do Processo em ep&iacute;grafe, devendo V.Sa. se manifestar quanto À aceita&ccedil;&atilde;o " & _
        " ou recusa do encargfo bem como apresentar proposta de honor&aacute;rios, concomitantemente, " & _
        " diretamente a&egrave;quele ju&iacute;zo;.(ou neste sistema)." & _
        " <br><br> <p align='center' class='style4'> Cordialmente. </p> " & _
        "  <p align='center'> <span class='style4'><strong>MARCIO MARCELO DA SILVA OLIVEIRA <br> Matr&iacute;cula 16341 <br> " & _
        "  Diretor da Divis&atilde;o de Per&iacute;cias Judiciais  </strong></span></p>  " & _
        "  <p><br> " & _
        "  <br> " & _
        "  Ilmo. Sr. " & txtNome_Perito.Text & "." & _
        "  <br>(" & m_Descr_Profissao & "/" & m_Descr_Especialidade & ")" & _
        " </body> " & _
        "</html>"
        '==========================================================================
        m_Email = lblEmail.Text
        m_Email1 = lblEmail1.Text
        'teste
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
        If m_Email = "" Then
            MsgErro("Email inválido")
        End If
        p_To = m_Email
        p_Cc = ""
        p_From_Nome = "DGJUR/DEINP/DIPEJ"
        'Trocar para email da DIPEJ
        p_From_Address = "DGJUR/DEINP/DIPEJ"
        p_From_Address = "santo@tjrj.jus.br"
        p_Subject = "Indicação do Perito no Processo"
        p_Html = EmailHTML
        p_Smtp_Hostname = "mail.tjrj.jus.br"
        p_Smtp_Portnum = "25"
        BalEnviarEmail.EnviarEmail(p_To, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        'PreencherProcPerito(txtNum_CNJ.Text)

    End Sub

    Protected Sub chkLaudoLiberado_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkLaudoLiberado.CheckedChanged
        If chkLaudoLiberado.Checked And lblData_Liberacao.Text = "" Then
            lblData_Liberacao.Text = Today.ToShortDateString
        End If
    End Sub

    Protected Sub GrdProcPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdProcPerito.SelectedIndexChanged
        Dim m_ID_PF As Long
        Dim m_Descr_Especialidade As String
        Dim m_Descr_Profissao As String
        'Dim Acento As HttpServerUtility
        m_ID_PF = CInt(GrdProcPerito.SelectedRow.Cells(5).Text)
        m_Descr_Especialidade = Trim(HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(3).Text))
        m_Descr_Profissao = Trim(HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(2).Text))
        m_Cod_Profissao = 0
        If m_Descr_Profissao <> "" Then
            CboProfissao.SelectedValue = CboProfissao.Items.FindByText(m_Descr_Profissao).Value
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        End If
        PreencherEspecialidade(m_Cod_Profissao)
        If m_Descr_Especialidade <> "" Then
            CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByText(m_Descr_Especialidade).Value
        Else
            CboEspecialidade.SelectedValue = "0" 'Generico
        End If
        ''''PreencherPeritos(m_Cod_Profissao, CInt(CboEspecialidade.Items.FindByValue(CboEspecialidade.Text).Value))
        txtID_PF.Text = GrdProcPerito.SelectedRow.Cells(5).Text
        ExibirDadosPerito()


    End Sub

    Protected Sub BtnCurriculum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCurriculum.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        'DGTECGEDAR Class
        'DGTECGEDARDOTNET Namespace
        Dim Obj As DGTECGEDAR
        Dim IDDoc As String
        Dim Bal As New BALPERITO(GetUsuario)
        Dim Ent As EntPERITO
        'Dim f_ID_PF As String
        Dim m_URL As String
        Dim mm_ID_PF As Integer

        'IDGED_FOTO
        'IDGED_CV
        'f_ID_PF = Session("ID").ToString
        'mm_ID_PF = CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)
        If txtID_PF.Text = "" Then
            MsgErro("Identificação Inválida")
            Exit Sub
        End If
        mm_ID_PF = CInt(txtID_PF.Text)
        If mm_ID_PF = 0 Then
            Session("ID") = 0
        Else
            Session("ID") = mm_ID_PF
        End If
        Ent = Bal.ExibirDadosEnt("ID", mm_ID_PF.ToString, "N") 'f_ID_PF)
        If Not Ent Is Nothing Then
            IDDoc = Ent.IDGED_CV
        Else
            IDDoc = ""
        End If
        If IDDoc <> "" Then
            Obj = New DGTECGEDAR
            Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
            m_URL = Obj.MontaURLCacheWeb(IDDoc)
            Obj.Finaliza()
            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes')", True)
        End If
    End Sub

    Protected Sub BtnVerFoto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnVerFoto.Click
        Dim mm_ID_PF As Integer
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            MsgErro("Identificação Inválida")
            Exit Sub
        End If
        'mm_ID_PF = CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)
        mm_ID_PF = CInt(txtID_PF.Text)
        If mm_ID_PF = 0 Then
            Session("ID") = 0
        Else
            Session("ID") = mm_ID_PF
        End If
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmExibirFoto.aspx', '_blank', 'height=401,width=301, Top=150,left=120')", True)
    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged

        txtNome_Perito.Text = ""
        Session("ID") = Nothing
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataSource = Nothing
        CboEspecialidade.DataBind()
        'Erros nas conversões
        Try
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
            'PreencherEspecialidade(CInt(CboProfissao.SelectedValue))
        Catch
            m_Cod_Profissao = CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value)
            'PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
        End Try
        PreencherEspecialidade(m_Cod_Profissao)
        If m_Cod_Profissao = 0 Then
            MsgErro("Selecione uma Profissão")
            Exit Sub
        End If
        If CboEspecialidade.SelectedValue = "GENÉRICO" Then
            m_Cod_Especialidade = 0
        Else
            Try
                m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            Catch
                m_Cod_Especialidade = CInt(CboEspecialidade.Items.FindByValue(CboEspecialidade.Text).Value)
            End Try
        End If

        'PreencherPeritos(m_Cod_Profissao, m_Cod_Especialidade)

    End Sub

    Protected Sub txtNum_CNJ1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ1.TextChanged
        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
    End Sub

    Protected Sub txtNum_CNJ2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ2.TextChanged
        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
        AtualizatxtNum_CNJ()

    End Sub
    Protected Sub AtualizatxtNum_CNJ() '_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ.TextChanged
        Dim Ent As EntPROC_CNJ
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
        If Len(txtNum_CNJ1.Text) = 15 Then
            txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 15) + ".8.19." + txtNum_CNJ2.Text
        Else
            txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 7) + "-" + Mid(txtNum_CNJ1.Text, 8, 2) + "." + Mid(txtNum_CNJ1.Text, 10, 4) + ".8.19." + txtNum_CNJ2.Text
        End If
        If Not ValidaNumCNJ(txtNum_CNJ.Text) Then
            MsgErro("Número CNJ Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_Processo.Text = ""
            Exit Sub
        End If
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ.Text = "" Then txtNum_CNJ.Text = Ent.Cod_CNJ
            If ValidaNumCNJ(Ent.Cod_CNJ) Then
                PreencherProcPerito(Ent.Cod_CNJ) '(GrdProcPerito)
            Else
                MsgErro("Número de CNJ Inválido")
                txtNum_CNJ1.Text = ""
                txtNum_CNJ2.Text = ""
                txtNum_Processo.Text = ""
            End If
        Else
            MsgErro("Número de CNJ Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_Processo.Text = ""
        End If

    End Sub

    Protected Sub BtnPerito_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPerito.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        txtNome_Perito.Enabled = True
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        If CboProfissao.Text <> "" And CboEspecialidade.Text <> "" Then
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
            If CboEspecialidade.SelectedValue = "GENÉRICO" Then
                m_Cod_Especialidade = 0
            Else
                m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            End If
            Session("Cod_Profissao") = m_Cod_Profissao
            Session("Cod_Especialidade") = m_Cod_Especialidade
            Session("Num_Processo1") = txtNum_CNJ1.Text
            Session("Num_Processo2") = txtNum_CNJ2.Text

            Response.Redirect("frmEscolherPerito.aspx")

            'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "changeMe('" & txtID_PF.ClientID & "');", True)
            'If txtID_PF.Text <> "0" And txtID_PF.Text <> "" Then
            'ExibirDadosPerito()
            'End If
        End If
        'txtNome_Perito.Enabled = False

    End Sub

    'Protected Sub txtNome_Perito_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome_Perito.TextChanged
    '    ExibirDadosPerito()
    'End Sub

End Class
