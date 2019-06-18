'------------------------------------------------
' 01/08/2007 11:07:32
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------


<Serializable()> _
Public Class EntNURC

    Private nCodNurc as Integer
    Private sDescr as String
    Private nCodOrg as Integer
    Private sDescrRes as String

#Region " Propriedades " 

	Public Property CodNurc() as Integer
		Get
			Return nCodNurc
		End Get
		Set(ByVal values as Integer)
			nCodNurc = values
		End Set
	End Property

	Public Property Descr() as String
		Get
			Return sDescr
		End Get
		Set(ByVal values as String)
			sDescr = values
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

	Public Property DescrRes() as String
		Get
			Return sDescrRes
		End Get
		Set(ByVal values as String)
			sDescrRes = values
		End Set
	End Property

#End Region

End Class
