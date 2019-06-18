Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BALEspecialidade

    Inherits BaseBAL
    'Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Private Ent As EntEspecialidade

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosEnt(ByVal nCod_Profissao As Integer) As EntEspecialidade
        Dim dsSet As DataSet
        dsSet = ExibirDados(nCod_Profissao)
        If dsSet.Tables(0).Rows.Count = 1 Then
            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If
        Return Ent
    End Function

    Public Function Carregar(ByVal Id As Long, ByVal IdProfissao As Long) As EntEspecialidade
        sd.Open()
        Try
            Dim ds As DataSet
            Dim especialidade As EntEspecialidade = Nothing
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ObterEspecialidade", sd.CriaRefCursor, Id, IdProfissao)

            If ds.Tables(0).Rows.Count > 0 Then
                especialidade = Entidade(ds.Tables(0).Rows(0))
            End If

            Return especialidade
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExibirDadosSet(ByVal pCod_Profissao As Integer) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(pCod_Profissao)
        Return dsSet
    End Function

    Public Function ExibirDados(ByVal nCod_Profissao As Integer) As DataSet
        Dim ds As DataSet = Nothing
        Try
            ds = sd.ExecutaProcDS("ExibirDados_Especialidade", sd.CriaRefCursor, "Descr_Especialidade", nCod_Profissao)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Sub Gravar(ByVal Ent_Especialidade As EntEspecialidade)
        Try
            If Ent_Especialidade.Cod_Especialidade = 0 Then
                sd.ExecutaProc("Inserir_Especialidade", Ent_Especialidade.Cod_Especialidade, Ent_Especialidade.Descr_Especialidade, Ent_Especialidade.Cod_Profissao, Ent_Especialidade.Data_Exclusao)
            Else
                If Not Validar(Ent_Especialidade.Cod_Especialidade) Then
                    sd.ExecutaProc("Inserir_Especialidade", Ent_Especialidade.Cod_Especialidade, Ent_Especialidade.Descr_Especialidade, Ent_Especialidade.Cod_Profissao, Ent_Especialidade.Data_Exclusao)
                Else
                    sd.ExecutaProc("Alterar_Especialidade", Ent_Especialidade.Cod_Especialidade, Ent_Especialidade.Descr_Especialidade, Ent_Especialidade.Cod_Profissao, Ent_Especialidade.Data_Exclusao)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Excluir(ByVal pCod_Especialidade As Integer) As Boolean
        Try
            sd.ExecutaProc("Excluir_Especialidade", pCod_Especialidade)

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExistePerito_Especialidade(ByVal pCod_Profissao As String, ByVal pCod_Especialidade As String) As Boolean
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExistePerito_Especialidade", sd.CriaRefCursor, pCod_Especialidade, pCod_Profissao)

            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Validar(ByVal pCod_Especialidade As Integer) As Boolean
        Dim ds As DataSet
        Try
            ds = sd.ExecutaProcDS("Validar_Especialidade", sd.CriaRefCursor, pCod_Especialidade)

            If Not ds.Tables(0).Rows(0).ItemArray Is System.DBNull.Value Then
                Return True
            Else
                Return False
            End If
        Catch
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ValidarNomeEspecialidade(ByVal sCodProfissao As String, ByVal sDescrEspecialidade As String) As Boolean

        Dim sResultado As String = String.Empty
        
        sd.Open()
        sResultado = sd.ExecutaFunc("Validar_Nome_Especialidade", 250, sCodProfissao, sDescrEspecialidade)
        sd.Close()

        If sResultado = "null" Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntEspecialidade
        Dim Ent As New EntEspecialidade

        Ent.Cod_Especialidade = Rss("COD_Especialidade")
        Ent.Descr_Especialidade = Rss("DESCR_ESPECIALIDADE")
        Ent.Cod_Profissao = Rss("COD_PROFISSAO")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")

        Return Ent
    End Function

    Private Function Entidade(ByVal Rss As DataRow) As EntEspecialidade
        Try
            Dim especialidade As New EntEspecialidade

            especialidade.Cod_Especialidade = Rss("COD_Especialidade")
            especialidade.Descr_Especialidade = Rss("DESCR_ESPECIALIDADE")
            especialidade.Cod_Profissao = Rss("COD_PROFISSAO")
            especialidade.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), Nothing)

            Return especialidade
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class

