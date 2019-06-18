Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports App = System.Configuration
Imports log4net

Partial Public Class frmAceitacao
    Inherits BasePage
    Private ent As New EntPERITO
    Private entAnot As New EntAnotacao
    Dim i, j As Integer
    Dim logger As log4net.ILog
    Dim m_Justica_Grat As Boolean
    Dim m_DataInicioPer As String = ""
    Dim Tacita As Boolean = False
    Dim m_Valor_Justica_Gratuita As Double = 0
    Dim Psiquiatrica As Boolean = False
    Dim m_Cod_Tipo_Pericia As Integer
    Dim m_Data_do_Aceite As Date = Today
    'Dim BalValorPer As New BalValor_Pericia(GetUsuario)
    'Dim BalValorPer As BalValor_Pericia
    'Dim BalValor_Pericia As BalValor_Pericia

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Bal As New BALPERITO(GetUsuario)
        Dim Usuario As String
        Dim sCPF As String = String.Empty

        logger = log4net.LogManager.GetLogger("LogInFile")


        'lblMsg0.Text = "Versao  1"
        If Not IsPostBack Then
            BtnGravar.Enabled = False
            Usuario = GetUsuario.UsuarioSO
            sCPF = Bal.ValidarPeritoCPF(ID_Usuario)

            'sCPF = "09178963788"

            If sCPF <> String.Empty Then
                Dim ent As EntPERITO
                ent = Bal.ExibirDadosEnt("CPF", sCPF, "N")

                If Not ent Is Nothing Then
                    lblCPF.Text = ent.CPF
                    lblNome.Text = ent.Nome
                    txtID_PF.Text = ent.ID_PF.ToString
                    PreencherGrdProcesso(ent.ID_PF)
                End If
            End If

        End If

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALPERITO(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim BalAceitaPer As New BalAceitacao_Perito(GetUsuario)
        Dim n As Integer = 0
        Dim m_Data_Aceitacao As String
        Dim m_Data_Negacao As String
        Dim m_Sigla As String

        m_Sigla = GetUsuario.UsuarioSO

        If lblCPF.Text = "" Then
            MsgErro("Gravação rejeitada. CPF Perito não encontrado.")
            Exit Sub
        End If

        If lblNome.Text = "" Then
            MsgErro("Gravação rejeitada. Nome do perito não encontrado.")
            Exit Sub
        End If

        If txtID_PF.Text = "" Then
            MsgErro("Houve falha na gravação.")
            Exit Sub
        End If
        If RdoAceitacao.Items(1).Selected And Trim(TxtMotivo.Text) = "" Then
            MsgErro("Gravação Rejeitada. Favor preencher o motivo")
            Exit Sub
        End If
        If Len(TxtMotivo.Text) > 255 Then
            MsgErro("Gravação rejeitada. O motivo da recusa possui caracteres em excesso, o limite é de 255.")
            TxtMotivo.Text = ""
            TxtMotivo.Focus()
            Exit Sub
        End If

        'ID_Nomeacao, Data_Aceitacao Data_Negação, Sigla, Motivo_Recusa, Honorários

        ent.Nome = lblNome.Text
        ent.Num_Reg = Session("NumReg").ToString
        entAnot.SIGLA = GetUsuario.Login
        If IsNumeric(lblCPF.Text) Then
            ent.CPF = lblCPF.Text
        Else
            ent.CPF = ""
        End If
        If Not IsNumeric(Session("CodOrgaoProfissional").ToString) Then
            ent.COD_ORGAO_PER = 0
        End If

        If txtID_PF.Text = "" Then
            ent.ID_PF = 0
        Else
            ent.ID_PF = CInt(txtID_PF.Text)
        End If
        m_Data_Aceitacao = IIf(RdoAceitacao.Items(0).Selected, "", Today.ToShortDateString).ToString
        m_Data_Negacao = IIf(RdoAceitacao.Items(1).Selected, "", Today.ToShortDateString).ToString
        Dim sSubstituir As String = ""
        Dim nID_Substituto As Long = 0

        'Substituir
        'ID_Substituto

        '18/05/2012
        If txtHonorarios.Text = "" And m_Justica_Grat Then
            txtHonorarios.Text = m_Valor_Justica_Gratuita.ToString
        End If

        Try
            logger.Debug("BalAceitaPer.GravarAceitacao(" & txtID_Nomeacao.Text & "," & m_Data_Aceitacao & "," & m_Data_Negacao & "," & Usuario & "," & TxtMotivo.Text & "," & txtHonorarios.Text & "," & txtHonorarioJuiz.Text & ")")
            'BalAceitaPer.GravarAceitacao(txtID_Nomeacao.Text, m_Data_Aceitacao, m_Data_Negacao, m_Sigla, TxtMotivo.Text, txtHonorarios.Text, txtHonorarioJuiz.Text)
            BalAceitaPer.GravarAceitacao(txtID_Nomeacao.Text, m_Data_Aceitacao, m_Data_Negacao, m_Sigla, TxtMotivo.Text, txtHonorarios.Text, txtHonorarioJuiz.Text)
            'If lblSegAceitacao.Text = "ACEITAÇÃO/RECUSA APÓS FIXAÇÃO DOS HONORÁRIOS PELO JUÍZO" And RdoAceitacao.Items(1).Selected Then
            If Trim(lblSegAceitacao.Text) <> "" And RdoAceitacao.Items(0).Selected Then
                If Not BalProcPer.PossuiDataInicio(CInt(txtID_Nomeacao.Text)) Then
                    'Gravar a Data Inicio após o segundo aceite
                    logger.Debug("BalProcPer.GravarDtInicio(CInt(" & txtID_Nomeacao.Text & "))")
                    BalProcPer.GravarDtInicio(CInt(txtID_Nomeacao.Text))
                End If
            End If
            MsgErro("Gravação feita com Sucesso")
            If txtID_PF.Text = "" Then
                If Not ent.ID_PF = Nothing Then
                    txtID_PF.Text = ent.ID_PF.ToString
                End If
            End If
        Catch ex As Exception
            logger.Error(ex.Message.ToString())
            MsgErro(ex.Message)
        End Try

        Limpar()
        PreencherGrdProcesso(CLng(txtID_PF.Text))

    End Sub

    Private Sub Limpar()
        txtOrgaoProfissional.Text = ""
        txtData_Nomeacao.Text = ""
        txtNum_Processo.Text = ""
        txtNum_CNJ.Text = ""
        txtNum_Reg.Text = ""
        txtEspecialidade.Text = ""
        txtProfissao.Text = ""
        TxtMotivo.Visible = False
        txtHonorarios.Visible = False
        txtHonorarioJuiz.Visible = False
        lblHonorarioJuiz.Visible = False
        lblRefHonJuiz.Visible = False
        lblMsgHon.Visible = False
        lblRefHonJuiz.Visible = False
        lblRefHon.Visible = False
        txtHonorarios.Text = ""
        txtHonorarioJuiz.Text = ""
        lblPrazo.Text = " - "
        RdoAceitacao.Items(0).Selected = False
        RdoAceitacao.Items(1).Selected = False
        lblRecusa.Visible = False
        lnkVisualizaProcesso.Visible = False
        lblMsg.Text = ""
        txthonorarioJuiz.Text = ""
        lblSegAceitacao.Text = ""

    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Limpar()
    End Sub

    Protected Sub btnGrdExibir_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        'Dim vetor(3) As String
        'Dim Num_Aceitacoes_Proc As Integer
        'For i As Integer = 0 To 3
        ' vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
        'Next
        Dim vetor(6) As String
        Dim Num_Aceitacoes_Proc As Integer
        For i As Integer = 0 To 6
            vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
        Next
        TxtMotivo.Text = ""
        txtHonorarios.Text = ""
        lblMsg.Text = ""
        txtNum_CNJ.Text = Trim(vetor(0).ToString)
        txtData_Nomeacao.Text = vetor(1)

        Dim EntProc As New EntPROC_CNJ
        Dim EntProcPer As New EntProcesso_Perito
        Dim EntAceitaPer As New EntAceitacao_Perito
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim BalAceitaPer As New BalAceitacao_Perito(GetUsuario)
        'Dim sMsg As String = "Ao aceitar nomeação, por favor, informe o valor do honorário em UFIR. Ao recusar, informe o motivo."
        Dim sMsg As String = ""
        Dim m_ID_Nomeacao As Integer
        Dim Num_Negacoes_Proc As Integer

        lnkVisualizaProcesso.Visible = True

        EntProc = BalP.ExibirDadosEnt("", txtNum_CNJ.Text)
        If EntProc Is Nothing Then
            MsgErro("Falha na consulta do processo.")
            Exit Sub
        End If

        EntProcPer = BalProcPer.ExibirDadosEnt(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text), vetor(2), vetor(3), CDate(txtData_Nomeacao.Text)) 'ent.ID_PF)

        Dim N_Data_Aceitacao As String = ""
        Dim N_Data_Negacao As String = ""
        Dim mTipo As Integer = 1
        Dim BalValor As New BalValor_Pericia(GetUsuario)

        N_Data_Aceitacao = vetor(5)
        N_Data_Negacao = vetor(6)

        'EntProcPer = BalProcPer.ExibirDadosEnt(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text), vetor(2), vetor(3), CDate(txtData_Nomeacao.Text)) 'ent.ID_PF)
        m_ID_Nomeacao = EntProcPer.ID_Nomeacao
        EntAceitaPer = BalAceitaPer.ExibirDadosAceitaEntNew(m_ID_Nomeacao, CDate(IIf(N_Data_Aceitacao <> "", N_Data_Aceitacao, Nothing)), CDate(IIf(N_Data_Negacao <> "", N_Data_Negacao, Nothing)))

        If txtHonorarios.Text = "" And m_Justica_Grat Then
            mTipo = 1
            m_Valor_Justica_Gratuita = BalValor.ValorJG(mTipo)
            txtHonorarios.Text = m_Valor_Justica_Gratuita.ToString
        End If

        If Not EntProcPer Is Nothing Then
            Dim entEspPerito As EntEspecialidade_Perito
            Dim balEspPerito As New BALEspecialidadePerito(GetUsuario)
            Dim balOrgProfissional As New BalOrgao_Per(GetUsuario)
            Dim entOrgProfissional As EntOrgao_per
            Dim mm_ID_Nomeacao As Integer

            If EntProcPer.IndTacita = "S" Then
                Tacita = True
            Else
                Tacita = False
            End If
            txtNum_Processo.Text = EntProc.Cod_Proc

            mm_ID_Nomeacao = EntProcPer.ID_Nomeacao

            entEspPerito = balEspPerito.Consultar(CLng(txtID_PF.Text), EntProcPer.COD_PROFISSAO, EntProcPer.COD_ESPECIALIDADE)
            txtNum_Reg.Text = IIf(entEspPerito.Num_Reg = "", String.Empty, entEspPerito.Num_Reg & " - " & entEspPerito.UF).ToString

            Session("CodProfissao") = entEspPerito.COD_PROFISSAO
            Session("CodEspecialidade") = entEspPerito.COD_ESPECIALIDADE

            entOrgProfissional = balOrgProfissional.ConsultarOrgProfissional(CStr(entEspPerito.COD_ORGAO_PER))
            If entOrgProfissional Is Nothing Then
                Session("CodOrgaoProfissional") = 0
                Session("NumReg") = ""
                txtOrgaoProfissional.Text = ""
            Else
                Session("CodOrgaoProfissional") = entOrgProfissional.COD_ORGAO_PER
                Session("NumReg") = entEspPerito.Num_Reg
                txtOrgaoProfissional.Text = entOrgProfissional.DESCR_ORGAO_PER & ""
            End If

            txtProfissao.Text = EntProcPer.DESCR_PROFISSAO
            txtEspecialidade.Text = EntProcPer.DESCR_ESPECIALIDADE
            lblPrazo.Text = EntProcPer.PRAZO_ENTREGA.ToString
            txtID_Nomeacao.Text = EntProcPer.ID_Nomeacao.ToString


            If Not (EntProcPer.Data_Inicio_Per.ToString Is Nothing Or EntProcPer.Data_Inicio_Per.ToString = "01/01/0001 00:00:00" Or EntProcPer.Data_Inicio_Per.ToString = "1/1/0001 00:00:00" Or _
                    EntProcPer.Data_Inicio_Per.ToString = "01/01/0001 0:00:00" Or EntProcPer.Data_Inicio_Per.ToString = "1/1/0001 0:00:00") Then
                m_DataInicioPer = CStr(EntProcPer.Data_Inicio_Per)
            End If

            TxtMotivo.Visible = False
            lblRecusa.Visible = False
            txtHonorarios.Visible = False
            txtHonorarioJuiz.Visible = False
            lblHonorarioJuiz.Visible = False
            lblRefHonJuiz.Visible = False
            lblMsgHon.Visible = False
            lblRefHonJuiz.Visible = False
            lblRefHon.Visible = False
            RdoAceitacao.Items(0).Selected = False
            RdoAceitacao.Items(1).Selected = False
            If EntProcPer.Justica_Gratuita = "S" Then
                m_Justica_Grat = True
            Else
                m_Justica_Grat = False
            End If
            'If m_Justica_Grat Then
            '    txtHonorarios.Enabled = False
            '    txtHonorarios.Visible = True
            '    If txtHonorarios.Text = "" Then
            '        txtHonorarios.Text = m_Valor_Justica_Gratuita
            '    End If
            'End If
            Dim m_HonorarioJuiz As String
            m_HonorarioJuiz = IIf(EntProcPer.HonorarioJuiz = 0, "", EntProcPer.HonorarioJuiz).ToString
            txtHonorarioJuiz.Text = m_HonorarioJuiz
            If txtHonorarioJuiz.Text <> "" Then
                txtHonorarios.Enabled = False
                txtHonorarios.Visible = True
                lblMsgHon.Visible = True
                lblRefHon.Visible = True
                txtHonorarioJuiz.Visible = True
                lblHonorarioJuiz.Visible = True
                lblRefHonJuiz.Visible = True
            End If
            If EntProcPer.Justica_Gratuita = "S" Then
                txtHonorarios.Enabled = False
                txtHonorarios.Visible = False
                lblMsgHon.Visible = False
                lblRefHon.Visible = False
                lblRefHonJuiz.Visible = False
                txtHonorarioJuiz.Visible = False
                lblRefHonJuiz.Visible = False
            Else
                txtHonorarios.Visible = True
                lblMsgHon.Visible = True
                lblRefHon.Visible = True
            End If

            Num_Negacoes_Proc = 0
            Num_Negacoes_Proc = BalAceitaPer.Qte_Negacoes_Proc(txtID_Nomeacao.Text)

            Num_Aceitacoes_Proc = 0
            Num_Aceitacoes_Proc = BalAceitaPer.Qte_Aceitacoes_Proc(txtID_Nomeacao.Text)

            If Not EntAceitaPer Is Nothing Then
                m_Cod_Tipo_Pericia = EntProcPer.COD_TIPO_PERICIA
            End If

            If Not EntAceitaPer Is Nothing And Num_Aceitacoes_Proc <> 0 Then
                If Not (EntAceitaPer.Data_Aceitacao.ToString Is Nothing Or EntAceitaPer.Data_Aceitacao.ToString = "#12:00:00AM#") Then
                    If Not EntAceitaPer.Data_Aceitacao.ToString = "01/01/0001 00:00:00" And Not EntAceitaPer.Data_Aceitacao.ToString = "01/01/0001 0:00:00" And Not EntAceitaPer.Data_Aceitacao.ToString = "1/1/0001 00:00:00" And Not EntAceitaPer.Data_Aceitacao.ToString = "1/1/0001 0:00:00" Then
                        RdoAceitacao.Items(0).Selected = True
                        txtHonorarios.Visible = True
                        lblRefHon.Visible = True
                        lblMsgHon.Visible = True
                    End If
                End If
                If Not (EntAceitaPer.Data_Negacao.ToString Is Nothing Or EntAceitaPer.Data_Negacao.ToString = "#12:00:00AM#") Then
                    If Not EntAceitaPer.Data_Negacao.ToString = "01/01/0001 00:00:00" And Not EntAceitaPer.Data_Negacao.ToString = "1/1/0001 00:00:00" And Not EntAceitaPer.Data_Negacao.ToString = "01/01/0001 0:00:00" And Not EntAceitaPer.Data_Negacao.ToString = "1/1/0001 0:00:00" Then
                        RdoAceitacao.Items(1).Selected = True
                        TxtMotivo.Visible = True
                        lblRecusa.Visible = True
                    End If
                End If

                If Not EntAceitaPer.Honorarios.ToString Is Nothing Then
                    If EntAceitaPer.Honorarios = 0 Then
                        txtHonorarios.Text = ""
                    Else
                        txtHonorarios.Text = EntAceitaPer.Honorarios.ToString
                    End If
                End If

                If Not EntAceitaPer.Motivo_Recusa Is Nothing And EntAceitaPer.Motivo_Recusa <> "" Then
                    TxtMotivo.Text = EntAceitaPer.Motivo_Recusa.ToString
                End If
                If RdoAceitacao.Items(1).Selected Then
                    TxtMotivo.Enabled = False
                    RdoAceitacao.Enabled = False
                    BtnGravar.Enabled = False
                    'txthonorarioJuiz.Enabled = False
                Else
                    If (Not EntAceitaPer.Data_Aceitacao.ToString = "01/01/0001 00:00:00" And Not EntAceitaPer.Data_Aceitacao.ToString = "01/01/0001 0:00:00" And Not EntAceitaPer.Data_Aceitacao.ToString = "1/1/0001 00:00:00" And Not EntAceitaPer.Data_Aceitacao.ToString = "1/1/0001 0:00:00") Or _
                        (Not EntAceitaPer.Data_Aceitacao.ToString = "#12:00:00AM#") Or _
                        (Not EntAceitaPer.Data_Negacao.ToString = "01/01/0001 00:00:00" And Not EntAceitaPer.Data_Negacao.ToString = "1/1/0001 00:00:00" And Not EntAceitaPer.Data_Negacao.ToString = "01/01/0001 0:00:00" And Not EntAceitaPer.Data_Negacao.ToString = "1/1/0001 0:00:00") Or _
                        (Not EntAceitaPer.Data_Negacao.ToString = "#12:00:00AM#") Then
                        If txtHonorarioJuiz.Text <> txtHonorarios.Text And RdoAceitacao.Items(0).Selected And Trim(txtHonorarioJuiz.Text) <> "" Then
                            'RdoAceitacao.Items(0).Selected 'Aceito
                            'RdoAceitacao.Items(1).Selected 'Negado
                            RdoAceitacao.Enabled = True
                            txtHonorarios.Enabled = False
                        Else
                            TxtMotivo.Enabled = False
                            BtnGravar.Enabled = False
                            sMsg = ""
                        End If
                    Else
                        TxtMotivo.Enabled = True
                        txtHonorarios.Enabled = True
                        RdoAceitacao.Enabled = True
                        BtnGravar.Enabled = True
                    End If
                End If
                If txtHonorarioJuiz.Text <> "" Then
                    txtHonorarios.Enabled = False
                Else
                    lblHonorarioJuiz.Visible = False
                    lblRefHonJuiz.Visible = False
                    txtHonorarioJuiz.Visible = False
                End If
                '22/05/2012
                'm_Cod_Tipo_Pericia = EntProcPer.COD_TIPO_PERICIA
                m_Data_do_Aceite = EntAceitaPer.Data_Aceitacao

            Else
                If EntAceitaPer.Data_Negacao.ToString <> "01/01/0001 00:00:00" And EntAceitaPer.Data_Negacao.ToString <> "1/1/0001 00:00:00" And _
                    EntAceitaPer.Data_Negacao.ToString <> "01/01/0001 0:00:00" And EntAceitaPer.Data_Negacao.ToString <> "1/1/0001 0:00:00" And _
                    EntAceitaPer.Data_Negacao.ToString <> "#12:00:00AM#" Then
                    'If Not EntAceitaPer.Data_Negacao.ToString Is Nothing Then
                    RdoAceitacao.Enabled = False
                    BtnGravar.Enabled = False
                    RdoAceitacao.Enabled = False
                    RdoAceitacao.Items(1).Selected = True
                    If Not EntAceitaPer.Motivo_Recusa Is Nothing And EntAceitaPer.Motivo_Recusa <> "" Then
                        TxtMotivo.Text = EntAceitaPer.Motivo_Recusa.ToString
                    End If
                    txtHonorarios.Visible = False
                    txtHonorarioJuiz.Visible = False
                    lblHonorarioJuiz.Visible = False
                    'lblRefHonJuiz.Visible = False
                    lblMsgHon.Visible = False
                    lblRefHonJuiz.Visible = False
                    lblRefHon.Visible = False
                    TxtMotivo.Visible = True
                    TxtMotivo.Enabled = False
                    'End If
                End If
                If (EntAceitaPer.Data_Aceitacao.ToString <> "01/01/0001 00:00:00" And EntAceitaPer.Data_Aceitacao.ToString <> "1/1/0001 00:00:00" And _
                    EntAceitaPer.Data_Aceitacao.ToString <> "01/01/0001 0:00:00" And EntAceitaPer.Data_Aceitacao.ToString <> "1/1/0001 0:00:00") And _
                   EntAceitaPer.Data_Aceitacao.ToString <> "#12:00:00#" Then
                    If Not EntAceitaPer.Data_Aceitacao.ToString Is Nothing Then
                        RdoAceitacao.Enabled = False
                        RdoAceitacao.Items(0).Selected = True
                        If Not EntAceitaPer.Honorarios.ToString Is Nothing Then
                            If EntAceitaPer.Honorarios = 0 Then
                                txtHonorarios.Text = ""
                            Else
                                txtHonorarios.Text = EntAceitaPer.Honorarios.ToString
                            End If
                        End If
                        BtnGravar.Enabled = False
                        TxtMotivo.Visible = False
                        TxtMotivo.Enabled = False
                    End If
                End If
                If (EntAceitaPer.Data_Aceitacao.ToString = "01/01/0001 00:00:00" Or EntAceitaPer.Data_Aceitacao.ToString = "1/1/0001 00:00:00" Or _
                    EntAceitaPer.Data_Aceitacao.ToString = "01/01/0001 0:00:00" Or EntAceitaPer.Data_Aceitacao.ToString = "1/1/0001 0:00:00" Or _
                    EntAceitaPer.Data_Aceitacao.ToString = "#12:00:00#") And _
                    (EntAceitaPer.Data_Negacao.ToString = "01/01/0001 00:00:00" Or EntAceitaPer.Data_Negacao.ToString = "1/1/0001 0:00:00" Or _
                     EntAceitaPer.Data_Negacao.ToString = "01/01/0001 0:00:00" Or EntAceitaPer.Data_Negacao.ToString = "1/1/0001 00:00:00" Or _
                     EntAceitaPer.Data_Negacao.ToString = "#12:00:00#") Then
                    BtnGravar.Enabled = True
                    RdoAceitacao.Items(1).Selected = False
                    RdoAceitacao.Items(0).Selected = False
                    RdoAceitacao.Enabled = True
                    'txtHonorarios.Visible = False
                    txtHonorarioJuiz.Visible = False
                    lblHonorarioJuiz.Visible = False
                    lblRefHonJuiz.Visible = False
                    'lblMsgHon.Visible = False
                    lblRefHonJuiz.Visible = False
                    'lblRefHon.Visible = False
                    'End If
                End If
                If txtHonorarioJuiz.Text <> "" Then
                    txtHonorarios.Enabled = False
                Else
                    lblHonorarioJuiz.Visible = False
                    lblRefHonJuiz.Visible = False
                    txtHonorarioJuiz.Visible = False
                End If
            End If
        End If
        lblSegAceitacao.Text = ""
        If Num_Aceitacoes_Proc + Num_Negacoes_Proc = 2 Then
            RdoAceitacao.Enabled = False
            TxtMotivo.Enabled = False
            BtnGravar.Enabled = False
        ElseIf Num_Aceitacoes_Proc + Num_Negacoes_Proc = 1 Then
            If Not EntAceitaPer.Data_Aceitacao.ToString Is Nothing Then
                If txtHonorarioJuiz.Text <> "" Then
                    lblSegAceitacao.Text = "ACEITAÇÃO/RECUSA APÓS FIXAÇÃO DOS HONORÁRIOS PELO JUÍZO"
                    RdoAceitacao.Items(0).Selected = False
                    RdoAceitacao.Items(1).Selected = False
                    RdoAceitacao.Enabled = True
                    BtnGravar.Enabled = True
                    txtHonorarios.Enabled = False
                End If
            Else
                RdoAceitacao.Enabled = False
                TxtMotivo.Enabled = False
                BtnGravar.Enabled = False
            End If
        End If
        If m_Justica_Grat Then
            'txtHonorarios.Visible = False
            txtHonorarios.Enabled = False
            'txtHonorarioJuiz.Visible = False
            'lblHonorarioJuiz.Visible = False
            lblRefHonJuiz.Visible = False
            lblMsgHon.Visible = False
            lblRefHonJuiz.Visible = False
            lblRefHon.Visible = False
        End If
        txtHonorarioJuiz.Enabled = False
        If RdoAceitacao.Items(0).Selected = False And RdoAceitacao.Items(1).Selected = False Then
            BtnGravar.Enabled = False
        End If
        If m_DataInicioPer <> "" Then
            BtnGravar.Visible = False
            txtHonorarios.Enabled = False
            TxtMotivo.Enabled = False
            RdoAceitacao.Enabled = False
        End If
        If sMsg <> "" Then
            MsgErro(sMsg)
        End If
        If Tacita Then
            lblMsgTacita.Visible = True
            ChkComp_Deleg.Enabled = False
            RdoAceitacao.Enabled = False
            TxtMotivo.Enabled = False
            BtnGravar.Enabled = False
        Else
            lblMsgTacita.Visible = False
            ChkComp_Deleg.Enabled = True
        End If
        If Num_Aceitacoes_Proc + Num_Negacoes_Proc = 2 Then
            RdoAceitacao.Enabled = False
            TxtMotivo.Enabled = False
        End If
        If Num_Negacoes_Proc > 0 Then
            RdoAceitacao.Enabled = False
            TxtMotivo.Enabled = False
            txtHonorarios.Enabled = False
        End If
        If EntProcPer.Justica_Gratuita = "S" Then
            lblMsgJG.Visible = True
        Else
            lblMsgJG.Visible = False
        End If
        If EntProcPer.Interdicao_Per = "S" Then
            lblInterdicao.Visible = True
            Psiquiatrica = True
        Else
            lblInterdicao.Visible = False
            Psiquiatrica = False
        End If
        If EntProcPer.Justica_Gratuita = "S" And (Num_Negacoes_Proc = 1 Or Num_Aceitacoes_Proc = 1) Then
            RdoAceitacao.Enabled = False
            BtnGravar.Enabled = False
        End If
        If EntProcPer.HonorarioJuiz.ToString Is Nothing Then
            lblHonorarioJuiz.Visible = False
            txtHonorarioJuiz.Visible = False
            lblRefHonJuiz.Visible = False
            'Else
            'lblHonorarioJuiz.Visible = True
            'txtHonorarioJuiz.Visible = True
        End If
        '18/05/2012
        If m_Justica_Grat Then
            txtHonorarios.Enabled = False
            txtHonorarios.Visible = True
            lblRefHon.Visible = True
            lblMsgHon.Visible = True
            'txtHonorarios.Text = CStr(BalValorPer.Valor_Pericia_Padrao(m_Cod_Tipo_Pericia, m_Data_do_Aceite))
            txtHonorarios.Text = CStr(BalValor.Valor_Pericia_Padrao(m_Cod_Tipo_Pericia, m_Data_do_Aceite))
            ChkComp_Deleg.Visible = True
            'If txtHonorarios.Text = "" Then
            '   txtHonorarios.Text = m_Valor_Justica_Gratuita
            'End If
        Else
            ChkComp_Deleg.Visible = False
        End If
        'm_Valor_Justica_Gratuita = Bal....(Psiq) ... Tabela de Valores das perícias gratuita com data de início da vigência


    End Sub

    Protected Sub GrdProcessos_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)

        Dim index As Integer
        If e.CommandArgument.ToString.Length > 1 Then
            index = (Convert.ToInt32(Split(e.CommandArgument.ToString, ",")(4))) - 1
            If GrdProcessos.PageIndex > 0 Then
                index = (index - (GrdProcessos.PageIndex * 10))
            End If
            GrdProcessos.Rows(index).Font.Bold = True
            'Limpa selecao anterior
            If CInt(Session("linha")) <> index And GrdProcessos.Rows.Count > CInt(Session("linha")) Then
                GrdProcessos.Rows(CInt(Session("linha"))).Font.Bold = False
            End If
            Session("linha") = index
        End If

    End Sub

    'Protected Sub GrdProcesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdProcessos.SelectedIndexChanged

    'TxtMotivo.Text = ""
    'txtHonorarios.Text = ""
    'lblMsg.Text = ""
    '    If GrdProcessos.SelectedRow.Cells(1).Text <> "" Then
    '        txtNum_CNJ.Text = GrdProcessos.SelectedRow.Cells(1).Text
    '        txtData_Nomeacao.Text = GrdProcessos.SelectedRow.Cells(2).Text
    '        Dim EntProc As EntPROC_CNJ
    '        Dim EntProcPer As EntProcesso_Perito
    '        Dim BalP As New BalProc_CNJ(GetUsuario)
    '        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
    '        Dim sMsg As String = "Ao aceitar nomeação, por favor, informe o valor do honorário em UFIR. Ao recusar, informe o motivo."


    '        EntProc = BalP.ExibirDadosEnt("", txtNum_CNJ.Text)
    '        lnkVisualizaProcesso.Visible = True
    '        If EntProc Is Nothing Then
    '            'If txtNum_Processo.Text = "" Then
    '            'txtNum_Processo.Text = EntProc.Cod_Proc
    '            'End If
    '            EntProcPer = BalProcPer.ExibirDadosEnt(EntProc.Cod_CNJ, Convert.ToInt64(txtID_PF.Text), "", "", CDate(txtData_Nomeacao.Text)) 'ent.ID_PF)
    '        Else
    '            EntProcPer = BalProcPer.ExibirDadosEnt(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text), "", "", CDate(txtData_Nomeacao.Text)) 'ent.ID_PF)

    '        End If

    '        If Not EntProcPer Is Nothing Then
    '            Dim entEspPerito As EntEspecialidade_Perito
    '            Dim balEspPerito As New BALEspecialidadePerito(GetUsuario)
    '            Dim balOrgProfissional As New BalOrgao_Per(GetUsuario)
    '            Dim entOrgProfissional As EntOrgao_per

    '            lblMsg.Text = IIf(EntProcPer.IndTacita = "S", "Aceitação tácita.", "").ToString

    '            txtNum_Processo.Text = EntProc.Cod_Proc

    '            entEspPerito = balEspPerito.Consultar(CLng(txtID_PF.Text), EntProcPer.COD_PROFISSAO, EntProcPer.COD_ESPECIALIDADE)
    '            txtNum_Reg.Text = IIf(entEspPerito.Num_Reg = "", String.Empty, entEspPerito.Num_Reg & " - " & entEspPerito.UF).ToString

    '            entOrgProfissional = balOrgProfissional.ConsultarOrgProfissional(CStr(entEspPerito.COD_ORGAO_PER))
    '            If entOrgProfissional Is Nothing Then
    '                Session("CodOrgaoProfissional") = 0
    '                Session("NumReg") = ""
    '                txtOrgaoProfissional.Text = ""
    '            Else
    '                Session("CodOrgaoProfissional") = entOrgProfissional.COD_ORGAO_PER
    '                Session("NumReg") = entEspPerito.Num_Reg
    '                txtOrgaoProfissional.Text = entOrgProfissional.DESCR_ORGAO_PER & ""
    '            End If

    '            txtProfissao.Text = EntProcPer.DESCR_PROFISSAO
    '            txtEspecialidade.Text = EntProcPer.DESCR_ESPECIALIDADE
    '            lblPrazo.Text = EntProcPer.PRAZO_ENTREGA.ToString
    '            TxtMotivo.Visible = False
    '            lblRecusa.Visible = False
    '            RdoAceitacao.Items(0).Selected = False
    '            RdoAceitacao.Items(1).Selected = False

    '            If Not EntProcPer.Data_Aceitacao.ToString Is Nothing And Not EntProcPer.Data_Aceitacao.ToString = "01/01/0001 00:00:00" And Not EntProcPer.Data_Aceitacao.ToString = "01/01/0001 0:00:00" And Not EntProcPer.Data_Aceitacao.ToString = "1/1/0001 00:00:00" And Not EntProcPer.Data_Aceitacao.ToString = "1/1/0001 0:00:00" Then
    '                RdoAceitacao.Items(0).Selected = True
    '                txtHonorarios.Visible = True
    '            End If

    '            If Not EntProcPer.Data_Negacao.ToString Is Nothing And Not EntProcPer.Data_Negacao.ToString = "01/01/0001 00:00:00" And Not EntProcPer.Data_Negacao.ToString = "1/1/0001 00:00:00" And Not EntProcPer.Data_Negacao.ToString = "01/01/0001 0:00:00" And Not EntProcPer.Data_Negacao.ToString = "1/1/0001 0:00:00" Then
    '                RdoAceitacao.Items(1).Selected = True
    '                TxtMotivo.Visible = True
    '                lblRecusa.Visible = True
    '            End If

    '            If Not EntProcPer.Honorarios.ToString Is Nothing Then
    '                If EntProcPer.Honorarios = 0 Then
    '                    txtHonorarios.Text = ""
    '                Else
    '                    txtHonorarios.Text = EntProcPer.Honorarios.ToString
    '                End If
    '                'txtHonorarios.Text = EntProcPer.Honorarios.ToString
    '            End If

    '            If Not EntProcPer.Motivo_Recusa Is Nothing Then
    '                TxtMotivo.Text = EntProcPer.Motivo_Recusa.ToString
    '            End If

    '            If (Not EntProcPer.Data_Aceitacao.ToString = "01/01/0001 00:00:00" And Not EntProcPer.Data_Aceitacao.ToString = "01/01/0001 0:00:00" And Not EntProcPer.Data_Aceitacao.ToString = "1/1/0001 00:00:00" And Not EntProcPer.Data_Aceitacao.ToString = "1/1/0001 0:00:00") Or
    '                (Not EntProcPer.Data_Negacao.ToString = "01/01/0001 00:00:00" And Not EntProcPer.Data_Negacao.ToString = "1/1/0001 00:00:00" And Not EntProcPer.Data_Negacao.ToString = "01/01/0001 0:00:00" And Not EntProcPer.Data_Negacao.ToString = "1/1/0001 0:00:00") Then
    '                TxtMotivo.Enabled = False
    '                RdoAceitacao.Enabled = False
    '                BtnGravar.Enabled = False
    '                sMsg = ""
    '            Else
    '                TxtMotivo.Enabled = True
    '                txtHonorarios.Enabled = True
    '                RdoAceitacao.Enabled = True
    '                BtnGravar.Enabled = True
    '            End If
    '        End If

    '        If sMsg <> "" Then
    '            MsgErro(sMsg)
    '        End If
    '    End If

    'End Sub

    Protected Sub GrdProcessos_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        GrdProcessos.PageIndex = e.NewPageIndex
        PreencherGrdProcesso(CLng(txtID_PF.Text))
    End Sub

    Private Sub PreencherGrdProcesso(ByVal m_ID_PF As Long)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim DsProcPer As DataSet
        DsProcPer = BalProcPer.ExibirDadosSetPer(m_ID_PF)
        GrdProcessos.DataSource = DsProcPer.Tables(0)
        GrdProcessos.DataBind()

    End Sub

    Protected Sub RdoAceitacao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdoAceitacao.SelectedIndexChanged

        If RdoAceitacao.Items(1).Selected Then 'Recusa
            If txtHonorarioJuiz.Text = "" Then
                TxtMotivo.Visible = True
                TxtMotivo.Enabled = True
                lblRecusa.Visible = True
                txtHonorarios.Visible = False
                txtHonorarios.Enabled = False
                txtHonorarioJuiz.Visible = False
                lblHonorarioJuiz.Visible = False
                lblMsgHon.Visible = False
                lblRefHonJuiz.Visible = False
                lblRefHon.Visible = False
                BtnGravar.Enabled = True
                MsgErro("Ao recusar a nomeação, por favor, informe o motivo.")
            Else
                TxtMotivo.Visible = True
                TxtMotivo.Enabled = True
                lblRecusa.Visible = True
                txtHonorarios.Visible = True
                txtHonorarios.Enabled = False
                txtHonorarioJuiz.Visible = True
                lblHonorarioJuiz.Visible = True
                lblMsgHon.Visible = True
                lblRefHonJuiz.Visible = True
                lblRefHon.Visible = True
                BtnGravar.Enabled = True
                MsgErro("Ao recusar a nomeação, por favor, informe o motivo.")
            End If
        Else  'Aceitação
            TxtMotivo.Visible = False
            TxtMotivo.Enabled = False
            lblRecusa.Visible = False
            txtHonorarios.Visible = True
            txtHonorarioJuiz.Visible = True
            lblHonorarioJuiz.Visible = True
            lblRefHonJuiz.Visible = True
            lblMsgHon.Visible = True
            lblRefHonJuiz.Visible = True
            lblRefHon.Visible = True
            BtnGravar.Enabled = True
            If lblSegAceitacao.Text = "" And Not lblMsgJG.Visible Then
                txtHonorarios.Enabled = True
                MsgErro("Ao aceitar a nomeação, por favor, informe o valor do honorário em UFIR.")
            Else
                If lblMsgJG.Visible And ChkComp_Deleg.Checked Then
                    txtHonorarios.Enabled = True
                    MsgErro("Ao aceitar a nomeação, por favor, informe o valor do honorário em UFIR.")
                Else
                    txtHonorarios.Enabled = False
                End If
            End If
            'If lblMsgJG.Visible Then
            '    lblMsgJG.Visible = True
            '    'txtHonorarios.Visible = False
            '    'txtHonorarioJuiz.Visible = False
            '    'lblHonorarioJuiz.Visible = False
            '    'lblMsgHon.Visible = False
            '    'lblRefHonJuiz.Visible = False
            '    'lblRefHon.Visible = False
            'End If
        End If
        If lblSegAceitacao.Text = "" Then
            txtHonorarioJuiz.Visible = False
            lblHonorarioJuiz.Visible = False
        End If
    End Sub

    Protected Sub lnkVisualizaProcesso_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkVisualizaProcesso.Click
        If txtNum_Processo.Text = "" Then
            MsgErro("Selecione um processo da lista.")
            Exit Sub
        End If
        'Response.Redirect("http://tjerj314.tjrj.jus.br/consultaProcessoWebH/consultaProc.do?v=2&FLAGNOME=&back=1&tipoConsulta=publica&numProcesso=" & txtNum_Processo.Text)

        'Homologação http://tjerj307.tjrj.jus.br/portalDeServicos/montarProcesso?txtNumero=2008.001.00081-6&codCnj=0000082-48.2008.8.19.0001&codTipProc=1 
        'Produção http://www4.tjrj.jus.br/portalDeServicos/montarProcesso?txtNumero=2008.001.00081-6&codCnj=0000082-48.2008.8.19.0001&codTipProc=1 

        'Consulta Antiga -> Dim url As String = "http://tjerj314.tjrj.jus.br/consultaProcessoWebH/consultaProc.do?v=2&FLAGNOME=&back=1&tipoConsulta=publica&numProcesso=" & txtNum_Processo.Text

        Dim url As String

        Try

            If App.ConfigurationManager.AppSettings("AMBIENTE") <> "3" Then
                url = "http://tjerj307.tjrj.jus.br/portalDeServicos/montarProcesso?txtNumero=" & txtNum_Processo.Text & "&codCnj=" & txtNum_CNJ.Text & "&codTipProc=1 "
            Else
                'url = " http://www4.tjrj.jus.br/portalDeServicos/montarProcesso?txtNumero=" & txtNum_Processo.Text & Chr(38) & "codCnj=" & txtNum_CNJ.Text & Chr(38) & "codTipProc=1 "
                url = " http://www4.tjrj.jus.br/portalDeServicos/montarProcesso?txtNumero=" & txtNum_Processo.Text & "&codCnj=" & txtNum_CNJ.Text & "&codTipProc=1 "
            End If

            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ConsultaProcesso", "window.open('" & url & "', '_blank', 'height=600,width=850, Top=50,left=120,scrollbars=yes,toolbar=no,menubar=no');", True)

        Catch
            url = "http://tjerj314.tjrj.jus.br/consultaProcessoWebH/consultaProc.do?v=2&FLAGNOME=&back=1&tipoConsulta=publica&numProcesso=" & txtNum_Processo.Text
            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ConsultaProcesso", "window.open('" & url & "', '_blank', 'height=600,width=850, Top=50,left=120,scrollbars=yes,toolbar=no,menubar=no');", True)
        Finally

        End Try

    End Sub

    Protected Sub txtHonorarios_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtHonorarios.TextChanged

        BtnGravar.Enabled = True

    End Sub

    Protected Sub ChkComp_Deleg_CheckedChanged(sender As Object, e As EventArgs) Handles ChkComp_Deleg.CheckedChanged

        Dim BalValorP As New BalValor_Pericia(GetUsuario)

        If ChkComp_Deleg.Checked Then
            txtHonorarios.Enabled = True
            txtHonorarios.Visible = True
            lblRefHon.Visible = True
            lblMsgHon.Visible = True
            If RdoAceitacao.SelectedIndex = 0 Then
                txtHonorarios.Enabled = True
            Else
                txtHonorarios.Enabled = False
            End If
        Else
            txtHonorarios.Enabled = False
            txtHonorarios.Visible = False
            txtHonorarios.Text = CStr(BalValorP.Valor_Pericia_Padrao(m_Cod_Tipo_Pericia, m_Data_do_Aceite))
            lblRefHon.Visible = False
            lblMsgHon.Visible = False
        End If

    End Sub

    Protected Sub GrdProcessos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdProcessos.SelectedIndexChanged

    End Sub
End Class



