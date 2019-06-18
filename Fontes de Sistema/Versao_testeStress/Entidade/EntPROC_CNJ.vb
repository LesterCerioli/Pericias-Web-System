'------------------------------------------------
' 12/07/2010 11:07:32
'------------------------------------------------

<Serializable()> _
Public Class EntPROC_CNJ

    Private sCod_PROC As String
    Private sCod_CNJ As String
    Private nID_Proc As Long
    Private nCod_Tip_Proc As Integer


#Region " Propriedades "

    Public Property Cod_Proc() As String
        Get
            Return sCod_PROC
        End Get
        Set(ByVal values As String)
            sCod_PROC = values
        End Set
    End Property
    Public Property Cod_CNJ() As String
        Get
            Return sCod_CNJ
        End Get
        Set(ByVal values As String)
            sCod_CNJ = values
        End Set
    End Property
    Public Property ID_Proc() As Long
        Get
            Return nID_Proc
        End Get
        Set(ByVal values As Long)
            nID_Proc = values
        End Set
    End Property
    Public Property Cod_Tip_Proc() As Integer
        Get
            Return nCod_Tip_Proc
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_Proc = values
        End Set
    End Property

#End Region

End Class
