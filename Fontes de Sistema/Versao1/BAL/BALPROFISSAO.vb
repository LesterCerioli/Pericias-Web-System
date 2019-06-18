Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BALProfissao

    Inherits BaseBAL
    '    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
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

            Return sd.CreateDataSet("ExibirDados_Profissao", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Sub Gravar(ByVal Ent_Profissao As EntProfissao)

        Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Parametros(0) = New ServicoDadosOracle.ParameterInfo("COD_Profissao", OracleDbType.Int64, ParameterDirection.Input)
        Parametros(0).Tamanho = 4
        Parametros(0).Valor = IIf(Ent_Profissao.Cod_Profissao = Nothing, System.DBNull.Value, CDec(Ent_Profissao.Cod_Profissao))
        Parametros(1) = New ServicoDadosOracle.ParameterInfo("Descr_Profissao", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(1).Tamanho = 250
        Parametros(1).Valor = IIf(Ent_Profissao.Descr_Profissao = Nothing, System.DBNull.Value, Ent_Profissao.Descr_Profissao)
        Parametros(2) = New ServicoDadosOracle.ParameterInfo("Data_Exclusao", OracleDbType.Date, ParameterDirection.Input)
        Parametros(2).Tamanho = 10
        Parametros(2).Valor = IIf(Ent_Profissao.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Profissao.Data_Exclusao)

        Try

            sd.Open()

            If Ent_Profissao.Cod_Profissao = 0 Then
                sd.ExecuteNonQuery("Inserir_Profissao", Parametros)
            Else
                If Not Validar(Ent_Profissao.Cod_Profissao) Then
                    sd.ExecuteNonQuery("Inserir_Profissao", Parametros)
                Else
                    sd.ExecuteNonQuery("Alterar_Profissao", Parametros)
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

    Public Function Excluir(ByVal pCod_Profissao As Integer) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Valor = pCod_Profissao

        Try
            sd.Open()
            sd.ExecuteNonQuery("Excluir_Profissao", Parametros)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function Reativar(ByVal pDescr_Profissao As String) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Dim sSQL As String

        sSQL = "UPDATE Profissao set Data_Exclusao = to_date('01/01/1900','dd/mm/yyyy') " & _
               "where Upper(Descr_Profissao) = '" & UCase(pDescr_Profissao) & "'"

        Try

            sd.Open()
            sd.ExecuteNonQuery(sSQL)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExistePerito_Profissao(ByVal pCod_Profissao As String) As Boolean

        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(1).Valor = CInt(pCod_Profissao)

        Try
            sd.Open()

            Return IIf(sd.CreateDataSet("ExistePerito_Profissao", ParametrosExiste).Tables(0).Rows.Count > 0, True, False)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Validar(ByVal pCod_Profissao As Integer) As Boolean

        Dim ds As DataSet
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosValidar(1).Valor = pCod_Profissao

        Try

            sd.Open()

            ds = sd.CreateDataSet("Validar_Profissao", ParametrosValidar)

            If Not ds.Tables(0).Rows(0).ItemArray Is System.DBNull.Value Then
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

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntProfissao

        Dim Ent As New EntProfissao

        Ent.Cod_Profissao = Rss("COD_Profissao")
        Ent.Descr_Profissao = Rss("DESCR_Profissao")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")

        Return Ent

    End Function

    Public Function ValidarNomeProfissao(ByVal sNomeProfissao As String) As Boolean

        Dim sRetorno As String = String.Empty

        Try

            sd.Open()
            sRetorno = sd.ExecutaFunc("Validar_Nome_Profissao", 250, sNomeProfissao)

            If sRetorno <> "null" Then
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

    Public Function VerificaSeNomeProfissaoExiste(ByVal sDescrProfissao As String) As Integer

        Try

            sd.Open()

            Return sd.ExecutaFunc("retorna_cod_profissao", 250, sDescrProfissao)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try
    End Function

End Class

