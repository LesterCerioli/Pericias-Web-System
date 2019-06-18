Imports ServicoDadosODPNET
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager
Imports Oracle.DataAccess.Client

Public Class BALDOCUMENTO
    Inherits BaseBAL

#Region "Construtores"

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

#End Region

#Region "Eventos"

    Public Sub Incluir(ByVal documento As DOCUMENTO)
        Try
            sd.Open()
            sd.BeginTransaction()
            sd.ExecutaProc("NOVO_PERICIAS.INCLUIR_DOCUMENTO", documento.CodigoPerito, documento.GedId, documento.NomeArquivo)
            sd.CommitTrans()
        Catch ex As Exception
            sd.RollbackTrans()
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub Excluir(ByVal documento As DOCUMENTO)
        Try
            sd.Open()
            sd.BeginTransaction()
            sd.ExecutaProc("NOVO_PERICIAS.EXCLUIR_DOCUMENTO", documento.CodigoPerito, documento.GedId)
            sd.CommitTrans()
        Catch ex As Exception
            sd.RollbackTrans()
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Listar(ByVal codigoPerito As Long) As List(Of DOCUMENTO)
        Try
            Dim dsArq As DataSet = Nothing
            Dim listaDocumentos As New List(Of DOCUMENTO)

            sd.Open()
            dsArq = sd.ExecutaProcDS("NOVO_PERICIAS.LISTAR_DOCUMENTOS", sd.CriaRefCursor, codigoPerito)

            If dsArq.Tables(0).Rows.Count > 0 Then
                If Not dsArq.Tables(0).Rows.Item(0).Equals(DBNull.Value) Then
                    For Each linha As DataRow In dsArq.Tables(0).Rows
                        listaDocumentos.Add(GerarEntidade(linha))
                    Next
                End If
            End If

            Return listaDocumentos
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function GerarEntidade(ByVal linha As DataRow)
        Try
            Dim documento As New DOCUMENTO

            documento.CodigoPerito = NVL(linha.Item(0), 0)
            documento.GedId = NVL(linha.Item(1), String.Empty)
            documento.NomeArquivo = NVL(linha.Item(2), String.Empty)

            Return documento
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
