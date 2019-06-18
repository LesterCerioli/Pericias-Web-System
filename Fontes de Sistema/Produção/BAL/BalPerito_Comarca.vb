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

    Public Function ExibirDadosSet(ByVal ncod_perito As Integer) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(ncod_perito, 1)
        Return dsSet
    End Function

    Public Sub GravarPerito_Comarca(ByVal DsPerCom As DataSet, ByVal pCOD_PERITO As Int64)
        If pCOD_PERITO <> 0 Then
            Try
                For Each rs As DataRow In DsPerCom.Tables(0).Rows
                    If rs("Marcado") <> 0 Then
                        sd.ExecutaProc("Gravar_Perito_Comarca", pCOD_PERITO, rs("Cod_Comarca"))
                    End If
                Next
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Sub

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntPerito_Comarca
        Dim Ent As New EntPerito_Comarca

        Ent.ID_PF = Rss("ID_PF")
        Ent.Cod_Comarca = Rss("Cod_Comarca")
        Ent.Cod_Nur = Rss("Cod_Nur")
        Ent.Cod_Perito = Rss("Cod_Perito")

        Return Ent
    End Function

    Public Function ExibirDados(ByVal pCod_Perito As Int64, Optional ByVal pCod_Nur As Integer = 0) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Perito_Comarca", sd.CriaRefCursor, pCod_Perito, pCod_Nur)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosNur(ByVal pcod_perito As Int64, Optional ByVal pCod_Nur As Integer = 0) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirNURPerito", sd.CriaRefCursor, pcod_perito, pCod_Nur)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Sub ExcluirPerNur(ByVal pCOD_PERITO As Int64)
        If pCOD_PERITO <> 0 Then
            Try
                sd.ExecutaProcDS("ExcluirNURPerito", pCOD_PERITO)
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Sub
End Class
