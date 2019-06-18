

<Serializable()> _
Public Class EntTipo_Anotacao

    Private nCOD_TIPO_ANOTACAO As Integer
    Private nDESCR_TIPO_ANOTACAO As String
    Private dData_Exclusao As Date



#Region " Propriedades "

    Public Property COD_TIPO_ANOTACAO() As Integer
        Get
            Return nCOD_TIPO_ANOTACAO
        End Get
        Set(ByVal values As Integer)
            nCOD_TIPO_ANOTACAO = values
        End Set
    End Property

    Public Property DESCR_TIPO_ANOTACAO() As String
        Get
            Return nDESCR_TIPO_ANOTACAO
        End Get
        Set(ByVal values As String)
            nDESCR_TIPO_ANOTACAO = values
        End Set
    End Property
    Public Property Data_Exclusao() As Date
        Get
            Return dData_Exclusao
        End Get
        Set(ByVal values As Date)
            dData_Exclusao = values
        End Set
    End Property
#End Region

End Class

