''' <summary>
''' Classe referente aos documentos do perito da tabela DOCUMENTO
''' </summary>
<Serializable()> _
Public Class DOCUMENTO

#Region "Construtor"

    Sub New()

    End Sub

#End Region

#Region "Propriedades"

    Private _codigoPerito As Int64
    Public Property CodigoPerito() As Int64
        Get
            Return _codigoPerito
        End Get
        Set(ByVal value As Int64)
            _codigoPerito = value
        End Set
    End Property

    Private _idGed As String
    Public Property GedId() As String
        Get
            Return _idGed
        End Get
        Set(ByVal value As String)
            _idGed = value
        End Set
    End Property

    Private _nomeArquivo As String
    Public Property NomeArquivo() As String
        Get
            Return _nomeArquivo
        End Get
        Set(ByVal value As String)
            _nomeArquivo = value
        End Set
    End Property


#End Region

End Class
