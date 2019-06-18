Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BALUSUARIO
    Inherits BaseBal

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function Listar(ByVal nCodOrgao As String, ByVal sordernacao As String) As EntUSUARIO()

        Dim lst() As EntUSUARIO = New EntUSUARIO() {}
        Dim dr As OracleDataReader
        Dim sSql As String

        Try

            sSql = "Select Distinct u.cod_usu, u.nome_usu From seg.sistema s, seg.usuario u, seg.usuario_orgao_sistema uos " _
                    + " Where s.sg_sist='EPROT' And uos.cod_sist=s.cod_sist And uos.cod_usu = u.cod_usu And uos.cod_orgao= ?  order by u.nome_usu"

            sd.Open()

            'Franciana - valor atribuido para teste
            'nCodOrgao = 535

            dr = sd.ExecuteReader(sSql, nCodOrgao)
            While dr.Read
                Array.Resize(Of EntUSUARIO)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While
            dr.Close()

        Catch ex As Exception
            Throw
        Finally
            sd.Close()
        End Try

        Return lst

    End Function

    Public Function ListarUsuario(ByVal nCodOrgao As String, ByVal sordernacao As String) As EntUSUARIO()

        Dim lst() As EntUSUARIO = New EntUSUARIO() {}
        Dim dr As OracleDataReader
        Dim sSql As String

        Try

            sSql = "Select Distinct si.cod_usu , u.nome_usu From seg.sistema s, seg.usuario u,sigilousuario si " _
                    + " Where u.cod_usu = si.cod_usu  and si.cod_orgao = ? order by u.nome_usu "

            sd.Open()

            'Franciana - valor atribuido para teste
            'nCodOrgao = 535

            dr = sd.ExecuteReader(sSql, nCodOrgao)
            While dr.Read
                Array.Resize(Of EntUSUARIO)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While
            dr.Close()

        Catch ex As Exception
            Throw
        Finally
            sd.Close()
        End Try

        Return lst

    End Function

    Public Function Consultar(ByVal nCodOrgao, ByVal sCodUsuario) As EntUSUARIO

        Dim nState As ConnectionState
        Dim sSQL As String

        sSQL = "Select Distinct u.cod_usu, u.nome_usu From seg.sistema s, seg.usuario u, seg.usuario_orgao_sistema uos " _
                + " Where s.sg_sist='PERICIAS' And uos.cod_sist=s.cod_sist And uos.cod_usu = u.cod_usu And uos.cod_orgao= ? and u.cod_usu = ? "

        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntUSUARIO
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, nCodOrgao, sCodUsuario)
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

    Public Function Consultar(ByVal nCodOrgao) As DataSet

        Dim nState As ConnectionState
        Dim sSQL As String
        Dim ds As New DataSet

        sSQL = "Select Distinct u.cod_usu, u.nome_usu From seg.sistema s, seg.usuario u, seg.usuario_orgao_sistema uos " _
                + " Where s.sg_sist='PERICIAS' And uos.cod_sist=s.cod_sist And uos.cod_usu = u.cod_usu And uos.cod_orgao= ? order by u.nome_usu "

        Try

            nState = sd.State
            sd.Open()
            ds = sd.CreateDataSet(sSQL, nCodOrgao)
            Return ds

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

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntUSUARIO

        Dim ent As New EntUSUARIO

        ent.CodUsuario = CStr(NVL(row("cod_usu"), 0))
        ent.NomeUsuario = CStr(NVL(row("nome_usu"), 0))

        Return ent

    End Function

    Public Function ExibirComarca(ByVal nCod_Orgao As Integer) As Integer
        Dim nState As ConnectionState
        Dim sSQL As String
        Dim nCod_Comarca As Integer
        sSQL = "select cod_org,cod_com from uc.orgao where cod_org = ? "
        Try
            nState = sd.State
            sd.Open()
            '            Dim ent As New EntBairro
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, nCod_Orgao)
            nCod_Comarca = 0
            If dr.Read Then
                If IsDBNull(dr("Cod_Com")) Then
                    nCod_Comarca = 406
                Else
                    nCod_Comarca = CInt(dr("Cod_Com"))
                End If
            End If
            Return nCod_Comarca
            dr.Close()

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

End Class


