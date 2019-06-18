Public Class BIGIP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write(Request.QueryString("CODBIGIP"))
    End Sub

End Class