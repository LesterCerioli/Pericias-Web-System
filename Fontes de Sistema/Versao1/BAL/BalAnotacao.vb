Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

'NUM_ANOTACAO, ID_PF, DESCR_ANOTACAO
'Procedures utilizadas para atualizações na produção : CONSULTA_ANOTACAO, ExibirDados_Anotacao, ExibirDados_Anotacoes_Perito,Validar_Anotacao,
'Exibir_Peritos_Excluidos, Inserir_Anotacao, Excluir_Anotacao


Public Class BALAnotacao

    Inherits BaseBAL
    Dim Ent As EntAnotacao

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ConsultarAnotacao(ByVal pNum_Anotacao As String) As EntAnotacao
        
        Dim ds As DataSet
        Dim param(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("CRet", OracleDbType.RefCursor, ParameterDirection.Output)

        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("vNum_Anotacao", OracleDbType.Varchar2, ParameterDirection.Input)
        param(1).Valor = pNum_Anotacao

        Try

            sd.Open()

            ds = sd.CreateDataSet("CONSULTA_ANOTACAO", param)

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
    Public Function VerificaSeJaFoiIncluidoAnotacaoNoDia(ByVal pIdPF As String) As Integer

        Try
            sd.Open()

            Return CInt(sd.ExecutaFunc("VERIFICA_SE_HA_ANOTACAO_DIA", 1, pIdPF))

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntAnotacao
        Dim Ent As New EntAnotacao

        Ent.ID_PF = Rss("ID_PF")
        Ent.DESCR_ANOTACAO = Rss("Descr_Anotacao")
        Ent.DATA_ANOTACAO = Rss("Data_Anotacao")
        Ent.DATA_EXCLUSAO = Rss("Data_Exclusao")
        Ent.SIGLA = Rss("Sigla")
        Ent.Cod_Tipo_Anotacao = Rss("Cod_Tipo_Anotacao")
        Ent.Num_Anotacao = Rss("Num_Anotacao")

        Return Ent
    End Function
    Public Function ExibirDadosEnt() As EntAnotacao

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

            Return ExibirDados()

    End Function
    Public Function ExibirDados() As DataSet
        Dim ds As DataSet
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim sOrdenacao As String
        Try

            sOrdenacao = "Data_Anotacao"
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
            ParametrosExibir(1).Valor = sOrdenacao

            sd.Open()

            ds = sd.CreateDataSet("ExibirDados_Anotacao", ParametrosExibir)

            Return ds

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function ExibirAnotPer(ByVal pID_PF As Long) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try

            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pID_PF

            sd.Open()

            ds = sd.CreateDataSet("ExibirDados_Anotacoes_Perito", ParametrosExibir)

            Return ds

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Gravar(ByVal Ent_Anotacao As EntAnotacao) As Boolean

        Try

            Dim Parametros(4) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
            'ID_PF
            Parametros(0) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            Parametros(0).Tamanho = 10
            Parametros(0).Valor = IIf(Ent_Anotacao.ID_PF = Nothing, System.DBNull.Value, CDec(Ent_Anotacao.ID_PF))
            'Descr_Anotacao             VARCHAR2(250) not null,
            Parametros(1) = New ServicoDadosOracle.ParameterInfo("sDescr_Anotacao", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(1).Tamanho = 1000
            Parametros(1).Valor = IIf(Ent_Anotacao.DESCR_ANOTACAO = Nothing, System.DBNull.Value, Ent_Anotacao.DESCR_ANOTACAO)
            'Data_Anotacao
            Parametros(2) = New ServicoDadosOracle.ParameterInfo("dData_Anotacao", OracleDbType.Date, ParameterDirection.Input)
            Parametros(2).Tamanho = 10
            Parametros(2).Valor = CDate(Today.ToShortDateString)  'IIf(Ent_Anotacao.DATA_Anotacao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Anotacao.DATA_ANOTACAO)
            'Sigla
            Parametros(3) = New ServicoDadosOracle.ParameterInfo("sSigla", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(3).Valor = Ent_Anotacao.SIGLA
            'Cod_Tipo_Anotacao
            Parametros(4) = New ServicoDadosOracle.ParameterInfo("nCod_Tipo_Anotacao", OracleDbType.Int32, ParameterDirection.Input)
            Parametros(4).Valor = Ent_Anotacao.Cod_Tipo_Anotacao   'IIf(Ent_Anotacao.DATA_EXCLUSAO = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), Ent_Anotacao.DATA_EXCLUSAO)

            sd.Open()

            sd.ExecuteNonQuery("Inserir_Anotacao", Parametros)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    'Public Function Excluir(ByVal pID_PF As Long, ByVal pData_Anotacao As Date) As Boolean
    Public Function Excluir(ByVal pNum_Anotacao As Long) As Boolean

        Dim Parametros(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Parametros(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nNum_Anotacao", OracleDbType.Int64, ParameterDirection.Input)
        Parametros(0).Valor = pNum_Anotacao

        Try
            sd.Open()

            sd.ExecuteNonQuery("Excluir_Anotacao", Parametros)

            Return True

        Catch ex As ServicoDadosException
            Throw New ApplicationException(ex.Message)
            Return False
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    'Public Function ExistePerito_Anotacao(ByVal pID_PF As Long) As Boolean
    '    'Dim nregs As Integer
    '    Dim ds As DataSet
    '    Dim ParametrosExiste(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    '    ParametrosExiste(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
    '    ParametrosExiste(0).Valor = pID_PF
    '    'ParametrosExiste(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("dData_Anotacao", OracleDbType.Date, ParameterDirection.Input)
    '    'ParametrosExiste(1).Valor = pData_Anotacao
    '    Try
    '        sd.Open()
    '        ds = sd.CreateDataSet("ExibirDados_Anotacoes_Perito", ParametrosExiste)
    '        If Not ds.Tables(0).Rows(0).ItemArray Is System.DBNull.Value Then
    '            sd.Close()
    '            Return True
    '        End If
    '    Catch ex As ServicoDadosException
    '        'Throw New ApplicationException(ex.Message)
    '        sd.Close()
    '        Return False
    '    Catch ex As ApplicationException
    '        'Throw New ApplicationException(ex.Message)
    '        sd.Close()
    '        Return False
    '    Catch ex As Exception
    '        'Throw New Exception(ex.Message)
    '        sd.Close()
    '        Return False
    '    End Try
    'End Function

    Public Function Validar(ByVal pID_PF As Long, ByVal pData_Anotacao As Date) As Boolean
        Dim ds As DataSet
        Dim ParametrosValidar(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosValidar(1).Valor = pID_PF
            ParametrosValidar(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("dData_Anotacao", OracleDbType.Date, ParameterDirection.Input)
            ParametrosValidar(2).Valor = pData_Anotacao

            sd.Open()

            ds = sd.CreateDataSet("Validar_Anotacao", ParametrosValidar)

            If ds Is Nothing Then
                Return False
            Else
                If Not ds.Tables(0).Rows.Count = 0 Then
                    Return True
                Else
                    Return False
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
    Public Function ExibirDadosExclusao(ByVal pID_PF As Long) As Boolean

        Dim ds As DataSet
        Dim ParametrosExcluidos(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try

            ParametrosExcluidos(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExcluidos(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosExcluidos(1).Valor = pID_PF

            sd.Open()

            ds = sd.CreateDataSet("Exibir_Peritos_Excluidos", ParametrosExcluidos)

            If ds Is Nothing Then
                Return False
            Else
                If Not ds.Tables(0).Rows.Count = 0 Then
                    Return True
                Else
                    Return False
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
End Class
