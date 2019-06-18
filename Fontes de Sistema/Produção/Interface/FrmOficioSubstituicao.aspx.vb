Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class FrmOficioSubstituicao
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LblProcesso.Text = Request.QueryString("Processo")
        LblNome.Text = Request.QueryString("Nome")

    End Sub

End Class