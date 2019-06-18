Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing


Partial Public Class frmOrgaoPer
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PreencherOrgaosPer()
        End If
    End Sub

    Private Sub PreencherOrgaosPer()
        Dim balEsp As New BalOrgao_Per(GetUsuario)
        Dim ent As New EntOrgao_per
        Dim dsfila As New DataSet

        dsfila = balEsp.ListarOrgNomeSigla()

        CboOrgaoPer.Items.Clear()
        CboOrgaoPer.DataTextField = "Descr_Orgao_Per"
        CboOrgaoPer.DataValueField = "Cod_Orgao_Per"
        CboOrgaoPer.DataSource = dsfila.Tables(0).DefaultView
        CboOrgaoPer.DataBind()
        CboOrgaoPer.Items.Insert(0, "")
        CboOrgaoPer.SelectedIndex = 0

    End Sub

    Protected Sub CboOrgaoPer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboOrgaoPer.SelectedIndexChanged
        
        Dim Bal As New BalOrgao_Per(GetUsuario)
        Dim ent As EntOrgao_per

        If CboOrgaoPer.SelectedIndex = 0 Then
            LimparTela()
            Exit Sub
        End If

        ent = Bal.ConsultarOrgProfissional(CboOrgaoPer.SelectedValue)

        If ent.COD_ORGAO_PER <> 0 Then
            txtSigla_Orgao_Per.Text = ent.SIGLA_PER
            txtDescr_Orgao_Per.Text = ent.DESCR_ORGAO_PER
        End If

        If Bal.ExistePerito_Orgao_Per(CboOrgaoPer.SelectedItem.Value) Then
            lblMsg.Text = "Há perito cadastrado com este órgão profissional."
            BtnExcluir.Enabled = False
            BtnGravar.Enabled = False
        Else
            BtnExcluir.Enabled = True
            BtnGravar.Enabled = True
            lblMsg.Text = ""
        End If

    End Sub

    Protected Sub BtnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click

        Dim Bal As New BalOrgao_Per(GetUsuario)
        Dim ent As New EntOrgao_per
        Dim m_Cod_OrgaosPer As Integer
        Dim n As Integer = 0
        Dim msg As String = ""

        If txtSigla_Orgao_Per.Text = "" Then
            MsgErro("Informe a Sigla.")
            Exit Sub
        End If

        If txtDescr_Orgao_Per.Text = "" Then
            MsgErro("Informe o nome do órgão profissional.")
            Exit Sub
        End If

        If CboOrgaoPer.SelectedIndex = 0 Then
            m_Cod_OrgaosPer = Bal.VerificaSeNomeSiglaDoOrgaoProfissionalExiste(txtDescr_Orgao_Per.Text, txtSigla_Orgao_Per.Text)
        Else
            m_Cod_OrgaosPer = CInt(CboOrgaoPer.SelectedValue)
        End If

        If (Not CboOrgaoPer.Items.FindByValue(CStr(m_Cod_OrgaosPer)) Is Nothing) And CboOrgaoPer.SelectedIndex = 0 Then
            MsgErro("Gravação rejeitada. Órgão profissional já existe.")
            Exit Sub
        End If

        ent.COD_ORGAO_PER = CInt(m_Cod_OrgaosPer)
        ent.DESCR_ORGAO_PER = txtDescr_Orgao_Per.Text
        ent.Data_Exclusao = CDate(System.Data.SqlTypes.SqlDateTime.Null)
        ent.SIGLA_PER = txtSigla_Orgao_Per.Text

        Bal.Gravar(ent)

        If CboOrgaoPer.Items.FindByValue(CStr(m_Cod_OrgaosPer)) Is Nothing Then
            msg = "Órgão profissional incluído com sucesso."
        Else
            msg = "Órgão profissional alterado com sucesso."
        End If

        txtDescr_Orgao_Per.Text = ""
        txtSigla_Orgao_Per.Text = ""
        PreencherOrgaosPer()
        MsgErro(msg)
    End Sub

    Protected Sub BtnExcluir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcluir.Click
       
        Dim Bal As New BalOrgao_Per(GetUsuario)
        Dim m_Cod_OrgaosPer As String
        Dim Resultado As Boolean

        If CboOrgaoPer.SelectedIndex = 0 Then
            MsgErro("Selecione o órgão profissional.")
            Exit Sub
        Else
            m_Cod_OrgaosPer = CboOrgaoPer.SelectedValue
            Resultado = Bal.Excluir(CInt(m_Cod_OrgaosPer))
            If Resultado Then
                MsgErro("Órgão profissional excluído com sucesso.")
                LimparTela()
            End If
        End If
        PreencherOrgaosPer()
    End Sub

    Private Sub LimparTela()
        lblMsg.Text = ""
        txtDescr_Orgao_Per.Text = ""
        txtSigla_Orgao_Per.Text = ""
        BtnExcluir.Enabled = True
        BtnGravar.Enabled = True
        CboOrgaoPer.SelectedIndex = 0
    End Sub
    
    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        LimparTela()
    End Sub
End Class