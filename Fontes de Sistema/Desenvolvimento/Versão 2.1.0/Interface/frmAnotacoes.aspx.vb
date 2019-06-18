Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports log4net

Partial Public Class FrmAnotacoes
    Inherits BasePage
    Private peritoGlobal As New EntPERITO
    Private entAnot As New EntAnotacao
    Dim i, j As Integer
    Dim m_Sigla As String
    Dim logger As log4net.ILog


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            BtnExcluir.Enabled = False
            BtnGravar.Enabled = False
            PreencherTipo_Anotacao()
            PreencherCombo_Status()
            lblData_Anotacao.Text = Today.ToShortDateString
            BtnExcluir.Visible = True
            BtnLimpar.Visible = True
            BtnExcluir.Attributes.Add("OnClick", "return confirm('Confirma a Exclusão?');")
        End If
    End Sub

    'Tipo_Anotacao
    Private Sub PreencherTipo_Anotacao()
        Try
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
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
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

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Try
            Dim balPer As New BALPERITO(GetUsuario)
            Dim balAnot As New BALAnotacao(GetUsuario)
            Dim msgSucesso As String
            Dim perito As EntPERITO = Nothing
            Dim anotacao As New EntAnotacao
            Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)

            Dim tipoAnotacao As Integer = Nothing

            If txtCodPerito.Value = String.Empty Or CLng(txtCodPerito.Value) = 0 Then
                MsgErro("Perito não cadastrado.")
                Exit Sub
            End If

            perito = balPer.ExibirDadosCadPeritoEnt("CODIGO", txtCodPerito.Value, "S", tipoPessoa)

            If perito Is Nothing Then
                MsgErro("Perito não cadastrado.")
                Exit Sub
            End If

            If PermiteInclusaoDeAnotacao(True, False) Then
                tipoAnotacao = CInt(cboTipo_anotacao.SelectedValue)

                'Aqui ficará a verificação de Permissionamento da ação'
                '*****************************************************'
                '*****************************************************'

                If cboTipo_anotacao.SelectedIndex = 0 Then
                    MsgErro("Gravação Rejeitada. Não foi selecionado o Tipo de Anotação.")
                    Exit Sub
                End If

                If String.IsNullOrEmpty(txtAnotacao.Text) Then
                    MsgErro("Gravação Rejeitada. Favor informar a justificativa da anotação.")
                    Exit Sub
                End If

                If tipoPessoa = EntPERITO.Pessoa.Fisica Then
                    If String.IsNullOrEmpty(txtCPF.Text) Or Not ValidarCPF(RetornaNumerosDeString(txtCPF.Text)) Then
                        MsgErro("CPF ou CNPJ inválido.")
                        Exit Sub
                    End If
                Else
                    If String.IsNullOrEmpty(txtCNPJ.Text) Or Not ValidarCNPJ(RetornaNumerosDeString(txtCNPJ.Text)) Then
                        MsgErro("CPF ou CNPJ inválido.")
                        Exit Sub
                    End If
                End If

                '*Verifica as permissões para EXCLUIR e REATIVAR PERITO
                If tipoAnotacao = 1 Then
                    If Not VerificaAutorizacaoUsuario(NomePagina, "btnGravar", "EXCLPER") Then
                        MsgErro("Somente pessoas autorizadas podem realizar anotação do tipo exclusão ou reativação.")
                        Exit Sub
                    End If

                    '* Aqui é verificado 2 situações:
                    '* Se o perito já está EXCLUIDO o mesmo não pode receber anotação de EXCLUSÃO
                    '* Se o perito não está EXCLUIDO o mesmo não pode receber anotação de REATIVAÇÃO
                    If perito.StatusAtual.Codigo = 2 Then
                        MsgErro("Não é possível realizar uma anotação de exclusão para um perito que já está com status Excluído.")
                        Exit Sub
                    End If
                End If
                '*Verifica as permissões para EXCLUIR e REATIVAR PERITO
                If tipoAnotacao = 5 Then
                    If Not VerificaAutorizacaoUsuario(NomePagina, "btnGravar", "REATPER") Then
                        MsgErro("Somente pessoas autorizadas podem realizar anotação do tipo exclusão ou reativação.")
                        Exit Sub
                    End If

                    '* Aqui é verificado 2 situações:
                    '* Se o perito já está EXCLUIDO o mesmo não pode receber anotação de EXCLUSÃO
                    '* Se o perito não está EXCLUIDO o mesmo não pode receber anotação de REATIVAÇÃO
                    If perito.StatusAtual.Codigo <> 2 Then
                        MsgErro("Não é possível realizar uma anotação de reativação para um perito que não esteja com status Excluído.")
                        Exit Sub
                    End If
                End If

                anotacao.CodigoPerito = perito.Cod_Perito
                anotacao.DATA_ANOTACAO = CDate(lblData_Anotacao.Text)
                anotacao.DATA_EXCLUSAO = Nothing
                anotacao.DESCR_ANOTACAO = txtAnotacao.Text
                anotacao.SIGLA = GetUsuario.Login
                anotacao.Cod_Tipo_Anotacao = CInt(cboTipo_anotacao.SelectedValue)

                If tipoAnotacao = 1 Then
                    balPer.Excluir(perito, anotacao.SIGLA)
                ElseIf tipoAnotacao = 5 Then
                    balPer.Reativar(perito, anotacao.SIGLA)
                End If

                If balAnot.Gravar(anotacao) Then
                    msgSucesso = "Anotação gravada com sucesso."
                Else
                    Throw New Exception("Erro ao tentar gravar anotação.")
                End If

                PermiteInclusaoDeAnotacao(False, True)

                LimpaAnotacao()

                HabilitaComponentesBusca(True)

                PreencherAnotacoes(perito.Cod_Perito)

                MsgErro(msgSucesso, "Sucesso")
            End If

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try

    End Sub

    Private Sub LimpaAnotacao()
        Try
            cboTipo_anotacao.SelectedIndex = 0
            txtAnotacao.Text = ""
            lblData_Anotacao.Text = Today.ToShortDateString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
        Try
            Dim Bal As New BALAnotacao(GetUsuario)
            Dim anotacao As EntAnotacao = Nothing
            Dim Resultado As Boolean

            If Not VerificaAutorizacaoUsuario(NomePagina, "PAGINA", "EXCLANOT") Then
                MsgErro("Somente pessoas autorizadas podem excluir uma anotação.")
                Exit Sub
            End If

            If txtCodPerito.Value = "" Then
                MsgErro("Exclusão não efetuada. Verifique o código do perito.")
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

                anotacao = Bal.ConsultarAnotacao(CLng(cboTipo_anotacao.SelectedValue), lblData_Anotacao.Text, CLng(txtCodPerito.Value))

                If anotacao.Cod_Tipo_Anotacao = 1 Then
                    MsgErro("Não é possível excluir uma anotação que seja do tipo Exclusão.")
                    Exit Sub
                ElseIf anotacao.Cod_Tipo_Anotacao = 5 Then
                    MsgErro("Não é possível excluir uma anotação que seja do tipo Reativação.")
                    Exit Sub
                End If

                Resultado = Bal.Excluir(CLng(txtCodPerito.Value), CDate(lblData_Anotacao.Text))

                If Resultado Then
                    MsgErro("Anotação excluída com sucesso.")
                    LimpaAnotacao()
                End If
            End If

            PreencherAnotacoes(CLng(txtCodPerito.Value))
            PermiteInclusaoDeAnotacao(False, True)
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
        
        
    End Sub

    Private Sub PreencherSemelhantes(ByVal m_Nome As String, ByVal tipo As String)
        Try

            Dim bal As New BALPERITO(GetUsuario)
            Dim dsfila As New DataSet
            If m_Nome = "" Then Exit Sub
            CboPerito.Items.Clear()
            dsfila = bal.ExibirTodosDadosSet(tipo, m_Nome, 0, CInt(ddlTipoPessoa.SelectedValue)) 'Serão exibidos os excluídos
            CboPerito.DataTextField = CStr(IIf(tipo = "NOME", "Nome", "Nome_Fant"))
            CboPerito.DataValueField = "Cod_Perito"
            dsfila.Tables(0).DefaultView.RowFilter = "COD_PERITO IS NOT NULL"
            CboPerito.DataSource = dsfila.Tables(0).DefaultView '.DefaultView
            CboPerito.DataBind()
            CboPerito.Items.Insert(0, "Opcional -> Clique aqui para escolher nomes semelhantes")
            CboPerito.SelectedIndex = 0
            CboPerito.Enabled = True
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try

    End Sub

    Private Sub PreencherDadosPerito(Optional ByVal selecaoNomeSemelhante As Boolean = False)

        Try
            Dim Bal As New BALPERITO(GetUsuario)
            Dim BalPerNur As New BalPerito_Comarca(GetUsuario)
            Dim tipoPessoa As Integer
            Dim cpf As String
            Dim cnpj As String


            If ddlTipoPessoa.SelectedIndex = 0 Then
                If CboPerito.SelectedValue = "Opcional -> Clique aqui para escolher nomes semelhantes" And txtCPF.Text = "" Then
                    Exit Sub
                End If
            ElseIf ddlTipoPessoa.SelectedIndex = 1 Then
                If CboPerito.SelectedValue = "Opcional -> Clique aqui para escolher nomes semelhantes" And txtCNPJ.Text = "" Then
                    Exit Sub
                End If
            End If

            HabilitaComponentesBusca(False)

            tipoPessoa = CInt(ddlTipoPessoa.SelectedValue)
            cpf = CStr(IIf(String.IsNullOrEmpty(txtCPF.Text), Nothing, RetornaNumerosDeString(txtCPF.Text)))
            cnpj = CStr(IIf(String.IsNullOrEmpty(txtCNPJ.Text), Nothing, RetornaNumerosDeString(txtCNPJ.Text)))

            If selecaoNomeSemelhante Then
                peritoGlobal = Bal.ExibirDadosCadPeritoEnt("CODIGO", CStr(CboPerito.SelectedValue), "S", tipoPessoa)
            Else
                Select Case tipoPessoa
                    Case EntPERITO.Pessoa.Fisica
                        If txtNome.Text <> "" And txtCPF.Text = "" Then
                            peritoGlobal = Bal.ExibirDadosCadPeritoEnt("NOMEEXATO", txtNome.Text, "S", tipoPessoa)
                        ElseIf txtCPF.Text <> "" And txtNome.Text = "" Then
                            peritoGlobal = Bal.ExibirDadosCadPeritoEnt("CPF", cpf, "S", tipoPessoa)
                        ElseIf txtCPF.Text <> "" And txtNome.Text <> "" Then
                            peritoGlobal = Bal.ExibirDadosCadPeritoEnt("CPF", cpf, "S", tipoPessoa)
                        Else
                            Exit Sub
                        End If

                    Case EntPERITO.Pessoa.Juridica
                        If txtNome.Text <> "" And txtCNPJ.Text = "" Then
                            peritoGlobal = Bal.ExibirDadosCadPeritoEnt("NOMEEXATO", txtNome.Text, "S", tipoPessoa)
                        ElseIf Not String.IsNullOrEmpty(txtNomeFantasia.Text) And txtCNPJ.Text = "" Then
                            peritoGlobal = Bal.ExibirDadosCadPeritoEnt("NOMEFANT", txtNomeFantasia.Text, "S", tipoPessoa)
                        ElseIf txtCNPJ.Text <> "" And txtNome.Text = "" Then
                            peritoGlobal = Bal.ExibirDadosCadPeritoEnt("CNPJ", cnpj, "S", tipoPessoa)
                        ElseIf txtCNPJ.Text <> "" And txtNome.Text <> "" Then
                            peritoGlobal = Bal.ExibirDadosCadPeritoEnt("CNPJ", cnpj, "S", tipoPessoa)
                        Else
                            Exit Sub
                        End If

                    Case Else
                        Exit Sub
                End Select
            End If

            If Not peritoGlobal Is Nothing Then
                If peritoGlobal.Cod_Perito = Nothing Or peritoGlobal.Cod_Perito = 0 Then Exit Sub

                txtCodPerito.Value = peritoGlobal.Cod_Perito.ToString
                txtCPF.Text = peritoGlobal.CPF
                txtCNPJ.Text = peritoGlobal.CNPJ
                txtNome.Text = peritoGlobal.Nome
                txtNomeFantasia.Text = peritoGlobal.NomeFantasia
                If CDate(peritoGlobal.Dt_Nasc) = Nothing Then
                    txtDt_Nasc.Text = String.Empty
                Else
                    txtDt_Nasc.Text = CStr(Format(peritoGlobal.Dt_Nasc, "dd/MM/yyyy"))
                End If
                ddlStatusPerito.SelectedValue = CStr(peritoGlobal.StatusAtual.Codigo)
                ddlTipoPessoa.Enabled = False
                CboPerito.Enabled = False

                If Not CDate(entAnot.DATA_ANOTACAO) = Nothing Then
                    lblData_Anotacao.Text = CStr(Format(entAnot.DATA_ANOTACAO, "dd/MM/yyyy"))
                Else
                    lblData_Anotacao.Text = FormatDateTime(Date.Now, DateFormat.ShortDate).ToString()
                End If

                PreencherAnotacoes(peritoGlobal.Cod_Perito)

                PermiteInclusaoDeAnotacao(False, True)

                HabilitaComponentesBusca(False)

            Else
                HabilitaComponentesBusca(True)

                MsgErro("O Perito não foi localizado")
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try

    End Sub

    Private Sub HabilitaComponentesBusca(ByVal valor As Boolean)
        txtNome.AutoPostBack = valor
        txtNomeFantasia.AutoPostBack = valor
        txtCPF.AutoPostBack = valor
        txtCNPJ.AutoPostBack = valor
    End Sub

    Private Sub PreencherAnotacoes(ByVal ncod_perito As Long)
        Try
            Dim Ds As DataSet
            Dim BalAnot As New BALAnotacao(GetUsuario)
            Ds = BalAnot.ExibirAnotPer(ncod_perito)
            GrdAnotacoes.DataSource = Ds.Tables(0)
            GrdAnotacoes.DataBind()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnGrdVisualizar_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            Dim bal As New BALAnotacao(GetUsuario)
            Dim ent As EntAnotacao

            Dim Vetor(2) As String
            For i As Integer = 0 To 2
                Vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
            Next

            ent = bal.ConsultarAnotacao(CLng(Vetor(0)), Vetor(1), CLng(Vetor(2)))

            If Not ent Is Nothing Then
                cboTipo_anotacao.SelectedValue = ent.Cod_Tipo_Anotacao.ToString()
                txtAnotacao.Text = ent.DESCR_ANOTACAO.ToString()

                lblData_Anotacao.Text = IIf(CDate(ent.DATA_ANOTACAO) <> Nothing _
                                            , ent.DATA_ANOTACAO.ToShortDateString(), "").ToString()


                BtnExcluir.Enabled = True
                cboTipo_anotacao.Enabled = False
                txtAnotacao.Enabled = False
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub Limpar()
        Try
            txtCPF.Text = String.Empty
            txtCNPJ.Text = String.Empty
            ddlTipoPessoa.Enabled = True
            ddlTipoPessoa.SelectedIndex = 0
            ddlStatusPerito.SelectedIndex = -1
            txtNome.Text = String.Empty
            txtNomeFantasia.Text = String.Empty
            txtDt_Nasc.Text = String.Empty
            txtAnotacao.Text = String.Empty
            Session("Num_Nur") = 0
            txtCodPerito.Value = String.Empty
            txtNome.AutoPostBack = True
            txtCPF.AutoPostBack = True
            txtCNPJ.AutoPostBack = True
            txtNomeFantasia.AutoPostBack = True
            CboPerito.Items.Clear()
            GrdAnotacoes.DataSource = Nothing
            GrdAnotacoes.DataBind()
            cboTipo_anotacao.SelectedIndex = 0
            CboPerito.Enabled = False
            lblData_Anotacao.Text = Today.ToShortDateString
            BtnExcluir.Enabled = False
            BtnGravar.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function PermiteInclusaoDeAnotacao(ByVal bMensagem As Boolean, ByVal bHabilitaControles As Boolean) As Boolean
        Try
            If txtCodPerito.Value = String.Empty Then
                Exit Function
            End If

            Dim bal As New BALAnotacao(GetUsuario)

            If bal.VerificaSeJaFoiIncluidoAnotacaoNoDia(CLng(txtCodPerito.Value)) = 1 Then
                If bMensagem Then
                    MsgErro("Só é permitido uma inclusão de anotação por dia.")
                End If

                If bHabilitaControles Then
                    BtnNova.Enabled = False
                    cboTipo_anotacao.Enabled = False
                    txtAnotacao.Enabled = False
                End If

                Return False
            Else
                If bHabilitaControles Then
                    BtnNova.Enabled = True
                    cboTipo_anotacao.Enabled = True
                    txtAnotacao.Enabled = True
                    BtnGravar.Enabled = True
                End If

                Return True
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Function

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Try
            Limpar()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCPF.TextChanged
        Try
            If txtCPF.Text <> "" Then
                Dim cpf As String
                cpf = RetornaNumerosDeString(txtCPF.Text)
                If Not ValidarCPF(cpf) Then
                    MsgErro("CPF Inválido")
                    txtCPF.Text = String.Empty
                    Exit Sub
                End If

                If txtNome.Text = String.Empty Then
                    PreencherDadosPerito()
                End If
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtCNPJ_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCNPJ.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCNPJ.Text) Then
                Dim cnpj As String = RetornaNumerosDeString(txtCNPJ.Text)
                If Not ValidarCNPJ(cnpj) Then
                    MsgErro("CNPJ inválido")
                    txtCNPJ.Text = String.Empty
                    Exit Sub
                End If

                If txtNome.Text = String.Empty And txtNomeFantasia.Text = String.Empty Then
                    PreencherDadosPerito()
                End If
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNome.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtNome.Text) Then
                If txtCPF.Text = String.Empty And txtCNPJ.Text = String.Empty Then
                    PreencherSemelhantes(txtNome.Text, "NOME")
                End If
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtNomeFantasia_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNomeFantasia.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtNomeFantasia.Text) Then
                If txtCPF.Text = String.Empty And txtCNPJ.Text = String.Empty Then
                    PreencherSemelhantes(txtNomeFantasia.Text, "NOMEFANT")
                End If
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboPerito.SelectedIndexChanged
        Try

            If CboPerito.SelectedIndex <> 0 Then
                txtCPF.Text = String.Empty
                txtCNPJ.Text = String.Empty
                PreencherDadosPerito(True)
            End If

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub


    Protected Sub BtnNova_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNova.Click
        Try
            Dim bal As New BALAnotacao(GetUsuario)

            txtAnotacao.Text = ""
            cboTipo_anotacao.SelectedIndex = 0
            lblData_Anotacao.Text = Today.ToShortDateString

            cboTipo_anotacao.Enabled = True
            txtAnotacao.Enabled = True

            If Not PermiteInclusaoDeAnotacao(False, True) Then
                BtnGravar.Enabled = False
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub BtnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSair.Click
        Response.Redirect("Principal.aspx")
    End Sub

End Class