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
        Dim m_Cod_Profissao As Integer = 0
        Dim n As Integer = 0
        Dim msg As String = ""

        If txtDescr_Profissao.Text = "" Then
            MsgErro("Informe o nome da profissão.")
            Exit Sub
        End If

        If (Not CboProfissao.Items.FindByText(txtDescr_Profissao.Text) Is Nothing) And CboProfissao.SelectedIndex = 0 Then
            MsgErro("Gravação rejeitada. Profissão já existe.")
            Exit Sub
        End If

        If CboProfissao.SelectedIndex = 0 Then
            m_Cod_Profissao = Bal.VerificaSeNomeProfissaoExiste(txtDescr_Profissao.Text)
        Else
            m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        End If

        If (Not CboProfissao.Items.FindByValue(CStr(m_Cod_Profissao)) Is Nothing) And CboProfissao.SelectedIndex = 0 Then
            MsgErro("Gravação rejeitada. Profissão já existe.")
            Exit Sub
        End If

        ent.Cod_Profissao = CInt(m_Cod_Profissao)
        ent.Descr_Profissao = txtDescr_Profissao.Text
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        
        Bal.Gravar(ent)

        If CboProfissao.Items.FindByValue(CStr(m_Cod_Profissao)) Is Nothing Then
            msg = "Profissão incluída com sucesso."
        Else
            msg = "Profissão alterada com sucesso."
        End If
        txtDescr_Profissao.Text = ""
        PreencherProfissao()
        MsgErro(msg)
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

            If lblMsg.Text <> "" Then
                MsgErro("Exclusão não é permitida.")
                Exit Sub
            End If
           
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

    End Sub

    Protected Sub CboProfissao_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboProfissao.SelectedIndexChanged
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BALProfissao(GetUsuario)
        lblMsg.Text = ""

        If CboProfissao.SelectedValue = "" Then
            LimpaTela()
            Exit Sub
        End If

        If Bal.ExistePerito_Profissao(CboProfissao.SelectedItem.Value) Then
            lblMsg.Text = "Há perito cadastrado com esta profissão."
            BtnGravar.Enabled = False
            BtnExcluir.Enabled = False
        Else
            lblMsg.Text = ""
            BtnGravar.Enabled = True
            BtnExcluir.Enabled = True
        End If

        txtDescr_Profissao.Text = CboProfissao.SelectedItem.Text
    End Sub

    Private Sub LimpaTela()
        lblMsg.Text = ""
        txtDescr_Profissao.Text = ""
        BtnExcluir.Enabled = True
        BtnGravar.Enabled = True
        CboProfissao.SelectedIndex = 0
    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        LimpaTela()
    End Sub
End Class