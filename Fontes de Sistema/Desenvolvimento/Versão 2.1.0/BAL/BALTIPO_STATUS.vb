Imports ServicoDadosODPNET
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager
Imports Oracle.DataAccess.Client

Public Class BALTIPO_STATUS
    Inherits BaseBAL

#Region "Construtores da Classe"
    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosODPNET.ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que lista todos os status da tabela TIPO_STATUS
    ''' </summary>
    Public Function LISTAR_TODOS_STATUS() As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.LISTAR_STATUS_PERITO", sd.CriaRefCursor)
        Catch ex As Exception
            Throw New Exception("Houve uma falha ao tentar executar a listagem de status de Peritos!")
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function STATUS_ATUAL_PERITO(ByVal pCod_Perito As Int64) As TIPO_STATUS
        Dim statusAtual As TIPO_STATUS = Nothing

        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.STATUS_ATUAL_PERITO", pCod_Perito, sd.CriaRefCursor())

            If ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("O Perito está sem status cadastrado!")
            Else
                For Each dr As DataRow In ds.Tables(0).Rows
                    statusAtual = PreencherObjeto(dr)
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return statusAtual
    End Function

    Public Function PreencherObjeto(ByVal linha As DataRow) As TIPO_STATUS
        Try
            Dim status As New TIPO_STATUS

            status.Codigo = CInt(IIf(linha.Item("COD_TIPO_STATUS").Equals(DBNull.Value), 0, linha.Item("COD_TIP_STATUS")))
            status.Descricao = CStr(IIf(linha.Item("DESC_STATUS").Equals(DBNull.Value), 0, linha.Item("DESC_STATUS")))

            Return status
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class
