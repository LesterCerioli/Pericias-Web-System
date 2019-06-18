Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
'Imports log4net

Partial Public Class FrmAnotacoes
    Inherits BasePage
    Private ent As New EntPERITO
    Private entAnot As New EntAnotacao
    Dim i, j As Integer
    Dim Cadastro As Boolean
    Dim m_Sigla As String
    'Dim logger As log4net.ILog


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'logger = log4net.LogManager.GetLogger("LogInFile")


        Cadastro = False
        If Not Me.IsPostBack Then

            'logger.Debug("Acesso ao cadastro de anotações")

            lblValidaCPF.Visible = False
            lblValidaNome.Visible = False

            BtnExcluir.Enabled = False
            BtnGravar.Enabled = False

            txtCPF.Attributes.Add("onblur", "validacpf(ctl00$Tela$txtCPF.value);")
            txtCPF.Text = Request.QueryString("CPF")
            'logger.Debug("PreencherTipo_Anotacao")
            PreencherTipo_Anotacao()
            'logger.Debug("lblData_Anotacao.Text = " & Today.ToShortDateString)
            lblData_Anotacao.Text = Today.ToShortDateString
            If txtCPF.Text <> "" Then
                BtnExcluir.Visible = False
                Cadastro = True
                'logger.Debug("PreencherDadosPerito")
                PreencherDadosPerito()
                CboPerito.Text = txtNome.Text
                CboPerito.Visible = False
                lblNomesSemelhantes.Visible = False
                BtnNova.Visible = False
                GrdAnotacoes.Visible = False
                cboTipo_anotacao.SelectedValue = cboTipo_anotacao.Items.FindByText("EXCLUSÃO").Value
                cboTipo_anotacao.Enabled = False
                txtCPF.Enabled = False
                txtNome.Enabled = False
                BtnLimpar.Visible = False
            Else
                BtnExcluir.Visible = True
                BtnLimpar.Visible = True
                BtnExcluir.Attributes.Add("OnClick", "return confirm('Confirma a Exclusão?');")
            End If
        End If

    End Sub

    Private Sub PreencherTipo_Anotacao()
        'logger.Debug("PreencherTipo_Anotacao ...")
        Dim bal As New BalTipoAnotacao(GetUsuario)
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        cboTipo_anotacao.Items.Clear()
        cboTipo_anotacao.DataTextField = "Descr_Tipo_Anotacao"
        cboTipo_anotacao.DataValueField = "Cod_Tipo_Anotacao"
        cboTipo_anotacao.DataSource = dsfila.Tables(0)
        cboTipo_anotacao.DataBind()
        cboTipo_anotacao.Items.Insert(0, "Selecione o Tipo da Anotacao")
        cboTipo_anotacao.SelectedIndex = 0

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        'logger.Debug("BtnGravar_Click ...")
        Dim resultado As Boolean
        If Not Me.IsPostBack Then
            Exit Sub
        End If

        If PermiteInclusaoDeAnotacao(True, False) Then

            If cboTipo_anotacao.SelectedIndex = 0 Then
                MsgErro("Gravação rejeitada. Não foi selecionado o Tipo de Anotação")
                CboPerito.Focus()
                Exit Sub
            End If

            If txtAnotacao.Text = "" Then
                MsgErro("Gravação rejeitada. Favor informar a justificativa da anotação.")
                txtAnotacao.Focus()
                Exit Sub
            End If

            If txtCPF.Text = "" Then
                lblValidaCPF.Visible = True
                txtCPF.Focus()
                Exit Sub
            End If

            If lblValidaNome.Text = "" Then
                lblValidaNome.Visible = True
                Exit Sub
            End If

            Dim Bal As New BALPERITO(GetUsuario)
            Dim BalAnot As New BALAnotacao(GetUsuario)
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
            entAnot.SIGLA = GetUsuario.Login
            entAnot.DATA_ANOTACAO = CDate(lblData_Anotacao.Text)
            entAnot.DATA_EXCLUSAO = CDate(System.Data.SqlTypes.SqlDateTime.Null)
            entAnot.DESCR_ANOTACAO = txtAnotacao.Text
            If TxtNum_Anotacao.Text <> "" Then
                entAnot.Num_Anotacao = CInt(TxtNum_Anotacao.Text)
            Else
                entAnot.Num_Anotacao = 0
            End If
            If IsNumeric(txtCPF.Text) Then
                ent.CPF = txtCPF.Text
            Else
                ent.CPF = ""
            End If

            If txtID_PF.Text = "" Then
                ent.ID_PF = 0
            Else
                ent.ID_PF = CInt(txtID_PF.Text)
            End If
            entAnot.ID_PF = ent.ID_PF
            entAnot.Cod_Tipo_Anotacao = CInt(cboTipo_anotacao.Items.FindByValue(cboTipo_anotacao.Text).Value)

            If cboTipo_anotacao.Text = "1" Then '"EXCLUSÂO" Then
                resultado = Bal.Excluir(CInt(txtID_PF.Text), GetUsuario.Login)
                If resultado Then
                    MsgErro("Perito Excluído")
                    m_Sigla = GetUsuario.Login
                    Bal.NovoStatus(txtID_PF.Text, m_Sigla) 'Inativo
                End If
            End If

            Dim ok As Boolean
            ok = BalAnot.Gravar(entAnot)
            If ok Then
                MsgErro("Anotação gravada com sucesso")
            End If

            PermiteInclusaoDeAnotacao(False, True)

            LimpaAnotacao()

            If txtID_PF.Text = "" Then
                If Not ent.ID_PF = Nothing Then
                    txtID_PF.Text = ent.ID_PF.ToString
                End If
            End If
            txtNome.AutoPostBack = True
            txtCPF.AutoPostBack = True
            PreencherAnotacoes(CInt(txtID_PF.Text))
            If Cadastro Then
                Response.Redirect("frmCadastrarPerito.aspx")
            End If

        End If

    End Sub

    Private Sub LimpaAnotacao()
        'logger.Debug("LimpaAnotacao ...")
        cboTipo_anotacao.SelectedIndex = 0
        txtAnotacao.Text = ""
        lblData_Anotacao.Text = Today.ToShortDateString
    End Sub

    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
        'logger.Debug("BtnExcluir_Click ...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALAnotacao(GetUsuario)
        Dim Resultado As Boolean
        If txtID_PF.Text = "" Then
            MsgErro("Exclusão não efetuada. Verifique o código do perito")
            Exit Sub
        Else
            If cboTipo_anotacao.SelectedIndex = 0 Then
                MsgErro("Selecione a anotação a ser excluída.")
                Exit Sub
            End If

            If lblData_Anotacao.Text = "" Then
                MsgErro("Erro na Data da Anotação. Exclusão Rejeitada!")
                Exit Sub
            End If
            'Resultado = Bal.Excluir(CInt(txtID_PF.Text), CDate(lblData_Anotacao.Text))
            Resultado = Bal.Excluir(CInt(TxtNum_Anotacao.Text))

            If Resultado Then
                MsgErro("Anotação excluída com sucesso.")
                LimpaAnotacao()
            End If
        End If
        PreencherAnotacoes(CInt(txtID_PF.Text))
        PermiteInclusaoDeAnotacao(False, True)
    End Sub

    Private Sub PreencherSemelhantes(ByVal m_Nome As String)
        'logger.Debug("PreencherSemelhantes ...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim bal As New BALPERITO(GetUsuario)
        Dim dsfila As New DataSet
        If m_Nome = "" Then Exit Sub
        CboPerito.Items.Clear()
        dsfila = bal.ExibirTodosDadosSet("NOME", m_Nome) 'Serão exibidos os excluídos
        CboPerito.DataTextField = "Nome"
        CboPerito.DataValueField = "Cod_Perito"
        CboPerito.DataSource = dsfila.Tables(0) '.DefaultView
        CboPerito.DataBind()
        CboPerito.Items.Insert(0, "Opcional -> Clique aqui para escolher nomes semelhantes")
        CboPerito.SelectedIndex = 0

    End Sub

    Private Sub PreencherDadosPerito()
        'logger.Debug("PreencherSemelhantes ...")

        If Not Me.IsPostBack And txtCPF.Text = "" Then
            Exit Sub
        End If

        Dim Bal As New BALPERITO(GetUsuario)
        If txtNome.Text <> "" And txtCPF.Text = "" Then
            ent = Bal.ExibirTodosDadosEnt("NOMEEXATO", txtNome.Text, "S")
        ElseIf txtCPF.Text <> "" And txtNome.Text = "" Then
            ent = Bal.ExibirDadosEnt("CPF", txtCPF.Text, "S")
        Else
            Exit Sub
        End If
        If Not ent Is Nothing Then
            If ent.Cod_Perito.ToString = "0" Or ent.ID_PF.ToString = "0" Then Exit Sub
            txtID_PF.Text = ent.ID_PF.ToString
            txtCod_Perito.Text = ent.Cod_Perito.ToString
            txtCPF.Text = ent.CPF
            txtNome.Text = ent.Nome

            'logger.Debug("entAnot.DATA_ANOTACAO: " & entAnot.DATA_ANOTACAO)
            If entAnot.DATA_ANOTACAO <> #12:00:00 AM# Then
                lblData_Anotacao.Text = entAnot.DATA_ANOTACAO.ToShortDateString.ToString()
            Else
                lblData_Anotacao.Text = FormatDateTime(Date.Now, DateFormat.ShortDate).ToString()
            End If
            PreencherAnotacoes(ent.ID_PF)

            PermiteInclusaoDeAnotacao(False, True)

        Else
            MsgErro("O Perito não foi localizado")
        End If
    End Sub

    Private Sub PreencherAnotacoes(ByVal nID_PF As Long)
        'logger.Debug("PreencherAnotacoes ...")
        Dim Ds As DataSet
        Dim BalAnot As New BALAnotacao(GetUsuario)
        Ds = BalAnot.ExibirAnotPer(nID_PF)
        GrdAnotacoes.DataSource = Ds.Tables(0)
        GrdAnotacoes.DataBind()
    End Sub

    Protected Sub btnGrdVisualizar_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        'logger.Debug("btnGrdVisualizar_Command ...")
        Dim bal As New BALAnotacao(GetUsuario)
        Dim ent As EntAnotacao

        Dim Vetor(3) As String
        For i As Integer = 0 To 3
            Vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
        Next

        'ent = bal.ConsultarAnotacao(Vetor(0), Vetor(1), Vetor(2))
        ent = bal.ConsultarAnotacao(Vetor(3))

        If Not ent Is Nothing Then
            cboTipo_anotacao.SelectedValue = ent.Cod_Tipo_Anotacao.ToString()
            txtAnotacao.Text = ent.DESCR_ANOTACAO.ToString()

            lblData_Anotacao.Text = IIf(ent.DATA_ANOTACAO <> #12:00:00 AM# _
                                        , ent.DATA_ANOTACAO.ToShortDateString(), "").ToString()
            TxtNum_Anotacao.Text = ent.Num_Anotacao.ToString

            'logger.Debug("lblData_Anotacao.Text = " & IIf(ent.DATA_ANOTACAO <> #12:00:00 AM#, ent.DATA_ANOTACAO.ToShortDateString(), "").ToString())

            BtnExcluir.Enabled = True
            cboTipo_anotacao.Enabled = False
            txtAnotacao.Enabled = False
        End If

    End Sub

    Private Sub Limpar()

        lblValidaCPF.Visible = False
        lblValidaNome.Visible = False

        txtCPF.Text = ""
        txtNome.Text = ""
        txtAnotacao.Text = ""
        Session("Num_Nur") = 0
        txtCod_Perito.Text = ""
        txtID_PF.Text = ""
        txtNome.AutoPostBack = True
        txtCPF.AutoPostBack = True
        txtNome.Text = ""
        CboPerito.Items.Clear()
        GrdAnotacoes.DataSource = Nothing
        GrdAnotacoes.DataBind()
        cboTipo_anotacao.SelectedIndex = 0
        BtnExcluir.Enabled = False
        lblData_Anotacao.Text = Today.ToShortDateString
        BtnGravar.Enabled = False
    End Sub

    Private Function PermiteInclusaoDeAnotacao(ByVal bMensagem As Boolean, ByVal bHabilitaControles As Boolean) As Boolean
        'logger.Debug("PermiteInclusaoDeAnotacao ...")
        If txtID_PF.Text = String.Empty Then
            Exit Function
        End If

        Dim bal As New BALAnotacao(GetUsuario)

        'If bal.VerificaSeJaFoiIncluidoAnotacaoNoDia(txtID_PF.Text) = 1 Then
        '    If bMensagem Then
        '        MsgErro("Só é permitido uma inclusão de anotação por dia.")
        '    End If
        '    If bHabilitaControles Then
        '        BtnNova.Enabled = False
        '        cboTipo_anotacao.Enabled = False
        '        txtAnotacao.Enabled = False
        '    End If
        '    Return False
        'Else
        If bHabilitaControles Then
            BtnNova.Enabled = True
            cboTipo_anotacao.Enabled = True
            txtAnotacao.Enabled = True
            BtnGravar.Enabled = True
            'End If
            'Return True
        End If

        Return True

    End Function

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Limpar()
    End Sub

    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCPF.TextChanged
        If txtCPF.Text <> "" Then
            lblValidaCPF.Visible = False
            Dim m_CPF As String
            m_CPF = txtCPF.Text
            If Not ValidarCPFServ(txtCPF.Text) And txtCPF.Text <> "" Then
                MsgErro("CPF Inválido")
                txtCPF.Text = ""
                Exit Sub
            End If
            If txtNome.Text = "" Then
                txtCPF.Text = m_CPF
                If txtCPF.Text <> "" And txtNome.Text = "" Then
                    PreencherDadosPerito()
                End If
            End If
        End If
    End Sub

    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome.TextChanged
        If txtNome.Text <> "" Then
            lblValidaNome.Visible = False
            If txtCPF.Text = "" Then
                PreencherSemelhantes(txtNome.Text)
            End If
        End If

    End Sub
    Private Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboPerito.SelectedIndexChanged
        If Me.IsPostBack Then
            txtNome.Text = CboPerito.SelectedItem.ToString
            If CboPerito.SelectedIndex <> 0 Then
                txtCPF.Text = ""
                PreencherDadosPerito()
            End If
        End If
    End Sub

    Protected Sub BtnNova_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNova.Click
        txtAnotacao.Text = ""
        cboTipo_anotacao.SelectedIndex = 0
        lblData_Anotacao.Text = Today.ToShortDateString

        cboTipo_anotacao.Enabled = True
        txtAnotacao.Enabled = True

        Dim bal As New BALAnotacao(GetUsuario)

        If Not PermiteInclusaoDeAnotacao(False, True) Then
            BtnGravar.Enabled = False
        End If

    End Sub

    Protected Sub BtnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSair.Click
        Response.Redirect("Principal.aspx")
    End Sub

End Class