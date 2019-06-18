

<Serializable()> _
Public Class EntEspecialidade


    Private nCod_Especialidade As Integer
    Private nDescr_Especialidade As String
    Private nCod_Profissao As Integer
    Private nData_Exclusao As Date


#Region " Propriedades "

    Public Property Cod_Especialidade() As Integer
        Get
            Return nCod_Especialidade
        End Get
        Set(ByVal values As Integer)
            nCod_Especialidade = values
        End Set
    End Property

    Public Property Descr_Especialidade() As String
        Get
            Return nDescr_Especialidade
        End Get
        Set(ByVal values As String)
            nDescr_Especialidade = values
        End Set
    End Property
    Public Property Cod_Profissao() As Integer
        Get
            Return nCod_Profissao
        End Get
        Set(ByVal values As Integer)
            nCod_Profissao = values
        End Set
    End Property
    Public Property Data_Exclusao() As Date
        Get
            Return nData_Exclusao
        End Get
        Set(ByVal values As Date)
            nData_Exclusao = values
        End Set
    End Property

#End Region

End Class

