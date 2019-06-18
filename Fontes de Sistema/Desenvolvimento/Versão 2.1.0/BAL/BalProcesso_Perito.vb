Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalProcesso_Perito
    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal sCodProfissao As String, ByVal sCodEspecialidade As String) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(sNum_CNJ, nID_PF, sCodProfissao, sCodEspecialidade)
        Return dsSet
    End Function

    Public Function ExibirDadosEnt(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal sCodProfissao As String, ByVal sCodEspecialidade As String) As EntProcesso_Perito
        Dim EntRet As New EntProcesso_Perito
        Dim DsCNJ As DataSet

        DsCNJ = ExibirDadosSet(sNum_CNJ, nID_PF, sCodProfissao, sCodEspecialidade)

        If DsCNJ.Tables(0).Rows.Count = 1 Then
            EntRet = GerarEntidade(DsCNJ.Tables(0).Rows(0))
        Else
            EntRet = Nothing
        End If

        Return EntRet
    End Function

    Public Sub GravarProcesso_Perito(ByVal pNum_CNJ As String, ByVal pID_PF As Long, ByVal pData_Nomeacao As String, ByVal pDATA_NEGACAO As String, ByVal pDATA_ACEITACAO As String, ByVal pDATA_LIBERACAO As String, ByVal pPRAZO_ENTREGA As Integer, ByVal pCod_Profissao As Integer, ByVal pCod_Especialidade As Integer, ByVal pSigla As String, ByVal pMotivo_Recusa As String, ByVal pHonorarios As String, ByVal pNum_Oficio As Integer, ByVal pAno_Oficio As Integer, ByVal pJustica_Gratuita As String, ByVal pCod_Tipo_Pericia As Integer)
        Dim DsExibir As DataSet

        If pID_PF <> 0 Then
            DsExibir = ExibirDados(pNum_CNJ, pID_PF, pCod_Profissao, pCod_Especialidade)

            Try
                If DsExibir.Tables(0).Rows.Count > 0 Then
                    sd.ExecutaProc("Alterar_Processo_Perito", pNum_CNJ, pID_PF, _
                                       IIf(pData_Nomeacao.Trim = "", Today.ToShortDateString, pData_Nomeacao), _
                                       pDATA_LIBERACAO, IIf(pPRAZO_ENTREGA = 0, Nothing, pPRAZO_ENTREGA), _
                                       IIf(pCod_Profissao = 0, Nothing, pCod_Profissao), _
                                       pCod_Especialidade, pSigla, IIf(pNum_Oficio = 0, Nothing, pNum_Oficio), _
                                       IIf(pAno_Oficio = 0, Nothing, pAno_Oficio), pJustica_Gratuita, _
                                       IIf(pCod_Tipo_Pericia = 0, Nothing, pCod_Tipo_Pericia), _
                                       IIf(pHonorarios.Trim = "" Or pHonorarios.Trim = "0", Nothing, CDbl(pHonorarios)), _
                                       Nothing, Nothing, Nothing)
                Else
                    sd.ExecutaProc("Inserir_Processo_Perito", pNum_CNJ, pID_PF, _
                                       IIf(pData_Nomeacao.Trim = "", Today.ToShortDateString, pData_Nomeacao), _
                                       pDATA_LIBERACAO, IIf(pPRAZO_ENTREGA = 0, Nothing, pPRAZO_ENTREGA), _
                                       IIf(pCod_Profissao = 0, Nothing, pCod_Profissao), _
                                       pCod_Especialidade, pSigla, IIf(pNum_Oficio = 0, Nothing, pNum_Oficio), _
                                       IIf(pAno_Oficio = 0, Nothing, pAno_Oficio), pJustica_Gratuita, _
                                       IIf(pCod_Tipo_Pericia = 0, Nothing, pCod_Tipo_Pericia), _
                                       IIf(pHonorarios.Trim = "" Or pHonorarios.Trim = "0", Nothing, CDbl(pHonorarios)), _
                                       Nothing, Nothing, Nothing)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If
    End Sub

    Public Sub Gravar(ByVal pNum_CNJ As String, ByVal pID_PF As Long, ByVal pDATA_NEGACAO As String, ByVal pDATA_ACEITACAO As String, ByVal pMotivo_Recusa As String, ByVal pHonorarios As String)
        Dim ParametrosProcesso_Perito(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim DsExibir As DataSet

        If pID_PF = 0 Then
            Exit Sub
        End If
        'NUM_CNJ        NUMBER(20) not null,
        ParametrosProcesso_Perito(0) = New ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosProcesso_Perito(0).Valor = pNum_CNJ
        'ID_PF          NUMBER(14) not null,
        ParametrosProcesso_Perito(1) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosProcesso_Perito(1).Valor = pID_PF
        'DATA_NEGACAO   DATE,
        ParametrosProcesso_Perito(2) = New ServicoDadosOracle.ParameterInfo("dDATA_NEGACAO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_NEGACAO = "" Then
            ParametrosProcesso_Perito(2).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(2).Valor = CDate(pDATA_NEGACAO)
        End If
        'DATA_ACEITACAO DATE,
        ParametrosProcesso_Perito(3) = New ServicoDadosOracle.ParameterInfo("dDATA_ACEITACAO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_ACEITACAO = "" Then
            ParametrosProcesso_Perito(3).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(3).Valor = CDate(pDATA_ACEITACAO)
        End If
        ParametrosProcesso_Perito(4) = New ServicoDadosOracle.ParameterInfo("sMotivo_Recusa", OracleDbType.Varchar2, ParameterDirection.Input)
        If pMotivo_Recusa = "" Then
            ParametrosProcesso_Perito(4).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(4).Valor = pMotivo_Recusa
        End If
        ParametrosProcesso_Perito(5) = New ServicoDadosOracle.ParameterInfo("nHonorarios", OracleDbType.Varchar2, ParameterDirection.Input)
        If pHonorarios = "" Or pHonorarios = "0" Then
            ParametrosProcesso_Perito(5).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(5).Valor = Convert.ToDouble(pHonorarios)
        End If

        'FOS - Verificar a necessidade de informar o código da profissão e especialidade
        DsExibir = ExibirDados(pNum_CNJ, pID_PF, "", "")

        If DsExibir.Tables(0).Rows.Count > 0 Then

            'Alteracao
            sd.Open()
            'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
            sd.ExecutaProc("Alterar_Aceitacao_Processo_Per", ParametrosProcesso_Perito)
            sd.Close()
        End If

    End Sub

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntProcesso_Perito
        Dim Ent As New EntProcesso_Perito

        Ent.NUM_CNJ = Rss("NUM_CNJ")
        Ent.ID_PF = Rss("ID_PF")
        Ent.Data_Nomeacao = Rss("DATA_NOMEACAO")
        If Rss("DATA_NEGACAO").ToString <> "" Then
            Ent.Data_Negacao = Rss("DATA_NEGACAO")
        Else
            Ent.Data_Negacao = Nothing
        End If
        If Rss("DATA_ACEITACAO").ToString <> "" Then
            Ent.Data_Aceitacao = Rss("DATA_ACEITACAO")
        Else
            Ent.Data_Aceitacao = Nothing
        End If
        If Rss("DATA_LIBERACAO").ToString <> "" Then
            Ent.Data_Liberacao = Rss("DATA_LIBERACAO")
        Else
            Ent.Data_Liberacao = Nothing
        End If
        Ent.SIGLA_NOMEACAO = Rss("SIGLA_NOMEACAO")
        If Rss("MOTIVO_RECUSA").ToString <> "" Then
            Ent.Motivo_Recusa = Rss("MOTIVO_RECUSA")
        Else
            Ent.Motivo_Recusa = Nothing
        End If
        If Rss("HONORARIOS").ToString <> "" Then
            Ent.Honorarios = Rss("HONORARIOS") ' Em UFIR
        Else
            Ent.Honorarios = Nothing
        End If
        If Rss("ESPECIALIDADE").ToString <> "" Then
            Ent.DESCR_ESPECIALIDADE = Rss("ESPECIALIDADE")
        Else
            Ent.DESCR_ESPECIALIDADE = Nothing
        End If

        Ent.COD_ESPECIALIDADE = NVL(Rss("cod_especialidade"), 0)

        If Rss("DESCR_PROFISSAO").ToString <> "" Then
            Ent.DESCR_PROFISSAO = Rss("DESCR_PROFISSAO")
        Else
            Ent.DESCR_PROFISSAO = Nothing
        End If

        Ent.COD_PROFISSAO = NVL(Rss("cod_profissao"), 0)

        If Rss("JG").ToString <> "" Then
            Ent.Justica_Gratuita = Rss("JG")
        Else
            Ent.Justica_Gratuita = Nothing
        End If
        If Rss("PRAZO").ToString <> "" Then
            Ent.PRAZO_ENTREGA = Rss("PRAZO")
        Else
            Ent.PRAZO_ENTREGA = Nothing
        End If
        If Rss("COD_TIPO_PERICIA").ToString <> "" Then
            Ent.COD_TIPO_PERICIA = Rss("COD_TIPO_PERICIA")
        Else
            Ent.COD_TIPO_PERICIA = Nothing
        End If

        Return Ent
    End Function

    Public Function ExibirDados(ByVal pNum_CNJ As String, ByVal pID_PF As Long, _
                                ByVal pCodProfissao As String, ByVal pCodEspecialidade As String) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Processo_Perito", sd.CriaRefCursor, pNum_CNJ, pID_PF, pCodProfissao, pCodEspecialidade, Nothing)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosSetPer(ByVal pID_PF As Long) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDadosPer_Processo_Perito", sd.CriaRefCursor, pID_PF)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosTodos(ByVal pNum_CNJ As String) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Processo_PeritoT", sd.CriaRefCursor, pNum_CNJ)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function NumerarOficio(ByVal pNum_CNJ As String, ByVal pID_PF As Long) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("Numerar_Ofic_Nomeacao", sd.CriaRefCursor, pNum_CNJ)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirNumOficio(ByVal pNum_CNJ As String, ByVal pID_PF As String) As String
        Dim sSQL As String
        Dim m_ID_PF As Long
        Dim mNumOficio As String
        Dim mAnoOficio As String
        Dim retorno As String = ""

        If pID_PF <> "" Then
            m_ID_PF = Convert.ToInt64(pID_PF)

            sSQL = "select Num_Oficio, Ano_Oficio from processo_perito where Num_CNJ = ? and ID_PF = ?"

            Try
                sd.Open()

                Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pNum_CNJ, m_ID_PF)

                If dr.Read Then
                    mNumOficio = dr("Num_Oficio").ToString
                    mAnoOficio = dr("Ano_Oficio").ToString
                    retorno = mNumOficio + "/" + mAnoOficio
                End If

                dr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If

        Return retorno
    End Function

    Public Function ListarPeritosNomeadosSemAceitacao() As DataSet
        Dim ds As DataSet = Nothing
        Try
            ds = sd.CreateDataSet("SELECT PP.NUM_CNJ AS PROCESSO," & _
                                  "PF.NOME," & _
                                  "to_char(PP.DATA_NOMEACAO) as DATA_NOMEACAO," & _
                                  "TAB1.TELEFONE1," & _
                                  "TAB2.TELEFONE2," & _
                                  "to_char(DATA_NEGACAO) as DATA_NEGACAO " & _
                                  "FROM (SELECT PFTEL.NUM_TEL AS TELEFONE2, PFTEL.ID_PF " & _
                                        "FROM UC.PESSOAFISICATELEFONE PFTEL " & _
                                        "INNER JOIN UC.PESSOAFISICA PF2 " & _
                                        "ON PFTEL.ID_PF = PF2.ID_PF " & _
                                        "WHERE (PFTEL.SEQ_TEL = 2)) TAB2 " & _
                                        "INNER JOIN (SELECT PFTEL.NUM_TEL AS TELEFONE1, PFTEL.ID_PF " & _
                                        "FROM UC.PESSOAFISICATELEFONE PFTEL " & _
                                        "INNER JOIN UC.PESSOAFISICA PF1 " & _
                                        "ON PFTEL.ID_PF = PF1.ID_PF " & _
                                        "WHERE (PFTEL.SEQ_TEL = 1)) TAB1 " & _
                                        "ON TAB2.ID_PF = TAB1.ID_PF  INNER JOIN UC.PESSOAFISICA PF " & _
                                        "ON TAB2.ID_PF = PF.ID_PF  INNER JOIN PERITOS P " & _
                                        "ON PF.ID_PF = P.ID_PF  INNER JOIN PROCESSO_PERITO PP " & _
                                        "ON P.ID_PF = PP.ID_PF  LEFT JOIN ACEITACAO_PERITO AP " & _
                                        "ON PP.ID_NOMEACAO = AP.ID_NOMEACAO  WHERE (NOT (PP.DATA_NOMEACAO IS NULL)) " & _
                                        "AND (AP.DATA_ACEITACAO IS NULL) " & _
                                        "AND AP.DATA_NEGACAO IS NOT NULL ORDER BY PP.DATA_NOMEACAO DESC")
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

End Class

