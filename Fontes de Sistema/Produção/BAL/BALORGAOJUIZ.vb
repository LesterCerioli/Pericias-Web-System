'------------------------------------------------
' 02/08/2007 11:45:47
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BALORGAOJUIZ
    Inherits BaseBal

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function Listar(ByVal sFiltro As String, ByVal sOrdenacao As String) As EntORGAOJUIZ()
        Dim lst() As EntORGAOJUIZ = New EntORGAOJUIZ() {}
        Dim dr As OracleDataReader
        Dim sSQL As String

        Try
            sFiltro = "%" + sFiltro + "%"
            sSQL = "select * from ORGAOJUIZ where [CAMPO] like ? " + IIf(sOrdenacao = String.Empty, String.Empty, " order by " + sOrdenacao)

            sd.Open()
            dr = sd.ExecuteReader(sSQL, sFiltro)
            While dr.Read
                Array.Resize(Of EntORGAOJUIZ)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While
            dr.Close()
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return lst
    End Function

    Public Function Consultar(ByVal pNumMatr As String, ByVal pCodOrg As Decimal) As EntORGAOJUIZ
        Dim nState As ConnectionState
        Dim sSQL As String
        sSQL = "select * from ORGAOJUIZ where NUM_MATR = ? and COD_ORG = ?"
        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntORGAOJUIZ
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pNumMatr, pCodOrg)
            If dr.read Then
                ent = GerarEntidade(dr)
            End If
            dr.Close()
            Return ent
        Catch ex As ServicoDadosException
            Throw New ApplicationException(ex.message)
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.message)
        Catch ex As Exception
            Throw New Exception(ex.message)
        Finally
            If nState <> ConnectionState.Open And sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntORGAOJUIZ
        Dim Ent As New EntORGAOJUIZ

        ent.NumMatr = CStr(NVL(row("NUM_MATR"), String.Empty))
        ent.CodOrg = CInt(NVL(row("COD_ORG"), 0))
        ent.CodTipDesg = CInt(NVL(row("COD_TIP_DESG"), 0))
        Return Ent
    End Function


End Class
