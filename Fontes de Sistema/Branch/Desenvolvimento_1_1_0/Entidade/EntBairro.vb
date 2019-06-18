'------------------------------------------------
' 16/03/2010 11:07:32
'------------------------------------------------

<Serializable()> _
Public Class EntBairro

    Private nCod_Bai As Integer
    Private nCod_Cid As Integer
    Private sNome As String

#Region " Propriedades "

    Public Property Cod_Bai() As Integer
        Get
            Return nCod_Bai
        End Get
        Set(ByVal values As Integer)
            nCod_Bai = values
        End Set
    End Property
    Public Property Cod_Cid() As Integer
        Get
            Return nCod_Cid
        End Get
        Set(ByVal values As Integer)
            nCod_Cid = values
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return sNome
        End Get
        Set(ByVal values As String)
            sNome = values
        End Set
    End Property
 

#End Region

End Class
