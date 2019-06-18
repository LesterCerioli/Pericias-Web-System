Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BalEmail

    Inherits BaseBAL


    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Sub EnviarEmail(ByVal mm_Email As String, ByVal pp_Cc As String, ByVal pp_From_Nome As String, ByVal pp_From_Address As String, ByVal pp_Subject As String, ByVal pp_HtmlTotal As String, ByVal pp_Smtp_Hostname As String, ByVal pp_Smtp_Portnum As String)
        Dim Parametros(12) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim pp_Html As String
        Dim pp_Html1 As String
        Dim pp_Html2 As String
        Dim pp_Html3 As String
        Dim pp_Html4 As String
        Dim pp_Html5 As String
        Dim pp_Html6 As String
        Dim pp_Html7 As String
        Dim Tam, Tam1 As Long

        Tam = 30000
        Tam1 = 2 * Tam
        pp_Html = Mid(pp_HtmlTotal, 1, Tam)
        pp_Html1 = Mid(pp_HtmlTotal, Tam + 1, Tam1)
        Tam1 = Tam1 + Tam
        pp_Html2 = Mid(pp_HtmlTotal, 2 * Tam + 1, Tam1)
        Tam1 = Tam1 + Tam
        pp_Html3 = Mid(pp_HtmlTotal, 3 * Tam + 1, Tam1)
        Tam1 = Tam1 + Tam
        pp_Html4 = Mid(pp_HtmlTotal, 4 * Tam + 1, Tam1)
        Tam1 = Tam1 + Tam
        pp_Html5 = Mid(pp_HtmlTotal, 5 * Tam + 1, Tam1)
        Tam1 = Tam1 + Tam
        pp_Html6 = Mid(pp_HtmlTotal, 6 * Tam + 1, Tam1)
        Tam1 = Tam1 + Tam
        pp_Html7 = Mid(pp_HtmlTotal, 7 * Tam + 1, Tam1)


        Try
            sd.ExecutaProc("HtmlEmail", mm_Email, pp_Cc, pp_From_Nome, pp_From_Address, pp_Subject, pp_Html, pp_Html1, pp_Html2, pp_Html3, pp_Html4, pp_Html5, pp_Html6, pp_Html7)
        Catch
        Finally
            sd.Close()
        End Try
    End Sub

End Class
