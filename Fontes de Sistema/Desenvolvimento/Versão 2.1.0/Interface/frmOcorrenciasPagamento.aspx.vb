Imports Entidade

Public Class frmOcorrenciasPagamento
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CarregaOcorrencias()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CarregaOcorrencias()
        Try
            Dim Ocorrencias As DataTable = DirectCast(Session("OcorrenciasPagamento"), DataTable)

            lblProcesso.Text = String.Format("Processo: {0}", CStr(Ocorrencias(0).Item("PROCESSO")))

            grdAjudaCustoPerito.DataSource = Ocorrencias
            grdAjudaCustoPerito.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class