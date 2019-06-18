Public Class EntAgenda

    'NUM_AGENDAMENTO NUMBER not null,
    'CPF             VARCHAR2(11) not null,
    'ID_PF           NUMBER not null,
    'DATA_AGENDADA   DATE not null,
    'HORA_AGENDADA   VARCHAR2(5) not null,
    'NUM_CNJ         VARCHAR2(25) not null,
    'DATA_REGISTRO   DATE,
    'COD_SERVENTIA   VARCHAR2(6),
    'ID_JUIZ         NUMBER


    Private nID_PF As Integer
    Private sDESCR_ANOTACAO As String
    Private dDATA_ANOTACAO As Date
    Private dDATA_EXCLUSAO As Date
    Private sSIGLA As String
    Private nCod_Tipo_Anotacao As Integer

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
    Public Property SIGLA() As String
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
#End Region

End Class
