Imports System.Web
Imports System.Collections.Generic
Imports BAL
Imports Entidade
Imports WebServicePadrao
Imports SegurancaWeb.servicoSEG
Imports DGTECGEDARDOTNET
Imports System.Text.RegularExpressions


Public Class BasePage
    Inherits System.Web.UI.Page
    'Inherits System.Object

#Region "USUARIO"
    Public Function VerificaAutorizacaoUsuario(ByVal pagina As String, ByVal objeto As String, ByVal funcao As String) As Boolean
        Dim autorizacoes As SegurancaWeb.ServicoCofre.EstAutorizacao()
        Dim autorizado As Boolean = False
        Try
            autorizacoes = GetAutorizacoesUsuario()

            For Each aut As SegurancaWeb.ServicoCofre.EstAutorizacao In autorizacoes
                Select Case UCase(aut.janela)
                    Case UCase(pagina)
                        Select Case UCase(aut.objFunc)
                            Case UCase(objeto)
                                Select Case UCase(aut.siglaFunc)
                                    Case UCase(funcao)
                                        If aut.indAutorizado = "S" Then
                                            autorizado = True
                                            Exit For
                                        End If
                                End Select

                        End Select
                End Select
            Next

            Return autorizado
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetAutorizacoesUsuario() As SegurancaWeb.ServicoCofre.EstAutorizacao()
        Dim estr As SegurancaWeb.ServicoCofre.EstUsuario
        Try
            estr = Session("PERICIAS")
            Return estr.autorizacoes

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "msg", "alert('Atenção !!! \n\n " & ex.Message & "');", True)
            Return Nothing
        End Try
    End Function

    Public Function GetUsuario() As EstruturaPadrao.EstruturaIdentificacaoUsuario
        Dim ip As String
        'Dim estr As EstruturaPadrao.EstruturaIdentificacaoUsuario
        Dim est As EstruturaPadrao.EstruturaIdentificacaoUsuario
        'Dim estUsuario As Object

        'Ex:     Session("SIGLA_DO_SISTEMA")
        'Esta sessão contem 2 classes :
        'estUsuario - Dados do usuário autenticado
        'estAutorizacoes - Autorizações do usuário autenticado.

        '5.2 - Sessão com o orgão que o usuário está  autenticado no sistema:
        'Ex:     Session("SIGLA_DO_SISTEMA_CODORGAO")

        'Try

        'EstAutorizacao
        'Description()
        'indAutorizado()
        'indAutorizadoCertDig()
        'janela()
        'objFunc()
        'siglaFunc()
        'urlCertDig()

        'EstOrgaoGrupoUsu
        'Description()
        'codGrupo()
        'codOrgao()
        'descrGrupo()
        'descrOrgao()
        'indAtivo()

        'estUsuario()
        'Description()
        'autorizacoes()
        'cargoUsu()
        'cod_Grupo_Usu()
        'codAdv()
        'codOrgaoLotado()
        'codUsu()
        'codUsuMumps()
        'CPF()
        'dtCadPres()
        'grupo_Usu()
        'idDef()
        'idFunc()
        'idPessoa()
        'idProm()
        'idUsu()
        'indAutenticaAD()
        'indCadPres()
        'indTrocaSenha()
        'matr_Formatada()
        'matr_Rh()
        'matricula()
        'nome()
        'orgaoLotacao()
        'origemUsuario()
        'supervisor()

        Try
            'estUsuario = CType(Session("PERICIAS"), EstruturaPadrao.EstruturaIdentificacaoUsuario)
            'estUsuario = New EstruturaPadrao.EstruturaIdentificacaoUsuario
            'estUsuario = Session("PERICIAS")
            est = New EstruturaPadrao.EstruturaIdentificacaoUsuario
            'est.CodOrg = Session("SIGLA_DO_SISTEMA_CODORGAO").CodOrgao
            est.Login = Session("PERICIAS").codusu
            est.LoginAutent = Session("PERICIAS").codusu
            est.Senha = ""  'Session("PERICIAS").Senha
            est.SenhaAutent = "" 'Session("PERICIAS").SenhaAutent
            est.SiglaSist = "PERICIAS"
            est.UsuarioSO = Session("PERICIAS").codusu 'Session("PERICIAS").SiglaFunc
            ID_Usuario = Session("PERICIAS").idusu
            Usuario = est.UsuarioSO
            ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR") ' por proxy
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR") 'original
            End If
            Session("ip") = ip

            est.NomeMaquina = NomeMaquina() '.NomeMaquina = ip ' Identificação da Máquina
            est.CodOrg = Session("PERICIAS_CODORGAO")



            Return est
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "msg", "alert('Atenção !!! \n\n " & ex.Message & "');", True)
        Finally
            'Return est
        End Try

        Return Nothing

    End Function
#End Region


#Region "Métodos do Júlio"

    'Public estUsuario As EstruturaPadrao.EstruturaIdentificacaoUsuario 'Object
    Public Shared Usuario As String
    Public Shared ID_Usuario As Integer
    '    Public Shared UsuarioAutorizado As String

    Public Function NomeMaquina() As String
        Dim sNomeMaquina As String = UCase(Environment.MachineName)
        Return sNomeMaquina
    End Function

    Public Function PreencheNomeOrgao(ByVal sCodOrg As String) As String
        Dim sNomeOrgao As String
        Dim bal As New BALORGAO(GetUsuario)
        Dim ent As New EntORGAO

        ent = bal.Consultar(Convert.ToDecimal(sCodOrg))

        If ent.NomeDtl = String.Empty Then
            sNomeOrgao = "<< CÓDIGO INEXISTENTE >>"
        Else
            sNomeOrgao = ent.NomeDtl
        End If
        Return sNomeOrgao

    End Function

    Public Function validaAcesso() As Boolean

        Dim dsAutorizacoes As Data.DataSet = Session("dsAutorizacoes")
        Dim i As Integer
        Dim url As String()
        Dim pagina As String
        Dim sNomeObj As String

        Dim bRetorno As Boolean = False

        'Campos do dataset "dsAutorizacoes":
        'NOME_JAN, NOME_OBJ, IND_AUTORIZADO, SG_FUNC

        url = Split(Request.Url.AbsolutePath, "/")
        pagina = Left(url(url.Length - 1), Len(url(url.Length - 1)) - 5)

        For i = 0 To dsAutorizacoes.Tables(0).Rows.Count - 1
            With dsAutorizacoes.Tables(0).Rows(i)
                sNomeObj = .Item("NOME_OBJ")
                'snomepag()
                If sNomeObj.ToUpper = pagina.ToUpper Then
                    If .Item("IND_AUTORIZADO") = "S" Then
                        bRetorno = True
                        Exit For
                    End If
                End If
            End With
        Next

        Return bRetorno

    End Function

    Protected Sub ImprimeRelatorio(ByVal sURL As String)
        Dim js As New System.Text.StringBuilder
        Dim sFRM As String = "frmRelatorios.aspx" & sURL

        'Response.Redirect(sFRM)

        js.Append("<script>")
        js.Append("ExibeJanelaRelatorio('" & sFRM & "');")
        js.Append("</script>")

        If (Not ClientScript.IsClientScriptBlockRegistered("Wclose")) Then
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "Wclose", js.ToString, False)
        End If
    End Sub

    Protected Sub ImprimeRelatorioTXT(ByVal sURL As String)
        Dim js As New System.Text.StringBuilder
        Dim sFRM As String = "frmRelatorioTXT.aspx" & sURL

        'Response.Redirect(sFRM)

        js.Append("<script>")
        js.Append("ExibeJanelaRelatorio('" & sFRM & "');")
        js.Append("</script>")

        If (Not ClientScript.IsClientScriptBlockRegistered("Wclose")) Then
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "Wclose", js.ToString, False)
        End If
    End Sub
    Public Sub MsgErro(ByVal msg As String, Optional ByVal titulo As String = "Erro")

        titulo = IIf(String.IsNullOrEmpty(titulo), "Erro", titulo)
        Dim tipo As String = IIf(titulo = "Erro", "erro", "sucesso")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "MensagemNaTela", "Mensagem('" + tipo + "','" + titulo + "','" & msg & "');", True)

    End Sub

    Public Function CalcularIdade(ByVal DataNasc As String) As String
        Dim m_DtNasc As Date
        Dim m_Anos As Integer
        Dim m_Mes As Integer
        Dim m_Dia As Integer
        Dim m_Idade As String
        If DataNasc = "//" Or DataNasc = "' Then" Then
            m_Idade = "0"
            Return m_Idade
            Exit Function
        Else
            m_DtNasc = CDate(DataNasc)
        End If
        m_Anos = Year(Now) - Year(m_DtNasc)
        m_Mes = Month(m_DtNasc)
        m_Dia = Day(m_DtNasc)
        m_Idade = m_Anos.ToString
        If Month(Now) < m_Mes Then
            m_Idade = (m_Anos - 1).ToString
        Else
            If m_Mes = Month(Now) And Day(Now) < m_Dia Then
                m_Idade = (m_Anos - 1).ToString
            End If
        End If
        Return m_Idade
    End Function
    Public Function ValidaNumCNJ(ByVal Num_CNJ As String) As Boolean
        Dim Num_CNJ_New As Boolean
        Num_CNJ_New = False
        If Len(Num_CNJ) = 25 And Len(Num_CNJ) = 20 Then
            MsgErro("Número de CNJ Inválido")
        Else
            '00000317-85.2009.8.19.9000
            '00000178520098199000
            If Len(Num_CNJ) <> 20 Then
                Num_CNJ = Mid(Num_CNJ, 1, 7) + Mid(Num_CNJ, 9, 2) + Mid(Num_CNJ, 12, 4) + Mid(Num_CNJ, 17, 1) + Mid(Num_CNJ, 19, 2) + Mid(Num_CNJ, 22, 4)
            End If
            If IsNumeric(Num_CNJ) Then
                Num_CNJ_New = True
            End If
        End If
        Return Num_CNJ_New
    End Function
    Public Function ValidaNumProc(ByVal Num_Proc As String) As Boolean
        Dim Num_Proc_New As Boolean
        Num_Proc_New = False
        If Len(Num_Proc) <> 17 And Len(Num_Proc) <> 14 Then
            MsgErro("Número de Processo Inválido")
        Else
            '2009.700.000027-9
            If Len(Num_Proc) <> 14 Then
                Num_Proc = Mid(Num_Proc, 1, 4) + Mid(Num_Proc, 6, 3) + Mid(Num_Proc, 10, 6) + Mid(Num_Proc, 17, 1)
            End If
            If IsNumeric(Num_Proc) Then
                If InStr(Num_Proc, ".") = 0 Then
                    Num_Proc_New = True
                Else
                    Num_Proc_New = False
                End If
            End If
        End If
        Return Num_Proc_New
    End Function

    Public Function GetUsuarioExt() As EstruturaPadrao.EstruturaIdentificacaoUsuario
        Dim ip As String
        Dim est As EstruturaPadrao.EstruturaIdentificacaoUsuario

        Try
            est = New EstruturaPadrao.EstruturaIdentificacaoUsuario
            est.Login = "TJ_11111111112" 'Session("PERICIAS").codusu
            est.LoginAutent = "TJ_11111111112" 'Session("PERICIAS").codusu
            est.Senha = ""
            est.SenhaAutent = "" 'Session("PERICIAS").SenhaAutent
            est.SiglaSist = "PERICIAS"
            est.UsuarioSO = "TJ_11111111112" 'Session("PERICIAS").codusu 'Session("PERICIAS").SiglaFunc
            ID_Usuario = 20421 'Session("PERICIAS").idusu
            Usuario = est.UsuarioSO
            ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR") ' por proxy
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR") 'original
            End If
            Session("ip") = ip
            est.NomeMaquina = NomeMaquina() '.NomeMaquina = ip ' Identificação da Máquina
            'est.CodOrg = 2231 'Session("PERICIAS_CODORGAO") 2231 - Tribunal de Justiça
            est.CodOrg = 4640 'A DISPOSICAO

            Return est
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "msg", "alert('Atenção !!! \n\n " & ex.Message & "');", True)
        Finally
            'Return est
        End Try

        Return Nothing

    End Function

    Function ValidarCPFServ(ByVal pCPF As String) As Boolean

        Dim CPF_temp As String
        Dim CPF_Digito_temp As String
        Dim Soma, Resto As Integer
        Dim DigitoHum As String
        Dim DigitoDois As String
        Dim DigitoCPF As String
        Dim TudoIgual As Boolean
        Dim aux As String
        ValidarCPFServ = False
        If Not IsNumeric(pCPF) Or Len(pCPF) <> 11 Then
            Return False
            Exit Function
        End If
        ValidarCPFServ = False
        TudoIgual = True
        aux = Trim(pCPF)
        For i = 1 To Len(aux) - 1
            If Mid(aux, i, 1) <> Mid(aux, i + 1, 1) Then TudoIgual = False
        Next i
        If TudoIgual Then Exit Function
        CPF_temp = Trim(pCPF)
        CPF_Digito_temp = Mid(CPF_temp, 10, 2)

        'Somando os nove primeiros digitos do CPF
        Soma = (CLng(Mid(CPF_temp, 1, 1)) * 10) + (CLng(Mid(CPF_temp, 2, 1)) * 9) + (CLng(Mid(CPF_temp, 3, 1)) * 8) + (CLng(Mid(CPF_temp, 4, 1)) * 7) + (CLng(Mid(CPF_temp, 5, 1)) * 6) + (CLng(Mid(CPF_temp, 6, 1)) * 5) + (CLng(Mid(CPF_temp, 7, 1)) * 4) + (CLng(Mid(CPF_temp, 8, 1)) * 3) + (CLng(Mid(CPF_temp, 9, 1)) * 2)
        '----------------------------------
        'Calculando o 1º dígito verificador
        '----------------------------------
        'Pegando o resto da divisão por 11
        Resto = (Soma Mod 11)

        If Resto = 1 Or Resto = 0 Then
            DigitoHum = "0"
        Else
            DigitoHum = CStr(11 - Resto)
        End If
        '----------------------------------
        '----------------------------------
        'Calculando o 2º dígito verificador
        '----------------------------------
        'Somando os 9 primeiros digitos do CPF mais o 1º dígito
        Soma = (CLng(Mid(CPF_temp, 1, 1)) * 11) + (CLng(Mid(CPF_temp, 2, 1)) * 10) + (CLng(Mid(CPF_temp, 3, 1)) * 9) + (CLng(Mid(CPF_temp, 4, 1)) * 8) + (CLng(Mid(CPF_temp, 5, 1)) * 7) + (CLng(Mid(CPF_temp, 6, 1)) * 6) + (CLng(Mid(CPF_temp, 7, 1)) * 5) + (CLng(Mid(CPF_temp, 8, 1)) * 4) + (CLng(Mid(CPF_temp, 9, 1)) * 3) + (DigitoHum * 2)
        'Pegando o resto da divisão por 11
        Resto = (Soma Mod 11)

        If Resto = 1 Or Resto = 0 Then
            DigitoDois = "0"
        Else
            DigitoDois = CStr(11 - Resto)
        End If
        '----------------------------------
        'Verificando se os digitos são iguais aos digítados.
        DigitoCPF = CStr(DigitoHum) + CStr(DigitoDois)

        If CStr(CPF_Digito_temp) = CStr(DigitoCPF) Then
            ValidarCPFServ = True
        End If
        Return ValidarCPFServ
    End Function

#End Region

#Region "Métodos Gabriel"

    Public Sub ExcluiArquivoCaminho(caminhoArquivo As String, pastaLog As String)
        Try
            Dim log As New StringBuilder

            Try
                If IO.File.Exists(caminhoArquivo) Then
                    IO.File.Delete(caminhoArquivo)
                End If
            Catch ex As Exception
                log.AppendLine(caminhoArquivo & " - " & ex.Message)
            End Try

            If log.Length > 0 Then
                IO.File.AppendAllText(Server.MapPath(pastaLog + "/" & Format(Now, "dd-MM-yyyy HH-mm-ss") & "-Log.txt"), log.ToString)
            End If
        Catch

        End Try
    End Sub

    Public Function NomePagina() As String
        Try
            Return Request.Path.Substring(Request.Path.LastIndexOf("/") + 1)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function ValidarCPF(ByVal pCPF As String) As Boolean
        Try
            Dim digito As String = pCPF.Substring(pCPF.Length - 2)
            Dim v1, v2 As Integer

            For i As Integer = 0 To pCPF.Length - 3
                v1 = v1 + CInt(pCPF.ToCharArray()(i).ToString) * (10 - i)
                v2 = v2 + CInt(pCPF.ToCharArray()(i).ToString) * (11 - i)
            Next

            v1 = (v1 * 10) Mod 11

            If v1 >= 10 Then
                v1 = 0
            End If

            v2 += v1 * 2
            v2 = (v2 * 10) Mod 11

            If v2 >= 10 Then
                v2 = 0
            End If

            Return digito = (v1.ToString & v2.ToString)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ValidarCNPJ(ByVal pCNPJ As String) As Boolean
        Try
            Dim Numero(13) As Integer
            Dim soma As Integer
            Dim i As Integer
            Dim resultado1 As Integer
            Dim resultado2 As Integer

            For i = 0 To Numero.Length - 1
                Numero(i) = CInt(pCNPJ.Substring(i, 1))
            Next

            soma = Numero(0) * 5 + Numero(1) * 4 + Numero(2) * 3 + Numero(3) * 2 + Numero(4) * 9 + Numero(5) * 8 + Numero(6) * 7 + _
                       Numero(7) * 6 + Numero(8) * 5 + Numero(9) * 4 + Numero(10) * 3 + Numero(11) * 2
            soma = soma - (11 * (Int(soma / 11)))

            If soma = 0 Or soma = 1 Then
                resultado1 = 0
            Else
                resultado1 = 11 - soma
            End If

            If resultado1 = Numero(12) Then
                soma = Numero(0) * 6 + Numero(1) * 5 + Numero(2) * 4 + Numero(3) * 3 + Numero(4) * 2 + Numero(5) * 9 + Numero(6) * 8 + _
                             Numero(7) * 7 + Numero(8) * 6 + Numero(9) * 5 + Numero(10) * 4 + Numero(11) * 3 + Numero(12) * 2
                soma = soma - (11 * (Int(soma / 11)))

                If soma = 0 Or soma = 1 Then
                    resultado2 = 0
                Else
                    resultado2 = 11 - soma
                End If

                If resultado2 = Numero(13) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornaNumerosDeString(ByVal texto As String) As String
        Dim retorno As String = String.Empty

        retorno = (From c As Char In texto Select c Where Char.IsDigit(c)).ToArray

        Return retorno
    End Function

    Public Function InicializaGed() As DGTECGEDAR
        Try
            Dim ambiente As Integer = CInt(ConfigurationManager.AppSettings("AMBIENTE"))
            Dim objGed As New DGTECGEDAR

            If ambiente = 3 Then
                objGed.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Producao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
            Else
                objGed.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
            End If

            Return objGed
        Catch ex As Exception
            MsgErro(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ArmazenaArquivoGED(ByVal arquivo As String) As String
        Try
            Dim Obj As DGTECGEDAR = InicializaGed()
            Dim idGed As String
            Dim ambiente As Integer = CInt(ConfigurationManager.AppSettings("AMBIENTE"))
            idGed = Obj.Armazena(arquivo)
            Obj.Finaliza()
            Return idGed
        Catch ex As Exception
            MsgErro(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Sub ExcluiArquivoGED(ByVal IdGed As String)
        Try
            Dim obj As DGTECGEDAR = InicializaGed()
            Dim ambiente As Integer = CInt(ConfigurationManager.AppSettings("AMBIENTE"))

            obj.Exclui(IdGed, False)
            obj.Finaliza()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Public Function MontaURLDocumentoGED(ByVal IdGed As String) As String
        Try
            Dim ged As DGTECGEDAR = InicializaGed()
            Dim url As String = ged.MontaURLCacheWeb(IdGed)
            ged.Finaliza()
            Return url
        Catch ex As Exception
            MsgErro(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function ExibirArquivoGedFoto(ByVal IdGed As String, ByVal tipoArquivo As String) As String
        Try
            Dim ged As DGTECGEDAR = InicializaGed()
            Dim caminhoArquivo As String = String.Empty

            Select Case tipoArquivo
                Case "Documento"
                    caminhoArquivo = "Documentos/Arquivo-" + IdGed
                Case "Imagem"
                    caminhoArquivo = "Imagens/Foto-" + IdGed
                Case Else

            End Select

            ged.Recupera(IdGed, Server.MapPath(caminhoArquivo))
            ged.Finaliza()

            Return caminhoArquivo

        Catch ex As Exception
            MsgErro(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RetornaCPFCNPJFormatado(ByVal cpfcnpj As String) As String
        Try
            Dim novoCpfCnpj As String = Nothing

            If cpfcnpj.Length = 11 Then
                novoCpfCnpj = String.Format("{0}.{1}.{2}-{3}",
                                            cpfcnpj.Substring(0, 3),
                                            cpfcnpj.Substring(3, 3),
                                            cpfcnpj.Substring(6, 3),
                                            cpfcnpj.Substring(9))
            ElseIf cpfcnpj.Length = 14 Then
                novoCpfCnpj = String.Format("{0}.{1}.{2}/{3}-{4}",
                                            cpfcnpj.Substring(0, 2),
                                            cpfcnpj.Substring(2, 3),
                                            cpfcnpj.Substring(5, 3),
                                            cpfcnpj.Substring(8, 4),
                                            cpfcnpj.Substring(11))
            End If

            Return novoCpfCnpj
        Catch ex As Exception
            MsgErro(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function RetornaNumeroProcessoAdministrativo(ByVal numeroProcesso As String) As Long
        Try
            Dim numProt As String = Nothing
            Dim ano As String = Nothing
            Dim processo As String = Nothing
            Dim numeroDeZeros As Integer = 0
            Dim tamanhoNumero As Integer = 11

            ano = numeroProcesso.Substring(0, 4)
            processo = RetornaNumerosDeString(numeroProcesso.Substring(4))

            numeroDeZeros = tamanhoNumero - (ano.Length + processo.Length)

            For i As Integer = 0 To (numeroDeZeros - 1)
                ano = String.Format("{0}0", ano)
            Next
            numProt = String.Format("{0}{1}", ano, processo)

            Return CLng(numProt)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidaFormatoProcesso(ByVal numeracaoProcesso As String) As TipoNumeracaoProcesso
        Try
            Dim expressaoLocalizada As Match = Nothing

            Dim formatoAdm As String = "^[0-9]{4}\-[0-9]+$"
            Dim formatoAdm2 As String = "^[0-9]{11}"
            Dim formatoNumAnt As String = "^[0-9]{4}\.[0-9]{3}\.[0-9]{6}\-[0-9]{1}$"
            Dim formatoNumOutros As String = "^[0-9]{4}\.[0-9]{3}\.[0-9]{5}$"
            Dim formatoCnj As String = "^[0-9]{7}\-[0-9]{2}\.[0-9]{4}\.[0-9]{1}\.[0-9]{2}\.[0-9]{4}$"



            'expressaoLocalizada = 
            If Regex.Match(numeracaoProcesso, formatoAdm).Success OrElse Regex.Match(numeracaoProcesso, formatoAdm2).Success Then
                Return TipoNumeracaoProcesso.ADMINISTRATIVO
            ElseIf Regex.Match(numeracaoProcesso, formatoNumAnt).Success OrElse Regex.Match(numeracaoProcesso, formatoNumOutros).Success Then
                Return TipoNumeracaoProcesso.NUMERACAOANTIGA
            ElseIf Regex.Match(numeracaoProcesso, formatoCnj).Success Then
                Return TipoNumeracaoProcesso.NUMERACAOCNJ
            Else
                Return TipoNumeracaoProcesso.FORMATOINVALIDO
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Enum TipoNumeracaoProcesso
        ADMINISTRATIVO = 1
        NUMERACAOANTIGA = 2
        NUMERACAOCNJ = 3
        FORMATOINVALIDO = 4
    End Enum

    Enum TipoDeProcesso
        ADMINISTRATIVO = 1
        JUDICIAL = 2
    End Enum

#End Region

#Region "Métodos do Júlio - parte 2"
    Public Function ToBase64(ByVal data() As Byte) As String

        If data Is Nothing Then Throw New ArgumentNullException("data")

        Return Convert.ToBase64String(data)

    End Function

    Public Function FromBase64(ByVal base64 As String) As Byte()

        If base64 Is Nothing Then Throw New ArgumentNullException("base64")

        Return Convert.FromBase64String(base64)

    End Function

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim caminho As String = Server.MapPath(ImageFilePath)
        'Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(caminho) = True Then
            Throw New ArgumentNullException("O nome do arquivo de imagem não pode ser nulo ou vazio", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(caminho)
            'Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(caminho, IO.FileMode.Open, IO.FileAccess.Read)
            'Dim _BinaryReader As New IO.BinaryReader(_FStream)
            '_tempByte = _BinaryReader.ReadBytes(Convert.ToInt64(_NumBytes))
            '_fileInfo = Nothing
            '_NumBytes = 0
            '_FStream.Close()
            '_FStream.Dispose()
            '_BinaryReader.Close()
            'Return _tempByte
            Dim filebyte(_FStream.Length() - 1) As Byte
            _FStream.Read(filebyte, 0, filebyte.Length)
            _FStream.Close()
            Return filebyte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        carregaInformacoesUsuario()
    End Sub

    Public Sub carregaInformacoesUsuario()
        Try
            Dim dsSeg As New System.Data.DataSet
            Dim estr As SegurancaWeb.ServicoCofre.EstUsuario

            estr = New SegurancaWeb.ServicoCofre.EstUsuario
            estr = Session("PERICIAS")
            Session("codOrgaoUsu") = Session("PERICIAS_CODORGAO")
            Session("Usuario") = estr.codUsu
            Session("CPFUsu") = estr.CPF
            Session("NomeUsuCPF") = estr.nome
            Session("NomeUsuMatr") = estr.matricula
            Session("UsuMatr") = estr.matricula
            Session("IdFunc") = estr.idFunc
            Session("Origem") = estr.origemUsuario
            Session("JANELAS") = estr.autorizacoes

            'Session("codOrgaoUsu") = "8950"
            'Session("Usuario") = "RSCARDOSO"
            'Session("CPFUsu") = "000000"
            'Session("NomeUsuCPF") = "RENATO SILVADO"
            'Session("NomeUsuMatr") = "015015"
            'Session("UsuMatr") = "015015"
            'Session("IdFunc") = "4641"
            'Session("Origem") = "T"
            'Session("JANELAS") = ""

        Catch ex As Exception
            MsgErro("Usuário não logado! Favor sair e logar novamente.")
        End Try

    End Sub

#End Region

End Class

