'Ajustar para nova Bal

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalPagamento_Perito

    Inherits BaseBAL
    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
    Public Function ExibirDadosSet(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal nCodEspecialidade As Integer) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(sNum_CNJ, nID_PF, nCodEspecialidade)
        Return dsSet
    End Function

    Public Function ExibirDadosEnt(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal nCodEspecialidade As Integer) As EntPagamento_Perito
        Dim EntRet As New EntPagamento_Perito
        Dim DsCNJ As DataSet
        DsCNJ = ExibirDadosSet(sNum_CNJ, nID_PF, nCodEspecialidade)
        If DsCNJ.Tables(0).Rows.Count = 1 Then
            EntRet = GerarEntidade(DsCNJ.Tables(0).Rows(0))
        Else
            EntRet = Nothing
        End If
        Return EntRet
    End Function

    Public Sub GravarPagamento_Perito(ByVal pNum_CNJ As String, ByVal pID_PF As Long, ByVal pDATA_AUTORIZACAO As String, ByVal pDATA_CANCELAMENTO As String, ByVal pCOD_TIPO_PERICIA As Integer, ByVal pCOD_ESPECIALIDADE As Integer, ByVal pNUM_PROT As Long, ByVal pDATA_ENVIO_DGPCF As String)
        Dim DsExibir As DataSet = Nothing

        Try
            If pID_PF <> 0 Then
                DsExibir = ExibirDados(pNum_CNJ, pID_PF, pCOD_ESPECIALIDADE)

                If DsExibir.Tables(0).Rows.Count > 0 Then
                    sd.ExecuteNonQuery("Alterar_Pagamento_Perito", pNum_CNJ, pID_PF, IIf(pDATA_AUTORIZACAO = "", Today.ToShortDateString, pDATA_AUTORIZACAO), pDATA_CANCELAMENTO, IIf(pCOD_TIPO_PERICIA = "", "", pCOD_TIPO_PERICIA), IIf(pCOD_ESPECIALIDADE = 0, "", pCOD_ESPECIALIDADE), IIf(pNUM_PROT = 0, "", pNUM_PROT), pDATA_ENVIO_DGPCF)
                Else
                    sd.ExecuteNonQuery("Inserir_Pagamento_Perito", pNum_CNJ, pID_PF, IIf(pDATA_AUTORIZACAO = "", Today.ToShortDateString, pDATA_AUTORIZACAO), pDATA_CANCELAMENTO, IIf(pCOD_TIPO_PERICIA = "", "", pCOD_TIPO_PERICIA), IIf(pCOD_ESPECIALIDADE = 0, "", pCOD_ESPECIALIDADE), IIf(pNUM_PROT = 0, "", pNUM_PROT), pDATA_ENVIO_DGPCF)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntPagamento_Perito
        Dim Ent As New EntPagamento_Perito

        Ent.NUM_CNJ = Rss("NUM_CNJ")
        Ent.ID_PF = Rss("ID_PF")
        If Rss("DATA_NEGACAO").ToString <> "" Then
            Ent.DATA_AUTORIZACAO = Rss("DATA_AUTORIZACAO")
        Else
            Ent.DATA_AUTORIZACAO = Nothing
        End If
        If Rss("DATA_CANCELAMENTO").ToString <> "" Then
            Ent.DATA_CANCELAMENTO = Rss("DATA_CANCELAMENTO")
        Else
            Ent.DATA_CANCELAMENTO = Nothing
        End If
        If Rss("COD_TIPO_PERICIA").ToString <> "" Then
            Ent.COD_TIPO_PERICIA = Rss("COD_TIPO_PERICIA")
        Else
            Ent.COD_TIPO_PERICIA = Nothing
        End If
        If Rss("COD_ESPECIALIDADE").ToString <> "" Then
            Ent.COD_ESPECIALIDADE = Rss("COD_ESPECIALIDADE")
        Else
            Ent.COD_ESPECIALIDADE = Nothing
        End If
        If Rss("NUM_PROT").ToString <> "" Then
            Ent.NUM_PROT = Rss("NUM_PROT")
        Else
            Ent.NUM_PROT = Nothing
        End If
        If Rss("DATA_ENVIO_DGPCF").ToString <> "" Then
            Ent.DATA_ENVIO_DGPCF = Rss("DATA_ENVIO_DGPCF")
        Else
            Ent.DATA_ENVIO_DGPCF = Nothing
        End If

        Return Ent

    End Function

    Public Function ExibirDados(ByVal pNum_CNJ As String, ByVal pID_PF As Long, _
                                ByVal pCodEspecialidade As Integer) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Pagamento_Perito", sd.CriaRefCursor, pNum_CNJ, pID_PF, pCodEspecialidade)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosSetPer(ByVal pID_PF As Long) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDadosPer_Pag_Perito", sd.CriaRefCursor, pID_PF)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosTodos(ByVal pNum_CNJ As String) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_pagamento_PeritoT", sd.CriaRefCursor, pNum_CNJ)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
End Class


