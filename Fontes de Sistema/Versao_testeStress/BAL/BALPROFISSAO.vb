Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BALProfissao

    Inherits BaseBAL
    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Private Ent As EntProfissao

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
    Public Function ExibirDadosEnt() As EntProfissao
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        If dsSet.Tables(0).Rows.Count = 1 Then
            'For Each rs As DataRow In dsSet.Tables(0).Rows
            'GerarEntidade(rs)
            GerarEntidade(dsSet.Tables(0).Rows(0))
            'Next
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
        'Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        'Dim sOrdenacao As String
        Try
            sd.Open()
            'sOrdenacao = "P.Cod_Profissao"
            'sOrdenacao = "Descr_Profissao"
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            'ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
            'ParametrosExibir(1).Valor = sOrdenacao
            'Parametros(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Filtro", OracleDbType.Varchar2, ParameterDirection.Input)
            'Parametros(2).Valor = sFiltro
            ds = sd.CreateDataSet("ExibirDados_Profissao", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Sub Gravar(ByVal Ent_Profissao As EntProfissao)

        Try
            sd.Open()
            'CriarParametros(Ent_Profissao)
            Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
            Parametros(0) = New ServicoDadosOracle.ParameterInfo("COD_Profissao", OracleDbType.Int64, ParameterDirection.Input)
            Parametros(0).Tamanho = 4
            Parametros(0).Valor = IIf(Ent_Profissao.Cod_Profissao = Nothing, System.DBNull.Value, CDec(Ent_Profissao.Cod_Profissao))
            'Descr_Profissao             VARCHAR2(250) not null,
            Parametros(1) = New ServicoDadosOracle.ParameterInfo("Descr_Profissao", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(1).Tamanho = 250
            Parametros(1).Valor = IIf(Ent_Profissao.Descr_Profissao = Nothing, System.DBNull.Value, Ent_Profissao.Descr_Profissao)
            'Data_Exclusao                   DATE
            Parametros(2) = New ServicoDadosOracle.ParameterInfo("Data_Exclusao", OracleDbType.Date, ParameterDirection.Input)
            Parametros(2).Tamanho = 10
            Parametros(2).Valor = IIf(Ent_Profissao.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Profissao.Data_Exclusao)
            If Ent_Profissao.Cod_Profissao = 0 Then
                'Dim Parametros(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
                ''Descr_Profissao             VARCHAR2(250) not null,
                'Parametros(0) = New ServicoDadosOracle.ParameterInfo("sDescr_Profissao", OracleDbType.Varchar2, ParameterDirection.Input)
                'Parametros(0).Tamanho = 250
                'Parametros(0).Valor = IIf(Ent_Profissao.Descr_Profissao = Nothing, System.DBNull.Value, Ent_Profissao.Descr_Profissao)
                ''Data_Exclusao                   DATE
                'Parametros(1) = New ServicoDadosOracle.ParameterInfo("dData_Exclusao", OracleDbType.Date, ParameterDirection.Input)
                'Parametros(1).Tamanho = 10
                'Parametros(1).Valor = IIf(Ent_Profissao.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Profissao.Data_Exclusao)

                'Inserindo data como 01/01/1900("NULO") 

                'CriarParametros(Ent_Profissao, "I")
                Try
                    sd.ExecuteNonQuery("Inserir_Profissao", Parametros)
                Catch
                Finally
                    'MsgErro("Inserido com Sucesso")
                End Try
            Else
                If Not Validar(Ent_Profissao.Cod_Profissao) Then
                    'Dim Parametros(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
                    'CriarParametros(Ent_Profissao, "I")
                    'Dim Parametros(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
                    ''Descr_Profissao             VARCHAR2(250) not null,
                    'Parametros(0) = New ServicoDadosOracle.ParameterInfo("sDescr_Profissao", OracleDbType.Varchar2, ParameterDirection.Input)
                    'Parametros(0).Tamanho = 250
                    'Parametros(0).Valor = IIf(Ent_Profissao.Descr_Profissao = Nothing, System.DBNull.Value, Ent_Profissao.Descr_Profissao)
                    ''Data_Exclusao                   DATE
                    'Parametros(1) = New ServicoDadosOracle.ParameterInfo("dData_Exclusao", OracleDbType.Date, ParameterDirection.Input)
                    'Parametros(1).Tamanho = 10
                    'Parametros(1).Valor = IIf(Ent_Profissao.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Profissao.Data_Exclusao)
                    Try
                        sd.ExecuteNonQuery("Inserir_Profissao", Parametros)
                    Catch
                    Finally
                        'MsgErro("Inserido com Sucesso")
                    End Try
                Else
                    'Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
                    'CriarParametros(Ent_Profissao, "A")
                    'Parametros(0) = New ServicoDadosOracle.ParameterInfo("nCOD_Profissao", OracleDbType.Int64, ParameterDirection.Input)
                    'Parametros(0).Tamanho = 4
                    'Parametros(0).Valor = IIf(Ent_Profissao.Cod_Profissao = Nothing, System.DBNull.Value, CDec(Ent_Profissao.Cod_Profissao))
                    ''Descr_Profissao             VARCHAR2(250) not null,
                    'Parametros(1) = New ServicoDadosOracle.ParameterInfo("sDescr_Profissao", OracleDbType.Varchar2, ParameterDirection.Input)
                    'Parametros(1).Tamanho = 250
                    'Parametros(1).Valor = IIf(Ent_Profissao.Descr_Profissao = Nothing, System.DBNull.Value, Ent_Profissao.Descr_Profissao)
                    ''Data_Exclusao                   DATE
                    'Parametros(2) = New ServicoDadosOracle.ParameterInfo("dData_Exclusao", OracleDbType.Date, ParameterDirection.Input)
                    'Parametros(2).Tamanho = 10
                    'Parametros(2).Valor = IIf(Ent_Profissao.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Profissao.Data_Exclusao)
                    Try
                        sd.ExecuteNonQuery("Alterar_Profissao", Parametros)
                    Catch
                    Finally
                        'MsgErro("Alterado com Sucesso")
                    End Try
                End If
            End If
        Catch ex As ServicoDadosException
            'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
        Catch ex As ApplicationException
            'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
        Catch ex As Exception
            'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
        End Try
        sd.Close()

    End Sub

    Public Function Excluir(ByVal pCod_Profissao As Integer) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Valor = pCod_Profissao

        Try
            sd.Open()
            sd.ExecuteNonQuery("Excluir_Profissao", Parametros)
            sd.Close()
            Return True
        Catch ex As ServicoDadosException
            Throw New ApplicationException(ex.Message)
            sd.Close()
            Return False
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
            sd.Close()
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
            sd.Close()
            Return False
        End Try

    End Function
    Public Function ExistePerito_Profissao(ByVal pCod_Profissao As String) As Boolean
        'Dim nregs As Integer
        Dim ds As DataSet
        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(1).Valor = CInt(pCod_Profissao)
        Try
            sd.Open()
            ds = sd.CreateDataSet("ExistePerito_Profissao", ParametrosExiste)
            If Not ds.Tables(0).Rows(0).ItemArray Is System.DBNull.Value Then
                sd.Close()
                Return True
                'Else
                'sd.Close()
                'Return False
            End If
            'nregs = sd.ExecuteNonQuery("ExistePerito_Profissao", ParametrosExiste)
            'If nregs = 0 Then
            'sd.Close()
            'Return False
            'Else
            'sd.Close()
            'Return True
            'End If
        Catch ex As ServicoDadosException
            'Throw New ApplicationException(ex.Message)
            sd.Close()
            Return False
        Catch ex As ApplicationException
            'Throw New ApplicationException(ex.Message)
            sd.Close()
            Return False
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            sd.Close()
            Return False
        End Try
    End Function

    Public Function Validar(ByVal pCod_Profissao As Integer) As Boolean
        Dim ds As DataSet
        Dim sOrdenacao As String
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            sOrdenacao = "P.COD_Profissao"
            ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosValidar(1).Valor = pCod_Profissao
            ds = sd.CreateDataSet("Validar_Profissao", ParametrosValidar)
            If Not ds.Tables(0).Rows(0).ItemArray Is System.DBNull.Value Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

    End Function

    'Private Function GerarEntidade(ByVal row As OracleDataReader) As EntProfissao
    Private Function GerarEntidade(ByVal Rss As DataRow) As EntProfissao
        Dim Ent As New EntProfissao

        Ent.Cod_Profissao = Rss("COD_Profissao")
        Ent.Descr_Profissao = Rss("DESCR_Profissao")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")

        Return Ent
    End Function
    'Private Sub CriarParametros(ByVal ent As EntProfissao, ByVal Tipo As String)
    '' ''Private Sub CriarParametros(ByVal ent As EntProfissao)

    '' ''    'If Tipo = "A" Then
    '' ''    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    '' ''    Parametros(0) = New ServicoDadosOracle.ParameterInfo("COD_Profissao", OracleDbType.Int64, ParameterDirection.Input)
    '' ''    Parametros(0).Tamanho = 4
    '' ''    Parametros(0).Valor = IIf(ent.Cod_Profissao = Nothing, System.DBNull.Value, CDec(ent.Cod_Profissao))
    '' ''    'Descr_Profissao             VARCHAR2(250) not null,
    '' ''    Parametros(1) = New ServicoDadosOracle.ParameterInfo("Descr_Profissao", OracleDbType.Varchar2, ParameterDirection.Input)
    '' ''    Parametros(1).Tamanho = 250
    '' ''    Parametros(1).Valor = IIf(ent.Descr_Profissao = Nothing, System.DBNull.Value, ent.Descr_Profissao)
    '' ''    'Data_Exclusao                   DATE
    '' ''    Parametros(2) = New ServicoDadosOracle.ParameterInfo("Data_Exclusao", OracleDbType.Date, ParameterDirection.Input)
    '' ''    Parametros(2).Tamanho = 10
    '' ''    Parametros(2).Valor = IIf(ent.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), ent.Data_Exclusao)
    '' ''    'Else
    '' ''    'Dim Parametros(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    '' ''    ''Descr_Profissao             VARCHAR2(250) not null,
    '' ''    'Parametros(0) = New ServicoDadosOracle.ParameterInfo("Descr_Profissao", OracleDbType.Varchar2, ParameterDirection.Input)
    '' ''    'Parametros(0).Tamanho = 250
    '' ''    'Parametros(0).Valor = IIf(ent.Descr_Profissao = Nothing, System.DBNull.Value, ent.Descr_Profissao)
    '' ''    ''Data_Exclusao                   DATE
    '' ''    'Parametros(1) = New ServicoDadosOracle.ParameterInfo("Data_Exclusao", OracleDbType.Date, ParameterDirection.Input)
    '' ''    'Parametros(1).Tamanho = 10
    '' ''    'Parametros(1).Valor = IIf(ent.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), ent.Data_Exclusao)
    '' ''    'End If

    '' ''End Sub

End Class

