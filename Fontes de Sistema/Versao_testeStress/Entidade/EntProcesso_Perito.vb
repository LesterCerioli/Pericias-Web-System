Public Class EntProcesso_Perito
    'NUM_CNJ        NUMBER(20) not null,
    'ID_PF          NUMBER(14) not null,
    'DATA_NOMEACAO  DATE,
    'DATA_NEGACAO   DATE,
    'DATA_ACEITACAO DATE,
    'DATA_LIBERACAO DATE
    'SIGLA_NOMEACAO VARCHAR2(20)
    Private nNUM_CNJ As String
    Private nID_PF As Long
    Private nData_Nomeacao As Date
    Private nData_Negacao As Date
    Private nData_Aceitacao As Date
    Private nData_Liberacao As Date
    Private sSIGLA_NOMEACAO As String
    Private nPRAZO_ENTREGA As Integer
    Private nCOD_ESPECIALIDADE As Integer
    Private sDESCR_ESPECIALIDADE As String
    Private nCOD_PROFISSAO As Integer
    Private sDESCR_PROFISSAO As String
    Private sMotivo_Recusa As String
    Private nHonorarios As Double
    Private nNum_Oficio As Integer
    Private nAno_Oficio As Integer
    Private nJustica_Gratuita As String


#Region " Propriedades "
    Public Property NUM_CNJ() As String
        Get
            Return nNUM_CNJ
        End Get
        Set(ByVal values As String)
            nNUM_CNJ = values
        End Set
    End Property
    Public Property ID_PF() As Long
        Get
            Return nID_PF
        End Get
        Set(ByVal values As Long)
            nID_PF = values
        End Set
    End Property
    Public Property Data_Nomeacao() As Date
        Get
            Return nData_Nomeacao
        End Get
        Set(ByVal values As Date)
            nData_Nomeacao = values
        End Set
    End Property
    Public Property Data_Negacao() As Date
        Get
            Return nData_Negacao
        End Get
        Set(ByVal values As Date)
            nData_Negacao = values
        End Set
    End Property
    Public Property Data_Aceitacao() As Date
        Get
            Return nData_Aceitacao
        End Get
        Set(ByVal values As Date)
            nData_Aceitacao = values
        End Set
    End Property
    Public Property Data_Liberacao() As Date
        Get
            Return nData_Liberacao
        End Get
        Set(ByVal values As Date)
            nData_Liberacao = values
        End Set
    End Property
    Public Property SIGLA_NOMEACAO() As String
        Get
            Return sSIGLA_NOMEACAO
        End Get
        Set(ByVal values As String)
            sSIGLA_NOMEACAO = values
        End Set
    End Property
    Public Property PRAZO_ENTREGA() As Integer
        Get
            Return nPRAZO_ENTREGA
        End Get
        Set(ByVal values As Integer)
            nPRAZO_ENTREGA = values
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
    Public Property DESCR_ESPECIALIDADE() As String
        Get
            Return sDESCR_ESPECIALIDADE
        End Get
        Set(ByVal values As String)
            sDESCR_ESPECIALIDADE = values
        End Set
    End Property
    Public Property COD_PROFISSAO() As Integer
        Get
            Return nCOD_PROFISSAO
        End Get
        Set(ByVal values As Integer)
            nCOD_PROFISSAO = values
        End Set
    End Property
    Public Property DESCR_PROFISSAO() As String
        Get
            Return sDESCR_PROFISSAO
        End Get
        Set(ByVal values As String)
            sDESCR_PROFISSAO = values
        End Set
    End Property
    Public Property Motivo_Recusa() As String
        Get
            Return sMotivo_Recusa
        End Get
        Set(ByVal values As String)
            sMotivo_Recusa = values
        End Set
    End Property
    Public Property Honorarios() As Double
        Get
            Return nHonorarios
        End Get
        Set(ByVal values As Double)
            nHonorarios = values
        End Set
    End Property
    Public Property Num_Oficio() As Integer
        Get
            Return nNum_Oficio
        End Get
        Set(ByVal values As Integer)
            nNum_Oficio = values
        End Set
    End Property

    Public Property Ano_Oficio() As Integer
        Get
            Return nAno_Oficio
        End Get
        Set(ByVal values As Integer)
            nAno_Oficio = values
        End Set
    End Property
    Public Property Justica_Gratuita() As String
        Get
            Return nJustica_Gratuita
        End Get
        Set(ByVal values As String)
            nJustica_Gratuita = values
        End Set
    End Property
#End Region

End Class
