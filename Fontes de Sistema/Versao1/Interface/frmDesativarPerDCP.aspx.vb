Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Partial Public Class frmDesativarPerDCP
    Inherits BasePage
    Public DsPerDCP As New DataSet()
    Public m_Nome As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'select NOME, COD_PERIT, Num_Reg  from Perito_Dcp where Sit_Oper <> 1
        If Not IsPostBack Then
            'Dim m_ID_PF As String = ""
            If Not Session("Nome") Is Nothing Then m_Nome = Session("Nome").ToString
            Dim nPer As Integer = 0
            nPer = CInt(Request.QueryString("n"))
            If nPer = 0 Then
                CarregarGrid()
            Else
                Session("Num_Per") = nPer
                'PreencherPer(nPer)
                PreencherPer(m_Nome.ToString)
                CarregarGrid()
            End If
        End If
    End Sub

    Protected Sub BtnConfirmar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConfirmar.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        'Dim i As Integer
        'Dim cb As CheckBox
        Dim m_Cod_Perito As Integer
        Dim m_Nome As String
        Dim m_Marcado As Integer
        Dim m_Num_Reg As Integer
        'Dim GrdItem As GridViewRow
        'Dim lbl_Cod_Comarca As Label
        Dim objPerDCP As New DataSetPer
        Dim dsPerDCP As DataSet
        Dim drDesPer As DataSetPer.TabDesPerRow

        'DataSet => Select 1 ou 0 Marcado, Nome, C.Cod_Com Cod_Comarca
        DsPerDCP = CType(Session("DsPer"), DataSet)
        If Not dsPerDCP Is Nothing Then
            If dsPerDCP.Tables(0).Rows.Count > 0 Then
                For Each rs As DataRow In dsPerDCP.Tables(0).Rows
                    m_Marcado = 0
                    m_Nome = rs("Nome").ToString
                    m_Cod_Perito = CInt(rs("Cod_Perit"))
                    m_Num_Reg = CInt(rs("Num_Reg"))
                    'i = 0
                    ' For Each d As GridViewRow In GrdDesativar.Rows
                    'GrdItem = GrdDesativar.Rows(i)
                    'lbl_Cod_Comarca = CType(GrdItem.FindControl("lbl_Cod_Comarca"), Label)
                    'If CInt(lbl_Cod_Comarca.Text) = m_Cod_Comarca Then
                    'cb = CType(GrdItem.FindControl("chk"), CheckBox)
                    'If cb.Checked Then
                    ' m_Marcado = 1
                    'Exit For
                    'Else
                    'Exit For
                    'End If
                    'End If
                    'i = i + 1
                    'Next
                    drDesPer = objPerDCP.TabDesPer.NewTabDesPerRow
                    drDesPer.Cod_Perito = m_Cod_Perito.ToString
                    drDesPer.Nome = m_Nome
                    drDesPer.Num_Reg = m_Num_Reg.ToString
                    objPerDCP.TabNurPer.Rows.Add(drDesPer)
                Next
                objPerDCP.AcceptChanges()
                Session.Remove("DsPer")
                Session("DsPer") = objPerDCP
            End If
        End If

        'Session.Remove("DsPer1")
        'Session("DsPer1") = CType(Session("DsPer"), DataSet)

        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "FechaJanela", "window.close()", True)

    End Sub

    Protected Sub BtnMarcar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnMarcar.Click
        Dim II As Integer
        Dim GrdItem As GridViewRow
        Dim cb As CheckBox
        II = 0
        For Each d As GridViewRow In GrdDesativar.Rows
            GrdItem = GrdDesativar.Rows(II)
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
        For Each d As GridViewRow In GrdDesativar.Rows
            GrdItem = GrdDesativar.Rows(II)
            cb = CType(GrdItem.FindControl("chk"), CheckBox)
            cb.Checked = False
            II = II + 1
        Next
    End Sub

    Private Sub PreencherPer(ByVal nNome As String)
        Dim BalPerito_Comarca As New BalPerito_Comarca(GetUsuarioExt)
        Session.Remove("DsDesPer")
        Dim dsDesPer As DataSet

        'If Not dsDesPer Is Nothing And txtNome.Text <> "" Then
        ''logger.Debug("BtnNur1_Click ...")
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        dsDesPer = BalPerito_Comarca.ExibirDadosSetDCP(Trim(UCase(Session("Nome").ToString)))
        Session("DsDesPer") = dsDesPer

    End Sub

    Private Sub CarregarGrid()
        If CType(Session("DsDesPer"), DataSet) Is Nothing Then
            Exit Sub
        End If

        DsPerDCP = CType(Session("DsPer"), DataSet)
        If Not DsPerDCP Is Nothing Then
            GrdDesativar.DataSource = DsPerDCP.Tables(0)
            GrdDesativar.DataBind()
        End If
    End Sub

End Class


