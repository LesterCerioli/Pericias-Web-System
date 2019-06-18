Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BALProfissao

    Inherits BaseBAL
    '    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Private Ent As EntProfissao

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosEnt() As EntProfissao
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        If dsSet.Tables(0).Rows.Count = 1 Then
            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If
        Return Ent
    End Function

    Public Function Carregar(ByVal Id As Long) As EntProfissao
        Try
            Dim ds As DataSet
            Dim profissao As EntProfissao = Nothing

            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ObterProfissao", sd.CriaRefCursor, Id)

            If ds.Tables(0).Rows.Count > 0 Then
                profissao = GerarEntidade(ds.Tables(0).Rows(0))
            End If

            Return profissao
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExibirDadosSet() As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        Return dsSet
    End Function

    Public Function ExibirDados() As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Profissao", sd.CriaRefCursor)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Sub Gravar(ByVal Ent_Profissao As EntProfissao)
        Try
            If Ent_Profissao.Cod_Profissao = 0 Then
                sd.ExecutaProc("Inserir_Profissao", Ent_Profissao.Cod_Profissao, Ent_Profissao.Descr_Profissao, Ent_Profissao.Data_Exclusao)
            Else
                If Not Validar(Ent_Profissao.Cod_Profissao) Then
                    sd.ExecutaProc("Inserir_Profissao", Ent_Profissao.Cod_Profissao, Ent_Profissao.Descr_Profissao, Ent_Profissao.Data_Exclusao)
                Else
                    sd.ExecutaProc("Alterar_Profissao", Ent_Profissao.Cod_Profissao, Ent_Profissao.Descr_Profissao, Ent_Profissao.Data_Exclusao)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Excluir(ByVal pCod_Profissao As Integer) As Boolean
        Try
            sd.ExecutaProc("Excluir_Profissao", pCod_Profissao)

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Reativar(ByVal pDescr_Profissao As String) As Boolean
        Dim sSQL As String

        Try
            sd.Open()
            sSQL = "UPDATE Profissao set Data_Exclusao = to_date('01/01/1900','dd/mm/yyyy') " & _
                   "where Upper(Descr_Profissao) = '" & UCase(pDescr_Profissao) & "'"
            sd.ExecuteNonQuery(sSQL)

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExistePerito_Profissao(ByVal pCod_Profissao As String) As Boolean
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExistePerito_Profissao", sd.CriaRefCursor, pCod_Profissao)

            Return ds.Tables(0).Rows.Count > 0
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Validar(ByVal pCod_Profissao As Integer) As Boolean
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("Validar_Profissao", sd.CriaRefCursor, pCod_Profissao)

            Return ds.Tables(0).Rows(0).ItemArray IsNot Nothing
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntProfissao
        Dim Ent As New EntProfissao

        Ent.Cod_Profissao = Rss("COD_Profissao")
        Ent.Descr_Profissao = Rss("DESCR_Profissao")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")

        Return Ent
    End Function

    Private Function Entidade(ByVal linha As DataRow) As EntProfissao
        Dim profissao As New EntProfissao
        Try
            profissao.Cod_Profissao = linha("COD_PROFISSAO")
            profissao.Descr_Profissao = linha("DESCR_PROFISSAO")
            profissao.Data_Exclusao = NVL(linha("DATA_EXCLUSAO"), Nothing)

            Return profissao
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidarNomeProfissao(ByVal sNomeProfissao As String) As Boolean
        Dim sRetorno As String = String.Empty

        Try
            sRetorno = sd.ExecutaFunc("Validar_Nome_Profissao", 250, sNomeProfissao)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return (sRetorno <> "null" OrElse sRetorno.Trim = String.Empty)
    End Function
End Class

