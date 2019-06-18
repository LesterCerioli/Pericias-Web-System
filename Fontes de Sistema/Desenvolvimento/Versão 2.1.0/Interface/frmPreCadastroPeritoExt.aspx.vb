'Option Strict On
'Option Strict Off
Imports ServicoDadosODPNET
Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports log4net

Partial Public Class frmPreCadastroPeritoExt

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
    Public NumABA As Integer

    Private ent As New EntPERITO
    Dim i, j As Integer
    Dim logger As log4net.ILog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        logger = log4net.LogManager.GetLogger("LogInFile")
        logger.Debug("Acesso ao cadastro de Perito ...")

        If Not Me.IsPostBack Then
            Try

                TabContainer1.ActiveTabIndex = 0
                txtCPF.Focus()
                lblRamal.Visible = False
                TxtRamal.Visible = False

                txtCPF.Attributes.Add("onblur", "validacpf('" & txtCPF.ClientID & "');")
                txtDt_Nasc.Attributes.Add("onblur", "validardata('" & txtDt_Nasc.ClientID & "');")
                PreencherOrgao_Per()
                PreencherTip_Logr()
                PreencherUF()
                PreencherCidade("RJ")
                PreencherBairro("1")
                PreencherTip_Logr1()
                PreencherUF1()
                PreencherCidade1("RJ")
                PreencherBairro1("1")
                PreencherPROFISSAO()
                PreencherUFProfissao()
            Catch ex As Exception
                MsgErro(ex.Message)
            End Try
            txtNome.Attributes.Add("onkeydown", "if(event.which==13){ event.which = 9; } if(event.keyCode==13){ event.keyCode = 9; }")
        End If
        If Session("ABA") Is Nothing Then
            Session("ABA") = 0
        End If
        'NumABA = CInt(Session("ABA").ToString)
        'TabContainer1.ActiveTabIndex = NumABA
        'lblValidaNome.Visible = False
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init

        Dim divBarraProgresso As New HtmlControls.HtmlGenericControl("div")
        Dim divFundoDesabilitado As New HtmlControls.HtmlGenericControl("div")

        divBarraProgresso.Attributes.Add("id", "progresso")
        divBarraProgresso.Attributes.Add("class", "progressoAtualizacao")
        divBarraProgresso.InnerHtml = "<br/> Aguarde. <br/><br/> Carregando <img src=IMAGENS/animacao_carregando_barra.gif border=0>"

        divFundoDesabilitado.Attributes.Add("id", "fundoDesab")
        divFundoDesabilitado.Attributes.Add("class", "fundoDesabilitado")

        uppTab.Controls.Add(divBarraProgresso)
        uppTab.Controls.Add(divFundoDesabilitado)

        Dim divBarraProgresso2 As New HtmlControls.HtmlGenericControl("div")
        Dim divFundoDesabilitado2 As New HtmlControls.HtmlGenericControl("div")

        divBarraProgresso2.Attributes.Add("id", "progresso2")
        divBarraProgresso2.Attributes.Add("class", "progressoAtualizacao")
        divBarraProgresso2.InnerHtml = "<br/> Aguarde. <br/><br/> Carregando <img src=IMAGENS/animacao_carregando_barra.gif border=0>"

        divFundoDesabilitado2.Attributes.Add("id", "fundoDesab2")
        divFundoDesabilitado2.Attributes.Add("class", "fundoDesabilitado")

        uppPrincipal.Controls.Add(divBarraProgresso2)
        uppPrincipal.Controls.Add(divFundoDesabilitado2)

    End Sub

    Protected Sub TabContainer1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TabContainer1.ActiveTabChanged
        logger.Debug("TabContainer1_Click ...")
        Session("ABA") = TabContainer1.ActiveTabIndex
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        logger.Debug("BtnGravar_Click ...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If

        If txtNome.Text = "" Then
            MsgErro("Favor informar o nome.")
            Exit Sub
        End If

        Dim Existe_CPF As Boolean
        Dim Contador As Integer
        Dim BalCom As New BALCOMARCA(GetUsuarioExt)
        Dim Bal As New BALPERITO(GetUsuarioExt)
        Dim BalPerNur As New BalPerito_Comarca(GetUsuarioExt)
        Dim balTel As New BalTelefone(GetUsuarioExt)
        Dim entTel As New EntTelefone
        Dim balProf As New BALEspecialidadePerito(GetUsuarioExt)
        Dim entProf As New EntEspecialidade_Perito
        Dim n As Integer = 0
        Dim nIDPF As Long = 0

        If txtCPF.Text = "" Then
            MsgErro("O Preenchimento do CPF é obrigatório")
            Exit Sub
        End If
        Existe_CPF = Bal.VerCPFPeritoExt(txtCPF.Text)
        If Existe_CPF Then
            MsgErro("CPF já cadastrado. Favor comparecer na DIPEJ para atualização do cadastro.")
            Exit Sub
        End If

        If txtNome.Text = "" Or txtCPF.Text = "" Then
            If txtNome.Text = "" Then
                MsgErro("Gravação Rejeitada. Sem Nome")
                Exit Sub
            ElseIf txtCPF.Text = "" Then
                MsgErro("Gravação Rejeitada. Sem CPF")
                Exit Sub
            End If
        End If

        ent.Cod_Perito = 0
        ent.Nome = txtNome.Text
        ent.Num_Reg = txtNum_Reg.Text
        ent.Cod_Tip_End = 2
        ent.Cod_Tip_End1 = 1

        If Not IsNumeric(CboTip_Logr.Text) Then
            ent.Cod_Tip_Logr = 0
        Else
            ent.Cod_Tip_Logr = CInt(CboTip_Logr.Items.FindByValue(CboTip_Logr.Text).Value)
        End If
        ent.Nome_Logr = txtNome_Logr.Text
        ent.Num_Logr = txtNum_Logr.Text
        ent.Compl_Logr = txtCompl_Logr.Text
        If Not IsNumeric(CboBairro.Text) Then
            ent.Cod_Bairro = 0
        Else
            ent.Cod_Bairro = CInt(CboBairro.Items.FindByValue(CboBairro.Text).Value)
        End If
        If Not IsNumeric(CboCidade.Text) Then
            ent.Cod_Cidade = 0
        Else
            ent.Cod_Cidade = CInt(CboCidade.Items.FindByValue(CboCidade.Text).Value)
        End If
        If Not IsNumeric(CboTip_Logr1.Text) Then
            ent.Cod_Tip_Logr1 = 0
        Else
            ent.Cod_Tip_Logr1 = CInt(CboTip_Logr1.Items.FindByValue(CboTip_Logr1.Text).Value)
        End If
        ent.Nome_Logr1 = txtNome_Logr1.Text
        ent.Num_Logr1 = txtNum_Logr1.Text
        ent.Compl_Logr1 = txtCompl_Logr1.Text
        If Not IsNumeric(CboBairro1.Text) Then
            ent.Cod_Bairro1 = 0
        Else
            ent.Cod_Bairro1 = CInt(CboBairro1.Items.FindByValue(CboBairro1.Text).Value)
        End If
        If Not IsNumeric(CboCidade1.Text) Then
            ent.Cod_Cidade1 = 0
        Else
            ent.Cod_Cidade1 = CInt(CboCidade1.Items.FindByValue(CboCidade1.Text).Value)
        End If

        If txtCEP.Text <> "" And IsNumeric(txtCEP.Text) Then
            ent.CEP = txtCEP.Text
        Else
            ent.CEP = ""
        End If
        If txtCEP1.Text <> "" And IsNumeric(txtCEP1.Text) Then
            ent.CEP1 = txtCEP1.Text
        Else
            ent.CEP1 = ""
        End If
        If Mid(UCase(CboUF.Text), 1, 9) = "SELECIONE" Then
            ent.Sigla_UF = ""
        Else
            ent.Sigla_UF = CboUF.Text
        End If
        If Mid(UCase(CboUF1.Text), 1, 9) = "SELECIONE" Then
            ent.Sigla_UF1 = ""
        Else
            ent.Sigla_UF1 = CboUF.Text
        End If

        ent.Cod_Tip_Sit = 2 'Inativo 'CInt(IIf(RdAtivo.Items(0).Selected, 1, 2))
        ent.OBS = "" 'txtObs.Text
        If Not IsNumeric(CboEspecialidade.SelectedValue) Then
            ent.COD_ESPECIALIDADE = 0
        Else
            ent.COD_ESPECIALIDADE = CInt(CboEspecialidade.SelectedValue)
        End If
        If Not IsNumeric(CboProfissao.SelectedValue) Then
            ent.COD_PROFISSAO = 0
        Else
            ent.COD_PROFISSAO = CInt(CboProfissao.SelectedValue)
        End If

        ent.SIGLA = "INTERNET"

        ent.Data_Cadastramento = CDate(Today.ToShortDateString)  'CDate(txtData_Cadastramento.Data) 'txtData_Cadastramento.Data1)
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        ent.DESCR_ESPECIALIDADE = ""
        ent.DESCR_PROFISSAO = ""
        ent.DESCR_ESPECIALIDADE1 = ""
        ent.DESCR_PROFISSAO1 = ""
        If IsNumeric(txtCPF.Text) Then
            ent.CPF = txtCPF.Text
        Else
            ent.CPF = ""
            MsgErro("O CPF '" & ent.CPF & "' é Inválido. Gravação Rejeitada")
            Exit Sub
        End If
        ent.SITUACAO_CADASTRO = "P" 'CStr(IIf(RdSit.Items(0).Selected, "O", "P"))
        If Not IsNumeric(CboOrgao_Per.Text) Then
            ent.COD_ORGAO_PER = 0
        Else
            ent.COD_ORGAO_PER = CInt(CboOrgao_Per.Items.FindByValue(CboOrgao_Per.Text).Value)  'Descr_Orgao_Per ... CREA, OAB, ...
        End If
       
        If txtDt_Nasc.Text = "" Then
            ent.Dt_Nasc = Nothing
        Else
            ent.Dt_Nasc = CDate(txtDt_Nasc.Text)
        End If
        ent.EMAIL = txtEmail.Text.ToString()
        ent.EMAIL1 = txtEmail1.Text.ToString()

        Dim ExisteCPFPerito As Boolean
        If txtCPF.Text <> "" Then
            ExisteCPFPerito = Bal.VerCPFPeritoExt(txtCPF.Text)
        Else
            ExisteCPFPerito = False
        End If
        If Not ExisteCPFPerito Then

            nIDPF = Bal.GravarExt(ent)

            If nIDPF = 0 Then
                MsgErro("Houve falha na gravação. Favor entrar em contato com a DIPEJ.")
                Exit Sub
            End If

            ''Telefone
            Dim dtTelefone As DataTable
            dtTelefone = getSessionTelefone()

            If Not dtTelefone Is Nothing Then
                For Each r As DataRow In dtTelefone.Rows
                    entTel.Cod_Tip_Tel = r("cod_tip_tel")
                    entTel.DDD = r("ddd")
                    entTel.Ramal = r("num_ramal")
                    entTel.SeqTel = 0
                    entTel.Tel = r("num_tel")
                    'balTel.InserirPessoaFisicaTelefone(entTel, nIDPF)
                    balTel.InserirPessoaFisicaTelefone(entTel, nIDPF, ent.Cod_Perito)
                Next
                removeSessionTelefone()
                entTel = Nothing
                dtTelefone = Nothing
            End If

            'Profissão
            Dim dtProfissao As DataTable
            dtProfissao = getSessionProfissao()
            If Not dtProfissao Is Nothing Then
                For Each r As DataRow In dtProfissao.Rows
                    entProf.COD_ESPECIALIDADE = r("cod_especialidade")
                    entProf.COD_ORGAO_PER = r("cod_org_profissional")
                    entProf.COD_PROFISSAO = r("cod_profissao")
                    entProf.ID_PF = nIDPF
                    entProf.UF = r("uf")
                    balProf.Inserir(entProf)
                Next
                removeSessionProfissao()
                entProf = Nothing
                dtProfissao = Nothing
            End If
        Else
            MsgErro("Inserção rejeitada. Existe Perito Cadastrado com este CPF")
            Exit Sub
        End If
        MsgErro("Pré-cadastro realizado com sucesso! O prazo para entrega da documentação necessária é de 30 dias, devendo entrar em contato com a DIPEJ para agendamento. Telefones 3133-4494 / 3133-3773. A relação de documentos está disponível neste site.")

        Contador = 0
        DsNurTotal = BalCom.ExibirDadosSet()
        DsNur1 = CType(Session("DsNur1"), DataSet)
        If DsNur1 Is Nothing Then
            DsNur1 = BalCom.ExibirDadosSet(1)
        End If
        For Each rs As DataRow In DsNur1.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 1
            Contador = Contador + 1
        Next
        DsNur2 = CType(Session("DsNur2"), DataSet)
        If DsNur2 Is Nothing Then
            DsNur2 = BalCom.ExibirDadosSet(2)
        End If
        For Each rs As DataRow In DsNur2.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 2
            Contador = Contador + 1
        Next
        DsNur3 = CType(Session("DsNur3"), DataSet)
        If DsNur3 Is Nothing Then
            DsNur3 = BalCom.ExibirDadosSet(3)
        End If
        For Each rs As DataRow In DsNur3.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 3
            Contador = Contador + 1
        Next
        DsNur4 = CType(Session("DsNur4"), DataSet)
        If DsNur4 Is Nothing Then
            DsNur4 = BalCom.ExibirDadosSet(4)
        End If
        For Each rs As DataRow In DsNur4.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 4
            Contador = Contador + 1
        Next
        DsNur5 = CType(Session("DsNur5"), DataSet)
        If DsNur5 Is Nothing Then
            DsNur5 = BalCom.ExibirDadosSet(5)
        End If
        For Each rs As DataRow In DsNur5.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 5
            Contador = Contador + 1
        Next
        DsNur6 = CType(Session("DsNur6"), DataSet)
        If DsNur6 Is Nothing Then
            DsNur6 = BalCom.ExibirDadosSet(6)
        End If
        For Each rs As DataRow In DsNur6.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 6
            Contador = Contador + 1
        Next
        DsNur7 = CType(Session("DsNur7"), DataSet)
        If DsNur7 Is Nothing Then
            DsNur7 = BalCom.ExibirDadosSet(7)
        End If
        For Each rs As DataRow In DsNur7.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 7
            Contador = Contador + 1
        Next
        DsNur8 = CType(Session("DsNur8"), DataSet)
        If DsNur8 Is Nothing Then
            DsNur8 = BalCom.ExibirDadosSet(8)
        End If
        For Each rs As DataRow In DsNur8.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 8
            Contador = Contador + 1
        Next
        DsNur9 = CType(Session("DsNur9"), DataSet)
        If DsNur9 Is Nothing Then
            DsNur9 = BalCom.ExibirDadosSet(9)
        End If
        For Each rs As DataRow In DsNur9.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 9
            Contador = Contador + 1
        Next
        DsNur10 = CType(Session("DsNur10"), DataSet)
        If DsNur10 Is Nothing Then
            DsNur10 = BalCom.ExibirDadosSet(10)
        End If
        For Each rs As DataRow In DsNur10.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 10
            Contador = Contador + 1
        Next
        DsNur11 = CType(Session("DsNur11"), DataSet)
        If DsNur11 Is Nothing Then
            DsNur11 = BalCom.ExibirDadosSet(11)
        End If
        For Each rs As DataRow In DsNur11.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 11
            Contador = Contador + 1
        Next
        DsNur12 = CType(Session("DsNur12"), DataSet)
        If DsNur12 Is Nothing Then
            DsNur12 = BalCom.ExibirDadosSet(12)
        End If
        For Each rs As DataRow In DsNur12.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 12
            Contador = Contador + 1
        Next
        DsNur13 = CType(Session("DsNur13"), DataSet)
        If DsNur13 Is Nothing Then
            DsNur13 = BalCom.ExibirDadosSet(13)
        End If
        For Each rs As DataRow In DsNur13.Tables(0).Rows
            DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
            DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
            DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
            DsNurTotal.Tables(0).Rows(Contador).Item(3) = 13
            Contador = Contador + 1
        Next
        BalPerNur.ExcluirPerNur(nIDPF)
        BalPerNur.GravarPerito_Comarca(DsNurTotal, CInt(nIDPF))

        Limpar()
    End Sub

    Private Sub CboUF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboUF.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug("CboUF_SelectedIndexChanged ...")
            PreencherCidade(CboUF.SelectedItem.Value)
            PreencherBairro(CboCidade.SelectedItem.Value)
        End If
    End Sub

    Private Sub CboCidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCidade.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug("CboCidade_SelectedIndexChanged ...")
            PreencherBairro(CboCidade.SelectedItem.Value)
        End If
    End Sub
    Private Sub CboUF1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboUF1.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug("CboUF1_SelectedIndexChanged ...")
            PreencherCidade1(CboUF1.SelectedItem.Value)
            PreencherBairro1(CboCidade1.SelectedItem.Value)
        End If
    End Sub

    Private Sub PreencherTip_Logr()
        logger.Debug("PreencherTip_Logr ...")
        Dim bal As New BalTipoLogradouro(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboTip_Logr.Items.Clear()
        CboTip_Logr.DataTextField = "Descr"
        CboTip_Logr.DataValueField = "Cod_tip_Logr"
        CboTip_Logr.DataSource = dsfila.Tables(0) '.DefaultView
        CboTip_Logr.DataBind()
        CboTip_Logr.Items.Insert(0, "Selecione Tipo Logr.")
        CboTip_Logr.SelectedIndex = 0

    End Sub
    Private Sub PreencherTip_Logr1()
        logger.Debug("PreencherTip_Logr1 ...")
        Dim bal As New BalTipoLogradouro(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboTip_Logr1.Items.Clear()
        CboTip_Logr1.DataTextField = "Descr"
        CboTip_Logr1.DataValueField = "Cod_tip_Logr"
        CboTip_Logr1.DataSource = dsfila.Tables(0) '.DefaultView
        CboTip_Logr1.DataBind()
        CboTip_Logr1.Items.Insert(0, "Selecione Tipo Logr.")
        CboTip_Logr1.SelectedIndex = 0

    End Sub

    Private Sub PreencherUF()
        logger.Debug("PreencherUF ...")
        Dim bal As New BALCIDADE(GetUsuarioExt)
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

    End Sub

    Private Sub PreencherUF1()
        logger.Debug("PreencherUF1 ...")
        Dim bal As New BALCIDADE(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosUFSet()
        CboUF1.Items.Clear()
        CboUF1.Items.Insert(0, "Selecione o Estado")
        i = 0
        For Each rs As DataRow In dsfila.Tables(0).Rows
            If Not IsDBNull(rs("SIGLA_UF")) Then
                i = i + 1
                CboUF1.Items.Insert(i, rs("SIGLA_UF").ToString)
            End If
        Next
        CboUF1.SelectedIndex = 21

    End Sub

    Private Sub PreencherBairro(ByVal m_Cidade As String)
        logger.Debug("PreencherBairro(" & m_Cidade & ")")
        If IsDBNull(m_Cidade) Or m_Cidade = "Selecione a cidade" Then
            MsgErro("Selecione a Cidade")
            Exit Sub
        End If
        Dim nCod_Cid As Long
        Dim bal As New BALBairro(GetUsuarioExt)
        Dim dsfila As New DataSet
        nCod_Cid = Convert.ToInt64(CboCidade.Items.FindByValue(m_Cidade).Value)
        dsfila = bal.ExibirDadosSet(nCod_Cid)
        CboBairro.DataSource = Nothing
        CboBairro.DataBind()
        CboBairro.DataTextField = "Nome"
        CboBairro.DataValueField = "Cod_Bai"
        CboBairro.DataSource = dsfila.Tables(0)
        CboBairro.DataBind()
        CboBairro.Items.Insert(0, "Selecione o bairro")
        CboBairro.SelectedIndex = 0

    End Sub

    Private Sub PreencherBairro1(ByVal m_Cidade As String)
        logger.Debug("PreencherBairro1(" & m_Cidade & ")")
        If IsDBNull(m_Cidade) Or m_Cidade = "Selecione a cidade" Then
            MsgErro("Selecione a cidade")
            Exit Sub
        End If
        Dim nCod_Cid As Long
        Dim bal As New BALBairro(GetUsuarioExt)
        Dim dsfila As New DataSet

        nCod_Cid = Convert.ToInt64(CboCidade.Items.FindByValue(m_Cidade).Value)
        dsfila = bal.ExibirDadosSet(nCod_Cid)
        CboBairro1.Items.Clear()
        CboBairro1.DataTextField = "Nome"
        CboBairro1.DataValueField = "Cod_Bai"
        CboBairro1.DataSource = dsfila.Tables(0)
        CboBairro1.DataBind()
        CboBairro1.Items.Insert(0, "Selecione o bairro")
        CboBairro1.SelectedIndex = 0
    End Sub

    Private Sub PreencherCidade1(ByVal m_UF As String)
        logger.Debug("PreencherCidade1(" & m_UF & ")")
        If IsDBNull(m_UF) Or m_UF = "Selecione o Estado" Then
            MsgErro("Selecione o Estado - UF")
            Exit Sub
        End If
        Dim bal As New BALCIDADE(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_UF)
        CboCidade1.Items.Clear()
        CboCidade1.DataTextField = "Nome"
        CboCidade1.DataValueField = "Cod_Cid"
        CboCidade1.DataSource = dsfila.Tables(0).DefaultView
        CboCidade1.DataBind()
        CboCidade1.Items.Insert(0, "Selecione a cidade")
        If m_UF = "RJ" Then
            CboCidade1.SelectedIndex = 230
        End If

    End Sub

    Private Sub PreencherCidade(ByVal m_UF As String)
        logger.Debug("PreencherCidade(" & m_UF & ") ...")
        If IsDBNull(m_UF) Or m_UF = "Selecione o Estado" Then
            MsgErro("Selecione o Estado - UF")
            Exit Sub
        End If
        Dim bal As New BALCIDADE(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_UF)
        CboCidade.Items.Clear()
        CboCidade.DataTextField = "Nome"
        CboCidade.DataValueField = "Cod_Cid"
        CboCidade.DataSource = dsfila.Tables(0).DefaultView
        CboCidade.DataBind()
        CboCidade.Items.Insert(0, "Selecione a cidade")
        If m_UF = "RJ" Then
            CboCidade.SelectedIndex = 230
        End If

    End Sub

    Private Sub Limpar()
        logger.Debug(" Limpar()...")
        TabContainer1.ActiveTabIndex = 0
        txtNome.Text = ""
        txtCPF.Text = ""
        txtDt_Nasc.Text = ""
        txtNum_Reg.Text = ""
        CboOrgao_Per.SelectedIndex = 0
        DsNurTotal.Clear()
        DsNur1.Clear()

        Session.Remove("DsNur")
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

        ''''''''''''''''''''''''''''''
        'Endereço residencial
        txtCEP.Text = ""
        CboCidade.SelectedIndex = 0
        CboBairro.SelectedIndex = 0
        CboTip_Logr.SelectedIndex = 0
        txtCompl_Logr.Text = ""
        txtNome_Logr.Text = ""
        txtNum_Logr.Text = ""

        'Endereço Comercial
        txtCEP1.Text = ""
        CboCidade1.SelectedIndex = 0
        CboBairro1.SelectedIndex = 0
        CboTip_Logr1.SelectedIndex = 0

        txtCompl_Logr1.Text = ""
        txtNome_Logr1.Text = ""
        txtNum_Logr1.Text = ""

        txtEmail.Text = ""
        txtEmail1.Text = ""

        LimpaCamposProfissao()
        removeSessionProfissao()

        LimparCamposTelefone()
        removeSessionTelefone()

        GrdProfissao.DataSource = Nothing
        GrdProfissao.DataBind()
        GrdTel.DataSource = Nothing
        GrdTel.DataBind()

        upTab.Update()

    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        logger.Debug("BtnLimpar_Click ...")
        Limpar()
    End Sub

    Protected Sub CboCidade1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboCidade1.SelectedIndexChanged
        If Me.IsPostBack Then
            logger.Debug("CboCidade1_SelectedIndexChanged ...")
            PreencherBairro1(CboCidade1.SelectedItem.Value)
        End If
    End Sub

    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCPF.TextChanged
        logger.Debug("txtCPF_TextChanged ...")
        Dim bal As New BALPERITO(GetUsuarioExt)
        If txtCPF.Text <> "" Then
            Dim m_CPF As String
            m_CPF = txtCPF.Text
            If Not ValidarCPFServ(txtCPF.Text) And txtCPF.Text <> "" Then
                MsgErro("CPF Inválido")
                txtCPF.Text = ""
                txtCPF.Focus()
                upPrincipal.Update()
                Exit Sub
            Else
                If bal.VerCPFPeritoExt(txtCPF.Text) Then
                    MsgErro("CPF já cadastrado. Favor comparecer na DIPEJ para atualização do cadastro.")
                    txtCPF.Text = ""
                    txtCPF.Focus()
                    Exit Sub
                Else
                    txtNome.Focus()
                    upPrincipal.Update()
                End If
            End If
            '  TabContainer1.ActiveTabIndex = 0
        End If
        'TabContainer1.Visible = True
        upPrincipal.Update()

    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        logger.Debug("CboProfissao_SelectedIndexChanged ...")
        PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
        Session("ABA") = 3
    End Sub

    Protected Sub CboOrgao_Per_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboOrgao_Per.SelectedIndexChanged
        logger.Debug("CboOrgao_Per_SelectedIndexChanged ...")
        PreencherSiglaProfissao()
        txtNum_Reg.Focus()
    End Sub

    Protected Sub TxtCEP1_TextChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles txtCEP1.TextChanged
        logger.Debug("TxtCEP1_TextChanged1 ...")
        Dim Bal As New BalCEP(GetUsuarioExt)
        Dim Ent As New EntCEP
        If Not Me.IsPostBack Or txtCEP1.Text = "" Then
            Exit Sub
        End If
        If Not IsNumeric(txtCEP1.Text) Then
            MsgErro("O CEP somente deverá possuir valores numéricos")
            txtCEP1.Text = ""
            Exit Sub
        End If

        Ent = Bal.ExibirDadosEnt(txtCEP1.Text)
        If Not Ent Is Nothing Then
            txtNome_Logr1.Text = Ent.Logradouro

            If CboUF1.Items.FindByValue(Ent.Sigla_UF.ToString) Is Nothing Then
                CboUF1.SelectedIndex = 0
            Else
                CboUF1.SelectedValue = CboUF1.Items.FindByValue(Ent.Sigla_UF.ToString).Value
            End If

            If Ent.Cod_Cid.ToString <> "" Then
                PreencherCidade1(Ent.Sigla_UF.ToString())
                If CboCidade1.Items.FindByValue(Ent.Cod_Cid.ToString) Is Nothing Then
                    CboCidade1.SelectedIndex = 0
                Else
                    CboCidade1.SelectedValue = CboCidade1.Items.FindByValue(Ent.Cod_Cid.ToString).Value
                End If
            End If
            If Ent.Cod_Bai.ToString <> "" Then
                PreencherBairro1(Ent.Cod_Cid.ToString())
                If CboBairro1.Items.FindByValue(Ent.Cod_Bai.ToString) Is Nothing Then
                    CboBairro1.SelectedIndex = 0
                Else
                    CboBairro1.SelectedValue = CboBairro1.Items.FindByValue(Ent.Cod_Bai.ToString).Value
                End If
            End If
        End If

        'Session("ABA") = 2
        'TabContainer1.ActiveTabIndex = 2
    End Sub

    Private Sub TabContainer1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.Load
        logger.Debug("TabContainer1_Load ...")
        NumABA = CInt(Session("ABA").ToString)
        TabContainer1.ActiveTabIndex = NumABA
    End Sub

    Protected Sub txtNome_Logr1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome_Logr1.TextChanged

    End Sub

    Protected Sub txtCEP_TextChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles txtCEP.TextChanged
        logger.Debug("txtCEP_TextChanged1 ...")
        Dim Bal As New BalCEP(GetUsuarioExt)
        Dim Ent As New EntCEP
        If Not Me.IsPostBack Or txtCEP.Text = "" Then
            Exit Sub
        End If
        If Not IsNumeric(txtCEP.Text) Then
            MsgErro("O CEP somente deverá possuir valores numéricos")
            txtCEP.Text = ""
            Exit Sub
        End If
        Ent = Bal.ExibirDadosEnt(txtCEP.Text)
        If Not Ent Is Nothing Then
            txtNome_Logr.Text = Ent.Logradouro

            If CboUF.Items.FindByValue(Ent.Sigla_UF.ToString) Is Nothing Then
                CboUF.SelectedIndex = 0
            Else
                CboUF.SelectedValue = CboUF.Items.FindByValue(Ent.Sigla_UF.ToString).Value
            End If

            If Ent.Cod_Cid.ToString <> "" Then
                PreencherCidade(Ent.Sigla_UF.ToString)
                If CboCidade.Items.FindByValue(Ent.Cod_Cid.ToString) Is Nothing Then
                    CboCidade.SelectedIndex = 0
                Else
                    CboCidade.SelectedValue = CboCidade.Items.FindByValue(Ent.Cod_Cid.ToString).Value
                End If
            End If

            If Ent.Cod_Bai.ToString <> "" Then
                PreencherBairro(Ent.Cod_Cid.ToString)
                If CboBairro.Items.FindByValue(Ent.Cod_Bai.ToString) Is Nothing Then
                    CboBairro.SelectedIndex = 0
                Else
                    CboBairro.SelectedValue = CboBairro.Items.FindByValue(Ent.Cod_Bai.ToString).Value
                End If
            End If
        End If
    End Sub

    Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged
        logger.Debug("CboEspecialidade_SelectedIndexChanged ...")
        'Verifica se especialidade já selecionada
        If CboEspecialidade.SelectedIndex = 0 Or CboEspecialidade.SelectedIndex = -1 Then
            Exit Sub
        End If
        If EspecialidadeSelecionada(CboEspecialidade.SelectedValue.ToString) Then
            MsgErro("Especialidade já selecionada. Favor selecionar outra especialidade.")
            CboEspecialidade.SelectedIndex = 0
        End If
    End Sub

#Region "Manipulação dos dados de Profissão"

    Private Sub PreencherUFProfissao()
        logger.Debug("PreencherUFProfissao ...")
        Dim bal As New BALCIDADE(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosUFSet()
        drpUFProf.Items.Clear()
        drpUFProf.Items.Insert(0, "UF")
        i = 0
        For Each rs As DataRow In dsfila.Tables(0).Rows
            If Not IsDBNull(rs("SIGLA_UF")) Then
                i = i + 1
                drpUFProf.Items.Insert(i, rs("SIGLA_UF").ToString)
            End If
        Next
        drpUFProf.SelectedIndex = 21
    End Sub

    Private Sub PreencherOrgao_Per()
        logger.Debug("PreencherOrgao_Per ...")
        Dim bal As New BalOrgao_Per(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboOrgao_Per.Items.Clear()
        CboOrgao_Per.DataTextField = "Descr_Orgao_Per"
        CboOrgao_Per.DataValueField = "Cod_Orgao_Per"
        CboOrgao_Per.DataSource = dsfila.Tables(0)
        CboOrgao_Per.DataBind()
        CboOrgao_Per.Items.Insert(0, "Selecione o órgão profissional")
        CboOrgao_Per.SelectedIndex = 0
    End Sub

    Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)
        logger.Debug("PreencherEspecialidade ...")
        Dim bal As New BALEspecialidade(GetUsuarioExt)
        Dim ent As New EntEspecialidade
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_Cod_Profissao)
        If dsfila.Tables(0).Rows.Count > 0 Then
            CboEspecialidade.Items.Clear()
            CboEspecialidade.DataTextField = "Descr_Especialidade"
            CboEspecialidade.DataValueField = "Cod_Especialidade"
            CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
            CboEspecialidade.DataBind()
        End If
        CboEspecialidade.Items.Insert(0, "Selecione a especialidade")
        CboEspecialidade.SelectedIndex = 0

    End Sub

    Private Sub PreencherPROFISSAO()
        logger.Debug("PreencherPROFISSAO ...")
        Dim bal As New BALProfissao(GetUsuarioExt)
        Dim ent As New EntProfissao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboProfissao.Items.Clear()
        CboProfissao.DataTextField = "Descr_PROFISSAO"
        CboProfissao.DataValueField = "Cod_PROFISSAO"
        CboProfissao.DataSource = dsfila.Tables(0) '.DefaultView
        CboProfissao.DataBind()
        CboProfissao.Items.Insert(0, "Selecione a profissão")
        CboProfissao.SelectedIndex = 0
        CboEspecialidade.Items.Clear()
    End Sub

    Private Sub PreencherSiglaProfissao()
        logger.Debug("PreencherSiglaProfissao ...")
        Dim bal As New BalOrgao_Per(GetUsuarioExt)

        If CboOrgao_Per.SelectedIndex = 0 Then
            MsgErro("Selecione um órgão profissional.")
            Exit Sub
        End If
        lblSiglaProf.Text = bal.ConsultarSiglaOrgaoProfissional(CInt(CboOrgao_Per.SelectedValue))
    End Sub

    Protected Sub btnAtualizarProf_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAtualizarProf.Click
        logger.Debug("btnAtualizarProf_Click ...")
        Dim dtProfissao As New DataTable
        Dim dt As DataTable
        dt = getSessionProfissao()

        If Not dt Is Nothing Then
            dtProfissao = dt
        Else
            dtProfissao.Columns.Add("cod_profissao")
            dtProfissao.Columns.Add("cod_especialidade")
            dtProfissao.Columns.Add("uf")
            dtProfissao.Columns.Add("sigla_per")
            dtProfissao.Columns.Add("NUM_REG")
            dtProfissao.Columns.Add("cod_org_profissional")
            dtProfissao.Columns.Add("descr_profissao")
            dtProfissao.Columns.Add("descr_especialidade")
        End If

        If CboProfissao.SelectedIndex = 0 Then
            MsgErro("Selecione a profissão")
            Exit Sub
        End If

        If CboEspecialidade.SelectedIndex = 0 Then
            MsgErro("Selecione a especialidade.")
            Exit Sub
        End If

        If CboOrgao_Per.SelectedIndex = 0 Then
            MsgErro("Selecione o orgão.")
            Exit Sub
        End If

        If drpUFProf.SelectedIndex = 0 Then
            MsgBox("Selecione a UF.")
            Exit Sub
        End If

        Dim dr As DataRow
        dr = dtProfissao.NewRow()
        dr("cod_profissao") = CboProfissao.SelectedValue
        dr("cod_especialidade") = CboEspecialidade.SelectedValue
        dr("uf") = drpUFProf.SelectedValue
        dr("sigla_per") = lblSiglaProf.Text
        dr("NUM_REG") = txtNum_Reg.Text
        dr("cod_org_profissional") = CboOrgao_Per.SelectedValue
        dr("descr_profissao") = CboProfissao.SelectedItem.Text
        dr("descr_especialidade") = CboEspecialidade.SelectedItem.Text
        dtProfissao.Rows.Add(dr)
        dtProfissao.AcceptChanges()

        setSessionProfissao(dtProfissao)
        ListarProfissaoPerito()
        LimpaCamposProfissao()
        upTab.Update()
    End Sub

    Protected Sub btnExcluirProf_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        logger.Debug("btnExcluirProf_Command ...")
        Dim dt As DataTable
        Dim dtResultado As New DataTable
        Dim sCodEspecialidade As String

        sCodEspecialidade = e.CommandArgument.ToString()

        dt = getSessionProfissao()
        For Each dr As DataRow In dt.Select("cod_especialidade = " & sCodEspecialidade)
            dt.BeginInit()
            dt.Rows.Remove(dr)
        Next
        dt.AcceptChanges()
        setSessionProfissao(dt)
        MsgErro("Profissão excluída com sucesso.", "Sucesso")
        ListarProfissaoPerito()

    End Sub

    Private Sub ListarProfissaoPerito()
        logger.Debug("ListarProfissaoPerito ...")
        GrdProfissao.DataSource = getSessionProfissao()
        GrdProfissao.DataBind()
    End Sub

    Private Sub setSessionProfissao(ByVal dt As DataTable)
        logger.Debug("setSessionProfissao ...")
        Session("dtProfissao") = dt
    End Sub

    Private Function getSessionProfissao() As DataTable
        logger.Debug("getSessionProfissao ...")
        Dim dt As New DataTable
        dt = CType(Session("dtProfissao"), DataTable)
        Return dt
    End Function

    Private Sub removeSessionProfissao()
        logger.Debug("removeSessionProfissao ...")
        Session.Remove("dtProfissao")
    End Sub

    Protected Sub GrdProfissao_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        logger.Debug("GrdProfissao_PageIndexChanging ...")
        GrdProfissao.PageIndex = e.NewPageIndex
        ListarProfissaoPerito()
    End Sub

    Private Sub LimpaCamposProfissao()
        logger.Debug("LimpaCamposProfissao ...")
        If CboProfissao.SelectedIndex <> -1 Then CboProfissao.SelectedIndex = 0
        If CboEspecialidade.SelectedIndex <> -1 Then CboEspecialidade.SelectedIndex = 0
        If CboOrgao_Per.SelectedIndex <> -1 Then CboOrgao_Per.SelectedIndex = 0

        txtNum_Reg.Text = ""
        lblSiglaProf.Text = ""
        drpUFProf.SelectedIndex = 21
    End Sub

    Private Function EspecialidadeSelecionada(ByVal sCodEspecialidade As String) As Boolean
        logger.Debug("EspecialidadeSelecionada ...")
        Dim dt As DataTable
        dt = getSessionProfissao()

        If dt Is Nothing Then
            Return False
            Exit Function
        End If

        If dt.Select("cod_especialidade = " & sCodEspecialidade).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Manipula dados do telefone"

    Private Sub ListarTelefone()
        logger.Debug("ListarTelefone ...")
        GrdTel.DataSource = getSessionTelefone()
        GrdTel.DataBind()
    End Sub

    Private Sub setSessionTelefone(ByVal dt As DataTable)
        logger.Debug("setSessionTelefone ...")
        Session("dtTelefone") = dt
    End Sub

    Private Function getSessionTelefone() As DataTable
        logger.Debug("getSessionTelefone ...")
        Dim dt As DataTable
        dt = CType(Session("dtTelefone"), DataTable)
        Return dt
    End Function

    Private Sub removeSessionTelefone()
        logger.Debug("removeSessionTelefone ...")
        Session.Remove("dtTelefone")
    End Sub

    Private Sub LimparCamposTelefone()
        logger.Debug("LimparCamposTelefone ...")
        cboCodTipTel.SelectedIndex = 0
        txtTel.Text = ""
        txtDDD.Text = ""
        TxtRamal.Text = ""
    End Sub

    Protected Sub btnExcluirTel_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        logger.Debug("btnExcluirTel_Command ...")
        Dim dt As DataTable
        Dim sNumTel As String
        dt = getSessionProfissao()
        sNumTel = e.CommandArgument.ToString

        dt = getSessionTelefone()

        For Each r As DataRow In dt.Select("num_tel = " & sNumTel)
            dt.BeginInit()
            dt.Rows.Remove(r)
        Next
        dt.AcceptChanges()
        setSessionProfissao(dt)
        MsgErro("Telefone excluído.", "Sucesso")
        ListarTelefone()
    End Sub

    Protected Sub btnGravarTel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGravarTel.Click
        logger.Debug("btnGravarTel_Click ...")

        If cboCodTipTel.SelectedIndex = 0 Then
            MsgErro("Selecione o tipo de telefone.")
            SetFocus(cboCodTipTel)
            Exit Sub
        End If

        If txtDDD.Text = "" Then
            MsgErro("Informe o DDD.")
            SetFocus(txtDDD)
            Exit Sub
        End If

        If txtTel.Text = "" Then
            MsgErro("Informe o número do telefone")
            SetFocus(txtTel)
            Exit Sub
        End If

        Dim dtTelefone As New DataTable
        Dim dt As DataTable
        dt = getSessionTelefone()

        If Not dt Is Nothing Then
            dtTelefone = dt
        Else
            dtTelefone.Columns.Add("cod_tip_tel")
            dtTelefone.Columns.Add("desc_tip_tel")
            dtTelefone.Columns.Add("num_tel")
            dtTelefone.Columns.Add("ddd")
            dtTelefone.Columns.Add("num_ramal")
        End If

        Dim r As DataRow
        r = dtTelefone.NewRow()
        r("cod_tip_tel") = cboCodTipTel.SelectedValue
        r("desc_tip_tel") = cboCodTipTel.SelectedItem.Text
        r("num_tel") = txtTel.Text
        r("ddd") = txtDDD.Text
        r("num_ramal") = TxtRamal.Text
        dtTelefone.Rows.Add(r)

        dtTelefone.AcceptChanges()
        setSessionTelefone(dtTelefone)
        ListarTelefone()
        LimparCamposTelefone()
        upTab.Update()
    End Sub

    Protected Sub GrdTel_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        logger.Debug("GrdTel_PageIndexChanging ...")
        GrdTel.PageIndex = e.NewPageIndex
        ListarTelefone()
    End Sub

    Private Function TelefoneSelecionado(ByVal sNumTel As String) As Boolean
        logger.Debug("TelefoneSelecionado ...")
        Dim dt As DataTable
        dt = getSessionTelefone()

        If dt Is Nothing Then
            Return False
            Exit Function
        End If

        If dt.Select("num_tel = " & sNumTel).Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

    Protected Sub txtTel_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTel.TextChanged
        logger.Debug("txtTel_TextChanged ...")
        If txtTel.Text <> "" Then
            If TelefoneSelecionado(txtTel.Text) Then
                MsgErro("Número de telefone " & txtTel.Text & " já foi informado. Favor informar outro número.")
                txtTel.Text = ""
                SetFocus(txtTel)
            End If
        End If
    End Sub

    Protected Sub cboCodTipTel_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCodTipTel.SelectedIndexChanged
        logger.Debug("cboCodTipTel_SelectedIndexChanged ...")
        If cboCodTipTel.SelectedValue = "2" Then
            lblRamal.Visible = True
            TxtRamal.Visible = True
        Else
            lblRamal.Visible = False
            TxtRamal.Visible = False
        End If
    End Sub

    Protected Sub btnNurMarcarTodos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNurMarcarTodos.Click
        'Nur1
        PreencherNurs(1)
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
        PreencherNurs(2)
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
        PreencherNurs(3)
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
        PreencherNurs(4)
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
        PreencherNurs(5)
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
        PreencherNurs(6)
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
        PreencherNurs(7)
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
        PreencherNurs(8)
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
        PreencherNurs(9)
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
        PreencherNurs(10)
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
        PreencherNurs(11)
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
        PreencherNurs(12)
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
        PreencherNurs(13)
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

    Public Sub PreencherNurs(ByVal nNur As Integer)

        Dim balComarca As New BALCOMARCA(GetUsuarioExt)
        Dim ds As DataSet

        ds = CType(Session("DsNur" & nNur), DataSet)
        If ds Is Nothing Then
            ds = balComarca.ExibirDadosNurExt(nNur)
            Session("DsNur" & nNur) = ds
        End If

    End Sub
End Class
