Imports Entidade
Imports BAL
Imports Microsoft.Reporting.WebForms

Public Class frmDetalhesPagamentosRealizados
    Inherits BasePage

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CarregaInformacoes()
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CarregaInformacoes()
        Try
            Dim ajudaCusto As AjudaCusto = DirectCast(Session("DetalhesPagamento"), AjudaCusto)

            lblNomePerito.Text = ajudaCusto.Perito.Nome
            lblCpfCnpj.Text = RetornaCPFCNPJFormatado(IIf(ajudaCusto.Perito.TipoPessoa = EntPERITO.Pessoa.Fisica, ajudaCusto.Perito.CPF, ajudaCusto.Perito.CNPJ))
            lblProfissao.Text = ajudaCusto.Profissao.Descr_Profissao
            lblEspecialidade.Text = ajudaCusto.Especialidade.Descr_Especialidade
            lblNumReg.Text = ajudaCusto.EspecialidadeProfissaoPerito.Num_Reg
            lblSigla.Text = ajudaCusto.EspecialidadeProfissaoPerito.COD_ORGAO_PER
            lblUF.Text = ajudaCusto.EspecialidadeProfissaoPerito.UF

            lblOficio.Text = ajudaCusto.Oficio
            lblDataRec.Text = ajudaCusto.DataRecebimento
            lblProcesso.Text = ajudaCusto.NumeroProcesso
            lblRelPgto.Text = ajudaCusto.RelacaoPagamento.Nome
            lblProcPgto.Text = ajudaCusto.ProcessoPagamento
            lblDataProt.Text = ajudaCusto.DataProtocolo
            lblDataPgto.Text = ajudaCusto.DataPagamento
            lblValor.Text = String.Format("R$ {0:N}", ajudaCusto.Valor)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    

End Class