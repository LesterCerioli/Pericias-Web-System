﻿Option Strict Off

Imports BAL
Imports Entidade
Imports System.Drawing.Printing
Imports System.Linq

Partial Public Class frmEspecialidade
    Inherits BasePage
    'Public Shared DescrOld As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PreencherProfissao()
        End If
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        
        Dim Bal As New BALEspecialidade(GetUsuario)
        Dim ent As New EntEspecialidade
        Dim m_Cod_Especialidade As String
        Dim m_Cod_Profissao As String
        Dim n As Integer = 0

        If CboProfissao.SelectedIndex = 0 Then
            MsgErro("Selecione a profissão.")
            Exit Sub
        End If

        If txtDescr_Especialidade.Text = "" Then
            MsgErro("Informe a descrição da especialidade.")
            Exit Sub
        End If

        If CboEspecialidade.SelectedIndex = 0 Then
            If Bal.ValidarNomeEspecialidade(CStr(CboProfissao.Text), txtDescr_Especialidade.Text) Then
                MsgErro("Alteração Rejeitada. Especialidade já existe")
                Exit Sub
            End If
            m_Cod_Especialidade = "0"
        Else
            m_Cod_Especialidade = CboEspecialidade.SelectedValue
        End If
        

        m_Cod_Profissao = CboProfissao.Text
        ent.Cod_Especialidade = CInt(m_Cod_Especialidade)
        ent.Descr_Especialidade = txtDescr_Especialidade.Text
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        ent.Cod_Profissao = CInt(m_Cod_Profissao)

        Bal.Gravar(ent)
        LimparTela()
        PreencherPROFISSAO()
        MsgErro("Especialidade gravada com sucesso.", "Sucesso")

    End Sub

    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
        
        Dim Bal As New BALEspecialidade(GetUsuario)
        Dim Resultado As Boolean

        'Verificar se existe perito com esta especialidade(se tiver, não pode excluir)
        If CboProfissao.SelectedIndex = 0 Then
            MsgErro("Selecione a profissão.")
            Exit Sub
        End If

        If CboEspecialidade.SelectedIndex = 0 Then
            MsgErro("Selecione a especialidade.")
            Exit Sub
        End If

        If CboEspecialidade.SelectedValue = "" Then
            MsgErro("Exclusão não efetuada. Descrição Inválida")
            Exit Sub
        Else
            Resultado = Bal.Excluir(CInt(CboEspecialidade.SelectedValue))
            If Resultado Then
                MsgErro("Especialidade excluída com sucesso.", "Sucesso")
                LimparTela()
            End If
        End If

        'PreencherEspecialidade(CInt(CboProfissao.SelectedValue))
    End Sub

    Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged
   
        Dim Bal As New BALEspecialidade(GetUsuario)
        If CboEspecialidade.SelectedIndex = 0 Then
            'LimparTela()
            lblMsg.Text = ""
            BtnGravar.Enabled = True
            Exit Sub
        End If

        If Bal.ExistePerito_Especialidade(CboProfissao.SelectedItem.Value, CboEspecialidade.SelectedItem.Value) Then
            lblMsg.Text = "Há perito cadastrado com esta especialidade."
            txtDescr_Especialidade.Text = ""
            BtnGravar.Enabled = False
        Else
            BtnGravar.Enabled = True
            lblMsg.Text = ""
        End If

        txtDescr_Especialidade.Text = CboEspecialidade.SelectedItem.Text
        'DescrOld = txtDescr_Especialidade.Text
    End Sub
  
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
        CboEspecialidade.Items.Insert(0, "Selecione a especialidade.")
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
        CboProfissao.Items.Insert(0, "")
        CboProfissao.SelectedIndex = 0

        CboEspecialidade.Items.Clear()

    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged

        If CboProfissao.SelectedIndex = 0 Then
            LimparTela()
        Else
            txtDescr_Especialidade.Text = ""
            lblMsg.Text = ""
            PreencherEspecialidade(CInt(CboProfissao.SelectedValue))
        End If
    End Sub

    Private Sub LimparTela()
        lblMsg.Text = ""
        BtnExcluir.Enabled = True
        BtnGravar.Enabled = True
        txtDescr_Especialidade.Text = ""
        'CboEspecialidade.SelectedIndex = 0
        CboProfissao.SelectedIndex = 0
        CboEspecialidade.Items.Clear()
    End Sub

End Class