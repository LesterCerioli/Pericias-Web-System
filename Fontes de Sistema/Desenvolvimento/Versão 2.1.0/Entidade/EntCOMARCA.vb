'------------------------------------------------
' 01/08/2007 11:07:32
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------


<Serializable()> _
Public Class EntCOMARCA

    Private nCodCom as Integer
    Private sNome as String
    Private sNomeRedu as String
    Private nCodNurc as Integer
    Private sTipEntranc as String
    Private sIndSedeNurc as String
    Private nCodComHll as Integer
    Private nCodRegiao as Integer
    Private sCodTJ As String
    Private nNumDiasAtrasoDo as Integer
    Private sIndDistOfic as String
    Private sCodComPc as String
    Private sNomeDtl as String
    Private nCodRegJud as Integer
    Private nCodComPrinc as Integer
    Private sIndVaraFed as String
    Private sNomeWeb as String
    Private nNumOrdemDo as Integer
    Private nCodComDo as Decimal

#Region " Propriedades " 

	Public Property CodCom() as Integer
		Get
			Return nCodCom
		End Get
		Set(ByVal values as Integer)
			nCodCom = values
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

	Public Property NomeRedu() as String
		Get
			Return sNomeRedu
		End Get
		Set(ByVal values as String)
			sNomeRedu = values
		End Set
	End Property

	Public Property CodNurc() as Integer
		Get
			Return nCodNurc
		End Get
		Set(ByVal values as Integer)
			nCodNurc = values
		End Set
	End Property

	Public Property TipEntranc() as String
		Get
			Return sTipEntranc
		End Get
		Set(ByVal values as String)
			sTipEntranc = values
		End Set
	End Property

	Public Property IndSedeNurc() as String
		Get
			Return sIndSedeNurc
		End Get
		Set(ByVal values as String)
			sIndSedeNurc = values
		End Set
	End Property

	Public Property CodComHll() as Integer
		Get
			Return nCodComHll
		End Get
		Set(ByVal values as Integer)
			nCodComHll = values
		End Set
	End Property

	Public Property CodRegiao() as Integer
		Get
			Return nCodRegiao
		End Get
		Set(ByVal values as Integer)
			nCodRegiao = values
		End Set
	End Property

	Public Property CodTj() as String
		Get
			Return sCodTj
		End Get
		Set(ByVal values as String)
			sCodTj = values
		End Set
	End Property

	Public Property NumDiasAtrasoDo() as Integer
		Get
			Return nNumDiasAtrasoDo
		End Get
		Set(ByVal values as Integer)
			nNumDiasAtrasoDo = values
		End Set
	End Property

	Public Property IndDistOfic() as String
		Get
			Return sIndDistOfic
		End Get
		Set(ByVal values as String)
			sIndDistOfic = values
		End Set
	End Property

	Public Property CodComPc() as String
		Get
			Return sCodComPc
		End Get
		Set(ByVal values as String)
			sCodComPc = values
		End Set
	End Property

	Public Property NomeDtl() as String
		Get
			Return sNomeDtl
		End Get
		Set(ByVal values as String)
			sNomeDtl = values
		End Set
	End Property

	Public Property CodRegJud() as Integer
		Get
			Return nCodRegJud
		End Get
		Set(ByVal values as Integer)
			nCodRegJud = values
		End Set
	End Property

	Public Property CodComPrinc() as Integer
		Get
			Return nCodComPrinc
		End Get
		Set(ByVal values as Integer)
			nCodComPrinc = values
		End Set
	End Property

	Public Property IndVaraFed() as String
		Get
			Return sIndVaraFed
		End Get
		Set(ByVal values as String)
			sIndVaraFed = values
		End Set
	End Property

	Public Property NomeWeb() as String
		Get
			Return sNomeWeb
		End Get
		Set(ByVal values as String)
			sNomeWeb = values
		End Set
	End Property

	Public Property NumOrdemDo() as Integer
		Get
			Return nNumOrdemDo
		End Get
		Set(ByVal values as Integer)
			nNumOrdemDo = values
		End Set
	End Property

	Public Property CodComDo() as Decimal
		Get
			Return nCodComDo
		End Get
		Set(ByVal values as Decimal)
			nCodComDo = values
		End Set
	End Property

#End Region

End Class
