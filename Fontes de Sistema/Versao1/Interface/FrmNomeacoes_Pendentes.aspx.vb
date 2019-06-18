Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Web.UI.WebControls
Imports System.Data.DataRow
Imports DGTECGEDARDOTNET
''Imports log4net

Public Class FrmNomeacoes_Pendentes
    Inherits BasePage
    Dim mm_ID_PF As String
    Dim dsfilaper As New DataSet
    ''Dim logger As log4net.ILog

    Public DsNomPend As New DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CarregarGrid()
        End If
    End Sub

    Private Sub CarregarGrid()

        Dim Bal As New BalProcesso_Perito(GetUsuario)
        Dim m_Cod_Org As Integer = 0
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)

        ''logger = log4net.LogManager.GetLogger("LogInFile")

        'Inserir os peritos que tiveram aceitação ou recusa, sendo que se existe hon juiz, tem que ser o segundo movimento.
        ''logger.Debug("Bal.ExibirDadosSetNomPend(" & GetUsuario.CodOrg.ToString & ")")
        DsNomPend = Bal.ExibirDadosSetNomPend(GetUsuario.CodOrg)
        If Not DsNomPend Is Nothing Then
            GrdNomPend.DataSource = DsNomPend.Tables(0)
            GrdNomPend.DataBind()
        Else
            MsgErro("Não há pendências de peritos nomeados")
            Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "str", "window.opener.location.href('FrmPeritoDCP.aspx?idNomeacao=" & "" & "'); " & "window.close();", True)
        End If
    End Sub

    Protected Sub btnGrdNomPend_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)

        'Dim vetor(0) As String

        'Session("Num_CNJ") = GrdNomPend.SelectedRow.Cells(1).Text
        'Session("Descr_Profissao") = GrdNomPend.SelectedRow.Cells(3).Text
        'Session("Descr_Especialidade") = GrdNomPend.SelectedRow.Cells(4).Text
        'Session("ID_PF") = GrdNomPend.SelectedRow.Cells(5).Text
        'Session("Data_Nomeacao") = CDate(GrdNomPend.SelectedRow.Cells(6).Text).ToShortDateString

        'vetor(0) = e.CommandArgument.ToString().Split(CChar(","))(0)
        'txtID_Nomeacao.Text = vetor(0)
        'Dim idNomeacao As String = vetor(0)

        Dim idNomeacao As String
        idNomeacao = e.CommandArgument.ToString().Split(CChar(","))(0)
        Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "str", "window.opener.location.href('FrmPeritoDCP.aspx?idNomeacao=" & idNomeacao & "'); " & "window.close();", True)

    End Sub

End Class

