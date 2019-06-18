<Serializable()> _
Public Class EntEspecialidade_Perito

    'COD_ESPECIALIDADE NUMBER(4) not null,
    'COD_PROFISSAO     NUMBER not null,
    'ID_PF             NUMBER(14) not null,
    'COD_ORGAO_PER     NUMBER,
    'NUM_REG           Varchar2(15)
    'UF                Varchar2(2)

    Private nCOD_ESPECIALIDADE As Integer
    Private nCOD_PROFISSAO As Integer
    Private nID_PF As Long
    Private nCOD_ORGAO_PER As Integer
    Private nNUM_REG As String
    Private sUF As String


#Region " Propriedades "

    Public Property COD_ESPECIALIDADE() As Integer
        Get
            Return nCOD_ESPECIALIDADE
        End Get
        Set(ByVal values As Integer)
            nCOD_ESPECIALIDADE = values
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

    Public Property ID_PF() As Long
        Get
            Return nID_PF
        End Get
        Set(ByVal values As Long)
            nID_PF = values
        End Set
    End Property

    Public Property COD_ORGAO_PER() As Integer
        Get
            Return nCOD_ORGAO_PER
        End Get
        Set(ByVal values As Integer)
            nCOD_ORGAO_PER = values
        End Set
    End Property
    Public Property Num_Reg() As String
        Get
            Return nNUM_REG
        End Get
        Set(ByVal values As String)
            nNUM_REG = values
        End Set
    End Property

    Public Property UF() As String
        Get
            Return sUF
        End Get
        Set(ByVal value As String)
            sUF = value
        End Set
    End Property

#End Region

End Class
