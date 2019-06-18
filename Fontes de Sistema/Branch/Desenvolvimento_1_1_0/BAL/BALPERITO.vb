Imports ServicoDadosODPNET
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager
Imports Oracle.DataAccess.Client


Public Class BALPERITO

    Inherits BaseBAL
    Dim Parametros(27) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosPF(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosExt(9) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosPFEnd(10) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosPFTel(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosPFEmail(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosPFEmail1(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosExcPFEmail(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosExcPFEmail1(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosExcPFTel(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosExcPFTel1(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosExcPFEnd(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
    Dim ParametrosConta(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

    Public EntBal As New EntPERITO

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosODPNET.ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub
    Public Function ExibirDadosEnt(ByVal pTipo As String, ByVal psFiltro As String, ByVal ExibirExcl As String, Optional ByVal pCod_Orgao_Per As Integer = 0) As EntPERITO
        Dim dsSet As DataSet
        Dim dsSetConta As DataSet
       
        Dim sNomePerito As String

        dsSet = ExibirDados(pTipo, psFiltro, ExibirExcl, pCod_Orgao_Per)

        If dsSet.Tables(0).Rows.Count > 0 Then

            sNomePerito = dsSet.Tables(0).Rows(0).Item("nome").ToString
            For Each r As DataRow In dsSet.Tables(0).Rows
                If sNomePerito <> r("nome").ToString Then
                    EntBal = Nothing
                    Return EntBal
                    Exit Function
                End If
            Next

            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If
        dsSet = Nothing
        If EntBal.ID_PF = 0 Then
            Return Nothing
        End If
        '=====================================================
        'Conta Corrente do DGPCF
        dsSetConta = ExibirDadosConta(EntBal.CPF)
        If Not dsSetConta Is Nothing Then
            If dsSetConta.Tables(0).Rows.Count = 1 Then
                GerarEntidadeConta(dsSetConta.Tables(0).Rows(0))
            End If
        End If
        dsSetConta = Nothing
        '======================================================
        dsSet = Nothing

        'Tabela PessoaFisicaEndereco
        dsSet = ExibirDadosPFEndereco(EntBal.ID_PF)
        If dsSet.Tables(0).Rows.Count > 0 Then
            GerarEntidadePFEndereco(dsSet.Tables(0).Rows(0))
        End If

        If dsSet.Tables(0).Rows.Count > 1 Then
            GerarEntidadePFEndereco(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing
        'Tabela PessoaFisicaEmail
        dsSet = ExibirDadosPFEmail(EntBal.ID_PF)
        If dsSet.Tables(0).Rows.Count > 0 Then
            EntBal.EMAIL = GerarEntidadePFEmail(dsSet.Tables(0).Rows(0))
        End If
        If dsSet.Tables(0).Rows.Count > 1 Then
            EntBal.EMAIL1 = GerarEntidadePFEmail(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing
        Return EntBal
    End Function

    Public Function ExibirDadosCadPeritoEnt(ByVal pTipo As String, ByVal psFiltro As String, ByVal ExibirExcl As String) As EntPERITO
        Dim dsSet As DataSet
        Dim dsSetConta As DataSet

        Dim sNomePerito As String

        dsSet = ExibirDadosCadPerito(pTipo, psFiltro, ExibirExcl)

        If dsSet.Tables(0).Rows.Count > 0 Then

            sNomePerito = dsSet.Tables(0).Rows(0).Item("nome").ToString
            For Each r As DataRow In dsSet.Tables(0).Rows
                If sNomePerito <> r("nome").ToString Then
                    EntBal = Nothing
                    Return EntBal
                    Exit Function
                End If
            Next

            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If
        dsSet = Nothing
        If EntBal.ID_PF = 0 Then
            Return Nothing
        End If
        '=====================================================
        'Conta Corrente do DGPCF
        dsSetConta = ExibirDadosConta(EntBal.CPF)
        If Not dsSetConta Is Nothing Then
            If dsSetConta.Tables(0).Rows.Count = 1 Then
                GerarEntidadeConta(dsSetConta.Tables(0).Rows(0))
            End If
        End If
        dsSetConta = Nothing
        '======================================================
        '  dsSet = Nothing

        'Tabela(PessoaFisicaEndereco)
        dsSet = ExibirDadosPFEndereco(EntBal.ID_PF)
        If dsSet.Tables(0).Rows.Count > 0 Then
            GerarEntidadePFEndereco(dsSet.Tables(0).Rows(0))
        End If

        If dsSet.Tables(0).Rows.Count > 1 Then
            GerarEntidadePFEndereco(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing

        'Tabela(PessoaFisicaEmail)
        dsSet = ExibirDadosPFEmail(EntBal.ID_PF)
        If dsSet.Tables(0).Rows.Count > 0 Then
            EntBal.EMAIL = GerarEntidadePFEmail(dsSet.Tables(0).Rows(0))
        End If
        If dsSet.Tables(0).Rows.Count > 1 Then
            EntBal.EMAIL1 = GerarEntidadePFEmail(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing
        Return EntBal

    End Function

    Public Function ExibirTodosDadosEnt(ByVal pTipo As String, ByVal psFiltro As String, ByVal ExibirExcluidos As String, Optional ByVal pCod_Orgao_Per As Integer = 0) As EntPERITO
        Dim dsSet As DataSet

        dsSet = ExibirTodosDados(pTipo, psFiltro, ExibirExcluidos, pCod_Orgao_Per)

        If dsSet.Tables(0).Rows.Count > 1 Then
            EntBal = Nothing
            Return EntBal
        End If
        If dsSet.Tables(0).Rows.Count = 1 Then
            GerarEntidade(dsSet.Tables(0).Rows(0))
        End If
        dsSet = Nothing
        If EntBal.ID_PF = 0 Then
            Return Nothing
        End If

        dsSet = Nothing

        dsSet = ExibirDadosPFEndereco(EntBal.ID_PF)
        If dsSet.Tables(0).Rows.Count > 0 Then
            GerarEntidadePFEndereco(dsSet.Tables(0).Rows(0))
        End If

        If dsSet.Tables(0).Rows.Count > 1 Then
            GerarEntidadePFEndereco(dsSet.Tables(0).Rows(1))
        End If

        dsSet = Nothing

        dsSet = ExibirDadosPFEmail(EntBal.ID_PF)
        If dsSet.Tables(0).Rows.Count > 0 Then
            EntBal.EMAIL = GerarEntidadePFEmail(dsSet.Tables(0).Rows(0))
        End If
        If dsSet.Tables(0).Rows.Count > 1 Then
            EntBal.EMAIL1 = GerarEntidadePFEmail(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing
        Return EntBal
    End Function

    Public Function ExibirDadosSet(ByVal pTipo As String, ByVal psFiltro As String, ByVal ExibirExcl As String, Optional ByVal pCod_Orgao_Per As Integer = 0) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(pTipo, psFiltro, ExibirExcl, pCod_Orgao_Per)
        Return dsSet
    End Function

    Public Function ExibirTodosDadosSet(ByVal pTipo As String, ByVal psFiltro As String, Optional ByVal pCod_Orgao_Per As Integer = 0) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirTodosDados(pTipo, psFiltro, pCod_Orgao_Per)
        Return dsSet
    End Function

    Public Function ExibirDadosSet(ByVal pID_PF As Long) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(pID_PF)
        Return dsSet
    End Function

    Public Function ExibirDados(ByVal Tipo As String, ByVal sFiltro As String, ByVal ExibirExcluido As String, Optional ByVal pCod_Orgao_Per As Integer = 0) As DataSet
        Dim ds As DataSet
       
        sd.Open()
        sFiltro = Replace(sFiltro, "'", "")
        ds = sd.ExecutaProcDS("ExibirDados_Perito", sd.CriaRefCursor, "PF.NOME", Tipo, UCase(sFiltro), CInt(pCod_Orgao_Per), ExibirExcluido) 'ParametrosExibir)
        If ds.Tables(0).Rows.Count = 0 And Tipo = "CPF" Then
            ds = sd.ExecutaProcDS("EXIBIRDADOS_PERITO_CPF", sd.CriaRefCursor, sFiltro)
        Else
            Return ds
        End If

        Return ds
    End Function

    Public Function ExibirDadosCadPerito(ByVal Tipo As String, ByVal sFiltro As String, ByVal ExibirExcluido As String) As DataSet
        Dim ds As DataSet

        sd.Open()
        sFiltro = Replace(sFiltro, "'", "")
        ds = sd.ExecutaProcDS("ExibirDados_CADPerito", sd.CriaRefCursor, "PF.NOME", Tipo, UCase(sFiltro), ExibirExcluido) 'ParametrosExibir)
        'If ds.Tables(0).Rows.Count = 0 And Tipo = "CPF" Then
        '    ds = sd.ExecutaProcDS("EXIBIRDADOS_PERITO_CPF", sd.CriaRefCursor, sFiltro)
        'Else
        '    Return ds
        'End If

        Return ds
    End Function
   
    Public Function ExibirTodosDados(ByVal Tipo As String, ByVal sFiltro As String, ByVal ExibirExcl As String, Optional ByVal pCod_Orgao_Per As Integer = 0) As DataSet
        Dim ds As DataSet
       
        sd.Open()
        sFiltro = Replace(sFiltro, "'", "")
        ds = sd.ExecutaProcDS("ExibirTodosDados_Perito", sd.CriaRefCursor, "PF.NOME", Tipo, UCase(sFiltro), CInt(pCod_Orgao_Per)) 'ParametrosExibir)
        Return ds
        sd.Close()

    End Function

    Public Function ExibirDadosConta(ByVal pCPF As String) As DataSet
        Dim ds As DataSet

        If pCPF = "" Then
            ds = Nothing
            Return ds
        End If
        sd.Open()
        'pCPF - 11 dígitos, somente números
        ds = sd.ExecutaProcDS("ExibirDados_Conta_Perito", sd.CriaRefCursor, pCPF)
        Return ds
        sd.Close()

    End Function

    Public Function ExibirDados(ByVal nID_PF As Long) As DataSet
        Dim ds As DataSet
        
        sd.Open()
        ds = sd.ExecutaProcDS("ExibirDados_Perito_ID", sd.CriaRefCursor, nID_PF)
        Return ds
        sd.Close()

    End Function

    Public Function ExibirDadosPerDCP(ByVal pCod_Profissao As Integer, ByVal pCod_Especialidade As Integer, Optional ByVal pCod_Comarca As Integer = 0) As DataSet
        Dim ds As DataSet
        'Ativo e se atua nesta comarca e docs OK e nao excluidos, ordendos por qte de processos
        Dim ParametrosExibir(3) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pCod_Especialidade
        ParametrosExibir(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(2).Valor = pCod_Profissao
        
        If pCod_Profissao = Nothing Then
            pCod_Profissao = 0
            ds = Nothing
            Return ds
            Exit Function
        End If
        ParametrosExibir(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Comarca", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(3).Valor = pCod_Comarca

        ds = sd.CreateDataSet("ExibirDados_PeritoDCP", ParametrosExibir)
        
        Return ds
        sd.Close()
    End Function

    Public Function ExibirDadosPFEndereco(ByVal pID_PF As String) As DataSet
        Dim ds As DataSet
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("pID_PF", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_PF
        ds = sd.CreateDataSet("ExibirDados_Perito_Endereco", ParametrosExibir)
        Return ds
        sd.Close()

    End Function

    Public Function ExibirDadosPFEmail(ByVal pID_PF As String) As DataSet
        Dim ds As DataSet
        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExibir(1).Valor = pID_PF
        ds = sd.CreateDataSet("ExibirDados_Perito_Email", ParametrosExibir)
        Return ds
        sd.Close()

    End Function

    Public Function Excluir(ByVal pID_PF As Integer, ByVal pSigla As String) As Boolean
        Dim ParametrosExcluir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        ParametrosExcluir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExcluir(0).Valor = pID_PF
        ParametrosExcluir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sSigla", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExcluir(1).Valor = pSigla

        sd.Open()
        Excluir = False
        sd.ExecuteNonQuery("Excluir_Perito", ParametrosExcluir)
        sd.ExecuteNonQuery("uc.Pericias_PKG.Excluir_PessoaFisica", ParametrosExcluir)
        Excluir = True
        
        sd.Close()
        Return Excluir
    End Function

    Public Function Validar(ByVal pID_PF As Integer) As Boolean
        Dim ParametrosValidar(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim ds As DataSet

        If IsDBNull(pID_PF) Or pID_PF = 0 Then
            Return False
        End If
        sd.Open()

        ParametrosValidar(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        ParametrosValidar(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosValidar(1).Valor = pID_PF
        ds = sd.CreateDataSet("Validar_Perito", ParametrosValidar)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
        
        sd.Close()

    End Function

    Private Sub GerarEntidade(ByVal Rss As DataRow) 'As EntPERITO

        EntBal.ID_PF = NVL(Rss("ID_PF"), 0) 'Tabela Pessoa Fisica
        If NVL(Rss("COD_PERITO"), 0) = 0 Then
            Dim sSQl As String
            sd.Open()
            sSQl = "Select max(Cod_Perito) Cod from Peritos"
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQl)
            If dr.Read Then
                EntBal.Cod_Perito = dr("Cod") + 1
            Else
                EntBal.Cod_Perito = 0
            End If
            sd.Close()
        Else
            EntBal.Cod_Perito = NVL(Rss("COD_PERITO"), 0) 'Tabela Perito
        End If

        EntBal.Nome = NVL(Rss("NOME"), String.Empty) 'Tabela Pessoa Fisica
        EntBal.Cod_Tip_Sit = NVL(Rss("Cod_Tip_Sit"), 0) 'Tabela PessoaFisicaSituacao
        If EntBal.Cod_Tip_Sit = 1 Then
            EntBal.Descr_SITUACAO = "ATIVO" 'NVL(Rss("Descr"), "") ' Tabela PessoaFisicaTipoSituacao
        ElseIf EntBal.Cod_Tip_Sit = 2 Then
            EntBal.Descr_SITUACAO = "INATIVO"
        ElseIf EntBal.Cod_Tip_Sit = 19 Then
            EntBal.Descr_SITUACAO = "EXCLUIDO"
        Else
            EntBal.Descr_SITUACAO = ""
        End If
        EntBal.OBS = NVL(Rss("OBS"), "") 'Tabela Perito
        EntBal.FALTA_ENTREGAR = NVL(Rss("FALTA_ENTREGAR"), "") 'Tabela Perito
        EntBal.SIGLA = NVL(Rss("SIGLA"), "") 'Tabela Perito
        EntBal.Data_Cadastramento = NVL(Rss("Data_Cadastramento"), CDate(Now.ToShortDateString))
        EntBal.Indicacao = NVL(Rss("Indicacao"), "") 'Tabela Perito
        EntBal.Data_Exclusao = NVL(Rss("Data_Exclusao"), CDate("01/01/1900"))
        EntBal.CPF = NVL(Rss("CPF"), "") 'Tabela Pessoa Fisica
        EntBal.SITUACAO_CADASTRO = NVL(Rss("SITUACAO_CADASTRO"), "") 'Tabela Perito
        EntBal.COD_BANCO = NVL(Rss("COD_BANCO"), 0) 'Tabela Perito
        EntBal.NUM_AGENCIA = "" 'Tabela Perito
        EntBal.NOME_AGENCIA = "" 'Tabela Perito
        EntBal.NUM_CONTA_CORRENTE = "" 'Tabela Perito
        EntBal.Dt_Nasc = NVL(Rss("Dt_Nasc"), CDate("01/01/1900")) 'Tabela PessoaFisica]
        EntBal.DocNecCV = NVL(Rss("DocNecCV"), "0") 'Tabela Perito
        EntBal.DocNecCPF = NVL(Rss("DocNecCPF"), "0") 'Tabela Perito
        EntBal.DocNecFoto = NVL(Rss("DocNecFoto"), "0") 'Tabela Perito
        EntBal.DocNecOrg = NVL(Rss("DocNecOrg"), "0") 'Tabela Perito
        EntBal.DocNecHab = NVL(Rss("DocNecHab"), "0") 'Tabela Perito
        EntBal.DocNecRes = NVL(Rss("DocNecRes"), "0") 'Tabela Perito
        EntBal.DocNecImp = NVL(Rss("DocNecImp"), "0") 'Tabela Perito
        EntBal.IDGED_Foto = NVL(Rss("IDGED_Foto"), "") 'Tabela Perito
        EntBal.IDGED_CV = NVL(Rss("IDGED_CV"), "") 'Tabela Perito
        EntBal.CodTipFunc = NVL(Rss("Cod_tip_func"), 0)
        EntBal.Dt_Nasc = NVL(Rss("dt_nasc"), CDate("01/01/1900"))

    End Sub

    Private Sub GerarEntidadeConta(ByVal Rss As DataRow) 'As EntPERITO

        EntBal.CPF = NVL(Rss("COD_CGC_CPF"), 0) 'Tabela Fornecedor(Perito) SIGAF
        EntBal.COD_BANCO = NVL(Rss("COD_BANCO"), 0) 'Tabela Fornecedor(Perito) SIGAF
        EntBal.NUM_AGENCIA = NVL(Rss("NUM_AGENCIA"), "") 'Tabela Fornecedor(Perito) SIGAF
        EntBal.NOME_AGENCIA = NVL(Rss("NOME_AGENCIA"), "") 'Tabela Fornecedor(Perito) SIGAF
        EntBal.NUM_CONTA_CORRENTE = NVL(Rss("NUM_CONTA_CORRENTE"), "") 'Tabela Fornecedor(Perito) SIGAF

    End Sub

    Private Sub GerarEntidadePFEndereco(ByVal Rss As DataRow) 'As EntPERITO
        EntBal.Cod_Tip_End = NVL(Rss("Cod_Tip_End"), 0)
        If EntBal.Cod_Tip_End = 2 Then '1 Profissional, 2 - Residencial
            EntBal.Cod_Tip_Logr = NVL(Rss("Cod_Tip_Logr"), 0) 'Tabela PessoaFisicaEndereco
            EntBal.Nome_Logr = NVL(Rss("Nome_Logr"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Num_Logr = NVL(Rss("Num_Logr"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Compl_Logr = NVL(Rss("Compl_Logr"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Cod_Bairro = NVL(Rss("Cod_Bai"), 0) 'Tabela PessoaFisicaEndereco
            EntBal.Cod_Cidade = NVL(Rss("Cod_Cid"), 0) 'Tabela PessoaFisicaEndereco
            EntBal.CEP = NVL(Rss("CEP"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Descr_Bairro = NVL(Rss("Descr_Bairro"), "") 'Tabela Bairro
            EntBal.Descr_Cidade = NVL(Rss("Descr_Cidade"), "")   'Tabela Cidade
            EntBal.Descr_Tip_Logr = NVL(Rss("Descr_Tip_Logr"), "") 'Tabela UC.Logradouro
            EntBal.Sigla_UF = NVL(Rss("UF"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Seq_End = NVL(Rss("Seq_End"), 0)
        ElseIf EntBal.Cod_Tip_End = 1 Then
            EntBal.Cod_Tip_Logr1 = NVL(Rss("Cod_Tip_Logr"), 0) 'Tabela PessoaFisicaEndereco
            EntBal.Nome_Logr1 = NVL(Rss("Nome_Logr"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Num_Logr1 = NVL(Rss("Num_Logr"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Compl_Logr1 = NVL(Rss("Compl_Logr"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Cod_Bairro1 = NVL(Rss("Cod_Bai"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Cod_Cidade1 = NVL(Rss("Cod_Cid"), 0) 'Tabela PessoaFisicaEndereco
            EntBal.CEP1 = NVL(Rss("CEP"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Descr_Bairro1 = NVL(Rss("Descr_Bairro"), 0) 'Tabela Bairro
            EntBal.Descr_Cidade1 = NVL(Rss("Descr_Cidade"), "")   'Tabela Cidade
            EntBal.Descr_Tip_Logr1 = NVL(Rss("Descr_Tip_Logr"), "") 'Tabela UC.Logradouro
            EntBal.Sigla_UF1 = NVL(Rss("UF"), "") 'Tabela PessoaFisicaEndereco
            EntBal.Seq_End1 = NVL(Rss("Seq_End"), 0)
        End If
    End Sub

    Public Function GerarEntidadePFEmail(ByVal Rss As DataRow) As String
        Dim Email As String
        Email = NVL(Rss("E_MAIL"), "") 'Tabela PessoaFisicaEMAIL
        Return Email
    End Function

    Public Sub Gravar(ByVal Ent_Perito As EntPERITO, ByVal pInserirConta As Boolean)

        Dim mm_ID_PF As Integer
        Dim DsAchar As DataSet
        'Dim Msg As String
        
        sd.Open()

        If Not VerCPFPerito(Ent_Perito.CPF) Then
            mm_ID_PF = Ent_Perito.ID_PF
            CriarParametros(Ent_Perito, mm_ID_PF)
            sd.ExecuteNonQuery("Inserir_Perito", Parametros)
            CriarParametrosPF(Ent_Perito, 1)
            sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_Pessoafisica", ParametrosPF)
        End If

        If Ent_Perito.ID_PF = 0 Or Ent_Perito.ID_PF = Nothing Then
            CriarParametrosPF(Ent_Perito, 1)
            If Ent_Perito.CPF = Nothing Then
                sd.Close()
                Exit Sub
            End If

            sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_Pessoafisica", ParametrosPF)
            Ent_Perito.ID_PF = CInt(ParametrosPF(0).Valor.ToString)

            If Ent_Perito.ID_PF = 0 Then
                sd.Close()
                Exit Sub
            End If

            mm_ID_PF = Ent_Perito.ID_PF
            CriarParametros(Ent_Perito, mm_ID_PF)
            sd.ExecuteNonQuery("Inserir_Perito", Parametros)

            If pInserirConta Then
                'Msg = GravarContaCorrente(Ent_Perito.CPF, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, Ent_Perito.NOME_AGENCIA, Ent_Perito.NUM_CONTA_CORRENTE)
                GravarContaCorrente(Ent_Perito.CPF, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, Ent_Perito.NOME_AGENCIA, Ent_Perito.NUM_CONTA_CORRENTE)
            End If

            'Alterar A exibição dos dados do perito segregar cc.
            If Ent_Perito.Nome_Logr <> "" Then
                InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End)
            End If
            If Ent_Perito.Nome_Logr1 <> "" Then
                InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End1)
            End If

            If Ent_Perito.EMAIL <> "" Then
                CriarParametrosPFEmail(Ent_Perito, mm_ID_PF)
                sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_Pessoafisicaemail", ParametrosPFEmail)
            End If
            If Ent_Perito.EMAIL1 <> "" Then
                CriarParametrosPFEmail1(Ent_Perito, mm_ID_PF)
                sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_Pessoafisicaemail", ParametrosPFEmail1)
            End If
        Else
            If Validar(Ent_Perito.ID_PF) Then 'And Ent_Perito.Cod_Perito <> 0 Then
                'AlterarPessoaFisica
                mm_ID_PF = Ent_Perito.ID_PF
                CriarParametrosPF(Ent_Perito, 0)
                sd.ExecuteNonQuery("uc.Pericias_PKG.Alterar_PessoaFisica", ParametrosPF)
                CriarParametros(Ent_Perito, mm_ID_PF)
                sd.ExecuteNonQuery("Alterar_Perito", Parametros)
                If pInserirConta Then
                    'Msg = GravarContaCorrente(Ent_Perito.CPF, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, Ent_Perito.NOME_AGENCIA, Ent_Perito.NUM_CONTA_CORRENTE)
                    GravarContaCorrente(Ent_Perito.CPF, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, Ent_Perito.NOME_AGENCIA, Ent_Perito.NUM_CONTA_CORRENTE)
                End If

                'Se endereço não preenchido exclui endereço residencial
                If Ent_Perito.Nome_Logr = "" Then
                    CriarParametrosExcPFEnd(mm_ID_PF, "EXC", Ent_Perito.Cod_Tip_End, Ent_Perito.Seq_End)
                    sd.ExecuteNonQuery("uc.Pericias_PKG.Excluir_PessoaFisicaEnd", ParametrosExcPFEnd)
                Else
                    CriarParametrosExcPFEnd(mm_ID_PF, "EXB", Ent_Perito.Cod_Tip_End, Ent_Perito.Seq_End)
                    DsAchar = sd.CreateDataSet("Exibirdados_Endereco_Unico", ParametrosExcPFEnd)
                    If DsAchar.Tables(0).Rows.Count > 0 Then
                        AlterarPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End)
                    Else
                        InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End)
                    End If
                End If

                'Se endereço não preenchido exclui endereço comercial
                If Ent_Perito.Nome_Logr1 = "" Then
                    CriarParametrosExcPFEnd(mm_ID_PF, "EXC", Ent_Perito.Cod_Tip_End1, Ent_Perito.Seq_End1)
                    sd.ExecuteNonQuery("uc.Pericias_PKG.Excluir_PessoaFisicaEnd", ParametrosExcPFEnd)
                Else
                    CriarParametrosExcPFEnd(mm_ID_PF, "EXB", Ent_Perito.Cod_Tip_End1, Ent_Perito.Seq_End1)
                    DsAchar = sd.CreateDataSet("Exibirdados_Endereco_Unico", ParametrosExcPFEnd)
                    If DsAchar.Tables(0).Rows.Count > 0 Then
                        AlterarPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End1)
                    Else
                        InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End1)
                    End If
                End If

                CriarParametrosPFEmail(Ent_Perito, mm_ID_PF)
                If ParametrosPFEmail(2).Valor.ToString() = "" Or ParametrosPFEmail(2).Valor Is Nothing Then
                    CriarParametrosExcPFEmail(mm_ID_PF, "EXC")
                    sd.ExecuteNonQuery("uc.Pericias_PKG.Excluir_PessoaFisicaEmail", ParametrosExcPFEmail)
                Else
                    CriarParametrosExcPFEmail(mm_ID_PF, "EXB")
                    DsAchar = sd.CreateDataSet("Exibirdados_Email_Unico", ParametrosExcPFEmail)
                    If DsAchar.Tables(0).Rows.Count > 0 Then
                        sd.ExecuteNonQuery("uc.Pericias_PKG.Alterar_PessoaFisicaEmail", ParametrosPFEmail)
                    Else
                        sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_PessoaFisicaEmail", ParametrosPFEmail)
                    End If
                End If
                CriarParametrosPFEmail1(Ent_Perito, mm_ID_PF)
                If ParametrosPFEmail1(2).Valor.ToString() = "" Or ParametrosPFEmail1(2).Valor Is Nothing Then
                    CriarParametrosExcPFEmail1(mm_ID_PF, "EXC")
                    sd.ExecuteNonQuery("uc.Pericias_PKG.Excluir_PessoaFisicaEmail", ParametrosExcPFEmail1)
                Else
                    CriarParametrosExcPFEmail1(mm_ID_PF, "EXB")
                    DsAchar = sd.CreateDataSet("Exibirdados_Email_Unico", ParametrosExcPFEmail1)
                    If DsAchar.Tables(0).Rows.Count > 0 Then
                        sd.ExecuteNonQuery("uc.Pericias_PKG.Alterar_PessoaFisicaEmail", ParametrosPFEmail1)
                    Else
                        sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_PessoaFisicaEmail", ParametrosPFEmail1)
                    End If
                End If
            End If
        End If
        sd.Close()
    End Sub

    Private Sub InserirPessoaFisicaTelefone(ByVal m_ID_PF As Long, ByVal nCodTipTel As Integer, _
                                                ByVal sDDD As String, ByVal sTel As String, ByVal sRamal As String)

        Dim ii As Integer
        ii = 0
        'ID_PF               NUMBER(5) not null,
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 14
        ParametrosPFTel(ii).Valor = m_ID_PF 'Na inserção este valor será criado no Seq do Banco PF.
        ii = ii + 1

        'Seq_tel
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        'ParametrosPFTel(ii).Valor = 1
        ParametrosPFTel(ii).Valor = RetornaUltimoSeqTel(m_ID_PF) + 1
        ii = ii + 1

        'Cod_Tip_Tel - (1-Profissional, 2-Residencial,3-Fax,4-Celular,9-Outros)
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        ParametrosPFTel(ii).Valor = IIf(nCodTipTel = Nothing, System.DBNull.Value, nCodTipTel)
        ii = ii + 1

        'Num_DDD
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_DDD", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 3
        ParametrosPFTel(ii).Valor = IIf(sDDD = Nothing, System.DBNull.Value, sDDD)
        ii = ii + 1

        'Num_Tel 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Tel", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 30
        ParametrosPFTel(ii).Valor = IIf(sTel = Nothing, System.DBNull.Value, sTel)
        ii = ii + 1

        'Num_Ramal 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Ramal", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 8
        ParametrosPFTel(ii).Valor = IIf(sRamal = Nothing, System.DBNull.Value, sRamal)

        sd.Open()
        sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_PessoaFisicaTel", ParametrosPFTel)
        sd.Close()

    End Sub

    Private Sub InserirPessoaFisicaEndereco(ByVal ent As EntPERITO, ByVal m_ID_PF As Long, ByVal nCodTipEnd As Integer)

        Dim ii As Integer
        ii = 0

        'nID_PF
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 14
        ParametrosPFEnd(ii).Valor = m_ID_PF
        ii = ii + 1

        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_End", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 1
        ParametrosPFEnd(ii).Valor = RetornaUltimoSeqEnd(CStr(m_ID_PF)) + 1
        ii = ii + 1

        'CEP
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sCEP", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 9
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.CEP.ToString, ent.CEP1.ToString)
        ii = ii + 1

        'Cod_Tip_End (1-Profissional, 2-Residencial,9-Outros)
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_End", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 1
        ParametrosPFEnd(ii).Valor = nCodTipEnd
        ii = ii + 1

        'Cod_Tip_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_Logr", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 3
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Cod_Tip_Logr, ent.Cod_Tip_Logr1)
        ii = ii + 1
        'Nome_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sNome_Logr", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 100
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Nome_Logr.ToString, ent.Nome_Logr1.ToString)
        ii = ii + 1
        'Num_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Logr", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 6
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Num_Logr.ToString, ent.Num_Logr1.ToString)
        ii = ii + 1
        'Compl_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sCompl_Logr", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 20
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Compl_Logr.ToString, ent.Compl_Logr1.ToString)
        ii = ii + 1

        'Cod_Bai
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Bai", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 5
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Cod_Bairro, ent.Cod_Bairro1)
        ii = ii + 1
        'sNOME_BAI, nCOD_CID, sNOME_CID, sUF
        'Cod_Cid
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Cid", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 10
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Cod_Cidade, ent.Cod_Cidade1)
        ii = ii + 1
        'UF
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 10
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Sigla_UF.ToString, ent.Sigla_UF1.ToString)

        sd.Open()
        sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_PessoaFisicaEnd", ParametrosPFEnd)
        sd.Close()

    End Sub

    Private Sub AlterarPessoaFisicaEndereco(ByVal ent As EntPERITO, ByVal m_ID_PF As Long, ByVal nCodTipEnd As Integer)

        Dim ii As Integer
        ii = 0
        'nID_PF
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 14
        ParametrosPFEnd(ii).Valor = m_ID_PF
        ii = ii + 1

        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_End", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 1
        ParametrosPFEnd(ii).Valor = CInt(IIf(nCodTipEnd = 2, ent.Seq_End, ent.Seq_End1))
        ii = ii + 1

        'CEP
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sCEP", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 9
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.CEP.ToString(), ent.CEP1.ToString)
        ii = ii + 1

        'Cod_Tip_End (1-Profissional, 2-Residencial,9-Outros)
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_End", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 1
        ParametrosPFEnd(ii).Valor = nCodTipEnd
        ii = ii + 1

        'Cod_Tip_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_Logr", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 3
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Cod_Tip_Logr.ToString(), ent.Cod_Tip_Logr1.ToString())
        ii = ii + 1

        'Nome_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sNome_Logr", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 100
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Nome_Logr.ToString, ent.Nome_Logr1.ToString)
        ii = ii + 1
        'Num_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Logr", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 6
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Num_Logr.ToString, ent.Num_Logr1.ToString)
        ii = ii + 1
        'Compl_Logr
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sCompl_Logr", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 20
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Compl_Logr.ToString, ent.Compl_Logr1.ToString)
        ii = ii + 1

        'Cod_Bai
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Bai", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 5
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Cod_Bairro, ent.Cod_Bairro1)
        ii = ii + 1
        'sNOME_BAI, nCOD_CID, sNOME_CID, sUF
        'Cod_Cid
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Cid", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 10
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Cod_Cidade, ent.Cod_Cidade1)
        ii = ii + 1
        'UF
        ParametrosPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEnd(ii).Tamanho = 10
        ParametrosPFEnd(ii).Valor = IIf(nCodTipEnd = 2, ent.Sigla_UF.ToString, ent.Sigla_UF1.ToString)

        sd.Open()
        sd.ExecuteNonQuery("uc.Pericias_PKG.Alterar_PessoaFisicaEnd", ParametrosPFEnd)
        sd.Close()

    End Sub

    Private Sub AlterarPessoaFisicaTelefone(ByVal m_ID_PF As Long, ByVal nSeqTel As Integer, ByVal nCodTipTel As Integer, _
                                                ByVal sDDD As String, ByVal sTel As String, ByVal sRamal As String)

        Dim ii As Integer
        ii = 0
        'ID_PF               NUMBER(5) not null,
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 14
        ParametrosPFTel(ii).Valor = m_ID_PF 'Na inserção este valor será criado no Seq do Banco PF.
        ii = ii + 1

        'Seq_tel
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        'ParametrosPFTel(ii).Valor = 1
        ParametrosPFTel(ii).Valor = nSeqTel
        ii = ii + 1

        'Cod_Tip_Tel - (1-Profissional, 2-Residencial,3-Fax,4-Celular,9-Outros)
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 1
        ParametrosPFTel(ii).Valor = IIf(nCodTipTel = Nothing, System.DBNull.Value, nCodTipTel)
        ii = ii + 1

        'Num_DDD
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_DDD", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 3
        ParametrosPFTel(ii).Valor = IIf(sDDD = Nothing, System.DBNull.Value, sDDD)
        ii = ii + 1

        'Num_Tel 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Tel", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 30
        ParametrosPFTel(ii).Valor = IIf(sTel = Nothing, System.DBNull.Value, sTel)
        ii = ii + 1

        'Num_Ramal 
        ParametrosPFTel(ii) = New ServicoDadosOracle.ParameterInfo("sNum_Ramal", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFTel(ii).Tamanho = 8
        ParametrosPFTel(ii).Valor = IIf(sRamal = Nothing, System.DBNull.Value, sRamal)

        sd.Open()
        sd.ExecuteNonQuery("uc.Pericias_PKG.Alterar_PessoaFisicaTel", ParametrosPFTel)
        sd.Close()

    End Sub

    Public Function GravarExt(ByVal Ent_Perito As EntPERITO) As Long
        Dim mm_ID_PF As Long

        sd.Open()
        'Inserir Perito
        If Ent_Perito.CPF = Nothing Then
            sd.Close()
            Exit Function
        End If
        CriarParametrosPF(Ent_Perito, 1)
        sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_Pessoafisica", ParametrosPF)
        Ent_Perito.ID_PF = CInt(ParametrosPF(0).Valor.ToString)
        If Ent_Perito.ID_PF = 0 Then
            sd.Close()
            Exit Function
        End If
        mm_ID_PF = Ent_Perito.ID_PF
        CriarParametrosExt(Ent_Perito, mm_ID_PF)
        sd.ExecuteNonQuery("Inserir_Perito_Ext", ParametrosExt)
        'Alterar A exibição dos dados do perito segregar cc.
        If Ent_Perito.Nome_Logr <> "" Then
            InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End)
        End If
        If Ent_Perito.Nome_Logr1 <> "" Then
            InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End1)
        End If

        If Ent_Perito.EMAIL <> "" Then
            CriarParametrosPFEmail(Ent_Perito, mm_ID_PF)
            sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_Pessoafisicaemail", ParametrosPFEmail)
        End If
        If Ent_Perito.EMAIL1 <> "" Then
            CriarParametrosPFEmail1(Ent_Perito, mm_ID_PF)
            sd.ExecuteNonQuery("uc.Pericias_PKG.Inserir_Pessoafisicaemail", ParametrosPFEmail1)
        End If

        sd.Close()
        Return mm_ID_PF

    End Function

    Public Function GravarContaCorrente(ByVal pCPF As String, ByVal pCod_Banco As String, ByVal pNum_Agencia As String, ByVal pNome_Agencia As String, ByVal pNum_ContaCorrente As String) As String

        ' sCodBanco     varchar2,
        ' sCPF          varchar2
        ' sCodBanco     varchar2,
        ' sCodAgencia   varchar2,
        ' sNomeAgencia  varchar2,
        ' sCodConta     varchar2,
        '  Dim Msg As String = "ERRO NA GRAVAÇÃO(PROCEDURE)"

        sd.Open()
        ParametrosConta(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCodBanco", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosConta(0).Tamanho = 3
        ParametrosConta(0).Valor = pCod_Banco
        ParametrosConta(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCodAgencia", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosConta(1).Tamanho = 6
        ParametrosConta(1).Valor = pNum_Agencia
        ParametrosConta(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNomeAgencia", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosConta(2).Tamanho = 20
        ParametrosConta(2).Valor = pNome_Agencia
        ParametrosConta(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCodConta", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosConta(3).Tamanho = 10
        ParametrosConta(3).Valor = pNum_ContaCorrente
        ParametrosConta(4) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sCPF", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosConta(4).Tamanho = 11
        ParametrosConta(4).Valor = pCPF
        ParametrosConta(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sMsg", OracleDbType.Varchar2, ParameterDirection.Output)
        ParametrosConta(5).Tamanho = 255

        'ParametrosConta(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sMsg", OracleDbType.Varchar2, ParameterDirection.Output)
        'sCodBanco     varchar2,
        'sCodAgencia   varchar2,
        'sNomeAgencia  varchar2,
        'sCodConta     varchar2,
        'sCPF(varchar2)
        sd.ExecuteNonQuery("pacote_perito.altera_perito_financeiro", ParametrosConta)

        Return ParametrosConta(5).Valor.ToString()
        sd.Close()

    End Function

    Private Sub CriarParametros(ByVal entbal As EntPERITO, ByVal m_ID_PF As Long)
        Dim iii As Integer

        'Novo Peritos
        'COD_PERITO               NUMBER(5) not null,
        'NUM_REG                  VARCHAR2(15),
        'OBS                      VARCHAR2(250),
        'FALTA_ENTREGAR           VARCHAR2(100),
        'Cod_Orgao_Per        NUMBER,
        'SIGLA                    VARCHAR2(20),
        'DATA_CADASTRAMENTO       DATE not null,
        'INDICACAO                CHAR(1),
        'DATA_EXCLUSAO            DATE,
        'SITUACAO_CADASTRO        CHAR(1),
        'ID_PF
        'Cod_Banco                Number(3),
        'Num_Agencia              Varchar2(10),
        'Num_Conta_Corrente       Varchar2(20),
        'Cod_Orgao_Per            Number,


        'COD_PERITO               NUMBER(5) not null,
        Parametros(0) = New ServicoDadosOracle.ParameterInfo("nCOD_PERITO", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(0).Valor = IIf(entbal.Cod_Perito = Nothing, System.DBNull.Value, entbal.Cod_Perito)

        'NUM_REG                  VARCHAR2(15),
        Parametros(1) = New ServicoDadosOracle.ParameterInfo("sNUM_REG", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(1).Valor = System.DBNull.Value

        'OBS                      VARCHAR2(250),
        Parametros(2) = New ServicoDadosOracle.ParameterInfo("sOBS ", OracleDbType.Varchar2, ParameterDirection.Input)
        'Parametros(2).Tamanho = 250
        Parametros(2).Valor = IIf(entbal.OBS = Nothing, System.DBNull.Value, entbal.OBS)

        'FALTA_ENTREGAR           VARCHAR2(100),
        Parametros(3) = New ServicoDadosOracle.ParameterInfo("sFALTA_ENTREGAR", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(3).Valor = IIf(entbal.FALTA_ENTREGAR = Nothing, System.DBNull.Value, entbal.FALTA_ENTREGAR)

        'Cod_Orgao_Per        NUMBER,
        Parametros(4) = New ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(4).Valor = System.DBNull.Value

        'Cod_Especialidade
        Parametros(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(5).Valor = System.DBNull.Value

        'SIGLA                    VARCHAR2(20),
        Parametros(6) = New ServicoDadosOracle.ParameterInfo("sSIGLA", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(6).Valor = IIf(entbal.SIGLA = Nothing, System.DBNull.Value, entbal.SIGLA)

        'DATA_CADASTRAMENTO       DATE
        Parametros(7) = New ServicoDadosOracle.ParameterInfo("dDATA_CADASTRAMENTO ", OracleDbType.Date, ParameterDirection.Input)

        If entbal.Data_Cadastramento = Nothing Then
            entbal.Data_Cadastramento = Today
        End If
        Parametros(7).Valor = IIf(entbal.Data_Cadastramento = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), entbal.Data_Cadastramento)

        'INDICACAO                CHAR(1)  
        Parametros(8) = New ServicoDadosOracle.ParameterInfo("sINDICACAO", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(8).Valor = IIf(entbal.Indicacao = Nothing, System.DBNull.Value, entbal.Indicacao)

        'DATA_EXCLUSÂO            DATE  
        Parametros(9) = New ServicoDadosOracle.ParameterInfo("dDATA_Exclusao", OracleDbType.Date, ParameterDirection.Input)
        Parametros(9).Valor = IIf(entbal.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), entbal.Data_Exclusao)

        'SITUACAO_CADASTRO
        Parametros(10) = New ServicoDadosOracle.ParameterInfo("sSITUACAO_CADASTRO", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(10).Valor = IIf(entbal.SITUACAO_CADASTRO = Nothing, System.DBNull.Value, entbal.SITUACAO_CADASTRO)

        'COD_BANCO
        Parametros(11) = New ServicoDadosOracle.ParameterInfo("nCOD_BANCO", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(11).Valor = IIf(entbal.COD_BANCO = Nothing, System.DBNull.Value, entbal.COD_BANCO)

        'NUM_AGENCIA
        Parametros(12) = New ServicoDadosOracle.ParameterInfo("sNUM_AGENCIA", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(12).Valor = IIf(entbal.NUM_AGENCIA = Nothing, System.DBNull.Value, entbal.NUM_AGENCIA)

        'NUM_CONTA_CORRENTE
        Parametros(13) = New ServicoDadosOracle.ParameterInfo("sNUM_CONTA_CORRENTE", OracleDbType.Varchar2, ParameterDirection.Input)
        'Parametros(13).Tamanho = 1
        Parametros(13).Valor = IIf(entbal.NUM_CONTA_CORRENTE = Nothing, System.DBNull.Value, entbal.NUM_CONTA_CORRENTE)

        'ID_PF               NUMBER(5) not null,
        Parametros(14) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        Parametros(14).Valor = m_ID_PF 'IIf(m_ID_PF = Nothing, System.DBNull.Value, m_ID_PF) ' EntBal.ID_PF)
        iii = 15

        'DOCNECCV
        '15
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sDOCNECCV", OracleDbType.Char, ParameterDirection.Input)
        Parametros(iii).Tamanho = 1
        Parametros(iii).Valor = IIf(entbal.DocNecCV = Nothing, System.DBNull.Value, entbal.DocNecCV)
        iii = iii + 1

        '16
        'DOCNECCPF
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sDOCNECCPF", OracleDbType.Char, ParameterDirection.Input)
        Parametros(iii).Tamanho = 1
        Parametros(iii).Valor = IIf(entbal.DocNecCPF = Nothing, System.DBNull.Value, entbal.DocNecCPF)
        iii = iii + 1

        'DOCNECFOTO
        '17
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sDOCNECFOTO", OracleDbType.Char, ParameterDirection.Input)
        Parametros(iii).Tamanho = 1
        Parametros(iii).Valor = IIf(entbal.DocNecFoto = Nothing, System.DBNull.Value, entbal.DocNecFoto)
        iii = iii + 1

        'DOCNECORG
        '18
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sDOCNECORG", OracleDbType.Char, ParameterDirection.Input)
        Parametros(iii).Tamanho = 1
        Parametros(iii).Valor = IIf(entbal.DocNecOrg = Nothing, System.DBNull.Value, entbal.DocNecOrg)
        iii = iii + 1

        'DOCNECHAB
        '19
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sDOCNECHAB", OracleDbType.Char, ParameterDirection.Input)
        Parametros(iii).Tamanho = 1
        Parametros(iii).Valor = IIf(entbal.DocNecHab = Nothing, System.DBNull.Value, entbal.DocNecHab)
        iii = iii + 1

        '20
        'DOCNECRES
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sDOCNECRES", OracleDbType.Char, ParameterDirection.Input)
        Parametros(iii).Tamanho = 1
        Parametros(iii).Valor = IIf(entbal.DocNecRes = Nothing, System.DBNull.Value, entbal.DocNecRes)
        iii = iii + 1

        '21
        'DOCNECIMP
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sDOCNECIMP", OracleDbType.Char, ParameterDirection.Input)
        Parametros(iii).Tamanho = 1
        Parametros(iii).Valor = IIf(entbal.DocNecImp = Nothing, System.DBNull.Value, entbal.DocNecImp)
        iii = iii + 1

        '22
        'IDGED_Foto
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sIDGED_Foto", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(iii).Tamanho = 48
        Parametros(iii).Valor = IIf(entbal.IDGED_Foto = Nothing, System.DBNull.Value, entbal.IDGED_Foto)
        iii = iii + 1

        'IDGED_CV
        '23
        Parametros(iii) = New ServicoDadosOracle.ParameterInfo("sIDGED_CV", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(iii).Tamanho = 48
        Parametros(iii).Valor = IIf(entbal.IDGED_CV = Nothing, System.DBNull.Value, entbal.IDGED_CV)
        iii = iii + 1

        'Cod_Profissao
        '24
        Parametros(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(iii).Valor = System.DBNull.Value
        iii = iii + 1

        '25
        'Cod_Especialidade1
        Parametros(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade1", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(iii).Valor = 0
        iii = iii + 1

        '26
        'Cod_Profissao1
        Parametros(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao1", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(iii).Valor = 0

        'Nome_Agencia
        iii = iii + 1
        '27
        Parametros(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNome_Agencia", OracleDbType.Varchar2, ParameterDirection.Input)
        Parametros(iii).Valor = IIf(entbal.NOME_AGENCIA = Nothing, System.DBNull.Value, entbal.NOME_AGENCIA)

    End Sub

    Private Sub CriarParametrosPF(ByVal entBal1 As EntPERITO, ByVal Inserir As Integer)

        'Peritos na tabela de pessoas físicas
        'PessoaFisica
        ''''''''''''''''''''''''''''''''''''''''''''
        'ID_PF(sequence)
        'Nome
        'CPF
        'Cod_USER_INC = Sigla(Perito) ou EntBal.Sigla
        'DT_Inc -> Sysdate (Procedure PF)
        'Dt_Nasc (EntBal.Data_Nascimento)
        ''''''''''''''''''''''''''''''''''''''''''''

        Dim ii As Integer
        ii = 0
        If Inserir = 0 Then
            'ID_PF               NUMBER(5) not null,
            ParametrosPF(ii) = New ServicoDadosOracle.ParameterInfo("Nid_Pf", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosPF(ii).Tamanho = 14
            ParametrosPF(ii).Valor = entBal1.ID_PF 'Na inserção este valor será criado no Seq do Banco PF.
            ii = ii + 1
        Else
            'CRet
            'ParametrosPF(ii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosPF(ii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Output)
            ii = ii + 1
        End If

        'NOME                     VARCHAR2(250) not null,
        If Not entBal1.Nome Is Nothing Then
            ParametrosPF(ii) = New ServicoDadosOracle.ParameterInfo("Snome", OracleDbType.Varchar2, ParameterDirection.Input)
            ParametrosPF(ii).Tamanho = 100
            ParametrosPF(ii).Valor = UCase(IIf(entBal1.Nome = Nothing, System.DBNull.Value, entBal1.Nome))
            ii = ii + 1
        Else
            'MsgErro("Gravação Rejeitada, o nome não pode estar em branco")
            Exit Sub
            'ParametrosPF(ii).Valor = ""
            'ii = ii + 1
        End If
        'PessoaFisica
        ''''''''''''''''''''''''''''''''''''''''''''
        'CPF
        ParametrosPF(ii) = New ServicoDadosOracle.ParameterInfo("Scpf", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPF(ii).Tamanho = 11
        ParametrosPF(ii).Valor = IIf(entBal1.CPF = Nothing, System.DBNull.Value, entBal1.CPF)
        ii = ii + 1
        'Cod_USER_INC ou Cod_USER_ALT = Sigla(Perito) ou EntBal.Sigla
        ParametrosPF(ii) = New ServicoDadosOracle.ParameterInfo("SSigla", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPF(ii).Tamanho = 20
        ParametrosPF(ii).Valor = IIf(entBal1.SIGLA = Nothing, System.DBNull.Value, entBal1.SIGLA)
        ii = ii + 1
        'Dt_Nasc (EntBal.Data_Nascimento)
        ParametrosPF(ii) = New ServicoDadosOracle.ParameterInfo("Ddt_Nasc", OracleDbType.Date, ParameterDirection.Input)
        ParametrosPF(ii).Tamanho = 10
        ParametrosPF(ii).Valor = IIf(entBal1.Dt_Nasc = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), entBal1.Dt_Nasc)
        ii = ii + 1
        'Cod_Tip_Sit (EntBal.Cod_Tip_Sit)
        ParametrosPF(ii) = New ServicoDadosOracle.ParameterInfo("Ncod_Tip_Sit", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPF(ii).Tamanho = 3
        ParametrosPF(ii).Valor = IIf(entBal1.Cod_Tip_Sit = Nothing, System.DBNull.Value, entBal1.Cod_Tip_Sit)
        'Situacao => 1 - Ativo, 2 - Inativo e 19 - Excluído na tabela PessoaFisicaFuncao -> Cod_Tip_Sit (numérico)

    End Sub

    Private Sub CriarParametrosPFEmail(ByVal entBal1 As EntPERITO, ByVal m_ID_PF As Long)
        'Email (2 Emails)
        ''''''''''''''''''''''''''''''''''''''''
        'ID_PF 
        'Seq_Email -> Sequencial (Procedure PF)
        'E_Mail
        'Dt_Inc -> Sysdate (Procedure PF)
        'Cod_User_Inc -> Sysdate (Procedure PF)
        ''''''''''''''''''''''''''''''''''''''''
        'E_Mail - varchar(200)
        Dim ii As Integer
        ii = 0
        'If Inserir = 0 Then
        'ID_PF               NUMBER(5) not null,

        ParametrosPFEmail(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEmail(ii).Tamanho = 14
        ParametrosPFEmail(ii).Valor = m_ID_PF
        ii = ii + 1

        'Seq_Email
        ParametrosPFEmail(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Email", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEmail(ii).Tamanho = 5
        ParametrosPFEmail(ii).Valor = 1
        ii = ii + 1

        ParametrosPFEmail(ii) = New ServicoDadosOracle.ParameterInfo("sE_Mail", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEmail(ii).Tamanho = 200
        ParametrosPFEmail(ii).Valor = IIf(entBal1.EMAIL = Nothing, System.DBNull.Value, entBal1.EMAIL)
        ii = ii + 1
        'Cod_USER_INC ou Cod_USER_ALT = Sigla(Perito) ou EntBal.Sigla
        ParametrosPFEmail(ii) = New ServicoDadosOracle.ParameterInfo("sCod_USER", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEmail(ii).Tamanho = 20
        ParametrosPFEmail(ii).Valor = IIf(entBal1.SIGLA = Nothing, System.DBNull.Value, entBal1.SIGLA)

    End Sub
    Private Sub CriarParametrosPFEmail1(ByVal entBal1 As EntPERITO, ByVal m_ID_PF As Long)
        'PessoaFisicaEmail (2 Emails)
        ''''''''''''''''''''''''''''''''''''''''
        'ID_PF 
        'Seq_Email -> Sequencial (Procedure PF)
        'E_Mail
        'Dt_Inc -> Sysdate (Procedure PF)
        'Cod_User_Inc -> Sysdate (Procedure PF)
        ''''''''''''''''''''''''''''''''''''''''
        'E_Mail - varchar(200)
        Dim ii As Integer
        ii = 0
        'If Inserir = 0 Then
        'ID_PF               NUMBER(5) not null,
        ParametrosPFEmail1(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosPFEmail1(ii).Tamanho = 14
        ParametrosPFEmail1(ii).Valor = m_ID_PF
        ii = ii + 1

        'Seq_Email
        ParametrosPFEmail1(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Email", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosPFEmail1(ii).Tamanho = 5
        ParametrosPFEmail1(ii).Valor = 2
        ii = ii + 1

        ParametrosPFEmail1(ii) = New ServicoDadosOracle.ParameterInfo("sE_Mail", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEmail1(ii).Tamanho = 200
        ParametrosPFEmail1(ii).Valor = IIf(entBal1.EMAIL1 = Nothing, System.DBNull.Value, entBal1.EMAIL1)
        ii = ii + 1
        'Cod_USER_INC ou Cod_USER_ALT = Sigla(Perito) ou EntBal.Sigla
        ParametrosPFEmail1(ii) = New ServicoDadosOracle.ParameterInfo("sCod_USER", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosPFEmail1(ii).Tamanho = 20
        ParametrosPFEmail1(ii).Valor = IIf(entBal1.SIGLA = Nothing, System.DBNull.Value, entBal1.SIGLA)

    End Sub

    Private Sub CriarParametrosExcPFEmail(ByVal m_ID_PF As Long, ByVal pTipOperacao As String)
        Dim ii As Integer
        ii = 0

        'EXC - excluir
        'EXB - exibir
        If pTipOperacao = "EXB" Then
            ReDim ParametrosExcPFEmail(2)
            'Retorno
            ParametrosExcPFEmail(ii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ii = ii + 1
        End If

        'ID_PF
        ParametrosExcPFEmail(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExcPFEmail(ii).Tamanho = 14
        ParametrosExcPFEmail(ii).Valor = m_ID_PF
        ii = ii + 1
        'Seq_Email
        ParametrosExcPFEmail(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Email", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExcPFEmail(ii).Tamanho = 5
        ParametrosExcPFEmail(ii).Valor = 1

    End Sub

    Private Sub CriarParametrosExcPFEmail1(ByVal m_ID_PF As Long, ByVal pTipOperacao As String)

        Dim ii As Integer
        ii = 0

        'EXC - excluir
        'EXB - exibir
        If pTipOperacao = "EXB" Then
            ReDim ParametrosExcPFEmail1(2)
            'Retorno
            ParametrosExcPFEmail1(ii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ii = ii + 1
        End If

        'ID_PF
        ParametrosExcPFEmail1(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExcPFEmail1(ii).Tamanho = 14
        ParametrosExcPFEmail1(ii).Valor = m_ID_PF
        ii = ii + 1
        'Seq_Email
        ParametrosExcPFEmail1(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Email", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExcPFEmail1(ii).Tamanho = 5
        ParametrosExcPFEmail1(ii).Valor = 2
    End Sub

    Private Sub CriarParametrosExcPFEnd(ByVal m_ID_PF As Long, ByVal pTipOperacao As String, ByVal nCodTipEnd As Integer, ByVal nseqEnd As Integer)
        Dim ii As Integer
        ii = 0

        'EXC - excluir
        'EXB - exibir

        If pTipOperacao = "EXB" Then
            ReDim ParametrosExcPFEnd(2)
            'Retorno
            ParametrosExcPFEnd(ii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ii = ii + 1
        Else
            ReDim ParametrosExcPFEnd(1)
        End If

        'nID_PF
        ParametrosExcPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExcPFEnd(ii).Tamanho = 14
        ParametrosExcPFEnd(ii).Valor = m_ID_PF
        ii = ii + 1

        If pTipOperacao = "EXC" Then
            ParametrosExcPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_End", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosExcPFEnd(ii).Tamanho = 1
            ParametrosExcPFEnd(ii).Valor = nseqEnd
        End If

        If pTipOperacao = "EXB" Then
            ParametrosExcPFEnd(ii) = New ServicoDadosOracle.ParameterInfo("nCod_Tip_End", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosExcPFEnd(ii).Tamanho = 1
            ParametrosExcPFEnd(ii).Valor = nCodTipEnd
        End If

    End Sub

    Private Sub CriarParametrosExcPFTel(ByVal m_ID_PF As Long, ByVal pTipOperacao As String)
        'PessoaFisicaTelefone - 2 Numeros => 1 - Residencia (Seq_Tel)

        Dim ii As Integer
        ii = 0

        'EXC - excluir
        'EXB - exibir
        If pTipOperacao = "EXB" Then
            ReDim ParametrosExcPFTel(2)
            'Retorno
            ParametrosExcPFTel(ii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ii = ii + 1
        End If

        'ID_PF               NUMBER(5) not null,
        ParametrosExcPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExcPFTel(ii).Tamanho = 14
        ParametrosExcPFTel(ii).Valor = m_ID_PF 'Na inserção este valor será criado no Seq do Banco PF.
        ii = ii + 1

        'Seq_tel
        ParametrosExcPFTel(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExcPFTel(ii).Tamanho = 1
        ParametrosExcPFTel(ii).Valor = 1

    End Sub

    Private Sub CriarParametrosExcPFTel1(ByVal m_ID_PF As Long, ByVal pTipOperacao As String)
        'PessoaFisicaTelefone - 2 Numeros => 1 - Residencia (Seq_Tel)

        Dim ii As Integer
        ii = 0

        'EXC - excluir
        'EXB - exibir
        If pTipOperacao = "EXB" Then
            ReDim ParametrosExcPFTel1(2)
            'Retorno
            ParametrosExcPFTel1(ii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ii = ii + 1
        End If

        'ID_PF               NUMBER(5) not null,
        ParametrosExcPFTel1(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExcPFTel1(ii).Tamanho = 14
        ParametrosExcPFTel1(ii).Valor = m_ID_PF 'Na inserção este valor será criado no Seq do Banco PF.
        ii = ii + 1

        'Seq_tel
        ParametrosExcPFTel1(ii) = New ServicoDadosOracle.ParameterInfo("nSeq_Tel", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExcPFTel1(ii).Tamanho = 1
        ParametrosExcPFTel1(ii).Valor = 2

    End Sub

    Public Sub GravarFoto(ByVal m_ID_PF As String, ByVal sIDGED_Foto As String)

        Dim ParametrosFoto(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        'If sID_PF = 0 Then
        '    MsgErro("gravação rejeitada. O perito não foi localizado")
        'End If
        Dim ii As Integer
        ii = 0
        'ID_PF               NUMBER(14) not null,
        ParametrosFoto(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosFoto(ii).Tamanho = 14
        ParametrosFoto(ii).Valor = Convert.ToInt64(m_ID_PF)
        ii = ii + 1

        'IDGED_Foto
        ParametrosFoto(ii) = New ServicoDadosOracle.ParameterInfo("sIDGED_Foto", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosFoto(ii).Tamanho = 48
        ParametrosFoto(ii).Valor = sIDGED_Foto

        sd.Open()
        'Inserir Foto (Alteração na tabela perito)
        sd.ExecuteNonQuery("Gravar_FotoPerito", ParametrosFoto)

        'MsgErro("Gravação feita com sucesso")
        sd.Close()

    End Sub

    Public Sub GravarCurriculum(ByVal m_ID_PF As String, ByVal sIDGED_CV As String)

        Dim ParametrosCurriculum(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        'If sID_PF = 0 Then
        '    MsgErro("gravação rejeitada. O perito não foi localizado")
        'End If
        Dim ii As Integer
        ii = 0
        'ID_PF               NUMBER(14) not null,
        ParametrosCurriculum(ii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosCurriculum(ii).Tamanho = 14
        ParametrosCurriculum(ii).Valor = Convert.ToInt64(m_ID_PF)
        ii = ii + 1

        'IDGED_Foto
        ParametrosCurriculum(ii) = New ServicoDadosOracle.ParameterInfo("sIDGED_CV", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosCurriculum(ii).Tamanho = 48
        ParametrosCurriculum(ii).Valor = sIDGED_CV

        sd.Open()
        'Inserir Foto (Alteração na tabela perito)
        sd.ExecuteNonQuery("Gravar_Curriculum", ParametrosCurriculum)

        sd.Close()
    End Sub

    Function GravarFornecedor(ByVal Ent_Perito As EntPERITO) As String

        Dim ParametrosFornIncl(10) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim ParametrosFornAlter(11) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim j As Integer
        Dim pComplemento As String 'Endereço Residencial
        Dim Alteracao As Boolean
        Dim sSQL As String
        Dim msgPer As String

        'Function INCLUI_PERITO 'SIGAF
        'sNome        varchar2,
        'sComplemento varchar2, (endereço)
        'sBairro      varchar2,
        'sCidade      varchar2,
        'sCEP         varchar2,
        'sTel1        varchar2,
        'sTel2        varchar2,
        'sTEl3        varchar2,
        'sFAX         varchar2,
        'sCPF         varchar2,
        'somente na alteração => sAtivo       number,
        'sUF          varchar2

        sd.Open()
        sSQL = "Select Nome_fornec from fornecedores_peritos where COD_CGC_CPF = ? " ' & Ent_Perito.CPF & "'"
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, Ent_Perito.CPF)
        If dr.Read Then
            Alteracao = True
        Else
            Alteracao = False
        End If
        j = 0
        'sNOME                     VARCHAR2(250) not null,
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sNOME", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Nome = Nothing, System.DBNull.Value, UCase(Ent_Perito.Nome))
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'sComplemento              varchar2, (endereço)
        pComplemento = IIf(Ent_Perito.Descr_Tip_Logr = Nothing, " ", Ent_Perito.Descr_Tip_Logr) & _
                       " " + IIf(Ent_Perito.Nome_Logr = Nothing, " ", Ent_Perito.Nome_Logr) & _
                       " " + IIf(Ent_Perito.Compl_Logr = Nothing, " ", Ent_Perito.Compl_Logr)

        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sComplemento", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosFornIncl(j).Valor = IIf(pComplemento = Nothing, System.DBNull.Value, pComplemento)
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'sBairro      varchar2,
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sBairro", OracleDbType.Varchar2, ParameterDirection.Input)
        'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Descr_Bairro = Nothing, System.DBNull.Value, UCase(Ent_Perito.Descr_Bairro))
        ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Descr_Bairro = Nothing, "", UCase(Ent_Perito.Descr_Bairro))
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'sCidade      varchar2,
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sCidade", OracleDbType.Varchar2, ParameterDirection.Input)
        'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Descr_Cidade = Nothing, System.DBNull.Value, UCase(Ent_Perito.Descr_Cidade))
        ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Descr_Cidade = Nothing, "", UCase(Ent_Perito.Descr_Cidade))
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'sCEP         varchar2,tamanho (8)
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sCEP", OracleDbType.Varchar2, ParameterDirection.Input)
        'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.CEP = Nothing, System.DBNull.Value, UCase(Ent_Perito.CEP))
        ParametrosFornIncl(j).Valor = IIf(Ent_Perito.CEP = Nothing, "", UCase(Ent_Perito.CEP))
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1

        '------------------------------------------------------------
        'FOS deverá ser alterado para buscar o telefone da BalTelefone
        '------------------------------------------------------------
        'sTel1        varchar2,tamanho (50)
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sTel1", OracleDbType.Varchar2, ParameterDirection.Input)
        'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.TEL = Nothing, System.DBNull.Value, UCase(Ent_Perito.TEL))
        ParametrosFornIncl(j).Valor = ""
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'sTel2        varchar2,tamanho (50)
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sTel2", OracleDbType.Varchar2, ParameterDirection.Input)
        'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.TEL1 = Nothing, System.DBNull.Value, UCase(Ent_Perito.TEL1))
        ParametrosFornIncl(j).Valor = ""
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'sTEl3        varchar2,tamanho (50)
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sTEl3", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosFornIncl(j).Valor = ""
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'sFAX         varchar2,tamanho (40)
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sFAX", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosFornIncl(j).Valor = ""
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1

        '------------------------------------------------------------
        '------------------------------------------------------------

        'sCPF         varchar2, tamanho (11)
        ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sCPF", OracleDbType.Varchar2, ParameterDirection.Input)
        'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.CPF = Nothing, System.DBNull.Value, UCase(Ent_Perito.CPF))
        ParametrosFornIncl(j).Valor = IIf(Ent_Perito.CPF = Nothing, "", UCase(Ent_Perito.CPF))
        ParametrosFornAlter(j) = ParametrosFornIncl(j)
        ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        j = j + 1
        'somente na alteração => sAtivo       number, 1 - Ativo, 2 - Inativo
        If Alteracao Then
            ParametrosFornAlter(j) = New ServicoDadosOracle.ParameterInfo("sAtivo", OracleDbType.Int64, ParameterDirection.Input)
            ParametrosFornAlter(j).Valor = Convert.ToInt64(Ent_Perito.Cod_Tip_Sit)
            j = j + 1
            ParametrosFornAlter(j) = New ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
            ParametrosFornAlter(j).Valor = IIf(Ent_Perito.Sigla_UF = Nothing, "", UCase(Ent_Perito.Sigla_UF))
            j = j + 1
        Else
            'sUF          varchar2
            ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
            'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Sigla_UF = Nothing, System.DBNull.Value, UCase(Ent_Perito.Sigla_UF))
            ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Sigla_UF = Nothing, "", UCase(Ent_Perito.Sigla_UF))
            ParametrosFornAlter(j) = ParametrosFornIncl(j)
            ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
            j = j + 1
        End If
        'sUF          varchar2
        'ParametrosFornIncl(j) = New ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
        ''ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Sigla_UF = Nothing, System.DBNull.Value, UCase(Ent_Perito.Sigla_UF))
        'ParametrosFornIncl(j).Valor = IIf(Ent_Perito.Sigla_UF = Nothing, "", UCase(Ent_Perito.Sigla_UF))
        'ParametrosFornAlter(j) = ParametrosFornIncl(j)
        'ParametrosFornAlter(j).Valor = ParametrosFornIncl(j).Valor
        'j = j + 1
        'Sessão(retorno)-OUT
        'ParametrosFornIncl(j) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Msg", OracleDbType.Varchar2, ParameterDirection.Output)

        sd.BeginTransaction()
        If Not Alteracao Then
            ''sd.ExecuteNonQuery("SIGAF.PKG_PERITOS.Inclui_Perito@SIGAF", ParametrosFornIncl)
            ''msg = sd.ExecutaFunc("SIGAF.PKG_PERITOS.Inclui_Perito@SIGAF", 200, ParametrosFornIncl)
            ''DsSession = sd.CreateDataSet("Grava_TmpFornecedor", ParametrosFornIncl)
            ''m_Session = DsSession.Tables(0).Rows(0).Item(0)
            'msgPer = sd.ExecuteNonQuery("Grava_TmpFornecedor", ParametrosFornIncl)
            msgPer = sd.ExecutaFunc("Grava_TmpForn", 250, _
            ParametrosFornIncl(0).Valor, _
            ParametrosFornIncl(1).Valor, _
            ParametrosFornIncl(2).Valor, _
            ParametrosFornIncl(3).Valor, _
            ParametrosFornIncl(4).Valor, _
            ParametrosFornIncl(5).Valor, _
            ParametrosFornIncl(6).Valor, _
            ParametrosFornIncl(7).Valor, _
            ParametrosFornIncl(8).Valor, _
            ParametrosFornIncl(9).Valor, _
            ParametrosFornIncl(10).Valor)

        Else
            msgPer = sd.ExecutaFunc("pacote_perito.altera_perito", 300, ParametrosFornAlter(0).Valor, ParametrosFornAlter(1).Valor, ParametrosFornAlter(2).Valor, ParametrosFornAlter(3).Valor, ParametrosFornAlter(4).Valor, ParametrosFornAlter(5).Valor, ParametrosFornAlter(6).Valor, ParametrosFornAlter(7).Valor, ParametrosFornAlter(8).Valor, ParametrosFornAlter(9).Valor, ParametrosFornAlter(10).Valor, ParametrosFornAlter(11).Valor)
        End If
        sd.Close()
        Return msgPer

    End Function

    Public Function VerCPFPerito(ByVal pCPF As String) As Boolean

        Dim sSQL As String
        sd.Open()
        sSQL = "Select * from UC.PessoaFisica PF, uc.pessoafisicafuncao pff where pf.id_pf =pff.id_pf and pff.cod_tip_func=4 and PF.CPF = ?  "
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pCPF)
        If dr.Read Then
            Return True
        Else
            Return False
        End If
        sd.Close()

    End Function
    Public Function VerCPFPeritoExt(ByVal pCPF As String) As Boolean

        Dim sSQL As String

        sd.Open()
        sSQL = "Select * from UC.PessoaFisica PF where PF.CPF = ?  "
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pCPF)
        If dr.Read Then
            Return True
        Else
            Return False
        End If

        sd.Close()

    End Function
    Public Function VerHomonimo(ByVal pNome As String) As Boolean

        Dim sSQL As String

        sd.Open()
        'PFF.Cod_Tip_Func = 4 (Tipo -> Perito)
        sSQL = "Select count(PF.Nome) Num_Nomes from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF where PF.Nome = ?  and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF"
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pNome)
        If dr.Read Then
            If dr("Num_Nomes") > 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

        sd.Close()

    End Function
    Public Function Nome_ID(ByVal pID As String) As String

        Dim sSQL As String

        sd.Open()
        sSQL = "Select Nome from UC.PessoaFisica where ID_PF=?"
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, Convert.ToInt64(pID))
        If dr.Read Then
            Return dr("Nome")
        Else
            Return ""
        End If

        sd.Close()

    End Function

    Function ValidarPeritoCPF(ByVal pId_USU As Integer) As String
        Dim sSQL As String
        Dim sCPF As String = String.Empty

        sd.Open()
        sSQL = "Select pf.CPF from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF, Usuario U, Peritos P " & _
               "where U.ID_USU = ? and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF and U.CPF_USU = PF.CPF and " & _
               "P.ID_PF = PF.ID_PF and P.Data_Exclusao = to_date('01/01/1900','dd/mm/yyyy')"
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pId_USU.ToString)
        If dr.Read Then
            sCPF = dr(0)
        End If

        Return sCPF
        sd.Close()

    End Function

    Function ValidarPerito(ByVal pId_USU As Integer) As Boolean
        Dim sSQL As String

        sd.Open()
        sSQL = "Select pf.CPF from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF, Usuario U, Peritos P " & _
               "where U.ID_USU = ? and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF and U.CPF_USU = PF.CPF and " & _
               "P.ID_PF = PF.ID_PF and P.Data_Exclusao = to_date('01/01/1900','dd/mm/yyyy')"
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pId_USU.ToString)
        If dr.Read Then
            Return True
        Else
            Return False
        End If

        Return False
        sd.Close()

    End Function
    Function ID_Perito(ByVal pId_PF As Integer) As String

        Dim sSQL As String

        sd.Open()
        sSQL = "Select PF.ID_PF from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF, Usuario U " & _
               "where U.ID_USU = ? and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF and U.CPF_USU = PF.CPF"
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pId_PF.ToString)
        If dr.Read Then
            Return dr("ID_PF").ToString
        Else
            Return 0
        End If
        sd.Close()

    End Function

    Function LocalizaCPFPerito(ByVal pId_PF As Integer) As String

        Dim sSQL As String

        sd.Open()
        sSQL = "Select CPF from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF where PF.ID_PF = ?  and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF"
        Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pId_PF.ToString)
        If dr.Read Then
            Return dr("CPF")
        Else
            Return ""
        End If
        sd.Close()

    End Function

    Private Sub CriarParametrosExt(ByVal entbal As EntPERITO, ByVal m_ID_PF As Long)
        Dim iii As Integer

        'Novo Peritos
        'COD_PERITO               NUMBER(5) not null,
        'NUM_REG                  VARCHAR2(15),
        'Cod_Orgao_Per            NUMBER,
        'SIGLA                    VARCHAR2(20),
        'DATA_CADASTRAMENTO       DATE not null,
        'ID_PF
        'Cod_Orgao_Per            Number,

        '------------------------------------------------------------
        'FOS - Restruturar a gravação do Pré-Cadastro.
        '------------------------------------------------------------

        iii = 0
        'NUM_REG                  VARCHAR2(15),
        ParametrosExt(iii) = New ServicoDadosOracle.ParameterInfo("sNUM_REG", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExt(iii).Tamanho = 15
        ParametrosExt(iii).Valor = DBNull.Value
        iii = iii + 1
        'Cod_Orgao_Per        NUMBER,
        ParametrosExt(iii) = New ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExt(iii).Tamanho = 3
        ParametrosExt(iii).Valor = DBNull.Value
        iii = iii + 1
        'Cod_Especialidade
        ParametrosExt(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExt(iii).Valor = IIf(entbal.COD_ESPECIALIDADE = Nothing, System.DBNull.Value, entbal.COD_ESPECIALIDADE)
        iii = iii + 1
        'SIGLA                    VARCHAR2(20),
        ParametrosExt(iii) = New ServicoDadosOracle.ParameterInfo("sSIGLA", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametrosExt(iii).Tamanho = 20
        ParametrosExt(iii).Valor = "INTERNET"
        iii = iii + 1
        'DATA_CADASTRAMENTO       DATE
        ParametrosExt(iii) = New ServicoDadosOracle.ParameterInfo("dDATA_CADASTRAMENTO ", OracleDbType.Date, ParameterDirection.Input)
        ParametrosExt(iii).Tamanho = 10
        ParametrosExt(iii).Valor = Today
        iii = iii + 1
        'ID_PF               NUMBER(5) not null,
        ParametrosExt(iii) = New ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametrosExt(iii).Tamanho = 14
        ParametrosExt(iii).Valor = m_ID_PF
        iii = iii + 1
        'Cod_Profissao
        ParametrosExt(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExt(iii).Valor = DBNull.Value
        iii = iii + 1
        ''Cod_Especialidade1
        ParametrosExt(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade1", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExt(iii).Valor = DBNull.Value
        iii = iii + 1
        ''Cod_Profissao1
        ParametrosExt(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao1", OracleDbType.Int32, ParameterDirection.Input)
        ParametrosExt(iii).Valor = IIf(entbal.COD_PROFISSAO1 = Nothing, System.DBNull.Value, entbal.COD_PROFISSAO1)
        iii = iii + 1
        'DATA_EXCLUSÂO            DATE  
        ParametrosExt(iii) = New ServicoDadosOracle.ParameterInfo("dDATA_Exclusao", OracleDbType.Date, ParameterDirection.Input)
        ParametrosExt(iii).Tamanho = 10
        ParametrosExt(iii).Valor = IIf(entbal.Data_Exclusao = Nothing, CDate(System.Data.SqlTypes.SqlDateTime.Null), entbal.Data_Exclusao)

    End Sub

    Public Sub NovoStatus(ByVal pID_PF As String, ByVal pSigla As String)
        Dim ParametroStatus(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        sd.Open()
        ParametroStatus(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        ParametroStatus(0).Valor = CInt(pID_PF)
        ParametroStatus(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sSIGLA", OracleDbType.Varchar2, ParameterDirection.Input)
        ParametroStatus(1).Valor = pSigla

        sd.ExecuteNonQuery("uc.Pericias_PKG.Alterar_Status_PessoaFisica", ParametroStatus)
        sd.Close()

    End Sub

    Public Sub LimpaPreCadastro()
        'Data Atual - Data Cadastramento > 30
        'Pendente -> nenhum item do chklist foi preenchido.
        Dim sSQL As String

        sd.Open()
        sSQL = "UPDATE PERITOS SET DATA_EXCLUSAO = SYSDATE " & _
               "where DOCNECCV = 0 AND DOCNECCPF = 0 AND DOCNECFOTO = 0 AND " & _
               "DOCNECORG = 0 AND DOCNECHAB = 0 AND DOCNECRES = 0 AND " & _
               "DOCNECIMP = 0 AND sysdate-DATA_CADASTRAMENTO > 20 AND " & _
               "(DATA_EXCLUSAO IS NULL OR DATA_EXCLUSAO = TO_DATE('01/01/1900','DD/MM/YYYY'))"
        sd.ExecuteNonQuery(sSQL)
        sd.Close()

    End Sub

    Public Function VerificaPeritoFornecedor(ByVal sCPF As String) As Boolean

        Dim retorno As String
        sd.Open()
        retorno = sd.ExecutaFunc("Verifica_Perito_Fornecedor", 250, sCPF)
        If retorno = "null" Then
            Return False
        Else
            Return True
        End If
        sd.Close()

    End Function

    Public Function RetornaUltimoSeqEnd(ByVal sIDPF As String) As Integer

        Dim nSeq As Integer = 0
        sd.Open()
        nSeq = sd.ExecutaFunc("UltimoSeqEndereco", 250, sIDPF)
        sd.Close()
        Return nSeq

    End Function

    Public Function RetornaUltimoSeqTel(ByVal sIDPF As String) As Integer

        Dim nSeq As Integer = 0
        sd.Open()
        nSeq = sd.ExecutaFunc("ultimoseqtelefone", 50, sIDPF)
        sd.Close()
        Return nSeq
    End Function

    Public Function RetornarIDPF(ByVal sCPF As String) As String
        Dim sRetorno As String = String.Empty

        sd.Open()
        sRetorno = sd.ExecutaFunc("RetornaIDPF", 20, sCPF)

        If sRetorno = "0" Then
            Return String.Empty
        Else
            Return sRetorno
        End If
    End Function

End Class

