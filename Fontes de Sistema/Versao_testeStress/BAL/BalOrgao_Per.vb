

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalOrgao_Per

    Inherits BaseBAL
    'Dim Parametros(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Private Ent As EntOrgao_Per

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
    Public Function ExibirDadosEnt() As EntOrgao_per
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
        'Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            'ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
            'ParametrosExibir(1).Valor = "Descr_Orgao_Per"
            ds = sd.CreateDataSet("ExibirDados_Orgao_Per", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Sub Gravar(ByVal Ent_Orgao_Per As EntOrgao_Per)

        Try
            sd.Open()
            'CriarParametros(Ent_Orgao_Per)
            Dim Parametros(4) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
            Parametros(0) = New ServicoDadosOracle.ParameterInfo("nCOD_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
            Parametros(0).Tamanho = 4
            Parametros(0).Valor = IIf(Ent_Orgao_Per.COD_ORGAO_PER = Nothing, System.DBNull.Value, Ent_Orgao_Per.COD_ORGAO_PER)
            'Descr_Orgao_Per             VARCHAR2(250) not null,
            Parametros(1) = New ServicoDadosOracle.ParameterInfo("sDescr_Orgao_Per", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(1).Tamanho = 250
            Parametros(1).Valor = IIf(Ent_Orgao_Per.DESCR_ORGAO_PER = Nothing, System.DBNull.Value, Ent_Orgao_Per.DESCR_ORGAO_PER)
            'Sigla_Per
            Parametros(2) = New ServicoDadosOracle.ParameterInfo("sSigla_Per", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(2).Tamanho = 25
            Parametros(2).Valor = IIf(Ent_Orgao_Per.SIGLA_PER = Nothing, System.DBNull.Value, Ent_Orgao_Per.SIGLA_PER)
            'Data_Exclusao                   DATE
            Parametros(3) = New ServicoDadosOracle.ParameterInfo("dData_Exclusao", OracleDbType.Date, ParameterDirection.Input)
            Parametros(3).Tamanho = 10
            Parametros(3).Valor = IIf(Ent_Orgao_Per.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Orgao_Per.Data_Exclusao)
            'UF varchar2(2)
            Parametros(4) = New ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(4).Tamanho = 2
            Parametros(4).Valor = IIf(Ent_Orgao_Per.Uf = Nothing, System.DBNull.Value, Ent_Orgao_Per.Uf)
            If Ent_Orgao_Per.Cod_Orgao_Per = 0 Then
                Try
                    sd.ExecuteNonQuery("Inserir_Orgao_Per", Parametros)
                Catch
                Finally
                    'MsgErro("Inserido com Sucesso")
                End Try
            Else
                If Not Validar(Ent_Orgao_Per.COD_ORGAO_PER) Then
                    Try
                        sd.ExecuteNonQuery("Inserir_Orgao_Per", Parametros)
                    Catch
                    Finally
                        'MsgErro("Inserido com Sucesso")
                    End Try
                Else
                    Try
                        sd.ExecuteNonQuery("Alterar_Orgao_Per", Parametros)
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

    Public Function Excluir(ByVal pCod_Orgao_Per As Integer) As Boolean

        Dim ParametrosExcluir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExcluir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExcluir(0).Valor = pCod_Orgao_Per

        Try
            sd.Open()
            sd.ExecuteNonQuery("Excluir_Orgao_Per", ParametrosExcluir)
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
    Public Function ExistePerito_Orgao_Per(ByVal pCod_Orgao_Per As String) As Boolean
        'Dim nregs As Integer
        Dim ds As DataSet
        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        If pCod_Orgao_Per = "" Then
            ParametrosExiste(1).Valor = 0
        Else
            ParametrosExiste(1).Valor = CInt(pCod_Orgao_Per)
        End If
        Try
            sd.Open()
            ds = sd.CreateDataSet("ExistePerito_Orgao_Per", ParametrosExiste)
            If Not ds.Tables(0).Rows(0).ItemArray Is System.DBNull.Value Then
                sd.Close()
                Return True
                'Else
                'sd.Close()
                'Return False
            End If
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

    Public Function Validar(ByVal pCod_Orgao_Per As Integer) As Boolean
        Dim ds As DataSet
        Dim sOrdenacao As String
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            sOrdenacao = "P.COD_Orgao_Per"
            ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosValidar(1).Valor = pCod_Orgao_Per
            ds = sd.CreateDataSet("Validar_Orgao_Per", ParametrosValidar)
            If  ds.Tables(0).Rows.Count > 0 Then '   (0).ItemArray Is System.DBNull.Value Then
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

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntOrgao_Per
        Dim Ent As New EntOrgao_Per

        Ent.Cod_Orgao_Per = Rss("COD_Orgao_Per")
        Ent.DESCR_ORGAO_PER = Rss("DESCR_Orgao_Per")
        Ent.SIGLA_PER = Rss("SIGLA_PER")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")
        Ent.SIGLA_PER = NVL(Rss("SIGLA_PER"), "")

        Return Ent
    End Function

End Class
