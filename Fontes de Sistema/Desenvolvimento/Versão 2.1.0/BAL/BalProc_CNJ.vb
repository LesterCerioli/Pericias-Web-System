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
        Dim ds As DataSet = Nothing

        sNum_CNJ = AplicarMascaraProcesso(sNum_CNJ)
        sNum_PROC = AplicarMascaraProcesso(sNum_PROC)

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Proc_CNJ", sd.CriaRefCursor, sNum_PROC.Trim, sNum_CNJ.Trim)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosEnt(ByVal pNum_PROC As String, ByVal pNum_CNJ As String) As EntPROC_CNJ
        Dim DsProc As DataSet
        Dim EntCNJ As EntPROC_CNJ = Nothing

        Try
            DsProc = ExibirDadosSet(pNum_PROC, pNum_CNJ)

            If Not DsProc Is Nothing Then
                If DsProc.Tables(0).Rows.Count = 1 Then
                    EntCNJ = GerarEntidade(DsProc.Tables(0).Rows(0))
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
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


