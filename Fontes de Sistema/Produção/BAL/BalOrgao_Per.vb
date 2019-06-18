Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BalOrgao_Per

    Inherits BaseBAL
    Private Ent As EntOrgao_per

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosEnt() As EntOrgao_per
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        If dsSet.Tables(0).Rows.Count = 1 Then
            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If
        Return Ent
    End Function

    Public Function ExibirDadosSet() As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        Return dsSet
    End Function

    Public Function ExibirDados() As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Orgao_Per", sd.CriaRefCursor)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ConsultarOrgProfissional(ByVal sCodOrgProfissional As String) As EntOrgao_per
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("Consultar_OrgaoProfissional", sd.CriaRefCursor, sCodOrgProfissional)

            If ds.Tables(0).Rows.Count > 0 Then
                Ent = GerarEntidade(ds.Tables(0).Rows(0))
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return Ent
    End Function

    Public Function ListarOrgNomeSigla() As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("exibirdados_orgpernomesigla", sd.CriaRefCursor)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Sub Gravar(ByVal Ent_Orgao_Per As EntOrgao_per)
        Try
            If Ent_Orgao_Per.COD_ORGAO_PER = 0 Then
                sd.ExecutaProc("Inserir_Orgao_Per", Ent_Orgao_Per.COD_ORGAO_PER, Ent_Orgao_Per.DESCR_ORGAO_PER, Ent_Orgao_Per.SIGLA_PER, Ent_Orgao_Per.Data_Exclusao)
            Else
                If Not Validar(Ent_Orgao_Per.COD_ORGAO_PER) Then
                    sd.ExecutaProc("Inserir_Orgao_Per", Ent_Orgao_Per.COD_ORGAO_PER, Ent_Orgao_Per.DESCR_ORGAO_PER, Ent_Orgao_Per.SIGLA_PER, Ent_Orgao_Per.Data_Exclusao)
                Else
                    sd.ExecutaProc("Alterar_Orgao_Per", Ent_Orgao_Per.COD_ORGAO_PER, Ent_Orgao_Per.DESCR_ORGAO_PER, Ent_Orgao_Per.SIGLA_PER, Ent_Orgao_Per.Data_Exclusao)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Excluir(ByVal pCod_Orgao_Per As Integer) As Boolean
        Try
            sd.ExecutaProc("Excluir_Orgao_Per", pCod_Orgao_Per)

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExistePerito_Orgao_Per(ByVal pCod_Orgao_Per As String) As Boolean
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExistePerito_Orgao_Per", sd.CriaRefCursor, IIf(pCod_Orgao_Per = "", 0, pCod_Orgao_Per))

            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Validar(ByVal pCod_Orgao_Per As Integer) As Boolean
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("Validar_Orgao_Per", sd.CriaRefCursor, pCod_Orgao_Per)

            If ds.Tables(0).Rows.Count > 0 Then '   (0).ItemArray Is System.DBNull.Value Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntOrgao_per
        Dim Ent As New EntOrgao_per

        Ent.COD_ORGAO_PER = NVL(Rss("COD_Orgao_Per"), 0)
        Ent.DESCR_ORGAO_PER = NVL(Rss("DESCR_Orgao_Per"), "")
        Ent.Data_Exclusao = NVL(Rss("DATA_EXCLUSAO"), "")
        Ent.SIGLA_PER = NVL(Rss("SIGLA_PER"), "")

        Return Ent
    End Function

    Public Function Validar_OrgProf_Nome(ByVal sNomeOrgProfissional As String) As Boolean
        Dim sRetorno As String = String.Empty

        Try
            sRetorno = sd.ExecutaFunc("Validar_OrgProf_Nome", 250, sNomeOrgProfissional)

            If sRetorno = "null" OrElse sRetorno Is Nothing Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ConsultarSiglaOrgaoProfissional(ByVal nCodOrgaoProfissional As Integer) As String
        Dim sRetorno As String = String.Empty

        Try
            sRetorno = sd.ExecutaFunc("siglaorgaoprofissional", 20, nCodOrgaoProfissional)

            If sRetorno = "null" OrElse sRetorno Is Nothing Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function
End Class
