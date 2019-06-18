Public Class BIGIP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.ClearContent()
        Response.ClearHeaders()
        Dim ws As New BAL.BaseBAL()
        Dim Msg As String = ws.TesteBIGIP(Request.QueryString("CODBIGIP"))
        Response.Write("<html><head><title>" & Environment.MachineName.ToUpper & "</title></head><body>" & Msg & "</body></html>")

    End Sub

End Class