Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalProcesso_Perito

    Inherits BaseBAL
    Dim Parametros(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    'Dim ParametrosProcesso_Perito(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Private Ent As EntEspecialidade

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
    Public Function ExibirDadosSet(ByVal sNum_CNJ As String, ByVal nID_PF As Long) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(sNum_CNJ, nID_PF)
        Return dsSet
    End Function

    Public Function ExibirDadosEnt(ByVal sNum_CNJ As String, ByVal nID_PF As Long) As EntProcesso_Perito
        Dim EntRet As New EntProcesso_Perito
        Dim DsCNJ As DataSet
        DsCNJ = ExibirDadosSet(sNum_CNJ, nID_PF)
        If DsCNJ.Tables(0).Rows.Count = 1 Then
            EntRet = GerarEntidade(DsCNJ.Tables(0).Rows(0))
        Else
            EntRet = Nothing
        End If
        Return EntRet
    End Function

    Public Sub GravarProcesso_Perito(ByVal pNum_CNJ As String, ByVal pID_PF As Long, ByVal pData_Nomeacao As String, ByVal pDATA_NEGACAO As String, ByVal pDATA_ACEITACAO As String, ByVal pDATA_LIBERACAO As String, ByVal pPRAZO_ENTREGA As Integer, ByVal pCod_Profissao As Integer, ByVal pCod_Especialidade As Integer, ByVal pSigla As String, ByVal pMotivo_Recusa As String, ByVal pHonorarios As String, ByVal pNum_Oficio As Integer, ByVal pAno_Oficio As Integer, ByVal pJustica_Gratuita As String)
        Dim ParametrosProcesso_Perito(14) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim DsExibir As DataSet
        'NUM_CNJ        NUMBER(20) not null,
        'ID_PF          NUMBER(14) not null,
        'DATA_NOMEACAO  DATE,
        'DATA_NEGACAO   DATE,
        'DATA_ACEITACAO DATE,
        'DATA_LIBERACAO DATE,
        'PRAZO_ENTREGA integer
        'nID_Proc      Number(long)
        'nCod_Tip_Proc Number(integer)
        If pID_PF = 0 Then
            Exit Sub
        End If
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
        'DATA_NEGACAO   DATE,
        ParametrosProcesso_Perito(3) = New ServicoDadosOracle.ParameterInfo("dDATA_NEGACAO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_NEGACAO = "" Then
            ParametrosProcesso_Perito(3).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(3).Valor = CDate(pDATA_NEGACAO)
        End If
        'DATA_ACEITACAO DATE,
        ParametrosProcesso_Perito(4) = New ServicoDadosOracle.ParameterInfo("dDATA_ACEITACAO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_ACEITACAO = "" Then
            ParametrosProcesso_Perito(4).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(4).Valor = CDate(pDATA_ACEITACAO)
        End If
        'DATA_LIBERACAO DATE
        ParametrosProcesso_Perito(5) = New ServicoDadosOracle.ParameterInfo("dDATA_LIBERACAO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_LIBERACAO = "" Then
            ParametrosProcesso_Perito(5).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(5).Valor = CDate(pDATA_LIBERACAO)
        End If
        ParametrosProcesso_Perito(6) = New ServicoDadosOracle.ParameterInfo("nPRAZO_ENTREGA", OracleDbType.Int32, ParameterDirection.Input)
        If pPRAZO_ENTREGA = 0 Then
            ParametrosProcesso_Perito(6).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(6).Valor = pPRAZO_ENTREGA
        End If
        ParametrosProcesso_Perito(7) = New ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        If pCod_Profissao = 0 Then
            ParametrosProcesso_Perito(7).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(7).Valor = pCod_Profissao
        End If
        ParametrosProcesso_Perito(8) = New ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        'If pCod_Especialidade = 0 Then
        'ParametrosProcesso_Perito(8).Valor = System.DBNull.Value
        'Else
        'ParametrosProcesso_Perito(8).Valor = pCod_Especialidade
        ParametrosProcesso_Perito(8).Valor = IIf(pCod_Especialidade = Nothing, 0, pCod_Especialidade)
        'End If
        ParametrosProcesso_Perito(9) = New ServicoDadosOracle.ParameterInfo("sSigla", OracleDbType.Varchar2, ParameterDirection.Input)
        If pSigla = "" Then
            ParametrosProcesso_Perito(9).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(9).Valor = pSigla
        End If
        ParametrosProcesso_Perito(10) = New ServicoDadosOracle.ParameterInfo("sMotivo_Recusa", OracleDbType.Varchar2, ParameterDirection.Input)
        If pMotivo_Recusa = "" Then
            ParametrosProcesso_Perito(10).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(10).Valor = pMotivo_Recusa
        End If
        ParametrosProcesso_Perito(11) = New ServicoDadosOracle.ParameterInfo("nHonorarios", OracleDbType.Double, ParameterDirection.Input)
        If pHonorarios = "" Or pHonorarios = "0" Then
            ParametrosProcesso_Perito(11).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(11).Valor = Convert.ToDouble(pHonorarios)
        End If
        ParametrosProcesso_Perito(12) = New ServicoDadosOracle.ParameterInfo("nNum_Oficio", OracleDbType.Int32, ParameterDirection.Input)
        If pNum_Oficio = 0 Then
            ParametrosProcesso_Perito(12).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(12).Valor = CInt(pNum_Oficio)
        End If
        ParametrosProcesso_Perito(13) = New ServicoDadosOracle.ParameterInfo("nAno_Oficio", OracleDbType.Int32, ParameterDirection.Input)
        If pAno_Oficio = 0 Then
            ParametrosProcesso_Perito(13).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(13).Valor = CInt(pAno_Oficio)
        End If
        ParametrosProcesso_Perito(14) = New ServicoDadosOracle.ParameterInfo("sJustica_Gratuita", OracleDbType.Varchar2, ParameterDirection.Input)
        If pJustica_Gratuita = "" Then
            ParametrosProcesso_Perito(14).Valor = System.DBNull.Value
        Else
            ParametrosProcesso_Perito(14).Valor = pJustica_Gratuita
        End If

        DsExibir = ExibirDados(pNum_CNJ, pID_PF)
        If DsExibir.Tables(0).Rows.Count > 0 Then
            'Alteracao
            Try
                sd.Open()
                'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
                sd.ExecuteNonQuery("Alterar_Processo_Perito", ParametrosProcesso_Perito)
            Catch ex As ServicoDadosException
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            Catch ex As ApplicationException
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            Catch ex As Exception
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            End Try
            sd.Close()
        Else
            Try
                sd.Open()
                sd.ExecuteNonQuery("Inserir_Processo_Perito", ParametrosProcesso_Perito)
            Catch ex As ServicoDadosException
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            Catch ex As ApplicationException
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            Catch ex As Exception
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            End Try
            sd.Close()
        End If

    End Sub

    Public Sub Gravar(ByVal pNum_CNJ As String, ByVal pID_PF As Long, ByVal pDATA_NEGACAO As String, ByVal pDATA_ACEITACAO As String, ByVal pMotivo_Recusa As String, ByVal pHonorarios As String)
        Dim ParametrosProcesso_Perito(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim DsExibir As DataSet
        'NUM_CNJ        NUMBER(20) not null,
        'ID_PF          NUMBER(14) not null,
        'DATA_NOMEACAO  DATE,
        'DATA_NEGACAO   DATE,
        'DATA_ACEITACAO DATE,
        'DATA_LIBERACAO DATE,
        'PRAZO_ENTREGA NUMBER
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
        DsExibir = ExibirDados(pNum_CNJ, pID_PF)
        If DsExibir.Tables(0).Rows.Count > 0 Then
            'Alteracao
            Try
                sd.Open()
                'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
                sd.ExecuteNonQuery("Alterar_Aceitacao_Processo_Per", ParametrosProcesso_Perito)
            Catch ex As ServicoDadosException
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            Catch ex As ApplicationException
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            Catch ex As Exception
                'MsgErro("Erro de Gravação!" + Chr(10) + ex.Message)
            Finally
                'MsgErro("Gravação feita com Sucesso!")
            End Try
            sd.Close()
        Else
            'MsgErro("Processo não localizado")
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
        If Rss("DESCR_PROFISSAO").ToString <> "" Then
            Ent.DESCR_PROFISSAO = Rss("DESCR_PROFISSAO")
        Else
            Ent.DESCR_PROFISSAO = Nothing
        End If
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


        Return Ent
    End Function
    Public Function ExibirDados(ByVal pNum_CNJ As String, ByVal pID_PF As Long) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pNum_CNJ
            ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosExibir(2).Valor = pID_PF
            ds = sd.CreateDataSet("ExibirDados_Processo_Perito", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    'ExibirDadosSetPer
    Public Function ExibirDadosSetPer(ByVal pID_PF As Long) As DataSet
        Dim ds As DataSet
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pID_PF
            ds = sd.CreateDataSet("ExibirDadosPer_Processo_Perito", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    Public Function ExibirDadosTodos(ByVal pNum_CNJ As String) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pNum_CNJ
            ds = sd.CreateDataSet("ExibirDados_Processo_PeritoT", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    Public Function NumerarOficio(ByVal pNum_CNJ As String, ByVal pID_PF As Long) As DataSet

        Dim ds As DataSet
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pNum_CNJ
            'ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            'ParametrosExibir(2).Valor = pID_PF

            ds = sd.CreateDataSet("Numerar_Ofic_Nomeacao", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds

    End Function
    Public Function ExibirNumOficio(ByVal pNum_CNJ As String, ByVal pID_PF As String) As String
        Dim nState As ConnectionState
        Dim sSQL As String
        Dim m_ID_PF As Long
        Dim mNumOficio As String
        Dim mAnoOficio As String
        ExibirNumOficio = ""
        If pID_PF = "" Then
            Return ExibirNumOficio
            Exit Function
        End If
        m_ID_PF = Convert.ToInt64(pID_PF)

        sSQL = "select Num_Oficio, Ano_Oficio from processo_perito where Num_CNJ = ? and ID_PF = ?"
        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntBairro
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pNum_CNJ, m_ID_PF)
            If dr.Read Then
                mNumOficio = dr("Num_Oficio").ToString
                mAnoOficio = dr("Ano_Oficio").ToString
                ExibirNumOficio = mNumOficio + "/" + mAnoOFicio
            End If
            dr.Close()
            Return ExibirNumOficio

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
    'Public Sub ExcluirPerNur(ByVal pID_PF As Integer)

    '    Dim ParametrosExibir(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

    '    If pID_PF = 0 Then
    '        Exit Sub
    '    End If
    '    Try
    '        sd.Open()
    '        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
    '        ParametrosExibir(0).Valor = pID_PF
    '        sd.CreateDataSet("ExcluirNURPerito", ParametrosExibir)
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        sd.Close()
    '    End Try

    'End Sub

End Class

