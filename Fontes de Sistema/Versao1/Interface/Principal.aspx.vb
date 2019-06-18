Option Strict On
Imports BAL
Imports App = System.Configuration

Partial Public Class Principal

    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim bal As New BALPERITO(GetUsuario)
        Dim balProcessoPerito As New BalProcesso_Perito(GetUsuario)

        If Not Page.IsPostBack Then
            'P - Perito A - Auxiliares S - Serventia
            'ID - Informática e DIPEJ
            Dim UsuarioAutorizado As String
            If Session("UsuarioAutorizado") Is Nothing Then
                UsuarioAutorizado = ""
            Else
                UsuarioAutorizado = Session("UsuarioAutorizado").ToString
            End If

            lblPrincipal.Visible = False
            GrdNomeados.Visible = False
            'If UsuarioAutorizado = "ID" Or UsuarioAutorizado = "S" Or UsuarioAutorizado = "I" Then
            'lblPrincipal.Visible = true
            'GrdNomeados.Visible = True
            'PreencheGrid()

            'Else
            'lblPrincipal.Visible = False
            'GrdNomeados.Visible = False
            ' End If

            'Exclui de forma lógica os pré-cadastro que estão a mais de 30 dias sem alteração da DIPEJ.
            bal.LimpaPreCadastro()

            'Atualiza o registro de nomeação para aceitação tácita caso não tenha data 
            'de aceitação ou negação cinco dias após a data da nomeação. 
            'FOS - o comentário da chamada de AtualizarProcessoPeritoNomeacaoTacita() deverá ser 
            'removido quando entrar em produção a nomeação do perito.
            balProcessoPerito.AtualizarProcessoPeritoNomeacaoTacita()
        End If



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