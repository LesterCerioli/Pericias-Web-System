Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

'NUM_ANOTACAO, ID_PF, DESCR_ANOTACAO

Public Class BALAnotacao

    Inherits BaseBAL
    Dim Ent As EntAnotacao

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ConsultarAnotacao(ByVal pCodTipAnotacao As Long, ByVal pDtAnotacao As String, ByVal pcod_perito As Long) As EntAnotacao
        Dim ds As DataSet

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.CONSULTA_ANOTACAO", sd.CriaRefCursor(), pCodTipAnotacao, pDtAnotacao, pcod_perito)

            If ds.Tables(0).Rows.Count > 0 Then
                Ent = GerarEntidade(ds.Tables(0).Rows(0))
            End If

            Return Ent
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

    End Function

    Public Function VerificaSeJaFoiIncluidoAnotacaoNoDia(ByVal pCodPerito As Long) As Integer
        Try
            Dim retorno As Integer

            retorno = CInt(sd.ExecutaFunc("NOVO_PERICIAS.VERIFICA_SE_HA_ANOTACAO_DIA", 1, pCodPerito))

            sd.Close()

            Return retorno
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntAnotacao
        Dim Ent As New EntAnotacao

        Ent.CodigoPerito = NVL(Rss("COD_PERITO"), 0)
        Ent.DESCR_ANOTACAO = NVL(Rss("Descr_Anotacao"), String.Empty)
        Ent.DATA_ANOTACAO = NVL(Rss("Data_Anotacao"), Nothing)
        Ent.DATA_EXCLUSAO = NVL(Rss("Data_Exclusao"), Nothing)
        Ent.SIGLA = NVL(Rss("Sigla"), Nothing)
        Ent.Cod_Tipo_Anotacao = NVL(Rss("Cod_Tipo_Anotacao"), 0)

        Return Ent
    End Function
    Public Function ExibirDadosEnt() As EntAnotacao
        Dim dsSet As DataSet
        dsSet = ExibirDados()
        If dsSet.Tables(0).Rows.Count = 1 Then
            'For Each rs As DataRow In dsSet.Tables(0).Rows
            'GerarEntidade(rs)
            GerarEntidade(dsSet.Tables(0).Rows(0))
            'Next
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
            Return sd.ExecutaProcDS("ExibirDados_Anotacao", sd.CriaRefCursor(), "Data_Anotacao")
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    Public Function ExibirAnotPer(ByVal pcod_perito As Long) As DataSet
        Dim ds As DataSet = Nothing

        Try
            Return sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Anotacoes_Perito", sd.CriaRefCursor(), pcod_perito)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function Gravar(ByVal Ent_Anotacao As EntAnotacao) As Boolean
        Try
            sd.ExecutaProc("NOVO_PERICIAS.Inserir_Anotacao", Ent_Anotacao.CodigoPerito, Ent_Anotacao.DESCR_ANOTACAO, CDate(Today.ToShortDateString), Ent_Anotacao.SIGLA, Ent_Anotacao.Cod_Tipo_Anotacao)

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Excluir(ByVal pcod_perito As Long, ByVal pData_Anotacao As Date) As Boolean
        Try
            sd.ExecutaProc("NOVO_PERICIAS.Excluir_Anotacao", pcod_perito, pData_Anotacao)

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Validar(ByVal pCOD_PERITO As Long, ByVal pData_Anotacao As Date) As Boolean
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.Validar_Anotacao", sd.CriaRefCursor, pCOD_PERITO, pData_Anotacao)

            If ds Is Nothing Then
                Return False
            Else
                If Not ds.Tables(0).Rows.Count = 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExibirDadosExclusao(ByVal pcod_perito As Long) As Boolean
        Dim ds As DataSet = Nothing
       
        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.Exibir_Peritos_Excluidos", sd.CriaRefCursor, pcod_perito)

            If ds Is Nothing Then
                Return False
            Else
                If Not ds.Tables(0).Rows.Count = 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

    End Function

    Public Function Listar_Anotacoes_Perito(Optional ByVal codPerito As Long = 0, Optional ByVal codTipoAnotacao As Integer = 0) As DataSet
        sd.Open()
        Try
            Dim ds As DataSet = Nothing
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.Listar_Anotacoes_Perito", sd.CriaRefCursor, codPerito, codTipoAnotacao)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

End Class
