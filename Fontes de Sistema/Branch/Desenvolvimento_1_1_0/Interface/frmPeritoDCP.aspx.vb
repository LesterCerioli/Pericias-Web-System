Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports DGTECGEDARDOTNET
Imports log4net
Imports App = System.Configuration.ConfigurationManager

Partial Public Class frmPeritoDCP
    Inherits BasePage
    Dim m_Cod_Profissao As Integer
    Dim m_Cod_Especialidade As Integer
    Dim EscolherPerito As String
    Dim logger As log4net.ILog

    Private Sub frmPeritoDCP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        logger = log4net.LogManager.GetLogger("LogInFile")
        logger.Debug("Acesso a Nomeação de Perito ...")

        Dim BalPer As New BALPERITO(GetUsuario)

        If Not IsPostBack Then

            If Not Session("Msg") Is Nothing Then
                MsgErro(Session("Msg").ToString)
            End If

            Session("ID") = ""
            txtID_PF.Text = Request.QueryString("ID_PF")

            logger.Debug("txtID_PF.Text: " & txtID_PF.Text)
            If txtID_PF.Text <> "" Then
                Session("ID") = txtID_PF.Text

                txtNum_CNJ1.Text = Session("Num_Processo1").ToString
                txtNum_CNJ2.Text = Session("Num_Processo2").ToString
                txtNum_CNJ.Text = Session("Num_CNJ").ToString

                logger.Debug("ExibirDadosPerito()")
                ExibirDadosPerito()

                logger.Debug("AtualizatxtNum_CNJ()")
                AtualizatxtNum_CNJ()
                tbAceiteLaudo.Visible = True
            Else
                txtNum_CNJ1.Attributes.Add("onblur", "return FormatarProcCNJ1('" & txtNum_CNJ1.ClientID & "');")
                txtPrazo.Text = "30"
                'logger.Debug("HabilitaAceiteLaudo(False)")
                tbAceiteLaudo.Visible = False
                'HabilitaAceiteLaudo(False)
            End If
            If Not Session("Cod_Profissao") Is Nothing Then
                m_Cod_Profissao = CInt(Session("Cod_Profissao").ToString)
                PreencherPROFISSAO()
                If m_Cod_Profissao <> 0 Then
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
                logger.Debug("PreencherPROFISSAO()")
                PreencherPROFISSAO()
            End If
        End If

        If Session("Escolha") Is Nothing Then
            EscolherPerito = "N"
        Else
            EscolherPerito = Session("Escolha").ToString
        End If

        logger.Debug("EscolherPerito: " & EscolherPerito)

        If txtNome_Perito.Text = "" And EscolherPerito = "S" Then
            If Not Session("ID") Is Nothing Then
                If Session("ID").ToString <> "" Then
                    txtID_PF.Text = Session("ID").ToString
                    txtNome_Perito.Text = BalPer.Nome_ID(txtID_PF.Text)
                End If
            End If
        End If

        If Not Session("Num_CNJ") Is Nothing Then
            txtNum_CNJ.Text = Session("Num_CNJ").ToString
        End If

        If Not Session("Num_Processo1") Is Nothing Then
            txtNum_CNJ1.Text = Session("Num_Processo1").ToString
        End If

        If Not Session("Num_Processo2") Is Nothing Then
            txtNum_CNJ2.Text = Session("Num_Processo2").ToString
        End If

        If txtNum_CNJ1.Text <> "" And txtNum_CNJ2.Text <> "" Then
            logger.Debug("AtualizatxtNum_CNJ()")
            AtualizatxtNum_CNJ()
        End If

    End Sub

    Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

        logger.Debug("PreencherEspecialidade - " & m_Cod_Profissao & " ...")

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

        logger.Debug("PreencherPROFISSAO()...")

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

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        logger.Debug("BtnGravar_Click ...")

        Dim Bal As New BalProcesso_Perito(GetUsuario)
        Dim BalPgto As New BalPagamento_Perito(GetUsuario)
        Dim BalEnviarEmail As New BalEmail(GetUsuario)
        Dim m_PRAZO_ENTREGA As String

        'Verificar se consta como nomeado na tabela Processo_Perito
        ' se sim não grava nada
        logger.Debug("txtNome_Perito.Text: " & txtNome_Perito.Text)
        logger.Debug("CboProfissao.Text: " & CboProfissao.Text)

        If txtNome_Perito.Text = "" Then
            MsgErro("Favor escolher um perito.")
            Exit Sub
        End If

        If CboProfissao.Text = "Selecione uma Profissão" Then
            MsgErro("Favor selecionar a profissão do perito.")
            Exit Sub
        End If

        Dim m_Num_Oficio As Integer

        'Gravar Processo_Perito
        Dim dsOficio As DataSet

        logger.Debug("Bal.NumerarOficio(" & txtNum_CNJ.Text & "," & txtID_PF.Text & ")")
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
        Dim m_Data_Liberacao As String
        Dim m_Cod_Tipo_Pericia As Integer

        m_PRAZO_ENTREGA = txtPrazo.Text
        m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        If CboEspecialidade.SelectedValue = "GENÉRICO" Then
            m_Cod_Especialidade = 0
        Else
            m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
        End If

        'm_Data_Liberacao = Trim(lblData_Liberacao.Text)

        'ChkPsiqLocal,chkPsiqAudiencia

        'logger.Debug("Bal.GravarProcesso_Perito(" & txtNum_CNJ.Text & "," & txtID_PF.Text & "," & Today.ToShortDateString & "," & "," & "," & m_Data_Liberacao & "," & m_PRAZO_ENTREGA & "," & m_Cod_Profissao & "," & m_Cod_Especialidade & "," & Usuario & "," & "," & "," & m_Num_Oficio & "," & Year(Now).ToString & "," & IIf(ChkJustGrat.Checked, "S", "N").ToString & ")")
        'Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(txtID_PF.Text), Today.ToShortDateString, "", "", m_Data_Liberacao, CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, "", "", m_Num_Oficio, Year(Now), IIf(ChkJustGrat.Checked, "S", "N").ToString)

        'Processo Perito - incluindo Cod_Tipo_Pericia

        m_Cod_Tipo_Pericia = 0
        If ChkPsiqLocal.Checked Then m_Cod_Tipo_Pericia = 1
        If chkPsiqAudiencia.Checked Then m_Cod_Tipo_Pericia = 2

        logger.Debug("Bal.GravarProcesso_Perito(" & txtNum_CNJ.Text & "," & txtID_PF.Text & "," & Today.ToShortDateString & "," & "," & "," & m_Data_Liberacao & "," & m_PRAZO_ENTREGA & "," & m_Cod_Profissao & "," & m_Cod_Especialidade & "," & Usuario & "," & "," & "," & m_Num_Oficio & "," & Year(Now).ToString & "," & IIf(ChkJustGrat.Checked, "S", "N").ToString & "," & m_Cod_Tipo_Pericia & ")")
        Bal.GravarProcesso_Perito(txtNum_CNJ.Text, CInt(txtID_PF.Text), Today.ToShortDateString, "", "", m_Data_Liberacao, CInt(m_PRAZO_ENTREGA), m_Cod_Profissao, m_Cod_Especialidade, Usuario, "", "", m_Num_Oficio, Year(Now), IIf(ChkJustGrat.Checked, "S", "N").ToString, m_Cod_Tipo_Pericia)

        Dim m_Data_Envio_DGPCF As String = ""
        Dim m_Num_Prot As Long = 11111 'teste

        'BalPgto.GravarPagamento_Perito(txtNum_CNJ.Text, CInt(txtID_PF.Text), lblData_Liberacao.Text, lblData_Cancelamento.Text, m_Cod_Tipo_Pericia, m_Cod_Especialidade, m_Num_Prot, m_Data_Envio_DGPCF)
        'Gerar Número do Protocolo
        'Provisorio para testes(analisar - teste) - Veriicar itens abaixo
        'm_Num_Prot = GerarNum_Prot (Função a ser rodada na base do EPROT com retono do número do protocolo no formato Long)
        ' m_Data_envio_DGPCF = Today.ToShortDateString
        ' A data de envio será a data do envio do lote que nao incluira os pagamentos(protocolos) cuja data de autorizacao seja inferior 
        ' a 7 dias da data de autorizacao ou aquele em que existe uma data de cancelamento.
        ' Ao limpar o cancelamento devera a data de cancelamento ficar vazia e a data de liberação tb assim como desmarcar o item de liberado(autorizado)
        'Na nova marcacao da liberação, devera ser gravada um novo registro de pagamento com a nova data de autorizacao, para preservar um historico de ocorrencias
        ' O envio do lote se dara numa vericacao semanal do sistema para enviar todos com mais de sete dias de autorizacao sem cancelamento.
        '''''''''''''''''''''''''''''''''''''''
        'Gravar Pgto
        logger.Debug("BalPgto.GravarPagamento_Perito(" & txtNum_CNJ.Text & "," & txtID_PF.Text & "," & lblData_Liberacao.Text & "," & lblData_Cancelamento.Text & "," & CStr(m_Cod_Tipo_Pericia) & "," & m_Data_Liberacao & "," & CStr(m_Cod_Especialidade) & "," & CStr(m_Num_Prot) & "," & m_Data_envio_DGPCF & ")")
        BalPgto.GravarPagamento_Perito(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text), lblData_Liberacao.Text, lblData_Cancelamento.Text, m_Cod_Tipo_Pericia, m_Cod_Especialidade, m_Num_Prot, m_Data_envio_DGPCF)

        logger.Debug("BalComarca.ExibirDadosServentia(" & GetUsuario.CodOrg & ")")
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
                                              IIf(CboEspecialidade.SelectedIndex = 0, "", CboEspecialidade.SelectedValue).ToString)
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

        EnviaEmail("", "", "", "", "")

        BtnGravar.Enabled = False
        logger.Debug("PreencherProcPerito(" & txtNum_CNJ.Text & ")")
        PreencherProcPerito(txtNum_CNJ.Text)
        '---------------------
        'ExibirDadosPerito()
        'AtualizatxtNum_CNJ()
        '---------------------
        txtAnotacao.Visible = False
        LblAnotacao.Visible = False

        MsgErro("Gravação realizada com sucesso.")

    End Sub

    'Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged

    '    logger.Debug("CboEspecialidade_SelectedIndexChanged ...")

    '    If CboEspecialidade.SelectedValue <> "GENÉRICO" Then
    '        m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
    '    End If

    '    m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
    '    If m_Cod_Profissao = 0 Then
    '        MsgErro("Selecione uma Profissão")
    '        Exit Sub
    '    End If
    '    Session("Cod_Especialidade") = m_Cod_Especialidade.ToString
    '    '''''''PreencherPeritos(m_Cod_Profissao, m_Cod_Especialidade)

    'End Sub

    Private Sub ExibirDadosPerito()

        logger.Debug("ExibirDadosPerito()...")

        Dim Bal As New BALAnotacao(GetUsuario)
        Dim Ent As New EntAnotacao
        Dim BalPer As New BALPERITO(GetUsuario)
        Dim EntPer As New EntPERITO
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim Balpgto As New BalPagamento_Perito(GetUsuario)
        Dim EntProcPer As New EntProcesso_Perito
        Dim Ds As DataSet
        Dim DsPer As DataSet
        Dim DsProcPer As DataSet
        Dim DsPgto As DataSet
        Dim rsPer As DataRow
        Dim rsPgto As DataRow
        Dim rsProcPer As DataRow
        Dim m_ID_PF As Long
        Dim i As Integer
        Dim m_Justica_Gratuita As String
        Dim sCodEspecialidade As String = String.Empty
        Dim sCodProfissao As String = String.Empty

        m_ID_PF = CInt(txtID_PF.Text)

        If (Not Session("Cod_Profissao") Is Nothing) And (Not Session("Cod_Especialidade") Is Nothing) Then
            logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & ")")
            DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, Session("Cod_Profissao").ToString, IIf(Session("Cod_Especialidade").ToString = "GENÉRICO", "", Session("Cod_Especialidade").ToString).ToString)
        Else
            logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & ")")
            DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "")
        End If

        If DsProcPer.Tables(0).Rows.Count > 0 Then
            rsProcPer = DsProcPer.Tables(0).Rows(0)

            If rsProcPer("nome") Is Nothing Or rsProcPer("nome").ToString = "" Then
                txtNome_Perito.Text = ""
            Else
                txtNome_Perito.Text = rsProcPer("Nome").ToString
            End If

            BtnAnotacao.Enabled = True

            If rsProcPer("Data_Aceitacao") Is Nothing Or rsProcPer("Data_Aceitacao").ToString = "" Then
                lblData_Aceitacao.Text = ""
                'logger.Debug("HabilitaAceiteLaudo(False)")
                'HabilitaAceiteLaudo(False)
            Else
                lblData_Aceitacao.Text = Convert.ToDateTime(rsProcPer("Data_Aceitacao").ToString).ToShortDateString
                'logger.Debug("HabilitaAceiteLaudo(True)")
                'HabilitaAceiteLaudo(True)
            End If

            If rsProcPer("Data_Negacao").ToString <> "" Then
                MsgErro("Perito foi Nomeado para este processo, anteriormente. Recusou o convite em " + lblData_Negacao.Text)
                lblData_Negacao.Text = Convert.ToDateTime(rsProcPer("Data_Negacao").ToString).ToShortDateString
                'logger.Debug("HabilitaAceiteLaudo(False)")
                'HabilitaAceiteLaudo(False)
                Exit Sub
            Else
                lblData_Negacao.Text = ""
            End If

            If rsProcPer("Data_Nomeacao") Is Nothing Or rsProcPer("Data_Nomeacao").ToString = "" Then
                lblData_Nomeacao.Text = Today.ToShortDateString
            Else
                lblData_Nomeacao.Text = Convert.ToDateTime(rsProcPer("Data_Nomeacao").ToString).ToShortDateString
            End If

            If rsProcPer("Data_Liberacao") Is Nothing Or rsProcPer("Data_Liberacao").ToString = "" And _
            lblData_Aceitacao.Text <> "" And lblData_Negacao.Text = "" Then
                'HabilitaAceiteLaudo(True)
                tbAceiteLaudo.Visible = True
                BtnGravar.Enabled = True
                chkLaudoLiberado.Enabled = True
                chkLaudoLiberado.Checked = False
            ElseIf rsProcPer("Data_Liberacao").ToString <> "" And lblData_Negacao.Text = "" Then
                lblData_Liberacao.Text = Convert.ToDateTime(rsProcPer("Data_Liberacao").ToString).ToShortDateString
                'HabilitaAceiteLaudo(False)
                tbAceiteLaudo.Visible = False
                BtnGravar.Enabled = True
                If lblData_Aceitacao.Text <> "" Then
                    chkLaudoLiberado.Checked = True
                    chkLaudoLiberado.Enabled = False
                End If
            ElseIf rsProcPer("Data_Cancelamento").ToString <> "" And lblData_Negacao.Text = "" Then
                lblData_Cancelamento.Text = Convert.ToDateTime(rsProcPer("Data_Cancelamento").ToString).ToShortDateString
                'HabilitaAceiteLaudo(False)
                tbAceiteLaudo.Visible = False
                BtnGravar.Enabled = True
                If lblData_Aceitacao.Text <> "" Then
                    chkLaudoLiberado.Checked = True
                    chkLaudoLiberado.Enabled = False
                End If
            Else
                'HabilitaAceiteLaudo(False)
                BtnGravar.Enabled = False
                If lblData_Negacao.Text = "" And lblData_Aceitacao.Text <> "" Then
                    tbAceiteLaudo.Visible = True
                    chkLaudoLiberado.Checked = True
                    chkLaudoLiberado.Enabled = False
                End If
            End If

            m_Justica_Gratuita = DsProcPer.Tables(0).Rows(0).Item("JG").ToString
            If m_Justica_Gratuita = "S" Then
                ChkJustGrat.Checked = True
            Else
                ChkJustGrat.Checked = False
            End If

            txtPrazo.Text = DsProcPer.Tables(0).Rows(0).Item("Prazo").ToString

            If Not IsDBNull(DsProcPer.Tables(0).Rows(0).Item("Cod_Tipo_Pericia")) Then
                If Val(DsProcPer.Tables(0).Rows(0).Item("Cod_Tipo_Pericia")) = 1 Then
                    ChkPsiqLocal.Checked = True
                    chkPsiqAudiencia.Checked = False
                ElseIf Val(DsProcPer.Tables(0).Rows(0).Item("Cod_Tipo_Pericia")) = 2 Then
                    chkPsiqAudiencia.Checked = True
                    ChkPsiqLocal.Checked = False
                End If
            Else
                chkPsiqAudiencia.Checked = False
                ChkPsiqLocal.Checked = False
            End If

            'Consutando dados de pagamentos

            DsPgto = Nothing
            If CboEspecialidade.SelectedValue = "GENÉRICO" Then
                m_Cod_Especialidade = 0
            Else
                m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            End If
            logger.Debug("Balpgto.ExibirDadosSet(" & txtNum_CNJ.Text & "," & m_ID_PF.ToString & "," & m_Cod_Especialidade & ")")
            DsPgto = Balpgto.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, m_Cod_Especialidade)

            If DsPgto.Tables(0).Rows.Count > 0 Then
                rsPgto = DsPgto.Tables(0).Rows(0)

                If rsPgto("Data_Autorizacao") Is Nothing Then
                    lblData_Liberacao.Text = rsPgto("Data_Autorizacao").ToString
                    chkLaudoLiberado.Checked = True
                End If
                If rsPgto("Data_Cancelamento") Is Nothing Then
                    lblData_Liberacao.Text = rsPgto("Data_Cancelamento").ToString
                    ChkCancelaPgto.Checked = True
                End If
            End If
        Else
            BtnGravar.Enabled = True
            BtnAnotacao.Enabled = True
            txtPrazo.Text = "30"
        End If

        DsProcPer = Nothing
        logger.Debug("Bal.ExibirAnotPer(" & m_ID_PF.ToString & ")")
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
        logger.Debug("BalPer.ExibirDadosSet(" & m_ID_PF.ToString & ")")
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

            txtNome_Perito.Text = rsPer("Nome").ToString

            If lblEmail.Text <> "" Or lblEmail1.Text <> "" Then
                BtnEmailNomeacao.Enabled = True
            End If
        End If

        DsPer = Nothing

    End Sub

    Protected Sub txtNum_Processo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_Processo.TextChanged

        logger.Debug("txtNum_Processo_TextChanged ...")

        Dim Ent As EntPROC_CNJ
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim Cod_CNJ_Valido As Boolean

        'txtNum_CNJ.ClientID.validar(false-retornar)
        logger.Debug("ValidaNumProc(" & txtNum_Processo.Text & ")")
        If Not ValidaNumProc(txtNum_Processo.Text) Then
            MsgErro("Número de Processo Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_CNJ.Text = ""
            Exit Sub
        End If

        logger.Debug("BalP.ExibirDadosEnt(" & txtNum_Processo.Text & "," & txtNum_CNJ.Text & ")")
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ1.Text = "" Then
                txtNum_CNJ1.Text = Mid(Ent.Cod_CNJ, 1, 7) + Mid(Ent.Cod_CNJ, 9, 2) + Mid(Ent.Cod_CNJ, 12, 4)
                txtNum_CNJ2.Text = Mid(Ent.Cod_CNJ, 22, 4)
                txtNum_CNJ.Text = Ent.Cod_CNJ
            End If

            logger.Debug("ValidaNumCNJ(" & Ent.Cod_CNJ & ")")
            Cod_CNJ_Valido = ValidaNumCNJ(Ent.Cod_CNJ)
            If Cod_CNJ_Valido Then
                logger.Debug("PreencherProcPerito(" & Ent.Cod_CNJ & ")")
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

    End Sub

    Protected Sub BtnAnotacao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAnotacao.Click

        logger.Debug("BtnAnotacao_Click ...")

        'Anotações
        If txtID_PF.Text = "" Then
            MsgErro("Selecione o Perito")
            Exit Sub
        End If
        Session("Nome") = txtNome_Perito.Text
        logger.Debug("txtNome_Perito.Text: " & txtNome_Perito.Text)
        logger.Debug("Abre frmAnotacaoDCP.aspx")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmAnotacaoDCP.aspx', '_blank', 'height=362,width=515,Top=270,left=420,scrollbars=0,toolbar=0,resizable=0,location=0,status=0');", True)
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
        logger.Debug("BtnNovo_Click ...")
        Limpar()
    End Sub

    Private Sub Limpar()
        logger.Debug("Limpar()")
        txtAnotacao.Visible = False
        LblAnotacao.Visible = False
        lblEmail.Text = ""
        lblEmail1.Text = ""
        txtAnotacao.Text = ""
        lblData_Nomeacao.Text = ""
        lblData_Liberacao.Text = ""
        lblData_Cancelamento.Text = ""
        ChkCancelaPgto.Checked = False
        chkLaudoLiberado.Checked = False
        chkLaudoLiberado.Enabled = False
        ChkCancelaPgto.Enabled = False
        ChkPsiqLocal.Checked = False
        chkPsiqAudiencia.Checked = False
        tbAceiteLaudo.Visible = False
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
        Session.Remove("ID")
        Session.Remove("Cod_Profissao")
        Session.Remove("Cod_Especialidade")
        Session.Remove("Num_Processo1")
        Session.Remove("Num_Processo2")
        pnlPeritos.Visible = False
        pnlPeritos.Enabled = False

        BtnGravar.Enabled = False
        BtnEmailNomeacao.Enabled = False
        BtnAnotacao.Enabled = False

    End Sub

    Protected Sub PreencherProcPerito(ByVal Num_CNJ As String)

        logger.Debug("PreencherProcPerito(" & Num_CNJ & ") ...")

        Dim Ds As DataSet
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        logger.Debug("BalProcPer.ExibirDadosTodos(" & Num_CNJ & ")")
        Ds = BalProcPer.ExibirDadosTodos(Num_CNJ)
        GrdProcPerito.DataSource = Ds.Tables(0)
        GrdProcPerito.DataBind()
        pnlPeritos.Visible = True
        pnlPeritos.Enabled = True
    End Sub

    Protected Sub BtnEmailNomeacao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEmailNomeacao.Click

        logger.Debug("BtnEmailNomeacao_Click ...")

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
        logger.Debug("Bal.ExibirNumOficio(" & txtNum_CNJ.Text & "," & txtID_PF.Text & ")")
        m_Num_Oficio = Bal.ExibirNumOficio(txtNum_CNJ.Text, txtID_PF.Text)

        Dim txtBrasao As String
        logger.Debug("Converte imagem e bytes")
        txtBrasao = ToBase64(ConvertImageFiletoBytes("Imagens/Brasao_oficial.GIF")) 'ToBase64(ByVal data() As Byte) As String
        logger.Debug("Monta corpo do email")

        EmailHTML = "<html><body><table id='CorpoEmail' border='0' width='100%'>" & _
        "<tr><td><table id='Cabecalho' width='100%' border='0'><tr><td><center>" & _
        "<img alt='Brasao' src='data:image/jpeg;base64," & txtBrasao & "style='width: 69px; height: 78px' border='0' id='idImagem' />" & _
        "</center></td></tr> <tr><td>" & _
        "<center><b style='font-size: small'>TRIBUNAL DE JUSTI&Ccedil;A DO ESTADO DO RIO DE JANEIRO</b><br />" & _
        "<b style='font-size: x-small'>Diretoria Geral de Apoio aos &Oacute;rg&atilde;os Jurisdicionais<br />" & _
        "Departamento de Instrução Processual<br />" & _
        "Divis&atilde;o Per&iacute;cias Judiciais" & _
        "</center></td></tr><tr> <td></td></tr></table></td></tr><tr><td>" & _
        "<table id='SubCabecalho' border='0' width='100%'>" & _
        "<tr><td align='left'> <b>Ofício DIPEJ-GAB n&deg " & m_Num_Oficio & " </b></td>" & _
        "<td align='right'><b>Rio de Janeiro, " & Now.Day.ToString & " de " & MonthName(Now.Month, False).ToString & " de " & Now.Year.ToString() & " </b> <br /></td></tr><tr><td></td></tr></table>"

        EmailHTML = EmailHTML + "<br/>Processo Judicial n&deg " + txtNum_CNJ.Text + "(" + txtNum_Processo.Text + ")" & _
        "</td></tr><tr><td></td></tr><tr><td></td></tr><tr><td><center><b><br/>Prezado Senhor,"

        EmailHTML = EmailHTML + "</b></center><br /></td></tr><tr><td align='justify' colspan='0'>" & _
        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & _
        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sirvo-me do presente para intim&aacute;-lo " & _
        "de sua nomea&ccedil&atilde;o pelo Exmo. Sr. Juiz de Direito da </span>"


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
        logger.Debug("BalComarca.ExibirDadosServentia(" & GetUsuario.CodOrg & ")")
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
            logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & txtID_PF.Text & "," & Session("Cod_Profissao").ToString & "," & Session("Cod_Especialidade").ToString & ")")
            DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, CInt(txtID_PF.Text), Session("Cod_Profissao").ToString, Session("Cod_Especialidade").ToString)
        Else
            logger.Debug("BalProcPer.ExibirDadosSet(" & txtNum_CNJ.Text & "," & txtID_PF.Text & ", ,)")
            DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, CInt(txtID_PF.Text), "", "")
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

        logger.Debug("Atualiza corpo do email")
        EmailHTML = EmailHTML + m_Descr_Serventia & " da " & m_Descr_Comarca & " , para atuar como perito daquele respeit&aacute;vel " & _
        "Ju&iacute;zo nos autos do Processo em ep&iacute;grafe, devendo V.Sa. se manifestar " & _
        "quanto à aceita&ccedil;&atilde;o ou recusa do encargo bem como apresentar " & _
        "proposta de honor&aacute;rios, concomitantemente, diretamente aquele ju&iacute;zo.(ou neste sistema)."

        EmailHTML = EmailHTML + "</td></tr><tr><td><center><br />Cordialmente.</center></td></tr>" & _
        "<tr><td><center><br /><b>MARCIO MARCELO DA SILVA OLIVEIRA</b><br />" & _
        "Diretor da Divis&atilde;o de Per&iacute;cias Judiciais</center></td></tr>"

        EmailHTML = EmailHTML + "<tr><td align='left'>Ilmo. Sr. " & txtNome_Perito.Text & ".<br />" & _
        "(" & m_Descr_Profissao & "/ " & m_Descr_Especialidade & ") " & "</td>"

        EmailHTML = EmailHTML + "</tr></table></body></html>"
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
            logger.Debug("BalEnviarEmail.EnviarEmail(" & m_Email & "," & p_Cc & "," & p_From_Nome & "," & p_From_Address & "," & p_Subject & "," & p_Html & "," & p_Smtp_Hostname & "," & p_Smtp_Portnum & ")")
            BalEnviarEmail.EnviarEmail(m_Email, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
            bEnviado = True
        Else
            logger.Debug("E-mail de teste não foi enviado.")
            If Not bEnviado Then
                bEnviado = False
            End If
        End If

        If lblEmail.Text <> "" Then
            logger.Debug("BalEnviarEmail.EnviarEmail(" & lblEmail.Text & "," & p_Cc & "," & p_From_Nome & "," & p_From_Address & "," & p_Subject & "," & p_Html & "," & p_Smtp_Hostname & "," & p_Smtp_Portnum & ")")
            BalEnviarEmail.EnviarEmail(lblEmail.Text, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        Else
            logger.Debug("E-mail não foi enviado.")
            If Not bEnviado Then
                bEnviado = False
            End If
        End If
        If lblEmail1.Text <> "" Then
            logger.Debug("BalEnviarEmail.EnviarEmail(" & lblEmail1.Text & "," & p_Cc & "," & p_From_Nome & "," & p_From_Address & "," & p_Subject & "," & p_Html & "," & p_Smtp_Hostname & "," & p_Smtp_Portnum & ")")
            BalEnviarEmail.EnviarEmail(lblEmail1.Text, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
            bEnviado = True
        Else
            logger.Debug("E-mail alternativo teste não foi enviado.")
            If Not bEnviado Then
                bEnviado = False
            End If
        End If
        Return bEnviado
    End Function

    Protected Sub chkLaudoLiberado_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkLaudoLiberado.CheckedChanged

        logger.Debug(" chkLaudoLiberado_CheckedChanged ...")

        If chkLaudoLiberado.Checked Then
            ChkCancelaPgto.Enabled = True
            If lblData_Liberacao.Text = "" Then
                lblData_Liberacao.Text = Today.ToShortDateString
            End If
        Else
            ChkCancelaPgto.Enabled = False
            lblData_Liberacao.Text = ""
        End If

        'If ChkCancelaPgto.Checked Then
        '    chkLaudoLiberado.Enabled = False
        '    If lblData_Cancelamento.Text = "" Then
        '        lblData_Cancelamento.Text = Today.ToShortDateString
        '    End If
        'Else
        '    chkLaudoLiberado.Enabled = False
        '    chkLaudoLiberado.Text = ""
        'End If

    End Sub


    Protected Sub GrdProcPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdProcPerito.SelectedIndexChanged
        logger.Debug("GrdProcPerito_SelectedIndexChanged ...")
        Dim m_ID_PF As Long
        Dim m_Descr_Especialidade As String
        Dim m_Descr_Profissao As String

        'Dim Acento As HttpServerUtility
        m_ID_PF = CInt(GrdProcPerito.SelectedRow.Cells(1).Text)
        m_Descr_Especialidade = Trim(HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(4).Text))
        m_Descr_Profissao = Trim(HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(3).Text))
        '  m_Cod_Profissao = 0

        If m_Descr_Profissao <> "" Then
            CboProfissao.SelectedValue = CboProfissao.Items.FindByText(m_Descr_Profissao).Value
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
            Session("Cod_profissao") = m_Cod_Profissao
        End If

        logger.Debug("PreencherEspecialidade(" & m_Cod_Profissao & ")")
        PreencherEspecialidade(m_Cod_Profissao)
        If m_Descr_Especialidade <> "" Then
            CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByText(m_Descr_Especialidade).Value
            Session("Cod_especialidade") = CboEspecialidade.SelectedValue
        Else
            CboEspecialidade.SelectedValue = "0" 'Generico
        End If
        ''''PreencherPeritos(m_Cod_Profissao, CInt(CboEspecialidade.Items.FindByValue(CboEspecialidade.Text).Value))
        txtID_PF.Text = GrdProcPerito.SelectedRow.Cells(1).Text
        logger.Debug("ExibirDadosPerito()")
        chkLaudoLiberado.Enabled = True
        ExibirDadosPerito()
        BtnGravar.Enabled = True
    End Sub

    Protected Sub GrdProcPerito_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)

        GrdProcPerito.PageIndex = e.NewPageIndex
        PreencherProcPerito(txtNum_CNJ.Text)

    End Sub

    'Protected Sub BtnCurriculum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCurriculum.Click
    '    If Not Me.IsPostBack Then
    '        Exit Sub
    '    End If
    '    'DGTECGEDAR Class
    '    'DGTECGEDARDOTNET Namespace
    '    Dim Obj As DGTECGEDAR
    '    Dim IDDoc As String
    '    Dim Bal As New BALPERITO(GetUsuario)
    '    Dim Ent As EntPERITO
    '    'Dim f_ID_PF As String
    '    Dim m_URL As String
    '    Dim mm_ID_PF As Integer

    '    'IDGED_FOTO
    '    'IDGED_CV
    '    'f_ID_PF = Session("ID").ToString
    '    'mm_ID_PF = CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)
    '    If txtID_PF.Text = "" Then
    '        MsgErro("Identificação Inválida")
    '        Exit Sub
    '    End If
    '    mm_ID_PF = CInt(txtID_PF.Text)
    '    If mm_ID_PF = 0 Then
    '        Session("ID") = 0
    '    Else
    '        Session("ID") = mm_ID_PF
    '    End If
    '    Ent = Bal.ExibirDadosEnt("ID", mm_ID_PF.ToString, "N") 'f_ID_PF)
    '    If Not Ent Is Nothing Then
    '        IDDoc = Ent.IDGED_CV
    '    Else
    '        IDDoc = ""
    '    End If
    '    If IDDoc <> "" Then
    '        Obj = New DGTECGEDAR
    '        Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
    '        m_URL = Obj.MontaURLCacheWeb(IDDoc)
    '        Obj.Finaliza()
    '        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes');", True)
    '    End If
    'End Sub

    'Protected Sub BtnVerFoto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnVerFoto.Click
    '    Dim mm_ID_PF As Integer
    '    If Not Me.IsPostBack Then
    '        Exit Sub
    '    End If
    '    If txtID_PF.Text = "" Then
    '        MsgErro("Identificação Inválida")
    '        Exit Sub
    '    End If
    '    'mm_ID_PF = CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)
    '    mm_ID_PF = CInt(txtID_PF.Text)
    '    If mm_ID_PF = 0 Then
    '        Session("ID") = 0
    '    Else
    '        Session("ID") = mm_ID_PF
    '    End If
    '    Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmExibirFoto.aspx', '_blank', 'height=401,width=301, Top=150,left=120');", True)
    'End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged

        logger.Debug("CboProfissao_SelectedIndexChanged ...")
        txtNome_Perito.Text = ""
        Session("ID") = Nothing
        Session("Msg") = Nothing

        If CboProfissao.Items.Count > 0 Then
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)

            logger.Debug("PreencherEspecialidade(" & m_Cod_Profissao & ")")
            PreencherEspecialidade(m_Cod_Profissao)

            'If CboEspecialidade.Text = "GENÉRICO" Then
            '    m_Cod_Especialidade = 0
            'Else
            '    m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            'End If

        Else
            MsgErro("Selecione uma Profissão")
            Exit Sub
        End If
        'Session("Cod_Profissao") = m_Cod_Profissao.ToString
    End Sub

    Protected Sub txtNum_CNJ1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ1.TextChanged
        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
    End Sub

    Protected Sub txtNum_CNJ2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ2.TextChanged
        logger.Debug("txtNum_CNJ2_TextChanged ...")
        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
        logger.Debug("AtualizatxtNum_CNJ()")
        AtualizatxtNum_CNJ()

    End Sub

    Protected Sub AtualizatxtNum_CNJ() '_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ.TextChanged
        logger.Debug("AtualizatxtNum_CNJ() ...")
        Dim Ent As EntPROC_CNJ
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)

        'txtNum_CNJ.Text = txtNum_CNJ1.Text + ".8.19." + txtNum_CNJ2.Text
        If Len(txtNum_CNJ1.Text) = 15 Then
            txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 15) + ".8.19." + txtNum_CNJ2.Text
        Else
            txtNum_CNJ.Text = Mid(txtNum_CNJ1.Text, 1, 7) + "-" + Mid(txtNum_CNJ1.Text, 8, 2) + "." + Mid(txtNum_CNJ1.Text, 10, 4) + ".8.19." + txtNum_CNJ2.Text
        End If
        logger.Debug("ValidaNumCNJ(" & txtNum_CNJ.Text & ")")
        If Not ValidaNumCNJ(txtNum_CNJ.Text) Then
            MsgErro("Número CNJ Inválido")
            txtNum_CNJ1.Text = ""
            txtNum_CNJ2.Text = ""
            txtNum_Processo.Text = ""
            Exit Sub
        End If
        logger.Debug("BalP.ExibirDadosEnt(" & txtNum_Processo.Text & "," & txtNum_CNJ.Text & ")")
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ.Text = "" Then txtNum_CNJ.Text = Ent.Cod_CNJ
            logger.Debug("ValidaNumCNJ(" & Ent.Cod_CNJ & ")")
            If ValidaNumCNJ(Ent.Cod_CNJ) Then
                logger.Debug("PreencherProcPerito(" & Ent.Cod_CNJ & ")")
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

        logger.Debug("BtnPerito_Click ...")
        Session("Msg") = Nothing

        If Not Me.IsPostBack Then
            Exit Sub
        End If

        If CboProfissao.SelectedIndex = 0 Then
            MsgErro("Selecione a profissão do perito.")
            CboProfissao.Focus()
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
            If CboEspecialidade.SelectedValue = "GENÉRICO" Then
                m_Cod_Especialidade = 0
            Else
                m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
            End If
            Session("Cod_Profissao") = m_Cod_Profissao
            Session("Cod_Especialidade") = m_Cod_Especialidade
            Session("Num_Processo1") = txtNum_CNJ1.Text
            Session("Num_Processo2") = txtNum_CNJ2.Text
            Session("Num_CNJ") = txtNum_CNJ.Text

            logger.Debug("m_Cod_Profissao: " & m_Cod_Profissao)
            logger.Debug("m_Cod_Especialidade: " & m_Cod_Especialidade)
            logger.Debug("txtNum_CNJ1.Text: " & txtNum_CNJ1.Text)
            logger.Debug("txtNum_CNJ2.Text: " & txtNum_CNJ2.Text)
            logger.Debug("Redireciona para frmEscolherPerito.aspx")
            Response.Redirect("frmEscolherPerito.aspx")
        End If

    End Sub

    'Private Sub HabilitaAceiteLaudo(ByVal bValor As Boolean)
    '    'logger.Debug("HabilitaAceiteLaudo(" & bValor & ")")
    '    'chkLaudoLiberado.Visible = bValor
    '    'lblData_Liberacao.Visible = bValor
    '    'lblDescDtLiberacao.Visible = bValor
    '    'tbAceiteLaudo.Visible = bValor
    'End Sub

    Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged
        txtNome_Perito.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load, Me.Load

    End Sub

    Protected Sub ChkJustGrat_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkJustGrat.CheckedChanged
        If ChkJustGrat.Checked Then
            ChkPsiqLocal.Visible = True
            chkPsiqAudiencia.Visible = True
        Else
            ChkPsiqLocal.Visible = False
            chkPsiqAudiencia.Visible = False
            ChkPsiqLocal.Checked = False
            chkPsiqAudiencia.Checked = False
        End If
    End Sub

    Protected Sub ChkCancelaPgto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkCancelaPgto.CheckedChanged

        If chkLaudoLiberado.Checked Then
            If ChkCancelaPgto.Checked Then
                chkLaudoLiberado.Enabled = False
                chkLaudoLiberado.Checked = False
                lblData_Liberacao.Text = ""
                If lblData_Cancelamento.Text = "" Then
                    lblData_Cancelamento.Text = Today.ToShortDateString
                End If
                chkLaudoLiberado.Enabled = False
            Else
                chkLaudoLiberado.Enabled = True
                lblData_Cancelamento.Text = ""
            End If
        Else
            ChkCancelaPgto.Enabled = False
            lblData_Liberacao.Text = ""
            lblData_Cancelamento.Text = ""
        End If

    End Sub

    Protected Sub ChkPsiqLocal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkPsiqLocal.CheckedChanged

        If ChkPsiqLocal.Checked Then
            chkPsiqAudiencia.Checked = False
        End If

    End Sub

    Protected Sub chkPsiqAudiencia_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPsiqAudiencia.CheckedChanged
        If chkPsiqAudiencia.Checked Then
            ChkPsiqLocal.Checked = False
        End If
    End Sub

    Protected Sub txtNome_Perito_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome_Perito.TextChanged
        If txtNome_Perito.Text <> "" Then
            BtnGravar.Enabled = True
        End If
    End Sub
End Class
