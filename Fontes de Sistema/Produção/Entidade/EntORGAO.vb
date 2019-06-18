'------------------------------------------------
' 01/08/2007 11:07:32
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------


<Serializable()> _
Public Class EntORGAO

    Private nCodOrg as Integer
    Private sNome as String
    Private sComplNome as String
    Private sNomeDtl as String
    Private sNomeRedu as String
    Private sNomeAbrev as String
    Private nCodGrupLocOrg as Integer
    Private sNumSeqRel as String
    Private sCodOrgHll as String
    Private nCodCom as Integer
    Private sNomeLogr as String
    Private sNumLogr as String
    Private sComplLogr as String
    Private nCodCid as Long
    Private nCodBai as Integer
    Private nCep as Integer
    Private sDdd as String
    Private sTel1 as String
    Private sRamal1 as String
    Private sTel2 as String
    Private sRamal2 as String
    Private sTelFax as String
    Private sRamalFax as String
    Private dDtInativ as  Date
    Private sEMail as String
    Private nCodSitOrg as Integer
    Private dDtInst as  Date
    Private sIndOrgOficlz as String
    Private sIndOrgJudic as String
    Private sIndOrgExtTj as String
    Private sTipGrau as String
    Private sTxtObs as String
    Private dDtEnd as  Date
    Private nCodTipOrg as Integer
    Private nCodOrgSup as Integer
    Private nTipVinc as Integer
    Private nCodAreaAtu as Integer
    Private sIndAtivo as String
    Private nTipAtribBolCgj as Integer
    Private sIndHabProt as String
    Private sNumMatrMag as String
    Private nCodImov as Integer
    Private sTipAreaAtiv as String
    Private sIndFiscal as String
    Private sIndInstalada as String
    Private sIndOrgFormal as String
    Private sIndSubLot as String
    Private sIndUnidPatr as String
    Private nCodGrupCadGpes as Integer
    Private sIndSoluc as String
    Private sTipVagaTit As String
    Private sDescrGrupCadGpes As String


#Region " Propriedades " 

	Public Property CodOrg() as Integer
		Get
			Return nCodOrg
		End Get
		Set(ByVal values as Integer)
			nCodOrg = values
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

	Public Property ComplNome() as String
		Get
			Return sComplNome
		End Get
		Set(ByVal values as String)
			sComplNome = values
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

	Public Property NomeRedu() as String
		Get
			Return sNomeRedu
		End Get
		Set(ByVal values as String)
			sNomeRedu = values
		End Set
	End Property

	Public Property NomeAbrev() as String
		Get
			Return sNomeAbrev
		End Get
		Set(ByVal values as String)
			sNomeAbrev = values
		End Set
	End Property

	Public Property CodGrupLocOrg() as Integer
		Get
			Return nCodGrupLocOrg
		End Get
		Set(ByVal values as Integer)
			nCodGrupLocOrg = values
		End Set
	End Property

	Public Property NumSeqRel() as String
		Get
			Return sNumSeqRel
		End Get
		Set(ByVal values as String)
			sNumSeqRel = values
		End Set
	End Property

	Public Property CodOrgHll() as String
		Get
			Return sCodOrgHll
		End Get
		Set(ByVal values as String)
			sCodOrgHll = values
		End Set
	End Property

	Public Property CodCom() as Integer
		Get
			Return nCodCom
		End Get
		Set(ByVal values as Integer)
			nCodCom = values
		End Set
	End Property

	Public Property NomeLogr() as String
		Get
			Return sNomeLogr
		End Get
		Set(ByVal values as String)
			sNomeLogr = values
		End Set
	End Property

	Public Property NumLogr() as String
		Get
			Return sNumLogr
		End Get
		Set(ByVal values as String)
			sNumLogr = values
		End Set
	End Property

	Public Property ComplLogr() as String
		Get
			Return sComplLogr
		End Get
		Set(ByVal values as String)
			sComplLogr = values
		End Set
	End Property

	Public Property CodCid() as Long
		Get
			Return nCodCid
		End Get
		Set(ByVal values as Long)
			nCodCid = values
		End Set
	End Property

	Public Property CodBai() as Integer
		Get
			Return nCodBai
		End Get
		Set(ByVal values as Integer)
			nCodBai = values
		End Set
	End Property

	Public Property Cep() as Integer
		Get
			Return nCep
		End Get
		Set(ByVal values as Integer)
			nCep = values
		End Set
	End Property

	Public Property Ddd() as String
		Get
			Return sDdd
		End Get
		Set(ByVal values as String)
			sDdd = values
		End Set
	End Property

	Public Property Tel1() as String
		Get
			Return sTel1
		End Get
		Set(ByVal values as String)
			sTel1 = values
		End Set
	End Property

	Public Property Ramal1() as String
		Get
			Return sRamal1
		End Get
		Set(ByVal values as String)
			sRamal1 = values
		End Set
	End Property

	Public Property Tel2() as String
		Get
			Return sTel2
		End Get
		Set(ByVal values as String)
			sTel2 = values
		End Set
	End Property

	Public Property Ramal2() as String
		Get
			Return sRamal2
		End Get
		Set(ByVal values as String)
			sRamal2 = values
		End Set
	End Property

	Public Property TelFax() as String
		Get
			Return sTelFax
		End Get
		Set(ByVal values as String)
			sTelFax = values
		End Set
	End Property

	Public Property RamalFax() as String
		Get
			Return sRamalFax
		End Get
		Set(ByVal values as String)
			sRamalFax = values
		End Set
	End Property

	Public Property DtInativ() as Date
		Get
			Return dDtInativ
		End Get
		Set(ByVal values as Date)
			dDtInativ = values
		End Set
	End Property

	Public Property EMail() as String
		Get
			Return sEMail
		End Get
		Set(ByVal values as String)
			sEMail = values
		End Set
	End Property

	Public Property CodSitOrg() as Integer
		Get
			Return nCodSitOrg
		End Get
		Set(ByVal values as Integer)
			nCodSitOrg = values
		End Set
	End Property

	Public Property DtInst() as Date
		Get
			Return dDtInst
		End Get
		Set(ByVal values as Date)
			dDtInst = values
		End Set
	End Property

	Public Property IndOrgOficlz() as String
		Get
			Return sIndOrgOficlz
		End Get
		Set(ByVal values as String)
			sIndOrgOficlz = values
		End Set
	End Property

	Public Property IndOrgJudic() as String
		Get
			Return sIndOrgJudic
		End Get
		Set(ByVal values as String)
			sIndOrgJudic = values
		End Set
	End Property

	Public Property IndOrgExtTj() as String
		Get
			Return sIndOrgExtTj
		End Get
		Set(ByVal values as String)
			sIndOrgExtTj = values
		End Set
	End Property

	Public Property TipGrau() as String
		Get
			Return sTipGrau
		End Get
		Set(ByVal values as String)
			sTipGrau = values
		End Set
	End Property

	Public Property TxtObs() as String
		Get
			Return sTxtObs
		End Get
		Set(ByVal values as String)
			sTxtObs = values
		End Set
	End Property

	Public Property DtEnd() as Date
		Get
			Return dDtEnd
		End Get
		Set(ByVal values as Date)
			dDtEnd = values
		End Set
	End Property

	Public Property CodTipOrg() as Integer
		Get
			Return nCodTipOrg
		End Get
		Set(ByVal values as Integer)
			nCodTipOrg = values
		End Set
	End Property

	Public Property CodOrgSup() as Integer
		Get
			Return nCodOrgSup
		End Get
		Set(ByVal values as Integer)
			nCodOrgSup = values
		End Set
	End Property

	Public Property TipVinc() as Integer
		Get
			Return nTipVinc
		End Get
		Set(ByVal values as Integer)
			nTipVinc = values
		End Set
	End Property

	Public Property CodAreaAtu() as Integer
		Get
			Return nCodAreaAtu
		End Get
		Set(ByVal values as Integer)
			nCodAreaAtu = values
		End Set
	End Property

	Public Property IndAtivo() as String
		Get
			Return sIndAtivo
		End Get
		Set(ByVal values as String)
			sIndAtivo = values
		End Set
	End Property

	Public Property TipAtribBolCgj() as Integer
		Get
			Return nTipAtribBolCgj
		End Get
		Set(ByVal values as Integer)
			nTipAtribBolCgj = values
		End Set
	End Property

	Public Property IndHabProt() as String
		Get
			Return sIndHabProt
		End Get
		Set(ByVal values as String)
			sIndHabProt = values
		End Set
	End Property

	Public Property NumMatrMag() as String
		Get
			Return sNumMatrMag
		End Get
		Set(ByVal values as String)
			sNumMatrMag = values
		End Set
	End Property

	Public Property CodImov() as Integer
		Get
			Return nCodImov
		End Get
		Set(ByVal values as Integer)
			nCodImov = values
		End Set
	End Property

	Public Property TipAreaAtiv() as String
		Get
			Return sTipAreaAtiv
		End Get
		Set(ByVal values as String)
			sTipAreaAtiv = values
		End Set
	End Property

	Public Property IndFiscal() as String
		Get
			Return sIndFiscal
		End Get
		Set(ByVal values as String)
			sIndFiscal = values
		End Set
	End Property

	Public Property IndInstalada() as String
		Get
			Return sIndInstalada
		End Get
		Set(ByVal values as String)
			sIndInstalada = values
		End Set
	End Property

	Public Property IndOrgFormal() as String
		Get
			Return sIndOrgFormal
		End Get
		Set(ByVal values as String)
			sIndOrgFormal = values
		End Set
	End Property

	Public Property IndSubLot() as String
		Get
			Return sIndSubLot
		End Get
		Set(ByVal values as String)
			sIndSubLot = values
		End Set
	End Property

	Public Property IndUnidPatr() as String
		Get
			Return sIndUnidPatr
		End Get
		Set(ByVal values as String)
			sIndUnidPatr = values
		End Set
	End Property

	Public Property CodGrupCadGpes() as Integer
		Get
			Return nCodGrupCadGpes
		End Get
		Set(ByVal values as Integer)
			nCodGrupCadGpes = values
		End Set
	End Property

	Public Property IndSoluc() as String
		Get
			Return sIndSoluc
		End Get
		Set(ByVal values as String)
			sIndSoluc = values
		End Set
	End Property

	Public Property TipVagaTit() as String
		Get
			Return sTipVagaTit
		End Get
		Set(ByVal values as String)
			sTipVagaTit = values
		End Set
    End Property
    Public Property DescrGrupCadGpes() As String
        Get
            Return sDescrGrupCadGpes
        End Get
        Set(ByVal values As String)
            sDescrGrupCadGpes = values
        End Set
    End Property

#End Region

End Class
