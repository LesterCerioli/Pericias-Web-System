Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports DGTECGEDARDOTNET
'Imports log4net
Imports App = System.Configuration.ConfigurationManager


Partial Public Class frmPeritoDCP
    Inherits BasePage
    Dim m_Cod_Profissao As Integer
    Dim m_Cod_Especialidade As Integer
    Dim EscolherPerito As String
    'Dim logger As log4net.ILog
    Dim EntCNJ As EntPROC_CNJ
    Dim m_Num_Processo_N As String
    Dim m_ID_nomeacao As Integer
    Dim NovoHonFixo As Boolean = False

    Private Sub frmPeritoDCP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'logger = log4net.LogManager.GetLogger("LogInFile")
        'logger.Debug("Acesso a Nomeação de Perito ...")
        Dim BalPer As New BALPERITO(GetUsuario)
        Dim BalComarca As New BALCOMARCA(GetUsuario)
        Dim m_ID_PF As Integer
        Dim DsProcPer1 As DataSet
        Dim BalProcPer1 As New BalProcesso_Perito(GetUsuario)
        Dim BalProcExemplo_CNJ As New BalProc_CNJ(GetUsuario)

        If Not IsPostBack Then

            Dim idNomeacao As String = Request.QueryString("idNomeacao")

            If GetUsuario.Login = "SANTO" Then
                LblNumProcExemplo.Visible = True
                txtNumProcExemplo.Visible = True
                'Exibir um número de processo exemplo somente para desenvolvedores, afim de realizar testes de uma determinada serventia
                'onde os 3 números finais do Num_CNJ ou os 3 Números intermediários do Número de Processo Antigo
                txtNumProcExemplo.Text = BalProcExemplo_CNJ.AcharExemploNumProcServ(GetUsuario.CodOrg)
                'est.CodOrg = 422
            Else
                LblNumProcExemplo.Visible = True
                txtNumProcExemplo.Visible = True
            End If

            If idNomeacao <> "" Then
                BtnPerito.Text = "Substituir"
                BtnNovoPer.Visible = False
                Session("Substituir") = "S"
            Else
                BtnPerito.Text = "Pesquisar Perito"
                BtnNovoPer.Visible = True
                Session("Substituir") = ""
            End If
            If Not Session("PERICIAS_CODORGAO") Is Nothing Then
                lblOrgaoUsuario.Text = "[ÓRGÃO: " & Session("PERICIAS_CODORGAO").ToString & " - " & PreencheNomeOrgao(Session("PERICIAS_CODORGAO").ToString) & "]"
            End If
            BtnInicio.Attributes.Add("OnClick", "return confirm('Confirma o Início da Perícia?');")
            HabilitaCampos(False)
            If Not Session("Msg") Is Nothing Then
                MsgErro(Session("Msg").ToString)
            End If
            If Session("Escolha") Is Nothing Then
                EscolherPerito = "N"
            Else
                EscolherPerito = Session("Escolha").ToString
            End If
            If Not Session("Cod_Profissao") Is Nothing Then
                m_Cod_Profissao = CInt(Session("Cod_Profissao").ToString)
                PreencherPROFISSAO()
                If m_Cod_Profissao <> 0 And EscolherPerito = "S" Then
                    CboProfissao.SelectedValue = CboProfissao.Items.FindByValue(m_Cod_Profissao.ToString).Value
                    PreencherEspecialidade(m_Cod_Profissao)
                    If Not Session("Cod_Especialidade") Is Nothing Then
                        m_Cod_Especialidade = CInt(Session("Cod_Especialidade").ToString)
                        If m_Cod_Especialidade <> 0 Then
                            If CboEspecialidade.Items.Count > 0 Then
                                CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByValue(m_Cod_Especialidade.ToString).Value
                            End If
                        End If
                    End If
                End If
            Else
                'logger.Debug("PreencherPROFISSAO()")
                PreencherPROFISSAO()
                PreencherEspecialidade(0)
            End If
            'If idNomeacao <> "" Then
            '    m_ID_nomeacao = CInt(idNomeacao)
            '    AtualizarDadosCNJ(m_ID_nomeacao)
            '    idNomeacao = ""
            'End If
            'End If

            '--------------------- informar qtes de processos que foram nomeados e que não cumpriram os prazos

            If idNomeacao <> "" Then
                m_ID_nomeacao = CInt(idNomeacao)
                'If Session("Substituir").ToString = "S" Then
                '    Session("Substituir") = "N"
                '    BtnPerito.Text = "Substituir"
                'Else
                '    BtnPerito.Text = "Pesquisar Perito"
                'End If
                AtualizarDadosCNJ(m_ID_nomeacao)
                idNomeacao = ""
            Else
                Session("ID") = ""
                If txtID_PF.Text = "" Then
                    txtID_PF.Text = Request.QueryString("ID_PF")
                End If

                If txtID_PF.Text <> "" Then
                    Session("ID") = txtID_PF.Text
                Else
                    txtNum_CNJ1.Attributes.Add("onblur", "return FormatarProcCNJ1('" & txtNum_CNJ1.ClientID & "');")
                    txtPrazo.Text = ""
                End If
                '-------------------------------------------------------
                If txtNome_Perito.Text = "" And EscolherPerito = "S" Then
                    If Not Session("ID") Is Nothing Then
                        If Session("ID").ToString <> "" Then
                            txtID_PF.Text = Session("ID").ToString
                            txtNome_Perito.Text = BalPer.Nome_ID(txtID_PF.Text)
                            '------------------------------------------
                            If txtID_PF.Text <> "" Then
                                m_ID_PF = CInt(txtID_PF.Text)
                            Else
                                m_ID_PF = 0
                            End If
                            '=================================

                            'If Len(txtNum_CNJ1.Text) = 15 Then
                            '   txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 15) + ".8.19." + txtNum_CNJ2.Text
                            'Else
                            '   txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 7) + "-" + Mid(txtNum_CNJ1.Text, 8, 2) + "." + Mid(txtNum_CNJ1.Text, 10, 4) + ".8.19." + txtNum_CNJ2.Text
                            'End If
                            If Not Session("Num_CNJ") Is Nothing Then
                                txtNum_CNJ.Text = Session("Num_CNJ").ToString
                                txtNum_CNJ1.Text = Mid(txtNum_CNJ.Text, 1, 15)
                                txtNum_CNJ2.Text = Mid(txtNum_CNJ.Text, 22, 4)
                            End If
                            '===================================

                            If (Not Session("Cod_Profissao") Is Nothing) And (Not Session("Cod_Especialidade") Is Nothing) Then
                                If lblData_Nomeacao.Text = "" Then
                                    'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & "," & Today.ToShortDateString & ")")
                                    DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, Session("Cod_Profissao").ToString, IIf(Session("Cod_Especialidade").ToString = "GENÉRICO", "", Session("Cod_Especialidade").ToString).ToString, CDate(Today.ToShortDateString))
                                Else
                                    'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & "," & lblData_Nomeacao.Text & ")")
                                    DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, Session("Cod_Profissao").ToString, IIf(Session("Cod_Especialidade").ToString = "GENÉRICO", "", Session("Cod_Especialidade").ToString).ToString, CDate(lblData_Nomeacao.Text))
                                End If
                            Else
                                If lblData_Nomeacao.Text = "" Then
                                    'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Today.ToShortDateString & ")")
                                    DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "", CDate(Today.ToShortDateString))
                                Else
                                    'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & lblData_Nomeacao.Text & ")")
                                    DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "", CDate(lblData_Nomeacao.Text))
                                End If
                            End If
                            '------------------------------------------
                            'logger.Debug("ExibirDadosPerito(DsProcPer1)")
                            ExibirDadosPerito(DsProcPer1)
                        End If
                    End If
                End If
                If Not Session("Num_CNJ") Is Nothing Then
                    If Session("Num_CNJ").ToString <> "" Then
                        txtNum_CNJ.Text = Session("Num_CNJ").ToString
                    End If
                End If
                If Not Session("Num_Processo1") Is Nothing Then
                    txtNum_CNJ1.Text = Session("Num_Processo1").ToString
                End If
                If Not Session("Num_Processo2") Is Nothing Then
                    txtNum_CNJ2.Text = Session("Num_Processo2").ToString
                End If
                If txtNum_CNJ1.Text <> "" And txtNum_CNJ2.Text <> "" Then
                    'logger.Debug("AtualizatxtNum_CNJ()")
                    'Preencher a Grid
                    AtualizatxtNum_CNJ()
                End If
            End If
            If UCase(Trim(CboProfissao.SelectedItem.Text)) = "MEDICO" Or UCase(Trim(CboProfissao.SelectedItem.Text)) = "MÉDICO" Then
                ChkPerInter.Visible = True
            Else
                ChkPerInter.Visible = False
            End If
        End If

    End Sub

    Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

        'logger.Debug("PreencherEspecialidade - " & m_Cod_Profissao & " ...")

        If m_Cod_Profissao = 0 Then
            CboEspecialidade.Items.Clear()
            CboEspecialidade.Items.Insert(0, "Selecione a especialidade")
            CboEspecialidade.SelectedIndex = 0
        End If

        Dim bal As New BALEspecialidade(GetUsuario)
        Dim ent As New EntEspecialidade
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_Cod_Profissao)

        If dsfila.Tables(0).Rows.Count < 1 Then
            If CboEspecialidade.SelectedIndex = -1 Then
                CboEspecialidade.Items.Insert(0, "Selecione a especialidade")
            End If
            CboEspecialidade.SelectedIndex = 0
            Exit Sub
        End If
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataTextField = "Descr_Especialidade"
        CboEspecialidade.DataValueField = "Cod_Especialidade"
        CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
        CboEspecialidade.DataBind()
        CboEspecialidade.Items.Insert(0, "Selecione a especialidade")
        CboEspecialidade.SelectedIndex = 0

    End Sub

    Private Sub PreencherPROFISSAO()

        'logger.Debug("PreencherPROFISSAO()...")

        Dim bal As New BALProfissao(GetUsuario)
        Dim ent As New EntProfissao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        If dsfila.Tables(0).Rows.Count < 1 Then
            If CboProfissao.SelectedIndex = -1 Then
                CboProfissao.Items.Insert(0, "Selecione uma profissão")
            End If
            CboProfissao.SelectedIndex = 0
            Exit Sub
        End If
        CboProfissao.Items.Clear()
        CboProfissao.DataTextField = "Descr_PROFISSAO"
        CboProfissao.DataValueField = "Cod_PROFISSAO"
        CboProfissao.DataSource = dsfila.Tables(0) '.DefaultView
        CboProfissao.DataBind()
        CboProfissao.Items.Insert(0, "Selecione uma profissão")
        CboProfissao.SelectedIndex = 0

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        'logger.Debug("BtnGravar_Click ...")

        Dim Bal As New BalProcesso_Perito(GetUsuario)
        Dim BalPgto As New BalPagamento_Perito(GetUsuario)
        Dim BalEnviarEmail As New BalEmail(GetUsuario)
        Dim m_PRAZO_ENTREGA As String
        Dim m_Data_Novo_Hon As Date

        'Verificar se consta como nomeado na tabela Processo_Perito
        ' se sim não grava nada
        'logger.Debug("txtNome_Perito.Text: " & txtNome_Perito.Text)
        'logger.Debug("CboProfissao.Text: " & CboProfissao.Text)

        If txtNome_Perito.Text = "" Then
            MsgErro("Favor escolher um perito.")
            Exit Sub
        End If

        If CboProfissao.Text = "Selecione uma profissão" Then
            MsgErro("Favor selecionar a profissão do perito.")
            Exit Sub
        End If

        If CboEspecialidade.SelectedIndex <= 0 Then
            MsgErro("Favor selecionar a especialidade do perito.")
            Exit Sub
        End If

        If RdbJustGrat.SelectedValue = "" Then
            MsgErro("Favor informar se justiça gratuita ou não.")
            Exit Sub
        End If

        If txtPrazo.Text = "" Then
            MsgErro("Informe o prazo.")
            txtPrazo.Focus()
            Exit Sub
        End If


        If lblData_Nomeacao.Text = "" Then
            MsgErro("Falha na gravação da nomeação. Não há data para esta nomeação.")
            Exit Sub
        End If

        Dim m_Num_Oficio As Integer
        Dim dsOficio As DataSet

        If CboEspecialidade.SelectedIndex <= 0 Then
            m_Cod_Especialidade = 0
        Else
            m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
        End If

        'logger.Debug("Bal.NumerarOficio(" & txtNum_CNJ.Text & "," & txtID_PF.Text & ")")
        dsOficio = Bal.NumerarOficio(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text))

        m_Num_Oficio = CInt(dsOficio.Tables(0).Rows(0).Item(0))

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
        Dim m_Data_Liberacao As String = String.Empty
        Dim m_Data_Cancelamento As String = String.Empty
        Dim m_Cod_Tipo_Pericia As Integer
        Dim m_HONORARIOS_JUIZ As Double
        Dim m_INTERDICAO_PER As String

        m_PRAZO_ENTREGA = txtPrazo.Text
        m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        m_Cod_Tipo_Pericia = 3

        If chkLaudoLiberado.Checked And lblData_Liberacao.Text = "" Then
            m_Data_Liberacao = CStr(Date.Now.ToShortDateString)
            lblData_Liberacao.Text = m_Data_Liberacao
        End If
        If chkLaudoCancelado.Checked And lblData_Cancelamento.Text = "" Then
            m_Data_Cancelamento = CStr(Date.Now.ToShortDateString)
            lblData_Cancelamento.Text = m_Data_Cancelamento
        End If
        If txtHonJuiz.Text <> "" And IsNumeric(txtHonJuiz.Text) Then
            m_HONORARIOS_JUIZ = Val(txtHonJuiz.Text)
        Else
            m_HONORARIOS_JUIZ = 0
        End If
        If ChkPerInter.Checked Then
            m_INTERDICAO_PER = "S"
            If RdInterdicao.Items(0).Selected Then
                'Psiquiatria Local
                m_Cod_Tipo_Pericia = 1
            Else
                'Psiquiatria em Audiência 
                m_Cod_Tipo_Pericia = 2
            End If
        Else
            m_INTERDICAO_PER = "N"
            m_Cod_Tipo_Pericia = 3
        End If
        If lblData_Novo_Hon.text = "" Then
            If txtHonJuiz.Text <> "" Then
                m_Data_Novo_Hon = Today.Date
            Else
                m_Data_Novo_Hon = Nothing
            End If
        Else
            m_Data_Novo_Hon = CDate(lblData_Novo_Hon.text)
        End If

        ''logger.Debug("Bal.GravarProcesso_Perito(" & txtNum_CNJ.Text & "," & txtID_PF.Text & "," & Today.ToShortDateString & "," & "," & "," & m_Data_Liberacao & "," & m_PRAZO_ENTREGA & "," & m_Cod_Profissao & "," & m_Cod_Especialidade & "," & Usuario & "," & "," & "," & m_Num_Oficio & "," & Year(Now).ToString & "," & RdbJustGrat.SelectedValue & "," & m_Cod_Tipo_Pericia & "," & m_HONORARIOS_JUIZ.ToString & "," & m_INTERDICAO_PER & ")")
        'logger.Debug("Bal.GravarProcesso_Perito(" & txtNum_CNJ.Text & "," & txtID_PF.Text & "," & Today.ToShortDateString & "," & "," & "," & m_Data_Liberacao & "," & m_PRAZO_ENTREGA & "," & m_Cod_Profissao & "," & m_Cod_Especialidade & "," & Usuario & "," & "," & "," & m_Num_Oficio & "," & Year(Now).ToString & "," & RdbJustGrat.SelectedValue & "," & m_Cod_Tipo_Pericia & "," & m_HONORARIOS_JUIZ.ToString & "," & m_INTERDICAO_PER & "," & m_Data_Novo_Hon & "," & GetUsuario.CodOrg & ")")
        'm_Data_Novo_Hon, GetUsuario.CodOrg
        'Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(txtID_PF.Text), lblData_Nomeacao.Text, "", CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, m_Num_Oficio, Year(Now), RdbJustGrat.SelectedValue, m_Cod_Tipo_Pericia, m_HONORARIOS_JUIZ, m_INTERDICAO_PER)
        Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(txtID_PF.Text), lblData_Nomeacao.Text, "", CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, m_Num_Oficio, Year(Now), RdbJustGrat.SelectedValue, m_Cod_Tipo_Pericia, m_HONORARIOS_JUIZ, m_INTERDICAO_PER, m_Data_Novo_Hon, GetUsuario.CodOrg)
        Dim m_Data_Envio_DGPCF As String = ""
        Dim m_Num_Prot As Long = 11111 'teste

        'logger.Debug("BalComarca.ExibirDadosServentia(" & GetUsuario.CodOrg & ")")
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
        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, CInt(txtID_PF.Text), CboProfissao.SelectedValue, _
                                              IIf(CboEspecialidade.SelectedIndex = 0, "", CboEspecialidade.SelectedValue).ToString, CDate(Today.ToShortDateString)) 'today
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

        If Session("indEmailEnviado").ToString = "N" Or Session("indEmailEnviado") Is Nothing Then
            EnviaEmail("", "", "", "", "")
            Bal.AtualizaProcessoPeritoEmailEnviado(txtNum_CNJ.Text, CLng(txtID_PF.Text), m_Cod_Profissao, m_Cod_Especialidade, "S", lblData_Nomeacao.Text)
            Session("indEmailEnviado") = "S"
        End If

        'logger.Debug("PreencherProcPerito(" & txtNum_CNJ.Text & ")")
        PreencherProcPerito(txtNum_CNJ.Text)
        txtAnotacao.Visible = False
        LblAnotacao.Visible = False

        If chkLaudoLiberado.Checked Then

        End If
        If chkLaudoCancelado.Checked Then

        End If

        MsgErro("Gravação realizada com sucesso.")
        LimparInformacoesPerito()

    End Sub

    Private Sub ExibirDadosPerito(ByVal DsProcPer As DataSet)

        'logger.Debug("ExibirDadosPerito(DsProcPer)...")

        Dim Bal As New BALAnotacao(GetUsuario)
        Dim BalAceitacaoPer As New BalAceitacao_Perito(GetUsuario)
        Dim Ent As New EntAnotacao
        Dim BalPer As New BALPERITO(GetUsuario)
        Dim EntPer As New EntPERITO
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim Balpgto As New BalPagamento_Perito(GetUsuario)
        Dim EntProcPer As New EntProcesso_Perito
        Dim Ds As DataSet
        Dim DsPer As DataSet
        '        Dim DsProcPer As DataSet
        Dim DsPgto As DataSet
        Dim rsPer As DataRow
        Dim rsPgto As DataRow
        Dim rsProcPer As DataRow
        Dim m_ID_PF As Long
        Dim i As Integer
        Dim m_Justica_Gratuita As String
        Dim sCodEspecialidade As String = String.Empty
        Dim sCodProfissao As String = String.Empty
        'Dim m_Tacita As String

        If txtID_PF.Text <> "" Then
            m_ID_PF = CInt(txtID_PF.Text)
        Else
            m_ID_PF = 0
        End If
        lblData_Liberacao.Text = ""

        'If (Not Session("Cod_Profissao") Is Nothing) And (Not Session("Cod_Especialidade") Is Nothing) Then
        '    If lblData_Nomeacao.Text = "" Then
        '        'logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & "," & Today.ToShortDateString & ")")
        '        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, Session("Cod_Profissao").ToString, IIf(Session("Cod_Especialidade").ToString = "GENÉRICO", "", Session("Cod_Especialidade").ToString).ToString, CDate(Today.ToShortDateString))
        '    Else
        '        'logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & "," & lblData_Nomeacao.Text & ")")
        '        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, Session("Cod_Profissao").ToString, IIf(Session("Cod_Especialidade").ToString = "GENÉRICO", "", Session("Cod_Especialidade").ToString).ToString, CDate(lblData_Nomeacao.Text))
        '    End If
        'Else
        '    If lblData_Nomeacao.Text = "" Then
        '        'logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Today.ToShortDateString & ")")
        '        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "", CDate(Today.ToShortDateString))
        '    Else
        '        'logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & lblData_Nomeacao.Text & ")")
        '        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "", CDate(lblData_Nomeacao.Text))
        '    End If
        'End If

        If DsProcPer.Tables(0).Rows.Count > 0 Then
            rsProcPer = DsProcPer.Tables(0).Rows(0)

            Session("IndEmailEnviado") = IIf(IsDBNull(rsProcPer("Ind_email_enviado")) Or rsProcPer("Ind_email_enviado").ToString = "N", "N", "S")

            If rsProcPer("nome") Is Nothing Or rsProcPer("nome").ToString = "" Then
                txtNome_Perito.Text = ""
            Else
                txtNome_Perito.Text = rsProcPer("Nome").ToString
            End If

            BtnAnotacao.Enabled = True
            If rsProcPer("Data_Nomeacao") Is Nothing Or rsProcPer("Data_Nomeacao").ToString = "" Then
                lblData_Nomeacao.Text = Today.ToShortDateString
            Else
                lblData_Nomeacao.Text = Convert.ToDateTime(rsProcPer("Data_Nomeacao").ToString).ToShortDateString
            End If
            If (rsProcPer("Honorarios_Juiz").ToString <> "0") And (Not rsProcPer("Honorarios_Juiz").ToString = Nothing) Then
                txtHonJuiz.Text = rsProcPer("Honorarios_Juiz").ToString
            Else
                txtHonJuiz.Text = ""
            End If

            If (rsProcPer("Honorarios").ToString <> "0") And (Not rsProcPer("Honorarios").ToString = Nothing) Then
                txtHonPer.Text = rsProcPer("Honorarios").ToString
            Else
                txtHonPer.Text = ""
            End If

            If rsProcPer("Interdicao_Per").ToString = "S" Then
                ChkPerInter.Checked = True
            Else
                ChkPerInter.Checked = False
            End If

            If rsProcPer("Data_Inicio_Per") Is Nothing Or rsProcPer("Data_Inicio_Per").ToString = "" Then
                lblData_Inicio.Text = ""
            Else
                lblData_Inicio.Text = Convert.ToDateTime(rsProcPer("Data_Inicio_Per").ToString).ToShortDateString
            End If
            txtID_Nomeacao.Text = rsProcPer("ID_Nomeacao").ToString

            Dim m_Data_PrimAceitacao As String = ""
            Dim m_Data_SegAceitacao As String = ""
            Dim m_Data_Negacao As String = ""
            Dim DsSetMotivo As DataSet

            m_Data_Negacao = BalAceitacaoPer.ExibirData_Negacao(txtID_Nomeacao.Text)
            m_Data_PrimAceitacao = BalAceitacaoPer.ExibirData_Aceitacao(txtID_Nomeacao.Text, 1)
            m_Data_SegAceitacao = BalAceitacaoPer.ExibirData_Aceitacao(txtID_Nomeacao.Text, 2)
            lblData_Novo_Hon.Text = BalAceitacaoPer.ExibirData_Novo_Hon(txtID_Nomeacao.Text)

            If m_Data_PrimAceitacao = "" Then
                lblData_Aceitacao.Text = ""
                'logger.Debug("HabilitaAceiteLaudo(False)")
                HabilitaAceiteLaudo(False)
                RdbJustGrat.Enabled = True
            Else
                lblData_Aceitacao.Text = Convert.ToDateTime(m_Data_PrimAceitacao).ToShortDateString
                'logger.Debug("HabilitaAceiteLaudo(True)")
                HabilitaAceiteLaudo(True)
                RdbJustGrat.Enabled = False
            End If
            If m_Data_SegAceitacao = "" Then
                lblData_Seg_Aceitacao.Text = ""
                'logger.Debug("HabilitaAceiteLaudo(False)")
                HabilitaAceiteLaudo(False)
                RdbJustGrat.Enabled = True
            Else
                lblData_Seg_Aceitacao.Text = Convert.ToDateTime(m_Data_SegAceitacao).ToShortDateString
                'logger.Debug("HabilitaAceiteLaudo(True)")
                HabilitaAceiteLaudo(True)
                RdbJustGrat.Enabled = False
            End If
            If m_Data_Negacao <> "" Then
                lblData_Negacao.Text = m_Data_Negacao
                'logger.Debug("HabilitaAceiteLaudo(False)")
                HabilitaAceiteLaudo(False)
                BtnInicio.Enabled = False
                BtnGravar.Enabled = False
                BtnEmailNomeacao.Enabled = False
                RdbJustGrat.Enabled = False
                lblMotivo_Recusa.Visible = True
                txtMotivo_Recusa.Visible = True
                DsSetMotivo = BalAceitacaoPer.ExibirDadosSet(CInt(txtID_Nomeacao.Text))
                txtMotivo_Recusa.Text = ""
                If DsSetMotivo.Tables(0).Rows.Count > 0 Then
                    i = 0
                    For Each Rss As DataRow In DsSetMotivo.Tables(0).Rows
                        If DsSetMotivo.Tables(0).Rows(i).Item(13).ToString <> "" Then
                            txtMotivo_Recusa.Text = DsSetMotivo.Tables(0).Rows(i).Item(13).ToString
                        End If
                        i = i + 1
                    Next

                End If
            Else
                If lblData_Inicio.Text = "" Then
                    If Not NovoHonFixo Then
                        BtnInicio.Enabled = True
                    End If
                    BtnGravar.Enabled = True
                    BtnEmailNomeacao.Enabled = True
                    RdbJustGrat.Enabled = True
                End If
                lblData_Negacao.Text = ""
                lblMotivo_Recusa.Visible = False
                txtMotivo_Recusa.Visible = False
            End If

            If rsProcPer("Data_Liberacao") Is Nothing Or rsProcPer("Data_Liberacao").ToString = "" And _
            lblData_Aceitacao.Text <> "" And lblData_Negacao.Text = "" Then
                HabilitaAceiteLaudo(True)
                tbAceiteLaudo.Visible = True
                chkLaudoLiberado.Enabled = True
                chkLaudoLiberado.Checked = False
                RdbJustGrat.Enabled = False
            ElseIf rsProcPer("Data_Nomeacao").ToString <> "" And rsProcPer("Data_Aceitacao").ToString = "" And rsProcPer("Data_Nomeacao").ToString = "" Then
                HabilitaAceiteLaudo(False)
            ElseIf rsProcPer("Data_Liberacao").ToString <> "" Then
                lblData_Liberacao.Text = Convert.ToDateTime(rsProcPer("Data_Liberacao").ToString).ToShortDateString
                HabilitaAceiteLaudo(True)
                tbAceiteLaudo.Visible = True
                BtnGravar.Enabled = False
                chkLaudoLiberado.Checked = True
                chkLaudoLiberado.Enabled = False
                RdbJustGrat.Enabled = False

            ElseIf rsProcPer("Data_Cancelamento").ToString <> "" And lblData_Negacao.Text = "" Then
                HabilitaAceiteLaudo(False)
                tbAceiteLaudo.Visible = False
                BtnGravar.Enabled = True
                If lblData_Aceitacao.Text <> "" Then
                    chkLaudoLiberado.Checked = True
                    chkLaudoLiberado.Enabled = False
                End If
            Else
                HabilitaAceiteLaudo(False)
                If lblData_Negacao.Text = "" And lblData_Aceitacao.Text <> "" Then
                    tbAceiteLaudo.Visible = True
                    chkLaudoLiberado.Checked = True
                    chkLaudoLiberado.Enabled = False
                End If
            End If

            m_Justica_Gratuita = DsProcPer.Tables(0).Rows(0).Item("JG").ToString
            RdbJustGrat.SelectedValue = m_Justica_Gratuita

            txtPrazo.Text = DsProcPer.Tables(0).Rows(0).Item("Prazo").ToString

            If lblData_Aceitacao.Text = "" Then
                If rsProcPer("Ind_Tacita").ToString = "S" Then
                    lblAceitacao_Tacita.Visible = True
                Else
                    lblAceitacao_Tacita.Visible = False
                End If
            Else
                If rsProcPer("Ind_Tacita").ToString = "S" Then
                    lblAceitacao_TacitaPos.Visible = True
                Else
                    lblAceitacao_TacitaPos.Visible = False
                End If
            End If

            'Data da Segunda Aceitação
            Dim Num_Aceitacoes_Proc As Integer
            Num_Aceitacoes_Proc = 0
            Num_Aceitacoes_Proc = BalAceitacaoPer.Qte_Aceitacoes_Proc(txtID_Nomeacao.Text)

            'If Num_Aceitacoes_Proc = 2 Then
            'lblData_Seg_Aceitacao.Text = BalAceitaPer.ExibirData_Aceitacao(txtID_Nomeacao.Text, 2)
            'Else
            'lblData_Seg_Aceitacao.Text = ""
            'End If
            DsPgto = Nothing
            If CboEspecialidade.SelectedIndex <= 0 Then
                m_Cod_Especialidade = 0
            Else
                m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            End If

            'logger.Debug("Balpgto.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & m_Cod_Especialidade & ")")
            DsPgto = Balpgto.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, m_Cod_Especialidade, CDate(lblData_Nomeacao.Text))

            If DsPgto.Tables(0).Rows.Count > 0 Then
                rsPgto = DsPgto.Tables(0).Rows(0)

                If rsPgto("Data_Autorizacao") Is Nothing Then
                    lblData_Liberacao.Text = rsPgto("Data_Autorizacao").ToString
                    chkLaudoLiberado.Checked = True
                End If
                If rsPgto("Data_Cancelamento") Is Nothing Then
                    lblData_Liberacao.Text = rsPgto("Data_Cancelamento").ToString
                End If
            End If
        Else
            BtnGravar.Enabled = True
            BtnAnotacao.Enabled = True
            ' txtPrazo.Text = ""
            lblData_Nomeacao.Text = Date.Now.ToShortDateString
            Session("IndEmailEnviado") = "N"
        End If

        DsProcPer = Nothing
        'logger.Debug("Bal.ExibirAnotPer(" & m_ID_PF.ToString & ")")
        Ds = Bal.ExibirAnotPer(m_ID_PF)
        If Ds.Tables(0).Rows.Count > 0 Then
            txtAnotacao.Visible = True
            LblAnotacao.Visible = True
            txtAnotacao.Text = ""
            If Ds.Tables(0).Rows.Count > 0 Then
                i = 0
                For Each rs As DataRow In Ds.Tables(0).Rows
                    txtAnotacao.Text = txtAnotacao.Text + " - " + Ds.Tables(0).Rows(i).Item(4).ToString + Chr(13) + Ds.Tables(0).Rows(i).Item(3).ToString + Chr(13)  'rs("Descr_Anotacao").ToString
                    i = i + 1
                Next
            End If
        Else
            LblAnotacao.Visible = False
            txtAnotacao.Visible = False
        End If
        Ds = Nothing
        'logger.Debug("BalPer.ExibirDadosSet(" & m_ID_PF.ToString & ")")
        DsPer = BalPer.ExibirDadosSet(m_ID_PF)

        If DsPer.Tables(0).Rows.Count > 0 Then
            rsPer = DsPer.Tables(0).Rows(0)

            For Each r As DataRow In DsPer.Tables(0).Rows
                If r("seq_email").ToString = "1" Then
                    lblEmail.Text = r("email").ToString
                ElseIf lblEmail.Text = "" Then
                    lblEmail.Text = ""
                End If

                If r("seq_email").ToString = "2" Then
                    lblEmail1.Text = r("email").ToString
                ElseIf lblEmail1.Text = "" Then
                    lblEmail1.Text = ""
                End If
            Next

            lblData_Cadastramento.Text = Convert.ToDateTime(rsPer("Data_Cadastramento").ToString).ToShortDateString
            txtQtePendentes.Text = rsPer("QtePendentes").ToString
            txtQteAceitos.Text = rsPer("QteAceitos").ToString
            txtQteRecusadas.Text = rsPer("QteRecusadas").ToString

            txtNome_Perito.Text = rsPer("Nome").ToString

            If lblEmail.Text <> "" Or lblEmail1.Text <> "" Then
                BtnEmailNomeacao.Enabled = True
            End If
        End If

        DsPer = Nothing

        If lblData_Aceitacao.Text = "" Then
            BtnInicio.Enabled = False
        Else
            If Not NovoHonFixo Then
                BtnInicio.Enabled = True
            End If
        End If
        If lblData_Negacao.Text <> "" Then
            BtnInicio.Enabled = False
            BtnGravar.Enabled = False
            BtnEmailNomeacao.Enabled = False
            RdbJustGrat.Enabled = False
        End If
        If lblData_Inicio.Text <> "" Then
            Bloquear()
        Else
            If BtnInicio.Enabled Then
                DesBloquear()
            End If
        End If
        'If RdbJustGrat.SelectedValue = "S" Then
        '    txtHonJuiz.Enabled = False
        'End If
        If lblData_Inicio.Text <> "" Then
            BtnInicio.Enabled = False
            BtnGravar.Enabled = False
            BtnEmailNomeacao.Enabled = False
            RdbJustGrat.Enabled = False
        End If

    End Sub

    Protected Sub txtNum_Processo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_Processo.TextChanged

        'logger.Debug("txtNum_Processo_TextChanged ...")

        Dim Ent As EntPROC_CNJ
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim Cod_CNJ_Valido As Boolean
        Dim ProcServ As Boolean

        NovoHonFixo = False
        'logger.Debug("ValidaNumProc(" & txtNum_Processo.Text & ")")
        If Not ValidaNumProc(txtNum_Processo.Text) Then
            MsgErro("Número de Processo Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_CNJ.Text = ""
            HabilitaCampos(False)
            txtNum_Processo.Focus()
            Exit Sub
        End If

        'logger.Debug("BalP.ExibirDadosEnt(" & txtNum_Processo.Text & "," & txtNum_CNJ.Text & ")")
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ1.Text = "" Then
                txtNum_CNJ1.Text = Mid(Ent.Cod_CNJ, 1, 7) + Mid(Ent.Cod_CNJ, 9, 2) + Mid(Ent.Cod_CNJ, 12, 4)
                txtNum_CNJ2.Text = Mid(Ent.Cod_CNJ, 22, 4)
                txtNum_CNJ.Text = Ent.Cod_CNJ
                HabilitaCampos(True)
            End If

            ProcServ = BalP.VerificarProc_Serventia(txtNum_Processo.Text, Ent.Cod_CNJ, GetUsuario.CodOrg)

            If Not ProcServ Then
                MsgErro("Processo não pertence a esta Serventia")
                txtNum_CNJ.Text = ""
                txtNum_CNJ1.Text = ""
                txtNum_CNJ2.Text = ""
                txtNum_Processo.Text = ""
            Else
                'logger.Debug("ValidaNumCNJ(" & Ent.Cod_CNJ & ")")
                Cod_CNJ_Valido = ValidaNumCNJ(Ent.Cod_CNJ)
                If Cod_CNJ_Valido Then
                    'logger.Debug("PreencherProcPerito(" & Ent.Cod_CNJ & ")")
                    PreencherProcPerito(Ent.Cod_CNJ) '(GrdProcPerito)
                    'Ao selecionar ... EntProcPer = BalProcPer.ExibirDadosEnt(m_Cod_CNJ,m_ID_PF)
                    HabilitaCampos(True)
                Else
                    MsgErro("Número de CNJ Inválido")
                    txtNum_CNJ1.Text = ""
                    txtNum_CNJ2.Text = ""
                    txtNum_Processo.Text = ""
                    HabilitaCampos(False)
                    txtNum_Processo.Focus()
                End If
            End If
        Else
            MsgErro("Número de Processo/CNJ não localizado!")
        End If


        'BtnVerCNJ.Enabled = False
        '*************************************************************
        'Verificar se o processo, no momento, pertence ao Juízo atual
        '*************************************************************
    End Sub

    Protected Sub BtnAnotacao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAnotacao.Click
        'logger.Debug("BtnAnotacao_Click ...")
        Dim Bal As New BALPERITO(GetUsuario)
        Dim m_CPF As String = String.Empty
        Dim EntCPF As New EntPERITO
        Dim mm_ID_PF As Long
        'Anotações
        If txtID_PF.Text = "" Then
            MsgErro("Selecione o Perito")
            Exit Sub
        End If
        mm_ID_PF = Convert.ToInt64(txtID_PF.Text)
        m_CPF = Bal.ID_PF_CPF(mm_ID_PF)
        Session("Nome") = txtNome_Perito.Text
        'logger.Debug("txtNome_Perito.Text: " & txtNome_Perito.Text)
        'logger.Debug("Abre frmAnotacaoDCP.aspx")
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmAnotacaoDCP.aspx', '_blank', 'height=362,width=515,Top=270,left=420,scrollbars=0,toolbar=0,resizable=0,location=0,status=0');", True)
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmAnotacaoDCP.aspx?m_CPF=" & m_CPF & "', '_blank', 'height=500,width=515,Top=200,left=420,scrollbars=0,toolbar=0,resizable=0,location=0,status=0');", True)

    End Sub

    Protected Sub BtnNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovo.Click
        'logger.Debug("BtnNovo_Click/Limpar ...")
        Limpar()

    End Sub

    Private Sub LimparInformacoesPerito()

        If Session("Substituir").ToString = "S" Then
            Session("Substituir") = "N"
            BtnPerito.Text = "Substituir"
            BtnNovoPer.Visible = False
        Else
            BtnPerito.Text = "Pesquisar Perito"
            BtnNovoPer.Visible = True
        End If
        NovoHonFixo = False
        lblAceitacao_Tacita.Visible = False
        lblData_Nomeacao.Text = ""
        lblData_Novo_Hon.Text = ""
        'ChkJustGrat.Checked = False
        'ChkJustGrat.Enabled = False
        txtPrazo.Enabled = False
        CboProfissao.SelectedIndex = 0
        CboEspecialidade.SelectedIndex = CInt(IIf(CboEspecialidade.SelectedIndex <> -1, 0, -1))
        txtNome_Perito.Text = ""
        lblData_Cadastramento.Text = ""
        lblEmail.Text = ""
        lblEmail1.Text = ""
        txtQteAceitos.Text = ""
        txtQtePendentes.Text = ""
        txtPrazo.Text = ""
        txtAnotacao.Text = ""
        txtAnotacao.Visible = False
        LblAnotacao.Visible = False
        lblData_Aceitacao.Text = ""
        lblData_Negacao.Text = ""
        txtHonJuiz.Text = ""
        txtHonPer.Text = ""
        ChkPerInter.Checked = False
        lblData_Inicio.Text = ""
        txtQteRecusadas.Text = ""
        'If RdbJustGrat.SelectedValue <> "" Then
        ' RdbJustGrat.SelectedValue = ""
        ' End If
        RdbJustGrat.ClearSelection()
        HabilitaAceiteLaudo(False)
        BtnEmailNomeacao.Enabled = False
        BtnGravar.Enabled = False
        BtnAnotacao.Enabled = False
        'BtnVerCNJ.Enabled = False
        lblMotivo_Recusa.Visible = False
        txtMotivo_Recusa.Visible = False
        txtMotivo_Recusa.Text = ""
        ChkPerInter.Visible = False
        lblData_Seg_Aceitacao.Text = ""
        'BtnNovoPer.Visible = True

    End Sub

    Private Sub Limpar()
        'logger.Debug("Limpar()")

        lblData_Liberacao.Text = ""
        chkLaudoLiberado.Checked = False
        chkLaudoLiberado.Enabled = False
        'RdbJustGrat.SelectedValue = ""
        RdbJustGrat.ClearSelection()
        tbAceiteLaudo.Visible = False
        txtNum_CNJ1.Text = ""
        txtNum_CNJ2.Text = ""
        txtNum_CNJ.Text = ""
        txtNum_Processo.Text = ""
        txtID_PF.Text = ""
        GrdProcPerito.DataSource = Nothing
        GrdProcPerito.DataBind()

        Session.Remove("ID")
        Session.Remove("Cod_Profissao")
        Session.Remove("Cod_Especialidade")
        Session.Remove("Num_Processo1")
        Session.Remove("Num_Processo2")
        Session.Remove("IndEmailEnviado")

        pnlPeritos.Visible = False
        pnlPeritos.Enabled = False
        'BtnGravar.Enabled = False
        'BtnEmailNomeacao.Enabled = False
        'BtnAnotacao.Enabled = False
        BtnVerCNJPend.Enabled = True

        HabilitaCampos(False)
        LimparInformacoesPerito()
        BtnNovoPer.Visible = True
        Session("Substituir") = "N"

    End Sub

    Protected Sub PreencherProcPerito(ByVal Num_CNJ As String)

        'logger.Debug("PreencherProcPerito(" & Num_CNJ & ") ...")

        Dim Ds As DataSet
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        'logger.Debug("BalProcPer.ExibirDadosTodos(" & Num_CNJ & ")")
        Ds = BalProcPer.ExibirDadosTodos(Num_CNJ)
        GrdProcPerito.DataSource = Ds.Tables(0)
        GrdProcPerito.DataBind()
        pnlPeritos.Visible = True
        pnlPeritos.Enabled = True
    End Sub

    Protected Sub BtnEmailNomeacao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEmailNomeacao.Click

        'logger.Debug("BtnEmailNomeacao_Click ...")

        If EnviaEmail("", "", "", "", "") Then
            MsgErro("E-mail enviado com sucesso.")
        Else
            MsgErro("Não foi possível enviar o e-mail.")
        End If

    End Sub

    Private Function EnviaEmail(ByVal sTO As String, ByVal sCc As String, ByVal sFrom As String, ByVal sFromAddress As String _
                                , ByVal sSubject As String) As Boolean

        Dim Bal As New BalProcesso_Perito(GetUsuario)
        Dim BalEnviarEmail As New BalEmail(GetUsuario)
        Dim p_To As String = String.Empty
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

        Dim m_Num_Oficio As String

        If txtID_PF.Text = "" Then
            MsgErro("Falha no envio do email. Perito não localizado")
            Exit Function
        End If
        ''logger.Debug("Bal.ExibirNumOficio(" & txtNum_CNJ.Text & "," & txtID_PF.Text & ")")


        If CboEspecialidade.SelectedIndex <= 0 Then
            m_Cod_Especialidade = 0
        Else
            m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
        End If

        'm_Num_Oficio = Bal.ExibirNumOficio(txtNum_CNJ.Text, txtID_PF.Text)
        m_Num_Oficio = Bal.ExibirNumOficio(txtNum_CNJ.Text, txtID_PF.Text, m_Cod_Especialidade)

        Dim txtBrasao As String
        ''logger.Debug("Converte imagem e bytes")
        txtBrasao = ToBase64(ConvertImageFiletoBytes("Imagens/Brasao_oficial.GIF")) 'ToBase64(ByVal data() As Byte) As String
        ''logger.Debug("Monta corpo do email")

        '"<img alt='Brasao' src='data:image/jpeg;base64," & txtBrasao & "style='width: 69px; height: 78px' border='0' id='idImagem' />" & _

        EmailHTML = "<html><body><table id='CorpoEmail' border='0' width='100%'>" & _
        "<tr><td><table id='Cabecalho' width='100%' border='0'><tr><td><center>"

        'JavaScript

        'Dim Msg As String
        'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "msg", "alert(navigator.appCodeName);", True)
        'ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "msg", "alert(navigator.appVersion);", True)
        'MsgErro(Msg)
        '''''''''''''''''''''
        'Dim s As String = ""
        'With Request.Browser
        '    s &= "Browser Capabilities" & vbCrLf
        '    s &= "Type = " & .Type & vbCrLf
        '    s &= "Name = " & .Browser & vbCrLf
        '    s &= "Version = " & .Version & vbCrLf
        '    s &= "Major Version = " & .MajorVersion & vbCrLf
        '    s &= "Minor Version = " & .MinorVersion & vbCrLf
        '    s &= "Platform = " & .Platform & vbCrLf
        '    s &= "Is Beta = " & .Beta & vbCrLf
        '    s &= "Is Crawler = " & .Crawler & vbCrLf
        '    s &= "Is AOL = " & .AOL & vbCrLf
        '    s &= "Is Win16 = " & .Win16 & vbCrLf
        '    s &= "Is Win32 = " & .Win32 & vbCrLf
        '    s &= "Supports Frames = " & .Frames & vbCrLf
        '    s &= "Supports Tables = " & .Tables & vbCrLf
        '    s &= "Supports Cookies = " & .Cookies & vbCrLf
        '    s &= "Supports VBScript = " & .VBScript & vbCrLf
        '    s &= "Supports JavaScript = " & _
        '        .EcmaScriptVersion.ToString() & vbCrLf
        '    s &= "Supports Java Applets = " & .JavaApplets & vbCrLf
        '    s &= "Supports ActiveX Controls = " & .ActiveXControls & _
        '        vbCrLf
        'End With
        'MsgErro(s)
        ' ''''''''''''''''''''''''

        'Dim m_Type, m_Browser, m_Version As String
        'm_Type = Request.Browser.Type
        'm_Browser = Request.Browser.Browser
        'm_Version = Request.Browser.Version

        'Dim userAgent As String
        'userAgent = Request.UserAgent
        'If userAgent.IndexOf("MSIE 7") > -1 Then    ' The browser is Microsoft Internet Explorer 7...
        '    EmailHTML = EmailHTML + " <img alt='Brasao' src='http://hwebserver.tjrj.jus.br/hpericias/Imagens/Brasao_oficial.GIF'style='width: 69px; height: 78px' border='0' id='idImagem' />"
        'Else
        '    EmailHTML = EmailHTML + " <img alt='Brasao' src='data:image/jpeg;base64," & txtBrasao & "' style='width: 69px; height: 78px' border='0' id='idImagem' />"
        '    'EmailHTML = EmailHTML + " <img alt='Brasao' src='data:image/jpeg;base64," & txtBrasao & "' style='width: 69px; height: 78px' border='0' id='idImagem' />"
        'End If

        'EmailHTML = EmailHTML + "</center></td></tr> <tr><td>" & _
        '" <center><b style='font-size: small'>TRIBUNAL DE JUSTI&Ccedil;A DO ESTADO DO RIO DE JANEIRO</b><br />" & _
        '" <b style='font-size: x-small'>Diretoria Geral de Apoio aos &Oacute;rg&atilde;os Jurisdicionais<br />" & _
        '" Departamento de Instrução Processual<br />" & _
        '" Divis&atilde;o Per&iacute;cias Judiciais" & _
        '" </center></td></tr><tr> <td></td></tr></table></td></tr><tr><td>" & _
        '" <table id='SubCabecalho' border='0' width='100%'>" & _
        '" <tr><td align='left'> <b>Ofício DIPEJ-GAB n&deg " & m_Num_Oficio & " </b></td>" & _
        '" <td align='right'><b>Rio de Janeiro, " & Now.Day.ToString & " de " & MonthName(Now.Month, False).ToString & " de " & Now.Year.ToString() & " </b> <br /></td></tr><tr><td></td></tr></table>"

        EmailHTML = EmailHTML + "</center></td></tr> <tr><td>" & _
        " <center><b style='font-size: small'>TRIBUNAL DE JUSTI&Ccedil;A DO ESTADO DO RIO DE JANEIRO</b><br />" & _
        " <b style='font-size: x-small'>DIRETORIA GERAL DE APOIO AOS &Oacute;RG&Atilde;OS JURIDISCIONAIS<br />" & _
        " DEPARTAMENTO DE INSTRU&Ccedil;&Atilde;O PROCESSUAL<br />" & _
        " DIVIS&Atilde;O DE PER&Iacute;CIAS JUDICIAIS" & _
        " </center></td></tr><tr> <td></td></tr></table></td></tr><tr><td>" & _
        " <table id='SubCabecalho' border='0' width='100%'>" & _
        " <tr><td align='left'> <b>Ofício DIPEJ-GAB n&deg " & m_Num_Oficio & " </b></td>" & _
        " <td align='right'><b>Rio de Janeiro, " & Now.Day.ToString & " de " & MonthName(Now.Month, False).ToString & " de " & Now.Year.ToString() & " </b> <br /></td></tr><tr><td></td></tr></table>"


        '        EmailHTML = "<html><body><table id='CorpoEmail' border='0' width='100%'>" & _
        '"<tr><td><table id='Cabecalho' width='100%' border='0'><tr><td><center>" & _
        '"<img alt='Brasao' src='http://hwebserver.tjrj.jus.br/hpericias/Imagens/Brasao_oficial.GIF'style='width: 69px; height: 78px' border='0' id='idImagem' />" & _
        '"</center></td></tr> <tr><td>" & _
        '"<center><b style='font-size: small'>TRIBUNAL DE JUSTI&Ccedil;A DO ESTADO DO RIO DE JANEIRO</b><br />" & _
        '"<b style='font-size: x-small'>Diretoria Geral de Apoio aos &Oacute;rg&atilde;os Jurisdicionais<br />" & _
        '"Departamento de Instrução Processual<br />" & _
        '"Divis&atilde;o Per&iacute;cias Judiciais" & _
        '"</center></td></tr><tr> <td></td></tr></table></td></tr><tr><td>" & _
        '"<table id='SubCabecalho' border='0' width='100%'>" & _
        '"<tr><td align='left'> <b>Ofício DIPEJ-GAB n&deg " & m_Num_Oficio & " </b></td>" & _
        '"<td align='right'><b>Rio de Janeiro, " & Now.Day.ToString & " de " & MonthName(Now.Month, False).ToString & " de " & Now.Year.ToString() & " </b> <br /></td></tr><tr><td></td></tr></table>"


        EmailHTML = EmailHTML + "<br/> <u> Refer&ecirc;ncia </u>: Processo Judicial n&deg " + txtNum_CNJ.Text + "(" + txtNum_Processo.Text + ")" & _
        "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td><b><br/>" & _
        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & _
        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp Prezado Senhor,"

        'EmailHTML = EmailHTML + "</b></center><br /></td></tr><tr><td align='justify' colspan='0'>" & _
        '"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & _
        '"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sirvo-me do presente para intim&aacute;-lo " & _
        '"de sua nomea&ccedil&atilde;o pelo Exmo. Sr. Juiz de Direito da </span>"

        'EmailHTML = EmailHTML + "</b></center><br /></td></tr><tr><td align='justify' colspan='0'>" & _
        '"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & _
        '"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp Sirvo-me do presente para comunic&aacute;-lo " & _
        '"de sua nomea&ccedil&atilde;o pelo Exmo. Sr. Juiz de Direito da </span>"
        EmailHTML = EmailHTML + "</b></center><br /></td></tr><tr><td align='justify' colspan='0'>" & _
       "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & _
       "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp Sirvo-me do presente para comunicá-lo " & _
       "de sua nomeação pelo Exmo. Sr. Juiz de Direito da </span>"


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
        Dim m_Descr_Comarca1 As String
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
        ''logger.Debug("BalComarca.ExibirDadosServentia(" & GetUsuario.CodOrg & ")")
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

        If (Not Session("Cod_Profissao") Is Nothing) And (Not Session("Cod_Especialidade") Is Nothing) Then
            ''logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & txtID_PF.Text & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & ")")
            DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, CInt(txtID_PF.Text), Session("Cod_Profissao").ToString, Session("Cod_Especialidade").ToString, CDate(lblData_Nomeacao.Text))
        Else
            ''logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & txtID_PF.Text & ", ,)")
            DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, CInt(txtID_PF.Text), "", "", CDate(lblData_Nomeacao.Text))
        End If

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

        'If m_Descr_Comarca <> "" Then
        '    m_Descr_Comarca.Replace("º", "&ordm;").Trim()
        '    m_Descr_Comarca.Replace("ª", "&ordf;").Trim()
        '    m_Descr_Comarca.Replace("Ó", "&Oacute;").Trim()
        '    m_Descr_Comarca.Replace("ó", "&oacute;").Trim()
        '    m_Descr_Comarca.Replace("ã", "&atilde;").Trim()
        '    m_Descr_Comarca.Replace("Ã", "&Atilde;").Trim()
        '    m_Descr_Comarca.Replace("ç", "&ccedil;").Trim()
        '    m_Descr_Comarca.Replace("Ç", "&Ccedil;").Trim()
        '    m_Descr_Comarca.Replace("á", "&aacute;").Trim()
        '    m_Descr_Comarca.Replace("Á", "&Aacute;").Trim()
        '    m_Descr_Comarca.Replace("í", "&iacute;").Trim()
        '    m_Descr_Comarca.Replace("Í", "&Iacute;").Trim()
        '    m_Descr_Comarca.Replace("u", "&uacute;").Trim()
        '    m_Descr_Comarca.Replace("Ú", "&Uacute;").Trim()
        'End If
        'If m_Descr_Especialidade <> "" Then
        '    m_Descr_Especialidade.Replace("º", "&ordm;").Trim()
        '    m_Descr_Especialidade.Replace("º", "&ordm;").Trim()
        '    m_Descr_Especialidade.Replace("ª", "&ordf;")
        '    m_Descr_Especialidade.Replace("Ó", "&Oacute;").Trim()
        '    m_Descr_Especialidade.Replace("ó", "&oacute;").Trim()
        '    m_Descr_Especialidade.Replace("ã", "&atilde;").Trim()
        '    m_Descr_Especialidade.Replace("Ã", "&Atilde;").Trim()
        '    m_Descr_Especialidade.Replace("ç", "&ccedil;").Trim()
        '    m_Descr_Especialidade.Replace("Ç", "&Ccedil;").Trim()
        '    m_Descr_Especialidade.Replace("á", "&aacute;").Trim()
        '    m_Descr_Especialidade.Replace("Á", "&Aacute;").Trim()
        '    m_Descr_Especialidade.Replace("í", "&iacute;").Trim()
        '    m_Descr_Especialidade.Replace("Í", "&Iacute;").Trim()
        '    m_Descr_Especialidade.Replace("u", "&uacute;").Trim()
        '    m_Descr_Especialidade.Replace("Ú", "&Uacute;").Trim()
        'End If
        'If m_Descr_Profissao <> "" Then
        '    m_Descr_Profissao.Replace("º", "&ordm;").Trim()
        '    m_Descr_Profissao.Replace("ª", "&ordf;").Trim()
        '    m_Descr_Profissao.Replace("Ó", "&Oacute;").Trim()
        '    m_Descr_Profissao.Replace("ó", "&oacute;").Trim()
        '    m_Descr_Profissao.Replace("ã", "&atilde;").Trim()
        '    m_Descr_Profissao.Replace("Ã", "&Atilde;").Trim()
        '    m_Descr_Profissao.Replace("ç", "&ccedil;").Trim()
        '    m_Descr_Profissao.Replace("Ç", "&Ccedil;").Trim()
        '    m_Descr_Profissao.Replace("á", "&aacute;").Trim()
        '    m_Descr_Profissao.Replace("Á", "&Aacute;").Trim()
        '    m_Descr_Profissao.Replace("í", "&iacute;").Trim()
        '    m_Descr_Profissao.Replace("Í", "&Iacute;").Trim()
        '    m_Descr_Profissao.Replace("u", "&uacute;").Trim()
        '    m_Descr_Profissao.Replace("Ú", "&Uacute;").Trim()
        'End If
        'If m_Descr_Serventia <> "" Then
        '    m_Descr_Serventia.Replace("º", "&ordm;").Trim()
        '    m_Descr_Serventia.Replace("ª", "&ordf;").Trim()
        '    m_Descr_Serventia.Replace("Ó", "&Oacute;").Trim()
        '    m_Descr_Serventia.Replace("ó", "&oacute;").Trim()
        '    m_Descr_Serventia.Replace("ã", "&atilde;").Trim()
        '    m_Descr_Serventia.Replace("Ã", "&Atilde;").Trim()
        '    m_Descr_Serventia.Replace("ç", "&ccedil;").Trim()
        '    m_Descr_Serventia.Replace("Ç", "&Ccedil;").Trim()
        '    m_Descr_Serventia.Replace("á", "&aacute;").Trim()
        '    m_Descr_Serventia.Replace("Á", "&Aacute;").Trim()
        '    m_Descr_Serventia.Replace("í", "&iacute;").Trim()
        '    m_Descr_Serventia.Replace("Í", "&Iacute;").Trim()
        '    m_Descr_Serventia.Replace("u", "&uacute;").Trim()
        '    m_Descr_Serventia.Replace("Ú", "&Uacute;").Trim()
        '    m_Descr_Serventia.Replace(m_Descr_Comarca, "").Trim()
        'End If

        If m_Descr_Serventia <> "" Then
            '    m_Descr_Serventia.Replace("º", "&ordm;").Trim()
            '    m_Descr_Serventia.Replace("ª", "&ordf;").Trim()
            '    m_Descr_Serventia.Replace("Ó", "&Oacute;").Trim()
            '    m_Descr_Serventia.Replace("ó", "&oacute;").Trim()
            '    m_Descr_Serventia.Replace("ã", "&atilde;").Trim()
            '    m_Descr_Serventia.Replace("Ã", "&Atilde;").Trim()
            '    m_Descr_Serventia.Replace("ç", "&ccedil;").Trim()
            '    m_Descr_Serventia.Replace("Ç", "&Ccedil;").Trim()
            '    m_Descr_Serventia.Replace("á", "&aacute;").Trim()
            '    m_Descr_Serventia.Replace("Á", "&Aacute;").Trim()
            '    m_Descr_Serventia.Replace("í", "&iacute;").Trim()
            '    m_Descr_Serventia.Replace("Í", "&Iacute;").Trim()
            '    m_Descr_Serventia.Replace("u", "&uacute;").Trim()
            '    m_Descr_Serventia.Replace("Ú", "&Uacute;").Trim()
            m_Descr_Comarca1 = m_Descr_Comarca.Replace("Comarca de ", " ").Trim()
            m_Descr_Comarca1 = m_Descr_Comarca1.Replace("Comarca da", " ").Trim()
            If m_Descr_Comarca1 <> "" Then
                m_Descr_Serventia = m_Descr_Serventia.Replace(m_Descr_Comarca1, " ").Trim()
                m_Descr_Serventia = m_Descr_Serventia.Replace(UCase(m_Descr_Comarca1), " ").Trim()
            End If
        End If

        'logger.Debug("Atualiza corpo do email")
        'EmailHTML = EmailHTML + m_Descr_Serventia & " da " & m_Descr_Comarca & " , para atuar como perito daquele respeit&aacute;vel " & _
        '"Ju&iacute;zo nos autos do Processo em refer&ecirc;ncia. Vossa Senhoria deverá manifestar-se nos termos do parágrafo único do artigo 146 do CPC , " & _
        '"bem como apresentar proposta de honor&aacute;rios."

        EmailHTML = EmailHTML + m_Descr_Serventia & " da " & m_Descr_Comarca & " , para atuar como perito daquele respeitável " & _
        "Juízo nos autos do Processo em referência. Vossa Senhoria deverá manifestar-se nos termos do parágrafo único do artigo 146 do CPC , " & _
        "bem como apresentar proposta de honorários."

        'EmailHTML = EmailHTML + m_Descr_Serventia & " da " & m_Descr_Comarca & " , para atuar como perito daquele respeit&aacute;vel " & _
        '"Ju&iacute;zo nos autos do Processo em ep&iacute;grafe, devendo V.Sa. se manifestar " & _
        '"quanto à aceita&ccedil;&atilde;o ou recusa do encargo bem como apresentar " & _
        '"proposta de honor&aacute;rios, concomitantemente, diretamente aquele ju&iacute;zo.(ou neste sistema)."

        EmailHTML = EmailHTML + "</td></tr><tr><td><center><br />Atenciosamente.</center></td></tr>" & _
        "<tr><td><center><br /><b>MÁRCIO MARCELO DA SILVA OLIVEIRA</b><br />" & _
        "Diretor da Divisão de Perícias Judiciais</center></td></tr>"

        EmailHTML = EmailHTML + "<tr><td align='left'>Ilmo. Sr. " & txtNome_Perito.Text & ".<br />" & _
        "(" & m_Descr_Profissao & "/ " & m_Descr_Especialidade & ") " & "</td>"

        EmailHTML = EmailHTML + "</tr></table></body></html>"

        If EmailHTML <> "" Then
            EmailHTML = EmailHTML.Replace("º", "&ordm;").Trim()
            EmailHTML = EmailHTML.Replace("ª", "&ordf;").Trim()
            EmailHTML = EmailHTML.Replace("Ó", "&Oacute;").Trim()
            EmailHTML = EmailHTML.Replace("ó", "&oacute;").Trim()
            EmailHTML = EmailHTML.Replace("ã", "&atilde;").Trim()
            EmailHTML = EmailHTML.Replace("Ã", "&Atilde;").Trim()
            EmailHTML = EmailHTML.Replace("ç", "&ccedil;").Trim()
            EmailHTML = EmailHTML.Replace("Ç", "&Ccedil;").Trim()
            EmailHTML = EmailHTML.Replace("á", "&aacute;").Trim()
            EmailHTML = EmailHTML.Replace("Á", "&Aacute;").Trim()
            EmailHTML = EmailHTML.Replace("í", "&iacute;").Trim()
            EmailHTML = EmailHTML.Replace("Í", "&Iacute;").Trim()
            EmailHTML = EmailHTML.Replace("ú", "&uacute;").Trim()
            EmailHTML = EmailHTML.Replace("Ú", "&Uacute;").Trim()
        End If

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
        If UCase(GetUsuario.Login) = "CRISTIANESOUSA" Then
            m_Email = "cristianesousa@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "MYLENAROCHA" Then
            m_Email = "Mylenarocha@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "DANIELAFMO" Then
            m_Email = "danielafmo@tjrj.jus.br"
        End If
        If UCase(GetUsuario.Login) = "FRANCIANASANTOS" Then
            m_Email = "francianasantos@tjrj.jus.br"
        End If

        p_Cc = ""
        p_From_Nome = "DGJUR/DEINP/DIPEJ"
        'Trocar para email da DIPEJ

        p_Subject = "Indicação do Perito no Processo"
        p_Html = EmailHTML

        p_From_Address = App.AppSettings("FromAddressNomeacao").ToString()
        'p_From_Address = "DGJUR/DEINP/DIPEJ"

        p_Smtp_Hostname = App.AppSettings("SmtpHostname").ToString()
        'p_Smtp_Hostname = "mail.tjrj.jus.br"

        p_Smtp_Portnum = App.AppSettings("SmtpPortnum").ToString()
        'p_Smtp_Portnum = "25"

        Dim bEnviado As Boolean

        If m_Email <> "" Then
            ' 'logger.Debug("BalEnviarEmail.EnviarEmail(" & m_Email & "," & p_Cc & "," & p_From_Nome & "," & p_From_Address & "," & p_Subject & "," & p_Html & "," & p_Smtp_Hostname & "," & p_Smtp_Portnum & ")")
            BalEnviarEmail.EnviarEmail(m_Email, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
            bEnviado = True
        Else
            ' 'logger.Debug("E-mail de teste não foi enviado.")
            'If Not bEnviado Then
            '    bEnviado = False
            'End If
        End If

        If m_Email1 <> "" Then
            ''logger.Debug("BalEnviarEmail.EnviarEmail(" & m_Email1 & "," & p_Cc & "," & p_From_Nome & "," & p_From_Address & "," & p_Subject & "," & p_Html & "," & p_Smtp_Hostname & "," & p_Smtp_Portnum & ")")
            BalEnviarEmail.EnviarEmail(m_Email1, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
            bEnviado = True
        Else
            'logger.Debug("E-mail alteranativo de teste não foi enviado.")
            'If Not bEnviado Then
            '    bEnviado = False
            'End If
        End If
        'If lblEmail1.Text <> "" Then
        '    'logger.Debug("BalEnviarEmail.EnviarEmail(" & lblEmail1.Text & "," & p_Cc & "," & p_From_Nome & "," & p_From_Address & "," & p_Subject & "," & p_Html & "," & p_Smtp_Hostname & "," & p_Smtp_Portnum & ")")
        '    BalEnviarEmail.EnviarEmail(lblEmail1.Text, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        '    bEnviado = True
        'Else
        '    'logger.Debug("E-mail alternativo teste não foi enviado.")
        '    If Not bEnviado Then
        '        bEnviado = False
        '    End If
        'End If
        Return bEnviado
    End Function

    Protected Sub chkLaudoLiberado_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkLaudoLiberado.CheckedChanged

        'logger.Debug(" chkLaudoLiberado_CheckedChanged ...")

        If chkLaudoLiberado.Checked Then
            'ChkCancelaPgto.Enabled = True
            If lblData_Liberacao.Text = "" Then
                lblData_Liberacao.Text = Today.ToShortDateString
            End If
        Else
            'ChkCancelaPgto.Enabled = False
            lblData_Liberacao.Text = ""
        End If

    End Sub

    Protected Sub GrdProcPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdProcPerito.SelectedIndexChanged
        'logger.Debug("GrdProcPerito_SelectedIndexChanged ...")
        Dim m_ID_PF As Long
        Dim m_Descr_Especialidade As String
        Dim m_Descr_Profissao As String
        Dim DsProcPer1 As DataSet
        Dim BalProcPer1 As New BalProcesso_Perito(GetUsuario)

        'Dim Acento As HttpServerUtility

        LimparInformacoesPerito()

        m_ID_PF = CInt(GrdProcPerito.SelectedRow.Cells(1).Text)
        m_Descr_Especialidade = Trim(HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(5).Text))
        m_Descr_Profissao = Trim(HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(4).Text))

        If m_Descr_Profissao <> "" Then
            If Not CboProfissao.Items.FindByText(UCase(m_Descr_Profissao)) Is Nothing Then
                CboProfissao.SelectedValue = CboProfissao.Items.FindByText(UCase(m_Descr_Profissao)).Value
                m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
                Session("Cod_profissao") = m_Cod_Profissao
            Else
                CboProfissao.SelectedIndex = 0
            End If
        End If

        'logger.Debug("PreencherEspecialidade(" & m_Cod_Profissao & ")")
        PreencherEspecialidade(m_Cod_Profissao)

        If m_Descr_Especialidade <> "" Then
            If Not CboEspecialidade.Items.FindByText(UCase(m_Descr_Especialidade)) Is Nothing Then
                CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByText(UCase(m_Descr_Especialidade)).Value
                Session("Cod_especialidade") = CboEspecialidade.SelectedValue
            Else
                CboEspecialidade.SelectedIndex = 0
            End If
        Else
            CboEspecialidade.SelectedIndex = 0 'Generico
        End If

        txtID_PF.Text = GrdProcPerito.SelectedRow.Cells(1).Text
        lblData_Nomeacao.Text = GrdProcPerito.SelectedRow.Cells(3).Text

        chkLaudoLiberado.Enabled = True

        '------------------------------------------
        If txtID_PF.Text <> "" Then
            m_ID_PF = CInt(txtID_PF.Text)
        Else
            m_ID_PF = 0
        End If
        If (Not Session("Cod_Profissao") Is Nothing) And (Not Session("Cod_Especialidade") Is Nothing) Then
            If lblData_Nomeacao.Text = "" Then
                'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & "," & Today.ToShortDateString & ")")
                DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, Session("Cod_Profissao").ToString, IIf(Session("Cod_Especialidade").ToString = "GENÉRICO", "", Session("Cod_Especialidade").ToString).ToString, CDate(Today.ToShortDateString))
            Else
                'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & "," & lblData_Nomeacao.Text & ")")
                DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, Session("Cod_Profissao").ToString, IIf(Session("Cod_Especialidade").ToString = "GENÉRICO", "", Session("Cod_Especialidade").ToString).ToString, CDate(lblData_Nomeacao.Text))
            End If
        Else
            If lblData_Nomeacao.Text = "" Then
                'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Today.ToShortDateString & ")")
                DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "", CDate(Today.ToShortDateString))
            Else
                'logger.Debug("BalProcPer1.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & lblData_Nomeacao.Text & ")")
                DsProcPer1 = BalProcPer1.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "", CDate(lblData_Nomeacao.Text))
            End If
        End If
        '------------------------------------------
        'logger.Debug("ExibirDadosPerito(DsProcPer1)")
        ExibirDadosPerito(DsProcPer1)

        If lblData_Inicio.Text <> "" Then
            Bloquear()
        Else
            DesBloquear()
        End If

        If lblData_Negacao.Text <> "" Then
            BtnInicio.Enabled = False
            txtHonJuiz.Enabled = False
            txtPrazo.Enabled = False
        Else
            If lblData_Inicio.Text <> "" Then
                BtnInicio.Enabled = False
                txtHonJuiz.Enabled = False
                txtPrazo.Enabled = False
            Else
                If Not NovoHonFixo Then
                    BtnInicio.Enabled = True
                End If
                If lblData_Seg_Aceitacao.Text <> "" Then
                    txtHonJuiz.Enabled = False
                    txtPrazo.Enabled = False
                Else
                    txtHonJuiz.Enabled = True
                    txtPrazo.Enabled = True
                End If
            End If
        End If
        If lblData_Negacao.Text <> "" Or lblData_Inicio.Text <> "" Or txtHonPer.Text <> "" Then
            RdbJustGrat.Enabled = False
        Else
            RdbJustGrat.Enabled = True
        End If
        BtnEmailNomeacao.Enabled = True
        If UCase(CboProfissao.SelectedItem.Text) = "MEDICO" Or UCase(CboProfissao.SelectedItem.Text) = "MÉDICO" Then
            ChkPerInter.Visible = True
        Else
            ChkPerInter.Visible = False
        End If

    End Sub

    Protected Sub GrdProcPerito_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)

        GrdProcPerito.PageIndex = e.NewPageIndex
        PreencherProcPerito(txtNum_CNJ.Text)

    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        Dim Medico As Boolean = False
        If txtNum_Processo.Text = "" Or txtNum_CNJ.Text = "" Then
            MsgErro("Informe o número do processo")
            HabilitaCampos(False)
            txtNum_Processo.Focus()
            Exit Sub
        End If

        'logger.Debug("CboProfissao_SelectedIndexChanged ...")
        txtNome_Perito.Text = ""
        Session("ID") = Nothing
        Session("Msg") = Nothing

        If CboProfissao.Items.Count > 0 And CboProfissao.SelectedIndex <> 0 Then
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
            'logger.Debug("PreencherEspecialidade(" & m_Cod_Profissao & ")")
            PreencherEspecialidade(m_Cod_Profissao)
            '-------------------------------- Limpeza Parcial
            BtnPerito.Text = "Pesquisar Perito"
            NovoHonFixo = False
            lblAceitacao_Tacita.Visible = False
            lblData_Nomeacao.Text = ""
            lblData_Novo_Hon.Text = ""
            txtPrazo.Enabled = False
            txtNome_Perito.Text = ""
            lblData_Cadastramento.Text = ""
            lblEmail.Text = ""
            lblEmail1.Text = ""
            txtQteAceitos.Text = ""
            txtQtePendentes.Text = ""
            txtPrazo.Text = ""
            txtAnotacao.Text = ""
            txtAnotacao.Visible = False
            LblAnotacao.Visible = False
            lblData_Aceitacao.Text = ""
            lblData_Negacao.Text = ""
            txtHonJuiz.Text = ""
            txtHonPer.Text = ""
            ChkPerInter.Checked = False
            lblData_Inicio.Text = ""
            txtQteRecusadas.Text = ""
            RdbJustGrat.ClearSelection()
            HabilitaAceiteLaudo(False)
            BtnEmailNomeacao.Enabled = False
            BtnGravar.Enabled = False
            BtnAnotacao.Enabled = False
            lblMotivo_Recusa.Visible = False
            txtMotivo_Recusa.Visible = False
            txtMotivo_Recusa.Text = ""
            ChkPerInter.Visible = False
            lblData_Seg_Aceitacao.Text = ""
            '--------------------------------
        Else
            MsgErro("Selecione uma Profissão")
            LimparInformacoesPerito()
            Exit Sub
        End If
        If UCase(Trim(CboProfissao.SelectedItem.Text)) = "MEDICO" Or UCase(Trim(CboProfissao.SelectedItem.Text)) = "MÉDICO" Then
            Medico = True
        Else
            Medico = False
        End If

        If Medico Then
            ChkPerInter.Visible = True
        Else
            ChkPerInter.Visible = False
        End If

    End Sub

    Protected Sub txtNum_CNJ1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ1.TextChanged
        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
        NovoHonFixo = False
        If txtNum_CNJ1.Text.Length < 13 Then
            txtNum_CNJ1.Text = ""
        Else
            txtNum_CNJ2.Focus()
        End If
        'BtnVerCNJ.Enabled = False
    End Sub

    Protected Sub txtNum_CNJ2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ2.TextChanged
        'logger.Debug("txtNum_CNJ2_TextChanged ...")
        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
        'logger.Debug("AtualizatxtNum_CNJ()")
        AtualizatxtNum_CNJ()
        'BtnVerCNJ.Enabled = False

    End Sub

    Protected Sub AtualizatxtNum_CNJ()
        'logger.Debug("AtualizatxtNum_CNJ() ...")
        Dim Ent As EntPROC_CNJ
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim mm_Cod_Org As Integer

        If Len(txtNum_CNJ1.Text) = 15 Then
            txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 15) + ".8.19." + txtNum_CNJ2.Text
        Else
            txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 7) + "-" + Mid(txtNum_CNJ1.Text, 8, 2) + "." + Mid(txtNum_CNJ1.Text, 10, 4) + ".8.19." + txtNum_CNJ2.Text
        End If

        'logger.Debug("ValidaNumCNJ(" & txtNum_CNJ.Text & ")")
        If Not ValidaNumCNJ(txtNum_CNJ.Text) Then
            MsgErro("Número CNJ Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_Processo.Text = ""
            HabilitaCampos(False)
            txtNum_Processo.Focus()
            Exit Sub
        End If

        'logger.Debug("BalP.ExibirDadosEnt(" & txtNum_Processo.Text & "," & txtNum_CNJ.Text & ")")
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        Dim ProcServ As Boolean
        If Not Ent Is Nothing Then
            mm_Cod_Org = GetUsuario.CodOrg
            'ProcServ = BalP.VerificarProc_Serventia(txtNum_Processo.Text, Ent.Cod_CNJ, GetUsuario.CodOrg)
            ProcServ = BalP.VerificarProc_Serventia(txtNum_Processo.Text, Ent.Cod_CNJ, mm_Cod_Org)
            If Not ProcServ Then
                MsgErro("Processo não pertence a esta Serventia")
                txtNum_CNJ.Text = ""
                txtNum_CNJ1.Text = ""
                txtNum_CNJ2.Text = ""
                txtNum_Processo.Text = ""
            Else
                If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
                If txtNum_CNJ.Text = "" Then txtNum_CNJ.Text = Ent.Cod_CNJ
                'logger.Debug("ValidaNumCNJ(" & Ent.Cod_CNJ & ")")

                If ValidaNumCNJ(Ent.Cod_CNJ) Then
                    'logger.Debug("PreencherProcPerito(" & Ent.Cod_CNJ & ")")
                    PreencherProcPerito(Ent.Cod_CNJ) '(GrdProcPerito)
                    HabilitaCampos(True)
                Else
                    MsgErro("Número de CNJ Inválido")
                    txtNum_CNJ1.Text = ""
                    txtNum_CNJ2.Text = ""
                    txtNum_Processo.Text = ""
                    HabilitaCampos(False)
                    txtNum_Processo.Focus()
                End If
            End If
        Else
            MsgErro("Número de CNJ Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_Processo.Text = ""
            txtNum_Processo.Focus()
        End If
        '*************************************************************
        'Verificar se o processo, no momento, pertence ao Juízo atual
        '*************************************************************

    End Sub

    Protected Sub BtnPerito_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPerito.Click

        'logger.Debug("BtnPerito_Click ...")
        Session("Msg") = Nothing

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        '---
        'LimparInformacoesPerito()
        '---
        If CboProfissao.SelectedIndex = 0 Then
            MsgErro("Selecione a profissão do perito.")
            CboProfissao.Focus()
            Exit Sub
        End If

        If CboEspecialidade.SelectedIndex = 0 Then
            MsgErro("Selecione a especialidade do perito.")
            CboEspecialidade.Focus()
            Exit Sub
        End If

        If txtNum_CNJ.Text = "" Then
            MsgErro("Informe o número do processo.")
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
            If CboEspecialidade.SelectedIndex <= 0 Then
                m_Cod_Especialidade = 0
            Else
                m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            End If
            Session("Cod_Profissao") = m_Cod_Profissao
            Session("Cod_Especialidade") = m_Cod_Especialidade
            Session("Num_Processo1") = txtNum_CNJ1.Text
            Session("Num_Processo2") = txtNum_CNJ2.Text
            Session("Num_CNJ") = txtNum_CNJ.Text

            'logger.Debug("m_Cod_Profissao: " & m_Cod_Profissao)
            'logger.Debug("m_Cod_Especialidade: " & m_Cod_Especialidade)
            'logger.Debug("txtNum_CNJ1.Text: " & txtNum_CNJ1.Text)
            'logger.Debug("txtNum_CNJ2.Text: " & txtNum_CNJ2.Text)
            'logger.Debug("Redireciona para frmEscolherPerito.aspx")
            Response.Redirect("frmEscolherPerito.aspx")
        End If

    End Sub

    Private Sub Bloquear()

        lblData_Nomeacao.Enabled = False
        txtPrazo.Enabled = False
        BtnPerito.Enabled = False
        BtnEmailNomeacao.Enabled = False
        'ChkPerInter.Enabled = False
        BtnInicio.Enabled = False
        'CboProfissao.SelectedIndex = 0
        'CboEspecialidade.SelectedIndex = CInt(IIf(CboEspecialidade.SelectedIndex <> -1, 0, -1))
        txtNome_Perito.Enabled = False
        txtPrazo.Enabled = False
        txtHonJuiz.Enabled = False
        'ChkPerInter.Enabled = False
        RdbJustGrat.Enabled = False

        HabilitaAceiteLaudo(True)

    End Sub
    Private Sub DesBloquear()

        lblData_Nomeacao.Enabled = True
        txtPrazo.Enabled = True
        BtnPerito.Enabled = True
        'BtnEmailNomeacao.Enabled = True
        ChkPerInter.Enabled = True
        If Not NovoHonFixo Then
            BtnInicio.Enabled = True
        End If
        ''CboProfissao.SelectedIndex = 0
        ''CboEspecialidade.SelectedIndex = CInt(IIf(CboEspecialidade.SelectedIndex <> -1, 0, -1))
        txtNome_Perito.Enabled = True
        txtPrazo.Enabled = True
        txtHonJuiz.Enabled = True
        ChkPerInter.Enabled = True
        RdbJustGrat.Enabled = True

        HabilitaAceiteLaudo(False)

    End Sub

    Private Sub HabilitaAceiteLaudo(ByVal bValor As Boolean)
        'logger.Debug("HabilitaAceiteLaudo(" & bValor & ")")
        chkLaudoLiberado.Visible = bValor
        lblData_Liberacao.Visible = bValor
        'lblDescDtLiberacao.Visible = bValor
        If bValor = False Then
            chkLaudoLiberado.Checked = False
            ' lblData_Aceitacao.Text = ""
        End If
        tbAceiteLaudo.Visible = bValor
    End Sub

    Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged
        If CboEspecialidade.SelectedIndex <= 0 Then
            LimparInformacoesPerito()
        End If

    End Sub

    Protected Sub txtNome_Perito_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome_Perito.TextChanged
        If txtNome_Perito.Text <> "" And RdbJustGrat.SelectedValue <> "" And txtPrazo.Text <> "" Then
            BtnGravar.Enabled = True
        End If
    End Sub

    Private Sub HabilitaCampos(ByVal bativo As Boolean)
        CboEspecialidade.Enabled = bativo
        CboProfissao.Enabled = bativo
        BtnPerito.Enabled = bativo
        BtnGravar.Enabled = bativo
        BtnEmailNomeacao.Enabled = bativo
        BtnAnotacao.Enabled = bativo
        'ChkJustGrat.Enabled = bativo
        RdbJustGrat.Enabled = bativo
        txtPrazo.Enabled = bativo
    End Sub


    Protected Sub BtnInicio_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnInicio.Click
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)

        'Inserir Data Nomeacao -> tb no atualiza envio de email
        'CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByText(UCase(CboEspecialidade.Text)).Value
        'CboEspecialidade.SelectedValue = CboEspecialidade.Text
        m_Cod_Especialidade = CInt(CboEspecialidade.Text)

        'CboProfissao.SelectedValue = CboProfissao.Items.FindByText(UCase(CboProfissao.Text)).Value
        m_Cod_Profissao = CInt(CboProfissao.Text)

        m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        If txtNum_CNJ.Text <> "" And txtID_PF.Text <> "" Then
            BalProcPer.AtualizaProcessoPeritoInicioPer(txtNum_CNJ.Text, CLng(txtID_PF.Text), m_Cod_Profissao, m_Cod_Especialidade, Today.ToShortDateString, lblData_Nomeacao.Text)
            lblData_Inicio.Text = Today.ToShortDateString
        End If
        txtHonJuiz.Enabled = False
        'MsgErro("Perícia Iniciada")
    End Sub

    Protected Sub RdbJustGrat_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdbJustGrat.SelectedIndexChanged
        'txtHonJuiz.Enabled = False
        'If RdbJustGrat.SelectedValue = "N" And txtHonJuiz.Text <> "" Then
        '    txtHonJuiz.Enabled = False
        'End If
        If txtHonJuiz.Text <> "" Then
            txtHonJuiz.Enabled = False
        End If
        'If RdbJustGrat.SelectedValue = "S" Then
        '    txtHonJuiz.Enabled = False
        'End If
        If txtNome_Perito.Text <> "" And txtPrazo.Text <> "" Then
            BtnGravar.Enabled = True
        End If

    End Sub

    'Protected Sub BtnVerCNJ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnVerCNJ.Click
    Private Sub AtualizarDadosCNJ(ByVal nID_Nomeacao As Integer)
        Dim BalPCNJ As New BalProc_CNJ(GetUsuario)
        Dim BalProcPerito As New BalProcesso_Perito(GetUsuario)

        ' m_Num_Processo_N - Num_CNJ
        If nID_Nomeacao <> 0 Then
            m_Num_Processo_N = BalProcPerito.ExibirNumProc(nID_Nomeacao)
        Else
            m_Num_Processo_N = ""
        End If

        txtNum_Processo.Text = ""

        'If Session("Num_CNJ").ToString <> "" Then
        'm_Num_Processo_N = Session("Num_CNJ").ToString
        'End If

        If m_Num_Processo_N <> "" Then
            'logger.Debug("BalPCNJ.ExibirDadosEnt(" & txtNum_Processo.Text & "," & m_Num_Processo_N & ")")
            EntCNJ = BalPCNJ.ExibirDadosEnt(txtNum_Processo.Text, m_Num_Processo_N)
            If Not EntCNJ Is Nothing Then
                'If txtNum_Processo.Text = "" Then txtNum_Processo.Text = EntCNJ.Cod_Proc
                'If txtNum_CNJ1.Text = "" Then
                'txtNum_CNJ1.Text = Mid(EntCNJ.Cod_CNJ, 1, 7) + Mid(EntCNJ.Cod_CNJ, 9, 2) + Mid(EntCNJ.Cod_CNJ, 12, 4)
                'txtNum_CNJ2.Text = Mid(EntCNJ.Cod_CNJ, 22, 4)
                'txtNum_CNJ.Text = EntCNJ.Cod_CNJ
                'HabilitaCampos(True)
                'End If
                'End If
                txtNum_Processo.Text = EntCNJ.Cod_Proc
            End If
            txtNum_CNJ1.Text = Mid(m_Num_Processo_N, 1, 7) + Mid(m_Num_Processo_N, 9, 2) + Mid(m_Num_Processo_N, 12, 4)
            txtNum_CNJ2.Text = Mid(m_Num_Processo_N, 22, 4)
            txtNum_CNJ.Text = m_Num_Processo_N
        End If
        'logger.Debug("txtNum_Processo_TextChanged ...")

        Dim Ent As EntPROC_CNJ
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim Cod_CNJ_Valido As Boolean

        'logger.Debug("ValidaNumProc(" & txtNum_Processo.Text & ")")
        If Not ValidaNumProc(txtNum_Processo.Text) Then
            MsgErro("Número de Processo Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_CNJ.Text = ""
            HabilitaCampos(False)
            txtNum_Processo.Focus()
            Exit Sub
        End If

        'logger.Debug("BalP.ExibirDadosEnt(" & txtNum_Processo.Text & "," & txtNum_CNJ.Text & ")")
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ1.Text = "" Then
                txtNum_CNJ1.Text = Mid(Ent.Cod_CNJ, 1, 7) + Mid(Ent.Cod_CNJ, 9, 2) + Mid(Ent.Cod_CNJ, 12, 4)
                txtNum_CNJ2.Text = Mid(Ent.Cod_CNJ, 22, 4)
                txtNum_CNJ.Text = Ent.Cod_CNJ
                HabilitaCampos(True)
            End If

            'logger.Debug("ValidaNumCNJ(" & Ent.Cod_CNJ & ")")
            Cod_CNJ_Valido = ValidaNumCNJ(Ent.Cod_CNJ)
            If Cod_CNJ_Valido Then
                'logger.Debug("PreencherProcPerito(" & Ent.Cod_CNJ & ")")
                PreencherProcPerito(Ent.Cod_CNJ)
                HabilitaCampos(True)
            Else
                MsgErro("Número de CNJ Inválido")
                txtNum_CNJ1.Text = ""
                txtNum_CNJ2.Text = ""
                txtNum_Processo.Text = ""
                HabilitaCampos(False)
                txtNum_Processo.Focus()
            End If
        Else
            MsgErro("Número de Processo/CNJ não localizado!")
        End If

        '--------------------------------
        Dim m_ID_PF As Long
        Dim m_Descr_Especialidade As String = ""
        Dim m_Descr_Profissao As String = ""
        Dim EntProcPerito As New EntProcesso_Perito

        'Dim Acento As HttpServerUtility

        LimparInformacoesPerito()

        EntProcPerito = BalProcPer.ExibirDadosEntIDNom(nID_Nomeacao)
        'txtID_PF.Text = BalProcPer.ExibirID(nID_Nomeacao)

        'Session("ID_PF") = txtID_PF.Text
        m_ID_PF = EntProcPerito.ID_PF

        '--------------------------------
        'm_Descr_Especialidade = EntProcPerito.DESCR_ESPECIALIDADE
        'm_Descr_Profissao = EntProcPerito.DESCR_PROFISSAO
        m_Descr_Especialidade = Trim(HttpUtility.HtmlDecode(EntProcPerito.DESCR_ESPECIALIDADE))
        m_Descr_Profissao = Trim(HttpUtility.HtmlDecode(EntProcPerito.DESCR_PROFISSAO))

        If m_Descr_Profissao <> "" Then
            If Not CboProfissao.Items.FindByText(UCase(m_Descr_Profissao)) Is Nothing Then
                CboProfissao.SelectedValue = CboProfissao.Items.FindByText(UCase(m_Descr_Profissao)).Value
                m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
                Session("Cod_profissao") = m_Cod_Profissao
            Else
                CboProfissao.SelectedIndex = 0
            End If
        End If

        'logger.Debug("PreencherEspecialidade(" & m_Cod_Profissao & ")")
        PreencherEspecialidade(m_Cod_Profissao)

        If m_Descr_Especialidade <> "" Then
            If Not CboEspecialidade.Items.FindByText(UCase(m_Descr_Especialidade)) Is Nothing Then
                CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByText(UCase(m_Descr_Especialidade)).Value
                Session("Cod_especialidade") = CboEspecialidade.SelectedValue
            Else
                CboEspecialidade.SelectedIndex = 0
            End If
        Else
            CboEspecialidade.SelectedIndex = 0 'Generico
        End If
        '---------------------------

        txtID_PF.Text = m_ID_PF.ToString
        'lblData_Nomeacao.Text = Session("Data_Nomeacao").ToString
        lblData_Nomeacao.Text = EntProcPerito.Data_Nomeacao.ToShortDateString
        chkLaudoLiberado.Enabled = True

        'logger.Debug("ExibirDadosPerito()")
        ExibirDadosPerito(BalProcPer.ExibirDadosSetID_Nom(nID_Nomeacao))

        If lblData_Inicio.Text <> "" Then
            Bloquear()
        Else
            DesBloquear()
        End If

        If lblData_Negacao.Text <> "" Then
            BtnInicio.Enabled = False
            txtHonJuiz.Enabled = False
            txtPrazo.Enabled = False
        Else
            If lblData_Inicio.Text <> "" Then
                BtnInicio.Enabled = False
                txtHonJuiz.Enabled = False
                txtPrazo.Enabled = False
            Else
                If Not NovoHonFixo Then
                    BtnInicio.Enabled = True
                End If
                If lblData_Seg_Aceitacao.Text <> "" Then
                    txtHonJuiz.Enabled = False
                    txtPrazo.Enabled = False
                Else
                    txtHonJuiz.Enabled = True
                    txtPrazo.Enabled = True
                End If
            End If
        End If
        'BtnVerCNJPend.Enabled = False
        If lblData_Aceitacao.Text <> "" Or lblData_Negacao.Text <> "" Then
            BtnEmailNomeacao.Enabled = False
            If lblData_Seg_Aceitacao.Text <> "" Then
                BtnGravar.Enabled = False
            End If
        Else
            BtnEmailNomeacao.Enabled = True
            BtnGravar.Enabled = True
        End If
        If lblData_Inicio.Text <> "" Then
            BtnGravar.Enabled = False
        End If
        If txtNome_Perito.Text <> "" Then
            BtnEmailNomeacao.Enabled = True
        End If
    End Sub

    Protected Sub BtnVerCNJPend_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnVerCNJPend.Click
        'logger = log4net.LogManager.GetLogger("LogInFile")
        'logger.Debug("Acesso a Nomeação de Perito ...")

        Dim BalPer As New BALPERITO(GetUsuario)

        'logger.Debug("BtnNovo_Click/Limpar ...")
        Limpar()
        'BtnVerCNJ.Enabled = True
        If Not Session("PERICIAS_CODORGAO") Is Nothing Then
            lblOrgaoUsuario.Text = "[ÓRGÃO: " & Session("PERICIAS_CODORGAO").ToString & " - " & PreencheNomeOrgao(Session("PERICIAS_CODORGAO").ToString) & "]"
        End If
        BtnInicio.Attributes.Add("OnClick", "return confirm('Confirma o Início da Perícia?');")
        HabilitaCampos(False)
        If Not Session("Msg") Is Nothing Then
            MsgErro(Session("Msg").ToString)
        End If
        Session("Num_CNJ") = ""
        Session("Descr_Profissao") = ""
        Session("Descr_Especialidade") = ""
        Session("ID_PF") = ""
        Session("Data_Nomeacao") = ""
        If txtNum_CNJ.Text = "" Then
            m_Num_Processo_N = ""
        Else
            m_Num_Processo_N = txtNum_CNJ.Text
        End If
        BtnEmailNomeacao.Enabled = True
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmNomeacoes_Pendentes.aspx', '_blank', 'height=550,width=820, Top=50,left=120, ,scrollbars=1');", True)

    End Sub
    ' Necessita de Substituição do Perito

    Protected Sub BtnVerCNJPendS_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnVerCNJPendS.Click
        'logger = log4net.LogManager.GetLogger("LogInFile")
        'logger.Debug("Acesso a Nomeação de Perito ...")

        Dim BalPer As New BALPERITO(GetUsuario)

        'logger.Debug("BtnNovo_Click/Limpar ...")
        Limpar()
        'BtnVerCNJ.Enabled = True
        If Not Session("PERICIAS_CODORGAO") Is Nothing Then
            lblOrgaoUsuario.Text = "[ÓRGÃO: " & Session("PERICIAS_CODORGAO").ToString & " - " & PreencheNomeOrgao(Session("PERICIAS_CODORGAO").ToString) & "]"
        End If
        BtnInicio.Attributes.Add("OnClick", "return confirm('Confirma o Início da Perícia?');")
        HabilitaCampos(False)
        If Not Session("Msg") Is Nothing Then
            MsgErro(Session("Msg").ToString)
        End If
        Session("Num_CNJ") = ""
        Session("Descr_Profissao") = ""
        Session("Descr_Especialidade") = ""
        Session("ID_PF") = ""
        Session("Data_Nomeacao") = ""
        If txtNum_CNJ.Text = "" Then
            m_Num_Processo_N = ""
        Else
            m_Num_Processo_N = txtNum_CNJ.Text
        End If
        BtnEmailNomeacao.Enabled = True
        '------------------------------
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmNomeacoes_Substituicoes.aspx', '_blank', 'height=550,width=810, Top=50,left=120, ,scrollbars=1');", True)
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmNomeacoes_Substituicoes.aspx', '_blank', 'height=550,width=840, Top=50,left=120, ,scrollbars=1');", True)
        '------------------------------

    End Sub

    Protected Sub BtnNovoPer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovoPer.Click
        LimparInformacoesPerito()
    End Sub

    Protected Sub txtHonJuiz_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtHonJuiz.TextChanged

        If txtHonJuiz.Text = "" Then
            NovoHonFixo = True
            BtnInicio.Enabled = True
        Else
            BtnInicio.Enabled = False
        End If

    End Sub

    Protected Sub txtPrazo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrazo.TextChanged
        If txtNome_Perito.Text <> "" And RdbJustGrat.SelectedValue <> "" And txtPrazo.Text <> "" Then
            BtnGravar.Enabled = True
        End If
    End Sub

    Private Function Int64(ByVal p1 As String) As Long
        Throw New NotImplementedException
    End Function

 
End Class
