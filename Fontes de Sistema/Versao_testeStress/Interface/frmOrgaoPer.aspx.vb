Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing


Partial Public Class frmOrgaoPer
    Inherits BasePage
    Public Shared DescrOld As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PreencherOrgaosPer()
            PreencherUF()
        End If
    End Sub

    Private Sub PreencherOrgaosPer()
        Dim balEsp As New BalOrgao_Per(GetUsuario)
        Dim ent As New EntOrgao_per
        Dim dsfila As New DataSet
        dsfila = balEsp.ExibirDadosSet()

        'Verificar para exibição dos não excluídos, ou seja, Data Exclusão nula

        CboOrgaoPer.Items.Clear()
        CboOrgaoPer.DataTextField = "Descr_Orgao_Per"
        CboOrgaoPer.DataValueField = "Cod_Orgao_Per"
        CboOrgaoPer.DataSource = dsfila.Tables(0).DefaultView
        CboOrgaoPer.DataBind()
        CboOrgaoPer.Items.Insert(0, "")
        CboOrgaoPer.SelectedIndex = 0

    End Sub
    Private Sub PreencherUF()

        Dim bal As New BALCIDADE(GetUsuario)
        Dim I As Integer

        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosUFSet()
        CboUf.Items.Clear()
        CboUf.Items.Insert(0, "Selecione a UF")
        I = 0
        For Each rs As DataRow In dsfila.Tables(0).Rows
            If Not IsDBNull(rs("SIGLA_UF")) Then
                I = I + 1
                CboUf.Items.Insert(I, rs("SIGLA_UF").ToString)
            End If
        Next
        CboUf.SelectedIndex = 21


    End Sub

    Protected Sub CboOrgaoPer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboOrgaoPer.SelectedIndexChanged
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BalOrgao_Per(GetUsuario)
        If Bal.ExistePerito_Orgao_Per(CboOrgaoPer.SelectedItem.Value) Then
            MsgErro("Alteração Rejeitada. Existe Perito com este Orgão Profissional")
            txtDescr_Orgao_Per.Text = ""
            DescrOld = ""
            Exit Sub
        End If
        txtDescr_Orgao_Per.Text = CboOrgaoPer.SelectedItem.Text
        DescrOld = txtDescr_Orgao_Per.Text
    End Sub
    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BalOrgao_Per(GetUsuario)
        Dim ent As New EntOrgao_per
        Dim m_Cod_OrgaosPer As String
        Dim n As Integer = 0
        If DescrOld = "" Then
            If IsNothing(CboOrgaoPer.Items.FindByText(UCase(txtDescr_Orgao_Per.Text))) Then
                m_Cod_OrgaosPer = "0"
            Else
                m_Cod_OrgaosPer = CboOrgaoPer.Items.FindByText(UCase(txtDescr_Orgao_Per.Text)).Value.ToString
            End If
        Else
            If IsNothing(CboOrgaoPer.Items.FindByText(UCase(DescrOld))) Then
                m_Cod_OrgaosPer = "0"
            Else
                m_Cod_OrgaosPer = CboOrgaoPer.Items.FindByText(UCase(DescrOld)).Value.ToString
            End If
        End If
        ent.COD_ORGAO_PER = CInt(m_Cod_OrgaosPer)
        ent.DESCR_ORGAO_PER = txtDescr_Orgao_Per.Text
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        ent.SIGLA_PER = txtSigla_Orgao_Per.Text
        ent.Uf = CboUf.Text
        'If m_Cod_Orgao_Per <> "0" Then
        'MsgErro("Alteração Rejeitada. OrgaosPer já existe")
        'Exit Sub
        'End If
        Bal.Gravar(ent)
        txtDescr_Orgao_Per.Text = ""
        DescrOld = ""
        PreencherOrgaosPer()

    End Sub
    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        Dim Bal As New BalOrgao_Per(GetUsuario)
        Dim m_Cod_OrgaosPer As String
        Dim Resultado As Boolean
        If IsPostBack Then
            'm_Cod_OrgaosPer = CboOrgaoPer.SelectedValue
            If IsNothing(CboOrgaoPer.Items.FindByText(UCase(txtDescr_Orgao_Per.Text))) Then
                m_Cod_OrgaosPer = "0"
            Else
                m_Cod_OrgaosPer = CboOrgaoPer.Items.FindByText(UCase(txtDescr_Orgao_Per.Text)).Value.ToString
            End If
            'Verificar se existe perito com esta OrgaosPer(se tiver, não pode excluir)
            If m_Cod_OrgaosPer = "" Or m_Cod_OrgaosPer = "0" Then
                MsgErro("Exclusão não efetuada. Descrição Inválida")
                Exit Sub
            Else
                'MsgBox("Excluido com Sucesso")
                'Gravar somente a data exclusão sem excluir efetivamente
                Resultado = Bal.Excluir(CInt(m_Cod_OrgaosPer))
                If Resultado Then
                    MsgBox("Excluído com Sucesso")
                    DescrOld = ""
                    txtDescr_Orgao_Per.Text = ""
                End If
            End If
        End If
        PreencherOrgaosPer()

    End Sub

    Protected Sub txtDescr_Orgao_Per_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtDescr_Orgao_Per.TextChanged

    End Sub

    Protected Sub CboUf_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboUf.SelectedIndexChanged

    End Sub
End Class