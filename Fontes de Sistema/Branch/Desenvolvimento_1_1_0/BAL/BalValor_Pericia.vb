
Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalValor_Pericia

    Inherits BaseBAL
    Private Ent As EntValor_Pericia

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosEnt() As EntValor_Pericia
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        If dsSet.Tables(0).Rows.Count = 1 Then
            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If
        Return Ent
    End Function

    Public Function ExibirDadosSet() As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        Return dsSet
    End Function

    Public Function ExibirDados() As DataSet
        Dim ds As DataSet
        Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        sd.Open()
        ds = sd.CreateDataSet("ExibirDados_Valor_pericia", ParametrosExibir)
        sd.Close()
        Return ds

    End Function

    Public Sub Gravar(ByVal Ent_Valor_pericia As EntValor_Pericia)
        Dim Parametros(4) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Parametros(0) = New ServicoDadosOracle.ParameterInfo("COD_TIPO_PERICIA", OracleDbType.Int64, ParameterDirection.Input)
        Parametros(0).Tamanho = 2
        Parametros(0).Valor = IIf(Ent_Valor_pericia.COD_TIPO_PERICIA = Nothing, System.DBNull.Value, CDec(Ent_Valor_pericia.COD_TIPO_PERICIA))
        'Descr_valor_pericia             VARCHAR2(250) not null,
        Parametros(1) = New ServicoDadosOracle.ParameterInfo("DESCR_TIPO_PERICIA", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(1).Tamanho = 50
        Parametros(1).Valor = IIf(Ent_Valor_pericia.DESCR_TIPO_PERICIA = Nothing, System.DBNull.Value, Ent_Valor_pericia.DESCR_TIPO_PERICIA)
        'Valor
        Parametros(2) = New ServicoDadosOracle.ParameterInfo("VALOR", OracleDbType.Double, ParameterDirection.Input)
        'Parametros(2).Tamanho = 250
        Parametros(2).Valor = IIf(Ent_Valor_pericia.VALOR = Nothing, System.DBNull.Value, Ent_Valor_pericia.VALOR)
        'Data_Tipo_Pericia                   DATE
        Parametros(3) = New ServicoDadosOracle.ParameterInfo("DATA_TIPO_PERICIA", OracleDbType.Date, ParameterDirection.Input)
        Parametros(3).Tamanho = 10
        Parametros(3).Valor = IIf(Ent_Valor_pericia.DATA_TIPO_PERICIA = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Valor_pericia.DATA_TIPO_PERICIA)
        'Data_Exclusao                   DATE
        Parametros(4) = New ServicoDadosOracle.ParameterInfo("DATA_EXCLUSAO", OracleDbType.Date, ParameterDirection.Input)
        Parametros(4).Tamanho = 10
        Parametros(4).Valor = IIf(Ent_Valor_pericia.DATA_EXCLUSAO = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Valor_pericia.DATA_EXCLUSAO)

        sd.Open()
        If Ent_Valor_pericia.COD_TIPO_PERICIA = 0 Then
            sd.ExecuteNonQuery("Inserir_valor_pericia", Parametros)
        Else
            If Not Validar(Ent_Valor_pericia.COD_TIPO_PERICIA) Then
                sd.ExecuteNonQuery("Inserir_valor_pericia", Parametros)
            Else
                sd.ExecuteNonQuery("Alterar_valor_pericia", Parametros)
            End If
        End If
        sd.Close()
    End Sub

    Public Function Excluir(ByVal pCOD_TIPO_PERICIA As Integer) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCOD_TIPO_PERICIA", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Valor = pCOD_TIPO_PERICIA

        sd.Open()
        sd.ExecuteNonQuery("Excluir_valor_pericia", Parametros)
        sd.Close()
        Return True
    End Function

    Public Function ExistePerito_valor_pericia(ByVal pCod_valor_pericia As String) As Boolean

        Dim ds As DataSet
        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCOD_TIPO_PERICIA", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(1).Valor = CInt(pCod_valor_pericia)

        sd.Open()
        ds = sd.CreateDataSet("ExistePerito_valor_pericia", ParametrosExiste)
        If ds.Tables(0).Rows.Count > 0 Then
            sd.Close()
            Return True
        End If

    End Function

    Public Function Validar(ByVal pCod_valor_pericia As Integer) As Boolean
        Dim ds As DataSet
        '    Dim sOrdenacao As String
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        '        sOrdenacao = "P.COD_valor_pericia"
        ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCOD_TIPO_PERICIA", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosValidar(1).Valor = pCod_valor_pericia
        ds = sd.CreateDataSet("Validar_valor_pericia", ParametrosValidar)

        If Not ds.Tables(0).Rows(0).ItemArray Is System.DBNull.Value Then
            Return True
        Else
            Return False
        End If
        sd.Close()
    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntValor_Pericia
        Dim Ent As New Entvalor_pericia

        'COD_TIPO_PERICIA  NUMBER(2) not null,
        'DESCR_TIPO_PERICIA VARCHAR2(50)
        'VALOR             NUMBER,
        'DATA_TIPO_PERICIA DATE not null
        'DATA_EXCLUSAO DATE 

        Ent.COD_TIPO_PERICIA = Rss("COD_TIPO_PERICIA")
        Ent.DESCR_TIPO_PERICIA = Rss("DESCR_TIPO_PERICIA")
        Ent.VALOR = Rss("VALOR")
        Ent.DATA_TIPO_PERICIA = NVL(Rss("DATA_TIPO_PERICIA"), "")
        Ent.DATA_EXCLUSAO = NVL(Rss("DATA_EXCLUSAO"), "")

        Return Ent
    End Function


End Class

