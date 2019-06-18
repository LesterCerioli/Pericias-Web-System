Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET

Partial Public Class frmEscolherPerito
    Inherits BasePage
    Dim mm_ID_PF As String
    Dim dsfilaper As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim m_Cod_Especialidade As Integer
        m_Cod_Especialidade = CInt(Session("Cod_Especialidade").ToString)
        Dim m_Cod_Profissao As Integer
        m_Cod_Profissao = CInt(Session("Cod_Profissao").ToString)
        Session("Escolha") = "S"
        If m_Cod_Profissao = 0 Then
            MsgErro("Selecione uma Profissão")
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim m_ID_PF As String
            m_ID_PF = Session("ID").ToString
        End If
        dsfilaper = PreencherPeritos(m_Cod_Profissao, m_Cod_Especialidade)
        If Not dsfilaper Is Nothing Then
            GrdPeritos.DataSource = dsfilaper.Tables(0)
            GrdPeritos.DataBind()
        Else
            Response.Redirect("frmPeritoDCP.aspx")
        End If

    End Sub
    Function PreencherPeritos(ByVal mm_Cod_Profissao As Integer, ByVal mm_Cod_Especialidade As Integer) As DataSet

        Dim bal As New BALPERITO(GetUsuario)
        Dim m_Cod_Orgao As Integer
        Dim m_Cod_Comarca As Integer
        Dim BalComarca As New BALUSUARIO(GetUsuario)
        Dim dsfila As New DataSet

        m_Cod_Orgao = CInt(Session("PERICIAS_CODORGAO"))
        m_Cod_Comarca = BalComarca.ExibirComarca(m_Cod_Orgao)
        'Cod_Tip_Sit -> "A" -> UC.PessoaFisicaFuncao.Cod_Tip_Sit = 1 (Ativo)
        'P.Situacao_CADASTRO <> P
        dsfila = bal.ExibirDadosPerDCP(mm_Cod_Profissao, mm_Cod_Especialidade, m_Cod_Comarca)
        If dsfila.Tables(0).Rows.Count = 0 Then
            MsgErro("Não existe perito cadastro para esta Comarca para esta especialidade")
            Return Nothing
            Exit Function
        End If
        Return dsfila
    End Function

    Private Sub GrdPeritos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdPeritos.RowCommand

        If e.CommandName = "Curriculum" Then
            Dim Obj As DGTECGEDAR
            Dim IDDoc As String
            Dim Bal As New BALPERITO(GetUsuario)
            Dim Ent As EntPERITO
            'Dim f_ID_PF As String
            Dim m_URL As String
            Dim mm_ID_PF As Integer
            Dim indice As Integer

            'IDGED_CV
            'mm_ID_PF = CInt(GrdPeritos.SelectedRow.Cells(2).Text)
            indice = Convert.ToInt32(e.CommandArgument)
            'indice = GrdPeritos.Rows(e.CommandArgument)
            mm_ID_PF = CInt(GrdPeritos.Rows(indice).Cells(2).Text)
            Ent = Bal.ExibirDadosEnt("ID", mm_ID_PF.ToString, "N")
            If Not Ent Is Nothing Then
                IDDoc = Ent.IDGED_CV
            Else
                IDDoc = ""
            End If
            If IDDoc <> "" Then
                Obj = New DGTECGEDAR
                Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
                m_URL = Obj.MontaURLCacheWeb(IDDoc)
                Obj.Finaliza()
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes')", True)
            End If
            'ElseIf e.CommandName = "Selecionar" Then
            '    Dim indice As Integer
            '    indice = Convert.ToInt32(e.CommandArgument)
            '    Try
            '        txtID_PF.Text = GrdPeritos.Rows(indice).Cells(2).Text
            '    Catch ex As Exception
            '        Throw ex
            '    End Try

            '    If txtID_PF.Text <> "" Then
            '        Session("ID_PF") = CInt(txtID_PF.Text)
            '        Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
            '    Else
            '        MsgErro("Selecione um perito")
            '    End If
            '    Dim IDDoc As String
            '    Dim Bal As New BALPERITO(GetUsuario)
            '    Dim Ent As EntPERITO
            '    'Dim f_ID_PF As String
            '    Dim m_URL As String
            '    Dim mm_ID_PF As Integer
            '    'IDGED_CV
            '    'mm_ID_PF = CInt(GrdPeritos.SelectedRow.Cells(2).Text)
            '    indice = Convert.ToInt32(e.CommandArgument)
            '    'indice = GrdPeritos.Rows(e.CommandArgument)
            '    mm_ID_PF = CInt(GrdPeritos.Rows(indice).Cells(2).Text)
            '    Ent = Bal.ExibirDadosEnt("ID", mm_ID_PF.ToString)
            '    If Not Ent Is Nothing Then
            '        IDDoc = Ent.IDGED_CV
            '    Else
            '        IDDoc = ""
            '    End If
            '    If IDDoc <> "" Then
            '        Obj = New DGTECGEDAR
            '        Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
            '        m_URL = Obj.MontaURLCacheWeb(IDDoc)
            '        Obj.Finaliza()
            '        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes')", True)
            '    End If
        ElseIf e.CommandName = "Selecionar" Then
            Dim indice As Integer
            indice = Convert.ToInt32(e.CommandArgument)
            Try
                txtID_PF.Text = GrdPeritos.Rows(indice).Cells(2).Text
            Catch ex As Exception
                Throw ex
            End Try

            If txtID_PF.Text <> "" Then
                Session("ID_PF") = CInt(txtID_PF.Text)
                Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
            Else
                MsgErro("Selecione um perito")
            End If

        End If
    End Sub


    'Protected Sub BtnConfirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConfirmar.Click

    '    Dim mmm_ID_PF As String
    '    mmm_ID_PF = ""
    '    Try
    '        mm_ID_PF = GrdPeritos.SelectedRow.Cells(2).Text
    '        Session("ID") = mmm_ID_PF
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "window.close()", True)

    'End Sub

    ' '' ''Protected Sub GrdPeritos_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdPeritos.SelectedIndexChanged

    ' '' ''    'Dim mmm_ID_PF As String
    ' '' ''    'mmm_ID_PF = ""
    ' '' ''    Try
    ' '' ''        'mmm_ID_PF = GrdPeritos.SelectedRow.Cells(2).Text
    ' '' ''        'Session.Remove("ID")
    ' '' ''        'Session("ID") = mmm_ID_PF.ToString
    ' '' ''        txtID_PF.Text = GrdPeritos.SelectedRow.Cells(2).Text
    ' '' ''    Catch ex As Exception
    ' '' ''        Throw ex
    ' '' ''    End Try

    ' '' ''    If txtID_PF.Text <> "" Then
    ' '' ''        Session("ID_PF") = CInt(txtID_PF.Text)
    ' '' ''        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "closeMe('" + txtID_PF.Text + "')", True)
    ' '' ''        'm_Cod_Profissao = CInt(Session("Cod_Profissao"))
    ' '' ''        'm_Cod_Especialidade = CInt(Session("Cod_Especialidade"))
    ' '' ''        'txtNum_CNJ1.Text = Session("Num_Processo1").ToString
    ' '' ''        'txtNum_CNJ2.Text = Session("Num_Processo2").ToString
    ' '' ''        'Response.Redirect("frmPeritoDCP.aspx")
    ' '' ''        Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
    ' '' ''    Else
    ' '' ''        MsgErro("Selecione um perito")
    ' '' ''    End If

    ' '' ''    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "window.returnValue = window.document.all.txtNome_Perito.value", True)
    ' '' ''    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Dados", "window.returnValue = '" + mmm_ID_PF + "'", True)
    ' '' ''    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "window.close()", True)
    ' '' ''    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "closeMe('" + mmm_ID_PF + "')", True)
    ' '' ''    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "window.close('frmEscolherPerito.aspx')", True)
    ' '' ''    'Response.Write("<script language='javascript'> " + "window.returnValue='" + mmm_ID_PF + "';" + "window.close();" + "</script>")
    ' '' ''    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "closeMe('" + mmm_ID_PF + "')", True)

    ' '' ''End Sub
    'Private Sub PreencherPeritos(ByVal m_Cod_Profissao As Integer, ByVal m_Cod_Especialidade As Integer, ByVal Unico As Boolean)

    '    Dim bal As New BALPERITO(GetUsuario)
    '    Dim dsfila As New DataSet
    '    Dim m_Cod_Orgao As Integer
    '    Dim m_Cod_Comarca As Integer
    '    Dim BalComarca As New BALUSUARIO(GetUsuario)
    '    If m_Cod_Profissao = 0 Then
    '        MsgErro("Selecione uma Profissão")
    '        Exit Sub
    '    End If
    '    m_Cod_Orgao = CInt(Session("PERICIAS_CODORGAO"))
    '    m_Cod_Comarca = BalComarca.ExibirComarca(m_Cod_Orgao)
    '    'Cod_Tip_Sit -> "A" -> UC.PessoaFisicaFuncao.Cod_Tip_Sit = 1 (Ativo)
    '    'P.Situacao_CADASTRO <> P
    '    dsfila = bal.ExibirDadosPerDCP(m_Cod_Profissao, m_Cod_Especialidade, m_Cod_Comarca)
    '    If dsfila.Tables(0).Rows.Count = 0 Then 'dsfila Is Nothing Then
    '        MsgErro("Não existe perito cadastro para esta Comarca para esta especialidade")
    '        Exit Sub
    '    End If
    '    GrdPeritos.DataSource = dsfila.Tables(0)
    '    GrdPeritos.DataBind()
    'End Sub

    Protected Sub txtID_PF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtID_PF.TextChanged

        If txtID_PF.Text <> "" Then
            'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "closeMe('" + txtID_PF.Text + "')", True)
            Session("ID_PF") = CInt(txtID_PF.Text)
            'Response.Redirect("frmPeritoDCP.aspx")
            Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
        Else
            MsgErro("Selecione um perito")
        End If

    End Sub

    'Protected Sub BtnConfirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConfirmar.Click

    '    If txtID_PF.Text <> "" Then
    '        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "closeMe('" + txtID_PF.Text + "')", True)
    '    Else
    '        MsgErro("Selecione um perito")
    '    End If

    'End Sub

    'Protected Sub GrdPeritos_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdPeritos.SelectedIndexChanged
    '       If e.CommandName = "Curriculum" Then
    '        Dim Obj As DGTECGEDAR
    '        Dim IDDoc As String
    '        Dim Bal As New BALPERITO(GetUsuario)
    '        Dim Ent As EntPERITO
    '        'Dim f_ID_PF As String
    '        Dim m_URL As String
    '        Dim mm_ID_PF As Integer
    '        Dim indice As Integer

    '        'IDGED_CV
    '        'mm_ID_PF = CInt(GrdPeritos.SelectedRow.Cells(2).Text)
    '        indice = Convert.ToInt32(e.CommandArgument)
    '        'indice = GrdPeritos.Rows(e.CommandArgument)
    '        mm_ID_PF = CInt(GrdPeritos.Rows(indice).Cells(2).Text)
    '        Ent = Bal.ExibirDadosEnt("ID", mm_ID_PF.ToString)
    '        If Not Ent Is Nothing Then
    '            IDDoc = Ent.IDGED_CV
    '        Else
    '            IDDoc = ""
    '        End If
    '        If IDDoc <> "" Then
    '            Obj = New DGTECGEDAR
    '            Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)
    '            m_URL = Obj.MontaURLCacheWeb(IDDoc)
    '            Obj.Finaliza()
    '            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes')", True)
    '        End If

    '    ElseIf e.CommandName = "Selecionar" Then
    '        Dim indice As Integer
    '        indice = Convert.ToInt32(e.CommandArgument)
    '        Try
    '            txtID_PF.Text = GrdPeritos.Rows(indice).Cells(2).Text
    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '        If txtID_PF.Text <> "" Then
    '            Session("ID_PF") = CInt(txtID_PF.Text)
    '            Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
    '        Else
    '            MsgErro("Selecione um perito")
    '        End If

    '    End If
    'End Sub
End Class