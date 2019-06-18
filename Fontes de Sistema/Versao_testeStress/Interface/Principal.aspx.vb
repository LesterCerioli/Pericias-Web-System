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

        If UsuarioAutorizado = "ID" Then
            lblPrincipal.Visible = True
            GrdNomeados.Visible = True
            bal.LimpaPreCadastro()
        Else
            lblPrincipal.Visible = False
            GrdNomeados.Visible = False
        End If
        ' Excluir fisicamente os dados de peritos cadastrados
        'que não apresentaram a documentação no prazo de 30 dias

    End Sub

    Protected Sub GrdNomeados_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdNomeados.SelectedIndexChanged
        Dim m_Num_Processo As String
        Dim m_Nome As String
        Dim m_URL As String
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        MsgErro("Aguardando Definição")
        Exit Sub
        m_Nome = GrdNomeados.SelectedRow.Cells(2).Text
        m_Num_Processo = GrdNomeados.SelectedRow.Cells(1).Text
        m_URL = "FrmOficioSubstituicao.aspx?Processo=" & m_Num_Processo & "&Nome=" & m_Nome
        'Response.Redirect("FrmOficioSubstituicao.aspx?Processo=" & m_Num_Processo & ",Nome = " & m_Nome)
        Response.Redirect(m_URL)
        'string URL = "Page2.aspx?FirstName=" + txtFirstName.Text + "&MiddleName=" + txtMiddleName.Text + "&LastName=" + txtLastName.LastName + "&DOB=" + Session["txtDOB"].ToString() + "&State=" + Request.Form.Get("hiddenState"); Response.Redirect(URL); 
    End Sub

End Class