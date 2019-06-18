Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Partial Public Class frmComarcaPer
    Inherits BasePage
    Public DsNurPer As New DataSet()
    'Public Num_de_Comarcas As Integer
    'Public Num_NUR As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim m_ID_PF As String
            m_ID_PF = Session("ID").ToString
            'If m_ID_PF = "" Then m_ID_PF = "0"
            If Not CType(Session("DsNur"), DataSet) Is Nothing Then
                DsNurPer = CType(Session("DsNur"), DataSet)
                GrdComarcas.DataSource = DsNurPer.Tables(0)
                GrdComarcas.DataBind()
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
        Try
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
                Session.Remove("DsNur11")
                Session("DsNur11") = CType(Session("DsNur"), DataSet)
            ElseIf CInt(Session("Num_Nur")) = 13 Then
                Session.Remove("DsNur11")
                Session("DsNur11") = CType(Session("DsNur"), DataSet)
            End If

        Catch ex As Exception

            Throw ex

        End Try
        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "open('frmComarcaPer.aspx', '_blank', 'height=400,width=600, Top=150,left=120')", True)
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

End Class

