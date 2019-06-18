'Ajustar para nova Bal

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalPagamento_Perito

    Inherits BaseBAL
    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
    Public Function ExibirDadosSet(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal nCodEspecialidade As Integer) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(sNum_CNJ, nID_PF, nCodEspecialidade)
        Return dsSet
    End Function

    Public Function ExibirDadosEnt(ByVal sNum_CNJ As String, ByVal nID_PF As Long, _
                                   ByVal nCodEspecialidade As Integer) As EntPagamento_Perito
        Dim EntRet As New EntPagamento_Perito
        Dim DsCNJ As DataSet
        DsCNJ = ExibirDadosSet(sNum_CNJ, nID_PF, nCodEspecialidade)
        If DsCNJ.Tables(0).Rows.Count = 1 Then
            EntRet = GerarEntidade(DsCNJ.Tables(0).Rows(0))
        Else
            EntRet = Nothing
        End If
        Return EntRet
    End Function

    Public Sub GravarPagamento_Perito(ByVal pNum_CNJ As String, ByVal pID_PF As Long, ByVal pDATA_AUTORIZACAO As String, ByVal pDATA_CANCELAMENTO As String, ByVal pCOD_TIPO_PERICIA As Integer, ByVal pCOD_ESPECIALIDADE As Integer, ByVal pNUM_PROT As Long, ByVal pDATA_ENVIO_DGPCF As String)
        Dim ParametrosPagamento_Perito(7) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim DsExibir As DataSet
        'ID_PF As Long
        If pID_PF = 0 Then
            Exit Sub
        End If
        'NUM_CNJ string
        ParametrosPagamento_Perito(0) = New ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPagamento_Perito(0).Valor = pNum_CNJ
        'ID_PF          NUMBER(14) not null,
        ParametrosPagamento_Perito(1) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPagamento_Perito(1).Valor = pID_PF
        'DATA_AUTORIZACAO  DATE,
        ParametrosPagamento_Perito(2) = New ServicoDadosOracle.ParameterInfo("dDATA_AUTORIZACAO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_AUTORIZACAO = "" Then
            ParametrosPagamento_Perito(2).Valor = CDate(Today.ToShortDateString)
        Else
            ParametrosPagamento_Perito(2).Valor = CDate(pDATA_AUTORIZACAO)
        End If
        'DATA_CANCELAMENTO   DATE,
        ParametrosPagamento_Perito(3) = New ServicoDadosOracle.ParameterInfo("dDATA_CANCELAMENTO", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_CANCELAMENTO = "" Then
            ParametrosPagamento_Perito(3).Valor = System.DBNull.Value
        Else
            ParametrosPagamento_Perito(3).Valor = CDate(pDATA_CANCELAMENTO)
        End If
        'COD_TIPO_PERICIA Integer,
        ParametrosPagamento_Perito(4) = New ServicoDadosOracle.ParameterInfo("nCOD_TIPO_PERICIA", OracleDbType.Int32, ParameterDirection.Input)
        If pCOD_TIPO_PERICIA = 0 Then
            ParametrosPagamento_Perito(4).Valor = System.DBNull.Value
        Else
            ParametrosPagamento_Perito(4).Valor = pCOD_TIPO_PERICIA
        End If
        'COD_ESPECIALIDADE Integer,
        ParametrosPagamento_Perito(5) = New ServicoDadosOracle.ParameterInfo("nCOD_ESPECIALIDADE", OracleDbType.Int32, ParameterDirection.Input)
        If pCOD_ESPECIALIDADE = 0 Then
            ParametrosPagamento_Perito(5).Valor = System.DBNull.Value
        Else
            ParametrosPagamento_Perito(5).Valor = pCOD_ESPECIALIDADE
        End If
        'NUM_PROT Long
        ParametrosPagamento_Perito(6) = New ServicoDadosOracle.ParameterInfo("nNUM_PROT", OracleDbType.Int64, ParameterDirection.Input)
        If pNUM_PROT = 0 Then
            ParametrosPagamento_Perito(6).Valor = System.DBNull.Value
        Else
            ParametrosPagamento_Perito(6).Valor = pNUM_PROT
        End If
        'DATA_ENVIO_DGPCF Date
        ParametrosPagamento_Perito(7) = New ServicoDadosOracle.ParameterInfo("dDATA_ENVIO_DGPCF", OracleDbType.Date, ParameterDirection.Input)
        If pDATA_ENVIO_DGPCF = "" Then
            ParametrosPagamento_Perito(7).Valor = System.DBNull.Value
        Else
            If IsDate(pDATA_ENVIO_DGPCF) Then
                ParametrosPagamento_Perito(7).Valor = CDate(pDATA_ENVIO_DGPCF)
            Else
                ParametrosPagamento_Perito(7).Valor = System.DBNull.Value
            End If
        End If
        DsExibir = ExibirDados(pNum_CNJ, pID_PF, pCOD_ESPECIALIDADE)
        If DsExibir.Tables(0).Rows.Count > 0 Then
            sd.Open()
            sd.ExecuteNonQuery("Alterar_Pagamento_Perito", ParametrosPagamento_Perito)
            sd.Close()
        Else
            sd.Open()
            sd.ExecuteNonQuery("Inserir_Pagamento_Perito", ParametrosPagamento_Perito)
            sd.Close()
        End If

    End Sub

    Private Function GerarEntidade(ByVal Rss As DataRow) As EntPagamento_Perito
        Dim Ent As New EntPagamento_Perito

        Ent.NUM_CNJ = Rss("NUM_CNJ")
        Ent.ID_PF = Rss("ID_PF")
        If Rss("DATA_NEGACAO").ToString <> "" Then
            Ent.DATA_AUTORIZACAO = Rss("DATA_AUTORIZACAO")
        Else
            Ent.DATA_AUTORIZACAO = Nothing
        End If
        If Rss("DATA_CANCELAMENTO").ToString <> "" Then
            Ent.DATA_CANCELAMENTO = Rss("DATA_CANCELAMENTO")
        Else
            Ent.DATA_CANCELAMENTO = Nothing
        End If
        If Rss("COD_TIPO_PERICIA").ToString <> "" Then
            Ent.COD_TIPO_PERICIA = Rss("COD_TIPO_PERICIA")
        Else
            Ent.COD_TIPO_PERICIA = Nothing
        End If
        If Rss("COD_ESPECIALIDADE").ToString <> "" Then
            Ent.COD_ESPECIALIDADE = Rss("COD_ESPECIALIDADE")
        Else
            Ent.COD_ESPECIALIDADE = Nothing
        End If
        If Rss("NUM_PROT").ToString <> "" Then
            Ent.NUM_PROT = Rss("NUM_PROT")
        Else
            Ent.NUM_PROT = Nothing
        End If
        If Rss("DATA_ENVIO_DGPCF").ToString <> "" Then
            Ent.DATA_ENVIO_DGPCF = Rss("DATA_ENVIO_DGPCF")
        Else
            Ent.DATA_ENVIO_DGPCF = Nothing
        End If

        Return Ent

    End Function

    Public Function ExibirDados(ByVal pNum_CNJ As String, ByVal pID_PF As Long, _
                                ByVal pCodEspecialidade As Integer) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pNum_CNJ
        ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExibir(2).Valor = pID_PF
        ParametrosExibir(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCodEspecialidade", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(3).Valor = pCodEspecialidade

        ds = sd.CreateDataSet("ExibirDados_Pagamento_Perito", ParametrosExibir)

        sd.Close()
        Return ds
    End Function
    'ExibirDadosSetPer
    Public Function ExibirDadosSetPer(ByVal pID_PF As Long) As DataSet
        Dim ds As DataSet
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_PF
        ds = sd.CreateDataSet("ExibirDadosPer_Pag_Perito", ParametrosExibir)
        sd.Close()
        Return ds
    End Function

    Public Function ExibirDadosTodos(ByVal pNum_CNJ As String) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_CNJ", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pNum_CNJ
        ds = sd.CreateDataSet("ExibirDados_pagamento_PeritoT", ParametrosExibir)
        sd.Close()

        Return ds
    End Function

    'Public Function ListarPeritosNomeadosSemAceitacao() As DataSet
    '    Dim ds As DataSet

    '    sd.Open()
    '    ds = sd.CreateDataSet("SELECT PP.NUM_CNJ AS pagamento, " & _
    '                            "       PF.NOME, " & _
    '                            "       to_char(PP.DATA_NOMEACAO) as DATA_NOMEACAO, " & _
    '                            "       TAB1.TELEFONE1, " & _
    '                            "       TAB2.TELEFONE2, " & _
    '                            "       to_char(DATA_NEGACAO) as DATA_NEGACAO " & _
    '                            "  FROM (SELECT PFTEL.NUM_TEL AS TELEFONE2, PFTEL.ID_PF " & _
    '                            "          FROM UC.PESSOAFISICATELEFONE PFTEL " & _
    '                            "         INNER JOIN UC.PESSOAFISICA PF2 " & _
    '                            "            ON PFTEL.ID_PF = PF2.ID_PF " & _
    '                            "         WHERE (PFTEL.SEQ_TEL = 2)) TAB2 " & _
    '                            " INNER JOIN (SELECT PFTEL.NUM_TEL AS TELEFONE1, PFTEL.ID_PF " & _
    '                            "               FROM UC.PESSOAFISICATELEFONE PFTEL " & _
    '                            "              INNER JOIN UC.PESSOAFISICA PF1 " & _
    '                            "                 ON PFTEL.ID_PF = PF1.ID_PF " & _
    '                            "              WHERE (PFTEL.SEQ_TEL = 1)) TAB1 " & _
    '                            "    ON TAB2.ID_PF = TAB1.ID_PF " & _
    '                            " INNER JOIN UC.PESSOAFISICA PF " & _
    '                            "    ON TAB2.ID_PF = PF.ID_PF " & _
    '                            " INNER JOIN PERITOS P " & _
    '                            "    ON PF.ID_PF = P.ID_PF " & _
    '                            " INNER JOIN pagamento_PERITO PP " & _
    '                            "    ON P.ID_PF = PP.ID_PF " & _
    '                            " WHERE (NOT (PP.DATA_NOMEACAO IS NULL)) " & _
    '                            "   AND (PP.DATA_ACEITACAO IS NULL) " & _
    '                            " AND PP.DATA_NEGACAO IS NOT NULL ORDER BY PP.DATA_NOMEACAO DESC")
    '    sd.Close()

    '    Return ds
    'End Function

End Class


