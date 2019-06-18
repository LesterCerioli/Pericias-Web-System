''' <summary>
''' Classe referente aos status dos peritos da tabela RELACAO_PAGAMENTO
''' </summary>
<Serializable()> _
Public Class RelacaoPagamento

#Region "Construtor"
    Public Sub New()

    End Sub

#End Region

#Region "Propriedades"

    Private _idRelacaoPagamento As Long
    Public Property Id() As Long
        Get
            Return _idRelacaoPagamento
        End Get
        Set(ByVal value As Long)
            _idRelacaoPagamento = value
        End Set
    End Property

    Private _nomeRelacaoPagamento As String
    Public Property Nome() As String
        Get
            Return _nomeRelacaoPagamento
        End Get
        Set(ByVal value As String)
            _nomeRelacaoPagamento = value
        End Set
    End Property

    Private _relacaoDefinitiva As Boolean
    Public Property Definitiva() As Boolean
        Get
            Return _relacaoDefinitiva
        End Get
        Set(ByVal value As Boolean)
            _relacaoDefinitiva = value
        End Set
    End Property

    Private _dataCadastro As Date
    Public Property DataCadastro() As Date
        Get
            Return _dataCadastro
        End Get
        Set(ByVal value As Date)
            _dataCadastro = value
        End Set
    End Property

    Private _pagamentoLote As Boolean
    Public Property PagamentoLote() As Boolean
        Get
            Return _pagamentoLote
        End Get
        Set(ByVal value As Boolean)
            _pagamentoLote = value
        End Set
    End Property


    'Private _listaAjudaCusto As List(Of AjudaCusto)
    'Public Property AjudasDeCusto() As List(Of AjudaCusto)
    '    Get
    '        Return _listaAjudaCusto
    '    End Get
    '    Set(ByVal value As List(Of AjudaCusto))
    '        _listaAjudaCusto = value
    '    End Set
    'End Property

#End Region

End Class
