Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmEspecialidade
    Inherits BasePage
    Public Shared DescrOld As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PreencherProfissao()
            'PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
        End If
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALEspecialidade(GetUsuario)
        Dim ent As New EntEspecialidade
        Dim m_Cod_Especialidade As String
        Dim m_Cod_Profissao As String
        Dim n As Integer = 0
        If DescrOld = "" Then
            If IsNothing(CboEspecialidade.Items.FindByText(UCase(txtDescr_Especialidade.Text))) Then
                m_Cod_Especialidade = "0"
            Else
                m_Cod_Especialidade = CboEspecialidade.Items.FindByText(UCase(txtDescr_Especialidade.Text)).Value.ToString
            End If
        Else
            If IsNothing(CboEspecialidade.Items.FindByText(UCase(DescrOld))) Then
                m_Cod_Especialidade = "0"
            Else
                m_Cod_Especialidade = CboEspecialidade.Items.FindByText(UCase(DescrOld)).Value.ToString
            End If
        End If
        'If IsNothing(CboProfissao.Items.FindByText(CboProfissao.Text)) Then
        m_Cod_Profissao = CboProfissao.Text
        'Else
        'm_Cod_Profissao = CboEspecialidade.Items.FindByText(CboProfissao.Text).Value.ToString
        'End If
        ent.Cod_Especialidade = CInt(m_Cod_Especialidade)
        ent.Descr_Especialidade = txtDescr_Especialidade.Text
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        ent.Cod_Profissao = CInt(m_Cod_Profissao)
        'If m_Cod_Especialidade <> "0" Then
        'MsgErro("Alteração Rejeitada. Especialidade já existe")
        'Exit Sub
        'End If
        Bal.Gravar(ent)
        txtDescr_Especialidade.Text = ""
        DescrOld = ""
        PreencherPROFISSAO()
        'PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
    End Sub
    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALEspecialidade(GetUsuario)
        Dim m_Cod_Especialidade As String
        Dim Resultado As Boolean
        If IsPostBack Then
            'm_Cod_Especialidade = CboEspecialidade.SelectedValue
            If IsNothing(CboEspecialidade.Items.FindByText(UCase(txtDescr_Especialidade.Text))) Then
                m_Cod_Especialidade = "0"
            Else
                m_Cod_Especialidade = CboEspecialidade.Items.FindByText(UCase(txtDescr_Especialidade.Text)).Value.ToString
            End If
            'Verificar se existe perito com esta especialidade(se tiver, não pode excluir)
            If m_Cod_Especialidade = "" Or m_Cod_Especialidade = "0" Then
                MsgErro("Exclusão não efetuada. Descrição Inválida")
                Exit Sub
            Else
                'MsgBox("Excluido com Sucesso")
                'Gravar somente a data exclusão sem excluir efetivamente
                Resultado = Bal.Excluir(CInt(m_Cod_Especialidade))
                If Resultado Then
                    MsgErro("Excluído com Sucesso")
                    DescrOld = ""
                    txtDescr_Especialidade.Text = ""
                End If
            End If
        End If
        PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))

    End Sub
    'Private Sub PreencherEspecialidade(ByVal nCod_Profissao As Integer)
    '    Dim bal As New BALEspecialidade(GetUsuario)
    '    Dim ent As New EntEspecialidade
    '    Dim dsfila As New DataSet
    '    dsfila = bal.ExibirDadosSet(nCod_Profissao)

    '    'Verificar para exibição dos não excluídos, ou seja, Data Exclusão nula

    '    CboEspecialidade.Items.Clear()
    '    CboEspecialidade.DataTextField = "Descr_Especialidade"
    '    CboEspecialidade.DataValueField = "Cod_Especialidade"
    '    CboEspecialidade.DataSource = dsfila.Tables(0).DefaultView
    '    CboEspecialidade.DataBind()
    '    CboEspecialidade.Items.Insert(0, "")
    '    CboEspecialidade.SelectedIndex = 0

    'End Sub

    Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALEspecialidade(GetUsuario)
        If Bal.ExistePerito_Especialidade(CboProfissao.SelectedItem.Value, CboEspecialidade.SelectedItem.Value) Then
            MsgErro("Alteração Rejeitada. Existe Perito com esta Especialidade")
            txtDescr_Especialidade.Text = ""
            DescrOld = ""
            Exit Sub
        End If
        txtDescr_Especialidade.Text = CboEspecialidade.SelectedItem.Text
        DescrOld = txtDescr_Especialidade.Text
    End Sub
    'Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

    '    Dim bal As New BALEspecialidade(GetUsuario)
    '    Dim ent As New EntEspecialidade
    '    Dim dsfila As New DataSet
    '    dsfila = bal.ExibirDadosSet(m_Cod_Profissao)
    '    CboEspecialidade.Items.Clear()
    '    CboEspecialidade.DataTextField = "Descr_Especialidade"
    '    CboEspecialidade.DataValueField = "Cod_Especialidade"
    '    CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
    '    CboEspecialidade.DataBind()
    '    CboEspecialidade.Items.Insert(0, "GENÉRICO")
    '    CboEspecialidade.SelectedIndex = 0

    'End Sub
    Private Sub PreencherEspecialidade(ByVal m_Cod_Profissao As Integer)

        Dim bal As New BALEspecialidade(GetUsuarioExt)
        Dim ent As New EntEspecialidade
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(m_Cod_Profissao)
        CboEspecialidade.Items.Clear()
        If dsfila.Tables(0).Rows.Count > 0 Then
            CboEspecialidade.Items.Clear()
            CboEspecialidade.DataTextField = "Descr_Especialidade"
            CboEspecialidade.DataValueField = "Cod_Especialidade"
            CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
            CboEspecialidade.DataBind()
        End If
        CboEspecialidade.Items.Insert(0, "GENÉRICO")
        CboEspecialidade.SelectedIndex = 0

    End Sub
    Private Sub PreencherPROFISSAO()
        Dim bal As New BALProfissao(GetUsuario)
        Dim ent As New EntProfissao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboProfissao.Items.Clear()
        CboProfissao.DataTextField = "Descr_PROFISSAO"
        CboProfissao.DataValueField = "Cod_PROFISSAO"
        CboProfissao.DataSource = dsfila.Tables(0) '.DefaultView
        CboProfissao.DataBind()
        CboProfissao.Items.Insert(0, "Selecione a PROFISSAO")
        CboProfissao.SelectedIndex = 0

        CboEspecialidade.Items.Clear()

    End Sub


    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
    End Sub
End Class