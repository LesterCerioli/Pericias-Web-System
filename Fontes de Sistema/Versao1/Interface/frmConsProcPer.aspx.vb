Option Strict On

Imports BAL
Imports Entidade
Imports System.Drawing.Printing

Partial Public Class frmConsProcPer
    Inherits BasePage

    Private Sub frmPeritoDCP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Temporizador", "window.setTimeout(location.reload(),200000))", True)
        If Not IsPostBack Then
            txtNum_CNJ.Attributes.Add("onblur", "FormatarProcCNJ(ctl00$Tela$txtNum_CNJ.value);")
            txtNum_CNJ.Attributes.Add("onblur", "ValidarProcCNJ(ctl00$Tela$txtNum_CNJ.value);")
            txtNum_Processo.Attributes.Add("onblur", "FormatarProc(ctl00$Tela$txtNum_Processo.value);")
            txtNum_Processo.Attributes.Add("onblur", "ValidarProc(ctl00$Tela$txtNum_Processo.value);")
            If Not Me.IsPostBack Then
                PreencherProfissao()
                PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
            End If
        End If

    End Sub

    Private Sub PreencherEspecialidade(ByVal nCod_Profissao As Integer)

        Dim bal As New BALEspecialidade(GetUsuario)
        Dim ent As New EntEspecialidade
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet(nCod_Profissao)
        CboEspecialidade.Items.Clear()
        CboEspecialidade.DataTextField = "Descr_Especialidade"
        CboEspecialidade.DataValueField = "Cod_Especialidade"
        CboEspecialidade.DataSource = dsfila.Tables(0) '.DefaultView
        CboEspecialidade.DataBind()
        'CboEspecialidade.Items.Insert(0, "Selecione a Especialidade")
        'CboEspecialidade.SelectedIndex = 0

    End Sub
    Private Sub PreencherProfissao()

        Dim bal As New BALProfissao(GetUsuario)
        Dim ent As New EntProfissao
        Dim dsfila As New DataSet
        dsfila = bal.ExibirDadosSet()
        CboProfissao.Items.Clear()
        CboProfissao.DataTextField = "Descr_Profissao"
        CboProfissao.DataValueField = "Cod_Profissao"
        CboProfissao.DataSource = dsfila.Tables(0) '.DefaultView
        CboProfissao.DataBind()


    End Sub
    Private Sub PreencherPeritos(ByVal m_Cod_Profissao As Integer, ByVal m_Cod_Especialidade As Integer)
        Dim m_Cod_Comarca As Integer
        If Not Me.IsPostBack Then
            Exit Sub
        End If
        txtAnotacao.Text = ""
        LblAnotacao.Visible = False
        txtAnotacao.Visible = False
        lblData_Cadastramento.Text = ""
        lblData_Liberacao.Text = ""
        LblAnotacao.Text = ""
        lblEmail.Text = ""
        lblEmail1.Text = ""
        txtQteAceitos.Text = ""
        txtQtePendentes.Text = ""
        Dim bal As New BALPERITO(GetUsuario)
        Dim dsfila As New DataSet

        CboPerito.Items.Clear()
        'Provisório - Teste (Comarca da senha do usuário) define o Cod_Comarca
        m_Cod_Comarca = 406
        'Cod_Tip_Sit -> "A"
        'P.Situacao_CADASTRO <> "P"
        dsfila = bal.ExibirDadosPerDCP(m_Cod_Profissao, m_Cod_Especialidade, m_Cod_Comarca)
        If dsfila.Tables(0).Rows.Count = 0 Then 'dsfila Is Nothing Then
            MsgErro("Não existe perito cadastro para esta Comarca")
            Exit Sub
        End If
        CboPerito.DataTextField = "Nome"
        CboPerito.DataValueField = "ID_PF"
        CboPerito.DataSource = dsfila.Tables(0) '.DefaultView
        CboPerito.DataBind()
        'CboPerito.Items.Insert(0, "Selecione um perito")
        'CboPerito.SelectedIndex = 0

    End Sub

    Protected Sub CboEspecialidade_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboEspecialidade.SelectedIndexChanged

        Dim m_Cod_Especialidade As Integer
        Dim m_Cod_Profissao As Integer
        m_Cod_Especialidade = CInt(CboEspecialidade.SelectedValue)
        m_Cod_Profissao = CInt(CboProfissao.SelectedValue)
        PreencherPeritos(m_Cod_Profissao, m_Cod_Especialidade)

    End Sub

    Protected Sub CboPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CboPerito.SelectedIndexChanged

        ExibirDadosPerito()

    End Sub
    Private Sub ExibirDadosPerito()

        Dim Bal As New BALAnotacao(GetUsuario)
        Dim Ent As New EntAnotacao
        Dim BalPer As New BALPERITO(GetUsuario)
        Dim EntPer As New EntPERITO
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim EntProcPer As New EntProcesso_Perito
        Dim Ds As DataSet
        Dim DsPer As DataSet
        Dim DsProcPer As DataSet
        Dim rsPer As DataRow
        Dim rsProcPer As DataRow
        Dim m_ID_PF As Long
        Dim i As Integer

        m_ID_PF = CInt(CboPerito.Items.FindByValue(CboPerito.Text).Value)

        'FOS - Verificar a necessidade de informar o código da profissão e especialidade
        DsProcPer = BalProcPer.ExibirDadosSet(txtNum_CNJ.Text, m_ID_PF, "", "", CDate(""))
        'If Not DsProcPer Is Nothing Then
        If DsProcPer.Tables(0).Rows.Count > 0 Then
            rsProcPer = DsProcPer.Tables(0).Rows(0)
            If Not rsProcPer("Data_Negacao").ToString = Nothing Then
                MsgErro("Perito já foi Nomeado anteriormente e Recusou o convite")
                Exit Sub
            End If
            lblData_Nomeacao.Text = rsProcPer("Data_Nomeacao").ToString
            If lblData_Nomeacao.Text = "" Then
                lblData_Nomeacao.Text = Today.ToShortDateString
            End If
            lblData_Liberacao.Text = rsProcPer("Data_Liberacao").ToString
            If lblData_Liberacao.Text <> "" Then
                chkLaudoLiberado.Checked = True
            Else
                chkLaudoLiberado.Checked = False
            End If
        End If
        'End If
        DsProcPer = Nothing
        Ds = Bal.ExibirAnotPer(m_ID_PF)
        If Ds.Tables(0).Rows.Count > 0 Then
            txtAnotacao.Visible = True
            LblAnotacao.Visible = True
            txtAnotacao.Text = ""
            If Ds.Tables(0).Rows.Count > 0 Then
                I = 0
                For Each rs As DataRow In Ds.Tables(0).Rows
                    txtAnotacao.Text = txtAnotacao.Text + " - " + Ds.Tables(0).Rows(i).Item(3).ToString + Chr(13)  'rs("Descr_Anotacao").ToString
                    I = I + 1
                Next
            End If
        Else
            LblAnotacao.Visible = False
            txtAnotacao.Visible = False
        End If
        Ds = Nothing
        DsPer = BalPer.ExibirDadosSet(m_ID_PF)
        'If Not DsPer Is Nothing Then
        If DsPer.Tables(0).Rows.Count > 0 Then
            rsPer = DsPer.Tables(0).Rows(0)
            lblEmail.Text = rsPer("Email").ToString
            lblData_Cadastramento.Text = rsPer("Data_Cadastramento").ToString
            txtQtePendentes.Text = rsPer("QtePendentes").ToString
            txtQteAceitos.Text = rsPer("QteAceitos").ToString
            rsPer = DsPer.Tables(0).Rows(1)
            lblEmail1.Text = rsPer("Email").ToString
        End If
        'End If
        DsPer = Nothing

    End Sub

    Protected Sub txtNum_Processo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_Processo.TextChanged
        Dim Ent As EntPROC_CNJ
        'Dim EntProcPer As EntProcesso_Perito
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Dim Cod_CNJ_Valido As Boolean

        If Not ValidaNumProc(txtNum_Processo.Text) Then
            MsgErro("Número de Processo Inválido")
            Exit Sub
        End If
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ.Text = "" Then txtNum_CNJ.Text = Ent.Cod_CNJ
            Cod_CNJ_Valido = ValidaNumCNJ(Ent.Cod_CNJ)
            If Cod_CNJ_Valido Then
                PreencherProcPerito(Ent.Cod_CNJ) '(GrdProcPerito)
                'Ao selecionar ... EntProcPer = BalProcPer.ExibirDadosEnt(m_Cod_CNJ,m_ID_PF)
            Else
                MsgErro("Número de CNJ Inválido")
            End If
        Else
            MsgErro("Número de Processo/CNJ não localizado!")
        End If
        'Ent.NUM_CNJ = Rss("NUM_CNJ")
        'Ent.ID_PF = Rss("ID_PF ")
        'Ent.DATA_NOMEACAO = Rss("DATA_NOMEACAO")
        'Ent.DATA_NEGACAO = Rss("DATA_NEGACAO")
        'Ent.DATA_ACEITACAO = Rss("DATA_ACEITACAO")
        'Ent.Data_Liberacao = Rss("DATA_LIBERACAO")
        'Ent.SIGLA_NOMEACAO = Rss("SIGLA_NOMEACAO")


    End Sub

    Protected Sub txtNum_CNJ_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum_CNJ.TextChanged
        Dim Ent As EntPROC_CNJ
        Dim BalP As New BalProc_CNJ(GetUsuario)
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        If Not ValidaNumCNJ(txtNum_CNJ.Text) Then
            MsgErro("Número CNJ Inválido")
            Exit Sub
        End If
        Ent = BalP.ExibirDadosEnt(txtNum_Processo.Text, txtNum_CNJ.Text)
        If Not Ent Is Nothing Then
            If txtNum_Processo.Text = "" Then txtNum_Processo.Text = Ent.Cod_Proc
            If txtNum_CNJ.Text = "" Then txtNum_CNJ.Text = Ent.Cod_CNJ
            PreencherProcPerito(Ent.Cod_CNJ) '(GrdProcPerito)
        Else
            MsgErro("Número de CNJ Inválido")
        End If

    End Sub

    Protected Sub BtnNovo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNovo.Click
        txtAnotacao.Visible = False
        LblAnotacao.Visible = False
        lblEmail.Text = ""
        lblEmail1.Text = ""
        txtAnotacao.Text = ""
        lblData_Nomeacao.Text = ""
        lblData_Liberacao.Text = ""
        txtNum_CNJ.Text = ""
        txtNum_Processo.Text = ""
        txtQteAceitos.Text = ""
        txtQtePendentes.Text = ""
        'CboPerito.Text = "Selecione um perito"
        CboPerito.DataSource = Nothing
        CboPerito.DataBind()
        CboEspecialidade.DataSource = Nothing
        CboEspecialidade.DataBind()
        PreencherEspecialidade(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value))
        CboProfissao.DataSource = Nothing
        CboProfissao.DataBind()
        PreencherProfissao()
        GrdProcPerito.DataSource = Nothing
        GrdProcPerito.DataBind()
    End Sub

    Protected Sub PreencherProcPerito(ByVal Num_CNJ As String)
        Dim Ds As DataSet
        Dim BalProcPer As New BalProcesso_Perito(GetUsuario)
        Ds = BalProcPer.ExibirDadosTodos(Num_CNJ)
        GrdProcPerito.DataSource = Ds.Tables(0)
        GrdProcPerito.DataBind()
    End Sub


    'Protected Sub chkLaudoLiberado_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkLaudoLiberado.CheckedChanged
    '    If chkLaudoLiberado.Checked And lblData_Liberacao.Text = "" Then
    '        lblData_Liberacao.Text = Today.ToShortDateString
    '    End If
    'End Sub

    Protected Sub GrdProcPerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GrdProcPerito.SelectedIndexChanged
        Dim m_ID_PF As Long
        Dim m_Descr_Especialidade As String
        Dim m_Descr_Profissao As String
        'Dim Acento As HttpServerUtility
        m_ID_PF = CInt(GrdProcPerito.SelectedRow.Cells(4).Text)
        m_Descr_Especialidade = HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(1).Text)
        'Inserir na Grid Profissao
        m_Descr_Profissao = HttpUtility.HtmlDecode(GrdProcPerito.SelectedRow.Cells(4).Text)
        CboEspecialidade.SelectedValue = CboEspecialidade.Items.FindByText(m_Descr_Especialidade).Value
        PreencherPeritos(CInt(CboProfissao.Items.FindByValue(CboProfissao.Text).Value), CInt(CboEspecialidade.Items.FindByValue(CboEspecialidade.Text).Value))
        CboPerito.SelectedValue = CboPerito.Items.FindByText(GrdProcPerito.SelectedRow.Cells(2).Text).Value
        'CboPerito.DataBind()
        ExibirDadosPerito()
    End Sub
End Class