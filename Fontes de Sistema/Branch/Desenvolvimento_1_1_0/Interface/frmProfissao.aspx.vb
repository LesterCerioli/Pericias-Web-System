Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmProfissao
    Inherits BasePage
    'Public Shared DescrOld As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PreencherProfissao()
        End If
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        
        Dim Bal As New BALProfissao(GetUsuario)
        Dim ent As New EntProfissao
        Dim m_Cod_Profissao As String
        Dim n As Integer = 0

        If txtDescr_Profissao.Text = "" Then
            MsgErro("Informe o nome da profissão.")
            Exit Sub
        End If

        If CboProfissao.SelectedIndex = 0 Then
            If Bal.ValidarNomeProfissao(txtDescr_Profissao.Text) Then
                MsgErro("Alteração Rejeitada. Profissão já existe. Clique no botão reativar se for necessário")
                BtnReativar.Visible = True
                Exit Sub
            End If
            m_Cod_Profissao = "0"
        Else
            m_Cod_Profissao = CboProfissao.SelectedValue
        End If

        ent.Cod_Profissao = CInt(m_Cod_Profissao)
        ent.Descr_Profissao = txtDescr_Profissao.Text
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        
        Bal.Gravar(ent)
        txtDescr_Profissao.Text = ""
        PreencherProfissao()
        MsgErro("Profissão gravada com sucesso.")

    End Sub

    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
     
        Dim Bal As New BALProfissao(GetUsuario)
        'Dim m_Cod_Profissao As String
        Dim Resultado As Boolean
        If IsPostBack Then

            If CboProfissao.SelectedValue = "" Then
                MsgErro("Selecione a profissão.")
                Exit Sub
            End If
            'Verificar se existe perito com esta Profissao(se tiver, não pode excluir)
            'If m_Cod_Profissao = "" Or m_Cod_Profissao = "0" Then
            '    MsgErro("Exclusão não efetuada. Descrição Inválida")
            '    Exit Sub
            'Else
            'Gravar somente a data exclusão sem excluir efetivamente
            Resultado = Bal.Excluir(CInt(CboProfissao.SelectedValue))
            If Resultado Then
                MsgErro("Excluído com Sucesso")
                LimpaTela()
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
        BtnReativar.Visible = False

    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALProfissao(GetUsuario)

        If CboProfissao.SelectedValue = "" Then
            LimpaTela()
            Exit Sub
        End If

        If Bal.ExistePerito_Profissao(CboProfissao.SelectedItem.Value) Then
            lblMsg.Text = "Há perito cadastrado com esta profissão."
            BtnGravar.Enabled = False
        Else
            lblMsg.Text = ""
            BtnGravar.Enabled = True
        End If

        txtDescr_Profissao.Text = CboProfissao.SelectedItem.Text
        BtnReativar.Visible = False

    End Sub

    Private Sub LimpaTela()
        lblMsg.Text = ""
        txtDescr_Profissao.Text = ""
        BtnExcluir.Enabled = True
        BtnGravar.Enabled = True
        BtnReativar.Visible = False
    End Sub

    Protected Sub BtnReativar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnReativar.Click

        Dim bal As New BALProfissao(GetUsuario)
        Dim Resultado As Boolean
        Resultado = bal.Reativar(txtDescr_Profissao.Text)
        If Resultado Then
            MsgErro("Excluído com Sucesso")
            LimpaTela()
        End If
        PreencherProfissao()

    End Sub
End Class