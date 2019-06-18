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
        'Parametro => True, m_Email, p_Cc, p_From_Nome, p_From_Address, p_Subject, p_Html, p_Smtp_Hostname, p_Smtp_Portnum)
        'Parametros da Procedure
        ' p_To In Varchar2,
        '-- deve ser um e somente um endereço de email
        ' p_Cc In Varchar2,
        ' -- pode ser nulo ou vários endereços de email separados por ponto e vírgula
        ' p_From_Nome In Varchar2,
        ' -- Nome de quem está enviando o email
        ' p_From_Address In Varchar2,
        ' -- Endereço de quem está enviando o email
        ' p_Subject In Varchar2,
        ' -- Assunto do email
        ' p_Html In Clob Default Null,
        ' p_Html1 In Clob Default Null,
        ' p_Html2 In Clob Default Null,
        ' p_Html3 In Clob Default Null,
        ' p_Html4 In Clob Default Null,
        ' p_Html5 In Clob Default Null,
        ' p_Html6 In Clob Default Null,
        ' p_Html7 In Clob Default Null
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
            sd.Open()
            'True
            Parametros(0) = New ServicoDadosOracle.ParameterInfo("p_To", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(0).Valor = mm_Email
            Parametros(1) = New ServicoDadosOracle.ParameterInfo("p_CC", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(1).Valor = pp_Cc
            Parametros(2) = New ServicoDadosOracle.ParameterInfo("p_From_Nome", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(2).Valor = pp_From_Nome
            Parametros(3) = New ServicoDadosOracle.ParameterInfo("p_From_Address", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(3).Valor = pp_From_Address
            Parametros(4) = New ServicoDadosOracle.ParameterInfo("p_Subject", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(4).Valor = pp_Subject
            Parametros(5) = New ServicoDadosOracle.ParameterInfo("p_Html", OracleDbType.Clob, ParameterDirection.Input)
            Parametros(5).Valor = pp_Html
            Parametros(6) = New ServicoDadosOracle.ParameterInfo("p_Html1", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(6).Valor = pp_Html1
            Parametros(7) = New ServicoDadosOracle.ParameterInfo("p_Html2", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(7).Valor = pp_Html2
            Parametros(8) = New ServicoDadosOracle.ParameterInfo("p_Html3", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(8).Valor = pp_Html3
            Parametros(9) = New ServicoDadosOracle.ParameterInfo("p_Html4", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(9).Valor = pp_Html4
            Parametros(10) = New ServicoDadosOracle.ParameterInfo("p_Html5", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(10).Valor = pp_Html5
            Parametros(11) = New ServicoDadosOracle.ParameterInfo("p_Html6", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(11).Valor = pp_Html6
            Parametros(12) = New ServicoDadosOracle.ParameterInfo("p_Html7", OracleDbType.Varchar2, ParameterDirection.Input)
            Parametros(12).Valor = pp_Html7

            sd.ExecuteNonQuery("HtmlEmail", Parametros)

        Catch ex As ServicoDadosException
            'MsgErro("Erro no Envio!" + Chr(10) + ex.Message)
        Catch ex As ApplicationException
            'MsgErro("Erro no Envio!" + Chr(10) + ex.Message)
        Catch ex As Exception
            'MsgErro("Erro no Envio!" + Chr(10) + ex.Message)
        Finally
            'MsgErro("Enviado com Sucesso")
        End Try
        sd.Close()

    End Sub

End Class
