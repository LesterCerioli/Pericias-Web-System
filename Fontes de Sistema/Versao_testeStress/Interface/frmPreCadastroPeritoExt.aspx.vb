Option Strict On


Imports ServicoDadosODPNET

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET

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

    'Public CodOrg As Integer
    'Public Login As String
    'Public NomeMaquina As String
    'Public Senha As String
    'Public Senhautent As String
    'Public SiglaSist As String = "PERICIAS"
    'Public UsuarioSO As String


    Private ent As New EntPERITO
    Dim i, j As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Try
                ' ''Tempo2 = TimeOfDay.Second
                ' ''If Tempo1 > TimeOfDay.Second Then
                ' ''    Duracao1 = TimeOfDay.Second - (60 - Tempo1)
                ' ''Else
                ' ''    Duracao1 = TimeOfDay.Second - Tempo1
                ' ''End If
                Session("CV") = "N"
                If Session("ABA") Is Nothing Then
                    Session("ABA") = 0
                End If
                'txtCPF.Attributes.Add("onblur", "validacpf(ctl00$Tela$txtCPF.value,'" & txtCPF.ClientID & "');")
                txtCPF.Attributes.Add("onblur", "validacpf('" & txtCPF.ClientID & "');")
                'txtNome.Attributes.Add("onblur", "validacpf('" & txtCPF.ClientID & "');")

                txtDt_Nasc.Attributes.Add("onblur", "validardata('" & txtDt_Nasc.ClientID & "');")
                'If txtCPF.Text = "" Then
                'Exit Sub
                'End If
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
                PreencherPROFISSAO1()
                'PreencherBanco()
                'TabContainer1.ActiveTabIndex = 0
                PreencherNurs()
            Catch ex As Exception
                MsgErro(ex.Message)
            End Try
            '[enter] virando [tab]
            txtCod_Perito.Attributes.Add("onkeydown", "if(event.which==13){ event.which = 9; } if(event.keyCode==13){ event.keyCode = 9; }")
            txtNome.Attributes.Add("onkeydown", "if(event.which==13){ event.which = 9; } if(event.keyCode==13){ event.keyCode = 9; }")
        End If
        If Session("ABA") Is Nothing Then
            Session("ABA") = 0
        End If
        NumABA = CInt(Session("ABA").ToString)
        TabContainer1.ActiveTabIndex = NumABA
        'TabContainer1.ActiveTab =  

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        If Not Me.IsPostBack Then
            Exit Sub
        End If

        Dim Existe_CPF As Boolean
        Dim Contador As Integer
        Dim BalCom As New BALCOMARCA(GetUsuarioExt)
        Dim Bal As New BALPERITO(GetUsuarioExt)
        Dim BalPerNur As New BalPerito_Comarca(GetUsuarioExt)
        Dim n As Integer = 0

        If txtCPF.Text = "" Then
            MsgErro("O Preenchimento do CPF é obrigatório")
            Exit Sub
        End If
        Existe_CPF = Bal.VerCPFPerito(txtCPF.Text)
        If Existe_CPF Then
            MsgErro("Gravação Rejeitada. CPF já cadastrado.")
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

        If txtCod_Perito.Text = "" Then
            ent.Cod_Perito = 0
        Else
            ent.Cod_Perito = CInt(txtCod_Perito.Text)
        End If
        ent.Nome = txtNome.Text
        ent.Num_Reg = txtNum_Reg.Text
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
        If Not IsNumeric(CboTip_Tel.Text) Then
            ent.Cod_Tip_Tel = 0
        Else
            ent.Cod_Tip_Tel = CInt(CboTip_Tel.Items.FindByValue(CboTip_Tel.Text).Value)
        End If
        If Not IsNumeric(CboTip_Tel1.Text) Then
            ent.Cod_Tip_Tel1 = 0
        Else
            ent.Cod_Tip_Tel1 = CInt(CboTip_Tel1.Items.FindByValue(CboTip_Tel1.Text).Value)
        End If
        ent.DDD = txtDDD.Text
        ent.DDD1 = txtDDD1.Text
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
            ent.Sigla_UF1 = CboUF.Text 'CboUF1.Items.FindByValue(CboUF1.Text).Value
        End If
        ent.TEL = TxtTel.Text
        ent.TEL1 = txtTel1.Text
        ent.RAMAL = TxtRamal.Text
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
        If Not IsNumeric(CboEspecialidade1.SelectedValue) Then
            ent.COD_ESPECIALIDADE1 = 0
        Else
            ent.COD_ESPECIALIDADE1 = CInt(CboEspecialidade1.SelectedValue)
        End If
        If Not IsNumeric(CboProfissao1.SelectedValue) Then
            ent.COD_PROFISSAO1 = 0
        Else
            ent.COD_PROFISSAO1 = CInt(CboProfissao1.SelectedValue)
        End If
        'ent.SIGLA = GetUsuarioExt.Login
        'If ent.SIGLA = "" Then
        ent.SIGLA = "INTERNET"
        'End If
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
        If txtID_PF.Text = "" Then
            ent.ID_PF = 0
        Else
            ent.ID_PF = CInt(txtID_PF.Text)
        End If
        If txtDt_Nasc.Text = "" Then
            ent.Dt_Nasc = Nothing
        Else
            ent.Dt_Nasc = CDate(txtDt_Nasc.Text)
        End If
        'ent.Cod_Tip_Sit = If(RdAtivo.Items(0).Selected, 1, 2)
        'Situacao => 1 - Ativo, 2 - Inativo e 19 - Excluído na tabela PessoaFisicaFuncao -> Cod_Tip_Sit (numérico)
        Try
            Dim ExisteCPFPerito As Boolean
            If txtID_PF.Text = "" And txtCPF.Text <> "" Then
                ExisteCPFPerito = Bal.VerCPFPerito(txtCPF.Text)
            Else
                ExisteCPFPerito = False
            End If
            If Not ExisteCPFPerito Then
                Bal.GravarExt(ent)
            Else
                MsgErro("Inserção rejeitada. Existe Perito Cadastrado com este CPF")
                Exit Sub
            End If
            MsgErro("Prazo para entrega da documentação necessária : 30 dias. Vide Documentos Necessários neste site.")

        Catch
            MsgErro("Gravação Rejeitada")
        Finally
            MsgErro("Gravado com Sucesso")
        End Try
        If txtID_PF.Text = "" Then
            If Not ent.ID_PF = Nothing Then
                txtID_PF.Text = ent.ID_PF.ToString
            End If
        End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
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
        'End If
        BalPerNur.ExcluirPerNur(CInt(txtID_PF.Text))
        BalPerNur.GravarPerito_Comarca(DsNurTotal, CInt(txtID_PF.Text))
        'txtNum_Reg.AutoPostBack = True
        'txtNome.AutoPostBack = True
        'txtCPF.AutoPostBack = True
        'TabContainer1.ActiveTabIndex = 0
    End Sub


    Private Sub CboUF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboUF.SelectedIndexChanged
        If Me.IsPostBack Then
            PreencherCidade(CboUF.SelectedItem.Value)
            PreencherBairro(CboCidade.SelectedItem.Value)
        End If
    End Sub

    Private Sub CboCidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCidade.SelectedIndexChanged
        If Me.IsPostBack Then
            PreencherBairro(CboCidade.SelectedItem.Value)
        End If
    End Sub
    Private Sub CboUF1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboUF1.SelectedIndexChanged
        If Me.IsPostBack Then
            PreencherCidade1(CboUF1.SelectedItem.Value)
            PreencherBairro1(CboCidade1.SelectedItem.Value)
        End If
    End Sub

    Private Sub PreencherTip_Logr()
        Dim bal As New BalTipoLogradouro(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboTip_Logr.Items.Clear()
        CboTip_Logr.DataTextField = "Descr"
        CboTip_Logr.DataValueField = "Cod_tip_Logr"
        CboTip_Logr.DataSource = dsfila.Tables(0) '.DefaultView
        CboTip_Logr.DataBind()
        CboTip_Logr.SelectedIndex = 117

    End Sub
    Private Sub PreencherTip_Logr1()
        Dim bal As New BalTipoLogradouro(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboTip_Logr1.Items.Clear()
        CboTip_Logr1.DataTextField = "Descr"
        CboTip_Logr1.DataValueField = "Cod_tip_Logr"
        CboTip_Logr1.DataSource = dsfila.Tables(0) '.DefaultView
        CboTip_Logr1.DataBind()
        CboTip_Logr1.SelectedIndex = 117

    End Sub
    'Orgao_Per
    Private Sub PreencherOrgao_Per()
        Dim bal As New BalOrgao_Per(GetUsuarioExt)
        'Dim Parametro As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        'Parametro.Valor = ""
        'Dim bal As New BalOrgao_Per(Parametro)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboOrgao_Per.Items.Clear()
        CboOrgao_Per.DataTextField = "Descr_Orgao_Per"
        CboOrgao_Per.DataValueField = "Cod_Orgao_Per"
        CboOrgao_Per.DataSource = dsfila.Tables(0)
        CboOrgao_Per.DataBind()
        CboOrgao_Per.Items.Insert(0, "Selecione o Órgão")
        CboOrgao_Per.SelectedIndex = 0

    End Sub
    Private Sub PreencherUF()
        Dim bal As New BALCIDADE(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosUFSet()
        CboUF.Items.Clear()
        CboUF.Items.Insert(0, "Selecione o Estado")
        i = 0
        For Each rs As DataRow In dsfila.Tables(0).Rows
            If Not IsDBNull(rs("SIGLA_UF")) Then
                i = i + 1
                CboUF.Items.Insert(i, rs("SIGLA_UF").ToString)
            End If
        Next
        'CboUF.Items.Insert(0, "Selecione o Estado")
        CboUF.SelectedIndex = 21

    End Sub
    Private Sub PreencherUF1()
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
        'CboUF1.Items.Insert(0, "Selecione o Estado")
        CboUF1.SelectedIndex = 21

    End Sub

    Private Sub PreencherBairro(ByVal m_Cidade As String)
        If IsDBNull(m_Cidade) Or m_Cidade = "Selecione a Cidade" Then
            MsgErro("Selecione a Cidade")
            Exit Sub
        End If
        Dim nCod_Cid As Long
        Dim bal As New BALBairro(GetUsuarioExt)
        Dim dsfila As New DataSet
        'dsfila = bal.ExibirDadosSet(m_Cidade)
        'nCod_Cid = CInt(CboCidade.Items.FindByValue(CboCidade.Text).Value)
        nCod_Cid = Convert.ToInt64(CboCidade.Items.FindByValue(m_Cidade).Value)
        dsfila = bal.ExibirDadosSet(nCod_Cid)
        CboBairro.Items.Clear()
        CboBairro.DataTextField = "Nome"
        CboBairro.DataValueField = "Cod_Bai"
        CboBairro.DataSource = dsfila.Tables(0)
        CboBairro.DataBind()
        CboBairro.Items.Insert(0, "Selecione o Bairro")
        CboBairro.SelectedIndex = 0

    End Sub
    Private Sub PreencherBairro1(ByVal m_Cidade As String)
        If IsDBNull(m_Cidade) Or m_Cidade = "Selecione a Cidade" Then
            MsgErro("Selecione a Cidade")
            Exit Sub
        End If
        Dim nCod_Cid As Long
        Dim bal As New BALBairro(GetUsuarioExt)
        Dim dsfila As New DataSet
        'dsfila = bal.ExibirDadosSet(m_Cidade)
        'nCod_Cid = CInt(CboCidade.Items.FindByValue(CboCidade.Text).Value)
        nCod_Cid = Convert.ToInt64(CboCidade.Items.FindByValue(m_Cidade).Value)
        dsfila = bal.ExibirDadosSet(nCod_Cid)
        'dsfila = bal.ExibirDadosSet(m_Cidade)
        CboBairro1.Items.Clear()
        CboBairro1.DataTextField = "Nome"
        CboBairro1.DataValueField = "Cod_Bai"
        CboBairro1.DataSource = dsfila.Tables(0)
        CboBairro1.DataBind()
        CboBairro1.Items.Insert(0, "Selecione o Bairro")
        CboBairro1.SelectedIndex = 0
    End Sub
    Private Sub PreencherCidade1(ByVal m_UF As String)
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
        CboCidade1.Items.Insert(0, "Selecione a Cidade")
        If m_UF = "RJ" Then
            CboCidade1.SelectedIndex = 230
        End If

    End Sub
    Private Sub PreencherCidade(ByVal m_UF As String)
        If IsDBNull(m_UF) Or m_UF = "Selecione o Estado" Then
            MsgErro("Selecione o Estado - UF")
            Exit Sub
        End If
        Dim bal As New BALCIDADE(GetUsuarioExt)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_UF)
        'dsfila = bal.ExibirDadosUFSet(m_UF)
        CboCidade.Items.Clear()
        CboCidade.DataTextField = "Nome"
        CboCidade.DataValueField = "Cod_Cid"
        CboCidade.DataSource = dsfila.Tables(0).DefaultView
        CboCidade.DataBind()
        CboCidade.Items.Insert(0, "Selecione a Cidade")
        If m_UF = "RJ" Then
            CboCidade.SelectedIndex = 230
        End If

    End Sub

    Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

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
        CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.SelectedIndex = 0

    End Sub
    Private Sub PreencherEspecialidade1(ByVal m_Cod_Profissao1 As Integer)

        Dim bal As New BALEspecialidade(GetUsuarioExt)
        Dim ent As New EntEspecialidade
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_Cod_Profissao1)
        If dsfila.Tables(0).Rows.Count > 0 Then
            CboEspecialidade1.Items.Clear()
            CboEspecialidade1.DataTextField = "Descr_Especialidade"
            CboEspecialidade1.DataValueField = "Cod_Especialidade"
            CboEspecialidade1.DataSource = dsfila.Tables(0) '.DefaultView
            CboEspecialidade1.DataBind()
        End If
        CboEspecialidade1.Items.Insert(0, "GENÉRICO")
        CboEspecialidade1.SelectedIndex = 0

    End Sub
    Private Sub PreencherPROFISSAO()
        Dim bal As New BALProfissao(GetUsuarioExt)
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
    Private Sub PreencherPROFISSAO1()
        Dim bal As New BALProfissao(GetUsuarioExt)
        Dim ent As New EntProfissao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboProfissao1.Items.Clear()
        CboProfissao1.DataTextField = "Descr_PROFISSAO"
        CboProfissao1.DataValueField = "Cod_PROFISSAO"
        CboProfissao1.DataSource = dsfila.Tables(0) '.DefaultView
        CboProfissao1.DataBind()
        CboProfissao1.Items.Insert(0, "Selecione a PROFISSAO")
        CboProfissao1.SelectedIndex = 0

        CboEspecialidade1.Items.Clear()

    End Sub
    Private Sub Limpar()
        LimparSemNome()
        'lblExcluido.Text = ""
        txtNome.Text = ""
        txtCPF.Text = ""
        txtDt_Nasc.Text = ""
        txtID_PF.Text = ""
        txtCod_Perito.Text = ""
        txtNum_Reg.Text = ""
        CboOrgao_Per.SelectedIndex = 0
        '''''''''''''''''''''''''''''
        'PreencherOrgao_Per()
        'PreencherTip_Logr()
        'PreencherUF()
        'PreencherCidade("RJ")
        'PreencherBairro("1")
        'PreencherTip_Logr1()
        'PreencherUF1()
        'PreencherCidade1("RJ")
        'PreencherBairro1("1")
        'PreencherPROFISSAO()
        'PreencherPROFISSAO1()
        '''''''''''''''''''''''''''''
        DsNurTotal.Clear()
        DsNur1.Clear()

        PreencherNurs()

        Session("DsNur1") = DsNur1
        DsNur2.Clear()
        Session("DsNur2") = DsNur2
        DsNur3.Clear()
        Session("DsNur3") = DsNur3
        DsNur4.Clear()
        Session("DsNur4") = DsNur4
        DsNur5.Clear()
        Session("DsNur5") = DsNur5
        DsNur6.Clear()
        Session("DsNur6") = DsNur6
        DsNur7.Clear()
        Session("DsNur7") = DsNur7
        DsNur8.Clear()
        Session("DsNur8") = DsNur8
        DsNur9.Clear()
        Session("DsNur9") = DsNur9
        DsNur10.Clear()
        Session("DsNur10") = DsNur10
        DsNur11.Clear()
        Session("DsNur11") = DsNur11
        DsNur12.Clear()
        Session("DsNur12") = DsNur12
        DsNur13.Clear()
        Session("DsNur13") = DsNur13
        ''''''''''''''''''''''''''''''
    End Sub

    Private Sub LimparSemNome()


        Session("Num_Nur") = 0
        txtCod_Perito.Text = ""
        txtID_PF.Text = ""
        CboTip_Tel.SelectedValue = "1"
        CboTip_Tel1.SelectedValue = "2"
        '--------------
        'PreencherOrgao_Per()
        '--------------
        CboOrgao_Per.SelectedIndex = 0
        '--------------
        'PreencherBairro("RJ")
        '--------------
        CboBairro.SelectedIndex = 0
        '--------------
        'PreencherBairro1("RJ")
        '--------------
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
        txtDDD1.Text = ""
        TxtTel.Text = ""
        txtTel1.Text = ""
        TxtRamal.Text = ""
        txtDt_Nasc.Text = ""
        PreencherTip_Logr()
        'PreencherUF()
        CboUF.SelectedIndex = 21
        'PreencherCidade("RJ")
        'PreencherBairro("1")
        PreencherTip_Logr1()
        'PreencherUF1()
        CboUF1.SelectedIndex = 21
        'PreencherCidade1("RJ")
        'PreencherBairro1("1")
        PreencherPROFISSAO()
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataTextField = "Descr_Especialidade"
        CboEspecialidade.DataValueField = "Cod_Especialidade"
        CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.SelectedIndex = 0
        'CboEspecialidade.Text = "GENÉRICO"
        CboProfissao.SelectedIndex = 0

        CboEspecialidade1.Items.Clear()
        CboEspecialidade1.DataTextField = "Descr_Especialidade"
        CboEspecialidade1.DataValueField = "Cod_Especialidade"
        CboEspecialidade1.Items.Insert(0, "GENÉRICO")
        CboEspecialidade1.SelectedIndex = 0
        'CboEspecialidade1.Text = "GENÉRICO"
        CboProfissao1.SelectedIndex = 0
        'PreencherBanco()

        txtEmail.Text = ent.EMAIL
        'ValidarCPF.Text = ""
        ValidarEmail.Text = ""
        ValidarEmail1.Text = ""
        ValidarNome.Text = ""
        DsNurTotal.Clear()
        'txtNum_Reg.AutoPostBack = True
        'txtNome.AutoPostBack = True
        'txtCPF.AutoPostBack = True

    End Sub
    'Private Sub PreencherBanco()
    '    Dim bal As New BalBanco(GetUsuarioExt)
    '    Dim ent As New EntBANCO
    '    Dim dsfila As New DataSet
    '    dsfila = bal.ExibirDadosSet()
    '    CboBanco.Items.Clear()
    '    CboBanco.DataTextField = "Nome"
    '    CboBanco.DataValueField = "Cod_Banco"
    '    CboBanco.DataSource = dsfila.Tables(0) '.DefaultView
    '    CboBanco.DataBind()
    '    CboBanco.Items.Insert(0, "Selecione o Banco")
    '    CboBanco.SelectedIndex = 0

    'End Sub
    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Limpar()
    End Sub

    Protected Sub txtDDD_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtDDD.TextChanged
        txtDDD1.Text = txtDDD.Text
    End Sub

    Protected Sub CboCidade1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboCidade1.SelectedIndexChanged
        If Me.IsPostBack Then
            PreencherBairro1(CboCidade1.SelectedItem.Value)
        End If
    End Sub

    Protected Sub BtnNur1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur1.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "1"
        Session("DsNur") = Session("DsNur1")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub

    Protected Sub BtnNur2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur2.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "2"
        Session("DsNur") = Session("DsNur2")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur3.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "3"
        Session("DsNur") = Session("DsNur3")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub

    Protected Sub BtnNur4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur4.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "4"
        Session("DsNur") = Session("DsNur4")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur5.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "5"
        Session("DsNur") = Session("DsNur5")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur6.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "6"
        Session("DsNur") = Session("DsNur6")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur7.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "7"
        Session("DsNur") = Session("DsNur7")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur8.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "8"
        Session("DsNur") = Session("DsNur8")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur9.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "9"
        Session("DsNur") = Session("DsNur9")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)
    End Sub
    Protected Sub BtnNur10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur10.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "10"
        Session("DsNur") = Session("DsNur10")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur11.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Session("ID") = txtID_PF.Text
        Session("Num_Nur") = "11"
        Session("DsNur") = Session("DsNur11")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur12.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "12"
        Session("DsNur") = Session("DsNur12")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Protected Sub BtnNur13_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNur13.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Session("Num_Nur") = "13"
        Session("DsNur") = Session("DsNur13")
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=550,width=500, Top=50,left=120')", True)

    End Sub
    Public Sub PreencherNurs()
        'If Not Me.IsPostBack Then
        '    Exit Sub
        'End If
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

        Dim BalComarca As New BALCOMARCA(GetUsuarioExt)

        'If txtID_PF.Text <> "" Then
        'DsNur1
        DsNur1 = BalComarca.ExibirDadosNurExt(1)
        Session("DsNur1") = CType(DsNur1, DataSet)
        'DsNur2
        DsNur2 = BalComarca.ExibirDadosNurExt(2)
        Session("DsNur2") = CType(DsNur2, DataSet)
        'DsNur3
        DsNur3 = BalComarca.ExibirDadosNurExt(3)
        Session("DsNur3") = CType(DsNur3, DataSet)
        'DsNur4
        DsNur4 = BalComarca.ExibirDadosNurExt(4)
        Session("DsNur4") = CType(DsNur4, DataSet)
        'DsNur5
        DsNur5 = BalComarca.ExibirDadosNurExt(5)
        Session("DsNur5") = CType(DsNur5, DataSet)
        'DsNur6
        DsNur6 = BalComarca.ExibirDadosNurExt(6)
        Session("DsNur6") = CType(DsNur6, DataSet)
        'DsNur7
        DsNur7 = BalComarca.ExibirDadosNurExt(7)
        Session("DsNur7") = CType(DsNur7, DataSet)
        'DsNur8
        DsNur8 = BalComarca.ExibirDadosNurExt(8)
        Session("DsNur8") = CType(DsNur8, DataSet)
        'DsNur9
        DsNur9 = BalComarca.ExibirDadosNurExt(9)
        Session("DsNur9") = CType(DsNur9, DataSet)
        'DsNur10
        DsNur10 = BalComarca.ExibirDadosNurExt(10)
        Session("DsNur10") = CType(DsNur10, DataSet)
        'DsNur11
        DsNur11 = BalComarca.ExibirDadosNurExt(11)
        Session("DsNur11") = CType(DsNur11, DataSet)
        'DsNur12
        DsNur12 = BalComarca.ExibirDadosNurExt(12)
        Session("DsNur12") = CType(DsNur12, DataSet)
        'DsNur13
        DsNur13 = BalComarca.ExibirDadosNurExt(13)
        Session("DsNur13") = CType(DsNur13, DataSet)
        'End If

    End Sub

    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCPF.TextChanged
        If txtCPF.Text <> "" Then
            Dim m_CPF As String
            m_CPF = txtCPF.Text
            If Not ValidarCPFServ(txtCPF.Text) And txtCPF.Text <> "" Then
                MsgErro("CPF Inválido")
                Exit Sub
            End If
            'If Not ValidarCPFServ(m_CPF) Then
            'MsgErro("CPF Inválido")
            'End If
            If txtNome.Text = "" And txtNum_Reg.Text = "" Then
                LimparSemNome()
                txtDt_Nasc.Text = ""
                txtID_PF.Text = ""
                txtCod_Perito.Text = ""
                'CboPerito.Items.Clear()
                'CboOrgao_Per.SelectedIndex = 0
                txtCPF.Text = m_CPF
                'If txtCPF.Text <> "" And txtNome.Text = "" And txtNum_Reg.Text = "" Then
                'PreencherDadosPerito()
                'End If
            End If
            TabContainer1.ActiveTabIndex = 0
        End If
        TabContainer1.Visible = True

    End Sub

    'Protected Sub txtNum_Reg_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_Reg.TextChanged
    '    If txtNum_Reg.Text <> "" Then
    '        Dim m_Reg As String
    '        m_Reg = txtNum_Reg.Text
    '        If txtCPF.Text = "" And txtNome.Text = "" Then
    '            LimparSemNome()
    '            txtDt_Nasc.Text = ""
    '            txtID_PF.Text = ""
    '            txtCod_Perito.Text = ""
    '            'CboPerito.Items.Clear()
    '            txtNum_Reg.Text = m_Reg
    '            If txtNum_Reg.Text <> "" And txtNome.Text = "" And txtCPF.Text = "" Then
    '                'PreencherDadosPerito()
    '            End If
    '        End If
    '    End If
    'End Sub
    'Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome.TextChanged
    '    If txtNome.Text <> "" Then
    '        If txtCPF.Text = "" And txtNum_Reg.Text = "" Then
    '            LimparSemNome()
    '            txtDt_Nasc.Text = ""
    '            txtCod_Perito.Text = ""
    '            'CboPerito.Items.Clear()
    '            txtID_PF.Text = ""
    '            CboOrgao_Per.SelectedIndex = 0
    '            'PreencherSemelhantes(txtNome.Text)
    '        End If
    '    End If

    'End Sub
    'Protected Sub TxtCEP1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCEP.TextChanged

    '    Dim Bal As New BalCEP(GetUsuarioExt)
    '    Dim Ent As New EntCEP
    '    If Not Me.IsPostBack Or TxtCEP1.Text = "" Then
    '        Exit Sub
    '    End If
    '    Ent = Bal.ExibirDadosEnt(TxtCEP1.Text)
    '    If Not Ent Is Nothing Then
    '        CboBairro1.SelectedValue = CboBairro.Items.FindByValue(Ent.Cod_Bai.ToString).Value
    '        txtNome_Logr1.Text = Ent.Logradouro
    '        CboCidade1.SelectedValue = CboCidade.Items.FindByValue(Ent.Cod_Cid.ToString).Value
    '        CboUF1.SelectedValue = CboUF.Items.FindByValue(Ent.Sigla_UF.ToString).Value
    '    End If

    'End Sub

    'Protected Sub BtnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSair.Click
    '   Response.Redirect("frmMenuPerito.aspx")
    'End Sub
    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
        Session("ABA") = 3
    End Sub
    Protected Sub CboProfissao1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao1.SelectedIndexChanged
        PreencherEspecialidade1(CInt(CboProfissao1.Items.FindByValue(CboProfissao1.Text).Value))
        Session("ABA") = 3
    End Sub

    Protected Sub CboOrgao_Per_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboOrgao_Per.SelectedIndexChanged
        'ScriptManager1.SetFocus(txtNum_Reg)
    End Sub

    'Protected Sub BtnDadosBancarios_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnDadosBancarios.Click
    '    Dim BalPer As New BALPERITO(GetUsuarioExt)

    '    If txtID_PF.Text = "" Then 'perito sem dados gravados
    '        MsgErro("Gravação rejeitada! Grave os dados do perito antes de cadastrar os dados bancários")
    '        Exit Sub
    '    End If
    '    'OK = RdSit.Items(0).Selected
    '    If Not RdSit.Items(0).Selected Then
    '        MsgErro("Gravação rejeitada! O Perito possui pendências")
    '        Exit Sub
    '    End If
    '    'Ativo - RdAtivo.Items(0).Selected
    '    If Not RdAtivo.Items(0).Selected Then
    '        MsgErro("Gravação rejeitada! O Perito esta inativo")
    '        Exit Sub
    '    End If
    '    Dim m_Cod_Banco As String
    '    m_Cod_Banco = CboBanco.Items.FindByValue(CboBanco.Text).Value

    '    'BalPer.GravarContaCorrente(txtCPF.Text, m_Cod_Banco, TxtNum_Agencia.Text, txtNome_Agencia.Text, txtNum_Conta_Corrente.Text)
    'End Sub


    Protected Sub TxtCEP1_TextChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCEP1.TextChanged
        Dim Bal As New BalCEP(GetUsuarioExt)
        Dim Ent As New EntCEP
        If Not Me.IsPostBack Or TxtCEP1.Text = "" Then
            Exit Sub
        End If
        If Not IsNumeric(TxtCEP1.Text) Then
            MsgErro("O CEP somente deverá possuir valores numéricos")
            TxtCEP1.Text = ""
            Exit Sub
        End If
        Try
            Ent = Bal.ExibirDadosEnt(txtCEP1.Text)
            If Not Ent Is Nothing Then
                txtNome_Logr1.Text = Ent.Logradouro
                CboBairro1.SelectedValue = CboBairro.Items.FindByValue(Ent.Cod_Bai.ToString).Value
                CboCidade1.SelectedValue = CboCidade.Items.FindByValue(Ent.Cod_Cid.ToString).Value
                CboUF1.SelectedValue = CboUF.Items.FindByValue(Ent.Sigla_UF.ToString).Value
            End If
        Catch
        End Try
        Session("ABA") = 2
        TabContainer1.ActiveTabIndex = 2
    End Sub

    Private Sub TabContainer1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.Load
        NumABA = CInt(Session("ABA").ToString)
        TabContainer1.ActiveTabIndex = NumABA
    End Sub

    Protected Sub txtNome_Logr1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome_Logr1.TextChanged

    End Sub

    Protected Sub txtCEP_TextChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles txtCEP.TextChanged
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
        Try
            Ent = Bal.ExibirDadosEnt(txtCEP.Text)
            If Not Ent Is Nothing Then
                txtNome_Logr.Text = Ent.Logradouro
                CboBairro.SelectedValue = CboBairro.Items.FindByValue(Ent.Cod_Bai.ToString).Value
                CboCidade.SelectedValue = CboCidade.Items.FindByValue(Ent.Cod_Cid.ToString).Value
                CboUF.SelectedValue = CboUF.Items.FindByValue(Ent.Sigla_UF.ToString).Value
            End If
        Catch
        End Try

    End Sub

End Class
