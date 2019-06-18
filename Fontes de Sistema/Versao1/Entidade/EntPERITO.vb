
<Serializable()> _
Public Class EntPERITO


    'COD_PERITO               NUMBER(4) not null,
    'NOME                     VARCHAR2(100) not null,
    'NUM_REG                  VARCHAR2(15),
    'COD_TIP_LOGR             NUMBER(3),
    'NOME_LOGR                VARCHAR2(100),

    Private nCod_Perito As Integer
    Private sNome As String
    Private sNum_Reg As String
    ''
    Private nSeq_End As Integer
    Private nCod_Tip_End As Integer
    Private nCod_Tip_Logr As Integer
    Private sDescr_Tip_Logr As String
    ''
    Private sNome_Logr As String

    'NUM_LOGR                 VARCHAR2(6),
    'COMPL_LOGR               VARCHAR2(20),
    'COD_BAIRRO               NUMBER(5),
    'COD_CIDADE               NUMBER(10),
    'DDD                      VARCHAR2(3),

    Private sNum_Logr As String
    Private sCompl_Logr As String
    ''
    Private nCod_Bairro As Long
    Private sDescr_Bairro As String
    ''
    ''
    Private nCod_Cidade As Long
    Private sDescr_Cidade As String
    ''
    ''
    Private nSeq_End1 As Integer
    Private nCod_Tip_End1 As Integer
    Private nCod_Tip_Logr1 As Integer
    Private sDescr_Tip_Logr1 As String
    ''
    Private sNome_Logr1 As String


    Private sNum_Logr1 As String
    Private sCompl_Logr1 As String
    ''
    Private nCod_Bairro1 As Long
    Private sDescr_Bairro1 As String
    ''
    ''
    Private nCod_Cidade1 As Long
    Private sDescr_Cidade1 As String
    ''
    Private sDDD As String
    Private sDDD1 As String

    'CEP                      NUMBER(9),
    'TEL                      VARCHAR2(30),
    'RAMAL                    VARCHAR2(8),
    'COD_SITUACAO             NUMBER(2),
    'DESCR_SITUACAO           VARCHAR2(20)
    'OBS                      VARCHAR2(250),

    ''
    Private nCEP As String
    Private nCEP1 As String

    Private nSeqTel As Integer
    Private nSeqTel1 As Integer
    Private sTEL As String
    Private sTEL1 As String
    Private sTEL2 As String
    Private sRAMAL As String
    Private sRAMAL1 As String
    Private nCod_Tip_Sit As Integer
    Private sDescr_SITUACAO As String
    Private sOBS As String
    Private sDocNecCV As String
    Private sDocNecCPF As String
    Private sDocNecFoto As String
    Private sDocNecOrg As String
    Private sDocNecHab As String
    Private sDocNecRes As String
    Private sDocNecImp As String

    'EMAIL                    VARCHAR2(200),
    'FALTA_ENTREGAR           VARCHAR2(100),
    'COD_ESPECIALIDADE        NUMBER,
    'COD_PROFISSAO            NUMBER,
    'DESCR_PROFISSAO          VARCHAR2(100),
    'DESCR_ESPECIALIDADE      VARCHAR2(100),
    'SIGLA                    VARCHAR2(20),
    'DATA_CADASTRAMENTO       DATE

    Private sEMAIL As String
    Private sEMAIL1 As String
    Private sFALTA_ENTREGAR As String
    ''
    Private nCOD_ESPECIALIDADE As Integer
    Private sDESCR_ESPECIALIDADE As String
    Private nCOD_PROFISSAO As Integer
    Private sDESCR_PROFISSAO As String
    Private nCOD_ESPECIALIDADE1 As Integer
    Private sDESCR_ESPECIALIDADE1 As String
    Private nCOD_PROFISSAO1 As Integer
    Private sDESCR_PROFISSAO1 As String
    ''
    Private sSIGLA As String
    Private dData_Cadastramento As Date
    Private sIndicacao As String
    Private dData_Exclusao As Date
    Private sSigla_UF As String
    Private sSigla_UF1 As String
    Private nCPF As String
    Private sSITUACAO_CADASTRO As String
    Private nCod_BANCO As String
    Private sNUM_AGENCIA As String
    Private sNome_AGENCIA As String
    Private sNUM_CONTA_CORRENTE As String
    Private nCOD_ORGAO_PER As Integer
    Private sDescr_Orgao_Per As String

    'PessoaFisica
    Private nID_PF As Integer
    'ID_PF - Number
    'Cod_USER_INC = Sigla(Perito) ou Ent.Sigla - varchar(20)
    Private dDt_Nasc As Date
    'Dt_Nasc (Ent.Dt_Nasc) - Date

    'PessoaFisicaEmail 
    'E_Mail - varchar(200)

    'PessoaFisicaTelefone 
    Private nCod_Tip_Tel As Integer
    Private nCod_Tip_Tel1 As Integer
    'Cod_Tip_Tel - Ent.Cod_Tipo_Telefone (1-Profissional, 2-Residencial,3-Fax,4-Celular,9-Outros) - Number
    'Num_DDD - Ent.DDD - varchar(3)
    'Num_Tel - Ent.Tel varchar(30)
    'Num_Ramal - ent.Ramal varchar(8)

    'PessoaFisicaEndereco
    'Private nCod_Tip_End As Integer
    'Cod_Tip_End (1-Profissional, 2-Residencial,9-Outros) - Number
    'Cod_Bai = ent.Cod_Bairro - Number
    'Cod_Cid = ent.Cod_Cidade - Number
    'UF = ent.Sigla_UF - varchar(2)
    Private sIDGED_Foto As String
    Private sIDGED_CV As String

    Private nCodTipFunc As Integer
    Private nDias_Prorrogacao As Integer

#Region " Propriedades "

    Public Property Cod_Perito() As Integer
        Get
            Return nCod_Perito
        End Get
        Set(ByVal values As Integer)
            nCod_Perito = values
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return sNome
        End Get
        Set(ByVal values As String)
            sNome = values
        End Set
    End Property

    Public Property Num_Reg() As String
        Get
            Return sNum_Reg
        End Get
        Set(ByVal values As String)
            sNum_Reg = values
        End Set
    End Property

    Public Property Seq_End() As Integer
        Get
            Return nSeq_End
        End Get
        Set(ByVal values As Integer)
            nSeq_End = values
        End Set
    End Property

    Public Property Cod_Tip_Logr() As Integer
        Get
            Return nCod_Tip_Logr
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_Logr = values
        End Set
    End Property

    Public Property Descr_Tip_Logr() As String
        Get
            Return sDescr_Tip_Logr
        End Get
        Set(ByVal values As String)
            sDescr_Tip_Logr = values
        End Set
    End Property

    Public Property Nome_Logr() As String
        Get
            Return sNome_Logr
        End Get
        Set(ByVal values As String)
            sNome_Logr = values
        End Set
    End Property

    Public Property Num_Logr() As String
        Get
            Return sNum_Logr
        End Get
        Set(ByVal values As String)
            sNum_Logr = values
        End Set
    End Property
    Public Property Compl_Logr() As String
        Get
            Return sCompl_Logr
        End Get
        Set(ByVal values As String)
            sCompl_Logr = values
        End Set
    End Property

    Public Property Cod_Bairro() As Long
        Get
            Return nCod_Bairro
        End Get
        Set(ByVal values As Long)
            nCod_Bairro = values
        End Set
    End Property

    Public Property Descr_Bairro() As String
        Get
            Return sDescr_Bairro
        End Get
        Set(ByVal values As String)
            sDescr_Bairro = values
        End Set
    End Property
    Public Property Cod_Cidade() As Long
        Get
            Return nCod_Cidade
        End Get
        Set(ByVal values As Long)
            nCod_Cidade = values
        End Set
    End Property
    Public Property Descr_Cidade() As String
        Get
            Return sDescr_Cidade
        End Get
        Set(ByVal values As String)
            sDescr_Cidade = values
        End Set
    End Property

    Public Property Seq_End1() As Integer
        Get
            Return nSeq_End1
        End Get
        Set(ByVal values As Integer)
            nSeq_End1 = values
        End Set
    End Property

    Public Property Cod_Tip_Logr1() As Integer
        Get
            Return nCod_Tip_Logr1
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_Logr1 = values
        End Set
    End Property

    Public Property Descr_Tip_Logr1() As String
        Get
            Return sDescr_Tip_Logr1
        End Get
        Set(ByVal values As String)
            sDescr_Tip_Logr1 = values
        End Set
    End Property

    Public Property Nome_Logr1() As String
        Get
            Return sNome_Logr1
        End Get
        Set(ByVal values As String)
            sNome_Logr1 = values
        End Set
    End Property

    Public Property Num_Logr1() As String
        Get
            Return sNum_Logr1
        End Get
        Set(ByVal values As String)
            sNum_Logr1 = values
        End Set
    End Property
    Public Property Compl_Logr1() As String
        Get
            Return sCompl_Logr1
        End Get
        Set(ByVal values As String)
            sCompl_Logr1 = values
        End Set
    End Property

    Public Property Cod_Bairro1() As Long
        Get
            Return nCod_Bairro1
        End Get
        Set(ByVal values As Long)
            nCod_Bairro1 = values
        End Set
    End Property

    Public Property Descr_Bairro1() As String
        Get
            Return sDescr_Bairro1
        End Get
        Set(ByVal values As String)
            sDescr_Bairro1 = values
        End Set
    End Property
    Public Property Cod_Cidade1() As Long
        Get
            Return nCod_Cidade1
        End Get
        Set(ByVal values As Long)
            nCod_Cidade1 = values
        End Set
    End Property
    Public Property Descr_Cidade1() As String
        Get
            Return sDescr_Cidade1
        End Get
        Set(ByVal values As String)
            sDescr_Cidade1 = values
        End Set
    End Property

    Public Property SeqTel() As Integer
        Get
            Return nSeqTel
        End Get
        Set(ByVal value As Integer)
            nSeqTel = value
        End Set
    End Property

    Public Property SeqTel1() As Integer
        Get
            Return nSeqTel1
        End Get
        Set(ByVal value As Integer)
            nSeqTel1 = value
        End Set
    End Property

    Public Property DDD() As String
        Get
            Return sDDD
        End Get
        Set(ByVal values As String)
            sDDD = values
        End Set
    End Property
    Public Property DDD1() As String
        Get
            Return sDDD1
        End Get
        Set(ByVal values As String)
            sDDD1 = values
        End Set
    End Property
    Public Property CEP() As String
        Get
            Return nCEP
        End Get
        Set(ByVal values As String)
            nCEP = values
        End Set
    End Property
    Public Property CEP1() As String
        Get
            Return nCEP1
        End Get
        Set(ByVal values As String)
            nCEP1 = values
        End Set
    End Property
    Public Property TEL() As String
        Get
            Return sTEL
        End Get
        Set(ByVal values As String)
            sTEL = values
        End Set
    End Property
    Public Property TEL1() As String
        Get
            Return sTEL1
        End Get
        Set(ByVal values As String)
            sTEL1 = values
        End Set
    End Property
    'Esta proprieda foi criada devido a mudança da possibilidade de gravação de mais de dois telefones e a gravação de fornecedores necessitar de 3 telefones
    'O parametro enviado usa ENT(entidade) ao inves de variaveis.
    Public Property TEL2() As String
        Get
            Return sTEL2
        End Get
        Set(ByVal values As String)
            sTEL2 = values
        End Set
    End Property
    Public Property RAMAL() As String
        Get
            Return sRAMAL
        End Get
        Set(ByVal values As String)
            sRAMAL = values
        End Set
    End Property

    Public Property RAMAL1() As String
        Get
            Return sRAMAL1
        End Get
        Set(ByVal values As String)
            sRAMAL1 = values
        End Set
    End Property
    Public Property Cod_Tip_Sit() As Integer
        Get
            Return nCod_Tip_Sit
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_Sit = values
        End Set
    End Property
    Public Property Descr_SITUACAO() As String
        Get
            Return sDescr_SITUACAO
        End Get
        Set(ByVal values As String)
            sDescr_SITUACAO = values
        End Set
    End Property

    Public Property OBS() As String
        Get
            Return sOBS
        End Get
        Set(ByVal values As String)
            sOBS = values
        End Set
    End Property

    Public Property EMAIL() As String
        Get
            Return sEMAIL
        End Get
        Set(ByVal values As String)
            sEMAIL = values
        End Set
    End Property
    Public Property EMAIL1() As String
        Get
            Return sEMAIL1
        End Get
        Set(ByVal values As String)
            sEMAIL1 = values
        End Set
    End Property
    Public Property FALTA_ENTREGAR() As String
        Get
            Return sFALTA_ENTREGAR
        End Get
        Set(ByVal values As String)
            sFALTA_ENTREGAR = values
        End Set
    End Property
    Public Property COD_ESPECIALIDADE1() As Integer
        Get
            Return nCOD_ESPECIALIDADE1
        End Get
        Set(ByVal values As Integer)
            nCOD_ESPECIALIDADE1 = values
        End Set
    End Property
    Public Property DESCR_ESPECIALIDADE1() As String
        Get
            Return sDESCR_ESPECIALIDADE1
        End Get
        Set(ByVal values As String)
            sDESCR_ESPECIALIDADE1 = values
        End Set
    End Property
    Public Property COD_PROFISSAO1() As Integer
        Get
            Return nCOD_PROFISSAO1
        End Get
        Set(ByVal values As Integer)
            nCOD_PROFISSAO1 = values
        End Set
    End Property
    Public Property DESCR_PROFISSAO1() As String
        Get
            Return sDESCR_PROFISSAO
        End Get
        Set(ByVal values As String)
            sDESCR_PROFISSAO = values
        End Set
    End Property
    Public Property COD_ESPECIALIDADE() As Integer
        Get
            Return nCOD_ESPECIALIDADE
        End Get
        Set(ByVal values As Integer)
            nCOD_ESPECIALIDADE = values
        End Set
    End Property
    Public Property DESCR_ESPECIALIDADE() As String
        Get
            Return sDESCR_ESPECIALIDADE
        End Get
        Set(ByVal values As String)
            sDESCR_ESPECIALIDADE = values
        End Set
    End Property
    Public Property COD_PROFISSAO() As Integer
        Get
            Return nCOD_PROFISSAO
        End Get
        Set(ByVal values As Integer)
            nCOD_PROFISSAO = values
        End Set
    End Property
    Public Property DESCR_PROFISSAO() As String
        Get
            Return sDESCR_PROFISSAO
        End Get
        Set(ByVal values As String)
            sDESCR_PROFISSAO = values
        End Set
    End Property
    Public Property SIGLA() As String
        Get
            Return sSIGLA
        End Get
        Set(ByVal values As String)
            sSIGLA = values
        End Set
    End Property
    Public Property Data_Cadastramento() As Date
        Get
            Return dData_Cadastramento
        End Get
        Set(ByVal values As Date)
            dData_Cadastramento = values.ToShortDateString
        End Set
    End Property

    Public Property Indicacao() As String

        Get
            Return sIndicacao
        End Get
        Set(ByVal values As String)
            sIndicacao = values
        End Set
    End Property
    Public Property Data_Exclusao() As Date
        Get
            Return dData_Exclusao
        End Get
        Set(ByVal values As Date)
            dData_Exclusao = values.ToShortDateString
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
    Public Property Sigla_UF1() As String
        Get
            Return sSigla_UF1
        End Get
        Set(ByVal values As String)
            sSigla_UF1 = values
        End Set
    End Property
    Public Property CPF() As String
        Get
            Return nCPF
        End Get
        Set(ByVal values As String)
            nCPF = values
        End Set
    End Property
    Public Property SITUACAO_CADASTRO() As String
        Get
            Return sSITUACAO_CADASTRO
        End Get
        Set(ByVal values As String)
            sSITUACAO_CADASTRO = values
        End Set
    End Property
    Public Property COD_BANCO() As String
        Get
            Return nCod_BANCO
        End Get
        Set(ByVal values As String)
            nCod_BANCO = values
        End Set
    End Property
    Public Property NUM_AGENCIA() As String
        Get
            Return sNUM_AGENCIA
        End Get
        Set(ByVal values As String)
            sNUM_AGENCIA = values
        End Set
    End Property
    Public Property NOME_AGENCIA() As String
        Get
            Return sNome_AGENCIA
        End Get
        Set(ByVal values As String)
            sNome_AGENCIA = values
        End Set
    End Property
    Public Property NUM_CONTA_CORRENTE() As String
        Get
            Return sNUM_CONTA_CORRENTE
        End Get
        Set(ByVal values As String)
            sNUM_CONTA_CORRENTE = values
        End Set
    End Property
    Public Property ID_PF() As Integer
        Get
            Return nID_PF
        End Get
        Set(ByVal values As Integer)
            nID_PF = values
        End Set
    End Property
    Public Property Dt_Nasc() As Date
        Get
            Return dDt_Nasc
        End Get
        Set(ByVal values As Date)
            dDt_Nasc = values.ToShortDateString
            If dDt_Nasc = "#12:00:00 AM#" Or dDt_Nasc = "#1/1/1900#" Then
                dDt_Nasc = Nothing
            End If
        End Set
    End Property
    Public Property Cod_Tip_Tel() As Integer
        Get
            Return nCod_Tip_Tel
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_Tel = values
        End Set
    End Property
    Public Property Cod_Tip_Tel1() As Integer
        Get
            Return nCod_Tip_Tel1
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_Tel1 = values
        End Set
    End Property
    Public Property Cod_Tip_End() As Integer
        Get
            Return nCod_Tip_End
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_End = values
        End Set
    End Property
    Public Property Cod_Tip_End1() As Integer
        Get
            Return nCod_Tip_End1
        End Get
        Set(ByVal values As Integer)
            nCod_Tip_End1 = values
        End Set
    End Property
    Public Property Descr_ORGAO_PER() As String
        Get
            Return sDescr_Orgao_Per
        End Get
        Set(ByVal values As String)
            sDescr_Orgao_Per = values
        End Set
    End Property

    Public Property COD_ORGAO_PER() As Integer
        Get
            Return nCOD_ORGAO_PER
        End Get
        Set(ByVal values As Integer)
            nCOD_ORGAO_PER = values
        End Set
    End Property
    'Private sDocNecCV As String
    Public Property DocNecCV() As String
        Get
            Return sDocNecCV
        End Get
        Set(ByVal values As String)
            sDocNecCV = values
        End Set
    End Property
    'Private sDocNecCPF As String
    Public Property DocNecCPF() As String
        Get
            Return sDocNecCPF
        End Get
        Set(ByVal values As String)
            sDocNecCPF = values
        End Set
    End Property
    'Private sDocNecFoto As String
    Public Property DocNecFoto() As String
        Get
            Return sDocNecFoto
        End Get
        Set(ByVal values As String)
            sDocNecFoto = values
        End Set
    End Property
    'Private sDocNecOrg As String
    Public Property DocNecOrg() As String
        Get
            Return sDocNecOrg
        End Get
        Set(ByVal values As String)
            sDocNecOrg = values
        End Set
    End Property
    'Private sDocNecHab As String
    Public Property DocNecHab() As String
        Get
            Return sDocNecHab
        End Get
        Set(ByVal values As String)
            sDocNecHab = values
        End Set
    End Property
    'Private sDocNecRes As String
    Public Property DocNecRes() As String
        Get
            Return sDocNecRes
        End Get
        Set(ByVal values As String)
            sDocNecRes = values
        End Set
    End Property
    'Private sDocNecImp As String
    Public Property DocNecImp() As String
        Get
            Return sDocNecImp
        End Get
        Set(ByVal values As String)
            sDocNecImp = values
        End Set
    End Property

    Public Property IDGED_Foto() As String
        Get
            Return sIDGED_Foto
        End Get
        Set(ByVal values As String)
            sIDGED_Foto = values
        End Set
    End Property
    Public Property IDGED_CV() As String
        Get
            Return sIDGED_CV
        End Get
        Set(ByVal values As String)
            sIDGED_CV = values
        End Set
    End Property

    Public Property CodTipFunc As Integer
        Get
            Return nCodTipFunc
        End Get
        Set(ByVal value As Integer)
            nCodTipFunc = value
        End Set
    End Property

    Public Property Dias_Prorrogacao As Integer
        Get
            Return nDIAS_PRORROGACAO
        End Get
        Set(ByVal value As Integer)
            nDIAS_PRORROGACAO = value
        End Set
    End Property

#End Region

End Class
