Imports ServicoDadosODPNET
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager
Imports Oracle.DataAccess.Client


Public Class BALPERITO

    Inherits BaseBAL
    Dim Parametros(30) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
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
    Public Function ExibirDadosEnt(ByVal pTipo As String, ByVal psFiltro As String, ByVal ExibirExcl As String, Optional ByVal pCod_Orgao_Per As Integer = 0, Optional ByVal TipoPessoa As Integer = 1) As EntPERITO
        Dim dsSet As DataSet
        Dim dsSetConta As DataSet

        Dim sNomePerito As String

        dsSet = ExibirDados(pTipo, psFiltro, ExibirExcl, pCod_Orgao_Per, TipoPessoa)

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
        If EntBal.ID_PF = 0 AndAlso EntBal.ID_PJ = 0 Then
            Return Nothing
        End If
        '=====================================================
        'Conta Corrente do DGPCF
        dsSetConta = ExibirDadosConta(IIf(EntBal.TipoPessoa = 1, EntBal.CPF, EntBal.CNPJ))
        If Not dsSetConta Is Nothing Then
            If dsSetConta.Tables(0).Rows.Count = 1 Then
                GerarEntidadeConta(dsSetConta.Tables(0).Rows(0))
            End If
        End If
        dsSetConta = Nothing
        '======================================================
        dsSet = Nothing

        'Tabela PessoaFisicaEndereco
        dsSet = ExibirDadosEndereco(IIf(EntBal.TipoPessoa = 1, EntBal.ID_PF, EntBal.ID_PJ), EntBal.TipoPessoa)
        If dsSet.Tables(0).Rows.Count > 0 Then
            GerarEntidadeEndereco(dsSet.Tables(0).Rows(0))
        End If

        If dsSet.Tables(0).Rows.Count > 1 Then
            GerarEntidadeEndereco(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing
        'Tabela PessoaFisicaEmail
        dsSet = ExibirDadosEmail(IIf(EntBal.TipoPessoa = 1, EntBal.ID_PF, EntBal.ID_PJ), EntBal.TipoPessoa)
        If dsSet.Tables(0).Rows.Count > 0 Then
            EntBal.EMAIL = GerarEntidadeEmail(dsSet.Tables(0).Rows(0))
        End If
        If dsSet.Tables(0).Rows.Count > 1 Then
            EntBal.EMAIL1 = GerarEntidadeEmail(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing
        Return EntBal
    End Function

    Public Function Carregar(ByVal Id As Long) As EntPERITO
        Dim ds As DataSet = Nothing
        Dim dsSet As DataSet
        Dim dsSetConta As DataSet

        Dim sNomePerito As String
        Try
            dsSet = sd.ExecutaProcDS("NOVO_PERICIAS.ObterPerito", sd.CriaRefCursor, Id) 'ParametrosExibir)

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
            If EntBal.ID_PF = 0 AndAlso EntBal.ID_PJ = 0 Then
                Return Nothing
            End If
            '=====================================================
            'Conta Corrente do DGPCF
            dsSetConta = ExibirDadosConta(IIf(EntBal.TipoPessoa = 1, EntBal.CPF, EntBal.CNPJ))
            If Not dsSetConta Is Nothing Then
                If dsSetConta.Tables(0).Rows.Count = 1 Then
                    GerarEntidadeConta(dsSetConta.Tables(0).Rows(0))
                End If
            End If
            dsSetConta = Nothing
            '======================================================
            '  dsSet = Nothing

            'Tabela(PessoaFisicaEndereco)
            dsSet = ExibirDadosEndereco(IIf(EntBal.TipoPessoa = 1, EntBal.ID_PF, EntBal.ID_PJ), EntBal.TipoPessoa)
            If dsSet.Tables(0).Rows.Count > 0 Then
                GerarEntidadeEndereco(dsSet.Tables(0).Rows(0))
            End If

            If dsSet.Tables(0).Rows.Count > 1 Then
                GerarEntidadeEndereco(dsSet.Tables(0).Rows(1))
            End If
            dsSet = Nothing

            'Tabela(PessoaFisicaEmail)
            dsSet = ExibirDadosEmail(IIf(EntBal.TipoPessoa = 1, EntBal.ID_PF, EntBal.ID_PJ), EntBal.TipoPessoa)
            If dsSet.Tables(0).Rows.Count > 0 Then
                EntBal.EMAIL = GerarEntidadeEmail(dsSet.Tables(0).Rows(0))
            End If
            If dsSet.Tables(0).Rows.Count > 1 Then
                EntBal.EMAIL1 = GerarEntidadeEmail(dsSet.Tables(0).Rows(1))
            End If
            dsSet = Nothing
            Return EntBal
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function


    Public Function ExibirDadosCadPeritoEnt(ByVal pTipo As String, ByVal psFiltro As String, ByVal ExibirExcl As String, ByVal TipoPessoa As Integer) As EntPERITO
        Try
            Dim dsSet As DataSet
            Dim dsSetConta As DataSet

            Dim sNomePerito As String

            dsSet = ExibirDadosCadPerito(pTipo, psFiltro, ExibirExcl, TipoPessoa)

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
            If EntBal.ID_PF = 0 AndAlso EntBal.ID_PJ = 0 Then
                Return Nothing
            End If
            '=====================================================
            'Conta Corrente do DGPCF
            dsSetConta = ExibirDadosConta(IIf(EntBal.TipoPessoa = 1, EntBal.CPF, EntBal.CNPJ))
            If Not dsSetConta Is Nothing Then
                If dsSetConta.Tables(0).Rows.Count = 1 Then
                    GerarEntidadeConta(dsSetConta.Tables(0).Rows(0))
                End If
            End If
            dsSetConta = Nothing
            '======================================================
            '  dsSet = Nothing

            'Tabela(PessoaFisicaEndereco)
            dsSet = ExibirDadosEndereco(IIf(EntBal.TipoPessoa = 1, EntBal.ID_PF, EntBal.ID_PJ), EntBal.TipoPessoa)
            If dsSet.Tables(0).Rows.Count > 0 Then
                GerarEntidadeEndereco(dsSet.Tables(0).Rows(0))
            End If

            If dsSet.Tables(0).Rows.Count > 1 Then
                GerarEntidadeEndereco(dsSet.Tables(0).Rows(1))
            End If
            dsSet = Nothing

            'Tabela(PessoaFisicaEmail)
            dsSet = ExibirDadosEmail(IIf(EntBal.TipoPessoa = 1, EntBal.ID_PF, EntBal.ID_PJ), EntBal.TipoPessoa)
            If dsSet.Tables(0).Rows.Count > 0 Then
                EntBal.EMAIL = GerarEntidadeEmail(dsSet.Tables(0).Rows(0))
            End If
            If dsSet.Tables(0).Rows.Count > 1 Then
                EntBal.EMAIL1 = GerarEntidadeEmail(dsSet.Tables(0).Rows(1))
            End If
            dsSet = Nothing
            Return EntBal
        Catch ex As Exception
            Throw ex
        End Try

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

        dsSet = ExibirDadosEndereco(IIf(EntBal.TipoPessoa = 1, EntBal.CPF, EntBal.CNPJ), EntBal.TipoPessoa)
        If dsSet.Tables(0).Rows.Count > 0 Then
            GerarEntidadeEndereco(dsSet.Tables(0).Rows(0))
        End If

        If dsSet.Tables(0).Rows.Count > 1 Then
            GerarEntidadeEndereco(dsSet.Tables(0).Rows(1))
        End If

        dsSet = Nothing

        dsSet = ExibirDadosEmail(IIf(EntBal.TipoPessoa = 1, EntBal.ID_PF, EntBal.ID_PJ), EntBal.TipoPessoa)
        If dsSet.Tables(0).Rows.Count > 0 Then
            EntBal.EMAIL = GerarEntidadeEmail(dsSet.Tables(0).Rows(0))
        End If
        If dsSet.Tables(0).Rows.Count > 1 Then
            EntBal.EMAIL1 = GerarEntidadeEmail(dsSet.Tables(0).Rows(1))
        End If
        dsSet = Nothing
        Return EntBal
    End Function

    Public Function ExibirDadosSet(ByVal pTipo As String, ByVal psFiltro As String, ByVal ExibirExcl As String, Optional ByVal pCod_Orgao_Per As Integer = 0) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(pTipo, psFiltro, ExibirExcl, pCod_Orgao_Per)
        Return dsSet
    End Function

    Public Function ExibirTodosDadosSet(ByVal pTipo As String, ByVal psFiltro As String, Optional ByVal pCod_Orgao_Per As Integer = 0, Optional ByVal tipoPessoa As Integer = 1) As DataSet
        Try
            Dim dsSet As DataSet
            dsSet = ExibirTodosDados(pTipo, psFiltro, "S", pCod_Orgao_Per, tipoPessoa)
            Return dsSet
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ExibirDadosSet(ByVal pID_PF As Long) As DataSet
        Dim dsSet As DataSet
        dsSet = ExibirDados(pID_PF)
        Return dsSet
    End Function

    Public Function ExibirDados(ByVal Tipo As String, ByVal sFiltro As String, ByVal ExibirExcluido As String, Optional ByVal pCod_Orgao_Per As Integer = 0, Optional ByVal TipoPessoa As Integer = 1) As DataSet
        Dim ds As DataSet
        Dim ordenacao As String = IIf(TipoPessoa = 1, "PF.NOME", "PJ.NOME_PJ")

        sFiltro = Replace(sFiltro, "'", "")
        ds = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Perito", sd.CriaRefCursor, ordenacao, Tipo, UCase(sFiltro), CInt(pCod_Orgao_Per), ExibirExcluido, TipoPessoa) 'ParametrosExibir)
        If ds.Tables(0).Rows.Count = 0 And Tipo = "CPF" Then
            ds = sd.ExecutaProcDS("EXIBIRDADOS_PERITO_CPF", sd.CriaRefCursor, sFiltro)
        Else
            Return ds
        End If

        Return ds
    End Function

    Public Function ObterNomesSemelhantesPerito(ByVal fragmentoNomePerito) As DataSet
        Dim ds As DataSet
        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ObterNomesSemelhantesPerito", sd.CriaRefCursor, fragmentoNomePerito)

            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExibirDadosCadPerito(ByVal Tipo As String, ByVal sFiltro As String, ByVal ExibirExcluido As String, ByVal TipoPessoa As Integer) As DataSet
        Dim ds As DataSet
        Dim ordenacao As String

        Try
            ordenacao = IIf(TipoPessoa = 1, "PF.NOME", "PJ.NOME_PJ")

            If Tipo = "NOME_FANT" Or Tipo = "NOMEFANT" Then
                Tipo = "NOMEFANT"
                ordenacao = "PJ.NOME_FANT"
            End If

            sFiltro = Replace(sFiltro, "'", "")
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_CADPerito", sd.CriaRefCursor, ordenacao, Tipo, UCase(sFiltro), ExibirExcluido, TipoPessoa) 'ParametrosExibir)

            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ExibirTodosDados(ByVal Tipo As String, ByVal sFiltro As String, ByVal ExibirExcl As String, Optional ByVal pCod_Orgao_Per As Integer = 0, Optional ByVal tipoPessoa As Integer = 1) As DataSet
        Try
            Dim ds As DataSet
            Dim ordenacao As String = IIf(tipoPessoa = 1, "PF.NOME", "PJ.NOME_PJ")

            If Tipo = "NOME_FANT" Or Tipo = "NOMEFANT" Then
                Tipo = "NOMEFANT"
                ordenacao = "PJ.NOME_FANT"
            End If

            sFiltro = Replace(sFiltro, "'", "")
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.EXIBIRDADOS_PERITO", sd.CriaRefCursor, ordenacao, Tipo, UCase(sFiltro), CInt(pCod_Orgao_Per), "S", tipoPessoa) 'ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try


    End Function

    Public Function ExibirDadosConta(ByVal pIdentificador As String) As DataSet
        Dim ds As DataSet = Nothing

        If pIdentificador <> "" Then
            Try
                'pCPF - 11 dígitos, somente números
                ds = sd.ExecutaProcDS("ExibirDados_Conta_Perito", sd.CriaRefCursor, pIdentificador)
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If

        Return ds
    End Function

    Public Function ExibirDados(ByVal nID_PF As Long) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("ExibirDados_Perito_ID", sd.CriaRefCursor, nID_PF)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosPerDCP(ByVal pCod_Profissao As Integer, ByVal pCod_Especialidade As Integer, Optional ByVal pCod_Comarca As Integer = 0) As DataSet
        Dim ds As DataSet = Nothing
        'Ativo e se atua nesta comarca e docs OK e nao excluidos, ordendos por qte de processos

        If pCod_Profissao <> 0 Then
            Try
                ds = sd.ExecutaProcDS("ExibirDados_PeritoDCP", sd.CriaRefCursor, pCod_Especialidade, pCod_Profissao, pCod_Comarca)
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If

        Return ds
    End Function

    Public Function ExibirDadosEndereco(ByVal Id As Int64, ByVal tipoPessoa As Integer) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Perito_Endereco", sd.CriaRefCursor, Id, tipoPessoa)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosEmail(ByVal Id As Int64, ByVal tipoPessoa As Integer) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Perito_Email", sd.CriaRefCursor, Id, tipoPessoa)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ListarPeritosNomeacao(Optional ByVal codPerito As Long = 0, _
                                          Optional ByVal cpf_cnpj As String = "", _
                                          Optional ByVal codEspecialidade As Integer = 0, _
                                          Optional ByVal codComarca As Integer = 0, _
                                          Optional ByVal tipoPessoa As Integer = 1) As DataSet
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.Listar_Peritos_Nomeacao", sd.CriaRefCursor, tipoPessoa, cpf_cnpj, codPerito, codEspecialidade, codComarca)

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item(0) Is Nothing Then
                    ds.Tables(0).Rows.Clear()
                End If
            End If

            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Sub Reativar(ByVal perito As EntPERITO, ByVal pSigla As String)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.Reativar_Perito", perito.Cod_Perito, pSigla)

            If perito.TipoPessoa = 1 Then
                sd.ExecutaProc("uc.Pericias_PKG.Reativar_PessoaFisica", perito.ID_PF, pSigla)
            Else
                sd.ExecutaProc("uc.Pericias_PKG.Reativar_PessoaJuridica", perito.ID_PJ, pSigla)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub Excluir(ByVal perito As EntPERITO, ByVal pSigla As String)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.Excluir_Perito", perito.Cod_Perito, pSigla)

            If perito.TipoPessoa = 1 Then
                sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaFisica", perito.ID_PF, pSigla)
            Else
                sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaJuridica", perito.ID_PJ, pSigla)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Validar(ByVal pID_PF As Integer) As Boolean
        Dim ds As DataSet

        If pID_PF <> 0 Then
            Try
                ds = sd.ExecutaProcDS("Validar_Perito", sd.CriaRefCursor, pID_PF)

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
        Else
            Return False
        End If
    End Function

    Public Function RecuperaUltimaDataMigracao(ByVal pcod_perito As Int64) As Date
        Dim ds As DataSet = Nothing
        Dim dataMigracao As Date = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.RecuperaUltimaDataMigracao", pcod_perito, sd.CriaRefCursor())

            If ds.Tables(0).Rows.Count > 0 Then
                If Not ds.Tables(0).Rows(0).Item("data_inclusao").Equals(DBNull.Value) Then
                    dataMigracao = CDate(ds.Tables(0).Rows(0).Item("data_inclusao"))
                End If
            End If

            Return dataMigracao
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Private Sub GerarEntidade(ByVal Rss As DataRow) 'As EntPERITO
        Try
            EntBal.ID_PF = NVL(Rss("ID_PF"), 0) 'Tabela Pessoa Fisica
            EntBal.Cod_Perito = NVL(Rss("COD_PERITO"), 0) 'Tabela Perito

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
            EntBal.Data_Exclusao = NVL(Rss("Data_Exclusao"), Nothing)
            EntBal.CPF = NVL(Rss("CPF"), "") 'Tabela Pessoa Fisica
            EntBal.SITUACAO_CADASTRO = NVL(Rss("SITUACAO_CADASTRO"), "") 'Tabela Perito
            EntBal.COD_BANCO = NVL(Rss("COD_BANCO"), 0) 'Tabela Perito
            EntBal.NUM_AGENCIA = "" 'Tabela Perito
            EntBal.NOME_AGENCIA = "" 'Tabela Perito
            EntBal.NUM_CONTA_CORRENTE = "" 'Tabela Perito
            EntBal.Dt_Nasc = NVL(Rss("Dt_Nasc"), Nothing) 'Tabela PessoaFisica]
            If EntBal.Dt_Nasc = #1/1/1900# Or EntBal.Dt_Nasc = #12:00:00 AM# Then
                EntBal.Dt_Nasc = Nothing
            End If
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
            EntBal.ID_PJ = NVL(Rss("ID_PJ"), 0)
            EntBal.NomeFantasia = NVL(Rss("NOME_FANT"), String.Empty)
            EntBal.CNPJ = NVL(Rss("CNPJ"), String.Empty)
            EntBal.TipoPessoa = NVL(Rss("TIPOPESSOA"), 0)
            EntBal.StatusAtual = New TIPO_STATUS()
            EntBal.StatusAtual.Codigo = NVL(Rss("COD_TIPO_STATUS"), 0)
            EntBal.StatusAtual.Descricao = NVL(Rss("DESC_STATUS"), String.Empty)
            EntBal.HistoricoStatusAtual = Int64.Parse(NVL(Rss("HistoricoStatusAtual"), 0))
            EntBal.DataMigracao = RecuperaUltimaDataMigracao(EntBal.Cod_Perito)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub GerarEntidadeConta(ByVal Rss As DataRow) 'As EntPERITO
        Try
            If EntBal.TipoPessoa = EntPERITO.Pessoa.Fisica Then
                EntBal.CPF = NVL(Rss("COD_CGC_CPF"), 0)
            Else
                EntBal.CNPJ = NVL(Rss("COD_CGC_CPF"), 0)
            End If
            'Tabela Fornecedor(Perito) SIGAF
            EntBal.COD_BANCO = NVL(Rss("COD_BANCO"), 0) 'Tabela Fornecedor(Perito) SIGAF
            EntBal.NUM_AGENCIA = NVL(Rss("NUM_AGENCIA"), "") 'Tabela Fornecedor(Perito) SIGAF
            EntBal.NOME_AGENCIA = NVL(Rss("NOME_AGENCIA"), "") 'Tabela Fornecedor(Perito) SIGAF
            EntBal.NUM_CONTA_CORRENTE = NVL(Rss("NUM_CONTA_CORRENTE"), "") 'Tabela Fornecedor(Perito) SIGAF
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GerarEntidadeEndereco(ByVal Rss As DataRow) 'As EntPERITO
        Try
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
                EntBal.Cod_Bairro1 = NVL(Rss("Cod_Bai"), 0) 'Tabela PessoaFisicaEndereco
                EntBal.Cod_Cidade1 = NVL(Rss("Cod_Cid"), 0) 'Tabela PessoaFisicaEndereco
                EntBal.CEP1 = NVL(Rss("CEP"), "") 'Tabela PessoaFisicaEndereco
                EntBal.Descr_Bairro1 = NVL(Rss("Descr_Bairro"), 0) 'Tabela Bairro
                EntBal.Descr_Cidade1 = NVL(Rss("Descr_Cidade"), "")   'Tabela Cidade
                EntBal.Descr_Tip_Logr1 = NVL(Rss("Descr_Tip_Logr"), "") 'Tabela UC.Logradouro
                EntBal.Sigla_UF1 = NVL(Rss("UF"), "") 'Tabela PessoaFisicaEndereco
                EntBal.Seq_End1 = NVL(Rss("Seq_End"), 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GerarEntidadeEmail(ByVal Rss As DataRow) As String
        Dim Email As String
        Email = NVL(Rss("E_MAIL"), "")
        Return Email
    End Function

    Public Sub AlterarPJ(ByRef perito As EntPERITO, ByVal pInserirConta As Boolean, tipoPessoa As EntPERITO.Pessoa)
        Dim Msg As String = String.Empty

        Try
            'Altera os dados do Perito na tabela PessoaJuridica
            'sd.ExecuteNonQuery("uc.Pericias_PKG.ALTERAR_PESSOAJURIDICA", perito.ID_PJ, perito.Nome, perito.NomeFantasia, perito.CNPJ, perito.SIGLA)
            sd.ExecutaProc("uc.Pericias_Pkg.Inserir_PessoaJuridica", perito.ID_PJ, perito.Nome, perito.NomeFantasia, perito.CNPJ, perito.SIGLA)

            sd.ExecutaProc("NOVO_PERICIAS.Alterar_Perito", perito.Cod_Perito, DBNull.Value, _
                               perito.OBS, perito.FALTA_ENTREGAR, DBNull.Value, DBNull.Value, _
                               perito.SIGLA, If(perito.Data_Cadastramento = Nothing, Today, perito.Data_Cadastramento), DBNull.Value, _
                               DBNull.Value, perito.SITUACAO_CADASTRO, _
                               perito.COD_BANCO, perito.NUM_AGENCIA, perito.NUM_CONTA_CORRENTE, DBNull.Value, perito.DocNecCV, perito.DocNecCPF, perito.DocNecFoto, _
                               perito.DocNecOrg, perito.DocNecHab, perito.DocNecRes, perito.DocNecImp, perito.IDGED_Foto, perito.IDGED_CV, DBNull.Value, _
                               DBNull.Value, DBNull.Value, perito.NOME_AGENCIA, perito.ID_PJ, _
                               perito.StatusAtual.Codigo, IIf(pInserirConta, 1, 0))

            If pInserirConta Then
                Msg = GravarContaCorrente(perito.CNPJ, perito.COD_BANCO, perito.NUM_AGENCIA, perito.NOME_AGENCIA, perito.NUM_CONTA_CORRENTE)
            End If

            ManterEnderecoPeritoPJ(perito)

            ManterEmailPeritoPJ(perito)

            If Not String.IsNullOrEmpty(Msg) Then
                Throw New Exception("Ocorreu um erro ao tentar inserir os dados bancários do Perito")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub ManterEnderecoPeritoPJ(ByVal perito As EntPERITO)
        Dim dsEnderecosPerito As DataSet = Nothing
        Dim temDados As Boolean = False

        Try
            'Se endereço não preenchido exclui endereço residencial
            If perito.Nome_Logr = "" Then
                sd.ExecutaProc("uc.Pericias_PKG.EXCLUIR_PESSOAJURIDICAEND", perito.ID_PJ, perito.Seq_End)
            Else
                dsEnderecosPerito = sd.ExecutaProcDS("NOVO_PERICIAS.EXIBIRDADOS_ENDERECO_UNICO", sd.CriaRefCursor, perito.ID_PJ, perito.Cod_Tip_End, perito.TipoPessoa)

                If dsEnderecosPerito.Tables(0).Rows.Count > 0 Then
                    If Not dsEnderecosPerito.Tables(0).Rows(0).Item(0).Equals(DBNull.Value) Then
                        temDados = True
                    Else
                        temDados = False
                    End If
                Else
                    temDados = False
                End If

                If Not temDados Then
                    perito.Seq_End = (RetornaUltimoSeqEnd(perito.Cod_Perito.ToString, perito.TipoPessoa) + 1)

                    sd.ExecutaProc("uc.Pericias_PKG.INSERIR_PESSOAJURIDICAEND", perito.ID_PJ, perito.Seq_End, _
                                       perito.CEP, perito.Cod_Tip_End, perito.Cod_Tip_Logr, perito.Nome_Logr, _
                                       perito.Num_Logr, perito.Compl_Logr, perito.Cod_Bairro, perito.Cod_Cidade, _
                                       perito.SIGLA, perito.Sigla_UF)
                Else
                    sd.ExecutaProc("uc.Pericias_PKG.ALTERAR_PESSOAJURIDICAEND", perito.ID_PJ, perito.Seq_End, _
                                       perito.CEP, perito.Cod_Tip_End, perito.Cod_Tip_Logr, perito.Nome_Logr, _
                                       perito.Num_Logr, perito.Compl_Logr, perito.Cod_Bairro, perito.Cod_Cidade, _
                                       perito.Sigla_UF)
                End If

            End If

            If Not dsEnderecosPerito Is Nothing Then
                dsEnderecosPerito.Clear()

                dsEnderecosPerito = Nothing
            End If

            'Se endereço não preenchido exclui endereço comercial
            If perito.Nome_Logr1 = "" Then
                sd.ExecutaProc("uc.Pericias_PKG.EXCLUIR_PESSOAJURIDICAEND", perito.ID_PJ, perito.Seq_End1)
            Else
                dsEnderecosPerito = sd.ExecutaProcDS("NOVO_PERICIAS.EXIBIRDADOS_ENDERECO_UNICO", sd.CriaRefCursor, perito.ID_PJ, perito.Cod_Tip_End1, perito.TipoPessoa)

                If dsEnderecosPerito.Tables(0).Rows.Count > 0 Then
                    If Not dsEnderecosPerito.Tables(0).Rows(0).Item(0).Equals(DBNull.Value) Then
                        temDados = True
                    Else
                        temDados = False
                    End If
                Else
                    temDados = False
                End If

                If Not temDados Then
                    perito.Seq_End1 = (RetornaUltimoSeqEnd(perito.Cod_Perito.ToString, perito.TipoPessoa) + 1)

                    sd.ExecutaProc("uc.Pericias_PKG.INSERIR_PESSOAJURIDICAEND", perito.ID_PJ, perito.Seq_End1, _
                                       perito.CEP1, perito.Cod_Tip_End1, perito.Cod_Tip_Logr1, perito.Nome_Logr1, _
                                       perito.Num_Logr1, perito.Compl_Logr1, perito.Cod_Bairro1, perito.Cod_Cidade1, _
                                       perito.SIGLA, perito.Sigla_UF1)
                Else
                    sd.ExecutaProc("uc.Pericias_PKG.ALTERAR_PESSOAJURIDICAEND", perito.ID_PJ, perito.Seq_End1, _
                                       perito.CEP1, perito.Cod_Tip_End1, perito.Cod_Tip_Logr1, perito.Nome_Logr1, _
                                       perito.Num_Logr1, perito.Compl_Logr1, perito.Cod_Bairro1, perito.Cod_Cidade1, _
                                       perito.Sigla_UF1)
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub ManterEmailPeritoPJ(ByVal perito As EntPERITO)
        Dim dsEmailsPerito As DataSet = Nothing
        Dim temDados As Boolean = False

        Try

            If String.IsNullOrEmpty(perito.EMAIL) Then
                sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaJuridicaEmail", perito.ID_PJ, 1)
            Else
                dsEmailsPerito = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Email_Unico", sd.CriaRefCursor, perito.ID_PJ, 1, perito.TipoPessoa)
                If dsEmailsPerito.Tables(0).Rows.Count > 0 Then
                    If Not dsEmailsPerito.Tables(0).Rows(0).Item(0).Equals(DBNull.Value) Then
                        temDados = True
                    Else
                        temDados = False
                    End If
                Else
                    temDados = False
                End If

                If Not temDados Then
                    sd.ExecutaProc("uc.Pericias_PKG.Inserir_PessoaJuridicaEmail", perito.ID_PJ, 1, perito.EMAIL, perito.SIGLA)
                Else
                    sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaJuridicaEmail", perito.ID_PJ, 1, perito.EMAIL, perito.SIGLA)
                End If
            End If

            If Not dsEmailsPerito Is Nothing Then
                dsEmailsPerito.Clear()

                dsEmailsPerito = Nothing
            End If

            If String.IsNullOrEmpty(perito.EMAIL1) Then
                sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaJuridicaEmail", perito.ID_PJ, 2)
            Else
                dsEmailsPerito = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Email_Unico", sd.CriaRefCursor, perito.ID_PJ, 2, perito.TipoPessoa)

                If dsEmailsPerito.Tables(0).Rows.Count > 0 Then
                    If Not dsEmailsPerito.Tables(0).Rows(0).Item(0).Equals(DBNull.Value) Then
                        temDados = True
                    Else
                        temDados = False
                    End If
                Else
                    temDados = False
                End If


                If Not temDados Then
                    sd.ExecutaProc("uc.Pericias_PKG.Inserir_PessoaJuridicaEmail", perito.ID_PJ, 2, perito.EMAIL1, perito.SIGLA)
                Else
                    sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaJuridicaEmail", perito.ID_PJ, 2, perito.EMAIL1, perito.SIGLA)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Overridable Sub Gravar(ByRef perito As EntPERITO, ByVal pInserirConta As Boolean, tipoPessoa As EntPERITO.Pessoa)
        Dim dsPeritoUc As DataSet = Nothing
        Dim dsPeritoPer As DataSet = Nothing
        Dim inserirNaUC As Boolean = True
        Dim inserirNoPericias As Boolean = True

        Try
            If tipoPessoa = EntPERITO.Pessoa.Fisica Then
                Gravar(perito, pInserirConta)
            Else
                dsPeritoUc = BuscaPeritoPJ(perito.CNPJ)

                If dsPeritoUc Is Nothing OrElse dsPeritoUc.Tables(0).Rows.Count = 0 Then
                    perito.ID_PJ = GravaPessoaJuridicaNaUC(perito, pInserirConta)
                Else
                    perito.ID_PJ = Int64.Parse(dsPeritoUc.Tables(0).Rows(0).Item("id_pj"))
                    inserirNaUC = False
                End If

                dsPeritoPer = BuscaPeritoPJPericias(perito.ID_PJ)

                If dsPeritoPer Is Nothing OrElse dsPeritoPer.Tables(0).Rows.Count = 0 Then
                    perito.Cod_Perito = GravaPessoaJuridicaNoPericias(perito, pInserirConta)
                Else
                    perito.Cod_Perito = Int64.Parse(dsPeritoPer.Tables(0).Rows(0).Item("cod_perito"))
                    inserirNoPericias = False
                End If

                If Not inserirNaUC And Not inserirNoPericias Then
                    AlterarPJ(perito, pInserirConta, tipoPessoa)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GravaPessoaJuridicaNoPericias(ByVal perito As EntPERITO, ByVal pInserirConta As Boolean) As Int64
        Dim cod_perito As Int64 = 0
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.Gravar_Perito_PJ", DBNull.Value, _
                               perito.OBS, perito.FALTA_ENTREGAR, DBNull.Value, DBNull.Value, _
                               perito.SIGLA, If(perito.Data_Cadastramento = Nothing, Today, perito.Data_Cadastramento), DBNull.Value, _
                               DBNull.Value, perito.SITUACAO_CADASTRO, _
                               perito.COD_BANCO, perito.NUM_AGENCIA, perito.NUM_CONTA_CORRENTE, DBNull.Value, perito.DocNecCV, perito.DocNecCPF, perito.DocNecFoto, _
                               perito.DocNecOrg, perito.DocNecHab, perito.DocNecRes, perito.DocNecImp, perito.IDGED_Foto, perito.IDGED_CV, DBNull.Value, _
                               DBNull.Value, DBNull.Value, perito.NOME_AGENCIA, perito.ID_PJ, perito.NomeFantasia, perito.Nome, _
                               perito.CNPJ, perito.StatusAtual.Codigo, IIf(pInserirConta, 1, 0), 0, sd.CriaRefCursor)

            cod_perito = Int64.Parse(ds.Tables(0).Rows(0).Item("codigoPerito"))

            Return cod_perito
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function GravaPessoaJuridicaNaUC(ByVal perito As EntPERITO, ByVal pInserirConta As Boolean) As Int64
        Dim id_pj As Int64 = 0
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.Inserir_PJ", UCase(perito.Nome), UCase(perito.NomeFantasia), perito.CNPJ, UCase(perito.SIGLA), sd.CriaRefCursor)
            id_pj = Int64.Parse(ds.Tables(0).Rows(0).Item("id_pj"))

            Return id_pj
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Sub GravarPessoaJuridicaPerito(ByRef perito As EntPERITO, ByVal pInserirConta As Boolean)
        Dim dsRetornoPerito As DataSet = Nothing

        Try

            dsRetornoPerito = sd.ExecutaProcDS("NOVO_PERICIAS.Gravar_Perito_PJ", DBNull.Value, _
                               perito.OBS, perito.FALTA_ENTREGAR, DBNull.Value, DBNull.Value, _
                               perito.SIGLA, If(perito.Data_Cadastramento = Nothing, Today, perito.Data_Cadastramento), DBNull.Value, _
                               DBNull.Value, perito.SITUACAO_CADASTRO, _
                               perito.COD_BANCO, perito.NUM_AGENCIA, perito.NUM_CONTA_CORRENTE, DBNull.Value, perito.DocNecCV, perito.DocNecCPF, perito.DocNecFoto, _
                               perito.DocNecOrg, perito.DocNecRes, perito.DocNecImp, perito.IDGED_Foto, perito.IDGED_CV, DBNull.Value, _
                               DBNull.Value, DBNull.Value, perito.NOME_AGENCIA, perito.ID_PJ, perito.NomeFantasia, perito.Nome, _
                               perito.CNPJ, perito.StatusAtual.Codigo, IIf(pInserirConta, 1, 0), sd.CriaRefCursor)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub Gravar(ByRef Ent_Perito As EntPERITO, ByVal pInserirConta As Boolean)
        Dim mm_ID_PF As Long
        Dim DsAchar As DataSet
        Dim retorno As New Oracle.DataAccess.Client.OracleParameter

        With retorno
            .DbType = DbType.Int64
            .Direction = ParameterDirection.Output
            .OracleDbType = OracleDbType.Int64
            .ParameterName = "Nid_Pf"
        End With

        Try
            If Not VerCPFPerito(Ent_Perito.CPF) Then
                mm_ID_PF = CLng(Ent_Perito.ID_PF)

                sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoafisica", retorno, Ent_Perito.Nome, _
                               Ent_Perito.CPF, Ent_Perito.SIGLA, Ent_Perito.Dt_Nasc, Ent_Perito.Cod_Tip_Sit)

                Ent_Perito.ID_PF = Convert.ToInt64((retorno.Value).ToString)

            End If

            If Ent_Perito.ID_PF <> 0 Then
                If Not VerificaSeIDPFExisteEmPerito(Ent_Perito.ID_PF) Then
                    sd.ExecutaProc("NOVO_PERICIAS.Inserir_Perito", Ent_Perito.Cod_Perito, _
                                   Ent_Perito.Num_Reg, Ent_Perito.OBS, Ent_Perito.FALTA_ENTREGAR, _
                                   Ent_Perito.COD_ORGAO_PER, Ent_Perito.COD_ESPECIALIDADE, Ent_Perito.SIGLA, _
                                   IIf(Ent_Perito.Data_Cadastramento = #12:00:00 AM#, Today, Ent_Perito.Data_Cadastramento), _
                                   Ent_Perito.Indicacao, IIf(Ent_Perito.Data_Exclusao = #12:00:00 AM#, Nothing, Ent_Perito.Data_Exclusao), _
                                   Ent_Perito.SITUACAO_CADASTRO, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, _
                                   Ent_Perito.NUM_CONTA_CORRENTE, Ent_Perito.ID_PF, Ent_Perito.DocNecCV, _
                                   Ent_Perito.DocNecCPF, Ent_Perito.DocNecFoto, Ent_Perito.DocNecOrg, Ent_Perito.DocNecHab, _
                                   Ent_Perito.DocNecRes, Ent_Perito.DocNecImp, Ent_Perito.IDGED_Foto, _
                                   Ent_Perito.IDGED_CV, Nothing, 0, _
                                   0, Ent_Perito.NOME_AGENCIA, Nothing, _
                                   Ent_Perito.StatusAtual.Codigo, 0)

                    mm_ID_PF = CLng(Ent_Perito.ID_PF)
                    Ent_Perito.Cod_Perito = RetornaCodigoPerito(Ent_Perito.ID_PF, 1)
                End If
            End If


            If Ent_Perito.ID_PF = 0 Then
                If Ent_Perito.CPF = Nothing Then
                    sd.Close()
                    Exit Sub
                End If

                sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoafisica", retorno, Ent_Perito.Nome, _
                               Ent_Perito.CPF, Ent_Perito.SIGLA, Ent_Perito.Dt_Nasc, Ent_Perito.Cod_Tip_Sit)
                Ent_Perito.ID_PF = CInt(retorno.Value)

                If Ent_Perito.ID_PF = 0 Then
                    sd.Close()
                    Exit Sub
                End If

                mm_ID_PF = Ent_Perito.ID_PF

                sd.ExecutaProc("NOVO_PERICIAS.Inserir_Perito", Ent_Perito.Cod_Perito, _
                                   Ent_Perito.Num_Reg, Ent_Perito.OBS, Ent_Perito.FALTA_ENTREGAR, _
                                   Ent_Perito.COD_ORGAO_PER, Ent_Perito.COD_ESPECIALIDADE, Ent_Perito.SIGLA, _
                                   IIf(Ent_Perito.Data_Cadastramento = #12:00:00 AM#, Today, Ent_Perito.Data_Cadastramento), _
                                   Ent_Perito.Indicacao, IIf(Ent_Perito.Data_Exclusao = #12:00:00 AM#, Nothing, Ent_Perito.Data_Exclusao), _
                                   Ent_Perito.SITUACAO_CADASTRO, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, _
                                   Ent_Perito.NUM_CONTA_CORRENTE, Ent_Perito.ID_PF, Ent_Perito.DocNecCV, _
                                   Ent_Perito.DocNecCPF, Ent_Perito.DocNecFoto, Ent_Perito.DocNecOrg, Ent_Perito.DocNecHab, _
                                   Ent_Perito.DocNecRes, Ent_Perito.DocNecImp, Ent_Perito.IDGED_Foto, _
                                   Ent_Perito.IDGED_CV, Nothing, 0, _
                                   0, Ent_Perito.NOME_AGENCIA, Nothing, _
                                   Ent_Perito.StatusAtual.Codigo, 0)

                Ent_Perito.Cod_Perito = RetornaCodigoPerito(Ent_Perito.ID_PF, 1)

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
                    sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoafisicaemail", Ent_Perito.ID_PF, 1, Ent_Perito.EMAIL, Ent_Perito.SIGLA)
                End If
                If Ent_Perito.EMAIL1 <> "" Then
                    sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoafisicaemail", Ent_Perito.ID_PF, 2, Ent_Perito.EMAIL, Ent_Perito.SIGLA)
                End If
            Else
                If Validar(Ent_Perito.ID_PF) Then 'And Ent_Perito.Cod_Perito <> 0 Then
                    'AlterarPessoaFisica
                    mm_ID_PF = Ent_Perito.ID_PF

                    sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaFisica", Ent_Perito.ID_PF, Ent_Perito.Nome, Ent_Perito.CPF, Ent_Perito.SIGLA, Ent_Perito.Dt_Nasc, Ent_Perito.Cod_Tip_Sit)
                    sd.ExecutaProc("NOVO_PERICIAS.Alterar_Perito", Ent_Perito.Cod_Perito, _
                                   Ent_Perito.Num_Reg, Ent_Perito.OBS, Ent_Perito.FALTA_ENTREGAR, _
                                   Ent_Perito.COD_ORGAO_PER, Ent_Perito.COD_ESPECIALIDADE, Ent_Perito.SIGLA, _
                                   IIf(Ent_Perito.Data_Cadastramento = #12:00:00 AM#, Today, Ent_Perito.Data_Cadastramento), _
                                   Ent_Perito.Indicacao, IIf(Ent_Perito.Data_Exclusao = #12:00:00 AM#, Nothing, Ent_Perito.Data_Exclusao), _
                                   Ent_Perito.SITUACAO_CADASTRO, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, _
                                   Ent_Perito.NUM_CONTA_CORRENTE, Ent_Perito.ID_PF, Ent_Perito.DocNecCV, _
                                   Ent_Perito.DocNecCPF, Ent_Perito.DocNecFoto, Ent_Perito.DocNecOrg, Ent_Perito.DocNecHab, _
                                   Ent_Perito.DocNecRes, Ent_Perito.DocNecImp, Ent_Perito.IDGED_Foto, _
                                   Ent_Perito.IDGED_CV, Nothing, 0, 0, Ent_Perito.NOME_AGENCIA, Nothing, _
                                   Ent_Perito.StatusAtual.Codigo, 0)
                    If pInserirConta Then
                        'Msg = GravarContaCorrente(Ent_Perito.CPF, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, Ent_Perito.NOME_AGENCIA, Ent_Perito.NUM_CONTA_CORRENTE)
                        GravarContaCorrente(Ent_Perito.CPF, Ent_Perito.COD_BANCO, Ent_Perito.NUM_AGENCIA, Ent_Perito.NOME_AGENCIA, Ent_Perito.NUM_CONTA_CORRENTE)
                    End If

                    'Se endereço não preenchido exclui endereço residencial
                    If Ent_Perito.Nome_Logr = "" Then
                        sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaFisicaEnd", Ent_Perito.ID_PF, Ent_Perito.Seq_End)
                    Else
                        DsAchar = sd.ExecutaProcDS("Exibirdados_Endereco_Unico", sd.CriaRefCursor, Ent_Perito.ID_PF, Ent_Perito.Cod_Tip_End)

                        If DsAchar.Tables(0).Rows.Count > 0 Then
                            AlterarPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End)
                        Else
                            InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End)
                        End If
                    End If

                    'Se endereço não preenchido exclui endereço comercial
                    If Ent_Perito.Nome_Logr1 = "" Then
                        sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaFisicaEnd", Ent_Perito.ID_PF, Ent_Perito.Seq_End1)
                    Else
                        DsAchar = sd.ExecutaProcDS("Exibirdados_Endereco_Unico", sd.CriaRefCursor, Ent_Perito.ID_PF, Ent_Perito.Cod_Tip_End1)

                        If DsAchar.Tables(0).Rows.Count > 0 Then
                            AlterarPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End1)
                        Else
                            InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End1)
                        End If
                    End If

                    If Ent_Perito.EMAIL.ToString = "" Then
                        sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaFisicaEmail", Ent_Perito.ID_PF, 1)
                    Else
                        DsAchar = sd.ExecutaProcDS("Exibirdados_Email_Unico", sd.CriaRefCursor, Ent_Perito.ID_PF, 1)

                        If DsAchar.Tables(0).Rows.Count > 0 Then
                            sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaFisicaEmail", Ent_Perito.ID_PF, 1, Ent_Perito.EMAIL, Ent_Perito.SIGLA)
                        Else
                            sd.ExecutaProc("uc.Pericias_PKG.Inserir_PessoaFisicaEmail", Ent_Perito.ID_PF, 1, Ent_Perito.EMAIL, Ent_Perito.SIGLA)
                        End If
                    End If

                    CriarParametrosPFEmail1(Ent_Perito, mm_ID_PF)
                    If Ent_Perito.EMAIL1.ToString = "" Then
                        sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaFisicaEmail", Ent_Perito.ID_PF, 2)
                    Else
                        DsAchar = sd.ExecutaProcDS("Exibirdados_Email_Unico", sd.CriaRefCursor, Ent_Perito.ID_PF, 2)

                        If DsAchar.Tables(0).Rows.Count > 0 Then
                            sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaFisicaEmail", Ent_Perito.ID_PF, 2, Ent_Perito.EMAIL1, Ent_Perito.SIGLA)
                        Else
                            sd.ExecutaProc("uc.Pericias_PKG.Inserir_PessoaFisicaEmail", Ent_Perito.ID_PF, 2, Ent_Perito.EMAIL1, Ent_Perito.SIGLA)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try


    End Sub

    Private Sub InserirPessoaFisicaTelefone(ByVal m_ID_PF As Long, ByVal nCodTipTel As Integer, _
                                                ByVal sDDD As String, ByVal sTel As String, ByVal sRamal As String)
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Inserir_PessoaFisicaTel", m_ID_PF, RetornaUltimoSeqTel(m_ID_PF) + 1, nCodTipTel, sDDD, sTel, sRamal)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Private Sub InserirPessoaFisicaEndereco(ByVal ent As EntPERITO, ByVal m_ID_PF As Long, ByVal nCodTipEnd As Integer)
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Inserir_PessoaFisicaEnd", m_ID_PF, _
                               RetornaUltimoSeqEnd(CStr(ent.Cod_Perito), 1) + 1, _
                               IIf(nCodTipEnd = 2, ent.CEP, ent.CEP1), nCodTipEnd, _
                               IIf(nCodTipEnd = 2, ent.Cod_Tip_Logr, ent.Cod_Tip_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Nome_Logr, ent.Nome_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Num_Logr, ent.Num_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Compl_Logr, ent.Compl_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Cod_Bairro, ent.Cod_Bairro1), _
                               IIf(nCodTipEnd = 2, ent.Cod_Cidade, ent.Cod_Cidade1), _
                               IIf(nCodTipEnd = 2, ent.Sigla_UF, ent.Sigla_UF1))
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Private Sub AlterarPessoaFisicaEndereco(ByVal ent As EntPERITO, ByVal m_ID_PF As Long, ByVal nCodTipEnd As Integer)
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaFisicaEnd", m_ID_PF, _
                               RetornaUltimoSeqEnd(CStr(ent.Cod_Perito), 1) + 1, _
                               IIf(nCodTipEnd = 2, ent.CEP, ent.CEP1), nCodTipEnd, _
                               IIf(nCodTipEnd = 2, ent.Cod_Tip_Logr, ent.Cod_Tip_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Nome_Logr, ent.Nome_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Num_Logr, ent.Num_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Compl_Logr, ent.Compl_Logr1), _
                               IIf(nCodTipEnd = 2, ent.Cod_Bairro, ent.Cod_Bairro1), _
                               IIf(nCodTipEnd = 2, ent.Cod_Cidade, ent.Cod_Cidade1), _
                               IIf(nCodTipEnd = 2, ent.Sigla_UF, ent.Sigla_UF1))
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Private Sub AlterarPessoaFisicaTelefone(ByVal m_ID_PF As Long, ByVal nSeqTel As Integer, ByVal nCodTipTel As Integer, _
                                                ByVal sDDD As String, ByVal sTel As String, ByVal sRamal As String)
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaFisicaTel", m_ID_PF, nSeqTel, nCodTipTel, sDDD, sTel, sRamal)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function GravarExt(ByVal Ent_Perito As EntPERITO) As Long
        Dim mm_ID_PF As Long
        Dim retorno As New Oracle.DataAccess.Client.OracleParameter

        With retorno
            .DbType = DbType.Int64
            .Direction = ParameterDirection.Output
            .OracleDbType = OracleDbType.Int64
            .ParameterName = "Nid_Pf"
        End With

        If Ent_Perito.CPF.Trim <> "" Then
            Try
                sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoafisica", retorno, Ent_Perito.Nome, _
                                       Ent_Perito.CPF, Ent_Perito.SIGLA, Ent_Perito.Dt_Nasc, Ent_Perito.Cod_Tip_Sit)

                Ent_Perito.ID_PF = CInt(retorno.Value)

                If Ent_Perito.ID_PF <> 0 Then
                    mm_ID_PF = Ent_Perito.ID_PF

                    sd.ExecutaProc("Inserir_Perito_Ext", Nothing, Nothing, Ent_Perito.COD_ESPECIALIDADE, _
                                       "INTERNET", Today, Ent_Perito.ID_PF, Nothing, Nothing, Ent_Perito.COD_PROFISSAO1, _
                                       Ent_Perito.Data_Exclusao)
                    'Alterar A exibição dos dados do perito segregar cc.
                    If Ent_Perito.Nome_Logr.Trim <> "" Then
                        InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End)
                    End If
                    If Ent_Perito.Nome_Logr1.Trim <> "" Then
                        InserirPessoaFisicaEndereco(Ent_Perito, mm_ID_PF, Ent_Perito.Cod_Tip_End1)
                    End If

                    If Ent_Perito.EMAIL.Trim <> "" Then
                        sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoafisicaemail", Ent_Perito.ID_PF, 1, Ent_Perito.EMAIL, Ent_Perito.SIGLA)
                    End If
                    If Ent_Perito.EMAIL1.Trim <> "" Then
                        sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoafisicaemail", Ent_Perito.ID_PF, 2, Ent_Perito.EMAIL1, Ent_Perito.SIGLA)
                    End If
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sd.Close()
            End Try
        End If

        Return mm_ID_PF
    End Function

    Public Function GravarContaCorrente(ByVal pCpfCnpj As String, ByVal pCod_Banco As String, ByVal pNum_Agencia As String, ByVal pNome_Agencia As String, ByVal pNum_ContaCorrente As String) As String
        Dim retorno As New Oracle.DataAccess.Client.OracleParameter

        With retorno
            .Size = 4000
            .DbType = DbType.String
            .Direction = ParameterDirection.Output
            .OracleDbType = OracleDbType.Varchar2
            .ParameterName = "sMsg"
        End With

        Try
            sd.ExecutaProc("pacote_perito.altera_perito_financeiro", pCod_Banco, pNum_Agencia, pNome_Agencia, pNum_ContaCorrente, pCpfCnpj, retorno)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return retorno.Value.ToString()
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
        Parametros(0) = New ServicoDadosOracle.ParameterInfo("pCOD_PERITO", OracleDbType.Int32, ParameterDirection.Input)
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

        'ID_PJ
        iii = iii + 1
        '28
        Parametros(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PJ", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(iii).Valor = DBNull.Value

        'Status do Perito
        iii = iii + 1
        '29
        Parametros(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("ncod_tip_status", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(iii).Valor = IIf(entbal.StatusAtual.Codigo = Nothing, 1, entbal.StatusAtual.Codigo)

        'Inserir Conta
        iii = iii + 1
        '30
        Parametros(iii) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("pInserirConta", OracleDbType.Int32, ParameterDirection.Input)
        Parametros(iii).Valor = 0

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

    Public Sub GravarFoto(ByVal pCodigoPerito As String, ByVal sIDGED_Foto As String)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.Gravar_FotoPerito", pCodigoPerito, sIDGED_Foto)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub GravarCurriculum(ByVal m_ID_PF As String, ByVal sIDGED_CV As String)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.Gravar_FotoPerito", m_ID_PF, sIDGED_CV)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Function GravarFornecedor(ByVal Ent_Perito As EntPERITO) As String

        Dim ParametrosFornIncl(10) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim ParametrosFornAlter(11) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim j As Integer
        Dim pComplemento As String 'Endereço Residencial
        Dim Alteracao As Boolean
        Dim sSQL As String
        Dim msgPer As String

        Try
            sd.Open()

            sSQL = "Select Nome_fornec from fornecedores_peritos where COD_CGC_CPF = ? "

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, IIf(Ent_Perito.TipoPessoa = 1, Ent_Perito.CPF, Ent_Perito.CNPJ))

            If dr.Read Then
                Alteracao = True
            Else
                Alteracao = False
            End If

            pComplemento = (Ent_Perito.Descr_Tip_Logr.Trim & " " & Ent_Perito.Nome_Logr.Trim & _
                       " " & Ent_Perito.Compl_Logr.Trim).Trim

            If Not Alteracao Then
                msgPer = sd.ExecutaFunc("Grava_TmpForn", 250, _
                                        UCase(Ent_Perito.Nome), _
                                        pComplemento, _
                                        UCase(Ent_Perito.Descr_Bairro), _
                                        UCase(Ent_Perito.Descr_Cidade), _
                                        Ent_Perito.CEP, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        IIf(Ent_Perito.TipoPessoa = 1, Ent_Perito.CPF, Ent_Perito.CNPJ), _
                                        Ent_Perito.EMAIL, _
                                        Ent_Perito.EMAIL1, _
                                        Ent_Perito.Sigla_UF)

            Else
                msgPer = sd.ExecutaFunc("pacote_perito.altera_perito", 300, _
                                        UCase(Ent_Perito.Nome), _
                                        pComplemento, _
                                        UCase(Ent_Perito.Descr_Bairro), _
                                        UCase(Ent_Perito.Descr_Cidade), _
                                        Ent_Perito.CEP, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        Nothing, _
                                        IIf(Ent_Perito.TipoPessoa = 1, Ent_Perito.CPF, Ent_Perito.CNPJ), _
                                        Ent_Perito.Cod_Tip_Sit, _
                                        UCase(Ent_Perito.Sigla_UF), _
                                        Ent_Perito.EMAIL, _
                                        Ent_Perito.EMAIL1)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return msgPer
    End Function

    Public Function VerCPFPerito(ByVal pCPF As String) As Boolean
        Try
            sd.Open()

            Dim dr As OracleDataReader = sd.ExecutaProcDR("NOVO_PERICIAS.VerCPFPerito", pCPF, sd.CriaRefCursor)

            If dr.Read Then
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

    Public Function VerCNPJPerito(ByVal pCNPJ As String) As Boolean
        Try
            sd.Open()

            Dim dr As OracleDataReader = sd.ExecutaProcDR("NOVO_PERICIAS.VerCNPJPerito", pCNPJ, sd.CriaRefCursor)

            If dr.Read Then
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

    Public Function BuscaPeritoPJ(ByVal pCNPJ As String) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.VerCNPJPerito", pCNPJ, sd.CriaRefCursor)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function BuscaPeritoPJPericias(ByVal pID_PJ As Int64) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.VerificaSeIDPJExisteEmPerito", pID_PJ, sd.CriaRefCursor)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function


    Public Function VerCPFPeritoExt(ByVal pCPF As String) As Boolean
        Dim sSQL As String = "Select * from UC.PessoaFisica PF where PF.CPF = ?  "

        Try
            sd.Open()

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pCPF)

            If dr.Read Then
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
    Public Function VerHomonimo(ByVal pNome As String, ByVal pessoa As Integer, ByVal EhNomeFantasia As Boolean) As Boolean
        Try
            Dim ds As DataSet = sd.ExecutaProcDS("NOVO_PERICIAS.VerHomonimo", pNome, pessoa, IIf(EhNomeFantasia, 1, 0), sd.CriaRefCursor)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim numeroNomes As Integer = CInt(IIf(ds.Tables(0).Rows(0).Item(0).Equals(DBNull.Value), 0, ds.Tables(0).Rows(0).Item(0)))

                If numeroNomes > 1 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Nome_ID(ByVal pID As String) As String
        Dim NomePerito As String = String.Empty

        Try
            sd.Open()

            Dim dr As OracleDataReader = sd.ExecutaProcDR("NOVO_PERICIAS.Nome_ID", pID, sd.CriaRefCursor())

            If dr.Read Then
                NomePerito = dr("nome")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return NomePerito
    End Function

    Function ValidarPeritoCPF(ByVal pId_USU As Integer) As String
        Dim sSQL As String
        Dim sCPF As String = String.Empty

        Try
            sd.Open()

            sSQL = "Select pf.CPF from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF, Usuario U, Peritos P " & _
                   "where U.ID_USU = ? and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF and U.CPF_USU = PF.CPF and " & _
                   "P.ID_PF = PF.ID_PF and P.Data_Exclusao = to_date('01/01/1900','dd/mm/yyyy')"

            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pId_USU.ToString)

            If dr.Read Then
                sCPF = dr(0)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return sCPF
    End Function

    Function ValidarPerito(ByVal pId_USU As Integer) As Boolean
        Dim sSQL As String

        Try
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
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Function ID_Perito(ByVal pId_PF As Integer) As String
        Dim sSQL As String

        Try
            sd.Open()

            sSQL = "Select PF.ID_PF from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF, Usuario U " & _
                   "where U.ID_USU = ? and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF and U.CPF_USU = PF.CPF"
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pId_PF.ToString)

            If dr.Read Then
                Return dr("ID_PF").ToString
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Function LocalizaCPFPerito(ByVal pId_PF As Integer) As String
        Dim sSQL As String

        Try
            sd.Open()

            sSQL = "Select CPF from UC.PessoaFisica PF, UC.PessoaFisicaFuncao PFF where PF.ID_PF = ?  and PFF.Cod_Tip_Func = 4 and PFF.ID_PF = PF.ID_PF"
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pId_PF.ToString)

            If dr.Read Then
                Return dr("CPF")
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
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
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Alterar_Status_PessoaFisica", pID_PF, pSigla)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
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

    Public Function VerificaPeritoFornecedor(ByVal scod_perito As Long) As Boolean
        Try
            Dim retorno As String

            retorno = sd.ExecutaFunc("NOVO_PERICIAS.Verifica_Perito_Fornecedor", 250, scod_perito)

            If retorno = "null" OrElse retorno.Trim = String.Empty Then
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

    Public Function RetornaUltimoSeqEnd(ByVal codPerito As String, ByVal tipoPessoa As Integer) As Integer
        Dim nSeq As Integer = 0

        Try
            nSeq = sd.ExecutaFunc("NOVO_PERICIAS.UltimoSequenceEndereco", 250, codPerito, tipoPessoa)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return nSeq
    End Function

    Public Function RetornaUltimoSeqTel(ByVal sIDPF As String) As Integer
        Dim nSeq As Integer = 0

        Try
            nSeq = sd.ExecutaFunc("ultimoseqtelefone", 50, sIDPF)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return nSeq
    End Function

    Public Function RetornarIDPF(ByVal sCPF As String) As String
        Dim sRetorno As String = String.Empty

        Try
            sRetorno = sd.ExecutaFunc("RetornaIDPF", 20, sCPF)

            If sRetorno = "0" Then
                Return String.Empty
            Else
                Return sRetorno
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function RetornaCodigoPerito(ByVal sIdentificador As Int64, ByVal tipoPessoa As Integer) As Int64
        Dim ds As DataSet = Nothing
        Dim codigoPerito As Int64 = 0

        Try

            ds = sd.ExecutaProcDS("NOVO_PERICIAS.RetornaCodigoPerito", sIdentificador, tipoPessoa, sd.CriaRefCursor)

            If ds.Tables(0).Rows.Count > 0 Then
                If Not ds.Tables(0).Rows(0).Item("COD_PERITO").Equals(DBNull.Value) Then
                    codigoPerito = Int64.Parse(ds.Tables(0).Rows(0).Item("COD_PERITO"))
                End If
            End If

            Return codigoPerito
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function VerificaSeIDPFExisteEmPerito(pid_pf As Int64) As Boolean
        Dim ds As DataSet = Nothing

        Try
            If pid_pf = Nothing OrElse pid_pf = 0 Then
                Throw New Exception("Operação inválida!")
            End If

            ds = sd.ExecutaProcDS("NOVO_PERICIAS.VerificaSeIDPFExisteEmPerito", pid_pf, sd.CriaRefCursor())

            If ds.Tables(0).Rows.Count = 0 Then
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

    'Essa rotina foi adaptada para verificar a existência do perito na base do Perícias (Seja ele PF ou PJ)
    Public Function VerPeritoPericias(pIdentidade As String, pTipoPessoa As EntPERITO.Pessoa) As Long
        Dim id As Long = 0

        Try
            id = CLng(sd.ExecutaProcDS("NOVO_PERICIAS.VerPeritoPericias", pIdentidade, pTipoPessoa, sd.CriaRefCursor).Tables(0).Rows(0).Item("ID"))

            If id = Nothing Then
                id = 0
            End If

            Return id
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function
End Class

