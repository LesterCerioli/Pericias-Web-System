Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Partial Public Class frmComarcaPer
    Inherits BasePage
    Public DsNurPer As New DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim m_ID_PF As String = ""
            If Not Session("ID") Is Nothing Then m_ID_PF = Session("ID").ToString
            Dim nNur As Integer = 0
            nNur = CInt(Request.QueryString("n"))

            If nNur = 0 Then
                CarregarGrid()
            Else
                Session("Num_Nur") = nNur
                PreencherNur(nNur)
                CarregarGrid()
            End If
        End If
    End Sub

    Protected Sub BtnConfirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConfirmar.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim i As Integer
        Dim cb As CheckBox
        Dim m_Cod_Comarca As Integer
        Dim m_Nome As String
        Dim m_Marcado As Integer
        Dim GrdItem As GridViewRow
        Dim lbl_Cod_Comarca As Label
        Dim objNurPer As New DataSetPer
        Dim drNurPer As DataSetPer.TabNurPerRow
        'DataSet => Select 1 ou 0 Marcado, Nome, C.Cod_Com Cod_Comarca
        DsNurPer = CType(Session("DsNur"), DataSet)

        If Not DsNurPer Is Nothing Then
            If DsNurPer.Tables(0).Rows.Count > 0 Then
                For Each rs As DataRow In DsNurPer.Tables(0).Rows
                    m_Cod_Comarca = CInt(rs("Cod_Comarca"))
                    m_Nome = rs("Nome").ToString
                    m_Marcado = 0
                    i = 0
                    For Each d As GridViewRow In GrdComarcas.Rows
                        GrdItem = GrdComarcas.Rows(i)
                        lbl_Cod_Comarca = CType(GrdItem.FindControl("lbl_Cod_Comarca"), Label)
                        If CInt(lbl_Cod_Comarca.Text) = m_Cod_Comarca Then
                            cb = CType(GrdItem.FindControl("chk"), CheckBox)
                            If cb.Checked Then
                                m_Marcado = 1
                                Exit For
                            Else
                                Exit For
                            End If
                        End If
                        i = i + 1
                    Next
                    drNurPer = objNurPer.TabNurPer.NewTabNurPerRow
                    drNurPer.Marcado = m_Marcado
                    drNurPer.Nome = m_Nome
                    drNurPer.Cod_Comarca = m_Cod_Comarca
                    objNurPer.TabNurPer.Rows.Add(drNurPer)
                Next
                objNurPer.AcceptChanges()
                Session.Remove("DsNur")
                Session("DsNur") = objNurPer
            End If
        End If

        If CInt(Session("Num_Nur")) = 1 Then
            Session.Remove("DsNur1")
            Session("DsNur1") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 2 Then
            Session.Remove("DsNur2")
            Session("DsNur2") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 3 Then
            Session.Remove("DsNur3")
            Session("DsNur3") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 4 Then
            Session.Remove("DsNur4")
            Session("DsNur4") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 5 Then
            Session.Remove("DsNur5")
            Session("DsNur5") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 6 Then
            Session.Remove("DsNur6")
            Session("DsNur6") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 7 Then
            Session.Remove("DsNur7")
            Session("DsNur7") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 8 Then
            Session.Remove("DsNur8")
            Session("DsNur8") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 9 Then
            Session.Remove("DsNur9")
            Session("DsNur9") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 10 Then
            Session.Remove("DsNur10")
            Session("DsNur10") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 11 Then
            Session.Remove("DsNur11")
            Session("DsNur11") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 12 Then
            Session.Remove("DsNur12")
            Session("DsNur12") = CType(Session("DsNur"), DataSet)
        ElseIf CInt(Session("Num_Nur")) = 13 Then
            Session.Remove("DsNur13")
            Session("DsNur13") = CType(Session("DsNur"), DataSet)
        End If

        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "window.close()", True)

    End Sub

    Protected Sub BtnMarcar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnMarcar.Click
        Dim II As Integer
        Dim GrdItem As GridViewRow
        Dim cb As CheckBox
        II = 0
        For Each d As GridViewRow In GrdComarcas.Rows
            GrdItem = GrdComarcas.Rows(II)
            cb = CType(GrdItem.FindControl("chk"), CheckBox)
            cb.Checked = True
            II = II + 1
        Next
    End Sub

    Protected Sub BtnDesmarcar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnDesmarcar.Click
        Dim II As Integer
        Dim GrdItem As GridViewRow
        Dim cb As CheckBox
        II = 0
        For Each d As GridViewRow In GrdComarcas.Rows
            GrdItem = GrdComarcas.Rows(II)
            cb = CType(GrdItem.FindControl("chk"), CheckBox)
            cb.Checked = False
            II = II + 1
        Next
    End Sub

    Private Sub PreencherNur(ByVal nNur As Integer)
        Dim BalComarca As New BALCOMARCA(GetUsuarioExt)
        Dim ds As DataSet
        Session.Remove("DsNur")

        Select Case nNur
            Case 1
                If Not Session("DsNur1") Is Nothing Then Session("DsNur") = CType(Session("DsNur1"), DataSet)
            Case 2
                If Not Session("DsNur2") Is Nothing Then Session("DsNur") = Session("DsNur2")
            Case 3
                If Not Session("DsNur3") Is Nothing Then Session("DsNur") = Session("DsNur3")
            Case 4
                If Not Session("DsNur4") Is Nothing Then Session("DsNur") = Session("DsNur4")
            Case 5
                If Not Session("DsNur5") Is Nothing Then Session("DsNur") = Session("DsNur5")
            Case 6
                If Not Session("DsNur6") Is Nothing Then Session("DsNur") = Session("DsNur6")
            Case 7
                If Not Session("DsNur7") Is Nothing Then Session("DsNur") = Session("DsNur7")
            Case 8
                If Not Session("DsNur8") Is Nothing Then Session("DsNur") = Session("DsNur8")
            Case 9
                If Not Session("DsNur9") Is Nothing Then Session("DsNur") = Session("DsNur9")
            Case 10
                If Not Session("DsNur10") Is Nothing Then Session("DsNur") = Session("DsNur10")
            Case 11
                If Not Session("DsNur11") Is Nothing Then Session("DsNur") = Session("DsNur11")
            Case 12
                If Not Session("DsNur12") Is Nothing Then Session("DsNur") = Session("DsNur12")
            Case 13
                If Not Session("DsNur13") Is Nothing Then Session("DsNur") = Session("DsNur13")
        End Select

        ds = CType(Session("DsNur"), DataSet)
        If ds Is Nothing Then
            ds = BalComarca.ExibirDadosNurExt(nNur)
            Session("DsNur") = ds
        End If
    End Sub

    Private Sub CarregarGrid()
        If CType(Session("DsNur"), DataSet) Is Nothing Then
            Exit Sub
        End If

        DsNurPer = CType(Session("DsNur"), DataSet)
        If Not DsNurPer Is Nothing Then
            GrdComarcas.DataSource = DsNurPer.Tables(0)
            GrdComarcas.DataBind()
        End If
    End Sub

End Class

