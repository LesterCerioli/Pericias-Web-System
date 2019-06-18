Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BALEspecialidade

    Inherits BaseBAL
    'Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Private Ent As EntEspecialidade

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosEnt(ByVal nCod_Profissao As Integer) As EntEspecialidade
        Dim dsSet As DataSet

        dsSet = ExibirDados(nCod_Profissao)

        If dsSet.Tables(0).Rows.Count = 1 Then
            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If

        Return Ent

    End Function

    Public Function ExibirDadosSet(ByVal pCod_Profissao As Integer) As DataSet

        Dim dsSet As DataSet
        dsSet = ExibirDados(pCod_Profissao)

        Return dsSet

    End Function

    Public Function ExibirDados(ByVal nCod_Profissao As Integer) As DataSet

        Dim ParametrosExibir(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim sOrdenacao As String

        sOrdenacao = "Descr_Especialidade"
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(1).Valor = sOrdenacao
        ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(2).Valor = nCod_Profissao

        Try
            sd.Open()

            Return sd.CreateDataSet("ExibirDados_Especialidade", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Sub Gravar(ByVal Ent_Especialidade As EntEspecialidade)

        Dim Parametros(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Parametros(0) = New ServicoDadosOracle.ParameterInfo("COD_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Tamanho = 4
        Parametros(0).Valor = IIf(Ent_Especialidade.Cod_Especialidade = Nothing, System.DBNull.Value, CDec(Ent_Especialidade.Cod_Especialidade))
        'Descr_Especialidade             VARCHAR2(250) not null,
        Parametros(1) = New ServicoDadosOracle.ParameterInfo("Descr_Especialidade", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(1).Tamanho = 250
        Parametros(1).Valor = IIf(Ent_Especialidade.Descr_Especialidade = Nothing, System.DBNull.Value, Ent_Especialidade.Descr_Especialidade)
        'Cod_Profissao
        Parametros(2) = New ServicoDadosOracle.ParameterInfo("COD_PROFISSAO", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(2).Valor = IIf(Ent_Especialidade.Cod_Profissao = Nothing, System.DBNull.Value, CDec(Ent_Especialidade.Cod_Profissao))
        'Data_Exclusao                   DATE
        Parametros(3) = New ServicoDadosOracle.ParameterInfo("Data_Exclusao", OracleDbType.Date, ParameterDirection.Input)
        Parametros(3).Tamanho = 10
        Parametros(3).Valor = IIf(Ent_Especialidade.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Especialidade.Data_Exclusao)

        Try

            sd.Open()

            If Ent_Especialidade.Cod_Especialidade = 0 Then
                sd.ExecuteNonQuery("Inserir_Especialidade", Parametros)
            Else
                If Not Validar(Ent_Especialidade.Cod_Especialidade) Then
                    sd.ExecuteNonQuery("Inserir_Especialidade", Parametros)
                Else
                    sd.ExecuteNonQuery("Alterar_Especialidade", Parametros)
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

    Public Function Excluir(ByVal pCod_Especialidade As Integer) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Valor = pCod_Especialidade

        Try
            sd.Open()

            sd.ExecuteNonQuery("Excluir_Especialidade", Parametros)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Sub Reativar(ByVal nCodProfissao As Integer, ByVal sDescEspecialidade As String)

        Try

            sd.Open()

            sd.ExecuteNonQuery("update especialidades set data_exclusao = '01/01/1900' where cod_profissao = ? and upper(descr_especialidade) = ?", nCodProfissao, Trim(sDescEspecialidade.ToUpper))

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Public Function ExistePerito_Especialidade(ByVal pCod_Profissao As String, ByVal pCod_Especialidade As String) As Boolean

        Dim ds As DataSet
        Dim ParametrosExiste(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(1).Valor = CInt(pCod_Especialidade)
        ParametrosExiste(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCOD_PROFISSAO", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(2).Valor = CInt(pCod_Profissao)

        Try

            sd.Open()

            ds = sd.CreateDataSet("ExistePerito_Especialidade", ParametrosExiste)
            If ds.Tables(0).Rows.Count > 0 Then
                If CInt(ds.Tables(0).Rows(0).Item("Total").ToString) > 0 Then
                    Return True
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Validar(ByVal pCod_Especialidade As Integer) As Boolean

        Dim ds As DataSet
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosValidar(1).Valor = pCod_Especialidade

        Try

            sd.Open()


            ds = sd.CreateDataSet("Validar_Especialidade", ParametrosValidar)

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

    Public Function VerificaSeNomeEspecialidadeExiste(ByVal sCodProfissao As String, ByVal sDescrEspecialidade As String) As Integer

        Dim nResultado As Integer

        Try

            sd.Open()

            nResultado = sd.ExecutaFunc("retorna_cod_Especialidade", 250, sCodProfissao, sDescrEspecialidade)

            Return nResultado

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function


    Public Function ValidarNomeEspecialidade(ByVal sCodProfissao As String, ByVal sDescrEspecialidade As String) As Boolean

        Dim sResultado As String = String.Empty

        Try

            sd.Open()

            sResultado = sd.ExecutaFunc("Validar_Nome_Especialidade", 250, sCodProfissao, sDescrEspecialidade)

            If sResultado = "null" Then
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

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntEspecialidade
        Dim Ent As New EntEspecialidade

        Ent.Cod_Especialidade = Rss("COD_Especialidade")
        Ent.Descr_Especialidade = Rss("DESCR_ESPECIALIDADE")
        Ent.Cod_Profissao = Rss("COD_PROFISSAO")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")

        Return Ent
    End Function
End Class

