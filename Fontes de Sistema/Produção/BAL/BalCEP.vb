Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BalCEP

    Inherits BaseBAL
    Private Ent As EntCEP
    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosEnt(ByVal pCEP As String) As EntCEP
        Dim dsSet As DataSet
        dsSet = ExibirDados(pCEP)
        If Not dsSet Is Nothing Then
            If dsSet.Tables(0).Rows.Count = 1 Then
                Ent = GerarEntidade(dsSet.Tables(0).Rows(0))
            End If
            'Return Ent
        Else
            Ent = Nothing
        End If
        Return Ent
    End Function

    Public Function ExibirDados(ByVal sCEP As String) As DataSet
        Dim ds As DataSet = Nothing

        Try
            Return sd.ExecutaProcDS("ExibirDados_CEP", sd.CriaRefCursor, sCEP)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    Private Function GerarEntidade(ByVal Rss As DataRow) As EntCEP
        Dim Ent As New EntCEP

        Ent.Cod_Bai = Rss("Cod_Bai")
        Ent.Logradouro = Rss("Logradouro")
        Ent.Sigla_UF = Rss("Sigla_UF")
        Ent.Cod_Cid = NVL(Rss("Cod_Cid"), "")

        Return Ent

    End Function

End Class
