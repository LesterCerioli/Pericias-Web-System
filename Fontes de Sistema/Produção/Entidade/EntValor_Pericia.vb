
<Serializable()> _
Public Class EntValor_Pericia

    Private nCOD_TIPO_PERICIA As Integer
    Private sDESCR_TIPO_PERICIA As String
    Private nVALOR As Double
    Private dDATA_TIPO_PERICIA As Date
    Private dDATA_EXCLUSAO As Date
    'Tipo_Pericia(Tabela) - PREENCHIMENTO MANUAL (SEM MOVIMENTAÇÃO)
    'COD_TIPO_PERICIA   NUMBER(2) not null,
    'DESCR_TIPO_PERICIA VARCHAR2(50)
    'DATA_EXCLUSAO DATE (USO MANUAL)
    'Valor_Pericia(Tabela)
    'COD_TIPO_PERICIA  NUMBER(2) not null,
    'VALOR             NUMBER,
    'DATA_TIPO_PERICIA DATE not null
    'DATA_EXCLUSAO DATA

#Region " Propriedades "

    Public Property COD_TIPO_PERICIA() As Integer
        Get
            Return nCOD_TIPO_PERICIA
        End Get
        Set(ByVal values As Integer)
            nCOD_TIPO_PERICIA = values
        End Set
    End Property

    Public Property DESCR_TIPO_PERICIA() As String
        Get
            Return sDESCR_TIPO_PERICIA
        End Get
        Set(ByVal values As String)
            sDESCR_TIPO_PERICIA = values
        End Set
    End Property


    Public Property VALOR() As Double
        Get
            Return nVALOR
        End Get
        Set(ByVal values As Double)
            nVALOR = values
        End Set
    End Property

    Public Property DATA_TIPO_PERICIA() As Date
        Get
            Return dDATA_TIPO_PERICIA
        End Get
        Set(ByVal values As Date)
            dDATA_TIPO_PERICIA = values
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

#End Region

End Class
