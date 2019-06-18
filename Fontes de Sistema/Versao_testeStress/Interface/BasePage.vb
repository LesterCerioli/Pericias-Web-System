Imports System.Web
Imports System.Collections.Generic
Imports BAL
Imports Entidade
Imports WebServicePadrao
Imports SegurancaWeb.servicoSEG


Public Class BasePage
    Inherits System.Web.UI.Page
    'Inherits System.Object

    'Public estUsuario As EstruturaPadrao.EstruturaIdentificacaoUsuario 'Object
    Public Shared Usuario As String
    Public Shared ID_Usuario As Integer
    '    Public Shared UsuarioAutorizado As String
    'Public Function GetUsuario() As EstruturaPadrao.EstruturaIdentificacaoUsuario
    '    Dim ip As String
    '    'Dim estr As EstruturaPadrao.EstruturaIdentificacaoUsuario
    '    Dim est As EstruturaPadrao.EstruturaIdentificacaoUsuario
    '    'Dim estUsuario As Object

    '    'Ex:     Session("SIGLA_DO_SISTEMA")
    '    'Esta sessão contem 2 classes :
    '    'estUsuario - Dados do usuário autenticado
    '    'estAutorizacoes - Autorizações do usuário autenticado.

    '    '5.2 - Sessão com o orgão que o usuário está  autenticado no sistema:
    '    'Ex:     Session("SIGLA_DO_SISTEMA_CODORGAO")

    '    'Try

    '    'EstAutorizacao
    '    'Description()
    '    'indAutorizado()
    '    'indAutorizadoCertDig()
    '    'janela()
    '    'objFunc()
    '    'siglaFunc()
    '    'urlCertDig()

    '    'EstOrgaoGrupoUsu
    '    'Description()
    '    'codGrupo()
    '    'codOrgao()
    '    'descrGrupo()
    '    'descrOrgao()
    '    'indAtivo()

    '    'estUsuario()
    '    'Description()
    '    'autorizacoes()
    '    'cargoUsu()
    '    'cod_Grupo_Usu()
    '    'codAdv()
    '    'codOrgaoLotado()
    '    'codUsu()
    '    'codUsuMumps()
    '    'CPF()
    '    'dtCadPres()
    '    'grupo_Usu()
    '    'idDef()
    '    'idFunc()
    '    'idPessoa()
    '    'idProm()
    '    'idUsu()
    '    'indAutenticaAD()
    '    'indCadPres()
    '    'indTrocaSenha()
    '    'matr_Formatada()
    '    'matr_Rh()
    '    'matricula()
    '    'nome()
    '    'orgaoLotacao()
    '    'origemUsuario()
    '    'supervisor()

    '    Try
    '        'estUsuario = CType(Session("PERICIAS"), EstruturaPadrao.EstruturaIdentificacaoUsuario)
    '        'estUsuario = New EstruturaPadrao.EstruturaIdentificacaoUsuario
    '        'estUsuario = Session("PERICIAS")
    '        est = New EstruturaPadrao.EstruturaIdentificacaoUsuario
    '        'est.CodOrg = Session("SIGLA_DO_SISTEMA_CODORGAO").CodOrgao
    '        est.Login = Session("PERICIAS").codusu
    '        est.LoginAutent = Session("PERICIAS").codusu
    '        est.Senha = ""  'Session("PERICIAS").Senha
    '        est.SenhaAutent = "" 'Session("PERICIAS").SenhaAutent
    '        est.SiglaSist = "PERICIAS"
    '        est.UsuarioSO = Session("PERICIAS").codusu 'Session("PERICIAS").SiglaFunc
    '        ID_Usuario = Session("PERICIAS").idusu
    '        Usuario = est.UsuarioSO
    '        ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR") ' por proxy
    '        If ip = "" Then
    '            ip = Request.ServerVariables("REMOTE_ADDR") 'original
    '        End If
    '        Session("ip") = ip

    '        est.NomeMaquina = NomeMaquina() '.NomeMaquina = ip ' Identificação da Máquina
    '        est.CodOrg = Session("PERICIAS_CODORGAO")

    '        Return est
    '    Catch ex As Exception
    '        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType, "msg", "alert('Atenção !!! \n\n " & ex.Message & "');", True)
    '    Finally
    '        'Return est
    '    End Try

    '    Return Nothing

    'End Function

    Public Function GetUsuario() As EstruturaPadrao.EstruturaIdentificacaoUsuario
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

    'Public Sub carregaInformacoesUsuario()
    '    Dim dsSeg As New System.Data.DataSet
    '    Dim estr As SegurancaWeb.ServicoCofre.EstUsuario
    '    'Dim getUrl As String

    '    'objUC = New UCInterfaceBLL.UCInterfaceBLL
    '    'objOrgao = New UCInterfaceBLL.EstORGAO

    '    'Dim est As EstruturaPadrao.EstruturaIdentificacaoUsuario

    '    'est = New EstruturaPadrao.EstruturaIdentificacaoUsuario
    '    ''est.CodOrg = Session("SIGLA_DO_SISTEMA_CODORGAO").CodOrgao
    '    'est.Login = Session("PERICIAS").codusu
    '    'est.LoginAutent = Session("PERICIAS").codusu
    '    'est.Senha = ""  'Session("PERICIAS").Senha
    '    'est.SenhaAutent = "" 'Session("PERICIAS").SenhaAutent
    '    'est.SiglaSist = "PERICIAS"
    '    'est.UsuarioSO = Session("PERICIAS").codusu 'Session("PERICIAS").SiglaFunc
    '    'ID_Usuario = Session("PERICIAS").idusu
    '    'Usuario = est.UsuarioSO

    '    estr = New SegurancaWeb.ServicoCofre.EstUsuario
    '    estr = Session("PERICIAS")
    '    Session("codOrgaoUsu") = Session("PERICIAS_CODORGAO")
    '    Session("Usuario") = estr.codUsu
    '    Session("CPFUsu") = estr.CPF
    '    Session("NomeUsuCPF") = estr.nome
    '    Session("NomeUsuMatr") = estr.matricula
    '    Session("UsuMatr") = estr.matricula
    '    Session("IdFunc") = estr.idFunc
    '    Session("Origem") = estr.origemUsuario
    '    Session("JANELAS") = estr.autorizacoes
    '    ''objUC.Url = App.AppSettings("UCWs.UCInterfaceBLL")
    '    'objUC.CookieContainer = New System.Net.CookieContainer
    '    'objUC.RegistrarUsuario(Session("Usuario"), "", "MaquinaUsu", "UsuarioSO", "", 0)
    '    'objOrgao = objUC.ConsultarOrgao(Session("codOrgaoUsu"))
    '    'Session("descOrgaoUsu") = objOrgao.NomeDtl
    '    'Session("orgaoAtivo") = objOrgao.IndAtivo


    'End Sub

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
    Public Sub MsgErro(ByVal Erro As String)

        ScriptManager.RegisterStartupScript(Me, Me.GetType, "erro", "alert('" & Erro & "');", True)

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
    'Public Function Confirma(ByVal Pergunta As String, ByVal m_Formulario As String) As Boolean
    '    Dim Parametros As String
    '    Session("Resposta") = ""
    '    Parametros = "?Perg=Pergunta&Form=" & m_Formulario
    '    Response.Redirect("frmConfirma.aspx" & Parametros)
    '    If Session("Resposta") = "S" Then
    '        Confirma = True
    '    Else
    '        Confirma = False
    '    End If
    '    Return Confirma
    'End Function
    'Function LocalizarNumCNJ(ByVal m_Num_Proc As String) As String
    '    Dim m_Num_CNJ As String
    '    Dim BalProc As New BalProcesso_Perito(GetUsuario)
    '    Dim DsCNJ As DataSet
    '    DsCNJ = BalProc.ExibirDados(m_Num_Proc)
    '    If DsCNJ Is Nothing Then
    '        MsgErro("Processo não localizado")
    '        Return ""
    '        Exit Function
    '    End If
    '    m_Num_CNJ = DsCNJ.Tables(0).Rows(0).Item(0)
    '    Return m_Num_CNJ

    'End Function
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
    'Function ValidarCPFServ(ByVal pCPF As String) As Boolean

    '    Dim c As String
    '    Dim dv As String
    '    c = Mid(pCPF, 0, 9)
    '    dv = Mid(pCPF, 9, 2)

    '    Dim d1 As Integer = 0

    '    For i = 1 To 9
    '        d1 = CInt(Mid(c, i, 1)) * (9 - i)
    '    Next
    '    If d1 = 0 Then
    '        Return False
    '    End If
    '    d1 = 11 - (d1 / 11)
    '    If (d1 > 9) Then d1 = 0
    '    If CInt(Mid(dv, 0, 1)) <> d1 Then
    '        Return False
    '    End If
    '    d1 = d1 * 2
    '    For i = 1 To 9
    '        d1 = d1 + CInt(Mid(c, i, 1)) * (10 - i)
    '    Next
    '    d1 = 11 - (d1 / 11)
    '    If (d1 > 9) Then d1 = 0
    '    If Mid(dv, 1, 1) <> d1 Then
    '        Return False
    '    End If
    '    Return True

    'Function ValidarCPFServ(ByVal p_CgcCpf As String) As Boolean
    '    ' Esta Rotina Devolverá True  Se o Cgc/Cpf Informado For valido
    '    '                    ou False Se o Cgc/Cpf Não For Correto
    '    ' Para Chamar esta Rotina de Consistência
    '    ' 1 ) Atribuir o valor do CgcCpf a uma Variavel String
    '    ' 2 ) Chamar a Rotina com : Consistir_CgcCpf (Variavel)
    '    ' Uma Forma Simples de fazer a Consistencia
    '    ' é Copiando as linhas abaixo (exemplo)
    '    ' para dentro do Programa
    '    ' Dim Vl_CgcCpf As String
    '    ' Vl_CgcCpf = Me.CgcCpf.Text
    '    ' If Consistir_CgcCpf(Vl_CgcCpf) = False then
    '    '  MsgBox "( Cgc/Cpf Informado Não é um Cgc/Cpf Correto )"
    '    '  Me.CgcCpf.SetFocus
    '    '  Exit Sub
    '    ' End if

    '    ValidarCPFServ = False
    '    Dim VA_CgcCpf As String
    '    Dim VA_Digito As String
    '    Dim Numero(15) As Integer
    '    Dim VA_Resto As Integer
    '    Dim VA_Resultado As Integer
    '    Dim VA_SomaDigito10 As Integer
    '    Dim VA_resto1 As Integer
    '    Dim TudoIgual As Boolean
    '    Dim i As Integer
    '    Dim aux As String

    '    p_CgcCpf = Trim(p_CgcCpf)
    '    'A rotina esta sendo usada somente para CPF
    '    If Len(p_CgcCpf) = 11 Then
    '        VA_CgcCpf = Mid(p_CgcCpf, 1, 3) + "." + Mid(p_CgcCpf, 4, 3) + "." + Mid(p_CgcCpf, 7, 3) + "-" + Mid(p_CgcCpf, 10, 2)
    '    Else
    '        'VA_CgcCpf = p_CgcCpf
    '        ValidarCPFServ = False
    '        Exit Function
    '    End If
    '    VA_Digito = Mid(VA_CgcCpf, 13, 2)

    '    'Numero(1) = Val(Mid(VA_CgcCpf, 1, 1))
    '    'Numero(2) = Val(Mid(VA_CgcCpf, 2, 1))
    '    'Numero(3) = Val(Mid(VA_CgcCpf, 3, 1))
    '    'Numero(4) = Val(Mid(VA_CgcCpf, 4, 1))
    '    'Numero(5) = Val(Mid(VA_CgcCpf, 5, 1))
    '    'Numero(6) = Val(Mid(VA_CgcCpf, 6, 1))
    '    'Numero(7) = Val(Mid(VA_CgcCpf, 7, 1))
    '    'Numero(8) = Val(Mid(VA_CgcCpf, 8, 1))
    '    'Numero(9) = Val(Mid(VA_CgcCpf, 9, 1))
    '    'Numero(10) = Val(Mid(VA_CgcCpf, 10, 1))
    '    'Numero(11) = Val(Mid(VA_CgcCpf, 11, 1))
    '    'Numero(12) = Val(Mid(VA_CgcCpf, 12, 1))
    '    'Numero(13) = Val(Mid(VA_CgcCpf, 13, 1))
    '    'Numero(14) = Val(Mid(VA_CgcCpf, 14, 1))
    '    Numero(0) = Val(Mid(VA_CgcCpf, 1, 1))
    '    Numero(1) = Val(Mid(VA_CgcCpf, 2, 1))
    '    Numero(2) = Val(Mid(VA_CgcCpf, 3, 1))
    '    Numero(3) = Val(Mid(VA_CgcCpf, 4, 1))
    '    Numero(4) = Val(Mid(VA_CgcCpf, 5, 1))
    '    Numero(5) = Val(Mid(VA_CgcCpf, 6, 1))
    '    Numero(6) = Val(Mid(VA_CgcCpf, 7, 1))
    '    Numero(7) = Val(Mid(VA_CgcCpf, 8, 1))
    '    Numero(8) = Val(Mid(VA_CgcCpf, 9, 1))
    '    Numero(9) = Val(Mid(VA_CgcCpf, 10, 1))
    '    Numero(10) = Val(Mid(VA_CgcCpf, 11, 1))
    '    Numero(11) = Val(Mid(VA_CgcCpf, 12, 1))
    '    Numero(12) = Val(Mid(VA_CgcCpf, 13, 1))
    '    Numero(13) = Val(Mid(VA_CgcCpf, 14, 1))

    '    TudoIgual = True
    '    aux = Trim(VA_CgcCpf)
    '    For i = 1 To Len(aux) - 1
    '        If Mid(aux, i, 1) <> Mid(aux, i + 1, 1) Then TudoIgual = False
    '    Next i

    '    If TudoIgual Then Exit Function

    '    If Len(Trim(p_CgcCpf)) > 11 Then  ' Cgc
    '        VA_Resultado = Numero(0) * 2
    '        If VA_Resultado > 9 Then
    '            VA_SomaDigito10 = VA_Resultado + 1
    '        Else
    '            VA_SomaDigito10 = VA_Resultado
    '        End If
    '        VA_Resultado = Numero(2) * 2
    '        If VA_Resultado > 9 Then
    '            VA_SomaDigito10 = VA_SomaDigito10 + VA_Resultado + 1
    '        Else
    '            VA_SomaDigito10 = VA_SomaDigito10 + VA_Resultado
    '        End If
    '        VA_Resultado = Numero(4) * 2
    '        If VA_Resultado > 9 Then
    '            VA_SomaDigito10 = VA_SomaDigito10 + VA_Resultado + 1
    '        Else
    '            VA_SomaDigito10 = VA_SomaDigito10 + VA_Resultado
    '        End If
    '        VA_Resultado = Numero(6) * 2
    '        If VA_Resultado > 9 Then
    '            VA_SomaDigito10 = VA_SomaDigito10 + VA_Resultado + 1
    '        Else
    '            VA_SomaDigito10 = VA_SomaDigito10 + VA_Resultado
    '        End If
    '        VA_SomaDigito10 = VA_SomaDigito10 + Numero(1) + Numero(3) + Numero(5)
    '        If Mid(Str(VA_SomaDigito10), Len(Str(VA_SomaDigito10)), 1) = "0" Then
    '            VA_Resto = 0
    '        Else
    '            VA_Resto = 10 - Val(Mid(Str(VA_SomaDigito10), Len(Str(VA_SomaDigito10)), 1))
    '        End If
    '        If VA_Resto <> Numero(7) Then
    '            Exit Function
    '        End If
    '        VA_Resultado = (Numero(0) * 5) + (Numero(1) * 4) _
    '            + (Numero(2) * 3) + (Numero(3) * 2) _
    '            + (Numero(4) * 9) + (Numero(5) * 8) + _
    '            (Numero(6) * 7) + (Numero(7) * 6) + _
    '            (Numero(8) * 5) + (Numero(9) * 4) + _
    '            (Numero(10) * 3) + (Numero(11) * 2)
    '        ' Atribui para resto o resto da divisão
    '        ' de VA_resultado dividido por 11
    '        VA_Resto = VA_Resultado Mod 11
    '        If VA_Resto < 2 Then
    '            VA_resto1 = 0
    '        Else
    '            VA_resto1 = 11 - VA_Resto
    '        End If
    '        If VA_resto1 <> Numero(12) Then
    '            Exit Function
    '        End If
    '        VA_Resultado = (Numero(0) * 6) + _
    '            (Numero(1) * 5) + (Numero(2) * 4) + _
    '            (Numero(3) * 3) + (Numero(4) * 2) + _
    '            (Numero(5) * 9) + (Numero(6) * 8) + _
    '            (Numero(7) * 7) + (Numero(8) * 6) + _
    '            (Numero(9) * 5) + (Numero(10) * 4) + _
    '            (Numero(11) * 3) + (Numero(12) * 2)
    '        ' Atribui para resto o resto da divisão
    '        ' de VA_resultado dividido por 11
    '        VA_Resto = VA_Resultado Mod 11
    '        If VA_Resto < 2 Then
    '            VA_resto1 = 0
    '        Else
    '            VA_resto1 = 11 - VA_Resto
    '        End If
    '        If VA_resto1 <> Numero(13) Then
    '            Exit Function
    '        End If
    '    Else  ' Cpf
    '        VA_Resultado = (Numero(3) * 1) + (Numero(4) * 2) + (Numero(5) * 3) + (Numero(6) * 4) + (Numero(7) * 5) + (Numero(8) * 6) + (Numero(9) * 7) + (Numero(10) * 8) + (Numero(11) * 9)
    '        VA_Resto = VA_Resultado Mod 11
    '        If VA_Resto > 9 Then
    '            VA_resto1 = VA_Resto - 10
    '        Else
    '            VA_resto1 = VA_Resto
    '        End If
    '        If VA_resto1 <> Numero(12) Then
    '            Exit Function
    '        End If

    '        VA_Resultado = (Numero(4) * 1) _
    '            + (Numero(5) * 2) + (Numero(6) * 3) _
    '            + (Numero(7) * 4) + (Numero(8) * 5) + _
    '            (Numero(9) * 6) + (Numero(10) * 7) + _
    '            (Numero(11) * 8) + (VA_Resto * 9)
    '        VA_Resto = VA_Resultado Mod 11
    '        If VA_Resto > 9 Then
    '            VA_resto1 = VA_Resto - 10
    '        Else
    '            VA_resto1 = VA_Resto
    '        End If
    '        If VA_resto1 <> Numero(13) Then
    '            Exit Function
    '        End If

    '    End If
    '    ValidarCPFServ = True

    'End Function

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
    Public Function ToBase64(ByVal data() As Byte) As String

        If data Is Nothing Then Throw New ArgumentNullException("data")

        Return Convert.ToBase64String(data)

    End Function

    Public Function FromBase64(ByVal base64 As String) As Byte()

        If base64 Is Nothing Then Throw New ArgumentNullException("base64")

        Return Convert.FromBase64String(base64)

    End Function

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(ImageFilePath) = True Then
            Throw New ArgumentNullException("O nome do arquivo de imagem não pode ser nulo ou vazio", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


End Class

