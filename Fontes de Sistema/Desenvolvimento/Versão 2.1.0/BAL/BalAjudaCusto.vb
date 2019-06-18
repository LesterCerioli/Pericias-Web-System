Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BalAjudaCusto
    Inherits BaseBAL

    Private _usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario
    Public Property Usuario() As EstruturaPadrao.EstruturaIdentificacaoUsuario
        Get
            Return _usuario
        End Get
        Set(ByVal value As EstruturaPadrao.EstruturaIdentificacaoUsuario)
            _usuario = value
        End Set
    End Property


    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
        Me.Usuario = Usuario
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function Carregar(ByVal idAjudaCusto As Long) As AjudaCusto
        sd.Open()
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.CarregaAjudaCusto", sd.CriaRefCursor, idAjudaCusto)

            If ds.Tables(0).Rows.Count > 0 Then
                Return GerarEntidade(ds.Tables(0).Rows(0), 0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ConsultarPagamentosLista(ByVal codPerito As Long,
                                                  ByVal codProfissao As Long,
                                                  ByVal codEspecialidade As Long,
                                                  ByVal idRelacaoPagamento As Long,
                                                  ByVal idProcesso As Long,
                                                  ByVal processoPagamento As String,
                                                  ByVal dataProtocoloInicial As String,
                                                  ByVal dataProtocoloFinal As String,
                                                  ByVal dataPagamentoInicial As String,
                                                  ByVal dataPagamentoFinal As String) As List(Of AjudaCusto)
        Try
            Dim lista As New List(Of AjudaCusto)
            Dim ds = ConsultarPagamentosRealizados(codPerito,
                                                   codProfissao,
                                                   codEspecialidade,
                                                   idRelacaoPagamento,
                                                   idProcesso,
                                                   processoPagamento,
                                                   dataProtocoloInicial,
                                                   dataProtocoloFinal,
                                                   dataPagamentoInicial,
                                                   dataPagamentoFinal)

            If ds IsNot Nothing Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    lista.Add(GerarEntidade(linha, 0))
                Next
            End If

            Return lista
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ConsultarPagamentosRealizados(ByVal codPerito As Long,
                                                  ByVal codProfissao As Long,
                                                  ByVal codEspecialidade As Long,
                                                  ByVal idRelacaoPagamento As Long,
                                                  ByVal idProcesso As Long,
                                                  ByVal processoPagamento As String,
                                                  ByVal dataProtocoloInicial As String,
                                                  ByVal dataProtocoloFinal As String,
                                                  ByVal dataPagamentoInicial As String,
                                                  ByVal dataPagamentoFinal As String) As DataSet
        sd.Open()
        Try
            Dim ds = sd.ExecutaProcDS("NOVO_PERICIAS.ConsultaPagamentosRealizados", sd.CriaRefCursor,
                                    codPerito,
                                    codProfissao,
                                    codEspecialidade,
                                    idRelacaoPagamento,
                                    idProcesso,
                                    IIf(String.IsNullOrEmpty(processoPagamento), DBNull.Value, processoPagamento),
                                    IIf(String.IsNullOrEmpty(dataProtocoloInicial), DBNull.Value, dataProtocoloInicial),
                                    IIf(String.IsNullOrEmpty(dataProtocoloFinal), DBNull.Value, dataProtocoloFinal),
                                    IIf(String.IsNullOrEmpty(dataPagamentoInicial), DBNull.Value, dataPagamentoInicial),
                                    IIf(String.IsNullOrEmpty(dataPagamentoFinal), DBNull.Value, dataPagamentoFinal))

            If ds.Tables(0).Rows.Count = 0 Then
                Return Nothing
            Else
                Return ds
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function


    Public Function ListarAjudasCustoRelacao(ByVal idRelacaoPagamento As Long) As List(Of AjudaCusto)
        sd.Open()
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.ListarAjudasCustoRelacao", sd.CriaRefCursor, idRelacaoPagamento)
            Dim lista As New List(Of AjudaCusto)
            'Dim i As Long = 1

            If ds.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    lista.Add(GerarEntidade(linha, 0))
                    'i = i + 1
                Next
            End If

            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ListarAjudasCustoPerito(ByVal idRelacaoPagamento As Long, ByVal codPerito As Long) As List(Of AjudaCusto)
        sd.Open()
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.ListarAjudasCustoPerito", sd.CriaRefCursor, idRelacaoPagamento, codPerito)
            Dim lista As New List(Of AjudaCusto)
            'Dim i As Long = 1

            If ds.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    lista.Add(GerarEntidade(linha, 0))
                    'i = i + 1
                Next
            End If

            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ConsultarAjudasCustoRelacao(ByVal idRelacaoPagamento As Long) As DataSet
        sd.Open()
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.ConsultarAjudasCustoRelacao", sd.CriaRefCursor, idRelacaoPagamento)

            If ds.Tables(0).Rows.Count = 0 Then
                Return Nothing
            End If

            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ListarOcorrenciasPagamento(ByVal numeroProcesso As String, ByVal tipoProcesso As Integer, ByVal idRelacaoPagamento As Long) As DataSet
        sd.Open()
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.ListarOcorrenciasPagamento", sd.CriaRefCursor, numeroProcesso, tipoProcesso, idRelacaoPagamento)

            If ds.Tables(0).Rows.Count = 0 Then
                Return Nothing
            End If

            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Sub Alterar(ByVal listaAjudasCusto As List(Of AjudaCusto))
        sd.Open()
        Try
            Dim listaParaAlterar As List(Of AjudaCusto) = listaAjudasCusto.FindAll(Function(x) x.Id <> 0)

            For Each ac As AjudaCusto In listaParaAlterar
                sd.ExecutaProc("NOVO_PERICIAS.AlterarAjudaCusto", ac.RelacaoPagamento.Id, _
                                                    ac.Id, _
                                                    ac.Perito.Cod_Perito, _
                                                    ac.Profissao.Cod_Profissao, _
                                                    ac.Especialidade.Cod_Especialidade, _
                                                    ac.Oficio, _
                                                    CDate(ac.DataRecebimento), _
                                                    IIf(ac.IdProcesso = 0, DBNull.Value, ac.IdProcesso), _
                                                    ac.NumeroProcesso, _
                                                    ac.Valor, _
                                                    NVL(ac.ProcessoPagamento, DBNull.Value), _
                                                    NVL(ac.DataProtocolo, DBNull.Value), _
                                                    NVL(ac.DataPagamento, DBNull.Value), _
                                                    NVL(ac.Observacao, DBNull.Value), _
                                                    IIf(ac.IdProcessoAdministrativo = 0, DBNull.Value, ac.IdProcessoAdministrativo))
            Next

        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Gravar(ByVal ajudasCusto As List(Of AjudaCusto)) As Long
        sd.Open()
        Try
            Dim listaParaInserir As List(Of AjudaCusto) = ajudasCusto.FindAll(Function(x) x.Id = 0)
            Dim balRelacaoPagamento As New BalRelacaoPagamento(Usuario)
            Dim idRelacaoPagamento As Long = 0

            If ajudasCusto(0).RelacaoPagamento.Id = 0 Or ajudasCusto(0).RelacaoPagamento.Id = Nothing Then
                idRelacaoPagamento = balRelacaoPagamento.Gravar(ajudasCusto(0).RelacaoPagamento)
            End If

            For Each ac As AjudaCusto In listaParaInserir
                If idRelacaoPagamento <> 0 Then
                    ac.RelacaoPagamento.Id = idRelacaoPagamento
                End If

                If ac.Id = 0 Or ac.Id = Nothing Then
                    sd.ExecutaProc("NOVO_PERICIAS.GravarAjudaCusto", ac.RelacaoPagamento.Id, _
                                                        ac.Perito.Cod_Perito, _
                                                        ac.Profissao.Cod_Profissao, _
                                                        ac.Especialidade.Cod_Especialidade, _
                                                        ac.Oficio, _
                                                        CDate(ac.DataRecebimento), _
                                                        IIf(ac.IdProcesso = 0, DBNull.Value, ac.IdProcesso), _
                                                        ac.NumeroProcesso, _
                                                        ac.Valor, _
                                                        NVL(ac.ProcessoPagamento, DBNull.Value), _
                                                        NVL(ac.DataProtocolo, DBNull.Value), _
                                                        NVL(ac.DataPagamento, DBNull.Value), _
                                                        NVL(ac.Observacao, DBNull.Value), _
                                                        IIf(ac.IdProcessoAdministrativo = 0, DBNull.Value, ac.IdProcessoAdministrativo))
                End If

            Next

            Return idRelacaoPagamento
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Sub Excluir(ByVal listaAjudasCusto As List(Of AjudaCusto))
        sd.Open()
        Try
            Dim listaParaExcluir = listaAjudasCusto.FindAll(Function(x) x.Id <> 0)

            If listaParaExcluir.Count > 0 Then
                For Each ac As AjudaCusto In listaParaExcluir
                    sd.ExecutaProc("NOVO_PERICIAS.ExcluirAjudaCusto", _
                                   ac.RelacaoPagamento.Id, _
                                   ac.Id)
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function GerarEntidade(linha As DataRow, ByVal seq As Long) As AjudaCusto
        Try
            Dim ajudaCusto As New AjudaCusto
            Dim balPerito As New BALPERITO(Usuario)
            Dim balProfissao As New BALProfissao(Usuario)
            Dim balEspecialidade As New BALEspecialidade(Usuario)
            Dim balEspecialidadePerito As New BALEspecialidadePerito(Usuario)
            Dim balRelacao As New BalRelacaoPagamento(Usuario)

            ajudaCusto.Sequencial = seq
            ajudaCusto.Id = NVL(linha("ID_AJUDA_CUSTO"), 0)
            ajudaCusto.Perito = balPerito.Carregar(CLng(linha("COD_PERITO")))
            ajudaCusto.RelacaoPagamento = balRelacao.ListarRelacaoPagamento(CLng(linha("ID_RELACAO_PAGAMENTO")))
            ajudaCusto.Profissao = balProfissao.Carregar(CLng(linha("COD_PROFISSAO")))
            ajudaCusto.Especialidade = balEspecialidade.Carregar(CLng(linha("COD_ESPECIALIDADE")), CLng(linha("COD_PROFISSAO")))
            ajudaCusto.EspecialidadeProfissaoPerito = balEspecialidadePerito.Consultar(CLng(linha("COD_PERITO")), CInt(linha("COD_PROFISSAO")), CInt(linha("COD_ESPECIALIDADE")))
            ajudaCusto.Oficio = NVL(linha("NUMERO_OFICIO"), Nothing)
            ajudaCusto.DataRecebimento = NVL(linha("DATA_RECEBIMENTO_OFICIO"), Nothing)
            ajudaCusto.IdProcesso = CLng(NVL(linha("ID_PROC"), 0))
            ajudaCusto.NumeroProcesso = NVL(linha("NUMERO_PROCESSO"), Nothing)
            ajudaCusto.Valor = NVL(linha("VALOR"), Nothing)
            ajudaCusto.ProcessoPagamento = NVL(linha("PROCESSO_PAGAMENTO"), Nothing)
            ajudaCusto.DataProtocolo = NVL(linha("DATA_PROTOCOLO"), Nothing)
            ajudaCusto.DataPagamento = NVL(linha("DATA_PAGAMENTO"), Nothing)
            ajudaCusto.Observacao = NVL(linha("OBSERVACAO"), Nothing)
            ajudaCusto.IdProcessoAdministrativo = CLng(NVL(linha("NUM_PROT"), 0))
            ajudaCusto.DataExclusao = NVL(linha("DATA_EXCLUSAO"), Nothing)

            Return ajudaCusto
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
