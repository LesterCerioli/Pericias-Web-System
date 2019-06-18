
<Serializable()> _
Public Class EntBANCO


    Private nCod_Banco As String
    Private nNome As String


#Region " Propriedades "

    Public Property Cod_Banco() As String
        Get
            Return nCod_Banco
        End Get
        Set(ByVal values As String)
            nCod_Banco = values
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return nNome
        End Get
        Set(ByVal values As String)
            nNome = values
        End Set
    End Property

#End Region

End Class
