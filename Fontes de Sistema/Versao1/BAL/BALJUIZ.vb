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

Public Class bALJUIZ
    Inherits BaseBal

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function Listar(ByVal sFiltro As String, ByVal sOrdenacao As String) As EntJUIZ()
        Dim lst() As EntJUIZ = New EntJUIZ() {}
        Dim dr As OracleDataReader
        Dim sSQL As String
        Try
            sFiltro = "%" + sFiltro + "%"
            sSQL = "select * from JUIZ where nome like ? " + IIf(sOrdenacao = String.Empty, String.Empty, " order by " + sOrdenacao)
            dr = sd.ExecuteReader(sSQL, sFiltro)

            While dr.Read
                Array.Resize(Of EntJUIZ)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

        Return lst
    End Function

    Public Function ListarVogais() As EntJUIZ()

        Dim lst() As EntJUIZ = New EntJUIZ() {}
        Dim dr As OracleDataReader
        Dim sSQL As String

        Try
            sSQL = "select MATRICULA, NOME from rh.vwmagistrados_oe"

            sd.Open()

            dr = sd.ExecuteReader(sSQL)

            While (dr.Read)
                Array.Resize(Of EntJUIZ)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidadeII(dr)
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


    Public Function Consultar(ByVal pNumMatr As String) As EntJUIZ

        Dim nState As ConnectionState
        Dim sSQL As String

        sSQL = "select * from JUIZ where NUM_MATR = ?"

        Try
            nState = sd.State

            Dim ent As New EntJUIZ

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pNumMatr)

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

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntJUIZ
        Dim Ent As New EntJUIZ

        ent.NumMatr = CStr(NVL(row("NUM_MATR"), String.Empty))
        ent.Nome = CStr(NVL(row("NOME"), String.Empty))
        ent.Sexo = CStr(NVL(row("SEXO"), String.Empty))
        ent.DtNasc = CDate(NVL(row("DT_NASC"), #12:00:00 AM#))
        ent.UltProv = CDec(NVL(row("ULT_PROV"), 0D))
        ent.CodCargoSecretario = CDec(NVL(row("COD_CARGO_SECRETARIO"), 0D))
        Ent.Cpf = CStr(NVL(row("CPF"), String.Empty))

        Return Ent

    End Function

    Private Function GerarEntidadeII(ByVal row As OracleDataReader) As EntJUIZ
        Dim Ent As New EntJUIZ

        Ent.NumMatr = CStr(NVL(row("MATRICULA"), String.Empty))
        Ent.Nome = CStr(NVL(row("NOME"), String.Empty))

        Return Ent
    End Function

    ' Função utilizada pelo controle DGTECCodNome do ClientePadrao
    Public Function PesqPorDescr(ByVal str As String) As DataSet

        Try

            sd.Open()

            Return sd.CreateDataSet("Select NUM_MATR, NOME from JUIZ where NOME like ? ", str)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

End Class
