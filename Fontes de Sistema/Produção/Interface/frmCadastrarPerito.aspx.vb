Option Strict Off

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET
Imports log4net
Imports System.IO

Partial Public Class frmCadastrarPerito

    'ID_PF 557888 - JULIO CESAR MONTE SANTO para teste.
    Inherits BasePage

    Public DsNur1 As New DataSet
    Public DsNur2 As New DataSet
    Public DsNur3 As New DataSet
    Public DsNur4 As New DataSet
    Public DsNur5 As New DataSet
    Public DsNur6 As New DataSet
    Public DsNur7 As New DataSet
    Public DsNur8 As New DataSet
    Public DsNur9 As New DataSet
    Public DsNur10 As New DataSet
    Public DsNur11 As New DataSet
    Public DsNur12 As New DataSet
    Public DsNur13 As New DataSet
    Public DsNurTotal As New DataSet
    Public InserirConta As Boolean = False

    'COD_PERITO               NUMfBER(4) not null,    'NOME                     VARCHAR2(250) not null,
    'NUM_REG                  VARCHAR2(15),    'COD_TIP_LOGR             NUMBER(3),
    'NOME_LOGR                VARCHAR2(100),    'NUM_LOGR                 VARCHAR2(100),
    'COMPL_LOGR               VARCHAR2(50),    'COD_BAIRRO               NUMBER(5),
    'COD_CIDADE               NUMBER(10),    'DDD                      VARCHAR2(2),
    'CEP                      NUMBER(8),    'TEL                      VARCHAR2(35),
    'RAMAL                    VARCHAR2(4),    'SITUACAO                 CHAR(1) not null,
    'OBS                      VARCHAR2(250),    'EMAIL                    VARCHAR2(100),
    'FALTA_ENTREGAR           VARCHAR2(100),    'COD_ESPECIALIDADE        NUMBER,
    'DESCR_TEMP_ESPECIALIDADE VARCHAR2(100),    'SIGLA                    VARCHAR2(20),
    'DATA_CADASTRAMENTO       DATE

    Dim logger As log4net.ILog

    Private ent As New EntPERITO
    Dim i, j As Integer

    Private Sub RemoveSessoes()
        Session.Remove("DsNur1")
        Session.Remove("DsNur2")
        Session.Remove("DsNur3")
        Session.Remove("DsNur4")
        Session.Remove("DsNur5")
        Session.Remove("DsNur6")
        Session.Remove("DsNur7")
        Session.Remove("DsNur8")
        Session.Remove("DsNur9")
        Session.Remove("DsNur10")
        Session.Remove("DsNur11")
        Session.Remove("DsNur12")
        Session.Remove("DsNur13")
        Session.Remove("DsNur")
        Session.Remove("NumNur")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'ClientScript.RegisterClientScriptInclude(Me.GetType(), "ScriptDeInicializacao", "JS/InicializaComponentes.js")
        'ScriptManager.RegisterClientScriptInclude(Me, Me.GetType(), "ScriptDeInicializacao", "JS/InicializaComponentes.js")

        'CarregaEventosAssincronos()

        logger = log4net.LogManager.GetLogger("LogInFile")
        logger.Debug("Acesso ao cadastro de Perito ...")

        'lblValidaNome.Text = ""
        'lblValidaCPF.Text = ""
        btnGravarDocumento.Attributes.Add("onclick", "document.body.style.cursor = 'wait'; this.value='Aguarde, Enviando...'; this.disabled = true; " + ClientScript.GetPostBackEventReference(btnGravarDocumento, String.Empty) + ";")

        If Not Me.IsPostBack Then
            Try
                RemoveSessoes()
                HabilitaBotoes(False)

                BtnNur1.BackColor = Drawing.Color.Brown
                BtnNur1.ForeColor = Drawing.Color.White
                BtnNur2.BackColor = Drawing.Color.Brown
                BtnNur2.ForeColor = Drawing.Color.White
                BtnNur3.BackColor = Drawing.Color.Brown
                BtnNur3.ForeColor = Drawing.Color.White
                BtnNur4.BackColor = Drawing.Color.Brown
                BtnNur4.ForeColor = Drawing.Color.White
                BtnNur5.BackColor = Drawing.Color.Brown
                BtnNur5.ForeColor = Drawing.Color.White
                BtnNur6.BackColor = Drawing.Color.Brown
                BtnNur6.ForeColor = Drawing.Color.White
                BtnNur7.BackColor = Drawing.Color.Brown
                BtnNur7.ForeColor = Drawing.Color.White
                BtnNur8.BackColor = Drawing.Color.Brown
                BtnNur8.ForeColor = Drawing.Color.White
                BtnNur9.BackColor = Drawing.Color.Brown
                BtnNur9.ForeColor = Drawing.Color.White
                BtnNur10.BackColor = Drawing.Color.Brown
                BtnNur10.ForeColor = Drawing.Color.White
                BtnNur11.BackColor = Drawing.Color.Brown
                BtnNur11.ForeColor = Drawing.Color.White
                BtnNur12.BackColor = Drawing.Color.Brown
                BtnNur12.ForeColor = Drawing.Color.White
                BtnNur13.BackColor = Drawing.Color.Brown
                BtnNur13.ForeColor = Drawing.Color.White
                Session("CV") = "N"
                'txtCPF.Attributes.Add("onblur", "validacpf('" & txtCPF.ClientID & "');")
                'txtDt_Nasc.Attributes.Add("onblur", "validardata('" & txtDt_Nasc.ClientID & "');")
                'BtnExcluir.Attributes.Add("OnClick", "return confirm('Confirma a Exclusão?');")
                PreencherCombo_Status()

                logger.Debug("Popula dropdown de órgão perito. ")
                PreencherOrgao_Per()
                logger.Debug("Popula dropdown  Tipo logradouro. ")
                PreencherTip_Logr()
                logger.Debug("Popula dropdown UF. ")
                PreencherUF()
                logger.Debug("Popula dropdown Cidade - RJ. ")
                PreencherCidade("RJ")
                logger.Debug("Popula dropdown Bairro - 1.")
                PreencherBairro("1")
                logger.Debug("Popula dropdown  Tipo logradouro 1. ")
                PreencherTip_Logr1()
                logger.Debug("Popula dropdown UF1. ")
                PreencherUF1()
                logger.Debug("Popula dropdown cidade 1 - RJ.")
                PreencherCidade1("RJ")
                logger.Debug("Popula dropdown Bairro 1 - 1.")
                PreencherBairro1("1")
                logger.Debug("Popula dropdown Profissão")
                'PreencherPROFISSAO()
                logger.Debug("Popula dropdown Profissão 1.")
                logger.Debug("Popula dropdown Banco. ")
                PreencherBanco()
                'txtData_Cadastramento.Data = Today.ToShortDateString
                txtData_Cadastramento.Text = Today.ToString()
                txtDataAtual.Value = CStr(Format(Today, "dd/MM/yyyy"))
            Catch ex As Exception
                logger.Error(ex.Message.ToString())
                MsgErro(ex.Message, "Erro")
            End Try
            '[enter] virando [tab]
            'txtCod_Perito.Attributes.Add("onkeydown", "if(evEnt.which==13){ evEnt.which = 9; } if(evEnt.keyCode==13){ evEnt.keyCode = 9; }")
            'txtData_Cadastramento.Data = Now.ToShortDateString
            txtData_Cadastramento.Text = Today.ToString()
        Else

        End If
    End Sub

    'Private Sub CarregaEventosAssincronos()
    '    Try
    '        txtCPF.Attributes.Add("onblur", "javascript:setTimeout('__doPostBack(\'ctl00$Tela$txtCPF\',\'ValidaCPF\')', 0)")
    '        txtCNPJ.Attributes.Add("onblur", "javascript:setTimeout('__doPostBack(\'ctl00$Tela$txtCNPJ\',\'ValidaCNPJ\')', 0)")
    '    Catch ex As Exception
    '        'MsgErro(ex.Message, "Erro")
    '    End Try
    'End Sub

    'Protected Sub up1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles up1.Load
    '    Try
    '        Dim myarg As String = Request("__EVENTARGUMENT")


    '        Select Case myarg
    '            Case "ValidaCPF"
    '                ValidaCPF()
    '            Case "ValidaCNPJ"
    '                ValidaCNPJ()
    '            Case Else

    '        End Select
    '        'Dim val As Boolean = txtCPF.Enabled
    '        'Dim txt As String = txtCPF.Text
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCPF.TextChanged
        Try
            If txtCPF.AutoPostBack Then
                ValidaCPF()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtCNPJ_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCNPJ.TextChanged
        Try
            If txtCNPJ.AutoPostBack Then
                ValidaCNPJ()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub


    Private Sub ValidaCNPJ()
        Try
            If Not String.IsNullOrEmpty(txtCNPJ.Text) Then
                Dim cnpj As String = RetornaNumerosDeString(txtCNPJ.Text)

                If Not ValidarCNPJ(cnpj) Then
                    txtCNPJ.Text = String.Empty
                    txtCNPJ.Enabled = True
                    MsgErro("CNPJ Inválido", "Erro")
                    Exit Sub
                End If
                If txtNome.Text = "" And txtNomeFantasia.Text = "" Then
                    LimparSemNome()
                    txtDt_Nasc.Text = String.Empty
                    txtID_PF.Value = String.Empty
                    txtCod_Perito.Value = String.Empty
                    CboPerito.Items.Clear()
                    CboOrgao_Per.SelectedIndex = 0
                    txtCNPJ.Text = cnpj
                    PreencherDadosPerito()
                    txtNome.AutoPostBack = False
                    txtNomeFantasia.AutoPostBack = False
                    'linhaPessoaJuridica.Visible = True
                    linhaPessoaJuridica.Attributes.Add("display", "block")
                    txtCNPJ.Enabled = False
                End If
                HabilitaBotoes(True)
            End If
            CboPerito.Enabled = False
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Private Sub ValidaCPF()
        Try
            If Not String.IsNullOrEmpty(txtCPF.Text) Then
                Dim m_CPF As String
                m_CPF = RetornaNumerosDeString(txtCPF.Text)
                If Not ValidarCPF(m_CPF) Then
                    txtCPF.Text = ""
                    txtCPF.Enabled = True
                    MsgErro("CPF Inválido", "Erro")
                    Exit Sub
                End If
                If txtNome.Text = "" Then
                    LimparSemNome()
                    txtDt_Nasc.Text = ""
                    txtID_PF.Value = ""
                    txtCod_Perito.Value = ""
                    CboPerito.Items.Clear()
                    CboOrgao_Per.SelectedIndex = 0
                    txtCPF.Text = m_CPF
                    PreencherDadosPerito()
                    txtNome.AutoPostBack = False
                    txtNomeFantasia.AutoPostBack = False
                    txtCPF.AutoPostBack = False
                End If
                HabilitaBotoes(True)
            End If
            CboPerito.Enabled = False
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Dim MsgSucesso As String = String.Empty
        Dim BalCom As New BALCOMARCA(GetUsuario)
        Dim Bal As New BALPERITO(GetUsuario)
        Dim BalPerNur As New BalPerito_Comarca(GetUsuario)

        Try

            '# VALIDAÇÕES DOS DADOS PESSOAIS #'

            Dim MsgG As String = ""

            If Not Me.IsPostBack Then
                Exit Sub
            End If
            'lblValidaNome.Text = ""
            'lblValidaCPF.Text = ""

            If txtNome.Text = "" Then
                MsgErro("Inserção rejeitada. Nome do perito não informado.", "Erro")
                ''lblValidaNome.Text = "Preencher Nome"
                Exit Sub
            End If

            If ddlTipoPessoa.Items(0).Selected Then
                If txtCPF.Text = "" Then
                    MsgErro("Inserção rejeitada. CPF do perito não informado.", "Erro")
                    ''lblValidaCPF.Text = "Preencher CPF"
                    Exit Sub
                End If
            Else
                If txtCNPJ.Text = "" Then
                    MsgErro("Inserção rejeitada. CNPJ do perito não informado.", "Erro")
                    ''lblValidaCPF.Text = "Preencher CPF"
                    Exit Sub
                End If
            End If

            'Verifica se o perito já existe e seu status atual é excluído e estão tentando alterar para outro status
            'Caso isso ocorra o sistema não pode permitir, só na Anotação do períto.
            'Caso o perito não esteja excluído e o usuário esteja tentando excluí-lo também não pode, só pela Anotação do perito.
            If Not String.IsNullOrEmpty(txtCod_Perito.Value) Then
                Dim peritoAtual As EntPERITO = Nothing
                peritoAtual = Bal.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, ddlTipoPessoa.SelectedValue)
                If peritoAtual.StatusAtual.Codigo <> 2 AndAlso ddlStatusPerito.SelectedValue = 2 Then
                    MsgErro("Um perito somente poderá ter o status alterado para Excluído através de uma anotação de exclusão.", "Erro")
                    Exit Sub
                ElseIf peritoAtual.StatusAtual.Codigo = 2 AndAlso ddlStatusPerito.SelectedValue <> 2 Then
                    MsgErro("Um perito somente poderá ter o status de exclusão alterado através de uma anotação de reativação.", "Erro")
                    Exit Sub
                End If
            End If

            If String.IsNullOrEmpty(txtCod_Perito.Value) AndAlso ddlStatusPerito.SelectedItem.Text = "Excluído" Then
                MsgErro("Inserção rejeitada. O perito não pode ser inserido com o status de Excluído", "Erro")
                Exit Sub
            End If


            If txtCod_Perito.Value = "" Then
                ent.Cod_Perito = 0
            Else
                ent.Cod_Perito = CInt(txtCod_Perito.Value)
            End If

            ent.Nome = txtNome.Text

            If ddlTipoPessoa.Items(1).Selected Then
                ent.NomeFantasia = txtNomeFantasia.Text
            End If


            If txtID_PF.Value = "" Then
                ent.ID_PF = 0
            Else
                ent.ID_PF = CInt(txtID_PF.Value)
            End If

            If ddlTipoPessoa.Items(0).Selected Then
                If String.IsNullOrEmpty(txtDt_Nasc.Text) Then
                    MsgErro("Inserção rejeitada. Data de nascimento não informada.", "Erro")
                    Exit Sub
                Else
                    ent.Dt_Nasc = CDate(txtDt_Nasc.Text)
                End If
            Else
                ent.Dt_Nasc = Nothing
            End If

            If ddlTipoPessoa.Items(0).Selected Then
                If IsNumeric(RetornaNumerosDeString(txtCPF.Text)) Then
                    ent.CPF = RetornaNumerosDeString(txtCPF.Text)
                Else
                    ent.CPF = String.Empty
                    MsgErro("O CPF é Inválido. Gravação Rejeitada.", "Erro")
                    Exit Sub
                End If
            Else
                If IsNumeric(RetornaNumerosDeString(txtCNPJ.Text)) Then
                    ent.CNPJ = RetornaNumerosDeString(txtCNPJ.Text)
                Else
                    ent.CNPJ = String.Empty
                    MsgErro("O CNPJ é Inválido. Gravação Rejeitada.", "Erro")
                    Exit Sub
                End If

            End If

            ent.TipoPessoa = IIf(ent.TipoPessoa = Nothing, ddlTipoPessoa.SelectedValue, ent.TipoPessoa)

            If ddlStatusPerito.SelectedValue.Equals("Selecione...") Then
                MsgErro("Inserção rejeitada. Status do perito não informado.", "Erro")
                Exit Sub
            Else
                ent.StatusAtual = New TIPO_STATUS
                ent.StatusAtual.Codigo = CInt(ddlStatusPerito.SelectedValue)
                ent.StatusAtual.Descricao = ddlStatusPerito.SelectedItem.Text
            End If
            '# VALIDAÇÕES DOS DADOS PESSOAIS #'



            '# É TRISTE VER ESSE CÓDIGO AQUI #'
            Dim Contador As Integer

            Dim NumComarcas As Integer

            If Not Me.IsPostBack Then
                Exit Sub
            End If

            Dim n As Integer = 0
            '# É TRISTE VER ESSE CÓDIGO AQUI #'


            '# INICIO DO CÓDIGO RESPONSÁVEL PELO CADASTRO DE ENDEREÇOS (RESIDENCIAL E COMERCIAL) #'

            If Not IsNumeric(CboTip_Logr.Text) Then
                ent.Cod_Tip_Logr = 0
            Else
                ent.Cod_Tip_Logr = CInt(CboTip_Logr.Items.FindByValue(CboTip_Logr.Text).Value)
            End If

            If Session("SeqEnd") Is Nothing Then
                ent.Seq_End = 0
            Else
                ent.Seq_End = CInt(Session("SeqEnd").ToString())
            End If

            If Session("SeqEnd1") Is Nothing Then
                ent.Seq_End1 = 0
            Else
                ent.Seq_End1 = CInt(Session("SeqEnd1").ToString())
            End If

            ent.Cod_Tip_End = 2
            ent.Cod_Tip_End1 = 1

            ent.Nome_Logr = txtNome_Logr.Text
            ent.Num_Logr = txtNum_Logr.Text
            ent.Compl_Logr = txtCompl_Logr.Text

            If Not IsNumeric(CboBairro.Text) Then
                ent.Cod_Bairro = 0
                ent.Descr_Bairro = ""
            Else
                ent.Cod_Bairro = CInt(CboBairro.Items.FindByValue(CboBairro.Text).Value)
                ent.Descr_Bairro = CboBairro.SelectedItem.Text
            End If

            If Not IsNumeric(CboCidade.Text) Then
                ent.Cod_Cidade = 0
                ent.Descr_Cidade = ""
            Else
                ent.Cod_Cidade = CInt(CboCidade.Items.FindByValue(CboCidade.Text).Value)
                ent.Descr_Cidade = CboCidade.SelectedItem.Text
            End If

            If Not IsNumeric(CboTip_Logr1.Text) Then
                ent.Cod_Tip_Logr1 = 0
                ent.Descr_Tip_Logr1 = ""
            Else
                ent.Cod_Tip_Logr1 = CInt(CboTip_Logr1.Items.FindByValue(CboTip_Logr1.Text).Value)
                ent.Descr_Tip_Logr1 = CboTip_Logr1.SelectedItem.Text
            End If

            If Not IsNumeric(CboTip_Logr.Text) Then
                ent.Cod_Tip_Logr = 0
                ent.Descr_Tip_Logr = ""
            Else
                ent.Cod_Tip_Logr = CInt(CboTip_Logr.Items.FindByValue(CboTip_Logr.Text).Value)
                ent.Descr_Tip_Logr = CboTip_Logr.SelectedItem.Text
            End If

            ent.Nome_Logr1 = txtNome_Logr1.Text
            ent.Num_Logr1 = txtNum_Logr1.Text
            ent.Compl_Logr1 = txtCompl_Logr1.Text

            If Not IsNumeric(CboBairro1.Text) Then
                ent.Cod_Bairro1 = 0
                ent.Descr_Bairro1 = ""
            Else
                ent.Cod_Bairro1 = CInt(CboBairro1.Items.FindByValue(CboBairro1.Text).Value)
                ent.Descr_Bairro1 = CboBairro1.SelectedItem.Text
            End If

            If Not IsNumeric(CboCidade1.Text) Then
                ent.Cod_Cidade1 = 0
                ent.Descr_Cidade1 = ""
            Else
                ent.Cod_Cidade1 = CInt(CboCidade1.Items.FindByValue(CboCidade1.Text).Value)
                ent.Descr_Cidade1 = CboCidade1.SelectedItem.Text
            End If

            If TxtCEP.Text <> "" And IsNumeric(TxtCEP.Text) Then
                ent.CEP = TxtCEP.Text
            Else
                ent.CEP = ""
            End If

            If TxtCEP1.Text <> "" And IsNumeric(TxtCEP1.Text) Then
                ent.CEP1 = TxtCEP1.Text
            Else
                ent.CEP1 = ""
            End If

            If Mid(UCase(CboUF.Text), 1, 9) = "SELECIONE" Then
                ent.Sigla_UF = ""
            Else
                ent.Sigla_UF = CboUF.Text 'CboUF.Items.FindByValue(CboUF.Text).Value
            End If
            If Mid(UCase(CboUF1.Text), 1, 9) = "SELECIONE" Then
                ent.Sigla_UF1 = ""
            Else
                ent.Sigla_UF1 = CboUF1.Text 'CboUF1.Items.FindByValue(CboUF1.Text).Value
            End If

            '# FIM DO CÓDIGO RESPONSÁVEL PELO CADASTRO DE ENDEREÇOS (RESIDENCIAL E COMERCIAL) #'


            '# INICIO DO CÓDIGO RESPONSÁVEL PELO CADASTRO DE INFORMAÇÕES ADICIONAIS #'

            'Teste
            'ent.Cod_Tip_Sit = CInt(IIf(RdAtivo.Items(0).Selected, 1, 2))
            ent.OBS = ""
            ent.EMAIL = txtEmail.Text '"SANTO@TJ.RJ.GOV.BR"
            ent.EMAIL1 = txtEmail1.Text
            ent.FALTA_ENTREGAR = txtFalta_Entregar.Text
            ent.SIGLA = GetUsuario.Login
            ent.Data_Cadastramento = CDate(txtData_Cadastramento.Text) 'txtData_Cadastramento.Data1)
            'ent.Indicacao = CStr(IIf(RdIndic.Items(0).Selected, "D", "J"))
            'ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
            ent.Data_Exclusao = Nothing

            ent.SITUACAO_CADASTRO = CStr(IIf(RdSit.Items(0).Selected, "O", "P"))

            If Not IsNumeric(CboBanco.Text) Then
                ent.COD_BANCO = ""
            Else
                ent.COD_BANCO = CboBanco.Items.FindByValue(CboBanco.Text).Value
            End If

            ent.NUM_AGENCIA = TxtNum_Agencia.Text
            ent.NOME_AGENCIA = txtNome_Agencia.Text
            ent.NUM_CONTA_CORRENTE = txtNum_Conta_Corrente.Text

            '# FIM DO CÓDIGO RESPONSÁVEL PELO CADASTRO DE INFORMAÇÕES ADICIONAIS #'


            'ent.Cod_Tip_Sit = If(RdAtivo.Items(0).Selected, 1, 2)
            'Situacao => 1 - Ativo, 2 - Inativo e 19 - Excluído na tabela PessoaFisicaFuncao -> Cod_Tip_Sit (numérico)
            ent.Cod_Tip_Sit = 1
            If ddlTipoPessoa.SelectedValue = 1 Then
                If txtCod_Perito.Value = "" Then
                    ent.Cod_Tip_Sit = 1
                End If
                If ddlStatusPerito.SelectedValue = 2 Then
                    ent.Cod_Tip_Sit = 2
                End If
            End If




            '# INICIO DO CÓDIGO RESPONSÁVEL PELO CADASTRO DE CHECKLIST DE DOCUMENTOS DO PERITO #'
            If chkDOCNECCV.Checked Then
                ent.DocNecCV = "1"
            Else
                ent.DocNecCV = "0"
            End If
            If chkDOCNECFOTO.Checked Then
                ent.DocNecFoto = "1"
            Else
                ent.DocNecFoto = "0"
            End If
            If chkDOCNECCPF.Checked Then
                ent.DocNecCPF = "1"
            Else
                ent.DocNecCPF = "0"
            End If
            If chkDOCNECRES.Checked Then
                ent.DocNecRes = "1"
            Else
                ent.DocNecRes = "0"
            End If
            If chkDOCNECIMP.Checked Then
                ent.DocNecImp = "1"
            Else
                ent.DocNecImp = "0"
            End If
            If chkDOCNECHAB.Checked Then
                ent.DocNecHab = "1"
            Else
                ent.DocNecHab = "0"
            End If
            If chkDOCNECORG.Checked Then
                ent.DocNecOrg = "1"
            Else
                ent.DocNecOrg = "0"
            End If

            '# FIM DO CÓDIGO RESPONSÁVEL PELO CADASTRO DE CHECKLIST DE DOCUMENTOS DO PERITO #'



            'Esse bloco verifica se o CPF ou CNPJ do Perito já existe no Perícias e Realiza o cadastro.'
            Dim ExisteCPFPerito As Boolean
            Dim ExisteCNPJPerito As Boolean
            If ddlTipoPessoa.Items(0).Selected Then
                If txtID_PF.Value = "" And Not String.IsNullOrEmpty(txtCPF.Text) Then
                    ExisteCPFPerito = Bal.VerCPFPerito(txtCPF.Text)
                Else
                    ExisteCPFPerito = False
                End If
            Else
                If String.IsNullOrEmpty(txtCod_Perito.Value) And Not String.IsNullOrEmpty(txtCNPJ.Text) Then
                    ExisteCNPJPerito = Bal.VerCNPJPerito(txtCNPJ.Text)
                Else
                    ExisteCNPJPerito = False
                End If
            End If


            If ddlTipoPessoa.Items(0).Selected Then
                If Not ExisteCPFPerito Then
                    Bal.Gravar(ent, InserirConta, ddlTipoPessoa.SelectedValue)
                    txtCod_Perito.Value = ent.Cod_Perito.ToString
                    MsgErro("Gravação do Perito com Sucesso. Para acesso do perito ao sistema pela internet é necessário o Cadastro Presencial.", "Sucesso")
                Else
                    Dim retIDPF As Long = Bal.VerPeritoPericias(txtCPF.Text, ddlTipoPessoa.SelectedValue)

                    If retIDPF = 0 Then
                        MsgErro("Inserção rejeitada. Existe Perito Cadastrado com este CPF", "Erro")
                        Exit Sub
                    Else
                        txtID_PF.Value = retIDPF.ToString
                        ent.ID_PF = retIDPF
                        Bal.Gravar(ent, InserirConta)
                        txtCod_Perito.Value = ent.Cod_Perito.ToString
                    End If
                End If
            Else
                If Not ExisteCNPJPerito Then
                    Bal.Gravar(ent, InserirConta, ddlTipoPessoa.SelectedValue)
                    txtCod_Perito.Value = ent.Cod_Perito.ToString
                    MsgErro("Gravação do Perito com Sucesso. Para acesso do perito ao sistema pela internet é necessário o Cadastro Presencial.", "Sucesso")
                Else
                    Dim retIDPJ As Long = Bal.VerPeritoPericias(txtCNPJ.Text, ddlTipoPessoa.SelectedValue)

                    If retIDPJ = 0 Then
                        MsgErro("Inserção rejeitada. Existe Perito Cadastrado com este CNPJ", "Erro")
                        Exit Sub
                    Else
                        'txtID_PF.Value = retIDPJ.ToString
                        ent.ID_PJ = retIDPJ
                        Bal.Gravar(ent, InserirConta, ddlTipoPessoa.SelectedValue)
                        txtCod_Perito.Value = ent.Cod_Perito.ToString
                    End If
                End If
            End If

            '''''''''''''''''''''''''''
            If RdSit.Items(0).Selected = True Then
                If txtNum_Conta_Corrente.Text = "" And TxtNum_Agencia.Text = "" And CboBanco.SelectedIndex = 0 And txtNome_Agencia.Text = "" Then
                    BtnDadosBancarios.Enabled = True
                Else
                    BtnDadosBancarios.Enabled = False
                End If
            Else
                BtnDadosBancarios.Enabled = False
            End If
            '''''''''''''''''''''
            'MsgErro("Para acesso do perito ao sistema pela internet é necessário o Cadastro Presencial ")
            MsgSucesso = String.Format("{0}<br />{1}", "Perito Gravado com Sucesso", "Para acesso do perito ao sistema pela internet é necessário o Cadastro Presencial")

            If RdSit.Items(0).Selected Then
                MsgG = Bal.GravarFornecedor(ent)
                If Not MsgG.ToString = "null" Then
                    MsgG = "<br/>" & " " & Chr(42) & " " & MsgG
                End If
            End If

            'MsgErro("Gravação do Fornecedor Rejeitada." + MsgG)
            'MsgSucesso = String.Format("<br />{0}.{1}", "Gravação do Fornecedor Rejeitada", MsgG)
            'Colocar retorno nas gravações para mensagens.
            'OK = RdSit.Items(0).Selected?
            'Ativo - RdAtivo.Items(0).Selected?
            ' If RdSit.Items(0).Selected And RdAtivo.Items(0).Selected Then
            'logger.Debug("Bal.GravarFornecedor(ent)")
            ' MsgG = Bal.GravarFornecedor(ent)

            'If Not MsgG.ToString = "null" Then
            'MsgG = "<br/>" & " " & Chr(42) & " " & MsgG
            'End If

            'MsgErro("Gravação do Fornecedor Rejeitada." + MsgG)
            'MsgSucesso = String.Format("<br />{0}.{1}", "Gravação do Fornecedor Rejeitada", MsgG)
            'End If
            'MsgErro("Gravado do Perito com Sucesso")

            If txtID_PF.Value = "" Then
                If Not ent.ID_PF = Nothing Then
                    txtID_PF.Value = ent.ID_PF.ToString
                End If
            End If

            Contador = 0
            DsNurTotal = BalCom.ExibirDadosSet()
            logger.Debug("Retorno do BalCom.ExibirDadosSet():" & Convert.ToString(DsNurTotal.Tables(0).Rows.Count))
            Dim Nur As Boolean
            Nur = False

            If Not Session("DsNur1") Is Nothing Then
                DsNur1 = CType(Session("DsNur1"), DataSet)
                logger.Debug("Recupera DsNur1 da Session")
                If DsNur1.Tables.Count > 0 Then
                    logger.Debug("Not DsNur1 Is Nothing")
                    If DsNur1.Tables(0).Rows.Count > 0 Then
                        logger.Debug("Há registro no DsNur1")
                        NumComarcas = DsNur1.Tables(0).Rows.Count
                        logger.Debug("DsNur1 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur1.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur1.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur1.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 1
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur1.BackColor = Drawing.Color.Black
                                BtnNur1.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur1.BackColor = Drawing.Color.Brown
                            BtnNur1.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(1) ")
                            DsNur1 = BalCom.ExibirDadosSet(1)

                            For Each rs As DataRow In DsNur1.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur1.BackColor = Drawing.Color.Black
                                    BtnNur1.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 1
                                Contador = Contador + 1
                            Next
                        Else
                            BtnNur1.BackColor = Drawing.Color.Black
                            BtnNur1.ForeColor = Drawing.Color.Beige
                        End If
                    End If
                End If

                If Not Session("DsNur2") Is Nothing Then
                    logger.Debug("DsNur2 not is Nothing")
                    DsNur2 = CType(Session("DsNur2"), DataSet)
                    If DsNur2.Tables.Count > 0 Then
                        If DsNur2.Tables(0).Rows.Count > 0 Then
                            Nur = False
                            NumComarcas = DsNur2.Tables(0).Rows.Count
                            logger.Debug("DsNur2 - NumComarcas: " & NumComarcas)
                            For ii = 0 To NumComarcas - 1
                                If CInt(DsNur2.Tables(0).Rows(ii).Item(0)) = 1 Then
                                    '''''''''''''''''''''''''''
                                    DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                    DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur2.Tables(0).Rows(ii).Item(1))
                                    DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur2.Tables(0).Rows(ii).Item(2))
                                    DsNurTotal.Tables(0).Rows(Contador).Item(3) = 2
                                    Contador = Contador + 1
                                    ''''''''''''''''''''''''''''''
                                    BtnNur2.BackColor = Drawing.Color.Black
                                    BtnNur2.ForeColor = Drawing.Color.Beige
                                    Nur = True
                                End If
                            Next
                            If Not Nur Then
                                BtnNur2.BackColor = Drawing.Color.Brown
                                BtnNur2.ForeColor = Drawing.Color.White
                                logger.Debug("BalCom.ExibirDadosSet(2) ")
                                DsNur2 = BalCom.ExibirDadosSet(2)
                                For Each rs As DataRow In DsNur2.Tables(0).Rows
                                    DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                    If rs("Marcado").ToString = "1" Then
                                        BtnNur2.BackColor = Drawing.Color.Black
                                        BtnNur2.ForeColor = Drawing.Color.Beige
                                    End If
                                    DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                    DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                    DsNurTotal.Tables(0).Rows(Contador).Item(3) = 2
                                    Contador = Contador + 1
                                Next
                            Else
                                BtnNur2.BackColor = Drawing.Color.Black
                                BtnNur2.ForeColor = Drawing.Color.Beige
                            End If
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur3") Is Nothing Then
                DsNur3 = CType(Session("DsNur3"), DataSet)
                If DsNur3.Tables.Count > 0 Then
                    If DsNur3.Tables(0).Rows.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur3.Tables(0).Rows.Count
                        logger.Debug("DsNur3 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur3.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur3.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur3.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 2
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur3.BackColor = Drawing.Color.Black
                                BtnNur3.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur3.BackColor = Drawing.Color.Brown
                            BtnNur3.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(3) ")
                            DsNur3 = BalCom.ExibirDadosSet(3)

                            For Each rs As DataRow In DsNur3.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur3.BackColor = Drawing.Color.Black
                                    BtnNur3.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 2
                                Contador = Contador + 1
                            Next
                        Else
                            BtnNur3.BackColor = Drawing.Color.Black
                            BtnNur3.ForeColor = Drawing.Color.Beige
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur4") Is Nothing Then
                DsNur4 = CType(Session("DsNur4"), DataSet)
                If DsNur4.Tables.Count > 0 Then
                    If DsNur4.Tables(0).Rows.Count > 0 Then

                        Nur = False
                        NumComarcas = DsNur4.Tables(0).Rows.Count
                        logger.Debug("DsNur4 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur4.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur4.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur4.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 4
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur4.BackColor = Drawing.Color.Black
                                BtnNur4.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur4.BackColor = Drawing.Color.Brown
                            BtnNur4.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(4)")
                            DsNur4 = BalCom.ExibirDadosSet(4)

                            For Each rs As DataRow In DsNur4.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur4.BackColor = Drawing.Color.Black
                                    BtnNur4.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 4
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
                If Not Session("DsNur5") Is Nothing Then
                    DsNur5 = CType(Session("DsNur5"), DataSet)
                    If DsNur5.Tables.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur5.Tables(0).Rows.Count
                        logger.Debug("DsNur5 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur5.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur5.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur5.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 5
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur5.BackColor = Drawing.Color.Black
                                BtnNur5.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur5.BackColor = Drawing.Color.Brown
                            BtnNur5.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(5) ")
                            DsNur5 = BalCom.ExibirDadosSet(5)

                            For Each rs As DataRow In DsNur5.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur5.BackColor = Drawing.Color.Black
                                    BtnNur5.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 5
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur6") Is Nothing Then
                DsNur6 = CType(Session("DsNur6"), DataSet)
                If DsNur6.Tables.Count > 0 Then
                    If DsNur6.Tables(0).Rows.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur6.Tables(0).Rows.Count
                        logger.Debug("DsNur6 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur6.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur6.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur6.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 6
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur6.BackColor = Drawing.Color.Black
                                BtnNur6.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then

                            BtnNur6.BackColor = Drawing.Color.Brown
                            BtnNur6.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(6) ")
                            DsNur6 = BalCom.ExibirDadosSet(6)

                            For Each rs As DataRow In DsNur6.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur6.BackColor = Drawing.Color.Black
                                    BtnNur6.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 6
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur7") Is Nothing Then
                DsNur7 = CType(Session("DsNur7"), DataSet)
                If DsNur7.Tables.Count > 0 Then
                    If DsNur7.Tables(0).Rows.Count > 1 Then

                        Nur = False
                        NumComarcas = DsNur7.Tables(0).Rows.Count
                        logger.Debug("DsNur7 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur7.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur7.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur7.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 7
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur7.BackColor = Drawing.Color.Black
                                BtnNur7.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur7.BackColor = Drawing.Color.Brown
                            BtnNur7.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(7) ")
                            DsNur7 = BalCom.ExibirDadosSet(7)

                            For Each rs As DataRow In DsNur7.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur7.BackColor = Drawing.Color.Black
                                    BtnNur7.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 7
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur8") Is Nothing Then
                DsNur8 = CType(Session("DsNur8"), DataSet)
                If DsNur8.Tables.Count > 0 Then
                    If DsNur8.Tables(0).Rows.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur8.Tables(0).Rows.Count
                        logger.Debug("DsNur8 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur8.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur8.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur8.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 8
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur8.BackColor = Drawing.Color.Black
                                BtnNur8.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then

                            BtnNur8.BackColor = Drawing.Color.Brown
                            BtnNur8.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(8) ")
                            DsNur8 = BalCom.ExibirDadosSet(8)

                            For Each rs As DataRow In DsNur8.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur8.BackColor = Drawing.Color.Black
                                    BtnNur8.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 8
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur9") Is Nothing Then
                DsNur9 = CType(Session("DsNur9"), DataSet)
                If DsNur9.Tables.Count > 0 Then
                    If DsNur9.Tables(0).Rows.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur9.Tables(0).Rows.Count
                        logger.Debug("DsNur9 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur9.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur9.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur9.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 9
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur9.BackColor = Drawing.Color.Black
                                BtnNur9.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur9.BackColor = Drawing.Color.Brown
                            BtnNur9.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(9) ")
                            DsNur9 = BalCom.ExibirDadosSet(9)
                            For Each rs As DataRow In DsNur9.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur9.BackColor = Drawing.Color.Black
                                    BtnNur9.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 9
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
                If Not Session("DsNur10") Is Nothing Then
                    DsNur10 = CType(Session("DsNur10"), DataSet)
                    If DsNur10.Tables.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur10.Tables(0).Rows.Count
                        logger.Debug("DsNur10 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur10.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur10.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur10.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 10
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur10.BackColor = Drawing.Color.Black
                                BtnNur10.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then

                            BtnNur10.BackColor = Drawing.Color.Brown
                            BtnNur10.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(10) ")
                            DsNur10 = BalCom.ExibirDadosSet(10)
                            For Each rs As DataRow In DsNur10.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur10.BackColor = Drawing.Color.Black
                                    BtnNur10.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 10
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur11") Is Nothing Then
                DsNur11 = CType(Session("DsNur11"), DataSet)
                If DsNur11.Tables.Count > 0 Then
                    If DsNur11.Tables(0).Rows.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur11.Tables(0).Rows.Count
                        logger.Debug("DsNur11 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur11.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur11.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur11.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 11
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur11.BackColor = Drawing.Color.Black
                                BtnNur11.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur11.BackColor = Drawing.Color.Brown
                            BtnNur11.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(11) ")
                            DsNur11 = BalCom.ExibirDadosSet(11)
                            For Each rs As DataRow In DsNur11.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur11.BackColor = Drawing.Color.Black
                                    BtnNur11.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 11
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur12") Is Nothing Then
                DsNur12 = CType(Session("DsNur12"), DataSet)
                If DsNur12.Tables.Count > 0 Then
                    If DsNur12.Tables(0).Rows.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur12.Tables(0).Rows.Count
                        logger.Debug("DsNur12 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur12.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur12.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur12.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 12
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur12.BackColor = Drawing.Color.Black
                                BtnNur12.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then
                            BtnNur12.BackColor = Drawing.Color.Brown
                            BtnNur12.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(12) ")
                            DsNur12 = BalCom.ExibirDadosSet(12)
                            For Each rs As DataRow In DsNur12.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur12.BackColor = Drawing.Color.Black
                                    BtnNur12.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 12
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If Not Session("DsNur13") Is Nothing Then
                DsNur13 = CType(Session("DsNur13"), DataSet)
                If DsNur13.Tables.Count > 0 Then
                    If DsNur13.Tables(0).Rows.Count > 0 Then
                        Nur = False
                        NumComarcas = DsNur13.Tables(0).Rows.Count
                        logger.Debug("DsNur13 - NumComarcas: " & NumComarcas)
                        For ii = 0 To NumComarcas - 1
                            If CInt(DsNur13.Tables(0).Rows(ii).Item(0)) = 1 Then
                                '''''''''''''''''''''''''''
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur13.Tables(0).Rows(ii).Item(1))
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur13.Tables(0).Rows(ii).Item(2))
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 13
                                Contador = Contador + 1
                                ''''''''''''''''''''''''''''''
                                BtnNur13.BackColor = Drawing.Color.Black
                                BtnNur13.ForeColor = Drawing.Color.Beige
                                Nur = True
                            End If
                        Next
                        If Not Nur Then

                            BtnNur13.BackColor = Drawing.Color.Brown
                            BtnNur13.ForeColor = Drawing.Color.White

                            logger.Debug("BalCom.ExibirDadosSet(13) ")
                            DsNur13 = BalCom.ExibirDadosSet(13)
                            For Each rs As DataRow In DsNur13.Tables(0).Rows
                                DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                                If rs("Marcado").ToString = "1" Then
                                    BtnNur13.BackColor = Drawing.Color.Black
                                    BtnNur13.ForeColor = Drawing.Color.Beige
                                End If
                                DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                                DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                                DsNurTotal.Tables(0).Rows(Contador).Item(3) = 13
                                Contador = Contador + 1
                            Next
                        End If
                    End If
                End If
            End If
            If txtCod_Perito.Value <> "" Then
                'logger.Debug("BalPerNur.ExcluirPerNur(CInt(txtID_PF.Value)) : " & txtID_PF.Value.ToString())
                BalPerNur.ExcluirPerNur(CInt(txtCod_Perito.Value))
                'logger.Debug("BalPerNur.GravarPerito_Comarca(DsNurTotal, CInt(txtID_PF.Value)): " & txtID_PF.Value.ToString())
                BalPerNur.GravarPerito_Comarca(DsNurTotal, CInt(txtCod_Perito.Value))

                'BtnGravaCurriculum.Enabled = True
                BtnGravaFoto.Enabled = True
                'BtnExibirCurriculum.Enabled = True
                BtnExibirFoto.Enabled = True
            End If
            txtNum_Reg.AutoPostBack = True
            txtNome.AutoPostBack = True
            txtNomeFantasia.AutoPostBack = True
            txtCPF.AutoPostBack = True
            txtCNPJ.AutoPostBack = True

            Session("cpf") = txtCPF.Text
            Session("cnpj") = txtCNPJ.Text
            Limpar(False)
            HabilitaBotoes(True)
            PreencherNurs()
            txtCPF.Text = Session("cpf").ToString
            txtCNPJ.Text = Session("cnpj").ToString
            Session.Remove("cpf")
            Session.Remove("cnpj")
            PreencherDadosPerito()

            If Not MsgG Is Nothing Then
                AtualizaMsgTela(MsgG)
            Else
                AtualizaMsgTela("")
            End If

        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        Finally
            'MsgErro(MsgSucesso, "Sucesso")
        End Try

    End Sub

    'Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
    '    If Not Me.IsPostBack Then
    '        Exit Sub
    '    End If
    '    Dim Bal As New BALPERITO(GetUsuario)
    '    'lblValidaNome.Text = ""
    '    'lblValidaCPF.Text = ""

    '    If Session("CodTipFunc") <> 4 Then
    '        MsgErro("Exclusão rejeitada. CPF não cadastro como perito.", "Erro")
    '        Exit Sub
    '    End If

    '    If txtNome.Text = "" Then
    '        MsgErro("Exclusão Rejeitada. Preecher Nome do Perito.", "Erro")
    '        'lblValidaNome.Text = "Preencher Nome"
    '        Exit Sub
    '    End If

    '    'If 'lblValidaCPF.Text = "" Then
    '    '    MsgErro("Exclusão Rejeitada. Preecher CPF do Perito.", "Erro")
    '    '    'lblValidaCPF.Text = "Preencher CPF"
    '    'End If
    '    If txtID_PF.Value = "" Then
    '        MsgErro("Exclusão não efetuada. Verifique o código do perito.", "Erro")
    '        Exit Sub
    '    Else
    '        MsgErro("Cadastre a Anotação do tipo exclusão, com a sua descrição.", "Erro")
    '        Response.Redirect("frmAnotacoes.aspx?CPF=" & txtCPF.Text)
    '    End If
    'End Sub

    Private Sub CboUF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboUF.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug(" PreencherCidade(CboUF.SelectedItem.Value): " & CboUF.SelectedItem.Value.ToString())
            PreencherCidade(CboUF.SelectedItem.Value)
            logger.Debug("PreencherBairro(CboCidade.SelectedItem.Value): " & CboCidade.SelectedItem.Value.ToString())
            PreencherBairro(CboCidade.SelectedItem.Value, False)
        End If
    End Sub

    Private Sub CboCidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCidade.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug("PreencherBairro(CboCidade.SelectedItem.Value): " & CboCidade.SelectedItem.Value.ToString())
            PreencherBairro(CboCidade.SelectedItem.Value)
        End If
    End Sub

    Private Sub CboUF1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboUF1.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug("PreencherCidade1(CboUF1.SelectedItem.Value): " & CboUF1.SelectedItem.Value.ToString())
            PreencherCidade1(CboUF1.SelectedItem.Value)
            logger.Debug("PreencherBairro1(CboCidade1.SelectedItem.Value):" & CboCidade1.SelectedItem.Value.ToString())
            PreencherBairro1(CboCidade1.SelectedItem.Value, False)
        End If
    End Sub

    Private Sub PreencherCombo_Status()
        Try
            Dim bal As New BALTIPO_STATUS(GetUsuario)
            Dim ds As New DataSet
            ds = bal.LISTAR_TODOS_STATUS()
            ddlStatusPerito.Items.Clear()
            ddlStatusPerito.DataTextField = "DESC_STATUS"
            ddlStatusPerito.DataValueField = "COD_TIPO_STATUS"
            ddlStatusPerito.DataSource = ds.Tables(0)
            ddlStatusPerito.DataBind()
            ddlStatusPerito.Items.Insert(0, "Selecione...")
            ddlStatusPerito.SelectedIndex = 0
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Private Sub PreencherTip_Logr()
        Dim bal As New BalTipoLogradouro(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboTip_Logr.Items.Clear()
        CboTip_Logr.DataTextField = "Descr"
        CboTip_Logr.DataValueField = "Cod_tip_Logr"
        CboTip_Logr.DataSource = dsfila.Tables(0) '.DefaultView
        CboTip_Logr.DataBind()
        CboTip_Logr.Items.Insert(0, "Selecione o Tipo de Logradouro")
        CboTip_Logr.SelectedIndex = 0 '117

        up1.Update()
    End Sub

    Private Sub PreencherTip_Logr1()
        Dim bal As New BalTipoLogradouro(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboTip_Logr1.Items.Clear()
        CboTip_Logr1.DataTextField = "Descr"
        CboTip_Logr1.DataValueField = "Cod_tip_Logr"
        CboTip_Logr1.DataSource = dsfila.Tables(0) '.DefaultView
        CboTip_Logr1.DataBind()
        CboTip_Logr1.Items.Insert(0, "Selecione o Tipo de Logradouro")
        CboTip_Logr1.SelectedIndex = 0 '117

        up1.Update()
    End Sub
    'Orgao_Per
    Private Sub PreencherOrgao_Per()
        Dim bal As New BalOrgao_Per(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboOrgao_Per.Items.Clear()
        CboOrgao_Per.DataTextField = "Descr_Orgao_Per"
        CboOrgao_Per.DataValueField = "Cod_Orgao_Per"
        CboOrgao_Per.DataSource = dsfila.Tables(0)
        CboOrgao_Per.DataBind()
        CboOrgao_Per.Items.Insert(0, "Selecione o Órgão profissional.")
        CboOrgao_Per.SelectedIndex = 0

        up1.Update()
    End Sub

    Private Sub PreencherUF()
        Dim bal As New BALCIDADE(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosUFSet()
        CboUF.Items.Clear()
        CboUF.Items.Insert(0, "UF")
        i = 0
        For Each rs As DataRow In dsfila.Tables(0).Rows
            If Not IsDBNull(rs("SIGLA_UF")) Then
                i = i + 1
                CboUF.Items.Insert(i, rs("SIGLA_UF").ToString)
            End If
        Next
        CboUF.SelectedIndex = 21

        up1.Update()
    End Sub

    Private Sub PreencherUF1()
        Dim bal As New BALCIDADE(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosUFSet()
        CboUF1.Items.Clear()
        CboUF1.Items.Insert(0, "UF")
        i = 0
        For Each rs As DataRow In dsfila.Tables(0).Rows
            If Not IsDBNull(rs("SIGLA_UF")) Then
                i = i + 1
                CboUF1.Items.Insert(i, rs("SIGLA_UF").ToString)
            End If
        Next
        CboUF1.SelectedIndex = 21

        up1.Update()
    End Sub

    Private Sub PreencherBairro(ByVal m_Cidade As String, Optional ByVal exibiMensagem As Boolean = True)
        If IsDBNull(m_Cidade) Or m_Cidade = "Selecione a Cidade" Then
            If exibiMensagem Then
                MsgErro("Selecione a Cidade", "Erro")
            End If
            Exit Sub
        End If
        Dim bal As New BALBairro(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(CLng(m_Cidade))
        CboBairro.Items.Clear()
        CboBairro.DataTextField = "Nome"
        CboBairro.DataValueField = "Cod_Bai"
        CboBairro.DataSource = dsfila.Tables(0)
        CboBairro.DataBind()
        CboBairro.Items.Insert(0, "Selecione o Bairro")
        CboBairro.SelectedIndex = 0

        up1.Update()
    End Sub

    Private Sub PreencherBairro1(ByVal m_Cidade As String, Optional ByVal exibiMensagem As Boolean = True)
        If IsDBNull(m_Cidade) Or m_Cidade = "Selecione a Cidade" Then
            If exibiMensagem Then
                MsgErro("Selecione a Cidade", "Erro")
            End If
            Exit Sub
        End If
        Dim bal As New BALBairro(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(CLng(m_Cidade))
        CboBairro1.Items.Clear()
        CboBairro1.DataTextField = "Nome"
        CboBairro1.DataValueField = "Cod_Bai"
        CboBairro1.DataSource = dsfila.Tables(0)
        CboBairro1.DataBind()
        CboBairro1.Items.Insert(0, "Selecione o Bairro")
        CboBairro1.SelectedIndex = 0

        up1.Update()
    End Sub

    Private Sub PreencherCidade1(ByVal m_UF As String)
        If IsDBNull(m_UF) Or m_UF = "Selecione o Estado" Then
            MsgErro("Selecione o Estado - UF", "Erro")
            Exit Sub
        End If
        Dim bal As New BALCIDADE(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_UF)
        CboCidade1.Items.Clear()
        CboCidade1.DataTextField = "Nome"
        CboCidade1.DataValueField = "Cod_Cid"
        CboCidade1.DataSource = dsfila.Tables(0).DefaultView
        CboCidade1.DataBind()
        CboCidade1.Items.Insert(0, "Selecione a Cidade")
        If m_UF = "RJ" Then
            CboCidade1.SelectedIndex = 0
        End If

        up1.Update()
    End Sub

    Private Sub PreencherCidade(ByVal m_UF As String)
        If IsDBNull(m_UF) Or m_UF = "Selecione o Estado" Then
            MsgErro("Selecione o Estado - UF", "Erro")
            Exit Sub
        End If
        Dim bal As New BALCIDADE(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_UF)
        CboCidade.Items.Clear()
        CboCidade.DataTextField = "Nome"
        CboCidade.DataValueField = "Cod_Cid"
        CboCidade.DataSource = dsfila.Tables(0).DefaultView
        CboCidade.DataBind()
        CboCidade.Items.Insert(0, "Selecione a Cidade")
        If m_UF = "RJ" Then
            CboCidade.SelectedIndex = 0
        End If

        up1.Update()
    End Sub

    Private Sub PreencherSemelhantes(ByVal m_Nome As String, Optional ByVal tipo As String = "NOME")
        Try
            If Not Me.IsPostBack Then
                Exit Sub
            End If
            Dim pessoa As Integer = ddlTipoPessoa.SelectedValue

            CboPerito.Enabled = True
            Dim bal As New BALPERITO(GetUsuario)
            Dim dsfila As New DataSet
            If m_Nome = "" Then Exit Sub

            CboPerito.Items.Clear()
            'dsfila = bal.ExibirDadosSet("NOME", m_Nome, "N")
            dsfila = bal.ExibirDadosCadPerito(tipo, m_Nome, "S", pessoa)
            CboPerito.DataSource = dsfila.Tables(0) '.DefaultView
            CboPerito.DataTextField = tipo
            CboPerito.DataValueField = IIf(pessoa = 1, "ID_PF", "ID_PJ")
            CboPerito.DataBind()
            CboPerito.Items.Insert(0, "Opcional -> Clique aqui para escolher nomes semelhantes")
            CboPerito.SelectedIndex = 0
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try

    End Sub

    Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

        Dim bal As New BALEspecialidade(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_Cod_Profissao)
        If dsfila.Tables(0).Rows.Count > 0 Then
            CboEspecialidade.Items.Clear()
            CboEspecialidade.DataTextField = "Descr_Especialidade"
            CboEspecialidade.DataValueField = "Cod_Especialidade"
            CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
            CboEspecialidade.DataBind()
        End If
        'CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.Items.Insert(0, "Selecione uma Especialidade")
        CboEspecialidade.SelectedIndex = 0

        up1.Update()
    End Sub

    Private Sub PreencherPROFISSAO()
        Dim bal As New BALProfissao(GetUsuario)
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

    Private Sub PreencherDadosPerito(Optional ByVal selecaoNomeSemelhante As Boolean = False)
        Try

            'Dim m_Cod_Orgao_Per As Integer
            Dim EntPer As New EntPERITO
            Dim DsConta As New DataSet
            Dim RsConta As DataRow
            Dim m_Cod_Banco As String

            If Not Me.IsPostBack Then
                Exit Sub
            End If
            If ddlTipoPessoa.SelectedIndex = 0 Then
                If CboPerito.SelectedValue = "Opcional -> Clique aqui para escolher nomes semelhantes" And txtCPF.Text = "" Then
                    Exit Sub
                End If
            ElseIf ddlTipoPessoa.SelectedIndex = 1 Then
                If CboPerito.SelectedValue = "Opcional -> Clique aqui para escolher nomes semelhantes" And txtCNPJ.Text = "" Then
                    Exit Sub
                End If
            End If

            txtNum_Reg.AutoPostBack = False
            txtNome.AutoPostBack = False
            txtCPF.AutoPostBack = False
            txtCNPJ.AutoPostBack = False
            Dim Bal As New BALPERITO(GetUsuario)
            Dim BalPerNur As New BalPerito_Comarca(GetUsuario)
            Dim tipoPessoa As Integer = ddlTipoPessoa.SelectedValue
            Dim cpf As String = IIf(String.IsNullOrEmpty(txtCPF.Text), Nothing, RetornaNumerosDeString(txtCPF.Text))
            Dim cnpj As String = IIf(String.IsNullOrEmpty(txtCNPJ.Text), Nothing, RetornaNumerosDeString(txtCNPJ.Text))

            If selecaoNomeSemelhante Then
                EntPer = Bal.ExibirDadosCadPeritoEnt("ID", CStr(CboPerito.SelectedValue), "S", tipoPessoa)
            Else
                Select Case tipoPessoa
                    Case EntPERITO.Pessoa.Fisica
                        If txtNome.Text <> "" And txtCPF.Text = "" Then
                            EntPer = Bal.ExibirDadosCadPeritoEnt("NOMEEXATO", txtNome.Text, "S", tipoPessoa)
                        ElseIf txtCPF.Text <> "" And txtNome.Text = "" Then
                            EntPer = Bal.ExibirDadosCadPeritoEnt("CPF", cpf, "S", tipoPessoa)
                        ElseIf txtCPF.Text <> "" And txtNome.Text <> "" Then
                            EntPer = Bal.ExibirDadosCadPeritoEnt("CPF", cpf, "S", tipoPessoa)
                        Else
                            Exit Sub
                        End If

                    Case EntPERITO.Pessoa.Juridica
                        If txtNome.Text <> "" And txtCNPJ.Text = "" Then
                            EntPer = Bal.ExibirDadosCadPeritoEnt("NOMEEXATO", txtNome.Text, "S", tipoPessoa)
                        ElseIf Not String.IsNullOrEmpty(txtNomeFantasia.Text) And txtCNPJ.Text = "" Then
                            EntPer = Bal.ExibirDadosCadPeritoEnt("NOMEFANT", txtNomeFantasia.Text, "S", tipoPessoa)
                        ElseIf txtCNPJ.Text <> "" And txtNome.Text = "" Then
                            EntPer = Bal.ExibirDadosCadPeritoEnt("CNPJ", cnpj, "S", tipoPessoa)
                        ElseIf txtCNPJ.Text <> "" And txtNome.Text <> "" Then
                            EntPer = Bal.ExibirDadosCadPeritoEnt("CNPJ", cnpj, "S", tipoPessoa)
                        Else
                            Exit Sub
                        End If

                    Case Else
                        Exit Sub
                End Select
            End If



            If Not EntPer Is Nothing Then
                'Desabilita a alteração do tipo de pessoa.'
                ddlTipoPessoa.Enabled = False

                '==================================
                If EntPer.StatusAtual.Codigo = 2 Then
                    BtnGravar.Enabled = False
                    HabilitaBotoes(False)
                    btnGravarTel.Enabled = False
                End If

                Session("codTipFunc") = ent.CodTipFunc

                txtCPF.Text = EntPer.CPF
                txtData_Migracao.Text = IIf(CDate(EntPer.DataMigracao) = Nothing, String.Empty, EntPer.DataMigracao.ToString("dd/MM/yyyy"))
                If Not EntPer.StatusAtual Is Nothing And EntPer.StatusAtual.Codigo <> 0 Then
                    ddlStatusPerito.SelectedValue = EntPer.StatusAtual.Codigo
                    RdPeritoJuizo.SelectedIndex = CInt(IIf(EntPer.StatusAtual.Codigo = 4, 0, 1))
                End If

                If EntPer.TipoPessoa = 1 Then
                    If txtCPF.Text = "" And txtCod_Perito.Value = "" And txtNome.Text = "" Then
                        Exit Sub
                    End If
                    If Len(RetornaNumerosDeString(txtCPF.Text)) <> 11 And txtCod_Perito.Value = "" And txtNome.Text = "" Then
                        Exit Sub
                    End If
                ElseIf EntPer.TipoPessoa = 2 Then
                    If txtCNPJ.Text = String.Empty And txtCod_Perito.Value = String.Empty And txtNome.Text = String.Empty Then
                        Exit Sub
                    End If
                    If Len(RetornaNumerosDeString(txtCNPJ.Text)) <> 14 And txtCod_Perito.Value = String.Empty And txtNome.Text = String.Empty Then
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If

                'logger.Debug("Bal.ExibirDadosConta(txtCPF.Text): " & txtCPF.Text.ToString())
                DsConta = Bal.ExibirDadosConta(IIf(EntPer.TipoPessoa = 1, EntPer.CPF, EntPer.CNPJ))
                CboBanco.SelectedIndex = 0
                CboBanco.SelectedValue = "Selecione o Banco" 'CboBanco.Items.FindByValue(EntPer.COD_BANCO).Value
                EntPer.COD_BANCO = "0"
                EntPer.NUM_AGENCIA = ""
                EntPer.NOME_AGENCIA = ""
                EntPer.NUM_CONTA_CORRENTE = ""
                If Not DsConta Is Nothing Then
                    If DsConta.Tables(0).Rows.Count > 0 Then
                        RsConta = DsConta.Tables(0).Rows(0)
                        'Cod_CGC_CPF, Cod_Banco,Cod_Agencia Num_Agencia, Nome_Agencia, Cod_Conta Num_Conta_CorrEntPere
                        EntPer.COD_BANCO = RsConta("Cod_Banco").ToString
                        EntPer.NUM_AGENCIA = RsConta("Num_Agencia").ToString
                        EntPer.NOME_AGENCIA = RsConta("Nome_Agencia").ToString
                        EntPer.NUM_CONTA_CORRENTE = RsConta("Num_Conta_Corrente").ToString
                    End If
                    If EntPer.COD_BANCO = "" Then
                        CboBanco.SelectedIndex = 0
                    Else
                        If EntPer.COD_BANCO = "0" Then
                            CboBanco.SelectedValue = "Selecione o Banco" 'CboBanco.Items.FindByValue(EntPer.COD_BANCO).Value
                        Else
                            m_Cod_Banco = EntPer.COD_BANCO.ToString
                            If Len(EntPer.COD_BANCO) = 1 Then
                                m_Cod_Banco = "00" + m_Cod_Banco
                            ElseIf Len(EntPer.COD_BANCO) = 2 Then
                                m_Cod_Banco = "0" + m_Cod_Banco
                            End If
                            CboBanco.SelectedValue = CboBanco.Items.FindByValue(m_Cod_Banco).Value
                        End If

                    End If

                End If
                TxtNum_Agencia.Text = EntPer.NUM_AGENCIA
                txtNome_Agencia.Text = EntPer.NOME_AGENCIA
                txtNum_Conta_Corrente.Text = EntPer.NUM_CONTA_CORRENTE
                If TxtNum_Agencia.Text <> "" Or _
                   txtNome_Agencia.Text = "" Or _
                   txtNum_Conta_Corrente.Text <> "" Or _
                   CboPerito.Text <> "" Then
                    txtNum_Conta_Corrente.Enabled = False
                    TxtNum_Agencia.Enabled = False
                    txtNome_Agencia.Enabled = False
                    txtNum_Conta_Corrente.Enabled = False
                Else
                    txtNum_Conta_Corrente.Enabled = True
                    TxtNum_Agencia.Enabled = True
                    txtNome_Agencia.Enabled = True
                    txtNum_Conta_Corrente.Enabled = True
                End If
                If txtNum_Conta_Corrente.Text = "" And TxtNum_Agencia.Text = "" And CboBanco.SelectedIndex = 0 And txtNome_Agencia.Text = "" Then
                    txtNum_Conta_Corrente.Enabled = True
                    TxtNum_Agencia.Enabled = True
                    txtNome_Agencia.Enabled = True
                    CboBanco.Enabled = True
                    BtnDadosBancarios.Visible = True
                    InserirConta = False
                Else
                    txtNum_Conta_Corrente.Enabled = False
                    TxtNum_Agencia.Enabled = False
                    txtNome_Agencia.Enabled = False
                    CboBanco.Enabled = False
                    BtnDadosBancarios.Visible = False
                    InserirConta = False
                End If
                '==================================
                If EntPer.ID_PF = 0 And EntPer.ID_PJ = 0 Then Exit Sub

                txtID_PF.Value = EntPer.ID_PF.ToString
                txtCod_Perito.Value = IIf(EntPer.Cod_Perito = 0, "", EntPer.Cod_Perito.ToString)
                logger.Debug("PreencherNurs()")
                PreencherNurs()


                If EntPer.Dt_Nasc.ToString Is Nothing Or EntPer.Dt_Nasc = #1/1/1900# Or EntPer.Dt_Nasc = #12:00:00 AM# Then
                    txtDt_Nasc.Text = Nothing
                Else
                    If String.IsNullOrEmpty(EntPer.Dt_Nasc.ToString) Then
                        txtDt_Nasc.Text = Nothing
                    Else
                        txtDt_Nasc.Text = EntPer.Dt_Nasc.ToShortDateString
                    End If
                End If

                'txtCPF.Text = EntPer.CPF
                txtNome.Text = EntPer.Nome
                txtNomeFantasia.Text = EntPer.NomeFantasia
                txtCNPJ.Text = EntPer.CNPJ

                'If EntPer.Cod_Tip_Sit = 1 Then
                '    RdAtivo.Items(0).Selected = True
                '    RdAtivo.Items(1).Selected = False
                'Else
                '    RdAtivo.Items(0).Selected = False
                '    RdAtivo.Items(1).Selected = True
                'End If

                If EntPer.SITUACAO_CADASTRO = "O" Then
                    RdSit.Items(0).Selected = True
                    RdSit.Items(1).Selected = False
                    If txtNum_Conta_Corrente.Text = "" And TxtNum_Agencia.Text = "" And CboBanco.SelectedIndex = 0 And txtNome_Agencia.Text = "" Then
                        BtnDadosBancarios.Enabled = True
                    Else
                        BtnDadosBancarios.Enabled = False
                    End If
                Else
                    RdSit.Items(0).Selected = False
                    RdSit.Items(1).Selected = True
                    BtnDadosBancarios.Enabled = False
                End If

                txtFalta_Entregar.Text = EntPer.FALTA_ENTREGAR
                'If EntPer.Indicacao = "D" Then
                '    RdIndic.Items(0).Selected = True
                '    RdIndic.Items(1).Selected = False
                'Else
                '    RdIndic.Items(0).Selected = False
                '    RdIndic.Items(1).Selected = True
                'End If

                logger.Debug("EntPer.Data_Cadastramento: " & EntPer.Data_Cadastramento)
                txtData_Cadastramento.Text = IIf(EntPer.Data_Cadastramento <> #12:00:00 AM#, EntPer.Data_Cadastramento.ToShortDateString, _
                                                        Today.ToShortDateString()).ToString()

                If EntPer.DocNecCV = "1" Then
                    chkDOCNECCV.Checked = True
                Else
                    chkDOCNECCV.Checked = False
                End If
                If EntPer.DocNecFoto = "1" Then
                    chkDOCNECFOTO.Checked = True
                Else
                    chkDOCNECFOTO.Checked = False
                End If
                If EntPer.DocNecRes = "1" Then
                    chkDOCNECRES.Checked = True
                Else
                    chkDOCNECRES.Checked = False
                End If
                If EntPer.DocNecImp = "1" Then
                    chkDOCNECIMP.Checked = True
                Else
                    chkDOCNECIMP.Checked = False
                End If
                If EntPer.DocNecOrg = "1" Then
                    chkDOCNECORG.Checked = True
                Else
                    chkDOCNECORG.Checked = False
                End If
                If EntPer.DocNecHab = "1" Then
                    chkDOCNECHAB.Checked = True
                Else
                    chkDOCNECHAB.Checked = False
                End If
                If EntPer.DocNecCPF = "1" Then
                    chkDOCNECCPF.Checked = True
                Else
                    chkDOCNECCPF.Checked = False
                End If

                Session("seqEnd") = EntPer.Seq_End
                Session("seqEnd1") = EntPer.Seq_End1
                ''''''''''''''''''''
                'ENDEREÇO RESIDENCIAL
                If EntPer.Cod_Tip_Logr = 0 Then
                    CboTip_Logr.SelectedIndex = 0
                Else
                    CboTip_Logr.SelectedValue = CboTip_Logr.Items.FindByValue(EntPer.Cod_Tip_Logr.ToString).Value
                End If
                If EntPer.Sigla_UF = "" Or EntPer.Sigla_UF = Nothing Then
                    CboUF.SelectedIndex = 21
                Else
                    If CboUF.Items.FindByValue(EntPer.Sigla_UF.ToString) Is Nothing Then
                        CboUF.SelectedIndex = 0
                    Else
                        CboUF.SelectedValue = CboUF.Items.FindByValue(EntPer.Sigla_UF.ToString).Value
                        logger.Debug("PreencherCidade(CboUF.SelectedItem.Value): " & CboUF.SelectedItem.Value.ToString())
                        PreencherCidade(CboUF.SelectedItem.Value)
                        If EntPer.Cod_Cidade = 0 Then
                            CboCidade.SelectedIndex = 230
                        Else
                            If CboCidade.Items.FindByValue(EntPer.Cod_Cidade.ToString) Is Nothing Then
                                CboCidade.SelectedIndex = 0
                            Else
                                CboCidade.SelectedValue = CboCidade.Items.FindByValue(EntPer.Cod_Cidade.ToString).Value
                                logger.Debug("PreencherBairro(CboCidade.SelectedItem.Value): " & CboCidade.SelectedItem.Value.ToString())
                                PreencherBairro(CboCidade.SelectedItem.Value)
                                If EntPer.Cod_Bairro = 0 Then
                                    CboBairro.SelectedIndex = 0
                                Else
                                    If CboBairro.Items.FindByValue(EntPer.Cod_Bairro.ToString) Is Nothing Then
                                        CboBairro.SelectedIndex = 0
                                    Else
                                        CboBairro.SelectedValue = CboBairro.Items.FindByValue(EntPer.Cod_Bairro.ToString).Value
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If
                txtNome_Logr.Text = EntPer.Nome_Logr
                txtCompl_Logr.Text = EntPer.Compl_Logr
                txtNum_Logr.Text = EntPer.Num_Logr
                TxtCEP.Text = EntPer.CEP

                ''''''''''''''''''''
                'ENDEREÇO COMERCIAL
                If EntPer.Cod_Tip_Logr1 = 0 Then
                    CboTip_Logr1.SelectedIndex = 0
                Else
                    CboTip_Logr1.SelectedValue = CboTip_Logr1.Items.FindByValue(EntPer.Cod_Tip_Logr1.ToString).Value
                End If
                If EntPer.Sigla_UF1 = "" Or EntPer.Sigla_UF1 = Nothing Then
                    CboUF1.SelectedIndex = 21
                Else
                    If CboUF1.Items.FindByValue(EntPer.Sigla_UF1.ToString) Is Nothing Then
                        CboUF1.SelectedIndex = 0
                    Else
                        CboUF1.SelectedValue = CboUF1.Items.FindByValue(EntPer.Sigla_UF1.ToString).Value
                        logger.Debug("PreencherCidade1(CboUF1.SelectedItem.Value): " & CboUF1.SelectedItem.Value.ToString())
                        PreencherCidade1(CboUF1.SelectedItem.Value)
                        If EntPer.Cod_Cidade1 = 0 Then
                            CboCidade1.SelectedIndex = 0
                        Else
                            If CboCidade1.Items.FindByValue(EntPer.Cod_Cidade1.ToString) Is Nothing Then

                                CboCidade1.SelectedIndex = 0
                            Else
                                CboCidade1.SelectedValue = CboCidade1.Items.FindByValue(EntPer.Cod_Cidade1.ToString).Value
                                logger.Debug("PreencherBairro1(CboCidade1.SelectedItem.Value): " & CboCidade1.SelectedItem.Value.ToString())
                                PreencherBairro1(CboCidade1.SelectedItem.Value)
                                If EntPer.Cod_Bairro1 = 0 Then
                                    CboBairro1.SelectedIndex = 0
                                Else
                                    If CboBairro1.Items.FindByValue(EntPer.Cod_Bairro1.ToString) Is Nothing Then
                                        CboBairro1.SelectedIndex = 0
                                    Else
                                        CboBairro1.SelectedValue = CboBairro1.Items.FindByValue(EntPer.Cod_Bairro1.ToString).Value
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If

                txtNome_Logr1.Text = EntPer.Nome_Logr1
                txtCompl_Logr1.Text = EntPer.Compl_Logr1
                txtNum_Logr1.Text = EntPer.Num_Logr1
                TxtCEP1.Text = EntPer.CEP1
                '''''''''''''''''''''''''''''''''''
                'EMAILS
                ''''''''''''''''''''
                txtEmail.Text = EntPer.EMAIL
                txtEmail1.Text = EntPer.EMAIL1
                HabilitaBotoes(True)
            End If

            BtnGravar.Focus()
            AtualizaMsgTela("")

            ''''''''''''''''''''''''''''
            'PeritoNur
            '''''''''''''''''''''''''''''
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try

    End Sub

    Private Sub AtualizaMsgTela(ByVal pMsg As String)
        Try
            Dim bMsg As Boolean
            Dim balPer As New BALPERITO(GetUsuario)

            bMsg = False

            If Not IsPostBack Then
                Exit Sub
            End If

            If txtCod_Perito.Value = "" Then
                Exit Sub
            End If

            If Not balPer.VerificaPeritoFornecedor(CLng(txtCod_Perito.Value)) Then
                lblMsg.Text = " - Existe pendência no cadastro: Perito não cadastrado como fornecedor."
                bMsg = True
            Else
                lblMsg.Text = ""
            End If

            If pMsg Is Nothing Then
                lblMsg.Text = ""
                Exit Sub
            End If

            If pMsg = "null" Or pMsg = "" Then
                If Not bMsg Then
                    lblMsg.Text = ""
                End If
            Else
                If lblMsg.Text = "" Then
                    lblMsg.Text = " - Existe pendência no cadastro:"
                End If
                lblMsg.Text = lblMsg.Text & pMsg
            End If

        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try

    End Sub

    Private Sub Limpar(ByVal trocaPessoa As Boolean)
        logger.Debug("Limpar()...")
        ddlStatusPerito.SelectedIndex = -1
        If trocaPessoa Then
            ddlTipoPessoa.SelectedIndex = 0
        End If
        grdDoc.DataSource = Nothing
        grdDoc.DataBind()
        ddlTipoPessoa.Enabled = True
        HabilitaBotoes(False)
        lblMsg.Text = ""
        'lblValidaNome.Text = ""
        'lblValidaCPF.Text = ""
        LimparSemNome()
        CboTip_Logr.SelectedIndex = 0
        CboTip_Logr1.SelectedIndex = 0
        txtData_Cadastramento.Text = Today.ToShortDateString
        'lblExcluido.Text = ""
        txtNome.Text = ""
        txtNomeFantasia.Text = String.Empty
        txtCPF.Text = ""
        txtCPF.Enabled = True
        txtCNPJ.Text = ""
        txtCNPJ.Enabled = False
        txtDt_Nasc.Text = ""
        txtID_PF.Value = ""
        txtCod_Perito.Value = ""
        txtNum_Reg.Text = ""
        CboPerito.Items.Clear()
        CboOrgao_Per.SelectedIndex = 0
        CboBanco.Enabled = True
        TxtNum_Agencia.Enabled = True
        txtNome_Agencia.Enabled = True
        txtNum_Conta_Corrente.Enabled = True
        DsNurTotal.Clear()
        DsNur1.Clear()
        'Session("DsNur1") = DsNur1
        DsNur2.Clear()
        'Session("DsNur2") = DsNur2
        DsNur3.Clear()
        'Session("DsNur3") = DsNur3
        DsNur4.Clear()
        'Session("DsNur4") = DsNur4
        DsNur5.Clear()
        'Session("DsNur5") = DsNur5
        DsNur6.Clear()
        'Session("DsNur6") = DsNur6
        DsNur7.Clear()
        'Session("DsNur7") = DsNur7
        DsNur8.Clear()
        'Session("DsNur8") = DsNur8
        DsNur9.Clear()
        'Session("DsNur9") = DsNur9
        DsNur10.Clear()
        'Session("DsNur10") = DsNur10
        DsNur11.Clear()
        'Session("DsNur11") = DsNur11
        DsNur12.Clear()
        'Session("DsNur12") = DsNur12
        DsNur13.Clear()
        'Session("DsNur13") = DsNur13
        BtnNur1.BackColor = Drawing.Color.Brown
        BtnNur1.ForeColor = Drawing.Color.White
        BtnNur2.BackColor = Drawing.Color.Brown
        BtnNur2.ForeColor = Drawing.Color.White
        BtnNur3.BackColor = Drawing.Color.Brown
        BtnNur3.ForeColor = Drawing.Color.White
        BtnNur4.BackColor = Drawing.Color.Brown
        BtnNur4.ForeColor = Drawing.Color.White
        BtnNur5.BackColor = Drawing.Color.Brown
        BtnNur5.ForeColor = Drawing.Color.White
        BtnNur6.BackColor = Drawing.Color.Brown
        BtnNur6.ForeColor = Drawing.Color.White
        BtnNur7.BackColor = Drawing.Color.Brown
        BtnNur7.ForeColor = Drawing.Color.White
        BtnNur8.BackColor = Drawing.Color.Brown
        BtnNur8.ForeColor = Drawing.Color.White
        BtnNur9.BackColor = Drawing.Color.Brown
        BtnNur9.ForeColor = Drawing.Color.White
        BtnNur10.BackColor = Drawing.Color.Brown
        BtnNur10.ForeColor = Drawing.Color.White
        BtnNur11.BackColor = Drawing.Color.Brown
        BtnNur11.ForeColor = Drawing.Color.White
        BtnNur12.BackColor = Drawing.Color.Brown
        BtnNur12.ForeColor = Drawing.Color.White
        BtnNur13.BackColor = Drawing.Color.Brown
        BtnNur13.ForeColor = Drawing.Color.White

        ''''''''''''''''''''''''''''''
        'Endereço Residencial
        TxtCEP.Text = ""
        CboCidade.SelectedIndex = 0
        CboBairro.SelectedIndex = 0
        CboTip_Logr.SelectedIndex = 0
        txtNome_Logr.Text = ""
        txtNum_Logr.Text = ""

        'Endereço comercial
        TxtCEP1.Text = ""
        CboCidade1.SelectedIndex = 0
        CboBairro1.SelectedIndex = 0
        CboTip_Logr1.SelectedIndex = 0
        txtNome_Logr1.Text = ""
        txtNum_Logr1.Text = ""

        'Telefones
        CboTip_Tel.SelectedIndex = 0
        txtDDD.Text = ""
        TxtRamal.Text = ""

        'Informações adicionais
        txtEmail.Text = ""
        txtEmail1.Text = ""

        'Profissão
        CboProfissao.SelectedIndex = 0
        CboEspecialidade.SelectedIndex = 0
        lblSiglaProf.Text = ""
        txtNum_Reg.Text = ""
        CboOrgao_Per.SelectedIndex = 0
        CboUF1.SelectedIndex = 0
        Session.Remove("codTipFunc")

        RemoveSessoes()
    End Sub

    Private Sub LimparSemNome()

        Session("Num_Nur") = 0
        txtCod_Perito.Value = ""
        txtID_PF.Value = ""
        CboTip_Tel.SelectedValue = "1"
        CboBairro.SelectedIndex = 0
        CboBairro1.SelectedIndex = 0
        txtNum_Reg.Text = ""
        txtNome_Logr.Text = ""
        txtCompl_Logr.Text = ""
        txtNum_Logr.Text = ""
        txtNome_Logr1.Text = ""
        txtCompl_Logr1.Text = ""
        txtNum_Logr1.Text = ""
        txtEmail.Text = ""
        txtEmail1.Text = ""
        TxtCEP.Text = ""
        TxtCEP1.Text = ""
        txtCPF.Text = ""
        txtDDD.Text = ""
        TxtTel.Text = ""
        TxtRamal.Text = ""
        txtDt_Nasc.Text = "" 'Nothing
        'RdAtivo.Items(0).Selected = True
        PreencherTip_Logr()
        CboUF.SelectedIndex = 21
        PreencherTip_Logr1()
        CboUF1.SelectedIndex = 21
        PreencherPROFISSAO()
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataTextField = "Descr_Especialidade"
        CboEspecialidade.DataValueField = "Cod_Especialidade"
        'CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.Items.Insert(0, "Selecione uma Especialidade")
        CboEspecialidade.SelectedIndex = 0
        CboProfissao.SelectedIndex = 0

        PreencherBanco()
        txtEmail.Text = ent.EMAIL
        txtFalta_Entregar.Text = ent.FALTA_ENTREGAR
        'RdIndic.Items(0).Selected = True 'DIPEJ
        txtData_Cadastramento.Text = IIf(ent.Data_Cadastramento <> #12:00:00 AM#, ent.Data_Cadastramento.ToShortDateString, _
                                         Today.ToShortDateString()).ToString()
        TxtNum_Agencia.Text = ""
        txtNome_Agencia.Text = ""
        txtNum_Conta_Corrente.Text = ""
        RdSit.Items(1).Selected = True 'Pendente
        ValidarEmail.Text = ""
        ValidarEmail1.Text = ""
        'lblValidaNome.Text = ""
        'lblValidaCPF.Text = ""
        DsNurTotal.Clear()
        txtNum_Reg.AutoPostBack = True

        txtNome.AutoPostBack = True
        txtNomeFantasia.AutoPostBack = True
        txtCNPJ.AutoPostBack = True
        txtCPF.AutoPostBack = True

        chkDOCNECCPF.Checked = False
        chkDOCNECCV.Checked = False
        chkDOCNECFOTO.Checked = False
        chkDOCNECHAB.Checked = False
        chkDOCNECIMP.Checked = False
        chkDOCNECORG.Checked = False
        chkDOCNECRES.Checked = False

    End Sub

    Private Sub PreencherBanco()
        Try
            Dim bal As New BalBanco(GetUsuario)
            Dim dsfila As New DataSet
            dsfila = bal.ExibirDadosSet()
            CboBanco.Items.Clear()
            CboBanco.DataTextField = "Nome"
            CboBanco.DataValueField = "Cod_Banco"
            CboBanco.DataSource = dsfila.Tables(0) '.DefaultView
            CboBanco.DataBind()
            CboBanco.Items.Insert(0, "Selecione o Banco")
            CboBanco.SelectedIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        logger.Debug("BtnLimpar_Click ...")
        logger.Debug("Limpar()")
        Limpar(True)
    End Sub

    Protected Sub CboCidade1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboCidade1.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug("PreencherBairro1(CboCidade1.SelectedItem.Value): " & CboCidade1.SelectedItem.Value.ToString())
            PreencherBairro1(CboCidade1.SelectedItem.Value)
        End If
    End Sub

    Protected Sub BtnNur1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur1.Click
        logger.Debug("BtnNur1_Click ...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "1"
        Session("DsNur") = Session("DsNur1")
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)

    End Sub

    Protected Sub BtnNur2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur2.Click
        logger.Debug("BtnNur2_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "2"
        Session("DsNur") = Session("DsNur2")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)

    End Sub

    Protected Sub BtnNur3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur3.Click
        logger.Debug("BtnNur3_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "3"
        Session("DsNur") = Session("DsNur3")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur4.Click
        logger.Debug("BtnNur4_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "4"
        Session("DsNur") = Session("DsNur4")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=620,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur5.Click
        logger.Debug("BtnNur5_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "5"
        Session("DsNur") = Session("DsNur5")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur6.Click
        logger.Debug("BtnNur6_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "6"
        Session("DsNur") = Session("DsNur6")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur7.Click
        logger.Debug("BtnNur7_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "7"
        Session("DsNur") = Session("DsNur7")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur8.Click
        logger.Debug("BtnNur8_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "8"
        Session("DsNur") = Session("DsNur8")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur9.Click
        logger.Debug("BtnNur9_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "9"
        Session("DsNur") = Session("DsNur9")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=650,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur10.Click
        logger.Debug("BtnNur10_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "10"
        Session("DsNur") = Session("DsNur10")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur11.Click
        logger.Debug("BtnNur11_Click")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Session("ID") = txtID_PF.Value
        Session("Num_Nur") = "11"
        Session("DsNur") = Session("DsNur11")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur12.Click
        logger.Debug("BtnNur12_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "12"
        Session("DsNur") = Session("DsNur12")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Protected Sub BtnNur13_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur13.Click
        logger.Debug("BtnNur13_Click...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Value = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Value
        End If
        Session("Num_Nur") = "13"
        Session("DsNur") = Session("DsNur13")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120');", True)
    End Sub

    Public Sub PreencherNurs()
        logger.Debug("PreencherNurs()...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        DsNur1.Clear()
        DsNur2.Clear()
        DsNur3.Clear()
        DsNur4.Clear()
        DsNur5.Clear()
        DsNur6.Clear()
        DsNur7.Clear()
        DsNur8.Clear()
        DsNur9.Clear()
        DsNur10.Clear()
        DsNur11.Clear()
        DsNur12.Clear()
        DsNur13.Clear()

        Dim BalPer As New BalPerito_Comarca(GetUsuario)

        If txtCod_Perito.Value <> "" Then
            'DsNur1
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 1): " & txtID_PF.Value.ToString())
            DsNur1 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 1)
            logger.Debug("Total Nur1: " & Convert.ToString(DsNur1.Tables(0).Rows.Count))
            Session("DsNur1") = CType(DsNur1, DataSet)
            If DsNur1.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur1.BackColor = Drawing.Color.Black
                BtnNur1.ForeColor = Drawing.Color.Beige
            Else
                BtnNur1.BackColor = Drawing.Color.Brown
                BtnNur1.ForeColor = Drawing.Color.White
            End If
            'DsNur2
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 2): " & txtID_PF.Value.ToString())
            DsNur2 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 2)
            logger.Debug("Total Nur2: " & Convert.ToString(DsNur2.Tables(0).Rows.Count))
            Session("DsNur2") = CType(DsNur2, DataSet)
            If DsNur2.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur2.BackColor = Drawing.Color.Black
                BtnNur2.ForeColor = Drawing.Color.Beige
            Else
                BtnNur2.BackColor = Drawing.Color.Brown
                BtnNur2.ForeColor = Drawing.Color.White
            End If
            'DsNur3
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 3):" & txtID_PF.Value.ToString())
            DsNur3 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 3)
            logger.Debug("Total Nur3: " & Convert.ToString(DsNur3.Tables(0).Rows.Count))
            Session("DsNur3") = CType(DsNur3, DataSet)
            If DsNur3.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur3.BackColor = Drawing.Color.Black
                BtnNur3.ForeColor = Drawing.Color.Beige
            Else
                BtnNur3.BackColor = Drawing.Color.Brown
                BtnNur3.ForeColor = Drawing.Color.White
            End If
            'DsNur4
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 4): " & txtID_PF.Value.ToString())
            DsNur4 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 4)
            logger.Debug("Total Nur4: " & Convert.ToString(DsNur4.Tables(0).Rows.Count))
            Session("DsNur4") = CType(DsNur4, DataSet)
            If DsNur4.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur4.BackColor = Drawing.Color.Black
                BtnNur4.ForeColor = Drawing.Color.Beige
            Else
                BtnNur4.BackColor = Drawing.Color.Brown
                BtnNur4.ForeColor = Drawing.Color.White
            End If
            'DsNur5
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 5): " & txtID_PF.Value.ToString())
            DsNur5 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 5)
            logger.Debug("Total Nur5: " & Convert.ToString(DsNur5.Tables(0).Rows.Count))
            Session("DsNur5") = CType(DsNur5, DataSet)
            If DsNur5.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur5.BackColor = Drawing.Color.Black
                BtnNur5.ForeColor = Drawing.Color.Beige
            Else
                BtnNur5.BackColor = Drawing.Color.Brown
                BtnNur5.ForeColor = Drawing.Color.White
            End If
            'DsNur6
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 6) " & txtID_PF.Value.ToString())
            DsNur6 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 6)
            logger.Debug("Total Nur6: " & Convert.ToString(DsNur6.Tables(0).Rows.Count))
            Session("DsNur6") = CType(DsNur6, DataSet)
            If DsNur6.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur6.BackColor = Drawing.Color.Black
                BtnNur6.ForeColor = Drawing.Color.Beige
            Else
                BtnNur6.BackColor = Drawing.Color.Brown
                BtnNur6.ForeColor = Drawing.Color.White
            End If
            'DsNur7
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 7): " & txtID_PF.Value.ToString())
            DsNur7 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 7)
            logger.Debug(" Total Nur7: " & Convert.ToString(DsNur7.Tables(0).Rows.Count))
            Session("DsNur7") = CType(DsNur7, DataSet)
            If DsNur7.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur7.BackColor = Drawing.Color.Black
                BtnNur7.ForeColor = Drawing.Color.Beige
            Else
                BtnNur7.BackColor = Drawing.Color.Brown
                BtnNur7.ForeColor = Drawing.Color.White
            End If
            'DsNur8
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 8): " & txtID_PF.Value.ToString())
            DsNur8 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 8)
            logger.Debug("Total Nur8: " & Convert.ToString(DsNur8.Tables(0).Rows.Count))
            Session("DsNur8") = CType(DsNur8, DataSet)
            If DsNur8.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur8.BackColor = Drawing.Color.Black
                BtnNur8.ForeColor = Drawing.Color.Beige
            Else
                BtnNur8.BackColor = Drawing.Color.Brown
                BtnNur8.ForeColor = Drawing.Color.White
            End If
            'DsNur9
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 9): " & txtID_PF.Value.ToString())
            DsNur9 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 9)
            logger.Debug("Total Nur9: " & Convert.ToString(DsNur9.Tables(0).Rows.Count))
            Session("DsNur9") = CType(DsNur9, DataSet)
            If DsNur9.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur9.BackColor = Drawing.Color.Black
                BtnNur9.ForeColor = Drawing.Color.Beige
            Else
                BtnNur9.BackColor = Drawing.Color.Brown
                BtnNur9.ForeColor = Drawing.Color.White
            End If
            'DsNur10
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 10): " & txtID_PF.Value.ToString())
            DsNur10 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 10)
            logger.Debug("Total Nur10: " & Convert.ToString(DsNur10.Tables(0).Rows.Count))
            Session("DsNur10") = CType(DsNur10, DataSet)
            If DsNur10.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur10.BackColor = Drawing.Color.Black
                BtnNur10.ForeColor = Drawing.Color.Beige
            Else
                BtnNur10.BackColor = Drawing.Color.Brown
                BtnNur10.ForeColor = Drawing.Color.White
            End If
            'DsNur11
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 11): " & txtID_PF.Value.ToString())
            DsNur11 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 11)
            logger.Debug("Total Nur11: " & Convert.ToString(DsNur11.Tables(0).Rows.Count))
            Session("DsNur11") = CType(DsNur11, DataSet)
            If DsNur11.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur11.BackColor = Drawing.Color.Black
                BtnNur11.ForeColor = Drawing.Color.Beige
            Else
                BtnNur11.BackColor = Drawing.Color.Brown
                BtnNur11.ForeColor = Drawing.Color.White
            End If
            'DsNur12
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 12): " & txtID_PF.Value.ToString())
            DsNur12 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 12)
            logger.Debug("Total Nur12: " & Convert.ToString(DsNur12.Tables(0).Rows.Count))
            Session("DsNur12") = CType(DsNur12, DataSet)
            If DsNur12.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur12.BackColor = Drawing.Color.Black
                BtnNur12.ForeColor = Drawing.Color.Beige
            Else
                BtnNur12.BackColor = Drawing.Color.Brown
                BtnNur12.ForeColor = Drawing.Color.White
            End If
            'DsNur13
            logger.Debug("BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 13): " & txtID_PF.Value.ToString())
            DsNur13 = BalPer.ExibirDadosNur(Int64.Parse(txtCod_Perito.Value), 13)
            logger.Debug("Total Nur13: " & Convert.ToString(DsNur13.Tables(0).Rows.Count))
            Session("DsNur13") = CType(DsNur13, DataSet)
            If DsNur13.Tables(0).Select("marcado=1").Count > 0 Then
                BtnNur13.BackColor = Drawing.Color.Black
                BtnNur13.ForeColor = Drawing.Color.Beige
            Else
                BtnNur13.BackColor = Drawing.Color.Brown
                BtnNur13.ForeColor = Drawing.Color.White
            End If
        End If
    End Sub

    Protected Sub BtnNovoEndCom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovoEndCom.Click
        logger.Debug("BtnNovoEndCom_Click...")
        logger.Debug("PreencherTip_Logr1()")
        PreencherTip_Logr1()
        logger.Debug("PreencherUF1()")
        PreencherUF1()
        logger.Debug("PreencherCidade1(RJ)")
        PreencherCidade1("RJ")
        logger.Debug("PreencherBairro1(1)")
        PreencherBairro1("1")
        txtCompl_Logr1.Text = ""
        TxtCEP1.Text = ""
        txtNum_Logr1.Text = ""
        txtNome_Logr1.Text = ""
    End Sub

    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome.TextChanged
        Try
            If txtNome.Text <> "" Then
                If txtCPF.Text = "" Then
                    logger.Debug("LimparSemNome()")
                    LimparSemNome()
                    txtDt_Nasc.Text = ""
                    txtCod_Perito.Value = ""
                    txtID_PF.Value = ""
                    ' CboOrgao_Per.SelectedIndex = 0
                    logger.Debug("PreencherSemelhantes(txtNome.Text): " & txtNome.Text.ToString())
                    PreencherSemelhantes(txtNome.Text)
                End If
            End If
            'If txtNome.Text <> "" Then 'lblValidaNome.Text = ""
            '    If txtCPF.Text <> "" Then 'lblValidaCPF.Text = ""
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtNomeFantasia_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNomeFantasia.TextChanged
        Try
            If txtNomeFantasia.Text <> "" Then
                If txtCNPJ.Text = "" Then
                    LimparSemNome()
                    txtDt_Nasc.Text = ""
                    txtCod_Perito.Value = ""
                    txtID_PF.Value = ""
                    PreencherSemelhantes(txtNomeFantasia.Text, "NOME_FANT")
                End If
            End If
            'If txtNome.Text <> "" Then 'lblValidaNome.Text = ""
            '    If txtCPF.Text <> "" Then 'lblValidaCPF.Text = ""
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboPerito.SelectedIndexChanged
        Try
            Dim Homonimos As Boolean
            Dim pessoa As Integer = ddlTipoPessoa.SelectedValue
            Dim BalHom As New BALPERITO(GetUsuario)

            If CboPerito.SelectedIndex <= 0 Then
                Exit Sub
            End If

            'If pessoa = 1 Then
            '    txtNome.Text = CboPerito.SelectedItem.Text.ToString
            'Else
            '    txtNomeFantasia.Text = CboPerito.SelectedItem.Text.ToString
            'End If

            logger.Debug("LimparSemNome()")
            LimparSemNome()

            If pessoa = 1 Then
                If Trim(txtNome.Text) <> "" And txtCPF.Text = "" And txtNum_Reg.Text = "" Then
                    logger.Debug("BalHom.VerHomonimo(txtNome.Text): " & txtNome.Text.ToString())
                    Homonimos = BalHom.VerHomonimo(txtNome.Text, pessoa, False)
                    If Homonimos Then
                        txtNome.Text = ""
                        CboPerito.Items.Clear()
                        MsgErro("Existem Homônimos. Localize o perito pelo CPF", "Erro")
                    Else
                        logger.Debug("PreencherDadosPerito()")
                        PreencherDadosPerito(True)
                    End If
                End If
            Else
                If Trim(txtNome.Text) <> "" And Trim(txtNomeFantasia.Text) = "" And txtCNPJ.Text = "" Then
                    Homonimos = BalHom.VerHomonimo(txtNome.Text, pessoa, False)
                    If Homonimos Then
                        txtNome.Text = ""
                        CboPerito.Items.Clear()
                        MsgErro("Existem Homônimos. Localize o perito pelo CNPJ", "Erro")
                    Else
                        PreencherDadosPerito(True)
                    End If
                ElseIf Trim(txtNomeFantasia.Text) <> "" And Trim(txtNome.Text) = "" And txtCNPJ.Text = "" Then
                    Homonimos = BalHom.VerHomonimo(txtNomeFantasia.Text, pessoa, True)
                    If Homonimos Then
                        txtNomeFantasia.Text = ""
                        CboPerito.Items.Clear()
                        MsgErro("Existem Homônimos. Localize o perito pelo CNPJ", "Erro")
                    Else
                        PreencherDadosPerito(True)
                    End If
                End If

            End If

            CboPerito.Enabled = False
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try


    End Sub

    Protected Sub RdSit_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdSit.SelectedIndexChanged
        logger.Debug("RdSit_SelectedIndexChanged...")
        If RdSit.Items(0).Selected And (Not chkDOCNECCV.Checked Or _
            Not chkDOCNECFOTO.Checked Or _
            Not chkDOCNECRES.Checked Or _
            Not chkDOCNECIMP.Checked Or _
            Not chkDOCNECHAB.Checked Or _
            Not chkDOCNECORG.Checked Or _
            Not chkDOCNECCPF.Checked) Then
            RdSit.Items(1).Selected = True
            MsgErro("Existe Documentação Pendente.", "Erro")
        End If
    End Sub

    Protected Sub BtnGravaFoto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravaFoto.Click
        Try
            Dim perito As EntPERITO = Nothing
            Dim Identificador As String = String.Empty
            Dim tipo As String = String.Empty
            Dim balPer As New BALPERITO(GetUsuario)

            If Not Me.IsPostBack Then
                Exit Sub
            End If

            Dim tipoPessoa As Integer = ddlTipoPessoa.SelectedValue

            Identificador = RetornaNumerosDeString(IIf(tipoPessoa = 1, txtCPF.Text, txtCNPJ.Text))
            tipo = IIf(tipoPessoa = 1, "CPF", "CNPJ")

            perito = balPer.ExibirDadosCadPeritoEnt(tipo, Identificador, "S", tipoPessoa)

            If perito.StatusAtual.Codigo = 2 Then
                MsgErro("Perito Excluído", "Erro")
                Exit Sub
            End If
            If txtCod_Perito.Value = "" Then
                MsgErro("Antes de inserir a foto, Grave os dados do perito.", "Erro")
                Exit Sub
            End If
            If txtID_PF.Value = "" Then
                Session("ID") = 0
            Else
                Session("ID") = txtID_PF.Value
            End If
            If txtCod_Perito.Value = "" Then
                Session("CodigoPerito") = 0
            Else
                Session("CodigoPerito") = txtCod_Perito.Value
            End If
            Session("TipoPessoa") = tipoPessoa
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmGravaFoto.aspx', '_blank', 'height=350,width=620, Top=150,left=120');", True)
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try

    End Sub

    Protected Sub BtnExibirFoto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExibirFoto.Click
        logger.Debug("BtnExibirFoto_Click...")
        If txtCod_Perito.Value = "" Then
            Exit Sub
        End If
        Session("ID") = txtID_PF.Value
        Session("CodigoPerito") = txtCod_Perito.Value
        Session("TipoPessoa") = ddlTipoPessoa.SelectedValue
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmExibirFoto.aspx', '_blank', 'height=401,width=301, Top=150,left=120');", True)
    End Sub

    'Protected Sub BtnGravaCurriculum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravaCurriculum.Click
    '    logger.Debug("BtnGravaCurriculum_Click...")
    '    If txtID_PF.Value = "" Then
    '        Exit Sub
    '    End If
    '    If lblExcluido.Text = "EXCLUÍDO" Then
    '        MsgErro("Perito Excluído.", "Erro")
    '        Exit Sub
    '    End If
    '    If txtID_PF.Value = "" Then
    '        MsgErro("Antes de inserir o Curriculum, Grave os dados do perito.", "Erro")
    '        Exit Sub
    '    End If
    '    Session("ID") = txtID_PF.Value
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('frmGravaCurriculum.aspx', '_blank', 'height=150,width=300, Top=150,left=120');", True)
    'End Sub

    'Protected Sub BtnExibirCurriculum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExibirCurriculum.Click
    '    logger.Debug("BtnExibirCurriculum_Click...")
    '    If Not Me.IsPostBack Then
    '        Exit Sub
    '    End If
    '    If txtID_PF.Value = "" Then
    '        Session("ID") = 0
    '    Else
    '        Session("ID") = txtID_PF.Value
    '    End If

    '    Dim Obj As DGTECGEDAR
    '    Dim IDDoc As String
    '    Dim Bal As New BALPERITO(GetUsuario)
    '    Dim Ent As New EntPERITO
    '    Dim f_ID_PF As String
    '    Dim m_URL As String

    '    f_ID_PF = Session("ID").ToString
    '    If f_ID_PF = "" Then
    '        MsgErro("Identificador não encontrado.", "Erro")
    '        Exit Sub
    '    End If
    '    logger.Debug("Bal.ExibirDadosEnt(ID, f_ID_PF, S): " & f_ID_PF.ToString())
    '    Ent = Bal.ExibirDadosEnt("ID", f_ID_PF, "S")
    '    If Ent Is Nothing Then
    '        logger.Debug("Não foi possível localizar o currículo do períto.")
    '        MsgErro("Não foi possível localizar o currículo do períto.", "Erro")
    '        Exit Sub
    '    End If
    '    IDDoc = Ent.IDGED_CV
    '    logger.Debug("IDDoc: " & Ent.IDGED_CV.ToString())

    '    If IDDoc <> "" Then
    '        Obj = New DGTECGEDAR
    '        Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
    '        m_URL = Obj.MontaURLCacheWeb(IDDoc)
    '        Obj.Finaliza()
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes');", True)
    '    Else
    '        MsgErro("Este Perito não possui Curriculum.", "Erro")
    '    End If

    'End Sub

    Protected Sub TxtCEP_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCEP.TextChanged

        logger.Debug("TxtCEP_TextChanged...")
        Dim Bal As New BalCEP(GetUsuario)
        Dim EntCEP As New EntCEP
        If Not Me.IsPostBack Or TxtCEP.Text = "" Then
            Exit Sub
        End If
        CboTip_Logr.SelectedIndex = 0
        logger.Debug("Bal.ExibirDadosEnt(TxtCEP.Text): " & TxtCEP.Text.ToString())
        EntCEP = Bal.ExibirDadosEnt(TxtCEP.Text)

        If Not EntCEP Is Nothing Then
            If CboUF.Items.FindByValue(EntCEP.Sigla_UF.ToString) Is Nothing Then
                CboUF.SelectedIndex = 0
            Else
                CboUF.SelectedValue = CboUF.Items.FindByValue(EntCEP.Sigla_UF.ToString).Value
            End If

            If CboUF.SelectedValue.Count > 0 Then
                PreencherCidade(CboUF.SelectedValue)
                If CboCidade.SelectedValue.Count > 0 Then
                    If CboCidade.Items.FindByValue(EntCEP.Cod_Cid.ToString) Is Nothing Then
                        CboCidade.SelectedIndex = 0
                    Else
                        CboCidade.SelectedValue = CboCidade.Items.FindByValue(EntCEP.Cod_Cid.ToString).Value
                        PreencherBairro(CboCidade.SelectedValue)
                        If CboBairro.SelectedValue.Count > 0 Then
                            If CboBairro.Items.FindByValue(EntCEP.Cod_Bai.ToString) Is Nothing Then
                                CboBairro.SelectedIndex = 0
                            Else
                                CboBairro.SelectedValue = CboBairro.Items.FindByValue(EntCEP.Cod_Bai.ToString).Value
                            End If

                        End If
                    End If

                End If
            End If
            txtNome_Logr.Text = EntCEP.Logradouro
        End If

        up1.Update()
    End Sub

    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

    End Sub

    Protected Sub BtnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSair.Click
        logger.Debug("BtnSair_Click...")
        RemoveSessoes()
        Response.Redirect("Principal.aspx")
    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        logger.Debug("CboProfissao_SelectedIndexChanged...")
        logger.Debug("PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value)): " & CboProfissao.Items.FindByValue(CboProfissao.Text).Value.ToString())

        CboEspecialidade.Items.Clear()
        If CboProfissao.SelectedIndex <> 0 Then
            PreencherEspecialidade(CInt(CboProfissao.SelectedValue))
        Else
            If CboEspecialidade.SelectedIndex = -1 Then
                'CboEspecialidade.Items.Insert(0, "GENÉRICO")
                CboEspecialidade.Items.Insert(0, "Selecione uma Especialidade")
            Else
                CboEspecialidade.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub PreencherSiglaOrgaoProfissional(ByVal nCodOrgaoProfissional As Integer)

        If nCodOrgaoProfissional = 0 Then
            lblSiglaProf.Text = ""
            Exit Sub
        End If

        Dim bal As New BalOrgao_Per(GetUsuario)
        lblSiglaProf.Text = bal.ConsultarSiglaOrgaoProfissional(nCodOrgaoProfissional)

    End Sub

    Protected Sub CboOrgao_Per_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboOrgao_Per.SelectedIndexChanged
        logger.Debug("CboOrgao_Per_SelectedIndexChanged...")
        If Not CboOrgao_Per.SelectedIndex = 0 Then
            PreencherSiglaOrgaoProfissional(CboOrgao_Per.SelectedValue)
        Else
            PreencherSiglaOrgaoProfissional(0)
        End If

        ScriptManager1.SetFocus(txtNum_Reg)

        up1.Update()
    End Sub

    Protected Sub BtnDadosBancarios_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnDadosBancarios.Click
        Try

            Dim BalPer As New BALPERITO(GetUsuario)
            Dim perito As EntPERITO = Nothing
            Dim Identificador As String
            Dim filtro As String
            Dim Msg As String

            If Not Me.IsPostBack Then
                Exit Sub
            End If

            Identificador = RetornaNumerosDeString(IIf(ddlTipoPessoa.SelectedValue = 1, txtCPF.Text, txtCNPJ.Text))
            filtro = IIf(ddlTipoPessoa.SelectedValue = 1, "CPF", "CNPJ")

            perito = BalPer.ExibirDadosCadPeritoEnt(filtro, Identificador, "S", ddlTipoPessoa.SelectedValue)

            'EXCLUIDO
            If perito.StatusAtual.Codigo = 2 Then
                MsgErro("Perito Excluído. Gravação rejeitada.", "Erro")
                Exit Sub
            End If

            If txtCod_Perito.Value = "" Then
                MsgErro("Gravação rejeitada! Grave os dados do perito antes de cadastrar os dados bancários.", "Erro")
                Exit Sub
            End If

            Dim m_Cod_Banco As String
            m_Cod_Banco = CboBanco.Items.FindByValue(CboBanco.Text).Value

            Msg = BalPer.GravarContaCorrente(Identificador, m_Cod_Banco, TxtNum_Agencia.Text, txtNome_Agencia.Text, txtNum_Conta_Corrente.Text)

            If Msg = "null" Then
                MsgErro("Conta Bancária Gravada com Sucesso.", "Sucesso")
            Else
                MsgErro(Msg, "Erro")
            End If
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub BtnNovoEndRes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovoEndRes.Click
        logger.Debug("BtnNovoEndRes_Click...")

        Dim BalPer As New BALPERITO(GetUsuario)
        Dim perito As EntPERITO = Nothing
        Dim Identificador As String
        Dim filtro As String

        If Not Me.IsPostBack Then
            Exit Sub
        End If

        Identificador = RetornaNumerosDeString(IIf(ddlTipoPessoa.SelectedValue = 1, txtCPF.Text, txtCNPJ.Text))
        filtro = IIf(ddlTipoPessoa.SelectedValue = 1, "CPF", "CNPJ")

        perito = BalPer.ExibirDadosCadPeritoEnt(filtro, Identificador, "S", ddlTipoPessoa.SelectedValue)

        'EXCLUIDO
        If perito.StatusAtual.Codigo = 2 Then
            MsgErro("Perito Excluído. Gravação rejeitada.", "Erro")
            Exit Sub
        End If

        logger.Debug("PreencherTip_Logr()")
        PreencherTip_Logr()
        logger.Debug("PreencherUF()")
        PreencherUF()
        logger.Debug("PreencherCidade(RJ)")
        PreencherCidade("RJ")
        logger.Debug("PreencherBairro(1)")
        PreencherBairro("1")
        txtCompl_Logr.Text = ""
        TxtCEP.Text = ""
        txtNum_Logr.Text = ""
        txtNome_Logr.Text = ""

    End Sub

    Private Sub HabilitaBotoes(ByVal vbol As Boolean)

        'logger.Debug("HabilitaBotoes(" & Convert.ToString(vbol) & ")")
        BtnChklist.Enabled = vbol
        BtnEndCom.Enabled = vbol
        BtnEndRes.Enabled = vbol
        BtnProfissao.Enabled = vbol
        BtnDocumentos.Enabled = vbol

        'Os txtCod_Perito eram txtID_PF, estou modificando pelo fato de que pode ser Pessoa Fisica ou Juridica - Gabriel Angelo 04/10/2016
        'Removi também os lblExcluido pois esta verificação agora deve ser feita de outra forma

        BtnGravaFoto.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnExibirFoto.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        'BtnGravaCurriculum.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        'BtnExibirCurriculum.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        btnGravarTel.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        btnGravarProfissao.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))

        BtnDadosBancarios.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "", True, False))
        BtnNur1.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur2.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur3.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur4.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur5.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur6.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur7.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur8.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur9.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur10.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur11.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur12.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnNur13.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))

        BtnTel.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnInf.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnEndCom.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnEndRes.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnChklist.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnProfissao.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))
        BtnDocumentos.Enabled = CBool(IIf(vbol And txtCod_Perito.Value <> "" And CInt(IIf(ddlStatusPerito.SelectedValue = "" Or ddlStatusPerito.SelectedValue = "Selecione...", 0, ddlStatusPerito.SelectedValue)) <> 2, True, False))

        If Not vbol Then
            PanelChklist.Visible = False
            PanelEndCom.Visible = False
            PanelEndRes.Visible = False
            PanelInf.Visible = False
            PanelTel.Visible = False
            pnlProfissao.Visible = False
            PanelDoc.Visible = False
        End If

    End Sub


    Protected Sub TxtCEP1_TextChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCEP1.TextChanged

        logger.Debug("TxtCEP1_TextChanged1...")
        Dim Bal As New BalCEP(GetUsuario)
        Dim EntCEP As New EntCEP
        If Not Me.IsPostBack Or TxtCEP1.Text = "" Then
            Exit Sub
        End If
        CboTip_Logr1.SelectedIndex = 0
        logger.Debug("Bal.ExibirDadosEnt(TxtCEP1.Text): " & TxtCEP1.Text.ToString())
        EntCEP = Bal.ExibirDadosEnt(TxtCEP1.Text)

        If Not EntCEP Is Nothing Then
            If CboUF.Items.FindByValue(EntCEP.Sigla_UF.ToString) Is Nothing Then
                CboUF1.SelectedIndex = 0
            Else
                CboUF1.SelectedValue = CboUF.Items.FindByValue(EntCEP.Sigla_UF.ToString).Value
            End If

            If CboUF1.SelectedValue.Count > 0 Then
                PreencherCidade1(CboUF1.SelectedValue)
                If CboCidade1.SelectedValue.Count > 0 Then
                    If CboCidade1.Items.FindByValue(EntCEP.Cod_Cid.ToString) Is Nothing Then
                        CboCidade1.SelectedIndex = 0
                    Else
                        CboCidade1.SelectedValue = CboCidade.Items.FindByValue(EntCEP.Cod_Cid.ToString).Value
                        PreencherBairro1(CboCidade1.SelectedValue)
                        If CboBairro1.SelectedValue.Count > 0 Then
                            If CboBairro1.Items.FindByValue(EntCEP.Cod_Bai.ToString) Is Nothing Then
                                CboBairro1.SelectedIndex = 0
                            Else
                                CboBairro1.SelectedValue = CboBairro1.Items.FindByValue(EntCEP.Cod_Bai.ToString).Value
                            End If

                        End If
                    End If
                End If
            End If
            txtNome_Logr1.Text = EntCEP.Logradouro
        End If

        up1.Update()
    End Sub

    Protected Sub BtnEndRes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEndRes.Click
        logger.Debug("BtnEndRes_Click...")
        PanelDoc.Visible = False
        PanelDoc.Enabled = False
        PanelEndRes.Visible = True
        PanelEndCom.Visible = False
        PanelChklist.Visible = False
        PanelTel.Visible = False
        PanelInf.Visible = False
        PanelEndRes.Enabled = True
        PanelEndCom.Enabled = False
        PanelChklist.Enabled = False
        PanelTel.Enabled = False
        PanelInf.Enabled = False
        pnlProfissao.Visible = False
        pnlProfissao.Enabled = False
        PanelDoc.Enabled = False
        PanelDoc.Visible = False
    End Sub

    Protected Sub BtnEndCom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEndCom.Click
        logger.Debug("BtnEndCom_Click...")
        PanelDoc.Visible = False
        PanelDoc.Enabled = False
        PanelEndRes.Visible = False
        PanelEndCom.Visible = True
        PanelChklist.Visible = False
        PanelTel.Visible = False
        PanelInf.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Enabled = True
        PanelChklist.Enabled = False
        PanelTel.Enabled = False
        PanelInf.Visible = False
        pnlProfissao.Visible = False
        pnlProfissao.Enabled = False
    End Sub

    Protected Sub BtnTel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnTel.Click
        logger.Debug("BtnTel_Click...")
        PanelDoc.Visible = False
        PanelDoc.Enabled = False
        PanelEndRes.Visible = False
        PanelEndCom.Visible = False
        PanelChklist.Visible = False
        PanelTel.Visible = True
        PanelInf.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Visible = False
        PanelChklist.Enabled = False
        PanelTel.Enabled = True
        PanelInf.Enabled = False
        pnlProfissao.Visible = False
        pnlProfissao.Enabled = False
        ListaTelefones()
    End Sub

    Protected Sub BtnChklist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnChklist.Click
        logger.Debug("BtnChklist_Click...")
        PanelDoc.Visible = False
        PanelDoc.Enabled = False
        PanelEndRes.Visible = False
        PanelEndCom.Visible = False
        PanelChklist.Visible = True
        PanelTel.Visible = False
        PanelInf.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Enabled = False
        PanelChklist.Enabled = True
        PanelTel.Enabled = False
        PanelInf.Enabled = False
        pnlProfissao.Visible = False
        pnlProfissao.Enabled = False
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim CV As String
        CV = Session("CV").ToString
        If CV = "S" Then
            chkDOCNECCV.Checked = True
        End If
    End Sub

    Protected Sub BtnInf_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnInf.Click
        logger.Debug("BtnInf_Click...")
        PanelDoc.Visible = False
        PanelDoc.Enabled = False
        PanelEndRes.Visible = False
        PanelEndCom.Visible = False
        PanelChklist.Visible = False
        PanelTel.Visible = False
        PanelInf.Visible = True
        PanelEndRes.Enabled = False
        PanelEndCom.Enabled = False
        PanelChklist.Enabled = False
        PanelTel.Enabled = False
        PanelInf.Enabled = True
        pnlProfissao.Visible = False
        pnlProfissao.Enabled = False
        If RdSit.Items(0).Selected = True Then
            If txtNum_Conta_Corrente.Text = "" And TxtNum_Agencia.Text = "" And CboBanco.SelectedIndex = 0 And txtNome_Agencia.Text = "" Then
                BtnDadosBancarios.Enabled = True
            Else
                BtnDadosBancarios.Enabled = False
            End If
        Else
            BtnDadosBancarios.Enabled = False
        End If
    End Sub

    Protected Sub CboTip_Tel_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboTip_Tel.SelectedIndexChanged
        If CboTip_Tel.SelectedValue = "2" Then
            lblRamal.Visible = True
            TxtRamal.Visible = True
        Else
            lblRamal.Visible = False
            TxtRamal.Visible = False
        End If
    End Sub

#Region "Manipulação do Telefone"

    Private Sub ListaTelefones()

        Dim balTel As New BalTelefone(GetUsuario)
        Dim ds As DataSet

        If txtCod_Perito.Value = "" Then
            Exit Sub
        End If
        ds = balTel.ExibirDadosTelefone(txtCod_Perito.Value, ddlTipoPessoa.SelectedValue)
        GrdTel.Dispose()
        GrdTel.DataSource = ds
        GrdTel.DataBind()
        up1.Update()

    End Sub

    Private Sub setSessionTel(ByVal dt As DataTable)
        Session("dtTel") = dt
    End Sub

    Private Function getSessionTel() As DataTable
        Dim dt As DataTable
        'If Session("dtTel") Is Nothing Then
        '    Session("dtTel") = dt
        'Else
        dt = CType(Session("dtTel"), DataTable)
        'End If
        Return dt
    End Function

    Protected Sub btnGrdAlterar_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim bal As New BalTelefone(GetUsuario)
        Dim ent As EntTelefone
        Dim vetor(5) As String

        For i As Integer = 0 To 4
            vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
        Next

        ent = bal.ConsultarTelefone(Long.Parse(IIf(vetor(2) = "", 0, vetor(2))), _
                                    CInt(IIf(vetor(1) = "", 0, vetor(1))), _
                                    CInt(IIf(vetor(3) = "", 0, vetor(3))))

        If Not ent Is Nothing Then
            CboTip_Tel.SelectedValue = ent.Cod_Tip_Tel.ToString
            TxtTel.Text = ent.Tel
            txtDDD.Text = ent.DDD
            TxtRamal.Text = ent.Ramal
            Session("SeqTel") = ent.SeqTel
            up1.Update()
        End If

    End Sub

    Protected Sub btnExcluirTel_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim balPerito As New BALPERITO(GetUsuario)
        Dim perito As EntPERITO = Nothing

        perito = balPerito.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, ddlTipoPessoa.SelectedValue)

        'If lblExcluido.Text <> "" Then
        '    MsgErro("O telefone não pode ser excluído porque o cadastro do perito não pode ser alterado.", "Erro")
        '    Exit Sub
        'End If

        If perito.StatusAtual.Codigo = 2 Then
            MsgErro("O telefone não pode ser excluído porque o cadastro do perito não pode ser alterado.", "Erro")
            Exit Sub
        End If

        Dim bal As New BalTelefone(GetUsuario)

        Dim vetor(5) As String
        For i As Integer = 0 To 4
            vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
        Next

        'bal.ExcluirPessoaFisicaTelefone(vetor(0), CInt(vetor(1)))
        bal.Excluir(Long.Parse(IIf(vetor(0) = "", 0, vetor(0))), _
                    CInt(IIf(vetor(1) = "", 0, vetor(1))), _
                    Long.Parse(IIf(vetor(2) = "", 0, vetor(2))), _
                    CInt(IIf(vetor(3) = "", 0, vetor(3))), _
                    Long.Parse(IIf(vetor(4) = "", 0, vetor(4))))
        MsgErro("Telefone excluído com sucesso.", "Erro")
        ListaTelefones()

    End Sub

    Protected Sub btnGravarTel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGravarTel.Click
        Dim bal As New BalTelefone(GetUsuario)
        Dim balPerito As New BALPERITO(GetUsuario)
        Dim telefone As New EntTelefone
        Dim perito As EntPERITO = Nothing

        perito = balPerito.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, ddlTipoPessoa.SelectedValue)

        If TxtTel.Text = "" Then
            MsgErro("Informe o número do telefone.", "Erro")
            Exit Sub
        End If

        If txtDDD.Text = "" Then
            MsgErro("Informe o DDD.", "Erro")
            Exit Sub
        End If

        telefone.Tel = TxtTel.Text.ToString
        telefone.DDD = txtDDD.Text.ToString
        telefone.Cod_Tip_Tel = CInt(CboTip_Tel.SelectedValue)
        telefone.Ramal = TxtRamal.Text.ToString
        If Not Session("SeqTel") Is Nothing Then
            telefone.SeqTel = CInt(Session("SeqTel").ToString)
        Else
            telefone.SeqTel = 0
        End If

        bal.Gravar(telefone, perito)
        MsgErro("Telefone gravado com sucesso.", "Sucesso")
        TxtTel.Text = ""
        txtDDD.Text = ""
        TxtRamal.Text = ""
        Session.Remove("SeqTel")
        CboTip_Tel.SelectedIndex = 0
        ListaTelefones()
    End Sub

    Protected Sub GrdTel_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        GrdTel.PageIndex = e.NewPageIndex
        ListaTelefones()
    End Sub
#End Region

#Region "Manipulação de informações da profissão do perito"

    Private Sub PreencherUFProf()
        Dim bal As New BALCIDADE(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosUFSet()
        cboUFProf.Items.Clear()
        cboUFProf.Items.Insert(0, "Selecione o Estado")
        i = 0
        For Each rs As DataRow In dsfila.Tables(0).Rows
            If Not IsDBNull(rs("SIGLA_UF")) Then
                i = i + 1
                cboUFProf.Items.Insert(i, rs("SIGLA_UF").ToString)
            End If
        Next
        cboUFProf.SelectedIndex = 21
    End Sub

    Private Sub ListarProfissaoPerito()

        Try
            If txtCod_Perito.Value = "" Then
                Exit Sub
            End If

            Dim bal As New BALEspecialidadePerito(GetUsuario)

            GrdProfissao.DataSource = bal.ListarEspecialidadesPerito(txtCod_Perito.Value.ToString)
            GrdProfissao.DataBind()
            GrdProfissao.Visible = True
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub BtnProfissao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnProfissao.Click
        PanelDoc.Visible = False
        PanelEndRes.Visible = False
        PanelEndCom.Visible = False
        PanelChklist.Visible = False
        PanelTel.Visible = False
        PanelInf.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Enabled = False
        PanelChklist.Enabled = False
        PanelTel.Enabled = False
        PanelInf.Enabled = False
        pnlProfissao.Visible = True
        pnlProfissao.Enabled = True

        PreencherPROFISSAO()
        PreencherUFProf()
        PreencherOrgao_Per()
        ListarProfissaoPerito()
    End Sub

    Protected Sub GrdProfissao_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            GrdProfissao.PageIndex = e.NewPageIndex
            ListarProfissaoPerito()
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try

    End Sub

    Protected Sub btnGrdAlterarProf_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            Dim balPer As New BALPERITO(GetUsuario)
            Dim perito As EntPERITO = Nothing
            Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)

            perito = balPer.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, tipoPessoa)

            If perito.StatusAtual.Codigo = 2 Then
                MsgErro("A profissão não pode ser alterada porque o cadastro do perito está como excluído.", "Erro")
                Exit Sub
            End If

            Dim vetor(7) As String

            For i As Integer = 0 To 7
                vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
            Next

            'Preenche profissão
            If Not CboProfissao.Items.FindByValue(vetor(1)) Is Nothing Then
                CboProfissao.SelectedValue = CInt(vetor(1))
            Else
                CboProfissao.SelectedIndex = 0
            End If

            'Preenche especialidade
            PreencherEspecialidade(CInt(vetor(1)))
            If Not CboEspecialidade.Items.FindByValue(CInt(vetor(2))) Is Nothing Then
                CboEspecialidade.SelectedValue = CInt(vetor(2))
            Else
                CboEspecialidade.SelectedIndex = 0
            End If

            'Preenche UF
            'PreencherUFProf()
            If Not cboUFProf.Items.FindByText(vetor(4)) Is Nothing Then
                cboUFProf.SelectedValue = vetor(4)
            Else
                cboUFProf.SelectedIndex = 0
            End If

            'Preenche Sigla
            lblSiglaProf.Text = vetor(3)
            txtNum_Reg.Text = vetor(5)

            'Preenche órgão profissional
            If Not CboOrgao_Per.Items.FindByValue(CInt(vetor(6))) Is Nothing Then
                CboOrgao_Per.SelectedValue = CInt(vetor(6))
            Else
                CboOrgao_Per.SelectedIndex = 0
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub btnExcluirProf_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            Dim bal As New BALEspecialidadePerito(GetUsuario)
            Dim balPer As New BALPERITO(GetUsuario)
            Dim perito As EntPERITO = Nothing
            Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)

            perito = balPer.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, tipoPessoa)

            If perito.StatusAtual.Codigo = 2 Then
                MsgErro("A profissão não pode ser excluída porque o cadastro do perito está como excluído.", "Erro")
                Exit Sub
            End If

            Dim vetor(3) As String
            For i As Integer = 0 To 3
                vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
            Next

            bal.Excluir(CLng(vetor(3)), CInt(vetor(1)), CInt(vetor(2)))
            MsgErro("Profissão excluída com sucesso.", "Sucesso")
            ListarProfissaoPerito()

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub btnGravarProfissao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGravarProfissao.Click
        Dim ent As New EntEspecialidade_Perito
        Dim bal As New BALEspecialidadePerito(GetUsuario)
        Dim balPerito As New BALPERITO(GetUsuario)
        Dim perito As EntPERITO = Nothing
        Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)

        If txtCod_Perito.Value = "" Then
            Exit Sub
        End If

        perito = balPerito.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, tipoPessoa)

        If perito.StatusAtual.Codigo = 2 Then
            MsgErro("A profissão não pode ser salva porque o cadastro do perito está como excluído.", "Erro")
            Exit Sub
        End If

        Dim sMsg As String = String.Empty

        If CboProfissao.SelectedIndex = 0 Then
            MsgErro("Selecione a profissão.", "Erro")
            Exit Sub
        End If

        If CboEspecialidade.SelectedIndex = 0 Then
            MsgErro("Selecione a especialidade. Caso não exista nenhuma, cadastre uma especialidade com o mesmo nome da profissão.", "Erro")
            Exit Sub
        End If

        ent.ID_PF = CLng(IIf(perito.TipoPessoa = 1, perito.ID_PF, perito.ID_PJ))
        ent.COD_PROFISSAO = CboProfissao.SelectedValue
        ent.COD_ESPECIALIDADE = CboEspecialidade.SelectedValue
        ent.COD_ORGAO_PER = CInt(IIf(CboOrgao_Per.SelectedIndex = 0, 0, CboOrgao_Per.SelectedValue))
        ent.UF = CStr(IIf(cboUFProf.SelectedIndex = 0, "", cboUFProf.SelectedValue))
        ent.Num_Reg = txtNum_Reg.Text.ToString
        ent.CodigoPerito = perito.Cod_Perito

        sMsg = bal.Gravar(ent)

        If sMsg <> "" Then
            MsgErro(sMsg, "Erro")
        Else
            MsgErro("Profissão gravada com sucesso.", "Sucesso")
        End If

        ListarProfissaoPerito()

        CboProfissao.ClearSelection()
        CboEspecialidade.ClearSelection()
        CboOrgao_Per.ClearSelection()
        lblSiglaProf.Text = ""
        txtNum_Reg.Text = ""
        cboUFProf.ClearSelection()
        'ListarProfissaoPerito()
    End Sub
#End Region


    Protected Sub btnNurMarcarTodos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNurMarcarTodos.Click
        'Nur1
        DsNur1 = CType(Session("DsNur1"), DataSet)
        For Each r As DataRow In DsNur1.Tables(0).Rows
            DsNur1.BeginInit()
            r("Marcado") = 1
            DsNur1.EndInit()
        Next
        DsNur1.AcceptChanges()
        Session("DsNur1") = DsNur1
        BtnNur1.BackColor = Drawing.Color.Black
        BtnNur1.ForeColor = Drawing.Color.Beige

        'Nur2
        DsNur2 = CType(Session("DsNur2"), DataSet)
        For Each r As DataRow In DsNur2.Tables(0).Rows
            DsNur2.BeginInit()
            r("marcado") = 1
            DsNur2.EndInit()
        Next
        DsNur2.AcceptChanges()
        Session("DsNur2") = DsNur2
        BtnNur2.BackColor = Drawing.Color.Black
        BtnNur2.ForeColor = Drawing.Color.Beige

        'dsNur3
        DsNur3 = CType(Session("DsNur3"), DataSet)
        For Each r As DataRow In DsNur3.Tables(0).Rows
            DsNur3.BeginInit()
            r("Marcado") = 1
            DsNur3.EndInit()
        Next
        DsNur3.AcceptChanges()
        Session("DsNur3") = DsNur3
        BtnNur3.BackColor = Drawing.Color.Black
        BtnNur3.ForeColor = Drawing.Color.Beige

        'dsNur4
        DsNur4 = CType(Session("DsNur4"), DataSet)
        For Each r As DataRow In DsNur4.Tables(0).Rows
            DsNur4.BeginInit()
            r("Marcado") = 1
            DsNur4.EndInit()
        Next
        DsNur4.AcceptChanges()
        Session("DsNur4") = DsNur4
        BtnNur4.BackColor = Drawing.Color.Black
        BtnNur4.ForeColor = Drawing.Color.Beige

        'dsNur5
        DsNur5 = CType(Session("DsNur5"), DataSet)
        For Each r As DataRow In DsNur5.Tables(0).Rows
            DsNur5.BeginInit()
            r("Marcado") = 1
            DsNur5.EndInit()
        Next
        DsNur5.AcceptChanges()
        Session("DsNur5") = DsNur5
        BtnNur5.BackColor = Drawing.Color.Black
        BtnNur5.ForeColor = Drawing.Color.Beige

        'dsNur6
        DsNur6 = CType(Session("DsNur6"), DataSet)
        For Each r As DataRow In DsNur6.Tables(0).Rows
            DsNur6.BeginInit()
            r("Marcado") = 1
            DsNur6.EndInit()
        Next
        DsNur6.AcceptChanges()
        Session("DsNur6") = DsNur6
        BtnNur6.BackColor = Drawing.Color.Black
        BtnNur6.ForeColor = Drawing.Color.Beige

        'dsNur7
        DsNur7 = CType(Session("DsNur7"), DataSet)
        For Each r As DataRow In DsNur7.Tables(0).Rows
            DsNur7.BeginInit()
            r("Marcado") = 1
            DsNur7.EndInit()
        Next
        DsNur7.AcceptChanges()
        Session("DsNur7") = DsNur7
        BtnNur7.BackColor = Drawing.Color.Black
        BtnNur7.ForeColor = Drawing.Color.Beige

        'dsNur8
        DsNur8 = CType(Session("DsNur8"), DataSet)
        For Each r As DataRow In DsNur8.Tables(0).Rows
            DsNur8.BeginInit()
            r("Marcado") = 1
            DsNur8.EndInit()
        Next
        DsNur8.AcceptChanges()
        Session("DsNur8") = DsNur8
        BtnNur8.BackColor = Drawing.Color.Black
        BtnNur8.ForeColor = Drawing.Color.Beige

        'dsNur9
        DsNur9 = CType(Session("DsNur9"), DataSet)
        For Each r As DataRow In DsNur9.Tables(0).Rows
            DsNur9.BeginInit()
            r("Marcado") = 1
            DsNur9.EndInit()
        Next
        DsNur9.AcceptChanges()
        Session("DsNur9") = DsNur9
        BtnNur9.BackColor = Drawing.Color.Black
        BtnNur9.ForeColor = Drawing.Color.Beige

        'dsNur10
        DsNur10 = CType(Session("DsNur10"), DataSet)
        For Each r As DataRow In DsNur10.Tables(0).Rows
            DsNur10.BeginInit()
            r("Marcado") = 1
            DsNur10.EndInit()
        Next
        DsNur10.AcceptChanges()
        Session("DsNur10") = DsNur10
        BtnNur10.BackColor = Drawing.Color.Black
        BtnNur10.ForeColor = Drawing.Color.Beige

        'dsNur11
        DsNur11 = CType(Session("DsNur11"), DataSet)
        For Each r As DataRow In DsNur11.Tables(0).Rows
            DsNur11.BeginInit()
            r("Marcado") = 1
            DsNur11.EndInit()
        Next
        DsNur11.AcceptChanges()
        Session("DsNur11") = DsNur11
        BtnNur11.BackColor = Drawing.Color.Black
        BtnNur11.ForeColor = Drawing.Color.Beige

        'dsNur12
        DsNur12 = CType(Session("DsNur12"), DataSet)
        For Each r As DataRow In DsNur12.Tables(0).Rows
            DsNur12.BeginInit()
            r("Marcado") = 1
            DsNur12.EndInit()
        Next
        DsNur12.AcceptChanges()
        Session("DsNur12") = DsNur12
        BtnNur12.BackColor = Drawing.Color.Black
        BtnNur12.ForeColor = Drawing.Color.Beige

        'dsNur13
        DsNur13 = CType(Session("DsNur13"), DataSet)
        For Each r As DataRow In DsNur13.Tables(0).Rows
            DsNur13.BeginInit()
            r("Marcado") = 1
            DsNur13.EndInit()
        Next
        DsNur13.AcceptChanges()
        Session("DsNur13") = DsNur13
        BtnNur13.BackColor = Drawing.Color.Black
        BtnNur13.ForeColor = Drawing.Color.Beige
    End Sub

    Protected Sub btnNurDesmarcarTodos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNurDesmarcarTodos.Click
        'Nur1
        DsNur1 = CType(Session("DsNur1"), DataSet)
        For Each r As DataRow In DsNur1.Tables(0).Rows
            DsNur1.BeginInit()
            r("Marcado") = 0
            DsNur1.EndInit()
        Next
        DsNur1.AcceptChanges()
        Session("DsNur1") = DsNur1

        BtnNur1.BackColor = Drawing.Color.Brown
        BtnNur1.ForeColor = Drawing.Color.White

        'Nur2
        DsNur2 = CType(Session("DsNur2"), DataSet)
        For Each r As DataRow In DsNur2.Tables(0).Rows
            DsNur2.BeginInit()
            r("marcado") = 0
            DsNur2.EndInit()
        Next
        DsNur2.AcceptChanges()
        Session("DsNur2") = DsNur2

        BtnNur2.BackColor = Drawing.Color.Brown
        BtnNur2.ForeColor = Drawing.Color.White

        'dsNur3
        DsNur3 = CType(Session("DsNur3"), DataSet)
        For Each r As DataRow In DsNur3.Tables(0).Rows
            DsNur3.BeginInit()
            r("Marcado") = 0
            DsNur3.EndInit()
        Next
        DsNur3.AcceptChanges()
        Session("DsNur3") = DsNur3
        BtnNur3.BackColor = Drawing.Color.Brown
        BtnNur3.ForeColor = Drawing.Color.White

        'dsNur4
        DsNur4 = CType(Session("DsNur4"), DataSet)
        For Each r As DataRow In DsNur4.Tables(0).Rows
            DsNur4.BeginInit()
            r("Marcado") = 0
            DsNur4.EndInit()
        Next
        DsNur4.AcceptChanges()
        Session("DsNur4") = DsNur4
        BtnNur4.BackColor = Drawing.Color.Brown
        BtnNur4.ForeColor = Drawing.Color.White

        'dsNur5
        DsNur5 = CType(Session("DsNur5"), DataSet)
        For Each r As DataRow In DsNur5.Tables(0).Rows
            DsNur5.BeginInit()
            r("Marcado") = 0
            DsNur5.EndInit()
        Next
        DsNur5.AcceptChanges()
        Session("DsNur5") = DsNur5
        BtnNur5.BackColor = Drawing.Color.Brown
        BtnNur5.ForeColor = Drawing.Color.White

        'dsNur6
        DsNur6 = CType(Session("DsNur6"), DataSet)
        For Each r As DataRow In DsNur6.Tables(0).Rows
            DsNur6.BeginInit()
            r("Marcado") = 0
            DsNur6.EndInit()
        Next
        DsNur6.AcceptChanges()
        Session("DsNur6") = DsNur6
        BtnNur6.BackColor = Drawing.Color.Brown
        BtnNur6.ForeColor = Drawing.Color.White

        'dsNur7
        DsNur7 = CType(Session("DsNur7"), DataSet)
        For Each r As DataRow In DsNur7.Tables(0).Rows
            DsNur7.BeginInit()
            r("Marcado") = 0
            DsNur7.EndInit()
        Next
        DsNur7.AcceptChanges()
        Session("DsNur7") = DsNur7
        BtnNur7.BackColor = Drawing.Color.Brown
        BtnNur7.ForeColor = Drawing.Color.White

        'dsNur8
        DsNur8 = CType(Session("DsNur8"), DataSet)
        For Each r As DataRow In DsNur8.Tables(0).Rows
            DsNur8.BeginInit()
            r("Marcado") = 0
            DsNur8.EndInit()
        Next
        DsNur8.AcceptChanges()
        Session("DsNur8") = DsNur8
        BtnNur8.BackColor = Drawing.Color.Brown
        BtnNur8.ForeColor = Drawing.Color.White

        'dsNur9
        DsNur9 = CType(Session("DsNur9"), DataSet)
        For Each r As DataRow In DsNur9.Tables(0).Rows
            DsNur9.BeginInit()
            r("Marcado") = 0
            DsNur9.EndInit()
        Next
        DsNur9.AcceptChanges()
        Session("DsNur9") = DsNur9
        BtnNur9.BackColor = Drawing.Color.Brown
        BtnNur9.ForeColor = Drawing.Color.White

        'dsNur10
        DsNur10 = CType(Session("DsNur10"), DataSet)
        For Each r As DataRow In DsNur10.Tables(0).Rows
            DsNur10.BeginInit()
            r("Marcado") = 0
            DsNur10.EndInit()
        Next
        DsNur10.AcceptChanges()
        Session("DsNur10") = DsNur10
        BtnNur10.BackColor = Drawing.Color.Brown
        BtnNur10.ForeColor = Drawing.Color.White

        'dsNur11
        DsNur11 = CType(Session("DsNur11"), DataSet)
        For Each r As DataRow In DsNur11.Tables(0).Rows
            DsNur11.BeginInit()
            r("Marcado") = 0
            DsNur11.EndInit()
        Next
        DsNur11.AcceptChanges()
        Session("DsNur11") = DsNur11
        BtnNur11.BackColor = Drawing.Color.Brown
        BtnNur11.ForeColor = Drawing.Color.White

        'dsNur12
        DsNur12 = CType(Session("DsNur12"), DataSet)
        For Each r As DataRow In DsNur12.Tables(0).Rows
            DsNur12.BeginInit()
            r("Marcado") = 0
            DsNur12.EndInit()
        Next
        DsNur12.AcceptChanges()
        Session("DsNur12") = DsNur12
        BtnNur12.BackColor = Drawing.Color.Brown
        BtnNur12.ForeColor = Drawing.Color.White

        'dsNur13
        DsNur13 = CType(Session("DsNur13"), DataSet)
        For Each r As DataRow In DsNur13.Tables(0).Rows
            DsNur13.BeginInit()
            r("Marcado") = 0
            DsNur13.EndInit()
        Next
        DsNur13.AcceptChanges()
        Session("DsNur13") = DsNur13
        BtnNur13.BackColor = Drawing.Color.Brown
        BtnNur13.ForeColor = Drawing.Color.White


    End Sub

#Region "Manipulação dos dados de Documentos"

    Protected Sub BtnDocumentos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnDocumentos.Click
        PanelEndRes.Visible = False
        PanelEndCom.Visible = False
        PanelChklist.Visible = False
        PanelTel.Visible = False
        PanelInf.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Enabled = False
        PanelChklist.Enabled = False
        PanelTel.Enabled = False
        PanelInf.Enabled = False
        pnlProfissao.Visible = False
        pnlProfissao.Enabled = False
        PanelDoc.Visible = True
        PanelDoc.Enabled = True

        ListarDocumentos(CLng(txtCod_Perito.Value))
    End Sub

    Protected Sub btnGravarDocumento_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGravarDocumento.Click
        Try
            Dim extensaoArquivo As String = String.Empty
            Dim nomeArquivo As String = String.Empty
            Dim caminhoSalvar As String = String.Empty
            Dim tipoPessoa As Integer = ddlTipoPessoa.SelectedValue
            Dim tamanhoArquivo As Long = 0
            Dim perito As EntPERITO = Nothing
            Dim balDoc As New BALDOCUMENTO(GetUsuario)
            Dim balPer As New BALPERITO(GetUsuario)
            Dim documento As DOCUMENTO

            If txtCod_Perito.Value = String.Empty Then
                Exit Sub
            End If

            perito = balPer.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, tipoPessoa)

            If perito.StatusAtual.Codigo = 2 Then
                MsgErro("Perito Excluído.", "Erro")
                Exit Sub
            End If

            If txtDocumento.FileName = String.Empty Then
                MsgErro("Gravação rejeitada. Selecione um arquivo.", "Erro")
                Exit Sub
            End If

            nomeArquivo = txtDocumento.FileName
            extensaoArquivo = Mid(nomeArquivo, Len(nomeArquivo) - 2, 3)

            If UCase(extensaoArquivo) <> "PDF" Then
                MsgErro("Arquivo inválido. Inclua um arquivo em formato PDF.", "Erro")
                Exit Sub
            End If

            tamanhoArquivo = ((txtDocumento.PostedFile.ContentLength / 1024) / 1024)

            'Se tamanho do arquivo maior que 6 MB então gera erro.
            If tamanhoArquivo > 6 Then
                MsgErro("Arquivo inválido. O tamanho máximo permitido por aquivo é de 6MB.", "Erro")
                Exit Sub
            End If

            caminhoSalvar = String.Format("Documentos/arquivo-{0}-{1}.pdf", txtCod_Perito.Value, Now.Ticks.ToString)

            txtDocumento.SaveAs(Server.MapPath(caminhoSalvar))

            documento = New DOCUMENTO
            documento.CodigoPerito = perito.Cod_Perito
            documento.GedId = ArmazenaArquivoGED(Server.MapPath(caminhoSalvar))
            documento.NomeArquivo = nomeArquivo

            balDoc.Incluir(documento)

            Try

                ExcluiArquivoCaminho(Server.MapPath(caminhoSalvar), "Documentos")
            Catch
            End Try

            ListarDocumentos(documento.CodigoPerito)

            MsgErro("Documento gravado com sucesso.", "Sucesso")
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub ListarDocumentos(ByVal codigoPerito As Long)
        Try
            Dim balDoc As New BALDOCUMENTO(GetUsuario)
            Dim listaDocumentos As List(Of DOCUMENTO)

            listaDocumentos = balDoc.Listar(codigoPerito)

            grdDoc.DataSource = listaDocumentos
            grdDoc.DataBind()

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub btnExcluirDoc_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            Dim balDoc As New BALDOCUMENTO(GetUsuario)
            Dim balPer As New BALPERITO(GetUsuario)
            Dim perito As EntPERITO = Nothing
            Dim documento As DOCUMENTO
            Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)

            perito = balPer.ExibirDadosEnt("CODIGO", txtCod_Perito.Value, "S", 0, tipoPessoa)

            If perito.StatusAtual.Codigo = 2 Then
                MsgErro("O documento não pode ser excluído porque o cadastro do perito está como excluído.", "Erro")
                Exit Sub
            End If

            Dim vetor(3) As String
            For i As Integer = 0 To 2
                vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
            Next

            documento = New DOCUMENTO
            documento.CodigoPerito = CLng(vetor(0))
            documento.GedId = CStr(vetor(1))
            documento.NomeArquivo = CStr(vetor(2))

            ExcluiArquivoGED(documento.GedId)
            balDoc.Excluir(documento)

            ListarDocumentos(documento.CodigoPerito)
            MsgErro("Documento excluído com sucesso.", "Sucesso")
        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Protected Sub grdDoc_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDoc.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim urlDocGed As String = String.Empty
                Dim IdGed As String = String.Empty
                Dim nomeArquivo As String = String.Empty

                IdGed = CStr(e.Row.Cells(0).Text)
                nomeArquivo = CStr(e.Row.Cells(2).Text)
                urlDocGed = MontaURLDocumentoGED(IdGed)

                Dim linkDocumento As HyperLink = e.Row.FindControl("linkDocumento")
                linkDocumento.NavigateUrl = urlDocGed
                linkDocumento.Text = nomeArquivo

                e.Row.Cells(0).Visible = False
                e.Row.Cells(2).Visible = False
                e.Row.Cells(1).ColumnSpan = 2
                e.Row.Cells(3).ColumnSpan = 2
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region





End Class
