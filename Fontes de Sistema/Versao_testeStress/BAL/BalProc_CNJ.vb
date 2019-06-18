Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

'COD_BAI, NOME, COD_CID 

Public Class BalProc_CNJ

    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet(ByVal sNum_PROC As String, ByVal sNum_CNJ As String) As DataSet 'As EntPROC_CNJ

        Dim ds As DataSet
        Dim oParam(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Try
            sd.Open()
            If sNum_CNJ.Length < 25 And sNum_CNJ <> "" Then
                sd.Close()
                Return Nothing
                Exit Function
            End If
            If sNum_PROC.Length = 14 Then
                sNum_PROC = Mid(sNum_PROC, 1, 4) + "." + Mid(sNum_PROC, 5, 3) + "." + Mid(sNum_PROC, 8, 6) + "-" + Mid(sNum_PROC, 14, 1)
            End If
            If sNum_PROC.Length < 17 And sNum_PROC <> "" Then
                sd.Close()
                Return Nothing
                Exit Function
            End If
            'sd.Open()
            oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_Proc", OracleDbType.Varchar2, ParameterDirection.Input)
            oParam(1).Valor = Trim(sNum_PROC)
            oParam(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
            oParam(2).Valor = Trim(sNum_CNJ)
            ds = sd.CreateDataSet("ExibirDados_Proc_CNJ", oParam)
            'EntCNJ = Nothing
            'If ds.Tables(0).Rows.Count = 1 Then
            'EntCNJ = GerarEntidade(ds.Tables(0).Rows(0))
            'End If
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds

    End Function
    Public Function ExibirDadosEnt(ByVal pNum_PROC As String, ByVal pNum_CNJ As String) As EntPROC_CNJ
        Dim DsProc As DataSet
        Dim EntCNJ As EntPROC_CNJ

        EntCNJ = Nothing
        Try
            'sd.Open()
            DsProc = ExibirDadosSet(pNum_PROC, pNum_CNJ)
            If Not DsProc Is Nothing Then
                If DsProc.Tables(0).Rows.Count = 1 Then
                    EntCNJ = GerarEntidade(DsProc.Tables(0).Rows(0))
                End If
            End If
            Return EntCNJ
        Catch ex As Exception
            Throw ex
        Finally
            'sd.Close()
        End Try

        Return EntCNJ

    End Function


    Private Function GerarEntidade(ByVal Rss As DataRow) As EntPROC_CNJ
        Dim EntP As New EntPROC_CNJ

        EntP.Cod_Proc = NVL(Rss("COD_PROC"), 0)
        EntP.Cod_CNJ = NVL(Rss("COD_CNJ"), 0)
        Return EntP

    End Function

End Class


