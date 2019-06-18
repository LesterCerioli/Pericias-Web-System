Imports Entidade
Imports BAL

Public Class frmSituacaoCadastralPerito
    Inherits BasePage

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLimpar.Click
        Try
            LimparFormulario()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnSair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSair.Click
        Try
            Response.Redirect("Principal.aspx")
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtCPF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCPF.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCPF.Text) And txtCPF.AutoPostBack = True Then
                Dim cpf As String = RetornaNumerosDeString(txtCPF.Text)
                If Not ValidarCPF(cpf) Then
                    MsgErro("CPF inválido")
                    txtCPF.Text = String.Empty
                    Exit Sub
                Else
                    If String.IsNullOrEmpty(txtNome.Text) And String.IsNullOrEmpty(txtNomeFantasia.Text) Then
                        BuscarPerito(0, RetornaNumerosDeString(txtCPF.Text))
                    End If
                End If
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtCNPJ_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCNPJ.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCNPJ.Text) And txtCNPJ.AutoPostBack = True Then
                Dim cnpj As String = RetornaNumerosDeString(txtCNPJ.Text)
                If Not ValidarCNPJ(cnpj) Then
                    MsgErro("CNPJ inválido")
                    txtCNPJ.Text = String.Empty
                    Exit Sub
                Else
                    If String.IsNullOrEmpty(txtNome.Text) And String.IsNullOrEmpty(txtNomeFantasia.Text) Then
                        BuscarPerito(0, RetornaNumerosDeString(txtCNPJ.Text))
                    End If
                End If
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNome.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtNome.Text) Then
                If String.IsNullOrEmpty(txtCPF.Text) And String.IsNullOrEmpty(txtCNPJ.Text) Then
                    CarregaNomeSemelhante(txtNome.Text, "NOME", CInt(ddlTipoPessoa.SelectedValue))
                End If
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtNomeFantasia_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNomeFantasia.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtNomeFantasia.Text) Then
                If String.IsNullOrEmpty(txtCPF.Text) And String.IsNullOrEmpty(txtCNPJ.Text) Then
                    CarregaNomeSemelhante(txtNomeFantasia.Text, "NOMEFANT", CInt(ddlTipoPessoa.SelectedValue))
                End If
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlNomeSemelhante_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNomeSemelhante.SelectedIndexChanged
        Try
            If ddlNomeSemelhante.SelectedIndex > -1 Then
                BuscarPerito(CLng(ddlNomeSemelhante.SelectedValue), "")
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub LimparFormulario()
        Try
            ddlTipoPessoa.SelectedIndex = 0
            ddlTipoPessoa.Enabled = True

            ddlNomeSemelhante.Enabled = False
            ddlNomeSemelhante.Items.Clear()

            txtCPF.Text = String.Empty
            txtCNPJ.Text = String.Empty
            txtNome.Text = String.Empty
            txtNomeFantasia.Text = String.Empty

            txtCPF.AutoPostBack = True
            txtCNPJ.AutoPostBack = True
            txtNome.AutoPostBack = True
            txtNomeFantasia.AutoPostBack = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaNomeSemelhante(ByVal nome As String, ByVal tipoNome As String, ByVal tipoPessoa As Integer)
        Try
            Dim balPerito As New BALPERITO(GetUsuario)
            Dim ds As DataSet = balPerito.ExibirTodosDadosSet(tipoNome, nome, 0, tipoPessoa)

            If ds.Tables(0).Rows.Count > 0 Then
                ddlNomeSemelhante.Items.Clear()
                ddlNomeSemelhante.DataValueField = "COD_PERITO"
                ddlNomeSemelhante.DataTextField = IIf(tipoNome = "NOME", "Nome", "Nome_Fant")
                ds.Tables(0).DefaultView.RowFilter = "COD_PERITO IS NOT NULL"
                ddlNomeSemelhante.DataSource = ds.Tables(0).DefaultView
                ddlNomeSemelhante.DataBind()
                ddlNomeSemelhante.Items.Insert(0, New ListItem("Opcional -> Clique aqui para escolher nomes semelhantes", "0"))
                ddlNomeSemelhante.SelectedIndex = 0
                ddlNomeSemelhante.Enabled = True
            Else
                MsgErro("Perito não cadastrado.")
                Exit Sub
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BuscarPerito(Optional ByVal codPerito As Long = 0, _
                             Optional ByVal cpf_cnpj As String = "")
        Try
            Dim balPerito As New BALPERITO(GetUsuario)
            Dim ds As DataSet = Nothing
            Dim perito As EntPERITO = Nothing
            Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)

            If Not String.IsNullOrEmpty(cpf_cnpj) Then
                perito = balPerito.ExibirDadosCadPeritoEnt(IIf(tipoPessoa = EntPERITO.Pessoa.Fisica, "CPF", "CNPJ"), cpf_cnpj, "S", tipoPessoa)

                If perito Is Nothing Then
                    If tipoPessoa = EntPERITO.Pessoa.Fisica Then
                        MsgErro("Perito não cadastrado.")
                        Exit Sub
                    ElseIf tipoPessoa = EntPERITO.Pessoa.Juridica Then
                        MsgErro("CNPJ não cadastrado.")
                        Exit Sub
                    End If
                End If
            ElseIf codPerito <> 0 Then
                perito = balPerito.ExibirDadosCadPeritoEnt("CODIGO", codPerito, "S", tipoPessoa)
            End If

            txtCPF.AutoPostBack = False
            txtCNPJ.AutoPostBack = False
            txtNome.AutoPostBack = False
            txtNomeFantasia.AutoPostBack = False

            ddlNomeSemelhante.Enabled = False
            ddlTipoPessoa.Enabled = False
            txtCPF.Text = perito.CPF
            txtCNPJ.Text = perito.CNPJ
            txtNome.Text = perito.Nome
            txtNomeFantasia.Text = perito.NomeFantasia

            Dim script As String = String.Format("AbrirJanelaPopUp('frmDetalhesPerito.aspx', 'codPerito={0},tipoPessoa={1}')", perito.Cod_Perito.ToString, perito.TipoPessoa.ToString)

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "RedirecionarPagina", script, True)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region


End Class