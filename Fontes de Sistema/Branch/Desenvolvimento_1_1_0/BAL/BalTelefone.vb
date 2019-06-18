Imports ServicoDadosODPNET
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager
Imports Oracle.DataAccess.Client


Public Class BalTelefone
    Inherits BaseBAL
    Public Ent As New EntTelefone

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ConsultarTelefone(ByVal pId_PF As String, ByVal pSeqTel As Integer) As EntTelefone
        Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        Parametros(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sID_PF", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(1).Valor = pId_PF
        Parametros(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sSeqTel", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(2).Valor = pSeqTel

        Dim ds As DataSet

        sd.Open()
        ds = sd.CreateDataSet("Consultar_PFTelefone", Parametros)

        If ds.Tables(0).Rows.Count > 0 Then
            Ent = GerarEntidade(ds)
        End If

        Return Ent

    End Function

    Public Function ExibirDadosPFTelefone(ByVal pID_PF As String) As DataSet
        Dim ds As DataSet
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("pID_PF", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_PF
        ds = sd.CreateDataSet("ExibirDados_Perito_Telefone", ParametrosExibir)
        Return ds
        sd.Close()

    End Function

    Private Function GerarEntidade(ByVal Rss As DataSet) As EntTelefone
        '        Dim i As Integer = 1

        For Each r As DataRow In Rss.Tables(0).Rows
            'If i = 1 Then
            Ent.Cod_Tip_Tel = NVL(r("Cod_Tip_Tel"), 0)
            Ent.DDD = NVL(r("DDD"), "")
            Ent.Tel = NVL(r("NUM_TEL"), "")
            Ent.Ramal = NVL(r("NUM_RAMAL"), "")
            Ent.SeqTel = NVL(r("seq_tel"), 0)
            'i = i + 1
            'Else
            ' End If
        Next
        Return Ent
    End Function

    Public Sub Gravar(ByVal ent As EntTelefone, ByVal pID_PF As Long)

        If Validar(CStr(ent.SeqTel), CStr(pID_PF)) Then
            AlterarPessoaFisicaTelefone(ent, pID_PF)
        Else
            InserirPessoaFisicaTelefone(ent, pID_PF)
        End If

    End Sub

    Private Function Validar(ByVal pSeqTel As String, ByVal pID_PF As String) As Boolean
        Dim sRetorno As String = ""
        sd.Open()
        sRetorno = sd.ExecutaFunc("Validar_Telefone", 250, pID_PF, pSeqTel)

        If sRetorno = "null" Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub AlterarPessoaFisicaTelefone(ByVal ent As EntTelefone, ByVal pID_PF As String)

        Dim ParametrosPFTel(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim ii As Integer
        ii = 0
        'ID_PF               NUMBER(5) not null,
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 14
        ParametrosPFTel(ii).Valor = pID_PF 'Na inserção este valor será criado no Seq do Banco PF.
        ii = ii + 1

        'Seq_tel
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        'ParametrosPFTel(ii).Valor = 1
        ParametrosPFTel(ii).Valor = ent.SeqTel
        ii = ii + 1

        'Cod_Tip_Tel - (1-Profissional, 2-Residencial,3-Fax,4-Celular,9-Outros)
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        ParametrosPFTel(ii).Valor = IIf(ent.Cod_Tip_Tel = Nothing, System.DBNull.Value, ent.Cod_Tip_Tel)
        ii = ii + 1

        'Num_DDD
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_DDD", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 3
        ParametrosPFTel(ii).Valor = IIf(ent.DDD = Nothing, System.DBNull.Value, ent.DDD)
        ii = ii + 1

        'Num_Tel 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Tel", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 30
        ParametrosPFTel(ii).Valor = IIf(ent.Tel = Nothing, System.DBNull.Value, ent.Tel)
        ii = ii + 1

        'Num_Ramal 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Ramal", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 8
        ParametrosPFTel(ii).Valor = IIf(ent.Ramal = Nothing, System.DBNull.Value, ent.Ramal)

        sd.Open()
        sd.ExecuteNonQuery("uc.Pericias_PKG.Alterar_PessoaFisicaTel", ParametrosPFTel)
        sd.Close()

    End Sub

    Public Sub InserirPessoaFisicaTelefone(ByVal ent As EntTelefone, ByVal pID_PF As Long)

        Dim ParametrosPFTel(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim ii As Integer
        ii = 0
        'ID_PF               NUMBER(5) not null,
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 14
        ParametrosPFTel(ii).Valor = pID_PF 'Na inserção este valor será criado no Seq do Banco PF.
        ii = ii + 1

        'Seq_tel
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        'ParametrosPFTel(ii).Valor = 1
        ParametrosPFTel(ii).Valor = RetornaUltimoSeqTel(pID_PF) + 1
        ii = ii + 1

        'Cod_Tip_Tel - (1-Profissional, 2-Residencial,3-Fax,4-Celular,9-Outros)
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        ParametrosPFTel(ii).Valor = IIf(ent.Cod_Tip_Tel = Nothing, System.DBNull.Value, ent.Cod_Tip_Tel)
        ii = ii + 1

        'Num_DDD
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_DDD", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 3
        ParametrosPFTel(ii).Valor = IIf(ent.DDD = Nothing, System.DBNull.Value, ent.DDD)
        ii = ii + 1

        'Num_Tel 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Tel", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 30
        ParametrosPFTel(ii).Valor = IIf(ent.Tel = Nothing, System.DBNull.Value, ent.Tel)
        ii = ii + 1

        'Num_Ramal 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Ramal", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 8
        ParametrosPFTel(ii).Valor = IIf(ent.Ramal = Nothing, System.DBNull.Value, ent.Ramal)

        sd.Open()
        sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_PessoaFisicaTel", ParametrosPFTel)
        sd.Close()

    End Sub

    Public Sub ExcluirPessoaFisicaTelefone(ByVal pID_PF As String, ByVal pSeq_Tel As Integer)

        Dim ParametrosPFTel(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosPFTel(0) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        'ParametrosPFTel(0).Tamanho = 14
        ParametrosPFTel(0).Valor = pID_PF

        'Seq_tel
        ParametrosPFTel(1) = New ServicoDadosOracle.ParameterInfo("nSeq_Tel", OracleDbType.Int32, ParameterDirection.Input)
        'ParametrosPFTel(1).Tamanho = 1
        ParametrosPFTel(1).Valor = pSeq_Tel

        sd.Open()
        sd.ExecuteNonQuery("uc.Pericias_PKG.Excluir_PessoaFisicaTel", ParametrosPFTel)
        sd.Close()

    End Sub

    Public Function RetornaUltimoSeqTel(ByVal sIDPF As String) As Integer

        Dim nSeq As Integer = 0
        sd.Open()
        nSeq = sd.ExecutaFunc("ultimoseqtelefone", 50, sIDPF)
        sd.Close()
        Return nSeq

    End Function

End Class
