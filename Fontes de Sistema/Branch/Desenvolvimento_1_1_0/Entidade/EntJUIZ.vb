'------------------------------------------------
' 01/08/2007 11:07:32
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------


<Serializable()> _
Public Class EntJUIZ

    Private sNumMatr as String
    Private sNome as String
    Private sSexo as String
    Private dDtNasc as  Date
    Private nUltProv as Decimal
    Private nCodCargoSecretario as Decimal
    Private sCpf as String

#Region " Propriedades " 

	Public Property NumMatr() as String
		Get
			Return sNumMatr
		End Get
		Set(ByVal values as String)
			sNumMatr = values
		End Set
	End Property

	Public Property Nome() as String
		Get
			Return sNome
		End Get
		Set(ByVal values as String)
			sNome = values
		End Set
	End Property

	Public Property Sexo() as String
		Get
			Return sSexo
		End Get
		Set(ByVal values as String)
			sSexo = values
		End Set
	End Property

	Public Property DtNasc() as Date
		Get
			Return dDtNasc
		End Get
		Set(ByVal values as Date)
			dDtNasc = values
		End Set
	End Property

	Public Property UltProv() as Decimal
		Get
			Return nUltProv
		End Get
		Set(ByVal values as Decimal)
			nUltProv = values
		End Set
	End Property

	Public Property CodCargoSecretario() as Decimal
		Get
			Return nCodCargoSecretario
		End Get
		Set(ByVal values as Decimal)
			nCodCargoSecretario = values
		End Set
	End Property

	Public Property Cpf() as String
		Get
			Return sCpf
		End Get
		Set(ByVal values as String)
			sCpf = values
		End Set
	End Property

#End Region

End Class
