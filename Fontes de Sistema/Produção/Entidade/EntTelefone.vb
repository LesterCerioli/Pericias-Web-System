<Serializable()> _
Public Class EntTelefone

    Private nSeqTel As Integer
    Private sTEL As String
    Private sRAMAL As String
    Private sDDD As String
    Private nCod_Tip_Tel As Integer

#Region "Propriedades"
    Public Property SeqTel() As Integer
        Get
            Return nSeqTel
        End Get
        Set(ByVal value As Integer)
            nSeqTel = value
        End Set
    End Property

    Public Property Tel() As String
        Get
            Return sTEL
        End Get
        Set(ByVal value As String)
            sTEL = value
        End Set
    End Property

    Public Property Ramal() As String
        Get
            Return sRAMAL
        End Get
        Set(ByVal value As String)
            sRAMAL = value
        End Set
    End Property


    Public Property DDD() As String
        Get
            Return sDDD
        End Get
        Set(ByVal value As String)
            sDDD = value
        End Set
    End Property

    Public Property Cod_Tip_Tel() As Integer
        Get
            Return nCod_Tip_Tel
        End Get
        Set(ByVal value As Integer)
            nCod_Tip_Tel = value
        End Set
    End Property

#End Region

End Class
