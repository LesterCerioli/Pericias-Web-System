''' <summary>
''' Classe referente aos status dos peritos da tabela TIPO_STATUS
''' </summary>
<Serializable()> _
Public Class TIPO_STATUS

#Region "Propriedades"

    Public Sub New()
    End Sub

    Private COD_TIPO_STATUS As Int64
    Public Property Codigo() As Int64
        Get
            Return COD_TIPO_STATUS
        End Get
        Set(ByVal value As Int64)
            COD_TIPO_STATUS = value
        End Set
    End Property

    Private DESC_STATUS As String
    Public Property Descricao() As String
        Get
            Return DESC_STATUS
        End Get
        Set(ByVal value As String)
            DESC_STATUS = value
        End Set
    End Property

#End Region


#Region "Métodos"


#End Region

End Class
