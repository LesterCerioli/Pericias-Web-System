Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalPerito_Comarca

    Inherits BaseBAL
    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosPeritoNUR(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Private Ent As EntEspecialidade

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
    Public Function ExibirDadosSet(ByVal nID_PF As Integer) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(nID_PF, 1)
        Return dsSet
    End Function

    Public Sub GravarPerito_Comarca(ByVal DsPerCom As DataSet, ByVal pID_PF As Integer)
        'Dim Contador As Integer
        'Dim pCod_Comarca As Integer
        If pID_PF = 0 Then
            Exit Sub
        End If
        Try
            sd.Open()
            'ID_PF
            ParametrosPeritoNUR(0) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            'ParametrosPeritoNUR(0).Tamanho = 7
            ParametrosPeritoNUR(0).Valor = pID_PF
            'Nurs
            ParametrosPeritoNUR(1) = New ServicoDadosOracle.ParameterInfo("nCod_Comarca", OracleDbType.Int64, ParameterDirection.Input)
            'ParametrosPeritoNUR(1).Tamanho = 5
            'Contador = 0
            For Each rs As DataRow In DsPerCom.Tables(0).Rows
                'dsnurtotal.Tables(0).Rows(0).Item(0)
                If rs("Marcado") <> 0 Then
                    'pCod_Comarca = rs("Cod_Comarca")
                    ParametrosPeritoNUR(1).Valor = rs("Cod_Comarca")
                    sd.ExecuteNonQuery("Gravar_Perito_Comarca", ParametrosPeritoNUR)
                    'Contador = Contador + 1
                End If
            Next
        Catch ex As ServicoDadosException
            'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
        Catch ex As ApplicationException
            'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
        Catch ex As Exception
            'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
        End Try
        sd.Close()

    End Sub

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntPerito_Comarca
        Dim Ent As New EntPerito_Comarca

        Ent.ID_PF = Rss("ID_PF")
        Ent.Cod_Comarca = Rss("Cod_Comarca")
        Ent.Cod_Nur = Rss("Cod_Nur")

        Return Ent
    End Function

    Public Function ExibirDados(ByVal pID_PF As Integer, Optional ByVal pCod_Nur As Integer = 0) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pID_PF
            If pCod_Nur = Nothing Then
                pCod_Nur = 0
            End If
            ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Nur", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosExibir(2).Valor = pCod_Nur
            ds = sd.CreateDataSet("ExibirDados_Perito_Comarca", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    
    Public Function ExibirDadosNur(ByVal pID_PF As Integer, Optional ByVal pCod_Nur As Integer = 0) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pID_PF
            If pCod_Nur = Nothing Then
                pCod_Nur = 0
            End If
            ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Nur", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosExibir(2).Valor = pCod_Nur
            ds = sd.CreateDataSet("ExibirNURPerito", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
   
    Public Sub ExcluirPerNur(ByVal pID_PF As Integer)

        Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        If pID_PF = 0 Then
            Exit Sub
        End If
        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosExibir(0).Valor = pID_PF
            sd.CreateDataSet("ExcluirNURPerito", ParametrosExibir)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

    End Sub

End Class
