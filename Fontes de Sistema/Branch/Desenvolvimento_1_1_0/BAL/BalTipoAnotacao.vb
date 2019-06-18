Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalTipoAnotacao


    Inherits BaseBAL
    '  Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    '    Private Ent As EntTipo_Anotacao

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet(Optional ByVal sFiltro As Long = 0) As DataSet

        Dim ds As DataSet
        Dim oParam(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim sOrdenacao As String

        sd.Open()
        sOrdenacao = "DESCR_TIPO_ANOTACAO"
        oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
        oParam(1).Valor = sOrdenacao
        oParam(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Filtro", OracleDbType.Int64, ParameterDirection.Input)
        oParam(2).Valor = sFiltro

        ds = sd.CreateDataSet("ExibirDados_TIPO_ANOTACAO", oParam)
        Return ds
        
        sd.Close()
       
    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntTipo_Anotacao
        Dim Ent As New EntTipo_Anotacao

        Ent.COD_TIPO_ANOTACAO = row("COD_TIPO_ANOTACAO")
        Ent.DESCR_TIPO_ANOTACAO = row("DESCR_TIPO_ANOTACAO")
        Ent.Data_Exclusao = NVL(row("DATA_EXCLUSAO"), "")


        Return Ent
    End Function

    Public Sub Gravar(ByVal Ent_Tipo_Anotacao As EntTipo_Anotacao)

        Dim Parametros(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Parametros(0) = New ServicoDadosOracle.ParameterInfo("COD_Tipo_Anotacao", OracleDbType.Int64, ParameterDirection.Input)
        Parametros(0).Tamanho = 4
        Parametros(0).Valor = IIf(Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO = Nothing, System.DBNull.Value, CDec(Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO))
        'Descr_Tipo_Anotacao            VARCHAR2(250) not null,
        Parametros(1) = New ServicoDadosOracle.ParameterInfo("Descr_Tipo_Anotacao", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(1).Tamanho = 250
        Parametros(1).Valor = IIf(Ent_Tipo_Anotacao.DESCR_TIPO_ANOTACAO = Nothing, System.DBNull.Value, Ent_Tipo_Anotacao.DESCR_TIPO_ANOTACAO)
        ''Data_Exclusao                   DATE
        'Parametros(2) = New ServicoDadosOracle.ParameterInfo("Data_Exclusao", OracleDbType.Date, ParameterDirection.Input)
        'Parametros(2).Tamanho = 10
        'Parametros(2).Valor = IIf(Ent_Tipo_Anotacao.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Tipo_Anotacao.Data_Exclusao)

        sd.Open()
        If Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO = 0 Then
            sd.ExecuteNonQuery("Inserir_Tipo_anotacao", Parametros)
        Else
            If Not Validar(Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO) Then
                sd.ExecuteNonQuery("Inserir_Tipo_anotacao", Parametros)
            Else
                sd.ExecuteNonQuery("Alterar_Tipo_Anotacao", Parametros)
            End If
        End If
        sd.Close()

    End Sub

    Public Function Excluir(ByVal pCod_Tipo_Anotacao As Integer) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Valor = pCod_Tipo_Anotacao

        sd.Open()
        If Not ExisteAnotacao_Tipo_Anotacao(pCod_Tipo_Anotacao) Then
            sd.ExecuteNonQuery("Excluir_Tipo_Anotacao", Parametros)
            sd.Close()
            Return True
        Else
            Return False
        End If

    End Function
    Public Function ExisteAnotacao_Tipo_Anotacao(ByVal pCod_Tipo_Anotacao As String) As Boolean

        Dim ds As DataSet
        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(1).Valor = CInt(pCod_Tipo_Anotacao)

        If pCod_Tipo_Anotacao = 0 Then
            Return True 'Não realiza a exclusão
            Exit Function
        End If
        sd.Open()
        ds = sd.CreateDataSet("ExisteAnotacao_Tipo_Anotacao", ParametrosExiste)
        If ds.Tables(0).Rows.Count > 0 Then
            sd.Close()
            Return True
        Else
            Return False
        End If

    End Function

    Public Function ExisteTipo_Anotacao(ByVal pCod_Tipo_Anotacao As String) As Boolean

        Dim ds As DataSet
        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(1).Valor = CInt(pCod_Tipo_Anotacao)

        If pCod_Tipo_Anotacao = 0 Then
            Return True 'Não realiza a exclusão
            Exit Function
        End If
        sd.Open()
        ds = sd.CreateDataSet("ExisteTipo_Anotacao", ParametrosExiste)
        If ds.Tables(0).Rows.Count > 0 Then
            sd.Close()
            Return True
        Else
            Return False
        End If

    End Function

    Public Function Validar(ByVal pCod_Tipo_Anotacao As Integer) As Boolean

        Dim ds As DataSet
        '   Dim sOrdenacao As String
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        '        sOrdenacao = "P.COD_Tipo_Anotacao"
        ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosValidar(1).Valor = pCod_Tipo_Anotacao
        ds = sd.CreateDataSet("Validar_Tipo_Anotacao", ParametrosValidar)
        sd.Close()

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function ValidarTipoAnotacaoNome(ByVal sNomeAnotacao As String) As Boolean

        Dim sRetorno As String = String.Empty

        sd.Open()
        sRetorno = sd.ExecutaFunc("Validar_TipoAnotacao_Nome", 250, sNomeAnotacao)
        sd.Close()

        If sRetorno = "null" Then
            Return False
        Else
            Return True
        End If
    End Function

End Class
