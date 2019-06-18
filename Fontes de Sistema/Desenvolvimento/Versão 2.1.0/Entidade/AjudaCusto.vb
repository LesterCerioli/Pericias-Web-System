''' <summary>
''' Classe referente aos status dos peritos da tabela AJUDA_CUSTO
''' </summary>
Public Class AjudaCusto

#Region "Construtor"

    Public Sub New()

    End Sub

#End Region

#Region "Propriedades"

    Private _sequencialTemporario As Long
    Public Property Sequencial() As Long
        Get
            Return _sequencialTemporario
        End Get
        Set(ByVal value As Long)
            _sequencialTemporario = value
        End Set
    End Property


    Private _idAjudaCusto As Long
    Public Property Id() As Long
        Get
            Return _idAjudaCusto
        End Get
        Set(ByVal value As Long)
            _idAjudaCusto = value
        End Set
    End Property

    Private _perito As EntPERITO
    Public Property Perito() As EntPERITO
        Get
            Return _perito
        End Get
        Set(ByVal value As EntPERITO)
            _perito = value
        End Set
    End Property

    Private _relacaoPagamento As RelacaoPagamento
    Public Property RelacaoPagamento() As RelacaoPagamento
        Get
            Return _relacaoPagamento
        End Get
        Set(ByVal value As RelacaoPagamento)
            _relacaoPagamento = value
        End Set
    End Property

    Private _profissao As EntProfissao
    Public Property Profissao() As EntProfissao
        Get
            Return _profissao
        End Get
        Set(ByVal value As EntProfissao)
            _profissao = value
        End Set
    End Property

    Private _especialidade As EntEspecialidade
    Public Property Especialidade() As EntEspecialidade
        Get
            Return _especialidade
        End Get
        Set(ByVal value As EntEspecialidade)
            _especialidade = value
        End Set
    End Property

    Private _especialidadePerito As EntEspecialidade_Perito
    Public Property EspecialidadeProfissaoPerito() As EntEspecialidade_Perito
        Get
            Return _especialidadePerito
        End Get
        Set(ByVal value As EntEspecialidade_Perito)
            _especialidadePerito = value
        End Set
    End Property


    Private _numeroOficio As String
    Public Property Oficio() As String
        Get
            Return _numeroOficio
        End Get
        Set(ByVal value As String)
            _numeroOficio = value
        End Set
    End Property

    Private _dataRecebimentoOficio As String
    Public Property DataRecebimento() As String
        Get
            Return _dataRecebimentoOficio
        End Get
        Set(ByVal value As String)
            _dataRecebimentoOficio = value
        End Set
    End Property

    Private _idProc As Long
    Public Property IdProcesso() As Nullable(Of Long)
        Get
            Return _idProc
        End Get
        Set(ByVal value As Nullable(Of Long))
            _idProc = value
        End Set
    End Property

    Private _numeroProcesso As String
    Public Property NumeroProcesso() As String
        Get
            Return _numeroProcesso
        End Get
        Set(ByVal value As String)
            _numeroProcesso = value
        End Set
    End Property

    Private _valor As Double
    Public Property Valor() As Double
        Get
            Return _valor
        End Get
        Set(ByVal value As Double)
            _valor = value
        End Set
    End Property

    Private _processoPagamento As String
    Public Property ProcessoPagamento() As String
        Get
            Return _processoPagamento
        End Get
        Set(ByVal value As String)
            _processoPagamento = value
        End Set
    End Property

    Private _dataProtocolo As String
    Public Property DataProtocolo() As String
        Get
            Return _dataProtocolo
        End Get
        Set(ByVal value As String)
            _dataProtocolo = value
        End Set
    End Property

    Private _dataPagamento As String
    Public Property DataPagamento() As String
        Get
            Return _dataPagamento
        End Get
        Set(ByVal value As String)
            _dataPagamento = value
        End Set
    End Property

    Private _observacao As String
    Public Property Observacao() As String
        Get
            Return _observacao
        End Get
        Set(ByVal value As String)
            _observacao = value
        End Set
    End Property

    Private _numProt As Long
    Public Property IdProcessoAdministrativo() As Nullable(Of Long)
        Get
            Return _numProt
        End Get
        Set(ByVal value As Nullable(Of Long))
            _numProt = value
        End Set
    End Property

    Private _dataExclusao As String
    Public Property DataExclusao() As String
        Get
            Return _dataExclusao
        End Get
        Set(ByVal value As String)
            _dataExclusao = value
        End Set
    End Property


#End Region

End Class
