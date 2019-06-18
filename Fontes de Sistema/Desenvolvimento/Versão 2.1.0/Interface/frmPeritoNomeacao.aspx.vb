Imports Entidade
Imports BAL

Public Class frmPeritoNomeacao
    Inherits BasePage

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                CarregaPagina()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlNomeSemelhante_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNomeSemelhante.SelectedIndexChanged
        Try
            If ddlNomeSemelhante.SelectedIndex > -1 Then
                BuscarPeritos(CLng(ddlNomeSemelhante.SelectedValue), "", 0, 0)
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProfissao.SelectedIndexChanged
        Try
            If ddlProfissao.SelectedIndex > 0 Then
                CarregaEspecialidades(0, CInt(ddlProfissao.SelectedValue))
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEspecialidade.SelectedIndexChanged
        Try
            If ddlEspecialidade.SelectedIndex > -1 Then
                If String.IsNullOrEmpty(CStr(ddlComarca.SelectedValue)) Then
                    ddlComarca.SelectedValue = -1
                End If
                BuscarPeritos(0, "", CLng(IIf(ddlEspecialidade.SelectedIndex = -1, 0, ddlEspecialidade.SelectedValue)), CLng(IIf(ddlComarca.SelectedIndex = -1, 0, ddlComarca.SelectedValue)))
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlNur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNur.SelectedIndexChanged
        Try
            If ddlNur.SelectedIndex > 0 Then
                CarregaComarcas(0, CInt(ddlNur.SelectedValue))
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlComarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlComarca.SelectedIndexChanged
        Try
            If ddlComarca.SelectedIndex > -1 Then
                If String.IsNullOrEmpty(CStr(ddlEspecialidade.SelectedValue)) Then
                    ddlEspecialidade.SelectedValue = -1
                End If
                BuscarPeritos(0, "", CLng(IIf(ddlEspecialidade.SelectedIndex = -1, 0, ddlEspecialidade.SelectedValue)), CLng(IIf(ddlComarca.SelectedIndex = -1, 0, ddlComarca.SelectedValue)))
            End If

            up1.Update()
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
                        BuscarPeritos(0, RetornaNumerosDeString(txtCPF.Text), 0, 0)
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
                        BuscarPeritos(0, RetornaNumerosDeString(txtCNPJ.Text), 0, 0)
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

#End Region


#Region "Funções"

    Private Sub LimparFormulario()
        Try
            ddlTipoPessoa.SelectedIndex = 0
            ddlTipoPessoa.Enabled = True
            CarregaProfissoes()

            CarregaNURs()
            ddlEspecialidade.Items.Clear()
            ddlComarca.Items.Clear()
            ddlNomeSemelhante.Enabled = False
            ddlNomeSemelhante.Items.Clear()
            txtCPF.Text = String.Empty
            txtCNPJ.Text = String.Empty
            txtNome.Text = String.Empty
            txtNomeFantasia.Text = String.Empty

            ddlNur.AutoPostBack = True
            ddlEspecialidade.AutoPostBack = True
            ddlComarca.AutoPostBack = True
            txtCPF.AutoPostBack = True
            txtCNPJ.AutoPostBack = True
            txtNome.AutoPostBack = True
            txtNomeFantasia.AutoPostBack = True
            ddlProfissao.AutoPostBack = True

            'grdPeritos.DataSource = Nothing
            'grdPeritos.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaPagina()
        Try
            CarregaProfissoes()
            CarregaNURs()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CarregaProfissoes(Optional ByVal codPerito As Long = 0)
        Try
            Dim balProfissao As New BALProfissao(GetUsuario)
            Dim balEspecialidadePerito As New BALEspecialidadePerito(GetUsuario)
            Dim ds As DataSet = Nothing

            ddlProfissao.Items.Clear()
            ddlProfissao.DataTextField = "Descr_Profissao"
            ddlProfissao.DataValueField = "Cod_Profissao"

            If codPerito = 0 Then
                ddlProfissao.DataSource = balProfissao.ExibirDados
            Else
                ddlProfissao.DataSource = balEspecialidadePerito.ListarEspecialidadesPerito(codPerito)
                ddlProfissao.AutoPostBack = False
            End If

            ddlProfissao.DataBind()

            If codPerito = 0 Then
                ddlProfissao.Items.Insert(0, New ListItem("Selecione uma Profissão", "0"))
            End If

            If ddlProfissao.Items.Count > 0 Then
                ddlProfissao.SelectedIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaNURs(Optional ByVal codPerito As Long = 0)
        Try
            Dim balNurc As New BALNURC(GetUsuario)

            ddlNur.Items.Clear()
            ddlNur.DataTextField = "DescrRes"
            ddlNur.DataValueField = "CodNurc"

            If codPerito = 0 Then
                ddlNur.DataSource = balNurc.ListarTodasNurcs
            Else
                ddlNur.DataSource = balNurc.ListarNurcsPerito(codPerito)
                ddlNur.AutoPostBack = False
            End If

            ddlNur.DataBind()

            If codPerito = 0 Then
                ddlNur.Items.Insert(0, New ListItem("Selecione um Nur", "0"))
            End If

            If ddlNur.Items.Count > 0 Then
                ddlNur.SelectedIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaEspecialidades(Optional ByVal codPerito As Long = 0, Optional codProfissao As Integer = 0)
        Try
            Dim balEspecialidade As New BALEspecialidade(GetUsuario)
            Dim balEspecialidadePerito As New BALEspecialidadePerito(GetUsuario)

            ddlEspecialidade.Items.Clear()
            ddlEspecialidade.DataTextField = "Descr_Especialidade"
            ddlEspecialidade.DataValueField = "Cod_Especialidade"

            If codPerito = 0 Then
                ddlEspecialidade.DataSource = balEspecialidade.ExibirDados(codProfissao)
            Else
                ddlEspecialidade.DataSource = balEspecialidadePerito.ListarEspecialidadesPerito(codPerito)
                ddlEspecialidade.AutoPostBack = False
            End If

            ddlEspecialidade.DataBind()

            If codPerito = 0 Then
                ddlEspecialidade.Items.Insert(0, New ListItem("Todas as Especialidades", "0"))
            End If

            If ddlEspecialidade.Items.Count > 0 Then
                ddlEspecialidade.SelectedIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaComarcas(Optional ByVal codPerito As Long = 0, Optional codNur As Integer = 0)
        Try
            Dim balComarca As New BALCOMARCA(GetUsuario)

            ddlComarca.Items.Clear()
            ddlComarca.DataTextField = "Nome"
            ddlComarca.DataValueField = "CodCom"

            If codPerito = 0 Then
                ddlComarca.DataSource = balComarca.ListarComarcasNurc(codNur)
            Else
                ddlComarca.DataSource = balComarca.ListarComarcasPerito(codPerito)
                ddlComarca.AutoPostBack = False
            End If

            ddlComarca.DataBind()

            If codPerito = 0 Then
                ddlComarca.Items.Insert(0, New ListItem("Todas as Comarcas", "0"))
            End If

            If ddlComarca.Items.Count > 0 Then
                ddlComarca.SelectedIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaNomeSemelhante(ByVal nome As String, ByVal tipoNome As String, ByVal tipoPessoa As Integer)
        Try
            Dim balPerito As New BALPERITO(GetUsuario)
            Dim ds As DataSet = balPerito.ExibirTodosDadosSet(tipoNome, nome, 0, tipoPessoa)

            ddlNomeSemelhante.Items.Clear()
            ddlNomeSemelhante.DataValueField = "COD_PERITO"
            ddlNomeSemelhante.DataTextField = IIf(tipoNome = "NOME", "Nome", "Nome_Fant")
            ds.Tables(0).DefaultView.RowFilter = "COD_PERITO IS NOT NULL"
            ddlNomeSemelhante.DataSource = ds.Tables(0).DefaultView
            ddlNomeSemelhante.DataBind()
            ddlNomeSemelhante.Items.Insert(0, New ListItem("Opcional -> Clique aqui para escolher nomes semelhantes", "0"))
            ddlNomeSemelhante.SelectedIndex = 0
            ddlNomeSemelhante.Enabled = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BuscarPeritos(Optional ByVal codPerito As Long = 0, _
                              Optional ByVal cpf_cnpj As String = "", _
                              Optional ByVal codEspecialidade As Long = 0, _
                              Optional ByVal codComarca As Long = 0)
        Try
            Dim balPerito As New BALPERITO(GetUsuario)
            Dim ds As DataSet = Nothing
            Dim perito As EntPERITO = Nothing
            Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)

            If codPerito <> 0 Then
                perito = balPerito.ExibirDadosCadPeritoEnt("CODIGO", codPerito, "S", tipoPessoa)
            End If

            If Not String.IsNullOrEmpty(cpf_cnpj) Then
                perito = balPerito.ExibirDadosCadPeritoEnt(IIf(tipoPessoa = EntPERITO.Pessoa.Fisica, "CPF", "CNPJ"), cpf_cnpj, "S", tipoPessoa)

                If perito Is Nothing Then
                    If tipoPessoa = EntPERITO.Pessoa.Fisica Then
                        MsgErro("Perito não cadastrado")
                        Exit Sub
                    Else
                        MsgErro("CNPJ não cadastrado")
                        Exit Sub
                    End If
                End If

                'codPerito = perito.Cod_Perito
            End If

            ds = balPerito.ListarPeritosNomeacao(codPerito, cpf_cnpj, codEspecialidade, codComarca, tipoPessoa)

            If ds.Tables(0).Rows.Count = 0 Then
                If codPerito <> 0 Then
                    MsgErro("Este perito não está em condições de nomeação. Verifique a situação cadastral do perito.")
                    Exit Sub
                Else
                    MsgErro("Não há nenhum perito em condições de nomeação que atenda aos parâmetros informados.")
                    Exit Sub
                End If
            End If

            GerarTabela(ds.Tables(0))

            'grdPeritos.DataSource = ds.Tables(0)
            'grdPeritos.DataBind()

            If Not perito Is Nothing Then
                If perito.Cod_Perito <> 0 Then
                    CarregaProfissoes(codPerito)
                    CarregaEspecialidades(codPerito)
                    CarregaNURs(codPerito)
                    CarregaComarcas(codPerito)

                    ddlTipoPessoa.Enabled = False
                    ddlNomeSemelhante.Enabled = False

                    txtCNPJ.AutoPostBack = False
                    txtCPF.AutoPostBack = False
                    txtNome.AutoPostBack = False
                    txtNomeFantasia.AutoPostBack = False

                    txtCNPJ.Text = perito.CNPJ
                    txtCPF.Text = perito.CPF
                    txtNome.Text = perito.Nome
                    txtNomeFantasia.Text = perito.NomeFantasia

                    If tipoPessoa = EntPERITO.Pessoa.Juridica Then
                        linhaPessoaJuridica.Attributes.Add("display", "block")
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GerarTabela(ByVal dt As DataTable)
        For Each linha As DataRow In dt.Rows
            Dim linhaTabelaPerito As New HtmlTableRow
            Dim colunaPerito1 As New HtmlTableCell
            Dim colunaPerito2 As New HtmlTableCell
            Dim colunaPerito3 As New HtmlTableCell
            Dim colunaPerito4 As New HtmlTableCell
            Dim colunaPerito5 As New HtmlTableCell

            Dim imgCollapsable As New Image
            imgCollapsable.ID = String.Format("imgCollapsable_{0}", CStr(linha(0)))
            Dim lblNomePerito As New Label
            lblNomePerito.ID = String.Format("lblNomePerito_{0}", CStr(linha(0)))
            Dim lblCpfCnpj As New Label
            lblCpfCnpj.ID = String.Format("lblCpfCnpj_{0}", CStr(linha(0)))
            Dim hfCodPerito As New HiddenField
            hfCodPerito.ID = String.Format("hfCodPerito_{0}", CStr(linha(0)))
            Dim hlDetalhePerito As New HyperLink
            hlDetalhePerito.ID = String.Format("hlDetalhePerito_{0}", CStr(linha(0)))

            imgCollapsable.ImageUrl = "~/Imagens/iconmonstr-arrow-69-24.png"
            imgCollapsable.Attributes.Add("OnClick", String.Format("Expandir_Retrair_Detalhes_Grid('{0}', '{1}')", CStr(linha(0)), imgCollapsable.ID))
            imgCollapsable.Attributes.Add("estado", "retraido")
            hfCodPerito.Value = CStr(linha(0))
            lblNomePerito.Text = CStr(linha(1))
            lblCpfCnpj.Text = CStr(linha(2))
            hlDetalhePerito.Text = "Detalhes do perito"
            'hlDetalhePerito.NavigateUrl = String.Format("frmDetalhesPerito.aspx?codPerito={0}&tipoPessoa={1}", CStr(linha(0)), CStr(linha(3)))
            hlDetalhePerito.Attributes.Add("OnClick", String.Format("AbrirJanelaPopUp('frmDetalhesPerito.aspx', 'codPerito={0},tipoPessoa={1}')", CStr(linha(0)), CStr(linha(3))))
            hlDetalhePerito.NavigateUrl = "#"
            'hlDetalhePerito.Target = "_blank"

            colunaPerito1.Controls.Add(imgCollapsable)
            colunaPerito2.Controls.Add(lblNomePerito)
            colunaPerito3.Controls.Add(lblCpfCnpj)
            colunaPerito4.Controls.Add(hlDetalhePerito)
            colunaPerito5.Controls.Add(hfCodPerito)

            linhaTabelaPerito.Cells.Add(colunaPerito1)
            linhaTabelaPerito.Cells.Add(colunaPerito2)
            linhaTabelaPerito.Cells.Add(colunaPerito3)
            linhaTabelaPerito.Cells.Add(colunaPerito4)
            linhaTabelaPerito.Cells.Add(colunaPerito5)

            linhaTabelaPerito.BgColor = "#DAD4D4"
            linhaTabelaPerito.Attributes.CssStyle.Add("border", "2px solid white")

            tblPeritos.Rows.Add(linhaTabelaPerito)

            Dim balAnotacao As New BALAnotacao(GetUsuario)
            Dim dsanotacoes As DataSet = balAnotacao.Listar_Anotacoes_Perito(CLng(linha(0)), 6)

            Dim textoAnotacao As HtmlGenericControl
            Dim colunaAnotacao1 As New HtmlTableCell
            Dim linhaTabelaAnotacao As New HtmlTableRow

            If dsanotacoes.Tables(0).Rows.Count > 0 Then
                For Each linhaAnotacoes As DataRow In dsanotacoes.Tables(0).Rows
                    linhaTabelaAnotacao = New HtmlTableRow
                    colunaAnotacao1 = New HtmlTableCell
                    textoAnotacao = New HtmlGenericControl
                    linhaTabelaAnotacao.ID = String.Format("lnTbl_{0}_{1}", CStr(linhaAnotacoes(0)), CStr(linhaAnotacoes(6)))
                    linhaTabelaAnotacao.Attributes.Add("pai", String.Format("lnTbl{0}", CStr(linha(0))))
                    textoAnotacao.InnerHtml = String.Format("<br /><p><label id=lblAnotacao_{0}_{1} style='margin-left: 30px;'>{2}</label></p><br />", CStr(linhaAnotacoes(0)), CStr(linhaAnotacoes(6)), CStr(linhaAnotacoes(1)))
                    colunaAnotacao1.Controls.Add(textoAnotacao)
                    colunaAnotacao1.ColSpan = "5"
                    linhaTabelaAnotacao.Cells.Add(colunaAnotacao1)
                    linhaTabelaAnotacao.Attributes.CssStyle.Add("display", "none")

                    tblPeritos.Rows.Add(linhaTabelaAnotacao)
                Next
            Else
                linhaTabelaAnotacao = New HtmlTableRow
                colunaAnotacao1 = New HtmlTableCell
                textoAnotacao = New HtmlGenericControl
                linhaTabelaAnotacao.ID = String.Format("lnTbl{0}", CStr(linha(0)))
                textoAnotacao.InnerHtml = String.Format("<br /><p><label id=lblAnotacao_{0} style='margin-left: 30px;'>Não há anotações cadastradas pare este perito.</label></p><br />", CStr(linha(0)))
                colunaAnotacao1.Controls.Add(textoAnotacao)
                colunaAnotacao1.ColSpan = "5"
                linhaTabelaAnotacao.Cells.Add(colunaAnotacao1)
                linhaTabelaAnotacao.Attributes.CssStyle.Add("display", "none")

                tblPeritos.Rows.Add(linhaTabelaAnotacao)
            End If

            tblPeritos.Visible = True
        Next
    End Sub

#End Region


End Class