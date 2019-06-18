Imports ServicoDadosODPNET
Imports System.Linq
Imports App = System.Configuration.ConfigurationManager

Public Class BaseBAL

    Private strConexao As String

    Public sd As ServicoDadosOracle

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        'Instancia ServicoDados para o Oracle

        If sd Is Nothing Then
            'sd = New ServicoDadosOracle(StringConexao, "", True, "JC7hzSXltYlnzsMo/b1R+ARu9c89li80,VYObDNc5pzI=")
            sd = New ServicoDadosOracle()
            sd.ConnectionString = StringConexao

        End If
        Usuario.LoginAutent = Usuario.Login
        sd.Usuario = Usuario

    End Sub

    'Public Sub New(ByVal sDados As ServicoDadosOracle, ByVal Nada As String)
    '    sd = sDados
    'End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        sd = sDados
    End Sub

    Protected ReadOnly Property StringConexao() As String
        Get
            'strConexao = App.AppSettings("StringConexao") + String.Empty
            'If strConexao = String.Empty Then
            '    'TODO: Colocar a string de conexão pro caso de não existir arquivo de configuração
            '    strConexao = "<String de Conexão>"
            'End If
            Dim dp As New DataProtection.OracleConnectionStringProtector

            Dim sNomeServidor As String = UCase(Environment.MachineName)


            'If Not sNomeServidor.Contains("TJERJ") Then
            sNomeServidor = "StringConexao"
            'Else
            'sNomeServidor = sNomeServidor + ".StringConexao"
            'End If

            strConexao = App.AppSettings(sNomeServidor) + String.Empty

            dp.SecureKey = "JC7hzSXltYlnzsMo/b1R+ARu9c89li80,VYObDNc5pzI="
            strConexao = dp.Decrypt(strConexao)

            'strConexao = "User Id=PERICIAS_DSV;Password=pericias#2010;Data Source=DSV01;"

            If strConexao = String.Empty Then
                'TODO: Colocar a string de conexão pro caso de não existir arquivo de configuração
                strConexao = "<String de Conexão>"
            End If

            Return strConexao
        End Get
    End Property

    Protected Function NovoCodigo(ByVal sNomeTabela As String, ByVal sNomeCampo As String, Optional ByVal sWhere As String = "") As Integer
        Dim sSQL As String
        sSQL = "select nvl(max(" + sNomeCampo + "),0)+1 from " + sNomeTabela + " " + sWhere
        Return CType(sd.ExecuteScalar(sSQL), Integer)
    End Function

    Public Function PesquisaCodigoProcesso(ByVal numeroProcesso As String, ByVal tipoFormatoProcesso As Integer, ByRef tipoProcesso As Integer) As Long
        sd.Open()
        Try
            Dim ds As DataSet = Nothing
            Dim codigoProcesso As Long = 0

            ds = sd.ExecutaProcDS("NOVO_PERICIAS.PesquisaCodigoProcesso", sd.CriaRefCursor, numeroProcesso, tipoFormatoProcesso)

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item(0) Is Nothing Then
                    codigoProcesso = 0
                    tipoProcesso = 3 'Significa que não foram achado dados
                Else
                    codigoProcesso = CLng(ds.Tables(0).Rows(0).Item(0))
                    tipoProcesso = CInt(ds.Tables(0).Rows(0).Item(1))
                End If
            End If

            Return codigoProcesso
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function PesquisaNumeroProcessoEprot(ByVal numeroProcesso As String) As Long
        sd.Open()
        Try
            Dim ds As DataSet = Nothing
            Dim codigoProcesso As Long = 0

            ds = sd.ExecutaProcDS("NOVO_PERICIAS.PesquisaNumeroProcessoEprot", sd.CriaRefCursor, numeroProcesso)

            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item(0) Is Nothing Then
                    codigoProcesso = 0
                Else
                    codigoProcesso = CLng(ds.Tables(0).Rows(0).Item(0))
                End If
            End If

            Return codigoProcesso
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function ListarProcessosAssociados(ByVal numeroProcesso As String) As DataSet
        sd.Open()
        Try
            Dim ds As DataSet = Nothing

            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ListarProcessosAssociados", sd.CriaRefCursor, numeroProcesso)

            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    ''' <summary>
    ''' Aplica máscara de processo no valor informado, utilizando expressão regular.
    ''' </summary>
    ''' <param name="valor">Valor para aplicação de máscara.</param>
    ''' <returns>Valor com máscara aplicada.</returns>
    Protected Function AplicarMascaraProcesso(valor As String) As String
        Dim retorno As String = String.Empty
        Dim expressaoRegular As String = String.Empty
        Dim substituicao As String = String.Empty
        Dim regex As Text.RegularExpressions.Regex = Nothing

        retorno = New String((From c As Char In valor Select c Where Char.IsLetterOrDigit(c)).ToArray)

        If retorno.Length <= 16 Then
            Select Case retorno.Length
                Case 14
                    expressaoRegular = "(\d{4})(\d{3})(\d{6})(\d{1})$"
                    substituicao = "$1.$2.$3-$4"
                Case 15
                    expressaoRegular = "(\d{4})(\d{3})(\d{6})(\d{1})(\w{1})$"
                    substituicao = "$1.$2.$3-$4$5"
                Case 12
                    expressaoRegular = "(\d{4})(\d{3})(\d{5})$"
                    substituicao = "$1.$2.$3"
                Case Else
                    expressaoRegular = "(\d{4})(\d{3})(\d{6})(\d{1})(\w{2})$"
                    substituicao = "$1.$2.$3-$4$5"
            End Select
        ElseIf retorno.Length = 20 Then
            expressaoRegular = "(\d{7})(\d{2})(\d{4})(\d{1})(\d{2})(\d{4})$"
            substituicao = "$1-$2.$3.$4.$5.$6"
        ElseIf retorno.Length = 19 Then
            expressaoRegular = "(\d{7})(\d{2})(\d{4})(\d{1})(\d{2})(\d{4})$"
            substituicao = "$1-$2.$3.$4.$5.$6"
        ElseIf retorno.Length > 20 Then
            expressaoRegular = "(\d{7})(\d{2})(\d{4})(\d{1})(\d{2})(\w{4,6})$"
            substituicao = "$1-$2.$3.$4.$5.$6"
        End If

        regex = New Text.RegularExpressions.Regex(expressaoRegular)
        retorno = regex.Replace(retorno, substituicao)

        Return retorno
    End Function
End Class
