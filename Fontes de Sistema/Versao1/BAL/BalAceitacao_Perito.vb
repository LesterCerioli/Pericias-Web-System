Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager


Public Class BalAceitacao_Perito

    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet(ByVal pID_Nomeacao As Integer) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDadosAceitacao(pID_Nomeacao)
        Return dsSet
    End Function
 
    Public Function ExibirDadosSetNew(ByVal pID_Nomeacao As Integer, ByVal dData_Aceitacao As Date, ByVal dData_Negacao As Date) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDadosAceitacaoNew(pID_Nomeacao, CDate(dData_Aceitacao), CDate(dData_Negacao))
        Return dsSet
    End Function

    Public Function ExibirDadosAceitaEnt(ByVal nID_Nomeacao As Integer) As EntAceitacao_Perito
        Dim EntRet As New EntAceitacao_Perito
        Dim DsAceita As DataSet
        DsAceita = ExibirDadosSet(nID_Nomeacao)
        If DsAceita.Tables(0).Rows.Count = 1 Then
            EntRet = GerarEntidadeAceita(DsAceita.Tables(0).Rows(0))
        Else
            'EntRet = Nothing
            EntRet.ID_Nomeacao = nID_Nomeacao
            EntRet.Data_Negacao = Nothing 'CDate("01/01/0001 00:00:00")
            EntRet.Data_Aceitacao = Nothing 'CDate("01/01/0001 00:00:00")
            EntRet.Motivo_Recusa = ""
            EntRet.Honorarios = 0
        End If
        Return EntRet

    End Function

    Public Function ExibirDadosAceitaEntNew(ByVal nID_Nomeacao As Integer, ByVal dData_Aceitacao As Date, ByVal dData_Negacao As Date) As EntAceitacao_Perito
        Dim EntRet As New EntAceitacao_Perito
        Dim DsAceita As DataSet
        DsAceita = ExibirDadosSetNew(nID_Nomeacao, CDate(dData_Aceitacao), CDate(dData_Negacao))
        If DsAceita.Tables(0).Rows.Count = 1 Then
            EntRet = GerarEntidadeAceita(DsAceita.Tables(0).Rows(0))
        Else
            'EntRet = Nothing
            EntRet.ID_Nomeacao = nID_Nomeacao
            EntRet.Data_Negacao = Nothing 'CDate("01/01/0001 00:00:00")
            EntRet.Data_Aceitacao = Nothing 'CDate("01/01/0001 00:00:00")
            EntRet.Motivo_Recusa = ""
            EntRet.Honorarios = 0
        End If
        Return EntRet

    End Function

    Private Function GerarEntidadeAceita(ByVal Rss As DataRow) As EntAceitacao_Perito
        Dim EntAceitacao As New EntAceitacao_Perito

        EntAceitacao.ID_Nomeacao = NVL(Rss("ID_NOMEACAO"), Nothing)
        EntAceitacao.Data_Negacao = NVL(Rss("DATA_NEGACAO"), Nothing) ' CDate("01/01/0001 00:00:00"))
        EntAceitacao.Data_Aceitacao = NVL(Rss("DATA_ACEITACAO"), Nothing) 'CDate("01/01/0001 00:00:00"))
        EntAceitacao.Motivo_Recusa = NVL(Rss("MOTIVO_RECUSA"), "")
        EntAceitacao.Honorarios = NVL(Rss("HONORARIOS"), 0) ' Em UFIR

        Return EntAceitacao

    End Function

    Public Sub GravarAceitacao(ByVal pID_Nomeacao As String, ByVal pDATA_NEGACAO As String, ByVal pDATA_ACEITACAO As String, ByVal pSigla As String, ByVal pMotivo_Recusa As String, _
                      ByVal pHonorarios As String, ByVal pHonorariosJuiz As String)

        Dim ParametrosAceitacao_Perito(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim DsExibir As DataSet
        If pID_Nomeacao = 0 Then
            Exit Sub
        End If

        Try

            ParametrosAceitacao_Perito(0) = New ServicoDadosOracle.ParameterInfo("nID_Nomeacao", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosAceitacao_Perito(0).Valor = Convert.ToDouble(pID_Nomeacao)

            'DATA_NEGACAO   DATE,
            ParametrosAceitacao_Perito(1) = New ServicoDadosOracle.ParameterInfo("dDATA_NEGACAO", OracleDbType.Date, ParameterDirection.Input)
            If pDATA_NEGACAO = "" Then
                ParametrosAceitacao_Perito(1).Valor = System.DBNull.Value
            Else
                ParametrosAceitacao_Perito(1).Valor = CDate(pDATA_NEGACAO)
            End If
            'DATA_ACEITACAO DATE,
            ParametrosAceitacao_Perito(2) = New ServicoDadosOracle.ParameterInfo("dDATA_ACEITACAO", OracleDbType.Date, ParameterDirection.Input)
            If pDATA_ACEITACAO = "" Then
                ParametrosAceitacao_Perito(2).Valor = System.DBNull.Value
            Else
                ParametrosAceitacao_Perito(2).Valor = CDate(pDATA_ACEITACAO)
            End If
            ParametrosAceitacao_Perito(3) = New ServicoDadosOracle.ParameterInfo("sSigla", OracleDbType.Varchar2, ParameterDirection.Input)
            ParametrosAceitacao_Perito(3).Valor = pSigla
            ParametrosAceitacao_Perito(4) = New ServicoDadosOracle.ParameterInfo("sMotivo_Recusa", OracleDbType.Varchar2, ParameterDirection.Input)
            If pMotivo_Recusa = "" Then
                ParametrosAceitacao_Perito(4).Valor = System.DBNull.Value
            Else
                ParametrosAceitacao_Perito(4).Valor = pMotivo_Recusa
            End If
            ParametrosAceitacao_Perito(5) = New ServicoDadosOracle.ParameterInfo("nHonorarios", OracleDbType.Int64, ParameterDirection.Input)
            If pHonorarios = "" Or pHonorarios = "0" Then
                ParametrosAceitacao_Perito(5).Valor = System.DBNull.Value
            Else
                ParametrosAceitacao_Perito(5).Valor = Convert.ToDouble(pHonorarios)
            End If

            'FOS - Verificar a necessidade de informar o código da profissão e especialidade
            DsExibir = ExibirDadosAceitacao(pID_Nomeacao)

            sd.Open()

            If DsExibir.Tables(0).Rows.Count > 0 Then
                If Qte_Aceitacoes_Proc(pID_Nomeacao) = 2 Then

                    Dim ParametrosIniciar_Pericia(0) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
                    ParametrosIniciar_Pericia(0) = New ServicoDadosOracle.ParameterInfo("nID_Nomeacao", OracleDbType.Int64, ParameterDirection.Input)
                    ParametrosIniciar_Pericia(0).Valor = Convert.ToDouble(pID_Nomeacao)

                    sd.ExecuteNonQuery("Iniciar_Pericia", ParametrosAceitacao_Perito)

                End If
                If Qte_Aceitacoes_Proc(pID_Nomeacao) = 2 Or Qte_Negacoes_Proc(pID_Nomeacao) = 1 Then
                    'Alteracao

                    'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
                    'sd.ExecuteNonQuery("Alterar_Aceitacao_Processo_Per", ParametrosAceitacao_Perito)
                    sd.ExecuteNonQuery("Alterar_Aceitacao_Perito", ParametrosAceitacao_Perito)

                Else
                    If Qte_Aceitacoes_Proc(pID_Nomeacao) = 1 And pHonorariosJuiz = "" Then
                        'Alteracao
                        'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
                        'sd.ExecuteNonQuery("Alterar_Aceitacao_Processo_Per", ParametrosAceitacao_Perito)
                        sd.ExecuteNonQuery("Alterar_Aceitacao_Perito", ParametrosAceitacao_Perito)
                    Else
                        'Inclusão
                        'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
                        'sd.ExecuteNonQuery("Inserir_Aceitacao_Processo_Per", ParametrosAceitacao_Perito)
                        sd.ExecuteNonQuery("Inserir_Aceitacao_Perito", ParametrosAceitacao_Perito)
                    End If
                End If
            Else
                'Inclusão
                'Nao é permitido trocar a data da Nomeacao(pois tem que permanecer a data da inserção
                'sd.ExecuteNonQuery("Inserir_Aceitacao_Processo_Per", ParametrosAceitacao_Perito)
                sd.ExecuteNonQuery("Inserir_Aceitacao_Perito", ParametrosAceitacao_Perito)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub
    Public Function ExibirDadosAceitacao(ByVal pID_Nomeacao As Integer) As DataSet
        
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_Nomeacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_Nomeacao

        Try

            sd.Open()

            Return sd.CreateDataSet("ExibirDados_Aceitacao_Perito", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function ExibirDadosAceitacaoNew(ByVal pID_Nomeacao As Integer, ByVal pData_Aceitacao As Date, ByVal pData_Negacao As Date) As DataSet
       
        Dim ParametrosExibir(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_Nomeacao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_Nomeacao
        ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("dData_Aceitacao", OracleDbType.Date, ParameterDirection.Input)
        ParametrosExibir(2).Valor = IIf(pData_Aceitacao = Nothing, System.DBNull.Value, pData_Aceitacao)
        ParametrosExibir(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("dData_Negacao", OracleDbType.Date, ParameterDirection.Input)
        ParametrosExibir(3).Valor = IIf(pData_Negacao = Nothing, System.DBNull.Value, pData_Negacao)

        Try

            sd.Open()

            Return sd.CreateDataSet("ExibirDados_Aceitacao_PeritoN", ParametrosExibir)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Qte_Aceitacoes_Proc(ByVal pID_Nomeacao As String) As Integer

        Dim sSQL As String

        sSQL = "Select count(PP.ID_Nomeacao) Total from Aceitacao_Perito PA, Processo_Perito PP where PA.ID_Nomeacao = PP.ID_Nomeacao and PP.ID_Nomeacao = ? and not Data_Aceitacao is null "

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)

        If dr.Read Then
            Return (dr("Total"))
        Else
            Return 0
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function Qte_Negacoes_Proc(ByVal pID_Nomeacao As String) As Integer

        Dim sSQL As String

        sSQL = "Select count(PP.ID_Nomeacao) Total from Aceitacao_Perito PA, Processo_Perito PP where PA.ID_Nomeacao = PP.ID_Nomeacao and PP.ID_Nomeacao = ? and not Data_Negacao is null "

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)

            If dr.Read Then
                Return (dr("Total"))
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExibirData_Aceitacao(ByVal pID_Nomeacao As String, ByVal Num_Aceitacao As Integer) As String

        Dim sSQL As String
        Dim m_Data_Aceitacao1 As Date
        Dim m_Data_Aceitacao2 As Date
        Dim mm_Data_Aceitacao As String

        sSQL = "Select Data_Aceitacao from Aceitacao_Perito PA, Processo_Perito PP where PA.ID_Nomeacao = PP.ID_Nomeacao and not Data_Aceitacao is null and PP.ID_Nomeacao = ? order by Data_Aceitacao"

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)
            m_Data_Aceitacao1 = Nothing

            If dr.Read Then
                m_Data_Aceitacao1 = dr("Data_Aceitacao")
                mm_Data_Aceitacao = m_Data_Aceitacao1
            Else
                mm_Data_Aceitacao = ""
            End If

            If Num_Aceitacao = 2 Then
                m_Data_Aceitacao2 = Nothing
                If dr.Read Then
                    m_Data_Aceitacao2 = dr("Data_Aceitacao")
                    mm_Data_Aceitacao = m_Data_Aceitacao2.ToShortDateString
                Else
                    mm_Data_Aceitacao = ""
                End If
            End If

            Return mm_Data_Aceitacao

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function
    Public Function ExibirData_Negacao(ByVal pID_Nomeacao As String) As String

        Dim sSQL As String
        Dim mm_Data_Negacao As String

        sSQL = "Select Data_Negacao from Aceitacao_Perito where not Data_Negacao is null and ID_Nomeacao = ?"

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)

            If dr.Read Then
                mm_Data_Negacao = dr("Data_Negacao").ToShortDateString
            Else
                mm_Data_Negacao = ""
            End If

            Return mm_Data_Negacao

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function ExibirData_Novo_Hon(ByVal pID_Nomeacao As String) As String

        Dim sSQL As String
        Dim mm_Data_Novo_Hon As String

        sSQL = "Select Data_Novo_Hon from Processo_Perito where ID_Nomeacao = ? and  not Data_Novo_Hon is null"

        Try

            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pID_Nomeacao)

            If dr.Read Then
                mm_Data_Novo_Hon = dr("Data_Novo_Hon").ToShortDateString
            Else
                mm_Data_Novo_Hon = ""
            End If

            Return mm_Data_Novo_Hon

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

End Class
