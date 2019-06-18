
'------------------------------------------------
' 08/03/2010 11:07:32
'------------------------------------------------


<Serializable()> _
Public Class EntTipoLogradouro

    Private nCod_Tip_Logr As Integer
    Private sDescr As String

#Region " Propriedades "

    Public Property Cod_Tip_Logr() As Integer
        Get
            Return nCod_Tip_Logr
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_Logr = values
        End Set
    End Property

    Public Property Descr() As String
        Get
            Return sDescr
        End Get
        Set(ByVal values As String)
            sDescr = values
        End Set
    End Property

#End Region

End Class
