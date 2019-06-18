Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalProcesso_Perito

    Inherits BaseBAL
    '    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    'Dim ParametrosProcesso_Perito(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    '    Private Ent As EntEspecialidade

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal sCodProfissao As String, ByVal sCodEspecialidade As String, ByVal dDataNomeacao As Date) As DataSet
        Dim dsSet As DataSet
        sNum_CNJ = Trim(sNum_CNJ)

        dsSet = ExibirDados(sNum_CNJ, nID_PF, sCodProfissao, sCodEspecialidade, dDataNomeacao)

        Return dsSet

    End Function
    Public Function ExibirDadosSetID_Nom(ByVal nID_Nomeacao As integer) As DataSet

        Return ExibirDadosID_Nom(nID_Nomeacao)

    End Function

    Public Function ExibirDadosEnt(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal sCodProfissao As String, ByVal sCodEspecialidade As String, ByVal dData_Nomeacao As Date) As EntProcesso_Perito
        Dim EntRet As New EntProcesso_Perito
        Dim DsCNJ As DataSet
        sNum_CNJ = Trim(sNum_CNJ)
        DsCNJ = ExibirDadosSet(sNum_CNJ, nID_PF, sCodProfissao, sCodEspecialidade, dData_Nomeacao)
        If DsCNJ.Tables(0).Rows.Count > 0 Then
            EntRet = GerarEntidade(DsCNJ.Tables(0).Rows(0))
        Else
            EntRet = Nothing
        End If
        Return EntRet
    End Function

    Public Function ExibirDadosEntIDNom(ByVal nID_Nomeacao As Integer) As EntProcesso_Perito
        Dim EntRet As New EntProcesso_Perito
        Dim DsID_Nom As DataSet

        DsID_Nom = ExibirDadosSetID_Nom(nID_Nomeacao)
        If DsID_Nom.Tables(0).Rows.Count > 0 Then
            EntRet = GerarEntidade(DsID_Nom.Tables(0).Rows(0))
        Else
            EntRet = Nothing
        End If
        Return EntRet
    End Function

    Public Sub GravarProcesso_Perito(ByVal pNum_CNJ As String, ByVal pID_PF As Long, ByVal pData_Nomeacao As String, _
                                     ByVal pDATA_LIBERACAO As String, ByVal pPRAZO_ENTREGA As Integer, ByVal pCod_Profissao As Integer, _
                                     ByVal pCod_Especialidade As Integer, ByVal pSigla As String, ByVal pNum_Oficio As Integer, _
                                     ByVal pAno_Oficio As Integer, ByVal pJustica_Gratuita As String, ByVal pCod_Tipo_Pericia As Integer, _
                                     ByVal pHONORARIOS_JUIZ As Double, ByVal pINTERDICAO_PER As String, ByVal pData_Novo_Hon As Date, ByVal pCod_Org As Integer)
        Dim ParametrosProcesso_Perito(15) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim DsExibir As DataSet

        If pID_PF = 0 Then
            Exit Sub
        End If

        pNum_CNJ = Trim(pNum_CNJ)
        'NUM_CNJ        NUMBER(20) not null,
        ParametrosProcesso_Perito(0) = New ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosProcesso_Perito(0).Valor = pNum_CNJ
        'ID_PF          NUMBER(14) not null,
        ParametrosProcesso_Perito(1) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosProcesso_Perito(1).Valor = pID_PF
        'DATA_NOMEACAO  DATE,
        ParametrosProcesso_Perito(2) = New ServicoDadosOracle.ParameterInfo("dData_Nomeacao", OracleDbType.Date, ParameterDirection.Input)
        If pData_Nomeacao = "" Then
            ParametrosProcesso_Perito(2).Valor = CDate(Today.ToShortDateString)
        Else
            ParametrosProcesso_Perito(2).Valor = CDate(pData_Nomeacao)
        End If
        'DATA_LIBERACAO DATE
        ParametrosProcesso_Perito(3) = New ServicoDadosOracle.ParameterInfo("dDATA_LIBERACAO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_LIBERACAO = "" Then
            ParametrosProcesso_Perito(3).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(3).Valor = CDate(pDATA_LIBERACAO)
        End If
        ParametrosProcesso_Perito(4) = New ServicoDadosOracle.ParameterInfo("nPRAZO_ENTREGA", OracleDbType.Int32, ParameterDirection.Input)
        If pPRAZO_ENTREGA = 0 Then
            ParametrosProcesso_Perito(4).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(4).Valor = pPRAZO_ENTREGA
        End If
        ParametrosProcesso_Perito(5) = New ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        If pCod_Profissao = 0 Then
            ParametrosProcesso_Perito(5).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(5).Valor = pCod_Profissao
        End If
        ParametrosProcesso_Perito(6) = New ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosProcesso_Perito(6).Valor = IIf(pCod_Especialidade = Nothing, 0, pCod_Especialidade)
        ParametrosProcesso_Perito(7) = New ServicoDadosOracle.ParameterInfo("sSigla", OracleDbType.Varchar2, ParameterDirection.Input)
        If pSigla = "" Then
            ParametrosProcesso_Perito(7).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(7).Valor = pSigla
        End If
        ParametrosProcesso_Perito(8) = New ServicoDadosOracle.ParameterInfo("nNum_Oficio", OracleDbType.Int32, ParameterDirection.Input)
        If pNum_Oficio = 0 Then
            ParametrosProcesso_Perito(8).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(8).Valor = CInt(pNum_Oficio)
        End If
        ParametrosProcesso_Perito(9) = New ServicoDadosOracle.ParameterInfo("nAno_Oficio", OracleDbType.Int32, ParameterDirection.Input)
        If pAno_Oficio = 0 Then
            ParametrosProcesso_Perito(9).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(9).Valor = CInt(pAno_Oficio)
        End If
        ParametrosProcesso_Perito(10) = New ServicoDadosOracle.ParameterInfo("sJustica_Gratuita", OracleDbType.Varchar2, ParameterDirection.Input)
        If pJustica_Gratuita = "" Then
            ParametrosProcesso_Perito(10).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(10).Valor = pJustica_Gratuita
        End If
        ParametrosProcesso_Perito(11) = New ServicoDadosOracle.ParameterInfo("nCod_Tipo_Pericia", OracleDbType.Int32, ParameterDirection.Input)
        If pCod_Tipo_Pericia = 0 Then
            ParametrosProcesso_Perito(11).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(11).Valor = pCod_Tipo_Pericia
        End If

        'HONORARIOS_JUIZ, INTERDICAO_PER

        ParametrosProcesso_Perito(12) = New ServicoDadosOracle.ParameterInfo("nHonorarios_juiz", OracleDbType.Double, ParameterDirection.Input)
        If pHONORARIOS_JUIZ = Nothing Or pHONORARIOS_JUIZ = 0 Then
            ParametrosProcesso_Perito(12).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(12).Valor = pHONORARIOS_JUIZ
        End If
        ParametrosProcesso_Perito(13) = New ServicoDadosOracle.ParameterInfo("sINTERDICAO_PER", OracleDbType.Varchar2, ParameterDirection.Input)
        If pINTERDICAO_PER = "" Then
            ParametrosProcesso_Perito(13).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(13).Valor = pINTERDICAO_PER
        End If
        ParametrosProcesso_Perito(14) = New ServicoDadosOracle.ParameterInfo("dData_Novo_Hon", OracleDbType.Date, ParameterDirection.Input)
        If pData_Novo_Hon = Nothing Then
            ParametrosProcesso_Perito(14).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(14).Valor = pData_Novo_Hon
        End If
        ParametrosProcesso_Perito(15) = New ServicoDadosOracle.ParameterInfo("nCod_Org", OracleDbType.Int32, ParameterDirection.Input)
        If pCod_Org = Nothing Then
            ParametrosProcesso_Perito(15).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(15).Valor = pCod_Org
        End If
        'FOS - Verificar a necessidade de informar o código da profissão e especialidade
        DsExibir = ExibirDados(pNum_CNJ, pID_PF, pCod_Profissao, pCod_Especialidade, pData_Nomeacao)

        Try

            sd.Open()

            If DsExibir.Tables(0).Rows.Count > 0 Then
                'Alteracao
                'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
                sd.ExecuteNonQuery("Alterar_Processo_Perito", ParametrosProcesso_Perito)
            Else
                sd.ExecuteNonQuery("Inserir_Processo_Perito", ParametrosProcesso_Perito)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub
    Public Sub AtualizaProcessoPeritoEmailEnviado(ByVal sNumCNJ As String, ByVal lIdPf As Long, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer, ByVal sFlag As String, ByVal sData_Nomeacao As String)

        Dim param(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        sNumCNJ = Trim(sNumCNJ)
        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        param(0).Valor = sNumCNJ

        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Long, ParameterDirection.Input)
        param(1).Valor = lIdPf

        param(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        param(2).Valor = nCodProfissao

        param(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        param(3).Valor = nCodEspecialidade

        param(4) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sIdEmailEnviado", OracleDbType.Char, ParameterDirection.Input)
        param(4).Valor = sFlag

        param(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sData_Nomeacao", OracleDbType.Varchar2, ParameterDirection.Input)
        param(5).Valor = sData_Nomeacao

        Try

            sd.Open()

            sd.ExecuteNonQuery("Altera_Processo_Perito_Email", param)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Public Sub AtualizaProcessoPeritoInicioPer(ByVal sNumCNJ As String, ByVal lIdPf As Long, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer, ByVal sData_Inicio_Per As String, ByVal sData_Nomeacao As String)

        Dim param(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        sNumCNJ = Trim(sNumCNJ)
        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        param(0).Valor = sNumCNJ

        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Long, ParameterDirection.Input)
        param(1).Valor = lIdPf

        param(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        param(2).Valor = nCodProfissao

        param(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        param(3).Valor = nCodEspecialidade

        param(4) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sData_Inicio_Per", OracleDbType.Varchar2, ParameterDirection.Input)
        param(4).Valor = sData_Inicio_Per

        param(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sData_Nomeacao", OracleDbType.Varchar2, ParameterDirection.Input)
        param(5).Valor = sData_Nomeacao

        Try
            sd.Open()

            sd.ExecuteNonQuery("Altera_Proc_Perito_InicioPer", param)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntProcesso_Perito
        Dim Ent As New EntProcesso_Perito

        Ent.NUM_CNJ = NVL(Rss("NUM_CNJ"), "")
        Ent.ID_PF = NVL(Rss("ID_PF"), 0)
        Ent.Data_Nomeacao = NVL(Rss("DATA_NOMEACAO"), Nothing)
        Ent.Data_Liberacao = NVL(Rss("DATA_LIBERACAO"), Nothing)
        Ent.SIGLA_NOMEACAO = NVL(Rss("SIGLA_NOMEACAO"), "")
        Ent.DESCR_ESPECIALIDADE = NVL(Rss("ESPECIALIDADE"), "")
        Ent.COD_ESPECIALIDADE = NVL(Rss("cod_especialidade"), 0)
        Ent.DESCR_PROFISSAO = NVL(Rss("DESCR_PROFISSAO"), "")
        Ent.COD_PROFISSAO = NVL(Rss("cod_profissao"), 0)
        Ent.Justica_Gratuita = NVL(Rss("JG"), "")
        Ent.PRAZO_ENTREGA = NVL(Rss("PRAZO"), 0)
        Ent.COD_TIPO_PERICIA = NVL(Rss("COD_TIPO_PERICIA"), 0)
        Ent.IndEmailEnviado = NVL(Rss("ind_email_enviado"), "")
        Ent.IndTacita = NVL(Rss("ind_tacita"), "")
        Ent.HonorarioJuiz = NVL(Rss("Honorarios_Juiz"), 0)
        Ent.ID_Nomeacao = NVL(Rss("ID_Nomeacao"), 0)
        Ent.Data_Inicio_Per = NVL(Rss("Data_Inicio_Per"), Nothing)
        Ent.Interdicao_Per = NVL(Rss("Interdicao_Per"), "")

        Return Ent
    End Function

    Public Function ExibirDados(ByVal pNum_CNJ As String, ByVal pID_PF As Long, _
                                ByVal pCodProfissao As String, ByVal pCodEspecialidade As String, ByVal pDataNomeacao As Date) As DataSet

        Dim ParametrosExibir(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        pNum_CNJ = Trim(pNum_CNJ)

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pNum_CNJ
        ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExibir(2).Valor = pID_PF
        ParametrosExibir(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCodProfissao", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(3).Valor = pCodProfissao
        ParametrosExibir(4) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCodEspecialidade", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(4).Valor = pCodEspecialidade
        ParametrosExibir(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("dDataNomeacao", OracleDbType.Date, ParameterDirection.Input)
        ParametrosExibir(5).Valor = pDataNomeacao

        Try

            sd.Open()

            Return sd.CreateDataSet("ExibirDados_Processo_Perito", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExibirDadosID_Nom(ByVal pID_Nomeacao As Integer) As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_Nomeacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_Nomeacao

        Try

            sd.Open()

            Return sd.CreateDataSet("ExibirDados_ProcPer_ID_Nom", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExibirDadosSetPer(ByVal pID_PF As Long) As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_PF

        Try
            sd.Open()

            Return sd.CreateDataSet("ExibirDadosPer_Processo_Perito", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExibirDadosTodos(ByVal pNum_CNJ As String) As DataSet
      
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        pNum_CNJ = Trim(pNum_CNJ)

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pNum_CNJ

        Try
            sd.Open()

            Return sd.CreateDataSet("ExibirDados_Processo_PeritoT", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function NumerarOficio(ByVal pNum_CNJ As String, ByVal pID_PF As Long) As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        pNum_CNJ = Trim(pNum_CNJ)
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pNum_CNJ

        Try

            sd.Open()

            Return sd.CreateDataSet("Numerar_Ofic_Nomeacao", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function ExibirNumOficio(ByVal pNum_CNJ As String, ByVal pID_PF As String, ByVal pCod_Especialidade As Integer) As String

        Dim sSQL As String
        Dim m_ID_PF As Long
        Dim mNumOficio As String
        Dim mAnoOficio As String

        pNum_CNJ = Trim(pNum_CNJ)
        ExibirNumOficio = ""

        If pID_PF = "" Then
            Return ExibirNumOficio
            Exit Function
        End If

        m_ID_PF = Convert.ToInt64(pID_PF)

        sSQL = "select Num_Oficio, Ano_Oficio from processo_perito where Num_CNJ = ? and ID_PF = ? and cod_especialidade = ?"

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pNum_CNJ, m_ID_PF, pCod_Especialidade)

            If dr.Read Then
                mNumOficio = dr("Num_Oficio").ToString
                mAnoOficio = dr("Ano_Oficio").ToString
                ExibirNumOficio = mNumOficio + "/" + mAnoOficio
            End If

            dr.Close()

            Return ExibirNumOficio

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ListarPeritosNomeadosSemAceitacao() As DataSet

        Try

            sd.Open()

            Return sd.CreateDataSet("SELECT PP.NUM_CNJ AS PROCESSO, " & _
                                    "       PF.NOME, " & _
                                    "       to_char(PP.DATA_NOMEACAO) as DATA_NOMEACAO, " & _
                                    "       TAB1.TELEFONE1, " & _
                                    "       TAB2.TELEFONE2, " & _
                                    "       to_char(DATA_NEGACAO) as DATA_NEGACAO " & _
                                    "  FROM (SELECT PFTEL.NUM_TEL AS TELEFONE2, PFTEL.ID_PF " & _
                                    "          FROM UC.PESSOAFISICATELEFONE PFTEL " & _
                                    "         INNER JOIN UC.PESSOAFISICA PF2 " & _
                                    "            ON PFTEL.ID_PF = PF2.ID_PF " & _
                                    "         WHERE (PFTEL.SEQ_TEL = 2)) TAB2 " & _
                                    " INNER JOIN (SELECT PFTEL.NUM_TEL AS TELEFONE1, PFTEL.ID_PF " & _
                                    "               FROM UC.PESSOAFISICATELEFONE PFTEL " & _
                                    "              INNER JOIN UC.PESSOAFISICA PF1 " & _
                                    "                 ON PFTEL.ID_PF = PF1.ID_PF " & _
                                    "              WHERE (PFTEL.SEQ_TEL = 1)) TAB1 " & _
                                    "    ON TAB2.ID_PF = TAB1.ID_PF " & _
                                    " INNER JOIN UC.PESSOAFISICA PF " & _
                                    "    ON TAB2.ID_PF = PF.ID_PF " & _
                                    " INNER JOIN PERITOS P " & _
                                    "    ON PF.ID_PF = P.ID_PF " & _
                                    " INNER JOIN PROCESSO_PERITO PP " & _
                                    "    ON P.ID_PF = PP.ID_PF " & _
                                    " LEFT JOIN ACEITACAO_PERITO AP " & _
                                    "    ON PP.ID_NOMEACAO = AP.ID_NOMEACAO " & _
                                    " WHERE (NOT (PP.DATA_NOMEACAO IS NULL)) " & _
                                    "   AND (AP.DATA_ACEITACAO IS NULL) " & _
                                    " AND AP.DATA_NEGACAO IS NOT NULL ORDER BY PP.DATA_NOMEACAO DESC")

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Sub AtualizarProcessoPeritoNomeacaoTacita()
        Try

            sd.Open()
            sd.ExecutaProc("Gravar_Aceitacao_Tacita")

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try
    End Sub

    Public Function ExibirDadosSetNomPend(ByVal nCod_Org As Integer) As DataSet
        Return ExibirDadosNomPend(nCod_Org)
    End Function

    Public Function ExibirDadosSetNomSubst(ByVal nCod_Org As Integer) As DataSet
        Return ExibirDadosNomSubst(nCod_Org)
    End Function

    Public Function ExibirDadosAceitaEntNomPend(ByVal nCod_Org As Integer) As EntProcesso_Perito

        Dim EntNomPend As New EntProcesso_Perito
        Dim DsNomPend As DataSet
        Dim Rss As DataRow

        DsNomPend = ExibirDadosSetNomPend(nCod_Org)
        EntNomPend = Nothing

        If DsNomPend.Tables(0).Rows.Count = 1 Then
            Rss = DsNomPend.Tables(0).Rows(0)
            EntNomPend.NUM_CNJ = NVL(Rss("Num_CNJ"), Nothing)
        End If

        Return EntNomPend

    End Function

    Public Function ExibirDadosNomPend(ByVal pCod_Org As Integer) As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Org", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pCod_Org

        'Num_CNJ, Nome, Descr_Profissao, Descr_Especialidade

        Try
            sd.Open()

            'Acrescentar o filtro do Orgao
            Return sd.CreateDataSet("ExibirDados_Nom_Pendentes", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function ExibirDadosNomSubst(ByVal pCod_Org As Integer) As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Org", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pCod_Org

        'Num_CNJ, Nome, Descr_Profissao, Descr_Especialidade

        Try

            sd.Open()

            'Acrescentar o filtro do Orgao
            Return sd.CreateDataSet("ExibirDados_Nom_Substituir", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    'Public Function ExibirID_Nomeacao(ByVal pNum_CNJ As String, ByVal pDescr_Profissao As String, ByVal pDescr_Especialidade As String, ByVal pID_PF As String, ByVal pData_Nomeacao As String) As Long

    '    Dim sSQL As String
    '    Dim mm_ID_Nomeacao As Integer
    '    Dim mm_Data_Nomeacao As Date

    '    sd.Open()

    '    mm_Data_Nomeacao = CDate(pData_Nomeacao)

    '    sSQL = "Select ID_Nomeacao from Processo_Perito PP, Profissao P, Especialidades E where Num_CNJ = ? and Descr_Profissao = ? and  Descr_Especialidade = ? and ID_PF = ? and Data_Nomeacao = ? and " & _
    '           "PP.Cod_Profissao = P.Cod_Profissao and PP.Cod_Especialidade = E. Cod_Especialidade "
    '    Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pNum_CNJ, pDescr_Profissao, pDescr_Especialidade, CInt(pID_PF), pData_Nomeacao)
    '    If dr.Read Then
    '        mm_ID_Nomeacao = dr("ID_Nomecao")
    '    Else
    '        mm_ID_Nomeacao = 0
    '    End If

    '    Return mm_ID_Nomeacao

    '    sd.Close()

    'End Function

    Public Function ExibirNumProc(ByVal pID_Nomeacao As Integer) As String

        Dim sSQL As String
        Dim mm_Num_CNJ As String

        sSQL = "Select Num_CNJ from Processo_Perito PP  where ID_Nomeacao = ? "

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)

            If dr.Read Then
                mm_Num_CNJ = dr("Num_CNJ")
            Else
                mm_Num_CNJ = 0
            End If

            Return mm_Num_CNJ

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExibirID(ByVal pID_Nomeacao As Integer) As String

        Dim sSQL As String
        Dim mm_ID_PF As String

        sSQL = "Select ID_PF from Processo_Perito PP  where ID_Nomeacao = ? "

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)

            If dr.Read Then
                mm_ID_PF = dr("Num_CNJ")
            Else
                mm_ID_PF = 0
            End If

            Return mm_ID_PF

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Sub GravarDtInicio(ByVal nID_Nomeacao As Integer)

        Try
            sd.Open()

            sd.ExecuteNonQuery("update Processo_Perito set data_inicio_per = to_date(sysdate,'dd/mm/yyyy') where ID_Nomeacao = ?", nID_Nomeacao)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Public Function PossuiDataInicio(ByVal pID_Nomeacao As Integer) As Boolean

        Dim sSQL As String
        sSQL = "Select Data_Inicio_Per from Processo_Perito PP  where ID_Nomeacao = ? "

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)
            If dr.Read Then
                If dr("Data_Inicio_Per").ToString <> "" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try


    End Function

End Class

