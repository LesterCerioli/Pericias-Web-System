'INCLUIR PARAMETROS PARA INCLUSAO E ALTERAÇÃO / REGRAS DE NEGÓCIOS(VALIDAÇÃO)...

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

'COD_BAI, NOME, COD_CID 

Public Class BALBairro

    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    'Public Function ExibirDadosSet(Optional ByVal sFiltro As String = "") As DataSet
    '    Dim ds As DataSet
    '    Dim oParam(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    '    Dim sOrdenacao As String
    '    Try
    '        sd.Open()
    '        sOrdenacao = "Nome"
    '        oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
    '        oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
    '        oParam(1).Valor = sOrdenacao
    '        oParam(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Filtro", OracleDbType.Varchar2, ParameterDirection.Input)
    '        oParam(2).Valor = sFiltro
    '        If sFiltro.Length > 5 Then
    '            sd.Close()
    '            Return Nothing
    '            Exit Function
    '        End If
    '        ds = sd.ExecutaProcDS("ExibirDados_Bairro", oParam)
    '        Return ds
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        sd.Close()
    '    End Try

    '    Return ds
    'End Function
    Public Function ExibirDadosSet(ByVal pCod_Cid As Long) As DataSet
        Dim ds As DataSet = Nothing

        Try
            Return sd.ExecutaProcDS("ExibirDados_Bairro", sd.CriaRefCursor, pCod_Cid)

        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    Public Function Validar() As Boolean
        Dim nState As ConnectionState
        Dim sSQL As String
        sSQL = "select * from Bairro where COD_BAI = ?"
        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntBairro
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, ent.Cod_Bai)
            If dr.Read Then
                ent = GerarEntidade(dr)
                Return True
            Else
                Return False
            End If
            dr.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If nState <> ConnectionState.Open And sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntBairro
        Dim Ent As New EntBairro

        Ent.Cod_Bai = row("COD_Bai")
        Ent.Nome = row("Nome")
        Ent.Cod_Cid = row("COD_Cid")
        Return Ent
    End Function

End Class

