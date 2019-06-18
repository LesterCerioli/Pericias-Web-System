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

        Dim oParam(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim sOrdenacao As String

        sOrdenacao = "DESCR_TIPO_ANOTACAO"
        oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
        oParam(1).Valor = sOrdenacao
        oParam(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Filtro", OracleDbType.Int64, ParameterDirection.Input)
        oParam(2).Valor = sFiltro

        Try
            sd.Open()

            Return sd.CreateDataSet("ExibirDados_TIPO_ANOTACAO", oParam)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

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
        Parametros(1) = New ServicoDadosOracle.ParameterInfo("Descr_Tipo_Anotacao", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(1).Tamanho = 250
        Parametros(1).Valor = IIf(Ent_Tipo_Anotacao.DESCR_TIPO_ANOTACAO = Nothing, System.DBNull.Value, Ent_Tipo_Anotacao.DESCR_TIPO_ANOTACAO)

        Try
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

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Public Function Excluir(ByVal pCod_Tipo_Anotacao As Integer) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Valor = pCod_Tipo_Anotacao

        Try

            sd.Open()

            If Not ExisteAnotacao_Tipo_Anotacao(pCod_Tipo_Anotacao) Then
                sd.ExecuteNonQuery("Excluir_Tipo_Anotacao", Parametros)
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

        Try

            sd.Open()
            ds = sd.CreateDataSet("ExisteAnotacao_Tipo_Anotacao", ParametrosExiste)

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

    Public Function ExisteTipo_Anotacao(ByVal pCod_Tipo_Anotacao As String) As Boolean

        Dim ds As DataSet
        Dim ParametrosExiste(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExiste(1).Valor = CInt(pCod_Tipo_Anotacao)

        If pCod_Tipo_Anotacao = 0 Then
            Return True 'Não realiza a exclusão
        End If

        Try

            sd.Open()
            ds = sd.CreateDataSet("ExisteTipo_Anotacao", ParametrosExiste)

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

    Public Function Validar(ByVal pCod_Tipo_Anotacao As Integer) As Boolean

        Dim ds As DataSet
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosValidar(1).Valor = pCod_Tipo_Anotacao

        Try

            sd.Open()
            ds = sd.CreateDataSet("Validar_Tipo_Anotacao", ParametrosValidar)

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

    Public Function ValidarTipoAnotacaoNome(ByVal sNomeAnotacao As String) As Boolean

        Dim sRetorno As String = String.Empty

        Try
            sd.Open()
            sRetorno = sd.ExecutaFunc("Validar_TipoAnotacao_Nome", 250, sNomeAnotacao)

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

    Public Function VerificaSeNomeDoTipoAnotacaoExiste(ByVal sNomeAnotacao As String) As Integer

        Try
            sd.Open()
            Return sd.ExecutaFunc("Retorna_Cod_Tipo_Anotacao", 250, sNomeAnotacao)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

End Class
