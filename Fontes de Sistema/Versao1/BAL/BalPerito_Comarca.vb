Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalPerito_Comarca

    Inherits BaseBAL
    '    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosPeritoNUR(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    '    Private Ent As EntEspecialidade

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

    Public Function ExibirDadosSetDCP(ByVal nNome As String) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDadosDCP(nNome)
        Return dsSet
    End Function

    Public Sub GravarPerito_Comarca(ByVal DsPerCom As DataSet, ByVal pID_PF As Integer)

        If pID_PF = 0 Then
            Exit Sub
        End If

        Try
            'ID_PF
            ParametrosPeritoNUR(0) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosPeritoNUR(0).Valor = pID_PF
            'Nurs
            ParametrosPeritoNUR(1) = New ServicoDadosOracle.ParameterInfo("nCod_Comarca", OracleDbType.Int64, ParameterDirection.Input)
            'Contador = 0

            sd.Open()

            For Each rs As DataRow In DsPerCom.Tables(0).Rows
                'dsnurtotal.Tables(0).Rows(0).Item(0)
                If rs("Marcado") <> 0 Then
                    'pCod_Comarca = rs("Cod_Comarca")
                    ParametrosPeritoNUR(1).Valor = rs("Cod_Comarca")
                    sd.ExecuteNonQuery("Gravar_Perito_Comarca", ParametrosPeritoNUR)
                    'Contador = Contador + 1
                End If

            Next
       Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try
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

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_PF
        If pCod_Nur = Nothing Then
            pCod_Nur = 0
        End If
        ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Nur", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(2).Valor = pCod_Nur

        Try

            sd.Open()

            ds = sd.CreateDataSet("ExibirDados_Perito_Comarca", ParametrosExibir)

            Return ds

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function ExibirDadosDCP(ByVal pNome As String) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nNome", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pNome

        Try
            sd.Open()

            ds = sd.CreateDataSet("ExibirDados_Perito_DCP", ParametrosExibir)

            Return ds

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    
    Public Function ExibirDadosNur(ByVal pID_PF As Integer, Optional ByVal pCod_Nur As Integer = 0) As DataSet

        Dim ds As DataSet

        Dim ParametrosExibir(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_PF

        If pCod_Nur = Nothing Then
            pCod_Nur = 0
        End If

        ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Nur", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(2).Valor = pCod_Nur

        Try
            sd.Open()

            ds = sd.CreateDataSet("ExibirNURPerito", ParametrosExibir)

            Return ds

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
   
    Public Sub ExcluirPerNur(ByVal pID_PF As Integer)

        Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        If pID_PF = 0 Then
            Exit Sub
        End If

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(0).Valor = pID_PF

            Try

                sd.Open()
                sd.CreateDataSet("ExcluirNURPerito", ParametrosExibir)

            Catch ex As Exception
                Throw ex
            Finally
                If sd.State = ConnectionState.Open Then
                    sd.Close()
                End If
            End Try

    End Sub

    Public Sub Desativar_Per_DCP(ByVal pNome As String)

        Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        If pNome = "" Then
            Exit Sub
        End If

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nNome", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(0).Valor = pNome

        Try
            sd.Open()

            sd.CreateDataSet("Desativar_Per_DCP", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

End Class
