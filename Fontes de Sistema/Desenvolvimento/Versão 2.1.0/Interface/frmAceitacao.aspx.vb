Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmAceitacao
    Inherits BasePage
    Private ent As New EntPERITO
    Private entAnot As New EntAnotacao
    Dim i, j As Integer
    'Dim Perito As Boolean


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Bal As New BALPERITO(GetUsuario)
        Dim Usuario As String
        Dim sCPF As String = String.Empty

        If Not IsPostBack Then
            BtnGravar.Enabled = False
        End If

        Usuario = GetUsuario.UsuarioSO
        sCPF = Bal.ValidarPeritoCPF(ID_Usuario)

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

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALPERITO(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim n As Integer = 0

        If lblCPF.Text = "" Then
            MsgErro("CPF Perito não encontrado.", "Erro")
            Exit Sub
        End If

        If lblNome.Text = "" Then
            MsgErro("Nome do perito não encontrado.", "Erro")
            Exit Sub
        End If

        If txtID_PF.Text = "" Then
            MsgErro("Houve falha na gravação.")
            Exit Sub
        End If

        'Data_Aceitacao Data_Negação, Motivo_Recusa, Honorários

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

        If RdoAceitacao.Items(0).Selected Then 'Aceito
            'Gravar(Data Aceitação, Data Negação)
            BalProcPer.Gravar(txtNum_CNJ.Text, ent.ID_PF, "", Today.ToShortDateString, TxtMotivo.Text, txtHonorarios.Text)
        End If
        If RdoAceitacao.Items(1).Selected Then 'Negado
            BalProcPer.Gravar(txtNum_CNJ.Text, ent.ID_PF, Today.ToShortDateString, "", TxtMotivo.Text, txtHonorarios.Text)
        End If

        If txtID_PF.Text = "" Then
            If Not ent.ID_PF = Nothing Then
                txtID_PF.Text = ent.ID_PF.ToString
            End If
        End If
       
        Limpar()
        PreencherGrdProcesso(CLng(txtID_PF.Text))
        
    End Sub

    Private Sub Limpar()
        txtOrgaoProfissional.Text = ""
        txtNum_Processo.Text = ""
        txtNum_CNJ.Text = ""
        txtNum_Reg.Text = ""
        txtEspecialidade.Text = ""
        txtProfissao.Text = ""
        TxtMotivo.Visible = False
        txtHonorarios.Text = ""
        lblPrazo.Text = "30"
        RdoAceitacao.Items(0).Selected = False
        RdoAceitacao.Items(1).Selected = False
        lblRecusa.Visible = False
        lnkVisualizaProcesso.Visible = False
    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Limpar()
    End Sub

    Protected Sub GrdProcesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdProcessos.SelectedIndexChanged

        TxtMotivo.Text = ""
        txtHonorarios.Text = ""
        If GrdProcessos.SelectedRow.Cells(1).Text <> "" Then
            txtNum_CNJ.Text = GrdProcessos.SelectedRow.Cells(1).Text
            Dim EntProc As EntPROC_CNJ
            Dim EntProcPer As EntProcesso_Perito
            Dim BalP As New BalProc_CNJ(GetUsuario)
            Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
            Dim sMsg As String = "Ao aceitar informe o valor em UFIRs. Ao Recusar informe o motivo"

            EntProc = BalP.ExibirDadosEnt("", txtNum_CNJ.Text)
            lnkVisualizaProcesso.Visible = True
            If Not EntProc Is Nothing Then
                If txtNum_Processo.Text = "" Then
                    txtNum_Processo.Text = EntProc.Cod_Proc
                End If
                EntProcPer = BalProcPer.ExibirDadosEnt(EntProc.Cod_CNJ, Convert.ToInt64(txtID_PF.Text), "", "") 'ent.ID_PF)
            Else
                EntProcPer = BalProcPer.ExibirDadosEnt(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text), "", "") 'ent.ID_PF)
            End If
            
            If Not EntProcPer Is Nothing Then
                Dim entEspPerito As EntEspecialidade_Perito
                Dim balEspPerito As New BALEspecialidadePerito(GetUsuario)
                Dim balOrgProfissional As New BalOrgao_Per(GetUsuario)
                Dim entOrgProfissional As EntOrgao_per

                entEspPerito = balEspPerito.Consultar(CLng(txtID_PF.Text), EntProcPer.COD_PROFISSAO, EntProcPer.COD_ESPECIALIDADE)
                txtNum_Reg.Text = IIf(entEspPerito.Num_Reg = "", String.Empty, entEspPerito.Num_Reg & " - " & entEspPerito.UF).ToString

                entOrgProfissional = balOrgProfissional.ConsultarOrgProfissional(CStr(entEspPerito.COD_ORGAO_PER))
                Session("CodOrgaoProfissional") = entOrgProfissional.COD_ORGAO_PER
                Session("NumReg") = entEspPerito.Num_Reg
                txtOrgaoProfissional.Text = entOrgProfissional.DESCR_ORGAO_PER & ""

                txtProfissao.Text = EntProcPer.DESCR_PROFISSAO
                txtEspecialidade.Text = EntProcPer.DESCR_ESPECIALIDADE
                lblPrazo.Text = EntProcPer.PRAZO_ENTREGA.ToString
                TxtMotivo.Visible = False
                lblRecusa.Visible = False
                'txtHonorarios.Visible = False
                RdoAceitacao.Items(0).Selected = False
                RdoAceitacao.Items(1).Selected = False
                If Not EntProcPer.Data_Aceitacao.ToString Is Nothing And Not EntProcPer.Data_Aceitacao.ToString = "01/01/0001 00:00:00" Then
                    RdoAceitacao.Items(0).Selected = True
                    txtHonorarios.Visible = True
                End If
                If Not EntProcPer.Data_Negacao.ToString Is Nothing And Not EntProcPer.Data_Negacao.ToString = "01/01/0001 00:00:00" Then
                    RdoAceitacao.Items(1).Selected = True
                    TxtMotivo.Visible = True
                    lblRecusa.Visible = True
                End If
                If Not EntProcPer.Honorarios.ToString Is Nothing Then
                    txtHonorarios.Text = EntProcPer.Honorarios.ToString
                End If
                If Not EntProcPer.Motivo_Recusa Is Nothing Then
                    TxtMotivo.Text = EntProcPer.Motivo_Recusa.ToString
                End If
                If EntProcPer.Data_Aceitacao.ToShortDateString <> "01/01/0001" Or EntProcPer.Data_Negacao.ToShortDateString <> "01/01/0001" Then
                    TxtMotivo.Enabled = False
                    RdoAceitacao.Enabled = False
                    BtnGravar.Enabled = False
                    sMsg = ""
                Else
                    TxtMotivo.Enabled = True
                    txtHonorarios.Enabled = True
                    RdoAceitacao.Enabled = True
                    BtnGravar.Enabled = True
                End If
            End If
            If sMsg <> "" Then
                MsgErro(sMsg)
            End If
        End If

    End Sub

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
            TxtMotivo.Visible = True
            TxtMotivo.Enabled = True
            lblRecusa.Visible = True
        Else  'Aceitação
            TxtMotivo.Visible = False
            TxtMotivo.Enabled = False
            lblRecusa.Visible = False
            txtHonorarios.Visible = True
            txtHonorarios.Enabled = True
            End If
    End Sub

    Protected Sub lnkVisualizaProcesso_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkVisualizaProcesso.Click
        If txtNum_Processo.Text = "" Then
            MsgErro("Selecione um processo da lista.")
            Exit Sub
        End If
        Response.Redirect("http://wwwh4.tjrj.jus.br/hconsultaProcessoWebV2/consultaProc.do?v=2&FLAGNOME=&back=1&tipoConsulta=publica&numProcesso=" & txtNum_Processo.Text)
    End Sub
End Class
