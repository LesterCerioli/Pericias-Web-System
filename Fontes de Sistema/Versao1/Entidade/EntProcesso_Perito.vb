Public Class EntProcesso_Perito
    'NUM_CNJ        NUMBER(20) not null,
    'ID_PF          NUMBER(14) not null,
    'DATA_NOMEACAO  DATE,
    'DATA_LIBERACAO DATE
    'SIGLA_NOMEACAO VARCHAR2(20)
    Private nNUM_CNJ As String
    Private nID_PF As Long
    Private dData_Nomeacao As Date
    Private dData_Liberacao As Date
    Private dData_Cancelamento As Date
    Private sSIGLA_NOMEACAO As String
    Private nPRAZO_ENTREGA As Integer
    Private nCOD_ESPECIALIDADE As Integer
    Private sDESCR_ESPECIALIDADE As String
    Private nCOD_PROFISSAO As Integer
    Private sDESCR_PROFISSAO As String
    Private nNum_Oficio As Integer
    Private nAno_Oficio As Integer
    Private nJustica_Gratuita As String
    Private nCod_Tipo_Pericia As Integer
    Private sIndEmailEnviado As String 'Indicar de envio de email na nomeação do perito.N - email não foi enviado, S - email foi enviado
    Private sIndTacita As String
    Private nHonorarios_Juiz As Decimal 'Honorários estipulados juiz depois da análise do pedido de honoráriios do perito
    Private dData_Inicio_Per As Date 'Após o clique no botão Iniciar será registrada a data de iníicio
    Private sInterdicao_per As String 'Flag de identificação de Interdição de Psiquiatria
    Private nID_Nomeacao As Integer
    Private dData_Novo_Hon As Date

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
            Return dData_Nomeacao
        End Get
        Set(ByVal values As Date)
            dData_Nomeacao = values
        End Set
    End Property
    Public Property Data_Cancelamento() As Date
        Get
            Return dData_Cancelamento
        End Get
        Set(ByVal values As Date)
            dData_Cancelamento = values
        End Set
    End Property
    Public Property Data_Liberacao() As Date
        Get
            Return dData_Liberacao
        End Get
        Set(ByVal values As Date)
            dData_Liberacao = values
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

    Public Property COD_TIPO_PERICIA() As Integer
        Get
            Return nCod_Tipo_Pericia
        End Get
        Set(ByVal values As Integer)
            nCod_Tipo_Pericia = values
        End Set
    End Property

    Public Property IndEmailEnviado() As String
        Get
            Return sIndEmailEnviado
        End Get
        Set(ByVal value As String)
            sIndEmailEnviado = value
        End Set
    End Property

    Public Property IndTacita() As String
        Get
            Return sIndTacita
        End Get
        Set(ByVal value As String)
            sIndTacita = value
        End Set
    End Property

    Public Property HonorarioJuiz() As Decimal
        Get
            Return nHonorarios_Juiz
        End Get
        Set(ByVal value As Decimal)
            nHonorarios_Juiz = value
        End Set
    End Property

    Public Property ID_Nomeacao() As Integer
        Get
            Return nID_Nomeacao
        End Get
        Set(ByVal values As Integer)
            nID_Nomeacao = values
        End Set
    End Property

    Public Property Data_Inicio_Per() As Date
        Get
            Return dData_Inicio_Per
        End Get
        Set(ByVal values As Date)
            dData_Inicio_Per = values
        End Set
    End Property
    Public Property Interdicao_Per() As String
        Get
            Return sInterdicao_per
        End Get
        Set(ByVal value As String)
            sInterdicao_per = value
        End Set
    End Property

    Public Property Data_Novo_Hon() As Date
        Get
            Return dData_Novo_Hon
        End Get
        Set(ByVal values As Date)
            dData_Novo_Hon = values
        End Set
    End Property
#End Region

End Class
