
Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalValor_Pericia

    Inherits BaseBAL
    Private Ent As EntValor_Pericia

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosEnt() As EntValor_Pericia
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
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Valor_pericia", sd.CriaRefCursor)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntValor_Pericia
        Dim Ent As New EntValor_Pericia

        Ent.COD_TIPO_PERICIA = Rss("COD_TIPO_PERICIA")
        Ent.DESCR_TIPO_PERICIA = Rss("DESCR_TIPO_PERICIA")
        Ent.VALOR = Rss("VALOR")
        Ent.DATA_TIPO_PERICIA = NVL(Rss("DATA_TIPO_PERICIA"), "")
        Ent.DATA_EXCLUSAO = NVL(Rss("DATA_EXCLUSAO"), "")

        Return Ent
    End Function
End Class

