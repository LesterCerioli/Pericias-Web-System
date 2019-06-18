<Serializable()> _
Public Class EntOrgao_per
    Private nCOD_ORGAO_PER As Integer
    Private sDESCR_ORGAO_PER As String
    Private sSIGLA_PER As String
    Private dDATA_EXCLUSAO As Date
    'Private sUF As String

#Region " Propriedades "

    Public Property COD_ORGAO_PER() As Integer
        Get
            Return nCOD_ORGAO_PER
        End Get
        Set(ByVal values As Integer)
            nCOD_ORGAO_PER = values
        End Set

    End Property

    Public Property DESCR_ORGAO_PER() As String
        Get
            Return sDESCR_ORGAO_PER
        End Get
        Set(ByVal values As String)
            sDESCR_ORGAO_PER = values
        End Set
    End Property

    Public Property SIGLA_PER() As String
        Get
            Return sSIGLA_PER
        End Get
        Set(ByVal values As String)
            sSIGLA_PER = values
        End Set
    End Property


    Public Property Data_Exclusao() As Date
        Get
            Return dDATA_EXCLUSAO
        End Get
        Set(ByVal values As Date)
            dDATA_EXCLUSAO = values
        End Set
    End Property
    'Public Property Uf() As String
    '    Get
    '        Return sUF
    '    End Get
    '    Set(ByVal values As String)
    '        sUF = values
    '    End Set
    'End Property
#End Region

End Class
