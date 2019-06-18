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
        BtnExcluir.Enabled = False
    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
       
        Dim Bal As New BalTipoAnotacao(GetUsuario)
        Dim ent As New EntTipo_Anotacao
        Dim m_Cod_Tipo_Anotacao As Integer
        Dim n As Integer = 0
        Dim msg As String = String.Empty

        If txtDescr_Tipo_Anotacao.Text = "" Then
            MsgErro("Informe a descrição do tipo da anotação.")
            txtDescr_Tipo_Anotacao.Focus()
            Exit Sub
        End If

        If CboTipo_Anotacao.SelectedIndex = 0 Then
            m_Cod_Tipo_Anotacao = Bal.VerificaSeNomeDoTipoAnotacaoExiste(txtDescr_Tipo_Anotacao.Text)
        Else
            m_Cod_Tipo_Anotacao = CInt(CboTipo_Anotacao.SelectedValue)
        End If

        If (Not CboTipo_Anotacao.Items.FindByValue(CStr(m_Cod_Tipo_Anotacao)) Is Nothing) And CboTipo_Anotacao.SelectedIndex = 0 Then
            MsgErro("Gravação rejeitada. Tipo de anotação já existe.")
            Exit Sub
        End If

        ent.COD_TIPO_ANOTACAO = CInt(m_Cod_Tipo_Anotacao)
        ent.DESCR_TIPO_ANOTACAO = UCase(txtDescr_Tipo_Anotacao.Text)
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)

        If CboTipo_Anotacao.Items.FindByValue(CStr(m_Cod_Tipo_Anotacao)) Is Nothing Then
            msg = "Tipo de anotação incluído com sucesso."
        Else
            msg = "Tipo de anotação alterado com sucesso."
        End If

        Bal.Gravar(ent)
        txtDescr_Tipo_Anotacao.Text = ""
        PreencherTipo_Anotacao()
        BtnExcluir.Enabled = False
        MsgErro(msg)

    End Sub

    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim Bal As New BalTipoAnotacao(GetUsuario)
        Dim Resultado As Boolean

        If CboTipo_Anotacao.SelectedIndex = 0 Then
            MsgErro("Selecione o tipo de anotação a ser excluída.")
            Exit Sub
        Else
            Resultado = Bal.Excluir(CInt(CboTipo_Anotacao.SelectedValue))
            If Resultado Then
                MsgErro("Tipo de anotação excluído com sucesso.")
                LimpaTela()
            Else
                MsgErro("Existe Anotação usando este Tipo. Exclusão não permitida")
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
    
        Dim Bal As New BalTipoAnotacao(GetUsuario)

        If CboTipo_Anotacao.SelectedIndex = 0 Then
            LimpaTela()
            Exit Sub
        End If

        If Bal.ExisteAnotacao_Tipo_Anotacao(CboTipo_Anotacao.SelectedItem.Value) Then
            lblMsg.Text = "Há registro de anotação para este tipo."
            BtnGravar.Enabled = False
            BtnExcluir.Enabled = False
        Else
            BtnGravar.Enabled = True
            BtnExcluir.Enabled = True
            lblMsg.Text = ""
        End If
        txtDescr_Tipo_Anotacao.Text = CboTipo_Anotacao.SelectedItem.Text

    End Sub

    Private Sub LimpaTela()
        txtDescr_Tipo_Anotacao.Text = ""
        lblMsg.Text = ""
        'BtnExcluir.Enabled = True
        BtnGravar.Enabled = True
        CboTipo_Anotacao.SelectedIndex = 0
    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        LimpaTela()
    End Sub
End Class
