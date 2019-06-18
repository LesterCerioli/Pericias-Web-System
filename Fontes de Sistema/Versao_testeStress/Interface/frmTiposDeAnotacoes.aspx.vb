Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmTiposDeAnotacoes
    Inherits BasePage
    Public Shared DescrOld As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PreencherTipo_Anotacao()
        End If
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BalTipoAnotacao(GetUsuario)
        Dim ent As New EntTipo_Anotacao
        Dim m_Cod_Tipo_Anotacao As String
        Dim n As Integer = 0
        If DescrOld = "" Then
            If IsNothing(CboTipo_Anotacao.Items.FindByText(UCase(txtDescr_Tipo_Anotacao.Text))) Then
                m_Cod_Tipo_Anotacao = "0"
            Else
                m_Cod_Tipo_Anotacao = CboTipo_Anotacao.Items.FindByText(UCase(txtDescr_Tipo_Anotacao.Text)).Value.ToString
            End If
        Else
            If IsNothing(CboTipo_Anotacao.Items.FindByText(UCase(DescrOld))) Then
                m_Cod_Tipo_Anotacao = "0"
            Else
                m_Cod_Tipo_Anotacao = CboTipo_Anotacao.Items.FindByText(UCase(DescrOld)).Value.ToString
            End If
        End If
        ent.COD_TIPO_ANOTACAO = CInt(m_Cod_Tipo_Anotacao)
        ent.DESCR_TIPO_ANOTACAO = UCase(txtDescr_Tipo_Anotacao.Text)
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        'If m_Cod_Tipo_Anotacao <> "0" Then
        'MsgErro("Alteração Rejeitada. Tipo_Anotacao já existe")
        'Exit Sub
        'End If
        Bal.Gravar(ent)
        txtDescr_Tipo_Anotacao.Text = ""
        DescrOld = ""
        PreencherTipo_Anotacao()

    End Sub
    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BalTipoAnotacao(GetUsuario)
        Dim m_Cod_Tipo_Anotacao As String
        Dim Resultado As Boolean
        If IsPostBack Then
            'm_Cod_Tipo_Anotacao = CboTipo_Anotacao.SelectedValue
            If IsNothing(CboTipo_Anotacao.Items.FindByText(UCase(txtDescr_Tipo_Anotacao.Text))) Then
                m_Cod_Tipo_Anotacao = "0"
            Else
                m_Cod_Tipo_Anotacao = CboTipo_Anotacao.Items.FindByText(UCase(txtDescr_Tipo_Anotacao.Text)).Value.ToString
            End If
            'Verificar se existe perito com esta Tipo_Anotacao(se tiver, não pode excluir)
            If m_Cod_Tipo_Anotacao = "" Or m_Cod_Tipo_Anotacao = "0" Then
                MsgErro("Exclusão não efetuada. Descrição Inválida")
                Exit Sub
            Else
                'MsgBox("Excluido com Sucesso")
                'Gravar somente a data exclusão sem excluir efetivamente
                Resultado = Bal.Excluir(CInt(m_Cod_Tipo_Anotacao))
                If Resultado Then
                    MsgBox("Excluído com Sucesso")
                    DescrOld = ""
                    txtDescr_Tipo_Anotacao.Text = ""
                Else
                    MsgErro("Existe Anotação usando este Tipo. Exclusão não permitida")
                End If
            End If
        End If
        txtDescr_Tipo_Anotacao.Text = ""
        PreencherTipo_Anotacao()


    End Sub
    Private Sub PreencherTipo_Anotacao()
        Dim bal As New BalTipoAnotacao(GetUsuario)
        Dim ent As New EntTipo_Anotacao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()

        'Verificar para exibição dos não excluídos, ou seja, Data Exclusão nula
        If Not dsfila Is Nothing Then
            CboTipo_Anotacao.Items.Clear()
            CboTipo_Anotacao.DataTextField = "Descr_Tipo_Anotacao"
            CboTipo_Anotacao.DataValueField = "Cod_Tipo_Anotacao"
            CboTipo_Anotacao.DataSource = dsfila.Tables(0).DefaultView
            CboTipo_Anotacao.DataBind()
        End If
        CboTipo_Anotacao.Items.Insert(0, "")
        CboTipo_Anotacao.SelectedIndex = 0

    End Sub

    Protected Sub CboTipo_Anotacao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboTipo_Anotacao.SelectedIndexChanged
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BalTipoAnotacao(GetUsuario)
        'If Not Bal.ExisteAnotacao_Tipo_Anotacao(CboTipo_Anotacao.SelectedItem.Value) Then
        If Not Bal.ExisteTipo_Anotacao(CboTipo_Anotacao.SelectedItem.Value) Then
            MsgErro("Erro na Exibição Dados")
            txtDescr_Tipo_Anotacao.Text = ""
            DescrOld = ""
            Exit Sub
        End If
        txtDescr_Tipo_Anotacao.Text = CboTipo_Anotacao.SelectedItem.Text
        DescrOld = txtDescr_Tipo_Anotacao.Text
    End Sub


End Class
