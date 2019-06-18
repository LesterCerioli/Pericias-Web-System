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

Public Class BALNURC
    Inherits BaseBal

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function Listar(ByVal sFiltro As String, ByVal sOrdenacao As String) As EntNURC()
        Dim lst() As EntNURC = New EntNURC() {}
        Dim dr As OracleDataReader
        Dim sSQL As String

        Try
            sFiltro = "%" + sFiltro + "%"
            sSQL = "select * from NURC where descr like ? " + IIf(sOrdenacao = String.Empty, String.Empty, " order by " + sOrdenacao)

            sd.Open()

            dr = sd.ExecuteReader(sSQL, sFiltro)

            While dr.Read
                Array.Resize(Of EntNURC)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While

            dr.Close()

            Return lst

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Consultar(ByVal pCodNurc As Decimal) As EntNURC
        Dim nState As ConnectionState
        Dim sSQL As String

        sSQL = "select * from NURC where COD_NURC = ?"

        Try
            nState = sd.State

            Dim ent As New EntNURC

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pCodNurc)

            If dr.Read Then
                ent = GerarEntidade(dr)
            End If

            dr.Close()

            Return ent

        Catch ex As ServicoDadosException
            Throw New ApplicationException(ex.Message)
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If nState <> ConnectionState.Open And sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntNURC

        Dim Ent As New EntNURC

        ent.CodNurc = CInt(NVL(row("COD_NURC"), 0))
        ent.Descr = CStr(NVL(row("DESCR"), String.Empty))
        ent.CodOrg = CInt(NVL(row("COD_ORG"), 0))
        Ent.DescrRes = CStr(NVL(row("DESCR_RES"), String.Empty))

        Return Ent

    End Function

    ' Função utilizada pelo controle DGTECCodNome do ClientePadrao
    Public Function PesqPorDescr(ByVal str As String) As DataSet

        Try

            sd.Open()

            Return sd.CreateDataSet("Select COD_NURC, DESCR from NURC where DESCR like ? ", str)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

End Class
