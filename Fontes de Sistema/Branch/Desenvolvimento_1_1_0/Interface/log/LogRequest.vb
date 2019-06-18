Imports System.Collections
Imports log4net
Imports log4net.Config
Imports System.Web.HttpRequest
Imports System.Web.HttpResponse

Imports System.Text

Namespace log
    Public Class LogRequest
        Private request As HttpRequest = Nothing
        Private response As HttpResponse = Nothing
        Private context As HttpApplication = Nothing

        Public Sub New(ByVal tmpContext As HttpApplication)
            context = tmpContext
            request = context.Request
            response = context.Response
        End Sub

        Public Sub logRequest()
            Dim logger As ILog = LogManager.GetLogger("LogInRequest")
            logger.Info("R;" & MensagemLog().ToString())

        End Sub

        Private Function MensagemLog() As StringBuilder

            Dim mensagem As New StringBuilder()
            mensagem.Append(ipUser())
            mensagem.Append(",")
            mensagem.Append(requestIdSession())
            mensagem.Append(",")
            mensagem.Append(requestUrl())
            mensagem.Append(",")
            mensagem.Append(requestHeaderParameters())
            mensagem.Append(",")
            mensagem.Append(requestParameters())
            Return mensagem
        End Function

        Private Function requestIdSession() As String
            Return "id sessão:" & context.Session.SessionID
        End Function

        Private Function requestUrl() As String
            Return "URL:" & request.FilePath & "CurrentExecutionFilePath:" & request.CurrentExecutionFilePath
        End Function

        Private Function requestHeaderParameters() As StringBuilder
            Dim mensagem As New StringBuilder("HEADERPARAMETERS:")
            Dim i, j As Integer

            ' Obtain a reference to the Request.Params
            ' collection.
            Dim pColl As NameValueCollection = request.Headers

            ' Iterate through the collection and add
            ' each key to the string variable.
            For i = 0 To pColl.Count - 1
                mensagem.Append(pColl.GetKey(i))

                ' Create a string array that contains
                ' the values associated with each key.
                Dim pValues() As String = pColl.GetValues(i)

                ' Iterate through the array and add
                ' each value to the string variable.
                For j = 0 To pValues.Length - 1
                    mensagem.Append(":" + pValues(j))
                Next j
                mensagem.Append(",")
            Next i
            Return mensagem


        End Function

        Private Function requestParameters() As StringBuilder
            Dim mensagem As New StringBuilder("PARAMETERS:")
            Dim i, j As Integer

            ' Obtain a reference to the Request.Params
            ' collection.
            Dim pColl As NameValueCollection = request.Params

            ' Iterate through the collection and add
            ' each key to the string variable.
            For i = 0 To pColl.Count - 1
                mensagem.Append(pColl.GetKey(i))

                ' Create a string array that contains
                ' the values associated with each key.
                Dim pValues() As String = pColl.GetValues(i)

                ' Iterate through the array and add
                ' each value to the string variable.
                For j = 0 To pValues.Length - 1
                    mensagem.Append(":" + pValues(j))
                Next j
                mensagem.Append(",")
            Next i
            Return mensagem
        End Function

        Private Function ipUser() As String
            Return "IP:" & request.UserHostAddress()
        End Function
    End Class

End Namespace