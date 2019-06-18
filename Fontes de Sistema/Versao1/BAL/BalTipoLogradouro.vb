

'INCLUIR PARAMETROS PARA INCLUSAO E ALTERAÇÃO / REGRAS DE NEGÓCIOS(VALIDAÇÃO)...

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

'COD_CID, NOME, Sit_Cid, Sigla_UF

Public Class BalTipoLogradouro

    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet() As DataSet

        Dim ds As DataSet
        Dim oParam(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim sOrdenacao As String

        Try
            sOrdenacao = "Descr"
            oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
            oParam(1).Valor = sOrdenacao

            sd.Open()
            ds = sd.CreateDataSet("ExibirDados_Tip_Logr", oParam)

            Return ds

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function Validar() As Boolean

        Dim sSQL As String
        sSQL = "select * from TipoLogradouro where Cod_Tip_Logr = ?"

        Try
            Dim ent As New EntTipoLogradouro

            sd.Open()
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, ent.Cod_Tip_Logr)

            If dr.Read Then
                ent = GerarEntidade(dr)
                Return True
            Else
                Return False
            End If

            dr.Close()

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

Private Function GerarEntidade(ByVal row As OracleDataReader) As EntTipoLogradouro
    Dim Ent As New EntTipoLogradouro

    Ent.Cod_Tip_Logr = row("Cod_Tip_Logr")
    Ent.Descr = row("Descr")

        Return Ent

    End Function

End Class
