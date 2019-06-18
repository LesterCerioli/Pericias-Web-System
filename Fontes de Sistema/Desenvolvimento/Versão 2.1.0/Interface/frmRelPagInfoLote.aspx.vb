Imports Entidade
Imports BAL

Public Class frmRelPagInfoLote
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                CarregaRelacoesPagamento()
            End If
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CarregaRelacoesPagamento()
        Try
            Dim balRelacaoPagamento As New BalRelacaoPagamento(GetUsuario)
            Dim listaRelacoesPagamento As List(Of RelacaoPagamento) = Nothing

            listaRelacoesPagamento = balRelacaoPagamento.RetornaTodasRelacoesPagamento

            If listaRelacoesPagamento.Count > 0 Then
                ddlRelacaoPagamento.DataValueField = "Id"
                ddlRelacaoPagamento.DataTextField = "NOME"
                ddlRelacaoPagamento.DataSource = listaRelacoesPagamento
                ddlRelacaoPagamento.DataBind()

                ddlRelacaoPagamento.Items.Insert(0, "Selecione uma relação de pagamento...")
            Else
                Throw New Exception("Não há relações de pagamento cadastradas!")
            End If
        Catch ex As Exception
            ddlRelacaoPagamento.Enabled = False
            Throw ex
        End Try
    End Sub

    Private Sub Limpar()
        Try
            txtDataPagamento.Text = String.Empty
            txtDataProtocolo.Text = String.Empty
            txtProcessoPagamento.Text = String.Empty
            ddlRelacaoPagamento.SelectedIndex = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnGravar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnGravar.Click
        Try
            If ddlRelacaoPagamento.SelectedIndex = -1 OrElse ddlRelacaoPagamento.SelectedIndex = 0 Then
                MsgErro("É necessário selecionar pelo menos uma Relação de Pagamento!")
                Exit Sub
            End If

            Dim balRelPagamento As New BalRelacaoPagamento(GetUsuario)
            Dim relacaoPagamento = balRelPagamento.ListarRelacaoPagamento(CLng(ddlRelacaoPagamento.SelectedValue))

            If relacaoPagamento Is Nothing Then
                MsgErro("Relação de pagamento inexistente.")
                Exit Sub
            End If

            If Not relacaoPagamento.PagamentoLote And relacaoPagamento.Definitiva Then
                MsgErro("Gravação rejeitada. A relação de pagamento é definitva e não poderá ser alterada.")
                Exit Sub
            ElseIf relacaoPagamento.PagamentoLote And relacaoPagamento.Definitiva Then
                MsgErro("Esta relação de pagamento não pode ter os dados alterados pois a mesma é definitiva.")
                Exit Sub
            End If

            balRelPagamento.AlterarInformacoesEmLote(relacaoPagamento.Id, txtProcessoPagamento.Text, txtDataProtocolo.Text, txtDataPagamento.Text)

            If relacaoPagamento.PagamentoLote Then
                MsgErro("Dados alterados com sucesso.", "Sucesso")
            Else
                MsgErro("Dados inseridos com sucesso.", "Sucesso")
            End If

            Limpar()

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnLimpar.Click
        Try
            Limpar()

            up1.Update()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnSair_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSair.Click
        Try
            Response.Redirect("Principal.aspx")
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

End Class