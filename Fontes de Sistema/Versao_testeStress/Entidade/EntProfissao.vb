

<Serializable()> _
Public Class EntProfissao


    Private nCod_Profissao As Integer
    Private sDescr_Profissao As String
    Private dData_Exclusao As Date


#Region " Propriedades "

    Public Property Cod_Profissao() As Integer
        Get
            Return nCod_Profissao
        End Get
        Set(ByVal values As Integer)
            nCod_Profissao = values
        End Set
    End Property

    Public Property Descr_Profissao() As String
        Get
            Return sDescr_Profissao
        End Get
        Set(ByVal values As String)
            sDescr_Profissao = values
        End Set
    End Property

    Public Property Data_Exclusao() As Date
        Get
            Return dData_Exclusao
        End Get
        Set(ByVal values As Date)
            dData_Exclusao = values
        End Set
    End Property

#End Region

End Class

