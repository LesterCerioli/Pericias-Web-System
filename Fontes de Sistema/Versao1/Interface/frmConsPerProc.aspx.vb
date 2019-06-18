Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmConsPerProc
    Inherits BasePage
    Private ent As New EntPERITO
    Private entAnot As New EntAnotacao
    Dim i, j As Integer
    Dim Perito As Boolean


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Bal As New BALPERITO(GetUsuario)
        'Dim Usuario As String

        'Usuario = GetUsuario.UsuarioSO
        'Perito = Bal.ValidarPerito(ID_Usuario)

        'Cod_tip_Func = 4 -> Perito Judicial
        '<> 4 -> DIPEJ e outros que tenham acesso para consultar
        'If Perito Then
        If Not Me.IsPostBack Then
            RdoAceitacao.Enabled = False
            txtID_PF.Text = ID_Usuario.ToString
            txtCPF.Enabled = True
            txtNome.Enabled = True
            CboPerito.Enabled = True
            CboPerito.Visible = True
            BtnLimpar.Visible = True
        End If


    End Sub

    Private Sub PreencherDadosPerito()

        Dim EntProcPer As New EntProcesso_Perito

        Dim Bal As New BALPERITO(GetUsuario)
        'If txtCPF.Text = "" And txtNome.Text = "" Then
        txtNome.AutoPostBack = False
        txtCPF.AutoPostBack = False
        If txtNome.Text <> "" And txtCPF.Text = "" Then
            ent = Bal.ExibirDadosEnt("NOMEEXATO", txtNome.Text, "N")
        ElseIf txtCPF.Text <> "" And txtNome.Text = "" Then
            ent = Bal.ExibirDadosEnt("CPF", txtCPF.Text, "N")
            'ElseIf txtCPF.Text = "" And txtNome.Text = "" Then
            'ent = Bal.ExibirDadosEnt("NUMREG", txtNum_Reg.Text, "N", CInt(CboOrgao_Per.Items.FindByValue(CboOrgao_Per.Text).Value))
        ElseIf txtID_PF.Text <> "" Then
            ent = Bal.ExibirDadosEnt("ID", txtID_PF.Text, "N")
        Else
            Exit Sub
        End If
        If Not ent Is Nothing Then
            If ent.Cod_Perito.ToString = "0" Or ent.ID_PF.ToString = "0" Then Exit Sub
            txtID_PF.Text = ent.ID_PF.ToString
            txtCPF.Text = ent.CPF
            txtNome.Text = ent.Nome
            If ent.ID_PF <> 0 Then
                PreencherGrdProcesso(ent.ID_PF)
            End If
        End If


    End Sub

    Private Sub Limpar()
        If txtNome.Text = "" Then
            txtCPF.Text = ""
            Exit Sub
        End If
        txtID_PF.Text = ""
        txtData_Nomeacao.Text = ""
        txtCPF.Text = ""
        CboPerito.Items.Clear()
        txtNum_Processo.Text = ""
        txtNum_CNJ.Text = ""
        ValidarCPF.Text = ""
        txtNome.AutoPostBack = True
        txtCPF.AutoPostBack = True
        txtNome.Text = ""
        TxtMotivo.Visible = False
        txtHonorarios.Visible = False
        txtPrazo.Text = "30"
        RdoAceitacao.Items(0).Selected = False
        RdoAceitacao.Items(1).Selected = False
        GrdProcessos.DataSource = Nothing
        GrdProcessos.DataBind()
    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Limpar()
    End Sub

    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCPF.TextChanged
        If txtCPF.Text <> "" Then
            Dim m_CPF As String
            m_CPF = txtCPF.Text
            If Trim(txtCPF.Text) = "" Then
                MsgErro("O campo CPF está vazio")
            End If
            If Not ValidarCPFServ(txtCPF.Text) Then
                MsgErro("CPF Inválido")
                txtCPF.Text = ""
                Exit Sub
            End If
            If txtNome.Text = "" Then
                txtCPF.Text = m_CPF
                If Trim(txtCPF.Text) <> "" And txtNome.Text = "" Then
                    PreencherDadosPerito()
                End If
            End If
        End If
    End Sub

    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome.TextChanged
        If txtNome.Text <> "" Then
            If txtCPF.Text = "" Then
                PreencherSemelhantes(txtNome.Text)
            End If
        End If

    End Sub
    Private Sub PreencherSemelhantes(ByVal m_Nome As String)
        If Not Me.IsPostBack Then
            Exit Sub
        End If
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

    Protected Sub GrdProcesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdProcessos.SelectedIndexChanged

        TxtMotivo.Text = ""
        txtHonorarios.Text = ""
        If GrdProcessos.SelectedRow.Cells(3).Text <> "" Then
            txtNum_CNJ.Text = Trim(GrdProcessos.SelectedRow.Cells(3).Text)
            txtData_Nomeacao.Text = Mid(GrdProcessos.SelectedRow.Cells(6).Text, 1, 10)
            Dim EntProc As EntPROC_CNJ
            Dim EntProcPer As EntProcesso_Perito
            Dim EntAceitaPer As New EntAceitacao_Perito
            Dim BalP As New BalProc_CNJ(GetUsuario)
            Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
            EntProc = BalP.ExibirDadosEnt("", txtNum_CNJ.Text)
            lnkVisualizaProcesso.Visible = True
            If Not EntProc Is Nothing Then
                If txtNum_Processo.Text = "" Then
                    txtNum_Processo.Text = EntProc.Cod_Proc
                End If
                'FOS - Verificar a necessidade de informar o código da profissão e especialidade
                EntProcPer = BalProcPer.ExibirDadosEnt(EntProc.Cod_CNJ, Convert.ToInt64(txtID_PF.Text), "", "", CDate(txtData_Nomeacao.Text)) 'ent.ID_PF)
            Else
                'FOS - Verificar a necessidade de informar o código da profissão e especialidade
                EntProcPer = BalProcPer.ExibirDadosEnt(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text), "", "", CDate(txtData_Nomeacao.Text)) 'ent.ID_PF)
            End If
            'Ent.ID_PF
            'EntProcPer = BalProcPer.ExibirDadosEnt(EntProc.Cod_CNJ, Convert.ToInt64(txtID_PF.Text)) 'ent.ID_PF)
            If Not EntProcPer Is Nothing Then
                txtProfissao.Text = EntProcPer.DESCR_PROFISSAO
                txtEspecialidade.Text = EntProcPer.DESCR_ESPECIALIDADE
                txtPrazo.Text = EntProcPer.PRAZO_ENTREGA.ToString
                TxtMotivo.Visible = False
                lblRecusa.Visible = False
                txtHonorarios.Visible = False
                RdoAceitacao.Items(0).Selected = False
                RdoAceitacao.Items(1).Selected = False
                If Not EntAceitaPer.Data_Aceitacao.ToString Is Nothing And Not EntAceitaPer.Data_Aceitacao.ToString = "01/01/0001 00:00:00" Then
                    RdoAceitacao.Items(0).Selected = True
                    txtHonorarios.Visible = True
                End If
                If Not EntAceitaPer.Data_Negacao.ToString Is Nothing And Not EntAceitaPer.Data_Negacao.ToString = "01/01/0001 00:00:00" Then
                    RdoAceitacao.Items(1).Selected = True
                    TxtMotivo.Visible = True
                    lblRecusa.Visible = True
                End If
                If Not EntAceitaPer.Honorarios.ToString Is Nothing Then
                    txtHonorarios.Text = EntAceitaPer.Honorarios.ToString
                End If
                If Not EntAceitaPer.Motivo_Recusa Is Nothing Then
                    TxtMotivo.Text = EntAceitaPer.Motivo_Recusa.ToString
                End If
                If EntAceitaPer.Data_Aceitacao.ToShortDateString <> "01/01/0001" Or EntAceitaPer.Data_Negacao.ToShortDateString <> "01/01/0001" Then
                    TxtMotivo.Enabled = False
                    txtHonorarios.Enabled = False
                    RdoAceitacao.Enabled = False
                Else
                    TxtMotivo.Enabled = True
                    txtHonorarios.Enabled = True
                    RdoAceitacao.Enabled = True
                End If
            End If
        End If

        RdoAceitacao.Enabled = False
        txtCPF.Enabled = True
        txtNome.Enabled = True
        CboPerito.Enabled = True
        CboPerito.Visible = True
        BtnLimpar.Visible = True

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
            TxtMotivo.Visible = True
            TxtMotivo.Enabled = True
            lblRecusa.Visible = True
            txtHonorarios.Visible = False
            txtHonorarios.Enabled = False
        Else  'Aceitação
            TxtMotivo.Visible = False
            TxtMotivo.Enabled = False
            lblRecusa.Visible = False
            txtHonorarios.Visible = True
            txtHonorarios.Enabled = True
        End If
    End Sub

    Protected Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboPerito.SelectedIndexChanged
        If Me.IsPostBack Then
            txtNome.Text = CboPerito.SelectedItem.ToString
            If Trim(txtNome.Text) <> "" And txtCPF.Text = "" Then
                PreencherDadosPerito()
            End If
        End If
    End Sub

    Protected Sub lnkVisualizaProcesso_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkVisualizaProcesso.Click
        Response.Redirect("http://tjerj314.tjrj.jus.br/consultaProcessoWebH/consultaProc.do?v=2&FLAGNOME=&back=1&tipoConsulta=publica&numProcesso=" & txtNum_Processo.Text)
    End Sub
End Class
