Public Class EntAnotacao

    Private nID_PF As Integer
    Private sDESCR_ANOTACAO As String
    Private dDATA_ANOTACAO As Date
    Private dDATA_EXCLUSAO As Date
    Private sSIGLA As String
    Private nCod_Tipo_Anotacao As Integer
    Private nNum_Anotacao As Integer


    'ID_PF          NUMBER not null,
    'DESCR_ANOTACAO VARCHAR2(1000) not null,
    'DATA_ANOTACAO  DATE not null,
    'DATA_EXCLUSAO  DATE,
    'SIGLA          VARCHAR2(20)

#Region " Propriedades "

    Public Property ID_PF() As Integer
        Get
            Return nID_PF
        End Get
        Set(ByVal values As Integer)
            nID_PF = values
        End Set
    End Property

    Public Property DESCR_ANOTACAO() As String
        Get
            Return sDESCR_ANOTACAO
        End Get
        Set(ByVal values As String)
            sDESCR_ANOTACAO = values
        End Set
    End Property
    Public Property DATA_ANOTACAO() As Date
        Get
            Return dDATA_ANOTACAO
        End Get
        Set(ByVal values As Date)
            dDATA_ANOTACAO = values
        End Set
    End Property
    Public Property DATA_EXCLUSAO() As Date
        Get
            Return dDATA_EXCLUSAO
        End Get
        Set(ByVal values As Date)
            dDATA_EXCLUSAO = values
        End Set
    End Property
    Public Property SIGLA() As string
        Get
            Return sSIGLA
        End Get
        Set(ByVal values As String)
            sSIGLA = values
        End Set
    End Property
    Public Property Cod_Tipo_Anotacao() As Integer
        Get
            Return nCod_Tipo_Anotacao
        End Get
        Set(ByVal values As Integer)
            nCod_Tipo_Anotacao = values
        End Set
    End Property
    Public Property Num_Anotacao() As Integer
        Get
            Return nNum_Anotacao
        End Get
        Set(ByVal values As Integer)
            nNum_Anotacao = values
        End Set
    End Property
#End Region

End Class
