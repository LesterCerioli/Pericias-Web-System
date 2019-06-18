
<Serializable()> _
Public Class EntUSUARIO
    Private sCodUsuario As String
    Private sNomeUsuario As String

#Region "Propriedades"
    Public Property CodUsuario() As String
        Get
            Return sCodUsuario
        End Get
        Set(ByVal values As String)
            sCodUsuario = values
        End Set
    End Property

    Public Property NomeUsuario() As String
        Get
            Return sNomeUsuario
        End Get
        Set(ByVal values As String)
            sNomeUsuario = values
        End Set
    End Property
#End Region

End Class

