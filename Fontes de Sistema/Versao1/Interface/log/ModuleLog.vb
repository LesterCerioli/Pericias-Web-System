Imports System.Web
'Imports log4net

Namespace log

    Public Class ModuleLog
        Implements IHttpModule

        'Dim logger As log4net.ILog = log4net.LogManager.GetLogger(Me.GetType().ToString)

        Private WithEvents _context As HttpApplication

        ''' <summary>
        '''  You will need to configure this module in the web.config file of your
        '''  web and register it with IIS before being able to use it. For more information
        '''  see the following link: http://go.microsoft.com/?linkid=8101007
        ''' </summary>
#Region "IHttpModule Members"

        Public Sub Dispose() Implements IHttpModule.Dispose

            ' Clean-up code here

        End Sub

        Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
            ' _context = context
            'Dim h As New EventHandler(AddressOf OnBeginRequest)
            'context.AddOnBeginRequestAsync(h)
            ' AddHandler(context.BeginRequest,AddressOf Me.OnBeginRequest)
            AddHandler context.PreRequestHandlerExecute, AddressOf Me.OnBeginRequest
            'AddHandler context., AddressOf Me.OnBeginRequest

        End Sub

#End Region

        Public Sub OnBeginRequest(ByVal source As Object, ByVal e As EventArgs) 'Handles _context.BeginRequest

            Try
                Dim app As HttpApplication = CType(source, HttpApplication)
                Dim mdcLog As New log.ControladorMDCLog()
                mdcLog.marcarInicioDaTrilha(app)

                Dim l As New log.LogRequest(app)
                l.logRequest()
            Catch ex As Exception
                'logger.Error(ex.ToString(), ex)
            End Try

        End Sub
    End Class

End Namespace