Public Class EntPerito_Comarca

    Private nID_PF As Long
    Private nCod_Comarca As Integer
    Private nCod_Nur As Integer
    Private nCod_Perito As Int64


#Region " Propriedades "

    Public Property ID_PF() As Long
        Get
            Return nID_PF
        End Get
        Set(ByVal values As Long)
            nID_PF = values
        End Set
    End Property

    Public Property Cod_Comarca() As String
        Get
            Return nCod_Comarca
        End Get
        Set(ByVal values As String)
            nCod_Comarca = values
        End Set
    End Property

    Public Property Cod_Nur() As String
        Get
            Return nCod_Nur
        End Get
        Set(ByVal values As String)
            nCod_Nur = values
        End Set
    End Property

    Public Property Cod_Perito() As Int64
        Get
            Return nCod_Perito
        End Get
        Set(ByVal value As Int64)
            nCod_Perito = value
        End Set
    End Property


#End Region
End Class
