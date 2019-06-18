Imports App = System.Configuration.ConfigurationManager

Partial Public Class erro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim sMsg As String = String.Empty

            If Not Session("erro") Is Nothing Then
                sMsg = Session("erro").ToString
            Else
                If Request.QueryString("msg") <> "" Then
                    sMsg = Request.QueryString("msg") & ""
                End If
            End If

            lblTitulo.Text = "Erro"
            lblMensagem.Text = App.AppSettings("m4")
            lblExcecao.Text = sMsg
            'lblExcecao.Text = Session("erro").ToString()
        Catch ex As Exception
            lblExcecao.Text = "Erro: " & ex.Message.ToString()
        End Try

    End Sub

    Protected Sub btnInicio_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInicio.Click
        Response.Redirect("principal.aspx")
    End Sub
End Class