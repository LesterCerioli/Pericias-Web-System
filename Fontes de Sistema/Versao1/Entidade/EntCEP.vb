<Serializable()> _
Public Class EntCEP

    Private nCod_Bai As Integer
    Private sLogradouro As String
    Private sSigla_UF As String
    Private nCod_Cid As Integer

#Region " Propriedades "

    Public Property Cod_Bai() As Integer
        Get
            Return nCod_Bai
        End Get
        Set(ByVal values As Integer)
            nCod_Bai = values
        End Set

    End Property

    Public Property Logradouro() As String
        Get
            Return sLogradouro
        End Get
        Set(ByVal values As String)
            sLogradouro = values
        End Set
    End Property

    Public Property Sigla_UF() As String
        Get
            Return sSigla_UF
        End Get
        Set(ByVal values As String)
            sSigla_UF = values
        End Set
    End Property


    Public Property Cod_Cid() As Integer
        Get
            Return nCod_Cid
        End Get
        Set(ByVal values As Integer)
            nCod_Cid = values
        End Set
    End Property

#End Region


End Class
