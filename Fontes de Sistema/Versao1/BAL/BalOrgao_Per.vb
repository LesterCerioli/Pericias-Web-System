Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BalOrgao_Per

    Inherits BaseBAL
    Private Ent As EntOrgao_per

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

        Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)

        Try

            sd.Open()

            Return sd.CreateDataSet("ExibirDados_Orgao_Per", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ConsultarOrgProfissional(ByVal sCodOrgProfissional As String) As EntOrgao_per

        Dim ds As DataSet
        Dim param(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("CRet", OracleDbType.RefCursor, ParameterDirection.Output)
        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCodOrgProfissional", OracleDbType.Varchar2, ParameterDirection.Input)
        param(1).Valor = sCodOrgProfissional

        Try

            sd.Open()

            ds = sd.CreateDataSet("Consultar_OrgaoProfissional", param)

            If ds.Tables(0).Rows.Count > 0 Then
                Ent = GerarEntidade(ds.Tables(0).Rows(0))
            End If

            Return Ent

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ListarOrgNomeSigla() As DataSet

        Dim ds As DataSet
        Dim param(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)

        Try
            sd.Open()

            ds = sd.CreateDataSet("exibirdados_orgpernomesigla", param)

            Return ds

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Sub Gravar(ByVal Ent_Orgao_Per As EntOrgao_Per)
        
        Dim Parametros(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Parametros(0) = New ServicoDadosOracle.ParameterInfo("nCOD_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        'Parametros(0).Tamanho = 4
        Parametros(0).Valor = IIf(Ent_Orgao_Per.COD_ORGAO_PER = Nothing, System.DBNull.Value, Ent_Orgao_Per.COD_ORGAO_PER)
        'Descr_Orgao_Per             VARCHAR2(250) not null,
        Parametros(1) = New ServicoDadosOracle.ParameterInfo("sDescr_Orgao_Per", OracleDbType.Varchar2, ParameterDirection.Input)
        'Parametros(1).Tamanho = 250
        Parametros(1).Valor = IIf(Ent_Orgao_Per.DESCR_ORGAO_PER = Nothing, System.DBNull.Value, UCase(Ent_Orgao_Per.DESCR_ORGAO_PER))
        'Sigla_Per
        Parametros(2) = New ServicoDadosOracle.ParameterInfo("sSigla_Per", OracleDbType.Varchar2, ParameterDirection.Input)
        'Parametros(2).Tamanho = 25
        Parametros(2).Valor = IIf(Ent_Orgao_Per.SIGLA_PER = Nothing, System.DBNull.Value, UCase(Ent_Orgao_Per.SIGLA_PER))
        'Data_Exclusao                   DATE
        Parametros(3) = New ServicoDadosOracle.ParameterInfo("dData_Exclusao", OracleDbType.Date, ParameterDirection.Input)
        'Parametros(3).Tamanho = 10
        Parametros(3).Valor = IIf(Ent_Orgao_Per.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Orgao_Per.Data_Exclusao)
        'UF varchar2(2)
        'Parametros(4) = New ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
        'Parametros(4).Tamanho = 2
        'Parametros(4).Valor = IIf(Ent_Orgao_Per.Uf = Nothing, System.DBNull.Value, Ent_Orgao_Per.Uf)

        Try

            sd.Open()

            If Ent_Orgao_Per.COD_ORGAO_PER = 0 Then
                sd.ExecuteNonQuery("Inserir_Orgao_Per", Parametros)
            Else
                If Not Validar(Ent_Orgao_Per.COD_ORGAO_PER) Then
                    sd.ExecuteNonQuery("Inserir_Orgao_Per", Parametros)
                Else
                    sd.ExecuteNonQuery("Alterar_Orgao_Per", Parametros)
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Public Function Excluir(ByVal pCod_Orgao_Per As Integer) As Boolean

        Dim ParametrosExcluir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExcluir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExcluir(0).Valor = pCod_Orgao_Per

        Try

            sd.Open()

            sd.ExecuteNonQuery("Excluir_Orgao_Per", ParametrosExcluir)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExistePerito_Orgao_Per(ByVal pCod_Orgao_Per As String) As Boolean

        Dim ds As DataSet
        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try

            ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)

            If pCod_Orgao_Per = "" Then
                ParametrosExiste(1).Valor = 0
            Else
                ParametrosExiste(1).Valor = CInt(pCod_Orgao_Per)
            End If

            sd.Open()

            ds = sd.CreateDataSet("ExistePerito_Orgao_Per", ParametrosExiste)

            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Validar(ByVal pCod_Orgao_Per As Integer) As Boolean

        Dim ds As DataSet
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosValidar(1).Valor = pCod_Orgao_Per

        Try
            sd.Open()

            ds = sd.CreateDataSet("Validar_Orgao_Per", ParametrosValidar)

            If ds.Tables(0).Rows.Count > 0 Then '   (0).ItemArray Is System.DBNull.Value Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntOrgao_per

        Dim Ent As New EntOrgao_per

        Ent.COD_ORGAO_PER = NVL(Rss("COD_Orgao_Per"), 0)
        Ent.DESCR_ORGAO_PER = NVL(Rss("DESCR_Orgao_Per"), "")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")
        Ent.SIGLA_PER = NVL(Rss("SIGLA_PER"), "")

        Return Ent
    End Function

    Public Function Validar_OrgProf_Nome(ByVal sNomeOrgProfissional As String) As Boolean

        Dim sRetorno As String = String.Empty

        Try

            sd.Open()

            sRetorno = sd.ExecutaFunc("Validar_OrgProf_Nome", 250, sNomeOrgProfissional)

            If sRetorno = "null" Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function VerificaSeNomeSiglaDoOrgaoProfissionalExiste(ByVal sDescrOrgaoProfissional As String, ByVal sSigla As String) As Integer
        Dim nRetorno As Integer = 0

        Try

            sd.Open()

            nRetorno = sd.ExecutaFunc("Retorna_cod_orgao_per", 250, sDescrOrgaoProfissional, sSigla)

            Return nRetorno

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ConsultarSiglaOrgaoProfissional(ByVal nCodOrgaoProfissional As Integer) As String

        Dim sSigla As String = String.Empty

        Try
            sd.Open()

            sSigla = sd.ExecutaFunc("siglaorgaoprofissional", 20, nCodOrgaoProfissional)

            If sSigla = "null" Then
                Return String.Empty
            Else
                Return sSigla
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

End Class
