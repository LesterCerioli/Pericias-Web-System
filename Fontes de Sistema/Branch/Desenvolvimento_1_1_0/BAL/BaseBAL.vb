Imports ServicoDadosODPNET
'Imports ServicoDadosODBC
Imports App = System.Configuration.ConfigurationManager

Public Class BaseBAL

    Private strConexao As String

    Public sd As ServicoDadosOracle

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        'Instancia ServicoDados para o Oracle

        If sd Is Nothing Then
            sd = New ServicoDadosOracle(StringConexao)
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

            Dim sNomeServidor As String = UCase(Environment.MachineName)


            If Not sNomeServidor.Contains("TJERJ") Then
                sNomeServidor = "StringConexao"
            Else
                sNomeServidor = sNomeServidor + ".StringConexao"
            End If

            strConexao = App.AppSettings(sNomeServidor) + String.Empty

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

End Class
