Imports Entidade
Imports BAL

Public Class frmConsultaPagamentosRealizados
    Inherits BasePage

    Private filtroNome As String = "Todos"
    Private filtroCpfCnpj As String = "Todos"
    Private filtroProfissao As String = "Todos"
    Private filtroEspecialidade As String = "Todos"
    Private filtroOficio As String = "Todos"
    Private filtroProcesso As String = "Todos"
    Private filtroRelacao As String = "Todos"
    Private filtroProcPag As String = "Todos"
    Private filtroDtPgto As String = "Todos"

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                Session.Add("DetalhesPagamento", Nothing)
                Session.Add("RELAJUDACUSTOPERITO", Nothing)
                Session.Add("FILTROS", Nothing)
                CarregaPagina()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlNomeSemelhante_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNomeSemelhante.SelectedIndexChanged
        Try
            If ddlNomeSemelhante.SelectedIndex > -1 Then
                txtCodPerito.Value = ddlNomeSemelhante.SelectedValue
                BuscarAjudaCusto()
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProfissao.SelectedIndexChanged
        Try
            If ddlProfissao.SelectedIndex > 0 Then
                CarregaEspecialidades(CInt(ddlProfissao.SelectedValue))
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEspecialidade.SelectedIndexChanged
        Try
            If ddlEspecialidade.SelectedIndex > -1 Then
                BuscarAjudaCusto()
            End If

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlRelacaoPagamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRelacaoPagamento.SelectedIndexChanged
        Try
            If ddlRelacaoPagamento.SelectedIndex > 0 Then
                BuscarAjudaCusto()
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
                        BuscarAjudaCusto()
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
                        BuscarAjudaCusto()
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

    Protected Sub txtOficio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOficio.TextChanged
        Try
            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtNumeroProcesso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumeroProcesso.TextChanged
        Try
            If ValidaFormatoProcesso(txtNumeroProcesso.Text) = TipoNumeracaoProcesso.NUMERACAOCNJ Then
                Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)
                Dim ds As DataSet = balAjudaCusto.ListarProcessosAssociados(txtNumeroProcesso.Text)

                If ds.Tables(0).Rows.Count > 0 Then
                    ddlProcessosRelacionados.DataTextField = "COD_PROC"
                    ddlProcessosRelacionados.DataValueField = "ID_PROC"
                    ddlProcessosRelacionados.DataSource = ds.Tables(0)
                    ddlProcessosRelacionados.DataBind()

                    ddlProcessosRelacionados.Items.Insert(0, "Selecione um processo associado...")
                    ddlProcessosRelacionados.Enabled = True
                End If

                up1.Update()
                Exit Sub
            Else
                ddlProcessosRelacionados.Items.Clear()
                ddlProcessosRelacionados.Enabled = False
            End If

            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlProcessosRelacionados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProcessosRelacionados.SelectedIndexChanged
        Try
            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtProcessoPagamento_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProcessoPagamento.TextChanged
        Try
            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtDataProtocoloInicial_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataProtocoloInicial.TextChanged
        Try
            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtDataProtocoloFinal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataProtocoloFinal.TextChanged
        Try
            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtDataPagamentoInicial_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataPagamentoInicial.TextChanged
        Try
            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtDataPagamentoFinal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataPagamentoFinal.TextChanged
        Try
            BuscarAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click
        Try
            If Not Session("RELAJUDACUSTOPERITO") Is Nothing Then
                Dim listaFiltros As List(Of String) = DirectCast(Session("FILTROS"), List(Of String))
                Dim pagina As String = String.Format("frmRelatorio.aspx?tipoRelatorio=1&filtroNome={0}&filtroCpfCnpj={1}&filtroProfissao={2}&filtroEspecialidade={3}&filtroOficio={4}&filtroProcesso={5}&filtroRelacao={6}&filtroProcPag={7}&filtroDtPgto={8}", listaFiltros(0), listaFiltros(1), listaFiltros(2), listaFiltros(3), listaFiltros(4), listaFiltros(5), listaFiltros(6), listaFiltros(7), listaFiltros(8))

                ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "RedirecionaRelatorio", "window.open('" + pagina + "','janela','width=1000, height=700, top=100, left=110');", True)


                ScriptManager.RegisterStartupScript(Me, Me.GetType, "RedirecionaRelatorio", "window.open('" + pagina + "', '_blank');", True)
                
            Else
                MsgErro("Erro ao tentar gerar o relatório.")
                Exit Sub
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLimpar.Click
        Try
            LimparFormulario()

            BtnImprimir.Enabled = False
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

    Protected Sub btnVisualizarDetalhes_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            Dim idAjudaCusto As Long = 0
            Dim ajudaCusto As AjudaCusto = Nothing
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)

            Dim argumentos() As String = e.CommandArgument.ToString.Split(CChar(","))
            idAjudaCusto = CLng(IIf(String.IsNullOrEmpty(argumentos(0)), 0, argumentos(0)))

            ajudaCusto = balAjudaCusto.Carregar(idAjudaCusto)

            Session("DetalhesPagamento") = ajudaCusto

            Dim script As String = "AbrirPopUp('frmDetalhesPagamentosRealizados.aspx', '800', '600', '', '')"
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "RedirecionarPagina", script, True)

            up1.Update()

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub grdAjudaCusto_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles grdAjudaCusto.PageIndexChanging
        Try
            Dim ds As DataSet = DirectCast(Session("RELAJUDACUSTOPERITO"), DataSet)
            grdAjudaCusto.DataSource = ds.Tables(0)
            grdAjudaCusto.PageIndex = e.NewPageIndex
            grdAjudaCusto.DataBind()

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

#End Region


#Region "Funções"

    Private Sub LimparFormulario()
        Try
            Session("DetalhesPagamento") = Nothing
            Session("RELAJUDACUSTOPERITO") = Nothing
            Session("FILTROS") = Nothing

            ddlTipoPessoa.SelectedIndex = 0
            ddlTipoPessoa.Enabled = True
            CarregaProfissoes()

            ddlEspecialidade.Items.Clear()

            ddlRelacaoPagamento.Items.Clear()


            ddlNomeSemelhante.Enabled = False
            ddlNomeSemelhante.Items.Clear()
            txtCPF.Text = String.Empty
            txtCNPJ.Text = String.Empty
            txtNome.Text = String.Empty
            txtNomeFantasia.Text = String.Empty
            lblQntdPags.Text = String.Empty
            BtnImprimir.Enabled = False

            grdAjudaCusto.DataSource = Nothing
            grdAjudaCusto.DataBind()

            up1.Update()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaPagina()
        Try
            CarregaProfissoes()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CarregaProfissoes()
        Try
            Dim balProfissao As New BALProfissao(GetUsuario)
            Dim balEspecialidadePerito As New BALEspecialidadePerito(GetUsuario)
            Dim ds As DataSet = Nothing

            ddlProfissao.Items.Clear()
            ddlProfissao.DataTextField = "Descr_Profissao"
            ddlProfissao.DataValueField = "Cod_Profissao"
            ddlProfissao.DataSource = balProfissao.ExibirDados
            ddlProfissao.DataBind()
            ddlProfissao.Items.Insert(0, New ListItem("Selecione uma Profissão", "0"))

            If ddlProfissao.Items.Count > 0 Then
                ddlProfissao.SelectedIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaRelacoesPagamento(ByVal codPerito As Long)
        Try
            Dim balrel As New BalRelacaoPagamento(GetUsuario)

            ddlRelacaoPagamento.Items.Clear()
            ddlRelacaoPagamento.DataTextField = "NOME"
            ddlRelacaoPagamento.DataValueField = "ID"
            ddlRelacaoPagamento.DataSource = balrel.ListaRelacoesPerito(codPerito)
            ddlRelacaoPagamento.DataBind()
            ddlRelacaoPagamento.Items.Insert(0, New ListItem("Selecione uma Relação de Pagamento", "0"))

            If ddlRelacaoPagamento.Items.Count > 0 Then
                ddlRelacaoPagamento.SelectedIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaEspecialidades(Optional codProfissao As Integer = 0)
        Try
            Dim balEspecialidade As New BALEspecialidade(GetUsuario)
            Dim balEspecialidadePerito As New BALEspecialidadePerito(GetUsuario)

            ddlEspecialidade.Items.Clear()
            ddlEspecialidade.DataTextField = "Descr_Especialidade"
            ddlEspecialidade.DataValueField = "Cod_Especialidade"

            ddlEspecialidade.DataSource = balEspecialidade.ExibirDados(codProfissao)

            ddlEspecialidade.DataBind()

            ddlEspecialidade.Items.Insert(0, New ListItem("Selecione uma especialidade", "0"))

            If ddlEspecialidade.Items.Count > 0 Then
                ddlEspecialidade.SelectedIndex = 0
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

    Private Sub BuscarAjudaCusto()
        Try
            Dim balPerito As New BALPERITO(GetUsuario)
            Dim balRelPag As New BalRelacaoPagamento(GetUsuario)
            Dim balAjudCusto As New BalAjudaCusto(GetUsuario)

            Dim tipoPessoa As Integer = CInt(ddlTipoPessoa.SelectedValue)
            Dim codPerito As Long = CLng(IIf(String.IsNullOrEmpty(txtCodPerito.Value), 0, txtCodPerito.Value))
            Dim codProfissao As Long = 0
            Dim codEspecialidade As Long = 0
            Dim numeroOficio As String = txtOficio.Text
            Dim numeroProcesso As String = txtNumeroProcesso.Text
            Dim idRelacaoPagamento As Long = 0
            Dim idProcesso As Long = 0
            Dim processoPagamento As String = txtProcessoPagamento.Text
            Dim dataProtocoloInicial As String = txtDataProtocoloInicial.Text
            Dim dataProtocoloFinal As String = txtDataProtocoloFinal.Text
            Dim dataPagamentoInicial As String = txtDataPagamentoInicial.Text
            Dim dataPagamentoFinal As String = txtDataPagamentoFinal.Text
            Dim perito As EntPERITO = Nothing
            Dim CpfCnpj As String = String.Empty
            Dim tipoProcesso As Integer

            If tipoPessoa = EntPERITO.Pessoa.Fisica Then
                CpfCnpj = RetornaNumerosDeString(txtCPF.Text)

                If Not String.IsNullOrEmpty(CpfCnpj) Then
                    perito = balPerito.ExibirDadosCadPeritoEnt("CPF", CpfCnpj, "S", tipoPessoa)

                    If perito Is Nothing Then
                        MsgErro("Perito não cadastrado.")
                        Exit Sub

                        up1.Update()
                    End If

                    filtroCpfCnpj = CpfCnpj
                End If
            Else
                CpfCnpj = RetornaNumerosDeString(txtCNPJ.Text)

                If Not String.IsNullOrEmpty(CpfCnpj) Then
                    perito = balPerito.ExibirDadosCadPeritoEnt("CNPJ", CpfCnpj, "S", tipoPessoa)

                    If perito Is Nothing Then
                        MsgErro("CNPJ não cadastrado.")
                        Exit Sub

                        up1.Update()
                    End If

                    filtroCpfCnpj = CpfCnpj
                End If
            End If

            If ddlNomeSemelhante.SelectedIndex > 0 Then
                filtroNome = ddlNomeSemelhante.SelectedItem.Text
            End If

            If perito IsNot Nothing Then
                codPerito = perito.Cod_Perito
            End If

            If ddlRelacaoPagamento.SelectedIndex > 0 Then
                idRelacaoPagamento = CLng(ddlRelacaoPagamento.SelectedValue)

                filtroRelacao = ddlRelacaoPagamento.SelectedItem.Text
            End If

            If codPerito <> 0 And idRelacaoPagamento = 0 Then
                CarregaRelacoesPagamento(codPerito)
            End If

            If ddlEspecialidade.SelectedIndex > 0 Then
                codEspecialidade = CLng(ddlEspecialidade.SelectedValue)

                filtroEspecialidade = ddlEspecialidade.SelectedItem.Text
            End If

            If ddlProfissao.SelectedIndex > 0 Then
                codProfissao = CLng(ddlProfissao.SelectedValue)

                filtroProfissao = ddlProfissao.SelectedItem.Text
            End If

            If ddlProcessosRelacionados.Enabled Then
                If ddlProcessosRelacionados.SelectedIndex > 0 Then
                    idProcesso = CLng(ddlProcessosRelacionados.SelectedValue)
                Else
                    MsgErro("Favor informar qual dos processos associados a este CNJ deve ser considerado.")
                    Exit Sub

                    up1.Update()
                End If
            Else
                If Not String.IsNullOrEmpty(numeroProcesso) Then
                    idProcesso = balAjudCusto.PesquisaNumeroProcessoEprot(numeroProcesso)

                    If idProcesso = 0 Then
                        idProcesso = balAjudCusto.PesquisaCodigoProcesso(numeroProcesso, ValidaFormatoProcesso(numeroProcesso), tipoProcesso)
                    End If

                    If idProcesso = 0 Then
                        MsgErro("Processo não encontrado.")
                        Exit Sub

                        up1.Update()
                    End If
                End If
            End If

            filtroProcesso = IIf(String.IsNullOrEmpty(numeroProcesso), filtroProcesso, numeroProcesso)

            filtroProcPag = IIf(String.IsNullOrEmpty(processoPagamento), filtroProcPag, processoPagamento)

            If Not String.IsNullOrEmpty(dataPagamentoInicial) And String.IsNullOrEmpty(dataPagamentoFinal) Then
                filtroDtPgto = String.Format("Acima de {0}", dataPagamentoInicial)
            ElseIf Not String.IsNullOrEmpty(dataPagamentoInicial) And Not String.IsNullOrEmpty(dataPagamentoFinal) Then
                filtroDtPgto = String.Format("{0} até {1}", dataPagamentoInicial, dataPagamentoFinal)
            End If

            Dim ds = balAjudCusto.ConsultarPagamentosRealizados(codPerito, _
                                                                codProfissao, _
                                                                codEspecialidade, _
                                                                idRelacaoPagamento, _
                                                                idProcesso, _
                                                                processoPagamento, _
                                                                dataProtocoloInicial, _
                                                                dataProtocoloFinal, _
                                                                dataPagamentoInicial, _
                                                                dataPagamentoFinal)

            Dim listaFiltros As New List(Of String)
            listaFiltros.Add(filtroNome)
            listaFiltros.Add(filtroCpfCnpj)
            listaFiltros.Add(filtroProfissao)
            listaFiltros.Add(filtroEspecialidade)
            listaFiltros.Add(filtroOficio)
            listaFiltros.Add(filtroProcesso)
            listaFiltros.Add(filtroRelacao)
            listaFiltros.Add(filtroProcPag)
            listaFiltros.Add(filtroDtPgto)

            If ds IsNot Nothing Then
                lblQntdPags.Text = String.Format("Quantidade de pagamentos realizados: {0}", ds.Tables(0).Rows.Count.ToString)
                Session("RELAJUDACUSTOPERITO") = ds
                Session("FILTROS") = listaFiltros
                grdAjudaCusto.DataSource = ds.Tables(0)
                grdAjudaCusto.DataBind()
            Else
                grdAjudaCusto.DataSource = Nothing
                grdAjudaCusto.DataBind()
                Session("RELAJUDACUSTOPERITO") = Nothing
                Session("FILTROS") = Nothing
                lblQntdPags.Text = String.Empty
                BtnImprimir.Enabled = False
                MsgErro("Pagamento(s) não encontrado(s).")
                up1.Update()
                Exit Sub
            End If

            BtnImprimir.Enabled = True

            up1.Update()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub



#End Region


End Class