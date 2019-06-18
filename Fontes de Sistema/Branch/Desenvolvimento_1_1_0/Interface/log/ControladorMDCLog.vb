Imports Microsoft.VisualBasic
Imports MDC = log4net.MDC
Imports System.Web.HttpRequest
Imports System.Web.SessionState.HttpSessionState

Namespace log

    Public Class ControladorMDCLog
        Private Const VARIAVEL_IDENTIFICADOR_TRILHA As String = "VARIAVEL_IDENTIFICADOR_TRILHA"

        Public Sub New()

        End Sub

        Public Overridable Sub marcarInicioDaTrilha(ByVal context As HttpApplication)
            Dim session As HttpSessionState = context.Session
            MDC.Set("sessionId", session.SessionID)
            MDC.Set("trilha", gerarNovaTrilha(session))
        End Sub

        Public Overridable Sub marcarFimDaTrilha()
            MDC.Remove("sessionId")
            MDC.Remove("trilha")
        End Sub

        Private Function gerarNovaTrilha(ByVal session As HttpSessionState) As Integer?
            Dim oTrilha As Object = session("VARIAVEL_IDENTIFICADOR_TRILHA")
            Dim trilha As Integer = 0

            If Not oTrilha Is Nothing Then
                trilha = CInt(oTrilha)
                trilha += 1
            End If
            session("VARIAVEL_IDENTIFICADOR_TRILHA") = trilha

            Return trilha
        End Function


    End Class

End Namespace