Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET
Imports log4net

Partial Public Class frmEscolherPerito
    Inherits BasePage
    Dim mm_ID_PF As String
    Dim dsfilaper As New DataSet
    Dim logger As log4net.ILog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim m_Cod_Especialidade As Integer
        m_Cod_Especialidade = CInt(Session("Cod_Especialidade").ToString)
        Dim m_Cod_Profissao As Integer
        m_Cod_Profissao = CInt(Session("Cod_Profissao").ToString)
        Dim bPreenchePerito As Boolean

        logger = log4net.LogManager.GetLogger("LogInFile")
        logger.Debug("Acesso a escolha de perito ...")

        If m_Cod_Profissao = 0 Then
            MsgErro("Selecione uma Profissão")
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim m_ID_PF As String
            logger.Debug("Session(ID).ToString: " & Session("ID").ToString)
            m_ID_PF = Session("ID").ToString
        End If
        logger.Debug("PreencherPeritos(" & m_Cod_Profissao & "," & m_Cod_Especialidade & ")")
        bPreenchePerito = PreencherPeritos(m_Cod_Profissao, m_Cod_Especialidade)

        If Not bPreenchePerito Then
            logger.Debug("Redireciona para frmPeritoDCP.aspx")
            Response.Redirect("frmPeritoDCP.aspx")
        End If
        'Session("Escolha") = "S"
    End Sub

    Function PreencherPeritos(ByVal mm_Cod_Profissao As Integer, ByVal mm_Cod_Especialidade As Integer) As Boolean

        logger.Debug("PreencherPeritos ...")

        Dim bal As New BALPERITO(GetUsuario)
        Dim m_Cod_Orgao As Integer
        Dim m_Cod_Comarca As Integer
        Dim BalComarca As New BALUSUARIO(GetUsuario)
        Dim dsfila As New DataSet

        m_Cod_Orgao = CInt(Session("PERICIAS_CODORGAO"))

        logger.Debug("BalComarca.ExibirComarca(" & m_Cod_Orgao & ")")
        m_Cod_Comarca = BalComarca.ExibirComarca(m_Cod_Orgao)

        logger.Debug("bal.ExibirDadosPerDCP(" & mm_Cod_Profissao & "," & mm_Cod_Especialidade & "," & m_Cod_Comarca & ")")
        dsfila = bal.ExibirDadosPerDCP(mm_Cod_Profissao, mm_Cod_Especialidade, m_Cod_Comarca)
        If Not dsfila Is Nothing Then
            If dsfila.Tables(0).Rows.Count = 0 Then
                Session("Msg") = "Não existe perito cadastro para esta Comarca para esta especialidade"
                'MsgErro("Não existe perito cadastro para esta Comarca para esta especialidade")
                Return False
            Else
                GrdPeritos.DataSource = dsfila.Tables(0)
                GrdPeritos.DataBind()
                Return True
            End If
        Else
            Session("Msg") = "Não existe perito cadastro para esta Comarca para esta especialidade"
            'MsgErro("Não existe perito cadastro para esta Comarca para esta especialidade")
            Return False
        End If

    End Function

    Private Sub GrdPeritos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdPeritos.RowCommand

        logger.Debug("GrdPeritos_RowCommand ...")

        Dim Obj As DGTECGEDAR
        Dim IDDoc As String

        If e.CommandName = "Curriculum" Then
            'Dim Obj As DGTECGEDAR
            'Dim IDDoc As String
            Dim Bal As New BALPERITO(GetUsuario)
            Dim Ent As EntPERITO
            Dim m_URL As String = String.Empty
            Dim mm_ID_PF As Integer
            Dim indice As Integer

            'IDGED_CV
            indice = Convert.ToInt32(e.CommandArgument)
            mm_ID_PF = CInt(GrdPeritos.Rows(indice).Cells(3).Text)

            logger.Debug("Bal.ExibirDadosEnt(ID," & mm_ID_PF.ToString & ", N)")
            Ent = Bal.ExibirDadosEnt("ID", mm_ID_PF.ToString, "N")
            If Not Ent Is Nothing Then
                logger.Debug("Ent.IDGED_CV: " & Ent.IDGED_CV)
                IDDoc = Ent.IDGED_CV
            Else
                IDDoc = ""
            End If

            logger.Debug("IDDoc: " & IDDoc)
            If IDDoc <> "" Then
                Obj = New DGTECGEDAR
                Obj.Inicializa(GetUsuario.Login, "", GetUsuario.NomeMaquina, "PERICIAS", GetUsuario.UsuarioSO, GetUsuario.CodOrg.ToString, DGTECGEDAR.TipoServidorIndexacao.Homologacao2, DGTECGEDAR.TipoServidorWebService.Automatico, False)

                logger.Debug("m_URL: " & m_URL)
                If m_URL = "" Then
                    MsgErro("Não foi possível recuperar o currículo do perito.")
                    Exit Sub
                End If
                logger.Debug("Obj.MontaURLCacheWeb(" & IDDoc & ")")
                m_URL = Obj.MontaURLCacheWeb(IDDoc)
                Obj.Finaliza()
                Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('" & m_URL & "','_blank','resizable=yes,scrollbars=yes,status=yes');", True)
            Else
                MsgErro("Não há currículo para este perito.")
            End If
        ElseIf e.CommandName = "Selecionar" Then
            Dim indice As Integer

            logger.Debug("e.CommandArgument: " & e.CommandArgument.ToString)
            indice = Convert.ToInt32(e.CommandArgument)

            logger.Debug("GrdPeritos.Rows(indice).Cells(3).Text: " & GrdPeritos.Rows(indice).Cells(3).Text)
            txtID_PF.Text = GrdPeritos.Rows(indice).Cells(3).Text
            Session("ID_PF") = GrdPeritos.Rows(indice).Cells(3).Text

            If txtID_PF.Text <> "" Then
                Session("Escolha") = "S"
                logger.Debug("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
                Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
            Else
                MsgErro("Selecione um perito")
            End If
        ElseIf e.CommandName = "Foto" Then

            If GrdPeritos.Rows(Convert.ToInt32(e.CommandArgument)).Cells(3).Text = "" Or CInt(GrdPeritos.Rows(Convert.ToInt32(e.CommandArgument)).Cells(3).Text) = 0 Then
                MsgErro("Identificação Inválida")
                Exit Sub
            Else
                'Dim Obj As DGTECGEDAR
                Dim Bal As New BALPERITO(GetUsuario)
                Dim Ent As EntPERITO

                Ent = Bal.ExibirDadosEnt("ID", GrdPeritos.Rows(Convert.ToInt32(e.CommandArgument)).Cells(3).Text, "N")
                If Not Ent Is Nothing Then
                    If Ent.IDGED_Foto = "" Then
                        MsgErro("Para o perito selecionado não há foto.")
                    Else
                        Session("ID") = Ent.ID_PF.ToString
                        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "window.open('frmExibirFoto.aspx', '_blank', 'height=401,width=301, Top=150,left=120');", True)
                    End If
                Else
                    MsgErro("Para o perito selecionado não há foto.")
                End If
            End If
        End If

    End Sub

    Protected Sub GrdPeritos_PageIndexChanging(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)

        logger.Debug("GrdPeritos_PageIndexChanging ...")

        GrdPeritos.PageIndex = e.NewPageIndex
        PreencherPeritos(CInt(Session("Cod_Profissao").ToString), CInt(Session("Cod_Especialidade").ToString))
    End Sub

    Protected Sub txtID_PF_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtID_PF.TextChanged

        logger.Debug("txtID_PF_TextChanged ...")

        If txtID_PF.Text <> "" Then
            Session("ID_PF") = CInt(txtID_PF.Text)
            logger.Debug("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
            Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
        Else
            MsgErro("Selecione um perito")
        End If

    End Sub

    Protected Sub btnVoltar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVoltar.Click

        logger.Debug("btnVoltar_Click ...")

        If Not Session("ID_PF") Is Nothing Then
            Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & Session("ID_PF").ToString)
        Else
            Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & "")
        End If
        ' Response.Redirect("frmPeritoDCP.aspx?ID_PF=" & txtID_PF.Text)
    End Sub
End Class