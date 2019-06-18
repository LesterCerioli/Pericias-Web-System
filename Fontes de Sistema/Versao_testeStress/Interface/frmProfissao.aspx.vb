Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmProfissao
    Inherits BasePage
    Public Shared DescrOld As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PreencherProfissao()
        End If
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALProfissao(GetUsuario)
        Dim ent As New EntProfissao
        Dim m_Cod_Profissao As String
        Dim n As Integer = 0
        If DescrOld = "" Then
            If IsNothing(CboProfissao.Items.FindByText(UCase(txtDescr_Profissao.Text))) Then
                m_Cod_Profissao = "0"
            Else
                m_Cod_Profissao = CboProfissao.Items.FindByText(UCase(txtDescr_Profissao.Text)).Value.ToString
            End If
        Else
            If IsNothing(CboProfissao.Items.FindByText(UCase(DescrOld))) Then
                m_Cod_Profissao = "0"
            Else
                m_Cod_Profissao = CboProfissao.Items.FindByText(UCase(DescrOld)).Value.ToString
            End If
        End If
        ent.Cod_Profissao = CInt(m_Cod_Profissao)
        ent.Descr_Profissao = txtDescr_Profissao.Text
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        'If m_Cod_Profissao <> "0" Then
        'MsgErro("Alteração Rejeitada. Profissao já existe")
        'Exit Sub
        'End If
        Bal.Gravar(ent)
        txtDescr_Profissao.Text = ""
        DescrOld = ""
        PreencherProfissao()

    End Sub
    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALProfissao(GetUsuario)
        Dim m_Cod_Profissao As String
        Dim Resultado As Boolean
        If IsPostBack Then
            'm_Cod_Profissao = CboProfissao.SelectedValue
            If IsNothing(CboProfissao.Items.FindByText(UCase(txtDescr_Profissao.Text))) Then
                m_Cod_Profissao = "0"
            Else
                m_Cod_Profissao = CboProfissao.Items.FindByText(UCase(txtDescr_Profissao.Text)).Value.ToString
            End If
            'Verificar se existe perito com esta Profissao(se tiver, não pode excluir)
            If m_Cod_Profissao = "" Or m_Cod_Profissao = "0" Then
                MsgErro("Exclusão não efetuada. Descrição Inválida")
                Exit Sub
            Else
                'MsgBox("Excluido com Sucesso")
                'Gravar somente a data exclusão sem excluir efetivamente
                Resultado = Bal.Excluir(CInt(m_Cod_Profissao))
                If Resultado Then
                    MsgBox("Excluído com Sucesso")
                    DescrOld = ""
                    txtDescr_Profissao.Text = ""
                End If
            End If
        End If
        PreencherProfissao()

    End Sub
    Private Sub PreencherProfissao()
        Dim bal As New BALProfissao(GetUsuario)
        Dim ent As New EntProfissao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()

        'Verificar para exibição dos não excluídos, ou seja, Data Exclusão nula

        CboProfissao.Items.Clear()
        CboProfissao.DataTextField = "Descr_Profissao"
        CboProfissao.DataValueField = "Cod_Profissao"
        CboProfissao.DataSource = dsfila.Tables(0).DefaultView
        CboProfissao.DataBind()
        CboProfissao.Items.Insert(0, "")
        CboProfissao.SelectedIndex = 0

    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALProfissao(GetUsuario)
        If Bal.ExistePerito_Profissao(CboProfissao.SelectedItem.Value) Then
            MsgErro("Alteração Rejeitada. Existe Perito com esta Profissao")
            txtDescr_Profissao.Text = ""
            DescrOld = ""
            Exit Sub
        End If
        txtDescr_Profissao.Text = CboProfissao.SelectedItem.Text
        DescrOld = txtDescr_Profissao.Text
    End Sub


End Class