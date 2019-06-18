Option Strict On
Imports BAL

Partial Public Class Principal

    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim bal As New BALPERITO(GetUsuario)
        'P - Perito A - Auxiliares S - Serventia
        'ID - Informática e DIPEJ
        Dim UsuarioAutorizado As String
        If Session("UsuarioAutorizado") Is Nothing Then
            UsuarioAutorizado = ""
        Else
            UsuarioAutorizado = Session("UsuarioAutorizado").ToString
        End If

        If UsuarioAutorizado = "ID" Or UsuarioAutorizado = "S" Or UsuarioAutorizado = "I" Then
            lblPrincipal.Visible = True
            GrdNomeados.Visible = True
            PreencheGrid()
            'FOS - Método LimpaPreCadastro comentado temporáriamente. Este método deverá ser ativado posteriormente
            'e criado a classificação da resolusão em que se base o cadastro do perito.
            'bal.LimpaPreCadastro()
        Else
            lblPrincipal.Visible = False
            GrdNomeados.Visible = False
        End If
        ' Excluir fisicamente os dados de peritos cadastrados
        'que não apresentaram a documentação no prazo de 30 dias

    End Sub

    Protected Sub GrdNomeados_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdNomeados.SelectedIndexChanged

        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim mm_URL As String
        mm_URL = "FrmOficioSubstituicao.aspx?Processo=" & GrdNomeados.SelectedRow.Cells(1).Text & "&Nome=" & GrdNomeados.SelectedRow.Cells(2).Text
        Response.Redirect(mm_URL)

    End Sub

    'Protected Sub btnSusbtituir_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
    '    Dim Vetor(1) As String
    '    For i As Integer = 0 To 2
    '        Vetor(i) = e.CommandArgument.ToString().Split(CChar(","))(i)
    '    Next
    '    Dim mm_URL As String
    '    mm_URL = "FrmOficioSubstituicao.aspx?Processo=" & Vetor(0) & "&Nome=" & Vetor(1)
    '    Response.Redirect(mm_URL)
    'End Sub

    Protected Sub GrdNomeado_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        GrdNomeados.PageIndex = e.NewPageIndex
        PreencheGrid()
    End Sub

    Private Sub PreencheGrid()
        Dim balProcessoPerito As New BalProcesso_Perito(GetUsuario)
        GrdNomeados.DataSource = balProcessoPerito.ListarPeritosNomeadosSemAceitacao
        GrdNomeados.DataBind()
    End Sub

End Class