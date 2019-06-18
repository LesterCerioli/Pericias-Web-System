Imports BAL
Imports Entidade
Imports System.Web.Services

Public Class frmRelacaoPagamento
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CarregaEventosAssincronos()
            VerificaResposta()

            If Not Page.IsPostBack Then
                Session.Add("ListaAjudasCustoRelacao", Nothing)
                Session.Add("ListaAjudasCustoPerito", Nothing)
                Session.Add("ListaAjudaExclusao", Nothing)
                Session.Add("OcorrenciasPagamento", Nothing)
                Session.Add("DataTableAjudasRelacao", Nothing)
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CarregaEventosAssincronos()
        Try
            txtCpfCnpj.Attributes.Add("onblur", "javascript:setTimeout('__doPostBack(\'ctl00$Tela$txtCpfCnpj\',\'ValidaCpfCnpj\')', 0)")
            txtNumeroProcesso.Attributes.Add("onblur", "javascript:setTimeout('__doPostBack(\'ctl00$Tela$txtCpfCnpj\',\'ListaProcessos\')', 0)")

        Catch ex As Exception
            MsgErro(ex.Message, "Erro")
        End Try
    End Sub

    Private Sub VerificaResposta()
        Try
            Dim mytarg As String = Request("__EVENTTARGET")


            Select Case mytarg
                Case "btnSim"
                    Sim()
                Case "btnNao"
                    Nao()
                Case Else

            End Select

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub Sim()
        Try
            InserirAjudaDeCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub Nao()
        Try
            LimparAjudaCusto()
            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub up1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles up1.Load
        Try
            Dim myarg As String = Request("__EVENTARGUMENT")


            Select Case myarg
                Case "ValidaCpfCnpj"
                    ValidaCpfCnpj()
                Case "ListaProcessos"
                    ListarProcessosAssociados()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ValidaCpfCnpj()
        Try
            If txtCpfCnpj.AutoPostBack = True And Not String.IsNullOrEmpty(txtCpfCnpj.Text) Then
                Dim perito As EntPERITO = Nothing
                Dim cpfcnpj As String = RetornaNumerosDeString(txtCpfCnpj.Text)
                Dim balPerito As New BALPERITO(GetUsuario)
                If cpfcnpj.Length = 11 Then
                    If Not ValidarCPF(cpfcnpj) Then
                        txtCpfCnpj.Text = String.Empty
                        MsgErro("CPF/CNPJ inválido.")
                        up1.Update()
                        Exit Sub
                    Else
                        perito = balPerito.ExibirDadosCadPeritoEnt("CPF", cpfcnpj, "S", 1)
                    End If
                ElseIf cpfcnpj.Length = 14 Then
                    If Not ValidarCNPJ(cpfcnpj) Then
                        txtCpfCnpj.Text = String.Empty
                        MsgErro("CPF/CNPJ inválido.")
                        up1.Update()
                        Exit Sub
                    Else
                        perito = balPerito.ExibirDadosCadPeritoEnt("CNPJ", cpfcnpj, "S", 2)
                    End If
                End If

                If perito Is Nothing Then
                    txtCpfCnpj.Text = String.Empty
                    MsgErro("Perito não cadastrado.")
                    up1.Update()
                    Exit Sub
                Else
                    CarregaDadosPerito(perito, Nothing)

                    If Not String.IsNullOrEmpty(txtIdRelacaoPagamento.Value) Then
                        CarregaAjudasCustoPerito(CLng(txtIdRelacaoPagamento.Value), perito.Cod_Perito)
                    End If

                    up1.Update()
                End If

            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub ListarProcessosAssociados()
        Try
            If txtNumeroProcesso.AutoPostBack And ValidaFormatoProcesso(txtNumeroProcesso.Text) = TipoNumeracaoProcesso.NUMERACAOCNJ Then
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
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub LimparRelacaoPagamento()
        Try
            txtNomeRelacaoPagamento.Text = String.Empty
            txtNomeRelacaoPagamento.AutoPostBack = True

            ddlRelacaoPgtoSemelhantes.Items.Clear()
            ddlRelacaoPgtoSemelhantes.Enabled = False
            ddlRelacaoPgtoSemelhantes.AutoPostBack = True

            chkRelacaoDefinitiva.Checked = False
            chkRelacaoDefinitiva.Enabled = False

            txtIdRelacaoPagamento.Value = String.Empty

            btnImprimir.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimparPerito()
        Try
            txtNomePerito.Text = String.Empty
            txtNomePerito.AutoPostBack = True
            ddlNomeSemelhante.Items.Clear()
            ddlNomeSemelhante.Enabled = True
            ddlNomeSemelhante.AutoPostBack = True
            txtCpfCnpj.Text = String.Empty
            txtCpfCnpj.AutoPostBack = True
            ddlNumeroConselho.Items.Clear()
            ddlSigla.Items.Clear()
            ddlUF.Items.Clear()
            ddlProfissao.Items.Clear()
            ddlEspecialidade.Items.Clear()
            txtCodPerito.Value = String.Empty
            btnNovoPerito.Enabled = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimparAjudaCusto()
        Try
            txtNumeroOficio.Text = String.Empty
            txtDataRecebimentoOficio.Text = String.Empty
            txtNumeroProcesso.Text = String.Empty
            txtValor.Text = String.Empty
            txtProcessoPagamento.Text = String.Empty
            txtDataProtocolo.Text = String.Empty
            txtDataPagamento.Text = String.Empty
            txtObs.Text = String.Empty
            txtIdAjudaCusto.Value = String.Empty
            ddlProcessosRelacionados.Items.Clear()
            ddlProcessosRelacionados.Enabled = False

            btnInserirAjudaCusto.Text = "Inserir Ajuda de Custo"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimparGridRelacao()
        Try
            grdAjudaCustoRelacao.DataSource = Nothing
            grdAjudaCustoRelacao.DataBind()

            Session("ListaAjudasCustoRelacao") = Nothing
            Session("DataTableAjudasRelacao") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimparGridAjudaCusto()
        Try
            grdAjudaCustoPerito.DataSource = Nothing
            grdAjudaCustoPerito.DataBind()

            Session("ListaAjudasCustoPerito") = Nothing
            Session("ListaAjudaExclusao") = Nothing
            Session("OcorrenciasPagamento") = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaAjudasCustoRelacaoPagamento(ByVal idRelacaoPagamento As Long)
        Try
            Dim ds As DataSet = Nothing
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)
            Dim balRelPag As New BalRelacaoPagamento(GetUsuario)

            grdAjudaCustoRelacao.DataSource = Nothing
            grdAjudaCustoRelacao.DataBind()

            ds = balAjudaCusto.ConsultarAjudasCustoRelacao(idRelacaoPagamento)
            If ds IsNot Nothing Then
                btnImprimir.Enabled = True
                ds.Tables(0).DefaultView.RowFilter = "DATA_EXCLUSAO IS NULL"
                Session("DataTableAjudasRelacao") = ds.Tables(0).DefaultView.ToTable
                grdAjudaCustoRelacao.DataSource = ds.Tables(0).DefaultView
                'txtNomeRelacaoPagamento.Text = CStr(ds.Tables(0).Rows(0).Item("NOME_RELACAO_PAGAMENTO"))
            Else
                btnImprimir.Enabled = False
                grdAjudaCustoRelacao.DataSource = Nothing
                Session("DataTableAjudasRelacao") = Nothing
                txtNomeRelacaoPagamento.Text = txtNomeRelacaoPagamento.Text.ToUpper
            End If

            grdAjudaCustoRelacao.DataBind()

            If idRelacaoPagamento <> 0 Then
                chkRelacaoDefinitiva.Enabled = VerificaAutorizacaoUsuario(NomePagina, "chkRelacaoDefinitiva", "RELDEF")
                chkRelacaoDefinitiva.Checked = balRelPag.ListarRelacaoPagamento(idRelacaoPagamento).Definitiva
            End If

            HabilitaPainelRelacaoPagamento(True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaAjudasCustoPerito(ByVal idRelacaoPagamento As Long, ByVal codPerito As Long)
        Try
            Dim listaAjudasCusto As List(Of AjudaCusto) = Nothing
            Dim listaOrdenada As List(Of AjudaCusto) = Nothing
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)

            listaAjudasCusto = balAjudaCusto.ListarAjudasCustoPerito(idRelacaoPagamento, codPerito)

            If listaAjudasCusto.Count > 0 Then
                listaOrdenada = listaAjudasCusto.OrderBy(Function(x) x.Oficio).ToList
            Else
                listaOrdenada = New List(Of AjudaCusto)
            End If

            Session("ListaAjudasCustoPerito") = listaAjudasCusto

            grdAjudaCustoPerito.DataSource = listaOrdenada
            grdAjudaCustoPerito.DataBind()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaDadosPerito(ByVal perito As EntPERITO, ByVal ajudaCusto As AjudaCusto)
        Try
            txtNomePerito.AutoPostBack = False
            txtCpfCnpj.AutoPostBack = False
            Dim dsProfissao As DataSet = Nothing
            Dim balEspecialidadePerito As New BALEspecialidadePerito(GetUsuario)

            txtCodPerito.Value = perito.Cod_Perito.ToString
            txtNomePerito.Text = perito.Nome
            ddlNomeSemelhante.Enabled = False
            If perito.TipoPessoa = EntPERITO.Pessoa.Fisica Then
                txtCpfCnpj.Text = RetornaCPFCNPJFormatado(perito.CPF)
            Else
                txtCpfCnpj.Text = RetornaCPFCNPJFormatado(perito.CNPJ)
            End If
            dsProfissao = balEspecialidadePerito.ListarProfissoesEspecialidadesPerito(perito.Cod_Perito)

            ddlNumeroConselho.DataSource = dsProfissao.Tables(0)
            ddlNumeroConselho.DataTextField = "NUM_REG"
            ddlNumeroConselho.DataValueField = "NUM_REG"
            ddlNumeroConselho.DataBind()
            ddlNumeroConselho.Items.Insert(0, "Selecione...")


            ddlSigla.DataSource = dsProfissao.Tables(0)
            ddlSigla.DataTextField = "SIGLA_PER"
            ddlSigla.DataValueField = "COD_ORGAO_PER"
            ddlSigla.DataBind()
            ddlSigla.Items.Insert(0, "Selecione...")

            ddlUF.DataSource = dsProfissao.Tables(0)
            ddlUF.DataTextField = "UF"
            ddlUF.DataValueField = "UF"
            ddlUF.DataBind()
            ddlUF.Items.Insert(0, "Selecione...")

            ddlProfissao.DataSource = dsProfissao.Tables(0)
            ddlProfissao.DataTextField = "DESCR_PROFISSAO"
            ddlProfissao.DataValueField = "COD_PROFISSAO"
            ddlProfissao.DataBind()
            ddlProfissao.Items.Insert(0, "Selecione...")

            ddlEspecialidade.DataSource = dsProfissao.Tables(0)
            ddlEspecialidade.DataTextField = "DESCR_ESPECIALIDADE"
            ddlEspecialidade.DataValueField = "COD_ESPECIALIDADE"
            ddlEspecialidade.DataBind()
            ddlEspecialidade.Items.Insert(0, "Selecione...")

            txtNomePerito.AutoPostBack = True
            txtCpfCnpj.AutoPostBack = True

            If Not ajudaCusto Is Nothing Then
                ddlNumeroConselho.SelectedValue = ajudaCusto.EspecialidadeProfissaoPerito.Num_Reg.ToString
                ddlSigla.SelectedValue = ajudaCusto.EspecialidadeProfissaoPerito.COD_ORGAO_PER.ToString
                ddlUF.SelectedValue = ajudaCusto.EspecialidadeProfissaoPerito.UF
                ddlProfissao.SelectedValue = ajudaCusto.EspecialidadeProfissaoPerito.COD_PROFISSAO.ToString
                ddlEspecialidade.SelectedValue = ajudaCusto.EspecialidadeProfissaoPerito.COD_ESPECIALIDADE.ToString
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CarregaDadosAjudaCusto(ByVal ajudaCusto As AjudaCusto)
        Try
            txtSeqAjudaCusto.Value = ajudaCusto.Sequencial.ToString
            txtIdAjudaCusto.Value = ajudaCusto.Id.ToString
            txtNumeroOficio.Text = ajudaCusto.Oficio
            txtDataRecebimentoOficio.Text = ajudaCusto.DataRecebimento
            txtNumeroProcesso.Text = ajudaCusto.NumeroProcesso
            txtValor.Text = String.Format("R$ {0:N}", ajudaCusto.Valor)
            txtProcessoPagamento.Text = ajudaCusto.ProcessoPagamento
            txtDataProtocolo.Text = ajudaCusto.DataProtocolo
            txtDataPagamento.Text = ajudaCusto.DataPagamento
            txtObs.Text = ajudaCusto.Observacao

            If ValidaFormatoProcesso(ajudaCusto.NumeroProcesso) = TipoNumeracaoProcesso.NUMERACAOCNJ Then
                Dim ds As DataSet = Nothing
                Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)

                ds = balAjudaCusto.ListarProcessosAssociados(ajudaCusto.NumeroProcesso)

                If ds.Tables(0).Rows.Count > 0 Then
                    ddlProcessosRelacionados.DataSource = ds.Tables(0)
                    ddlProcessosRelacionados.DataTextField = "COD_PROC"
                    ddlProcessosRelacionados.DataValueField = "ID_PROC"
                    ddlProcessosRelacionados.DataBind()

                    ddlProcessosRelacionados.Items.Insert(0, "Selecione um processo associado...")
                    ddlProcessosRelacionados.SelectedValue = ajudaCusto.IdProcesso
                    ddlProcessosRelacionados.Enabled = True
                End If
            Else
                ddlProcessosRelacionados.Items.Clear()
                ddlProcessosRelacionados.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub PreencheInformacoesProcesso(ByRef ajudaCusto As AjudaCusto, ByVal balAjudaCusto As BalAjudaCusto)
        Try

            If ddlProcessosRelacionados.Enabled Then
                ajudaCusto.IdProcesso = CLng(ddlProcessosRelacionados.SelectedValue)
                ajudaCusto.IdProcessoAdministrativo = 0
            Else
                Dim tipoProcesso As Integer
                Dim idProcesso As Long

                Select Case ValidaFormatoProcesso(ajudaCusto.NumeroProcesso)
                    Case TipoNumeracaoProcesso.ADMINISTRATIVO
                        idProcesso = balAjudaCusto.PesquisaCodigoProcesso(RetornaNumeroProcessoAdministrativo(ajudaCusto.NumeroProcesso), TipoNumeracaoProcesso.ADMINISTRATIVO, tipoProcesso)

                        If idProcesso = 0 Then
                            Throw New Exception(String.Format("O Processo Administrativo {0} não foi encontrado na base de dados do EPROT.", ajudaCusto.NumeroProcesso))
                        End If

                        ajudaCusto.IdProcesso = CLng(IIf(tipoProcesso = 1 And idProcesso <> 0, 0, idProcesso))
                        ajudaCusto.IdProcessoAdministrativo = RetornaNumeroProcessoAdministrativo(ajudaCusto.NumeroProcesso)
                    Case TipoNumeracaoProcesso.NUMERACAOANTIGA
                        idProcesso = balAjudaCusto.PesquisaCodigoProcesso(ajudaCusto.NumeroProcesso, TipoNumeracaoProcesso.NUMERACAOANTIGA, tipoProcesso)

                        If idProcesso = 0 Then
                            Throw New Exception(String.Format("O Processo Judicial {0} não foi encontrado na base de dados do PROCORP.", ajudaCusto.NumeroProcesso))
                        End If

                        ajudaCusto.IdProcesso = idProcesso
                        ajudaCusto.IdProcessoAdministrativo = 0
                    Case TipoNumeracaoProcesso.NUMERACAOCNJ
                        idProcesso = balAjudaCusto.PesquisaCodigoProcesso(ajudaCusto.NumeroProcesso, TipoNumeracaoProcesso.NUMERACAOCNJ, tipoProcesso)

                        If idProcesso = 0 Then
                            Throw New Exception(String.Format("O Processo {0} não foi encontrado na base de dados.", ajudaCusto.NumeroProcesso))
                        End If

                        ajudaCusto.IdProcesso = idProcesso

                        If tipoProcesso = TipoDeProcesso.ADMINISTRATIVO Then
                            ajudaCusto.IdProcessoAdministrativo = balAjudaCusto.PesquisaNumeroProcessoEprot(ajudaCusto.NumeroProcesso)

                            If ajudaCusto.IdProcessoAdministrativo = 0 Then
                                Throw New Exception(String.Format("O Processo Administrativo não foi encontrado na base de dados do EPROT.", ajudaCusto.NumeroProcesso))
                            End If
                        End If

                    Case TipoNumeracaoProcesso.FORMATOINVALIDO
                        Throw New Exception("O número do processo está num formato inválido.")
                    Case Else
                        Throw New Exception("O número do processo está num formato inválido.")
                End Select
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function PreencheAjudaCusto(ByVal seq As Long) As AjudaCusto
        Try
            Dim ajudaCusto As New AjudaCusto
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)
            Dim relacaoPagamento As RelacaoPagamento = Nothing
            Dim balRelacaoPagamento As New BalRelacaoPagamento(GetUsuario)
            Dim perito As EntPERITO = Nothing
            Dim balPerito As New BALPERITO(GetUsuario)
            Dim profissao As EntProfissao = Nothing
            Dim balProfissao As New BALProfissao(GetUsuario)
            Dim especialidade As EntEspecialidade = Nothing
            Dim balEspecialidade As New BALEspecialidade(GetUsuario)
            Dim balEspecialidadePerito As New BALEspecialidadePerito(GetUsuario)

            If String.IsNullOrEmpty(txtIdRelacaoPagamento.Value) Then
                relacaoPagamento = New RelacaoPagamento
                relacaoPagamento.Id = 0
                relacaoPagamento.Nome = txtNomeRelacaoPagamento.Text.ToUpper
                relacaoPagamento.Definitiva = chkRelacaoDefinitiva.Checked
                relacaoPagamento.DataCadastro = Now
            Else
                relacaoPagamento = balRelacaoPagamento.ListarRelacaoPagamento(CLng(txtIdRelacaoPagamento.Value))
            End If

            ajudaCusto.RelacaoPagamento = relacaoPagamento

            If Not String.IsNullOrEmpty(txtCodPerito.Value) Then
                perito = balPerito.Carregar(CLng(txtCodPerito.Value))
            End If

            If perito.StatusAtual.Codigo = 4 Then 'Não é permitido inserir uma ajuda de custo para um perito que esteja no status de Perito do Juízo
                Throw New Exception("Não é possível inserir ajuda de custo para um perito do juízo.")
            ElseIf perito.StatusAtual.Codigo = 3 Then
                Throw New Exception("Não é possível inserir ajuda de custo para um perito falecido.")
            End If

            profissao = balProfissao.Carregar(CLng(ddlProfissao.SelectedValue))
            especialidade = balEspecialidade.Carregar(CLng(ddlEspecialidade.SelectedValue), profissao.Cod_Profissao)

            If especialidade Is Nothing Then
                Throw New Exception(String.Format("A especialidade {0} não corresponde a profissão {1}.", ddlEspecialidade.SelectedItem.Text, ddlProfissao.SelectedItem.Text))
            End If

            ajudaCusto.Sequencial = seq

            ajudaCusto.Id = CLng(IIf(String.IsNullOrEmpty(txtIdAjudaCusto.Value), 0, txtIdAjudaCusto.Value))
            ajudaCusto.Perito = perito
            ajudaCusto.Profissao = profissao
            ajudaCusto.Especialidade = especialidade
            ajudaCusto.EspecialidadeProfissaoPerito = balEspecialidadePerito.Consultar(perito.Cod_Perito,
                                                                                       profissao.Cod_Profissao,
                                                                                       especialidade.Cod_Especialidade)
            ajudaCusto.Oficio = txtNumeroOficio.Text.ToUpper
            ajudaCusto.DataRecebimento = txtDataRecebimentoOficio.Text
            ajudaCusto.NumeroProcesso = txtNumeroProcesso.Text

            PreencheInformacoesProcesso(ajudaCusto, balAjudaCusto)

            ajudaCusto.Valor = CDbl(txtValor.Text.Replace(".", "").Replace("R$", "").Trim)
            ajudaCusto.ProcessoPagamento = txtProcessoPagamento.Text
            ajudaCusto.DataProtocolo = IIf(String.IsNullOrEmpty(txtDataProtocolo.Text), Nothing, txtDataProtocolo.Text)
            ajudaCusto.DataPagamento = IIf(String.IsNullOrEmpty(txtDataPagamento.Text), Nothing, txtDataPagamento.Text)
            ajudaCusto.Observacao = txtObs.Text

            Return ajudaCusto
        Catch imx As InsufficientMemoryException
            Throw New Exception("Erro! Valor da ajuda de custo acima do tamanho máximo permitido.")
        Catch ax As ArithmeticException
            Throw New Exception("Erro! Valor da ajuda de custo acima do tamanho máximo permitido.")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ValidaCampos() As Boolean
        Try
            If String.IsNullOrEmpty(txtNomeRelacaoPagamento.Text) Then
                MsgErro("Favor informar o nome da relação de pagamento.")
                Return False
            End If

            If String.IsNullOrEmpty(txtNomePerito.Text) Then
                MsgErro("Favor informar o nome do perito.")
                Return False
            End If

            If String.IsNullOrEmpty(txtCpfCnpj.Text) Then
                MsgErro("Favor informar o CPF/CNPJ do perito.")
                Return False
            End If

            'If ddlNumeroConselho.SelectedIndex = -1 OrElse ddlNumeroConselho.SelectedIndex = 0 Then
            '    MsgErro("Favor informar o número do conselho profissional do perito.")
            '    Return False
            'End If

            'If ddlSigla.SelectedIndex = -1 OrElse ddlSigla.SelectedIndex = 0 Then
            '    MsgErro("Favor informar a sigla do conselho profissional do perito.")
            '    Return False
            'End If

            'If ddlUF.SelectedIndex = -1 OrElse ddlUF.SelectedIndex = 0 Then
            '    MsgErro("Favor informar o UF.")
            '    Return False
            'End If

            If ddlProfissao.SelectedIndex = -1 OrElse ddlProfissao.SelectedIndex = 0 Then
                MsgErro("Favor informar a profissão do perito.")
                Return False
            End If

            If ddlEspecialidade.SelectedIndex = -1 OrElse ddlEspecialidade.SelectedIndex = 0 Then
                MsgErro("Favor informar a especialidade do perito.")
                Return False
            End If

            If String.IsNullOrEmpty(txtNumeroOficio.Text) Then
                MsgErro("Favor informar o número de ofício da ajuda de custo.")
                Return False
            End If

            If String.IsNullOrEmpty(txtDataRecebimentoOficio.Text) Then
                MsgErro("Favor informar a data de recebimento.")
                Return False
            End If

            If String.IsNullOrEmpty(txtNumeroProcesso.Text) Then
                MsgErro("Favor informar o número do processo.")
                Return False
            End If

            If ValidaFormatoProcesso(txtNumeroProcesso.Text) = TipoNumeracaoProcesso.FORMATOINVALIDO Then
                MsgErro("O número do processo está num formato inválido.")
                Return False
            End If

            If ValidaFormatoProcesso(txtNumeroProcesso.Text) = TipoNumeracaoProcesso.NUMERACAOCNJ And ddlProcessosRelacionados.Enabled Then
                If ddlProcessosRelacionados.SelectedIndex = -1 OrElse ddlProcessosRelacionados.SelectedIndex = 0 Then
                    MsgErro("Favor informar qual dos processos associados a este CNJ deve ser considerado.")
                    Return False
                End If
            End If

            If String.IsNullOrEmpty(txtValor.Text) Then
                MsgErro("Favor informar o valor da ajuda de custo.")
                Return False
            End If

            Try
                If CDbl(txtValor.Text) = 0 Then
                    MsgErro("Não pode ser inserido ajudas de custo com valor zerado.")
                    Return False
                End If
            Catch imx As InsufficientMemoryException
                MsgErro("Erro! Valor da ajuda de custo acima do tamanho máximo permitido.")
                Return False
            Catch ax As ArithmeticException
                MsgErro("Erro! Valor da ajuda de custo acima do tamanho máximo permitido.")
                Return False
            Catch ex As Exception
                MsgErro("O valor da ajuda de custo está num formato inválido.")
                Return False
            End Try

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub InserirAjudaDeCusto()
        Try
            Dim ajudaCusto As AjudaCusto = Nothing
            Dim listaAjudasCusto As List(Of AjudaCusto) = IIf(Session("ListaAjudasCustoPerito") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudasCustoPerito"), List(Of AjudaCusto)))
            Dim listaAjudasCustoOrdenada As List(Of AjudaCusto) = Nothing

            If (Not String.IsNullOrEmpty(txtSeqAjudaCusto.Value) AndAlso CLng(txtSeqAjudaCusto.Value) <> 0) OrElse (Not String.IsNullOrEmpty(txtIdAjudaCusto.Value) AndAlso CLng(txtIdAjudaCusto.Value) <> 0) Then
                ajudaCusto = PreencheAjudaCusto(CLng(txtSeqAjudaCusto.Value))

                If ajudaCusto.Sequencial <> 0 Then
                    listaAjudasCusto.Remove(listaAjudasCusto.Find(Function(x) x.Sequencial = ajudaCusto.Sequencial))
                ElseIf ajudaCusto.Id <> 0 Then
                    listaAjudasCusto.Remove(listaAjudasCusto.Find(Function(x) x.Id = ajudaCusto.Id))
                End If

                listaAjudasCusto.Add(ajudaCusto)
            Else
                ajudaCusto = PreencheAjudaCusto(CLng(listaAjudasCusto.Count + 1))
                listaAjudasCusto.Add(ajudaCusto)
            End If

            listaAjudasCustoOrdenada = listaAjudasCusto.OrderBy(Function(x) x.Oficio).ToList

            Session("ListaAjudasCustoPerito") = Nothing
            Session("ListaAjudasCustoPerito") = listaAjudasCusto

            grdAjudaCustoPerito.DataSource = Nothing
            grdAjudaCustoPerito.DataBind()

            grdAjudaCustoPerito.DataSource = listaAjudasCustoOrdenada
            grdAjudaCustoPerito.DataBind()

            btnNovoPerito.Enabled = True

            LimparAjudaCusto()

            BtnGravar.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HabilitaPainelRelacaoPagamento(ByVal valor As Boolean)
        Try
            btnNovaAjudaCusto.Enabled = valor
            pnlListaAjudaRelacao.Visible = valor
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HabilitaPainelAjudaCusto(ByVal valor As Boolean)
        Try
            pnlPeritoAjudaCusto.Visible = valor
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AlterarRelacaoPagamento(ByVal idRelacaoPagamento As Long)
        Try
            Dim balRelPagamento As New BalRelacaoPagamento(GetUsuario)
            Dim relacaoPagamento As RelacaoPagamento = balRelPagamento.ListarRelacaoPagamento(idRelacaoPagamento)

            If relacaoPagamento.Definitiva <> chkRelacaoDefinitiva.Checked Then
                relacaoPagamento.Definitiva = chkRelacaoDefinitiva.Checked
                balRelPagamento.Alterar(relacaoPagamento)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub txtNomeRelacaoPagamento_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNomeRelacaoPagamento.TextChanged
        Try
            Dim ds As DataSet = Nothing
            Dim balRelacaoPagamento As New BalRelacaoPagamento(GetUsuario)

            ds = balRelacaoPagamento.BuscaNomesSemelhantes(txtNomeRelacaoPagamento.Text)

            If ds.Tables(0).Rows.Count > 0 Then
                ddlRelacaoPgtoSemelhantes.DataValueField = "ID_RELACAO_PAGAMENTO"
                ddlRelacaoPgtoSemelhantes.DataTextField = "NOME_RELACAO_PAGAMENTO"
                ddlRelacaoPgtoSemelhantes.DataSource = ds.Tables(0)
                ddlRelacaoPgtoSemelhantes.DataBind()
                ddlRelacaoPgtoSemelhantes.Items.Insert(0, New ListItem("Selecione uma Relação de Pagamento", "0"))
                ddlRelacaoPgtoSemelhantes.SelectedValue = "0"
                ddlRelacaoPgtoSemelhantes.Enabled = True

                btnImprimir.Enabled = True

                up1.Update()
            Else
                ddlRelacaoPgtoSemelhantes.Enabled = False

                CarregaAjudasCustoRelacaoPagamento(0)


                up1.Update()
            End If

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlRelacaoPgtoSemelhantes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRelacaoPgtoSemelhantes.SelectedIndexChanged
        Try
            If ddlRelacaoPgtoSemelhantes.SelectedIndex <> -1 Then
                Dim idRelacaoPagamento As Long = CLng(ddlRelacaoPgtoSemelhantes.SelectedValue)

                idRelacaoPagamento = IIf(idRelacaoPagamento = Nothing, 0, idRelacaoPagamento)

                If Not idRelacaoPagamento = Nothing AndAlso idRelacaoPagamento <> 0 Then
                    CarregaAjudasCustoRelacaoPagamento(idRelacaoPagamento)

                    txtNomeRelacaoPagamento.Text = ddlRelacaoPgtoSemelhantes.SelectedItem.Text
                    txtIdRelacaoPagamento.Value = idRelacaoPagamento.ToString
                    ddlRelacaoPgtoSemelhantes.Enabled = False


                    up1.Update()
                End If
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnNovaAjudaCusto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovaAjudaCusto.Click
        Try
            pnlPeritoAjudaCusto.Visible = True
            btnNovaAjudaCusto.Enabled = False

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtNomePerito_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNomePerito.TextChanged
        Try
            If txtNomePerito.AutoPostBack = True And txtNomePerito.Text <> String.Empty Then
                Dim balPerito As New BALPERITO(GetUsuario)
                Dim dsNomesSemelhantes As DataSet = Nothing

                dsNomesSemelhantes = balPerito.ObterNomesSemelhantesPerito(txtNomePerito.Text)

                If dsNomesSemelhantes.Tables(0).Rows.Count = 0 Then
                    MsgErro("Não foram retornados registros na busca.")
                    Exit Sub
                End If

                ddlNomeSemelhante.DataSource = dsNomesSemelhantes.Tables(0)
                ddlNomeSemelhante.DataTextField = "NOME"
                ddlNomeSemelhante.DataValueField = "COD_PERITO"
                ddlNomeSemelhante.DataBind()

                ddlNomeSemelhante.Items.Insert(0, "Selecione um perito...")
                ddlNomeSemelhante.SelectedIndex = 0


                up1.Update()
            End If

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub ddlNomeSemelhante_SelectedIndexChanged(ByVal sender As Object, e As System.EventArgs) Handles ddlNomeSemelhante.SelectedIndexChanged
        Try
            If Not ddlNomeSemelhante.SelectedValue Is Nothing AndAlso ddlNomeSemelhante.SelectedIndex > -1 Then
                Dim balPerito As New BALPERITO(GetUsuario)
                Dim perito As EntPERITO = balPerito.Carregar(CLng(ddlNomeSemelhante.SelectedValue))
                CarregaDadosPerito(perito, Nothing)

                If Not String.IsNullOrEmpty(txtIdRelacaoPagamento.Value) Then
                    CarregaAjudasCustoPerito(CLng(txtIdRelacaoPagamento.Value), perito.Cod_Perito)
                End If


                up1.Update()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    'Protected Sub txtCpfCnpj_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCpfCnpj.TextChanged
    '    Try

    '    Catch ex As Exception
    '        MsgErro(ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnInserirAjudaCusto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInserirAjudaCusto.Click
        Try
            If ValidaCampos() Then
                Dim tipoProcesso As TipoNumeracaoProcesso = ValidaFormatoProcesso(txtNumeroProcesso.Text)
                Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)
                Dim listaOcorrencias As DataSet = Nothing

                If Not btnInserirAjudaCusto.Text.Contains("Alterar") Then
                    If Not String.IsNullOrEmpty(txtIdRelacaoPagamento.Value) AndAlso CLng(txtIdRelacaoPagamento.Value) <> 0 Then
                        listaOcorrencias = balAjudaCusto.ListarOcorrenciasPagamento(txtNumeroProcesso.Text, tipoProcesso, CLng(txtIdRelacaoPagamento.Value))

                        If Not listaOcorrencias Is Nothing Then
                            If listaOcorrencias.Tables(0).Rows.Count > 0 Then
                                Session("OcorrenciasPagamento") = listaOcorrencias.Tables(0)
                                'janelaMensagem.Show()
                                Dim script As String = "AbrirPopUp('frmOcorrenciasPagamento.aspx', '700', '500', '', 'Sim')"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "RedirecionarPagina", script, True)
                                Exit Sub
                            End If
                        End If
                    End If
                End If

                InserirAjudaDeCusto()

                up1.Update()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnNovoPerito_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovoPerito.Click
        Try
            LimparPerito()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnGravar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGravar.Click
        Try

            Dim listaAjudasCusto As List(Of AjudaCusto) = IIf(Session("ListaAjudasCustoPerito") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudasCustoPerito"), List(Of AjudaCusto)))
            Dim listaParaInserir As List(Of AjudaCusto) = listaAjudasCusto.FindAll(Function(x) x.Id = 0)
            Dim listaParaAlterar As List(Of AjudaCusto) = listaAjudasCusto.FindAll(Function(x) x.Id <> 0)
            Dim listaParaExcluir As List(Of AjudaCusto) = IIf(Session("ListaAjudaExclusao") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudaExclusao"), List(Of AjudaCusto)))
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)
            Dim inseriu = False, alterou = False, excluiu As Boolean = False

            If Not String.IsNullOrEmpty(txtIdRelacaoPagamento.Value) AndAlso CLng(txtIdRelacaoPagamento.Value) <> 0 Then
                AlterarRelacaoPagamento(CLng(txtIdRelacaoPagamento.Value))
            End If

            Try
                If listaParaInserir.Count > 0 Then
                    Dim id = balAjudaCusto.Gravar(listaParaInserir).ToString
                    If String.IsNullOrEmpty(txtIdRelacaoPagamento.Value) OrElse CLng(txtIdRelacaoPagamento.Value) = 0 Then
                        txtIdRelacaoPagamento.Value = id
                    End If
                    inseriu = True
                End If
            Catch ex As Exception
                Throw New Exception("Erro ao tentar inserir as ajudas de custo.")
            End Try

            Try
                If listaParaAlterar.Count > 0 Then
                    balAjudaCusto.Alterar(listaParaAlterar)
                    alterou = True
                End If
            Catch ex As Exception
                Throw New Exception("Erro ao tentar alterar os dados das ajudas de custo.")
            End Try

            Try
                If listaParaExcluir.Count > 0 Then
                    balAjudaCusto.Excluir(listaParaExcluir)
                    excluiu = True
                End If
            Catch ex As Exception
                Throw New Exception("Erro ao tentar excluir as ajudas de custo.")
            End Try

            LimparAjudaCusto()
            LimparGridAjudaCusto()
            LimparPerito()
            LimparGridRelacao()
            CarregaAjudasCustoRelacaoPagamento(CLng(txtIdRelacaoPagamento.Value))
            HabilitaPainelAjudaCusto(False)

            If inseriu And Not alterou And Not excluiu Then
                MsgErro("Dados armazenados com sucesso.", "Sucesso")
            ElseIf alterou And Not inseriu And Not excluiu Then
                MsgErro("Dados alterados com sucesso.", "Sucesso")
            ElseIf excluiu And Not inseriu And Not alterou Then
                MsgErro("Ajuda(s) de custo excluída(s).", "Sucesso")
            Else
                MsgErro("Dados gravados com sucesso.", "Sucesso")
            End If

            BtnGravar.Enabled = False

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Try
            LimparRelacaoPagamento()
            LimparPerito()
            LimparAjudaCusto()
            LimparGridRelacao()
            LimparGridAjudaCusto()
            HabilitaPainelRelacaoPagamento(False)
            HabilitaPainelAjudaCusto(False)

            BtnGravar.Enabled = False
            btnImprimir.Enabled = False
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSair.Click
        Try
            Response.Redirect("Principal.aspx", False)
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImprimir.Click
        Try
            If Not String.IsNullOrEmpty(txtIdRelacaoPagamento.Value) Then
                Dim pagina As String = String.Format("frmRelatorio.aspx?tipoRelatorio=2&idRelacaoPagamento={0}", txtIdRelacaoPagamento.Value)
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "RedirecionaRelatorio", "window.open('" + pagina + "', '_blank');", True)
            Else
                MsgErro("Erro ao tentar gerar o relatório.")
                Exit Sub
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnAlterarAjuda_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            Dim IdRelacaoPagamento As Long = 0, IdAjudaCusto As Long = 0, Sequencial As Long = 0
            Dim argumentos() As String = e.CommandArgument.ToString.Split(CChar(","))
            Dim listaDeAjudas As List(Of AjudaCusto) = Nothing
            Dim ajudaCusto As AjudaCusto = Nothing
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)

            Sequencial = CLng(IIf(String.IsNullOrEmpty(argumentos(0)), 0, argumentos(0)))
            IdAjudaCusto = CLng(IIf(String.IsNullOrEmpty(argumentos(1)), 0, argumentos(1)))
            IdRelacaoPagamento = CLng(IIf(String.IsNullOrEmpty(argumentos(2)), 0, argumentos(2)))

            listaDeAjudas = IIf(Session("ListaAjudasCustoPerito") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudasCustoPerito"), List(Of AjudaCusto)))

            If listaDeAjudas.Count > 0 Then
                If Sequencial <> 0 Then
                    ajudaCusto = listaDeAjudas.Find(Function(x) x.Sequencial = Sequencial)
                ElseIf IdAjudaCusto <> 0 Then
                    ajudaCusto = listaDeAjudas.Find(Function(x) x.Id = IdAjudaCusto)
                Else
                    MsgErro("Erro ao tentar alterar ajuda de custo.")
                    Exit Sub
                End If
            Else
                If IdAjudaCusto <> 0 Then
                    ajudaCusto = balAjudaCusto.Carregar(IdAjudaCusto)
                Else
                    MsgErro("Erro ao tentar alterar ajuda de custo.")
                    Exit Sub
                End If
            End If

            CarregaDadosPerito(ajudaCusto.Perito, ajudaCusto)
            CarregaDadosAjudaCusto(ajudaCusto)

            btnInserirAjudaCusto.Text = "Alterar Ajuda de Custo"

            up1.Update()

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnExcluirAjuda_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            Dim IdRelacaoPagamento As Long = 0, IdAjudaCusto As Long = 0, Sequencial As Long = 0
            Dim argumentos() As String = e.CommandArgument.ToString.Split(CChar(","))
            Dim listaDeAjudas As List(Of AjudaCusto) = Nothing
            Dim listaParaExcluir As List(Of AjudaCusto) = Nothing
            Dim listaOrdenada As List(Of AjudaCusto) = Nothing
            Dim ajudaCusto As AjudaCusto = Nothing
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)

            Sequencial = CLng(IIf(String.IsNullOrEmpty(argumentos(0)), 0, argumentos(0)))
            IdAjudaCusto = CLng(IIf(String.IsNullOrEmpty(argumentos(1)), 0, argumentos(1)))
            IdRelacaoPagamento = CLng(IIf(String.IsNullOrEmpty(argumentos(2)), 0, argumentos(2)))

            Dim listaAjudasCustoExistentes As List(Of AjudaCusto) = balAjudaCusto.ListarAjudasCustoRelacao(IdRelacaoPagamento)
            If listaAjudasCustoExistentes.Count = 1 Then
                MsgErro("Uma relação de pagamento não pode existir sem ajudas de custo!")
                Exit Sub
            End If

            listaDeAjudas = IIf(Session("ListaAjudasCustoPerito") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudasCustoPerito"), List(Of AjudaCusto)))
            listaParaExcluir = IIf(Session("ListaAjudaExclusao") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudaExclusao"), List(Of AjudaCusto)))

            If IdAjudaCusto = 0 AndAlso Sequencial <> 0 Then
                ajudaCusto = listaDeAjudas.Find(Function(x) x.Sequencial = Sequencial)
            Else
                ajudaCusto = listaDeAjudas.Find(Function(x) x.Id = IdAjudaCusto)
            End If

            If ajudaCusto.Id <> 0 Then
                If ajudaCusto.RelacaoPagamento.Definitiva Then
                    MsgErro("A ajuda de custo não pode ser excluída pois a mesma pertence a uma relação de pagamento definitva.")
                    Exit Sub
                End If

                If (Not String.IsNullOrEmpty(ajudaCusto.ProcessoPagamento) OrElse
                    Not String.IsNullOrEmpty(ajudaCusto.DataProtocolo) OrElse
                    Not String.IsNullOrEmpty(ajudaCusto.DataPagamento)) Then
                    MsgErro("A ajuda de custo não pode ser excluída pois possui uma das informações: processo de pagamento e/ou data protocolo e/ou data pagamento.")
                    Exit Sub
                End If

                listaParaExcluir.Add(ajudaCusto)
                Session("ListaAjudaExclusao") = listaParaExcluir
            End If

            listaDeAjudas.Remove(ajudaCusto)
            Session("ListaAjudasCustoPerito") = listaDeAjudas

            BtnGravar.Enabled = True

            If IdRelacaoPagamento = 0 And listaDeAjudas.Count = 0 Then
                BtnGravar.Enabled = False
            End If

            listaOrdenada = listaDeAjudas.OrderBy(Function(x) x.Oficio).ToList

            grdAjudaCustoPerito.DataSource = Nothing
            grdAjudaCustoPerito.DataBind()

            grdAjudaCustoPerito.DataSource = listaOrdenada
            grdAjudaCustoPerito.DataBind()



            up1.Update()

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnAlterarRel_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            Dim idRelacaoPagamento As Long = 0, idAjudaCusto As Long = 0
            Dim ajudaCusto As AjudaCusto = Nothing
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)

            Dim argumentos() As String = e.CommandArgument.ToString.Split(CChar(","))
            idRelacaoPagamento = CLng(IIf(String.IsNullOrEmpty(argumentos(0)), 0, argumentos(0)))
            idAjudaCusto = CLng(IIf(String.IsNullOrEmpty(argumentos(1)), 0, argumentos(1)))

            ajudaCusto = balAjudaCusto.Carregar(idAjudaCusto)

            HabilitaPainelAjudaCusto(True)

            CarregaDadosPerito(ajudaCusto.Perito, ajudaCusto)
            CarregaDadosAjudaCusto(ajudaCusto)
            CarregaAjudasCustoPerito(ajudaCusto.RelacaoPagamento.Id, ajudaCusto.Perito.Cod_Perito)

            btnInserirAjudaCusto.Text = "Alterar Ajuda de Custo"

            up1.Update()

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnExcluirRel_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            Dim idRelacaoPagamento As Long = 0, idAjudaCusto As Long = 0
            Dim ajudaCusto As AjudaCusto = Nothing
            Dim listaAjudaParaExcluir As List(Of AjudaCusto) = Nothing
            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)

            Dim argumentos() As String = e.CommandArgument.ToString.Split(CChar(","))
            idRelacaoPagamento = CLng(IIf(String.IsNullOrEmpty(argumentos(0)), 0, argumentos(0)))
            idAjudaCusto = CLng(IIf(String.IsNullOrEmpty(argumentos(1)), 0, argumentos(1)))

            Dim listaAjudasCustoExistentes As List(Of AjudaCusto) = balAjudaCusto.ListarAjudasCustoRelacao(idRelacaoPagamento)
            If listaAjudasCustoExistentes.Count = 1 Then
                MsgErro("Uma relação de pagamento não pode existir sem ajudas de custo!")
                Exit Sub
            End If

            listaAjudaParaExcluir = IIf(Session("ListaAjudaExclusao") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudaExclusao"), List(Of AjudaCusto)))
            ajudaCusto = balAjudaCusto.Carregar(idAjudaCusto)

            If ajudaCusto.RelacaoPagamento.Definitiva Then
                MsgErro("A ajuda de custo não pode ser excluída pois a mesma pertence a uma relação de pagamento definitva.")
                Exit Sub
            End If

            If (Not String.IsNullOrEmpty(ajudaCusto.ProcessoPagamento) OrElse
                Not String.IsNullOrEmpty(ajudaCusto.DataProtocolo) OrElse
                Not String.IsNullOrEmpty(ajudaCusto.DataPagamento)) Then
                MsgErro("A ajuda de custo não pode ser excluída pois possui uma das informações: processo de pagamento e/ou data protocolo e/ou data pagamento.")
                Exit Sub
            End If

            If listaAjudaParaExcluir.Find(Function(x) x.Id = ajudaCusto.Id) Is Nothing Then
                listaAjudaParaExcluir.Add(ajudaCusto)
                Session("ListaAjudaExclusao") = listaAjudaParaExcluir
            End If

            Dim IdAjudasExcluidas As String = String.Empty
            Dim i As Long = 1
            For Each ac As AjudaCusto In listaAjudaParaExcluir
                If i = 1 Then
                    IdAjudasExcluidas = String.Format("{0}", ac.Id.ToString)
                Else
                    IdAjudasExcluidas = String.Format("{0},{1}", IdAjudasExcluidas, ac.Id.ToString)
                End If
                i = i + 1
            Next

            'Dim dt As DataTable = grdAjudaCustoRelacao.DataSource
            Dim dt As DataTable = balAjudaCusto.ConsultarAjudasCustoRelacao(idRelacaoPagamento).Tables(0)
            dt.DefaultView.RowFilter = String.Format("ID_AJUDA_CUSTO NOT IN({0})", IdAjudasExcluidas)

            'dt.DefaultView.RowFilter = String.Format("ID_AJUDA_CUSTO <> {0}", ajudaCusto.Id.ToString)

            grdAjudaCustoRelacao.DataSource = Nothing
            grdAjudaCustoRelacao.DataBind()

            grdAjudaCustoRelacao.DataSource = dt.DefaultView
            grdAjudaCustoRelacao.DataBind()

            BtnGravar.Enabled = True

            up1.Update()

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub grdAjudaCustoRelacao_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles grdAjudaCustoRelacao.PageIndexChanging
        Try
            Dim dt As DataTable = DirectCast(Session("DataTableAjudasRelacao"), DataTable)
            grdAjudaCustoRelacao.DataSource = dt
            grdAjudaCustoRelacao.PageIndex = e.NewPageIndex
            grdAjudaCustoRelacao.DataBind()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub grdAjudaCustoPerito_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles grdAjudaCustoPerito.PageIndexChanging
        Try
            Dim listaDeAjudas As List(Of AjudaCusto) = IIf(Session("ListaAjudasCustoPerito") Is Nothing, New List(Of AjudaCusto), DirectCast(Session("ListaAjudasCustoPerito"), List(Of AjudaCusto)))

            If listaDeAjudas.Count > 0 Then
                Dim listaOrdenada As List(Of AjudaCusto) = listaDeAjudas.OrderBy(Function(x) x.Oficio).ToList
                grdAjudaCustoPerito.DataSource = listaOrdenada
                grdAjudaCustoPerito.PageIndex = e.NewPageIndex
                grdAjudaCustoPerito.DataBind()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    'Protected Sub btnSim_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSim.Click
    '    Try
    '        InserirAjudaDeCusto()
    '        up1.Update()
    '    Catch ex As Exception
    '        MsgErro(ex.Message)
    '    End Try
    'End Sub

    'Protected Sub btnNao_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNao.Click
    '    Try
    '        LimparAjudaCusto()
    '        up1.Update()
    '    Catch ex As Exception
    '        MsgErro(ex.Message)
    '    End Try
    'End Sub

End Class