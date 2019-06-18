Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmAceitacao
    Inherits BasePage
    Private ent As New EntPERITO
    Private entAnot As New EntAnotacao
    Dim i, j As Integer
    Dim Perito As Boolean


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Bal As New BALPERITO(GetUsuario)
        Dim Usuario As String

        Usuario = GetUsuario.UsuarioSO
        Perito = Bal.ValidarPerito(ID_Usuario)

        'Cod_tip_Func = 4 -> Perito Judicial
        '<> 4 -> DIPEJ e outros que tenham acesso para consultar
        If Perito Then
            If Not Me.IsPostBack Then
                RdoAceitacao.Enabled = True
                txtCPF.Enabled = False
                txtNome.Enabled = False
                CboPerito.Enabled = False
                CboPerito.Visible = False
                CboOrgao_Per.Enabled = False
                txtNum_Reg.Enabled = False
                BtnExcluir.Visible = False
                BtnLimpar.Visible = False
                txtID_PF.Text = Bal.ID_Perito(ID_Usuario)
                txtCPF.Attributes.Add("onblur", "validacpf(ctl00$Tela$txtCPF.value);")
                PreencherOrgao_Per()
                txtCPF.Text = Bal.LocalizaCPFPerito(CInt(txtID_PF.Text))
                PreencherDadosPerito()
            End If
            txtNome.Attributes.Add("onkeydown", "if(event.which==13){ event.which = 9; } if(event.keyCode==13){ event.keyCode = 9; }")
        Else
            If Not Me.IsPostBack Then
                RdoAceitacao.Enabled = False
                txtID_PF.Text = ID_Usuario.ToString
                txtCPF.Enabled = True
                txtNome.Enabled = True
                CboPerito.Enabled = True
                CboPerito.Visible = True
                CboOrgao_Per.Enabled = True
                txtNum_Reg.Enabled = True
                BtnExcluir.Visible = False
                BtnLimpar.Visible = True
                PreencherOrgao_Per()
            End If
        End If


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

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALPERITO(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
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

        'Data_Aceitacao Data_Negação, Motivo_Recusa, Honorários

        ent.Nome = txtNome.Text
        ent.Num_Reg = txtNum_Reg.Text
        entAnot.SIGLA = "SANTO"
        If IsNumeric(txtCPF.Text) Then
            ent.CPF = txtCPF.Text
        Else
            ent.CPF = ""
        End If
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
        txtNum_Reg.AutoPostBack = True
        txtNome.AutoPostBack = True
        txtCPF.AutoPostBack = True
        Response.Redirect("frmaceitacao.aspx")
        'Limpar()
        
    End Sub


    Private Sub PreencherDadosPerito()

        Dim EntProcPer As New EntProcesso_Perito
        If Not Me.IsPostBack Then
            PreencherOrgao_Per()
            'Exit Sub
        End If
        Dim Bal As New BALPERITO(GetUsuario)
        If txtCPF.Text = "" And txtNome.Text = "" Then
            If txtNum_Reg.Text = "" Or CboOrgao_Per.Items.FindByValue(CboOrgao_Per.Text).Value = "Selecione o Órgão" Then
                Exit Sub
            End If
        End If
        txtNum_Reg.AutoPostBack = False
        txtNome.AutoPostBack = False
        txtCPF.AutoPostBack = False
        If txtNome.Text <> "" And txtCPF.Text = "" And txtNum_Reg.Text = "" Then
            ent = Bal.ExibirDadosEnt("NOMEEXATO", txtNome.Text, "N")
        ElseIf txtCPF.Text <> "" And txtNome.Text = "" And txtNum_Reg.Text = "" Then
            ent = Bal.ExibirDadosEnt("CPF", txtCPF.Text, "N")
        ElseIf txtNum_Reg.Text <> "" And txtCPF.Text = "" And txtNome.Text = "" Then
            ent = Bal.ExibirDadosEnt("NUMREG", txtNum_Reg.Text, "N", CInt(CboOrgao_Per.Items.FindByValue(CboOrgao_Per.Text).Value))
        ElseIf txtID_PF.Text <> "" Then
            ent = Bal.ExibirDadosEnt("ID", txtID_PF.Text, "N")
        Else
            Exit Sub
        End If
        If Not ent Is Nothing Then
            If ent.Cod_Perito.ToString = "0" Or ent.ID_PF.ToString = "0" Then Exit Sub
            txtID_PF.Text = ent.ID_PF.ToString
            txtNum_Reg.Text = ent.Num_Reg
            txtCPF.Text = ent.CPF
            txtNome.Text = ent.Nome
            If Not ent.COD_ORGAO_PER = Nothing Then
                CboOrgao_Per.SelectedValue = CboOrgao_Per.Items.FindByValue(ent.COD_ORGAO_PER.ToString).Value
            End If
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
        txtCPF.Text = ""
        CboOrgao_Per.SelectedIndex = 0
        CboPerito.Items.Clear()
        txtNum_Processo.Text = ""
        txtNum_CNJ.Text = ""
        txtNum_Reg.Text = ""
        ValidarCPF.Text = ""
        txtNum_Reg.AutoPostBack = True
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
                Exit Sub
            End If
            If txtNome.Text = "" And txtNum_Reg.Text = "" Then
                CboOrgao_Per.SelectedIndex = 0
                txtCPF.Text = m_CPF
                If Trim(txtCPF.Text) <> "" And txtNome.Text = "" And txtNum_Reg.Text = "" Then
                    PreencherDadosPerito()
                End If
            End If
        End If
    End Sub

    Protected Sub txtNum_Reg_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_Reg.TextChanged
        If txtNum_Reg.Text <> "" Then
            Dim m_Reg As String
            m_Reg = txtNum_Reg.Text
            If txtCPF.Text = "" And txtNome.Text = "" Then
                txtNum_Reg.Text = m_Reg
                If txtNum_Reg.Text <> "" And txtNome.Text = "" And txtCPF.Text = "" Then
                    PreencherDadosPerito()
                End If
            End If
        End If
    End Sub
    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome.TextChanged
        If txtNome.Text <> "" Then
            If txtCPF.Text = "" And txtNum_Reg.Text = "" Then
                CboOrgao_Per.SelectedIndex = 0
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

        'Data Aceitacao = "" e Data Negação = ""
        TxtMotivo.Text = ""
        txtHonorarios.Text = ""
        If GrdProcessos.SelectedRow.Cells(1).Text <> "" Then
            txtNum_CNJ.Text = GrdProcessos.SelectedRow.Cells(1).Text
            'txtNum_Processo.Text = GrdProcessos.SelectedRow.Cells(3).Text
            Dim EntProc As EntPROC_CNJ
            Dim EntProcPer As EntProcesso_Perito
            Dim BalP As New BalProc_CNJ(GetUsuario)
            Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
            EntProc = BalP.ExibirDadosEnt("", txtNum_CNJ.Text)
            lnkVisualizaProcesso.Visible = True
            If Not EntProc Is Nothing Then
                If txtNum_Processo.Text = "" Then
                    txtNum_Processo.Text = EntProc.Cod_Proc
                End If
                EntProcPer = BalProcPer.ExibirDadosEnt(EntProc.Cod_CNJ, Convert.ToInt64(txtID_PF.Text)) 'ent.ID_PF)
            Else
                EntProcPer = BalProcPer.ExibirDadosEnt(txtNum_CNJ.Text, Convert.ToInt64(txtID_PF.Text)) 'ent.ID_PF)
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
                    txtHonorarios.Enabled = False
                    RdoAceitacao.Enabled = False
                Else
                    TxtMotivo.Enabled = True
                    txtHonorarios.Enabled = True
                    RdoAceitacao.Enabled = True
                End If
            End If
        End If
        If Perito Then
            MsgErro("Ao aceitar informe o valor em UFIRs. Ao Recusar informe o motivo")
            lblRecusa.Visible = False
            txtHonorarios.Visible = False
            TxtMotivo.Visible = False
            RdoAceitacao.Enabled = True
            RdoAceitacao.Items(0).Selected = False
            RdoAceitacao.Items(1).Selected = False
            txtCPF.Enabled = False
            txtNome.Enabled = False
            CboPerito.Enabled = False
            CboPerito.Visible = False
            CboOrgao_Per.Enabled = False
            txtNum_Reg.Enabled = False
            BtnExcluir.Visible = False
            BtnLimpar.Visible = False
        Else
            RdoAceitacao.Enabled = False
            txtCPF.Enabled = True
            txtNome.Enabled = True
            CboPerito.Enabled = True
            CboPerito.Visible = True
            CboOrgao_Per.Enabled = True
            txtNum_Reg.Enabled = True
            BtnExcluir.Visible = False
            BtnLimpar.Visible = True
        End If

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

    Protected Sub CboOrgao_Per_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboOrgao_Per.SelectedIndexChanged

    End Sub

    Protected Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboPerito.SelectedIndexChanged
        If Me.IsPostBack Then
            txtNome.Text = CboPerito.SelectedItem.ToString
            If Trim(txtNome.Text) <> "" And txtCPF.Text = "" And txtNum_Reg.Text = "" Then
                PreencherDadosPerito()
            End If
        End If
    End Sub

    Protected Sub lnkVisualizaProcesso_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkVisualizaProcesso.Click
        Response.Redirect("http://tjerj314.tjrj.jus.br/consultaProcessoWebH/consultaProc.do?v=2&FLAGNOME=&back=1&tipoConsulta=publica&numProcesso=" & txtNum_Processo.Text)
    End Sub
End Class
