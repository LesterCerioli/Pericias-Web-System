Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BalRelacaoPagamento
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

    Public Function ListaRelacoesPerito(ByVal codPerito As Long) As List(Of RelacaoPagamento)
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.ListaRelacoesPerito", sd.CriaRefCursor, codPerito)
            Dim lista As New List(Of RelacaoPagamento)

            If ds.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    lista.Add(GerarEntidade(linha))
                Next
            End If

            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function RetornaTodasRelacoesPagamento() As List(Of RelacaoPagamento)
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.RetornaTodasRelacoesPagamento", sd.CriaRefCursor)
            Dim lista As New List(Of RelacaoPagamento)

            If ds.Tables(0).Rows.Count > 0 Then
                For Each linha As DataRow In ds.Tables(0).Rows
                    lista.Add(GerarEntidade(linha))
                Next
            End If

            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function BuscaNomesSemelhantes(ByVal nome_relacao_pagamento As String) As DataSet
        Try
            Dim ds As DataSet = Nothing
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ListarRelacoesPgtoSemelhantes", sd.CriaRefCursor, nome_relacao_pagamento)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function RelatorioRelacoesPagamento(ByVal idRelacaoPagamento As Long) As DataSet
        Try
            Dim ds As DataSet = Nothing
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.RelatorioRelacoesPagamento", sd.CriaRefCursor, idRelacaoPagamento)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ListarRelacaoPagamento(ByVal id_relacao_pagamento As Long) As RelacaoPagamento
        Try
            Dim ds As DataSet = Nothing
            Dim relacaoPagamento As RelacaoPagamento = Nothing
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ListarRelacaoPagamento", sd.CriaRefCursor, id_relacao_pagamento)

            If ds.Tables(0).Rows.Count > 0 Then
                relacaoPagamento = GerarEntidade(ds.Tables(0).Rows(0))
            End If

            Return relacaoPagamento
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Gravar(ByVal relacaoPagamento As RelacaoPagamento) As Long
        Try
            Dim ds As DataSet = Nothing
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.InserirRelacaoPagamento", sd.CriaRefCursor,
                                                                            relacaoPagamento.Nome,
                                                                            IIf(relacaoPagamento.Definitiva, 1, 0),
                                                                            relacaoPagamento.DataCadastro)
            If ds.Tables(0).Rows.Count > 0 Then
                relacaoPagamento.Id = CLng(ds.Tables(0).Rows(0).Item(0))
            End If

            Return relacaoPagamento.Id
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Sub Alterar(ByVal relacaoPagamento As RelacaoPagamento)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.AlterarRelacaoPagamento", relacaoPagamento.Id,
                                                                    IIf(relacaoPagamento.Definitiva, 1, 0))
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub AlterarInformacoesEmLote(ByVal idRelacaoPagamento As Long, ByVal processoPagamento As String, ByVal dataProtocolo As String, ByVal dataPagamento As String)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.AlteraInfoRelacaoLote",
                           idRelacaoPagamento,
                           IIf(processoPagamento = Nothing, DBNull.Value, processoPagamento),
                           IIf(dataPagamento = Nothing, DBNull.Value, CDate(dataPagamento)),
                           IIf(dataProtocolo = Nothing, DBNull.Value, CDate(dataProtocolo)))
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function GerarEntidade(linha As DataRow) As RelacaoPagamento
        Try
            Dim relacaoPagamento As New RelacaoPagamento
            Dim balAjudaCusto As New BalAjudaCusto(Usuario)

            relacaoPagamento.Id = CLng(NVL(linha("ID_RELACAO_PAGAMENTO"), 0))
            relacaoPagamento.Nome = NVL(linha("NOME_RELACAO_PAGAMENTO"), Nothing)
            If CLng(NVL(linha("RELACAO_DEFINITIVA"), 0)) = 0 Then
                relacaoPagamento.Definitiva = False
            ElseIf CLng(NVL(linha("RELACAO_DEFINITIVA"), 0)) = 1 Then
                relacaoPagamento.Definitiva = True
            End If
            If CLng(NVL(linha("PAGAMENTO_LOTE"), 0)) = 0 Then
                relacaoPagamento.PagamentoLote = False
            ElseIf CLng(NVL(linha("PAGAMENTO_LOTE"), 0)) = 1 Then
                relacaoPagamento.PagamentoLote = True
            End If
            relacaoPagamento.DataCadastro = NVL(linha("DATA_CADASTRO"), Nothing)

            Return relacaoPagamento
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
