'------------------------------------------------
' 01/08/2007 11:07:32
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------


<Serializable()> _
Public Class EntORGAOJUIZ

    Private sNumMatr as String
    Private nCodOrg as Integer
    Private nCodTipDesg as Integer

#Region " Propriedades " 

	Public Property NumMatr() as String
		Get
			Return sNumMatr
		End Get
		Set(ByVal values as String)
			sNumMatr = values
		End Set
	End Property

	Public Property CodOrg() as Integer
		Get
			Return nCodOrg
		End Get
		Set(ByVal values as Integer)
			nCodOrg = values
		End Set
	End Property

	Public Property CodTipDesg() as Integer
		Get
			Return nCodTipDesg
		End Get
		Set(ByVal values as Integer)
			nCodTipDesg = values
		End Set
	End Property

#End Region

End Class
