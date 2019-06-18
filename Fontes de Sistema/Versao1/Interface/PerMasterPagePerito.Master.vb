Partial Public Class PerMasterPagePerito
    Inherits System.Web.UI.MasterPage

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            Nomeacao.Visible = (Session("EDITA_Nomeacao") = "1")
            Auxiliares.Visible = (Session("EDITA_Auxiliares") = "1")
            Perito.Visible = (Session("EDITA_Perito") = "1")
            DIPEJ.Visible = (Session("EDITA_DIPEJ") = "1")
            lblAmbiente.Text = Session("AmbienteServidor")
            Serventia.Visible = (Session("EDITA_SERVENTIA") = "1")

        End If
    End Sub

End Class