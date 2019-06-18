Public Partial Class PerMasterPagePerito
    Inherits System.Web.UI.MasterPage


    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Nomeacao.Visible = (Session("EDITA_Nomeacao") = "1")
        Auxiliares.Visible = (Session("EDITA_Auxiliares") = "1")
        Perito.Visible = (Session("EDITA_Perito") = "1")
        DIPEJ.Visible = (Session("EDITA_DIPEJ") = "1")

    End Sub
End Class