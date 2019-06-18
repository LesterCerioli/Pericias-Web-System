'------------------------------------------------
' 08/03/2010 11:07:32
'------------------------------------------------


<Serializable()> _
Public Class EntCidade

    Private nCod_Cid As Integer
    Private sNome As String
    Private sSigla_UF As String
    Private sSit_Cid As String

#Region " Propriedades "

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

    Public Property Sigla_UF() As String
        Get
            Return sSigla_UF
        End Get
        Set(ByVal values As String)
            sSigla_UF = values
        End Set
    End Property

    Public Property Sit_Cid() As String
        Get
            Return sSit_Cid
        End Get
        Set(ByVal values As String)
            sSit_Cid = values
        End Set
    End Property

#End Region

End Class


