<Serializable()> _
Public Class EntPagamento_Perito
    'ID_PF             NUMBER(14) not null,
    'DATA_AUTORIZACAO  DATE,
    'DATA_CANCELAMENTO DATE,
    'NUM_CNJ           VARCHAR2(25) not null,
    'COD_TIPO_PERICIA  NUMBER(2),
    'COD_ESPECIALIDADE NUMBER(4) not null,
    'NUM_PROT          NUMBER(11),
    'DATA_ENVIO_DGPCF  DATE
    Private nID_PF As Long
    Private dDATA_AUTORIZACAO As Date
    Private dDATA_CANCELAMENTO As Date
    Private sNUM_CNJ As String
    Private nCOD_TIPO_PERICIA As Integer
    Private nCOD_ESPECIALIDADE As Integer
    Private nNUM_PROT As Long
    Private dDATA_ENVIO_DGPCF As Date

#Region " Propriedades "
    Public Property ID_PF() As Long
        Get
            Return nID_PF
        End Get
        Set(ByVal values As Long)
            nID_PF = values
        End Set
    End Property
    Public Property DATA_AUTORIZACAO() As Date
        Get
            Return dDATA_AUTORIZACAO
        End Get
        Set(ByVal values As Date)
            dDATA_AUTORIZACAO = values
        End Set
    End Property
    Public Property DATA_CANCELAMENTO() As Date
        Get
            Return dDATA_CANCELAMENTO
        End Get
        Set(ByVal values As Date)
            dDATA_CANCELAMENTO = values
        End Set
    End Property
    Public Property NUM_CNJ() As String
        Get
            Return sNUM_CNJ
        End Get
        Set(ByVal values As String)
            sNUM_CNJ = values
        End Set
    End Property
    Public Property COD_TIPO_PERICIA() As Integer
        Get
            Return nCOD_TIPO_PERICIA
        End Get
        Set(ByVal values As Integer)
            nCOD_TIPO_PERICIA = values
        End Set
    End Property
    Public Property COD_ESPECIALIDADE() As Integer
        Get
            Return nCOD_ESPECIALIDADE
        End Get
        Set(ByVal values As Integer)
            nCOD_ESPECIALIDADE = values
        End Set
    End Property
    Public Property NUM_PROT() As Long
        Get
            Return nNUM_PROT
        End Get
        Set(ByVal values As Long)
            nNUM_PROT = values
        End Set
    End Property
    Public Property DATA_ENVIO_DGPCF() As Date
        Get
            Return dDATA_ENVIO_DGPCF
        End Get
        Set(ByVal values As Date)
            dDATA_ENVIO_DGPCF = values
        End Set
    End Property
    
#End Region

End Class

