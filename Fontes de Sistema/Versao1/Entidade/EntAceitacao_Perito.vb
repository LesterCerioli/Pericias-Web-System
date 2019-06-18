Public Class EntAceitacao_Perito

    'NUM_CNJ        NUMBER(20) not null,
    'ID_PF          NUMBER(14) not null,
    'DATA_NOMEACAO  DATE,
    'DATA_NEGACAO   DATE,
    'DATA_ACEITACAO DATE,
    'DATA_LIBERACAO DATE
    'SIGLA_NOMEACAO VARCHAR2(20)
    Private nID_Nomeacao As Integer
    Private dData_Negacao As Date
    Private dData_Aceitacao As Date
    Private sSIGLA_ACEITACAO As String
    Private sMotivo_Recusa As String
    Private nHonorarios As Double

#Region " Propriedades "
    Public Property ID_Nomeacao() As Integer
        Get
            Return nID_Nomeacao
        End Get
        Set(ByVal values As Integer)
            nID_Nomeacao = values
        End Set
    End Property
    Public Property Data_Negacao() As Date
        Get
            Return dData_Negacao
        End Get
        Set(ByVal values As Date)
            dData_Negacao = values
        End Set
    End Property
    Public Property Data_Aceitacao() As Date
        Get
            Return dData_Aceitacao
        End Get
        Set(ByVal values As Date)
            dData_Aceitacao = values
        End Set
    End Property
    Public Property SIGLA_ACEITACAO() As String
        Get
            Return sSIGLA_ACEITACAO
        End Get
        Set(ByVal values As String)
            sSIGLA_ACEITACAO = values
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

#End Region

End Class
