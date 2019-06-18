Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalTipoAnotacao
    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet(Optional ByVal sFiltro As Long = 0) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_TIPO_ANOTACAO", sd.CriaRefCursor, sFiltro)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntTipo_Anotacao
        Dim Ent As New EntTipo_Anotacao

        Ent.COD_TIPO_ANOTACAO = row("COD_TIPO_ANOTACAO")
        Ent.DESCR_TIPO_ANOTACAO = row("DESCR_TIPO_ANOTACAO")
        Ent.Data_Exclusao = NVL(row("DATA_EXCLUSAO"), "")


        Return Ent
    End Function

    Public Sub Gravar(ByVal Ent_Tipo_Anotacao As EntTipo_Anotacao)
        Try
            If Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO = 0 Then
                sd.ExecutaProc("Inserir_Tipo_anotacao", Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO, Ent_Tipo_Anotacao.DESCR_TIPO_ANOTACAO)
            Else
                If Not Validar(Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO) Then
                    sd.ExecutaProc("Inserir_Tipo_anotacao", Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO, Ent_Tipo_Anotacao.DESCR_TIPO_ANOTACAO)
                Else
                    sd.ExecutaProc("Alterar_Tipo_Anotacao", Ent_Tipo_Anotacao.COD_TIPO_ANOTACAO, Ent_Tipo_Anotacao.DESCR_TIPO_ANOTACAO)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Excluir(ByVal pCod_Tipo_Anotacao As Integer) As Boolean
        If Not ExisteAnotacao_Tipo_Anotacao(pCod_Tipo_Anotacao) Then
            Try
                sd.ExecutaProc("Excluir_Tipo_Anotacao", pCod_Tipo_Anotacao)

                Return True
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Function

    Public Function ExisteAnotacao_Tipo_Anotacao(ByVal pCod_Tipo_Anotacao As String) As Boolean
        If pCod_Tipo_Anotacao <> 0 Then
            Try
                Dim ds As DataSet = Nothing

                ds = sd.ExecutaProcDS("ExisteAnotacao_Tipo_Anotacao", sd.CriaRefCursor, pCod_Tipo_Anotacao)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Function

    Public Function ExisteTipo_Anotacao(ByVal pCod_Tipo_Anotacao As String) As Boolean
        If pCod_Tipo_Anotacao <> 0 Then
            Try
                Dim ds As DataSet = Nothing

                ds = sd.ExecutaProcDS("ExisteTipo_Anotacao", sd.CriaRefCursor, pCod_Tipo_Anotacao)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Function

    Public Function Validar(ByVal pCod_Tipo_Anotacao As Integer) As Boolean
        If pCod_Tipo_Anotacao <> 0 Then
            Try
                Dim ds As DataSet = Nothing

                ds = sd.ExecutaProcDS("Validar_Tipo_Anotacao", sd.CriaRefCursor, pCod_Tipo_Anotacao)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Function

    Public Function ValidarTipoAnotacaoNome(ByVal sNomeAnotacao As String) As Boolean
        If sNomeAnotacao.Trim <> "" Then
            Dim sRetorno As String = String.Empty

            Try
                sRetorno = sd.ExecutaFunc("Validar_TipoAnotacao_Nome", 250, sNomeAnotacao)

                If sRetorno.Trim = "null" OrElse sRetorno.Trim = "" Then
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Function
End Class
