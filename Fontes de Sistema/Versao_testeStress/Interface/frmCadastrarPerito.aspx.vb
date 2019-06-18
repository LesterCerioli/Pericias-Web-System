Option Strict Off

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET

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
   
    Private ent As New EntPERITO
    Dim i, j As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then

            Try
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
                txtCPF.Attributes.Add("onblur", "validacpf('" & txtCPF.ClientID & "');")
                txtDt_Nasc.Attributes.Add("onblur", "validardata('" & txtDt_Nasc.ClientID & "');")
                BtnExcluir.Attributes.Add("OnClick", "return confirm('Confirma a Exclusão?');")
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
                PreencherBanco()
            Catch ex As Exception
                MsgErro(ex.Message)
            End Try
            '[enter] virando [tab]
            txtCod_Perito.Attributes.Add("onkeydown", "if(evEnt.which==13){ evEnt.which = 9; } if(evEnt.keyCode==13){ evEnt.keyCode = 9; }")
            txtData_Cadastramento.Data = Now.ToShortDateString

        End If

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Dim MsgG As String

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If lblExcluido.Text = "EXCLUÍDO" Then
            MsgErro("Perito Excluído. Gravação rejeitada")
            Exit Sub
        End If
        Dim Contador As Integer
        Dim BalCom As New BALCOMARCA(GetUsuario)
        Dim NumComarcas As Integer
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALPERITO(GetUsuario)
        Dim BalPerNur As New BalPerito_Comarca(GetUsuario)
        Dim n As Integer = 0
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
        Else
            ent.Cod_Tip_Logr1 = CInt(CboTip_Logr1.Items.FindByValue(CboTip_Logr1.Text).Value)
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
        'MsgErro("Marco 1 - DDD - " + Ent.DDD)
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
        'MsgErro("Marco 1,5 - CÈP - " + Ent.CEP)
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
        'MsgErro("Marco 1,75 - Sigla_UFF - " + Ent.Sigla_UF)
        ent.TEL = TxtTel.Text
        ent.TEL1 = txtTel1.Text
        ent.RAMAL = TxtRamal.Text
        'Teste
        ent.Cod_Tip_Sit = CInt(IIf(RdAtivo.Items(0).Selected, 1, 2))
        ent.OBS = "" 'txtObs.Text
        ent.EMAIL = txtEmail.Text '"SANTO@TJ.RJ.GOV.BR"
        ent.EMAIL1 = txtEmail1.Text
        ent.FALTA_ENTREGAR = txtFalta_Entregar.Text
        'MsgErro("Marco 1,9 - Email - " + Ent.EMAIL)
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
        ent.SIGLA = GetUsuario.Login
        'MsgErro("Marco 2 - Sigla - " + Ent.SIGLA)
        'Ent.SIGLA = "SANTO"
        ent.Data_Cadastramento = CDate(txtData_Cadastramento.Data) 'txtData_Cadastramento.Data1)
        'Ent.Indicacao = CStr(IIf(OptDIPEJ.Checked, "D", "J"))
        ent.Indicacao = CStr(IIf(RdIndic.Items(0).Selected, "D", "J"))
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
        'Ent.SITUACAO_CADASTRO = CStr(IIf(OptOK.Checked, "O", "P"))
        ent.SITUACAO_CADASTRO = CStr(IIf(RdSit.Items(0).Selected, "O", "P"))
        If Not IsNumeric(CboBanco.Text) Then
            ent.COD_BANCO = ""
        Else
            ent.COD_BANCO = CboBanco.Items.FindByValue(CboBanco.Text).Value
        End If
        ent.NUM_AGENCIA = TxtNum_Agencia.Text
        ent.NOME_AGENCIA = txtNome_Agencia.Text
        ent.NUM_CONTA_CORRENTE = txtNum_Conta_Corrente.Text
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
        
        ent.Cod_Tip_Sit = If(RdAtivo.Items(0).Selected, 1, 2)
        'Situacao => 1 - Ativo, 2 - Inativo e 19 - Excluído na tabela PessoaFisicaFuncao -> Cod_Tip_Sit (numérico)
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
        'MsgErro("Marco 3 - antes da gravação")
        Try
            Dim ExisteCPFPerito As Boolean
            If txtID_PF.Text = "" And txtCPF.Text <> "" Then
                ExisteCPFPerito = Bal.VerCPFPerito(txtCPF.Text)
            Else
                ExisteCPFPerito = False
            End If
            If Not ExisteCPFPerito Then
                Bal.Gravar(ent, InserirConta)
                txtCod_Perito.Text = ent.Cod_Perito.ToString
            Else
                MsgErro("Inserção rejeitada. Existe Perito Cadastrado com este CPF")
                Exit Sub
            End If
            MsgErro("Para acesso do perito ao sistema pela internet é necessário o Cadastro Presencial ")
            'Colocar retorno nas gravações para mensagens.
            'OK = RdSit.Items(0).Selected?
            'Ativo - RdAtivo.Items(0).Selected?
            If RdSit.Items(0).Selected And RdAtivo.Items(0).Selected Then
                MsgG = Bal.GravarFornecedor(ent)
                'If MsgG <> "" Then
                If Not MsgG.ToString = "null" Then
                    MsgErro("Gravação do Fornecedor Rejeitada." + MsgG)
                End If
            End If
        Catch
            MsgErro("Gravação do Perito Rejeitada")
        Finally
            MsgErro("Gravado do Perito com Sucesso")
        End Try
        If txtID_PF.Text = "" Then
            If Not ent.ID_PF = Nothing Then
                txtID_PF.Text = ent.ID_PF.ToString
            End If
        End If
        Contador = 0
        DsNurTotal = BalCom.ExibirDadosSet()
        DsNur1 = CType(Session("DsNur1"), DataSet)
        Dim Nur As Boolean
        Nur = False
        If Not Session("DsNur1") Is Nothing Then
            NumComarcas = DsNur1.Tables(0).Rows.Count
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
        DsNur2 = CType(Session("DsNur2"), DataSet)
        If Not Session("DsNur2") Is Nothing Then
            Nur = False
            NumComarcas = DsNur2.Tables(0).Rows.Count
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
        DsNur3 = CType(Session("DsNur3"), DataSet)
        If Not Session("DsNur3") Is Nothing Then
            Nur = False
            NumComarcas = DsNur3.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If CInt(DsNur3.Tables(0).Rows(ii).Item(0)) = 1 Then
                    '''''''''''''''''''''''''''
                    DsNurTotal.Tables(0).Rows(Contador).Item(0) = 1
                    DsNurTotal.Tables(0).Rows(Contador).Item(1) = (DsNur3.Tables(0).Rows(ii).Item(1))
                    DsNurTotal.Tables(0).Rows(Contador).Item(2) = (DsNur3.Tables(0).Rows(ii).Item(2))
                    DsNurTotal.Tables(0).Rows(Contador).Item(3) = 3
                    Contador = Contador + 1
                    ''''''''''''''''''''''''''''''
                    BtnNur3.BackColor = Drawing.Color.Black
                    BtnNur3.ForeColor = Drawing.Color.Beige
                    Nur = True
                End If
            Next
            If DsNur3 Is Nothing Then
                DsNur3 = BalCom.ExibirDadosSet(3)
                For Each rs As DataRow In DsNur3.Tables(0).Rows
                    DsNurTotal.Tables(0).Rows(Contador).Item(0) = rs("Marcado")
                    If rs("Marcado").ToString = "1" Then
                        BtnNur3.BackColor = Drawing.Color.Black
                        BtnNur3.ForeColor = Drawing.Color.Beige
                    End If
                    DsNurTotal.Tables(0).Rows(Contador).Item(1) = rs("Nome")
                    DsNurTotal.Tables(0).Rows(Contador).Item(2) = rs("Cod_Comarca")
                    DsNurTotal.Tables(0).Rows(Contador).Item(3) = 3
                    Contador = Contador + 1
                Next
            Else
                BtnNur3.BackColor = Drawing.Color.Black
                BtnNur3.ForeColor = Drawing.Color.Beige
            End If
        End If
        DsNur4 = CType(Session("DsNur4"), DataSet)
        If Not Session("DsNur4") Is Nothing Then
            Nur = False
            NumComarcas = DsNur4.Tables(0).Rows.Count
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
        DsNur5 = CType(Session("DsNur5"), DataSet)
        If Not Session("DsNur5") Is Nothing Then
            Nur = False
            NumComarcas = DsNur5.Tables(0).Rows.Count
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
        DsNur6 = CType(Session("DsNur6"), DataSet)
        If Not Session("DsNur6") Is Nothing Then
            Nur = False
            NumComarcas = DsNur6.Tables(0).Rows.Count
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
        DsNur7 = CType(Session("DsNur7"), DataSet)
        If Not Session("DsNur7") Is Nothing Then
            Nur = False
            NumComarcas = DsNur7.Tables(0).Rows.Count
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
        DsNur8 = CType(Session("DsNur8"), DataSet)
        If Not Session("DsNur8") Is Nothing Then
            Nur = False
            NumComarcas = DsNur8.Tables(0).Rows.Count
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
        DsNur9 = CType(Session("DsNur9"), DataSet)
        If Not Session("DsNur9") Is Nothing Then
            Nur = False
            NumComarcas = DsNur9.Tables(0).Rows.Count
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
        DsNur10 = CType(Session("DsNur10"), DataSet)
        If Not Session("DsNur10") Is Nothing Then
            Nur = False
            NumComarcas = DsNur10.Tables(0).Rows.Count
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
        DsNur11 = CType(Session("DsNur11"), DataSet)
        If Not Session("DsNur11") Is Nothing Then
            Nur = False
            NumComarcas = DsNur11.Tables(0).Rows.Count
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
        DsNur12 = CType(Session("DsNur12"), DataSet)
        If Not Session("DsNur12") Is Nothing Then
            Nur = False
            NumComarcas = DsNur12.Tables(0).Rows.Count
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
        DsNur13 = CType(Session("DsNur13"), DataSet)
        If Not Session("DsNur13") Is Nothing Then
            Nur = False
            NumComarcas = DsNur13.Tables(0).Rows.Count
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
        BalPerNur.ExcluirPerNur(CInt(txtID_PF.Text))
        BalPerNur.GravarPerito_Comarca(DsNurTotal, CInt(txtID_PF.Text))
        txtNum_Reg.AutoPostBack = True
        txtNome.AutoPostBack = True
        txtCPF.AutoPostBack = True
    End Sub

    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
    
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALPERITO(GetUsuario)
        If txtID_PF.Text = "" Then
            MsgErro("Exclusão não efetuada. Verifique o código do perito")
            Exit Sub
        Else
            MsgErro("Cadastre a Anotação do tipo exclusão, com a sua descrição")
            Response.Redirect("frmAnotacoes.aspx?CPF=" & txtCPF.Text)
        End If
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
        Dim bal As New BalTipoLogradouro(GetUsuario)
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
        Dim bal As New BalTipoLogradouro(GetUsuario)
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
        Dim bal As New BalOrgao_Per(GetUsuario)
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
        Dim bal As New BALCIDADE(GetUsuario)
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
        CboUF.SelectedIndex = 21

    End Sub
    Private Sub PreencherUF1()
        Dim bal As New BALCIDADE(GetUsuario)
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
        If IsDBNull(m_Cidade) Or m_Cidade = "Selecione a Cidade" Then
            MsgErro("Selecione a Cidade")
            Exit Sub
        End If
        Dim nCod_Cid As Long
        Dim bal As New BALBairro(GetUsuario)
        Dim dsfila As New DataSet
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
        Dim bal As New BALBairro(GetUsuario)
        Dim dsfila As New DataSet
        nCod_Cid = Convert.ToInt64(CboCidade.Items.FindByValue(m_Cidade).Value)
        dsfila = bal.ExibirDadosSet(nCod_Cid)
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
            CboCidade1.SelectedIndex = 230
        End If

    End Sub
    Private Sub PreencherCidade(ByVal m_UF As String)
        If IsDBNull(m_UF) Or m_UF = "Selecione o Estado" Then
            MsgErro("Selecione o Estado - UF")
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
            CboCidade.SelectedIndex = 230
        End If
    End Sub
    Private Sub PreencherSemelhantes(ByVal m_Nome As String)
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        CboPerito.Enabled = True
        Dim bal As New BALPERITO(GetUsuario)
        Dim dsfila As New DataSet
        If m_Nome = "" Then Exit Sub
        CboPerito.Items.Clear()
        dsfila = bal.ExibirDadosSet("NOME", m_Nome, "N")
        CboPerito.DataTextField = "Nome"
        CboPerito.DataValueField = "Cod_Perito"
        CboPerito.DataSource = dsfila.Tables(0) '.DefaultView
        CboPerito.DataBind()
        CboPerito.Items.Insert(0, "Opcional -> Clique aqui para escolher nomes semelhantes")
        CboPerito.SelectedIndex = 0

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
        CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.SelectedIndex = 0
    End Sub
    Private Sub PreencherEspecialidade1(ByVal m_Cod_Profissao1 As Integer)

        Dim bal As New BALEspecialidade(GetUsuario)
        'Dim ent As New EntEspecialidade
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
        Dim bal As New BALProfissao(GetUsuario)
        'Dim ent As New EntProfissao
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
        Dim bal As New BALProfissao(GetUsuario)
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

    Private Sub PreencherDadosPerito()
        Dim m_Cod_Orgao_Per As Integer
        Dim EntPer As New EntPERITO
        Dim DsConta As New DataSet
        Dim RsConta As DataRow

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If CboPerito.SelectedValue = "Opcional -> Clique aqui para escolher nomes semelhantes" And txtCPF.Text = "" Then
            Exit Sub
        End If
        txtNum_Reg.AutoPostBack = False
        txtNome.AutoPostBack = False
        txtCPF.AutoPostBack = False
        Dim Bal As New BALPERITO(GetUsuario)
        Dim BalPerNur As New BalPerito_Comarca(GetUsuario)

        If txtNome.Text <> "" And txtCPF.Text = "" And txtNum_Reg.Text = "" Then
            EntPer = Bal.ExibirDadosEnt("NOMEEXATO", txtNome.Text, "N")
        ElseIf txtCPF.Text <> "" And txtNome.Text = "" And txtNum_Reg.Text = "" Then
            EntPer = Bal.ExibirDadosEnt("CPF", txtCPF.Text, "N")
        ElseIf txtNum_Reg.Text <> "" And txtCPF.Text = "" And txtNome.Text = "" Then
            EntPer = Bal.ExibirDadosEnt("NUMREG", txtNum_Reg.Text, "N", CInt(CboOrgao_Per.Items.FindByValue(CboOrgao_Per.Text).Value))
        Else
            Exit Sub
        End If
        If Not EntPer Is Nothing Then
            '================================================
            If txtCPF.Text = "" And txtCod_Perito.Text = "" And txtNome.Text = "" Then
                If txtNum_Reg.Text = "" Or m_Cod_Orgao_Per = 0 Then
                    Exit Sub
                End If
            End If
            If Len(txtCPF.Text) <> 11 And txtCod_Perito.Text = "" And txtNome.Text = "" Then
                If txtNum_Reg.Text = "" Or m_Cod_Orgao_Per = 0 Then
                    Exit Sub
                End If
            End If

            m_Cod_Orgao_Per = EntPer.COD_ORGAO_PER
            DsConta = Bal.ExibirDadosConta(txtCPF.Text)
            If Not DsConta Is Nothing Then
                If DsConta.Tables(0).Rows.Count > 0 Then
                    RsConta = DsConta.Tables(0).Rows(0)
                    EntPer.COD_BANCO = RsConta("Cod_Banco").ToString
                    EntPer.NUM_AGENCIA = RsConta("Num_Agencia").ToString
                    EntPer.NOME_AGENCIA = RsConta("Nome_Agencia").ToString
                    EntPer.NUM_CONTA_CORRENTE = RsConta("Num_Conta_Corrente").ToString
                End If
            End If
            If EntPer.COD_BANCO = "" Then
                CboBanco.SelectedIndex = 0
            Else
                'PreencherBanco()
                If EntPer.COD_BANCO = "0" Then
                    CboBanco.SelectedValue = "Selecione o Banco" 
                Else
                    CboBanco.SelectedValue = CboBanco.Items.FindByValue(EntPer.COD_BANCO).Value
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
                InserirConta = True
            Else
                txtNum_Conta_Corrente.Enabled = False
                TxtNum_Agencia.Enabled = False
                txtNome_Agencia.Enabled = False
                CboBanco.Enabled = False
                BtnDadosBancarios.Visible = False
                InserirConta = False
            End If

            '==================================
            If EntPer.Cod_Perito.ToString = "0" Or EntPer.ID_PF.ToString = "0" Then Exit Sub
            txtID_PF.Text = EntPer.ID_PF.ToString
            PreencherNurs()
            txtCod_Perito.Text = EntPer.Cod_Perito.ToString
            txtNum_Reg.Text = EntPer.Num_Reg
            If EntPer.Dt_Nasc.ToString Is Nothing Then
                txtDt_Nasc.Text = ""
            Else
                txtDt_Nasc.Text = EntPer.Dt_Nasc.ToShortDateString
                If txtDt_Nasc.Text = "01/01/1900" Then
                    txtDt_Nasc.Text = ""
                End If
            End If
            txtCPF.Text = EntPer.CPF
            txtNome.Text = EntPer.Nome
            '1 -> Ativo
            '2 -> Inativo
            If EntPer.Cod_Tip_Sit = 1 Then
                RdAtivo.Items(0).Selected = True
                RdAtivo.Items(1).Selected = False
            Else
                RdAtivo.Items(0).Selected = False
                RdAtivo.Items(1).Selected = True
            End If

            If EntPer.SITUACAO_CADASTRO = "O" Then
                RdSit.Items(0).Selected = True
                RdSit.Items(1).Selected = False
            Else
                RdSit.Items(0).Selected = False
                RdSit.Items(1).Selected = True
            End If
            If EntPer.COD_PROFISSAO = 0 Then
                CboProfissao.SelectedIndex = 0
                CboEspecialidade.Items.Clear()
                CboEspecialidade.DataTextField = "Descr_Especialidade"
                CboEspecialidade.DataValueField = "Cod_Especialidade"
                CboEspecialidade.Items.Insert(0, "GENÉRICO")
                CboEspecialidade.SelectedIndex = 0
            Else
                CboProfissao.SelectedValue = CboProfissao.Items.FindByValue(EntPer.COD_PROFISSAO.ToString).Value
                PreencherEspecialidade(EntPer.COD_PROFISSAO)
            End If
            If EntPer.COD_PROFISSAO <> 0 Then
                If EntPer.COD_ESPECIALIDADE = 0 Then
                    CboEspecialidade.SelectedIndex = 0
                Else
                    CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByValue(EntPer.COD_ESPECIALIDADE.ToString).Value
                End If
            Else
                CboEspecialidade.SelectedIndex = 0
            End If
            '----------------
            If EntPer.COD_PROFISSAO1 = 0 Then
                CboProfissao1.SelectedIndex = 0
                CboEspecialidade1.Items.Clear()
                CboEspecialidade1.DataTextField = "Descr_Especialidade"
                CboEspecialidade1.DataValueField = "Cod_Especialidade"
                CboEspecialidade1.Items.Insert(0, "GENÉRICO")
                CboEspecialidade1.SelectedIndex = 0
            Else
                CboProfissao1.SelectedValue = CboProfissao1.Items.FindByValue(EntPer.COD_PROFISSAO1.ToString).Value
                PreencherEspecialidade1(EntPer.COD_PROFISSAO1)
            End If
            If EntPer.COD_PROFISSAO1 <> 0 Then
                If EntPer.COD_ESPECIALIDADE1 = 0 Then
                    CboEspecialidade1.SelectedIndex = 0
                Else
                    CboEspecialidade1.SelectedValue = CboEspecialidade1.Items.FindByValue(EntPer.COD_ESPECIALIDADE1.ToString).Value
                End If
            Else
                CboEspecialidade1.SelectedIndex = 0
            End If

            txtFalta_Entregar.Text = EntPer.FALTA_ENTREGAR
            If EntPer.Indicacao = "D" Then
                RdIndic.Items(0).Selected = True
                RdIndic.Items(1).Selected = False
            Else
                RdIndic.Items(0).Selected = False
                RdIndic.Items(1).Selected = True
            End If
            txtData_Cadastramento.Data = EntPer.Data_Cadastramento.ToShortDateString

            IIf(EntPer.DocNecCV = "1", chkDOCNECCV.Checked = True, chkDOCNECCV.Checked = False)
            IIf(EntPer.DocNecFoto = "1", chkDOCNECFOTO.Checked = True, chkDOCNECFOTO.Checked = False)
            IIf(EntPer.DocNecRes = "1", chkDOCNECRES.Checked = True, chkDOCNECRES.Checked = False)
            IIf(EntPer.DocNecImp = "1", chkDOCNECIMP.Checked = True, chkDOCNECIMP.Checked = False)
            IIf(EntPer.DocNecOrg = "1", chkDOCNECORG.Checked = True, chkDOCNECORG.Checked = False)
            IIf(EntPer.DocNecHab = "1", chkDOCNECHAB.Checked = True, chkDOCNECHAB.Checked = False)
            IIf(EntPer.DocNecCPF = "1", chkDOCNECCPF.Checked = True, chkDOCNECCPF.Checked = False)

            '''''''''''
            'ENDEREÇOS
          
            If EntPer.Cod_Tip_Logr = 0 Then
                CboTip_Logr.SelectedIndex = 117
            Else
                CboTip_Logr.SelectedValue = CboTip_Logr.Items.FindByValue(EntPer.Cod_Tip_Logr.ToString).Value
            End If
            If EntPer.Sigla_UF = "" Or EntPer.Sigla_UF = Nothing Then
                CboUF.SelectedIndex = 21
            Else
                CboUF.SelectedValue = CboUF.Items.FindByValue(EntPer.Sigla_UF.ToString).Value
                PreencherCidade(CboUF.SelectedItem.Value)
                If EntPer.Cod_Cidade = 0 Then
                    CboCidade.SelectedIndex = 230
                Else
                    CboCidade.SelectedValue = CboCidade.Items.FindByValue(EntPer.Cod_Cidade.ToString).Value
                    PreencherBairro(CboCidade.SelectedItem.Value)
                    If EntPer.Cod_Bairro = 0 Then
                        CboBairro.SelectedIndex = 0
                    Else
                        CboBairro.SelectedValue = CboBairro.Items.FindByValue(EntPer.Cod_Bairro.ToString).Value
                    End If
                End If
            End If
            txtNome_Logr.Text = EntPer.Nome_Logr
            txtCompl_Logr.Text = EntPer.Compl_Logr
            txtNum_Logr.Text = EntPer.Num_Logr
            TxtCEP.Text = EntPer.CEP
            ''''''''''''''''''''
            'ENDEREÇO COMERCIAL
            'Endereco Comercial
            If EntPer.Cod_Tip_Logr1 = 0 Then
                CboTip_Logr1.SelectedIndex = 117
            Else
                CboTip_Logr1.SelectedValue = CboTip_Logr1.Items.FindByValue(EntPer.Cod_Tip_Logr1.ToString).Value
            End If
            If EntPer.Sigla_UF1 = "" Or EntPer.Sigla_UF1 = Nothing Then
                CboUF1.SelectedIndex = 21
            Else
                CboUF1.SelectedValue = CboUF1.Items.FindByValue(EntPer.Sigla_UF1.ToString).Value
                PreencherCidade1(CboUF1.SelectedItem.Value)
                If EntPer.Cod_Cidade1 = 0 Then
                    CboCidade1.SelectedIndex = 0
                Else
                    CboCidade1.SelectedValue = CboCidade1.Items.FindByValue(EntPer.Cod_Cidade1.ToString).Value
                    PreencherBairro1(CboCidade1.SelectedItem.Value)
                    If EntPer.Cod_Bairro = 0 Then
                        CboBairro1.SelectedIndex = 0
                    Else
                        CboBairro1.SelectedValue = CboBairro1.Items.FindByValue(EntPer.Cod_Bairro1.ToString).Value
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
            '''''''''''''''''''
            'TELEFONES
            '''''''''''''''''''''
            txtDDD.Text = EntPer.DDD
            txtDDD1.Text = EntPer.DDD

            If EntPer.Cod_Tip_Tel <> 0 Then
                CboTip_Tel.SelectedValue = CboTip_Tel.Items.FindByValue(EntPer.Cod_Tip_Tel.ToString).Value
            Else
                CboTip_Tel.SelectedValue = "1"
            End If
            If EntPer.Cod_Tip_Tel1 <> 0 Then
                CboTip_Tel1.SelectedValue = CboTip_Tel1.Items.FindByValue(EntPer.Cod_Tip_Tel1.ToString).Value
            Else
                CboTip_Tel1.SelectedValue = "2"
            End If
            If Not EntPer.COD_ORGAO_PER = Nothing Then
                CboOrgao_Per.SelectedValue = CboOrgao_Per.Items.FindByValue(EntPer.COD_ORGAO_PER.ToString).Value
            End If
            TxtTel.Text = EntPer.TEL
            txtTel1.Text = EntPer.TEL1
            TxtRamal.Text = EntPer.RAMAL
        End If
        Dim ok As Boolean
        Dim BalAnot As New BALAnotacao(GetUsuario)
        If txtID_PF.Text <> "" Then
            ok = BalAnot.ExibirDadosExclusao(Convert.ToInt64(txtID_PF.Text))
            If ok Then
                lblExcluido.Text = "EXCLUÍDO"
            Else
                lblExcluido.Text = ""
            End If
        End If
        ''''''''''''''''''''''''''''
        'PeritoNur
        '''''''''''''''''''''''''''''
    End Sub

    Private Sub Limpar()
        LimparSemNome()
        lblExcluido.Text = ""
        txtNome.Text = ""
        txtCPF.Text = ""
        txtDt_Nasc.Text = ""
        txtID_PF.Text = ""
        txtCod_Perito.Text = ""
        txtNum_Reg.Text = ""
        CboPerito.Items.Clear()
        CboOrgao_Per.SelectedIndex = 0
        CboBanco.Enabled = True
        TxtNum_Agencia.Enabled = True
        txtNome_Agencia.Enabled = True
        txtNum_Conta_Corrente.Enabled = True
        DsNurTotal.Clear()
        DsNur1.Clear()
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
        'CboOrgao_Per.SelectedIndex = 0
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
        txtDt_Nasc.Text = "" 'Nothing
        RdAtivo.Items(0).Selected = True
        PreencherTip_Logr()
        CboUF.SelectedIndex = 21
        PreencherTip_Logr1()
        CboUF1.SelectedIndex = 21
        PreencherPROFISSAO()
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataTextField = "Descr_Especialidade"
        CboEspecialidade.DataValueField = "Cod_Especialidade"
        CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.SelectedIndex = 0
        CboProfissao.SelectedIndex = 0

        CboEspecialidade1.Items.Clear()
        CboEspecialidade1.DataTextField = "Descr_Especialidade"
        CboEspecialidade1.DataValueField = "Cod_Especialidade"
        CboEspecialidade1.Items.Insert(0, "GENÉRICO")
        CboEspecialidade1.SelectedIndex = 0
        CboProfissao1.SelectedIndex = 0
        PreencherBanco()
        txtEmail.Text = ent.EMAIL
        txtFalta_Entregar.Text = ent.FALTA_ENTREGAR
        RdIndic.Items(0).Selected = True 'DIPEJ
        txtData_Cadastramento.Data = ent.Data_Cadastramento.ToShortDateString
        TxtNum_Agencia.Text = ""
        txtNome_Agencia.Text = ""
        txtNum_Conta_Corrente.Text = ""
        RdSit.Items(1).Selected = True 'Pendente
        ValidarCPF.Text = ""
        ValidarEmail.Text = ""
        ValidarEmail1.Text = ""
        ValidarNome.Text = ""
        DsNurTotal.Clear()
        txtNum_Reg.AutoPostBack = True
        txtNome.AutoPostBack = True
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
        Dim bal As New BalBanco(GetUsuario)
        'Dim ent As New EntBANCO
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboBanco.Items.Clear()
        CboBanco.DataTextField = "Nome"
        CboBanco.DataValueField = "Cod_Banco"
        CboBanco.DataSource = dsfila.Tables(0) '.DefaultView
        CboBanco.DataBind()
        CboBanco.Items.Insert(0, "Selecione o Banco")
        CboBanco.SelectedIndex = 0

    End Sub
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
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=620,width=500, Top=50,left=120')", True)

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
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmComarcaPer.aspx', '_blank', 'height=650,width=500, Top=50,left=120')", True)
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
        Dim NumComarcas As Integer
        Dim ii As Integer

        Dim BalPer As New BalPerito_Comarca(GetUsuario)

        If txtID_PF.Text <> "" Then
            'DsNur1
            DsNur1 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 1)
            Session("DsNur1") = CType(DsNur1, DataSet)
            NumComarcas = DsNur1.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur1.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur1.BackColor = Drawing.Color.Black
                    BtnNur1.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur2
            DsNur2 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 2)
            Session("DsNur2") = CType(DsNur2, DataSet)
            NumComarcas = DsNur2.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur2.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur2.BackColor = Drawing.Color.Black
                    BtnNur2.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur3
            DsNur3 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 3)
            Session("DsNur3") = CType(DsNur3, DataSet)
            NumComarcas = DsNur3.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur3.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur3.BackColor = Drawing.Color.Black
                    BtnNur3.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur4
            DsNur4 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 4)
            Session("DsNur4") = CType(DsNur4, DataSet)
            NumComarcas = DsNur4.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur4.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur4.BackColor = Drawing.Color.Black
                    BtnNur4.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur5
            DsNur5 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 5)
            Session("DsNur5") = CType(DsNur5, DataSet)
            NumComarcas = DsNur5.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur5.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur5.BackColor = Drawing.Color.Black
                    BtnNur5.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur6
            DsNur6 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 6)
            Session("DsNur6") = CType(DsNur6, DataSet)
            NumComarcas = DsNur6.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur6.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur6.BackColor = Drawing.Color.Black
                    BtnNur6.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur7
            DsNur7 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 7)
            Session("DsNur7") = CType(DsNur7, DataSet)
            NumComarcas = DsNur7.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur7.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur7.BackColor = Drawing.Color.Black
                    BtnNur7.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur8
            DsNur8 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 8)
            Session("DsNur8") = CType(DsNur8, DataSet)
            NumComarcas = DsNur8.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur8.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur8.BackColor = Drawing.Color.Black
                    BtnNur8.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur9
            DsNur9 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 9)
            Session("DsNur9") = CType(DsNur9, DataSet)
            NumComarcas = DsNur9.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur9.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur9.BackColor = Drawing.Color.Black
                    BtnNur9.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur10
            DsNur10 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 10)
            Session("DsNur10") = CType(DsNur10, DataSet)
            NumComarcas = DsNur10.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur10.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur10.BackColor = Drawing.Color.Black
                    BtnNur10.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur11
            DsNur11 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 11)
            Session("DsNur11") = CType(DsNur11, DataSet)
            NumComarcas = DsNur11.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur11.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur11.BackColor = Drawing.Color.Black
                    BtnNur11.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur12
            DsNur12 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 12)
            Session("DsNur12") = CType(DsNur12, DataSet)
            NumComarcas = DsNur12.Tables(0).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur12.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur12.BackColor = Drawing.Color.Black
                    BtnNur12.ForeColor = Drawing.Color.Beige
                End If
            Next
            'DsNur13
            DsNur13 = BalPer.ExibirDadosNur(CInt(txtID_PF.Text), 13)
            Session("DsNur13") = CType(DsNur13, DataSet)
            NumComarcas = DsNur13.Tables(0I).Rows.Count
            For ii = 0 To NumComarcas - 1
                If DsNur13.Tables(0).Rows(ii).Item(0).ToString = "1" Then
                    BtnNur13.BackColor = Drawing.Color.Black
                    BtnNur13.ForeColor = Drawing.Color.Beige
                End If
            Next
        End If
    End Sub

    Protected Sub BtnNovoEndCom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovoEndCom.Click
        PreencherTip_Logr1()
        PreencherUF1()
        PreencherCidade1("RJ")
        PreencherBairro1("1")
        txtCompl_Logr1.Text = ""
        TxtCEP1.Text = ""
        txtNum_Logr1.Text = ""
        txtNome_Logr1.Text = ""
    End Sub

    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCPF.TextChanged
        If txtCPF.Text <> "" Then
            Dim m_CPF As String
            m_CPF = txtCPF.Text
            If Not ValidarCPFServ(txtCPF.Text) And txtCPF.Text <> "" Then
                MsgErro("CPF Inválido")
                Exit Sub
            End If
            If txtNome.Text = "" And txtNum_Reg.Text = "" Then
                LimparSemNome()
                txtDt_Nasc.Text = ""
                txtID_PF.Text = ""
                txtCod_Perito.Text = ""
                CboPerito.Items.Clear()
                CboOrgao_Per.SelectedIndex = 0
                txtCPF.Text = m_CPF
                If txtCPF.Text <> "" And txtNome.Text = "" And txtNum_Reg.Text = "" Then
                    PreencherDadosPerito()
                End If
                txtNome.AutoPostBack = False
                txtNum_Reg.AutoPostBack = False
            End If
        End If
        CboPerito.Enabled = False
    End Sub

    Protected Sub txtNum_Reg_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_Reg.TextChanged
        If txtNum_Reg.Text <> "" Then
            Dim m_Reg As String
            m_Reg = txtNum_Reg.Text
            If txtCPF.Text = "" And txtNome.Text = "" Then
                LimparSemNome()
                txtDt_Nasc.Text = ""
                txtID_PF.Text = ""
                txtCod_Perito.Text = ""
                CboPerito.Items.Clear()
                txtNum_Reg.Text = m_Reg
                If txtNum_Reg.Text <> "" And txtNome.Text = "" And txtCPF.Text = "" Then
                    PreencherDadosPerito()
                End If
                txtNome.AutoPostBack = False
            End If
        End If
    End Sub
    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome.TextChanged
        If txtNome.Text <> "" Then
            If txtCPF.Text = "" And txtNum_Reg.Text = "" Then
                LimparSemNome()
                txtDt_Nasc.Text = ""
                txtCod_Perito.Text = ""
                CboPerito.Items.Clear()
                txtID_PF.Text = ""
                CboOrgao_Per.SelectedIndex = 0
                PreencherSemelhantes(txtNome.Text)
                txtNum_Reg.AutoPostBack = False
            End If
        End If

    End Sub
    Private Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboPerito.SelectedIndexChanged
        Dim Homonimos As Boolean
        Dim BalHom As New BALPERITO(GetUsuario)
        If Me.IsPostBack Then
            txtNome.Text = CboPerito.SelectedItem.ToString
            LimparSemNome()
            If Trim(txtNome.Text) <> "" And txtCPF.Text = "" And txtNum_Reg.Text = "" Then
                Homonimos = BalHom.VerHomonimo(txtNome.Text)
                If Homonimos Then
                    txtNome.Text = ""
                    CboPerito.Items.Clear()
                    MsgErro("Existem Homônimos. Localize o perito pelo CPF")
                Else
                    PreencherDadosPerito()
                End If
            End If
            CboPerito.Enabled = False
        End If
    End Sub

    Protected Sub RdSit_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdSit.SelectedIndexChanged
        If RdSit.Items(0).Selected And (Not chkDOCNECCV.Checked Or _
            Not chkDOCNECFOTO.Checked Or _
            Not chkDOCNECRES.Checked Or _
            Not chkDOCNECIMP.Checked Or _
            Not chkDOCNECHAB.Checked Or _
            Not chkDOCNECORG.Checked Or _
            Not chkDOCNECCPF.Checked) Then
            RdSit.Items(1).Selected = True
            MsgErro("Existe Documentação Pendente")
        End If
    End Sub

    Protected Sub BtnGravaFoto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravaFoto.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If lblExcluido.Text = "EXCLUÍDO" Then
            MsgErro("Perito Excluído")
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            MsgErro("Antes de inserir a foto, Grave os dados do perito")
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmGravaFoto.aspx', '_blank', 'height=350,width=620, Top=150,left=120')", True)

    End Sub

    Protected Sub BtnExibirFoto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExibirFoto.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Session("ID") = txtID_PF.Text
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmExibirFoto.aspx', '_blank', 'height=401,width=301, Top=150,left=120')", True)
    End Sub

    Protected Sub BtnGravaCurriculum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravaCurriculum.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If lblExcluido.Text = "EXCLUÍDO" Then
            MsgErro("Perito Excluído")
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            MsgErro("Antes de inserir o Curriculum, Grave os dados do perito")
            Exit Sub
        End If
        Session("ID") = txtID_PF.Text
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmGravaCurriculum.aspx', '_blank', 'height=150,width=300, Top=150,left=120')", True)
    End Sub

    Protected Sub BtnExibirCurriculum_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExibirCurriculum.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If txtID_PF.Text = "" Then
            Session("ID") = 0
        Else
            Session("ID") = txtID_PF.Text
        End If

        Dim Obj As DGTECGEDAR
        Dim IDDoc As String
        Dim Bal As New BALPERITO(GetUsuario)
        Dim Ent As New EntPERITO
        Dim f_ID_PF As String
        Dim m_URL As String

        Try
            f_ID_PF = Session("ID").ToString
            If f_ID_PF = "" Then
                MsgErro("Identificador não encontrado")
                Exit Sub
            End If
            Ent = Bal.ExibirDadosEnt("ID", f_ID_PF, "N")
            IDDoc = Ent.IDGED_CV

            If IDDoc <> "" Then
                Obj = New DGTECGEDAR
                Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                m_URL = Obj.MontaURLCacheWeb(IDDoc)
                Obj.Finaliza()
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes')", True)
            Else
                MsgErro("Este Perito não possui Curriculum")
            End If
        Catch
        Finally
        End Try

    End Sub

    Protected Sub TxtCEP_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCEP.TextChanged
        Dim Bal As New BalCEP(GetUsuario)
        Dim EntCEP As New EntCEP
        If Not Me.IsPostBack Or TxtCEP.Text = "" Then
            Exit Sub
        End If
        Try
            EntCEP = Bal.ExibirDadosEnt(TxtCEP.Text)
            If Not EntCEP Is Nothing Then
                CboBairro.SelectedValue = CboBairro.Items.FindByValue(EntCEP.Cod_Bai.ToString).Value
                txtNome_Logr.Text = EntCEP.Logradouro
                CboCidade.SelectedValue = CboCidade.Items.FindByValue(EntCEP.Cod_Cid.ToString).Value
                CboUF.SelectedValue = CboUF.Items.FindByValue(EntCEP.Sigla_UF.ToString).Value
            End If
        Catch
        End Try
    End Sub
    
    Protected Sub BtnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSair.Click
        Response.Redirect("Principal.aspx")
    End Sub
    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
    End Sub
    Protected Sub CboProfissao1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao1.SelectedIndexChanged
        PreencherEspecialidade1(CInt(CboProfissao1.Items.FindByValue(CboProfissao1.Text).Value))
    End Sub

    Protected Sub CboOrgao_Per_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboOrgao_Per.SelectedIndexChanged
        ScriptManager1.SetFocus(txtNum_Reg)
    End Sub

    Protected Sub BtnDadosBancarios_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnDadosBancarios.Click
        Dim BalPer As New BALPERITO(GetUsuario)

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If lblExcluido.Text = "EXCLUÍDO" Then
            MsgErro("Perito Excluído. Gravação rejeitada")
            Exit Sub
        End If
        If txtID_PF.Text = "" Then 'perito sem dados gravados
            MsgErro("Gravação rejeitada! Grave os dados do perito antes de cadastrar os dados bancários")
            Exit Sub
        End If
        If Not RdSit.Items(0).Selected Then
            MsgErro("Gravação rejeitada! O Perito possui pendências")
            Exit Sub
        End If
        'Ativo - RdAtivo.Items(0).Selected
        If Not RdAtivo.Items(0).Selected Then
            MsgErro("Gravação rejeitada! O Perito esta inativo")
            Exit Sub
        End If
        Dim m_Cod_Banco As String
        m_Cod_Banco = CboBanco.Items.FindByValue(CboBanco.Text).Value

        BalPer.GravarContaCorrente(txtCPF.Text, m_Cod_Banco, TxtNum_Agencia.Text, txtNome_Agencia.Text, txtNum_Conta_Corrente.Text)
    End Sub

    Protected Sub BtnNovoEndRes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovoEndRes.Click

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        If lblExcluido.Text = "EXCLUÍDO" Then
            MsgErro("Perito Excluído")
            Exit Sub
        End If
        PreencherTip_Logr()
        PreencherUF()
        PreencherCidade("RJ")
        PreencherBairro("1")
        txtCompl_Logr.Text = ""
        TxtCEP.Text = ""
        txtNum_Logr.Text = ""
        txtNome_Logr.Text = ""

    End Sub

    Protected Sub TxtCEP1_TextChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles TxtCEP1.TextChanged
        Dim Bal As New BalCEP(GetUsuario)
        Dim EntCEP As New EntCEP
        If Not Me.IsPostBack Or TxtCEP1.Text = "" Then
            Exit Sub
        End If
        Try
            EntCEP = Bal.ExibirDadosEnt(TxtCEP1.Text)
            If Not ent Is Nothing Then
                CboBairro1.SelectedValue = CboBairro.Items.FindByValue(EntCEP.Cod_Bai.ToString).Value
                txtNome_Logr1.Text = EntCEP.Logradouro
                CboCidade1.SelectedValue = CboCidade.Items.FindByValue(EntCEP.Cod_Cid.ToString).Value
                CboUF1.SelectedValue = CboUF.Items.FindByValue(EntCEP.Sigla_UF.ToString).Value
            End If
        Catch
        End Try
    End Sub

    Protected Sub BtnEndRes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEndRes.Click
        PanelEndRes.Visible = True
        PanelEndRes.Enabled = True
        PanelEndCom.Visible = False
        PanelEndCom.Enabled = False
        PanelChklist.Visible = False
        PanelChklist.Enabled = False
        PanelTel.Visible = False
        PanelTel.Enabled = False
        PanelInf.Visible = False
        PanelInf.Enabled = False
    End Sub

    Protected Sub BtnEndCom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEndCom.Click
        PanelEndRes.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Visible = True
        PanelEndCom.Enabled = True
        PanelChklist.Visible = False
        PanelChklist.Enabled = False
        PanelTel.Visible = False
        PanelTel.Enabled = False
        PanelInf.Visible = False
        PanelInf.Enabled = False
    End Sub

    Protected Sub BtnTel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnTel.Click
        PanelEndRes.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Visible = False
        PanelEndCom.Enabled = False
        PanelChklist.Visible = False
        PanelChklist.Enabled = False
        PanelTel.Visible = True
        PanelTel.Enabled = True
        PanelInf.Visible = False
        PanelInf.Enabled = False
    End Sub

    Protected Sub BtnChklist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnChklist.Click
        PanelEndRes.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Visible = False
        PanelEndCom.Enabled = False
        PanelChklist.Visible = True
        PanelChklist.Enabled = True
        PanelTel.Visible = False
        PanelTel.Enabled = False
        PanelInf.Visible = False
        PanelInf.Enabled = False
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
        PanelEndRes.Visible = False
        PanelEndRes.Enabled = False
        PanelEndCom.Visible = False
        PanelEndCom.Enabled = False
        PanelChklist.Visible = False
        PanelChklist.Enabled = False
        PanelTel.Visible = False
        PanelTel.Enabled = False
        PanelInf.Visible = True
        PanelInf.Enabled = True
    End Sub
End Class
