Imports ServicoDadosODPNET
'Imports ServicoDadosODBC
Imports App = System.Configuration.ConfigurationManager

Public Class BaseBAL

    Private strConexao As String

    Public sd As ServicoDadosOracle


    Public Sub New()
        'Instancia ServicoDados para o Oracle

        If sd Is Nothing Then
            sd = New ServicoDadosOracle(StringConexao, App.AppSettings.Item("SCHEMA").ToString, True, "/OVBrhkOwXMTyAqgffJ6ncx+EB/pSc0m,RaKhYKzMypE=")
        End If

        'Dados para teste no BIGIP, necessario para LOG de erro.
        Dim usu As New EstruturaPadrao.EstruturaIdentificacaoUsuario
        usu.Login = "DTE"
        usu.CodOrg = 3456
        usu.NomeMaquina = Environment.MachineName
        sd.Usuario = usu

    End Sub

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        'Instancia ServicoDados para o Oracle

        If sd Is Nothing Then
            sd = New ServicoDadosOracle(StringConexao, App.AppSettings.Item("SCHEMA").ToString, True, "/OVBrhkOwXMTyAqgffJ6ncx+EB/pSc0m,RaKhYKzMypE=")
        End If
        Usuario.LoginAutent = Usuario.Login
        sd.Usuario = Usuario

    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        sd = sDados
    End Sub

    Protected ReadOnly Property StringConexao() As String
        Get
            strConexao = App.AppSettings("StringConexao").ToString
            Return strConexao
        End Get
    End Property

    Protected Function NovoCodigo(ByVal sNomeTabela As String, ByVal sNomeCampo As String, Optional ByVal sWhere As String = "") As Integer
        Dim sSQL As String
        sSQL = "select nvl(max(" + sNomeCampo + "),0)+1 from " + sNomeTabela + " " + sWhere

        Try
            sd.Open()
            Return CType(sd.ExecuteScalar(sSQL), Integer)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function testeBIGIP(ByVal sCodigo As String) As String
            Return sCodigo
    End Function
End Class
