Imports BAL
Imports Entidade
Imports Microsoft.Reporting.WebForms


Public Class frmRelatorio
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GerarRelatorio()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub GerarRelatorio()
        Try
            Dim idRelacaoPagamento As Long = 0
            Dim loginUsuario As String = GetUsuario.Login
            Dim qntdAjudasCusto As Long = 0
            Dim qntdPeritos As Long = 0
            Dim nomeRelacaoPagamento As String = String.Empty
            Dim totalPagar As Double = 0D
            Dim filtroNome As String = String.Empty
            Dim filtroCpfCnpj As String = String.Empty
            Dim filtroProfissao As String = String.Empty
            Dim filtroEspecialidade As String = String.Empty
            Dim filtroOficio As String = String.Empty
            Dim filtroProcesso As String = String.Empty
            Dim filtroRelacao As String = String.Empty
            Dim filtroProcPag As String = String.Empty
            Dim filtroDtPgto As String = String.Empty
            Dim tipoRelatorio As Integer = 0
            Dim balRelPag As New BalRelacaoPagamento(GetUsuario)

            Dim ds As DataSet = Nothing
            tipoRelatorio = Request.QueryString("tipoRelatorio")
            Dim dv As DataView = Nothing

            If Not String.IsNullOrEmpty(Request.QueryString("idRelacaoPagamento")) Then
                idRelacaoPagamento = CLng(Request.QueryString("idRelacaoPagamento"))
            Else

                filtroNome = Request.QueryString("filtroNome")
                filtroCpfCnpj = Request.QueryString("filtroCpfCnpj")
                filtroProfissao = Request.QueryString("filtroProfissao")
                filtroEspecialidade = Request.QueryString("filtroEspecialidade")
                filtroOficio = Request.QueryString("filtroOficio")
                filtroProcesso = Request.QueryString("filtroProcesso")
                filtroRelacao = Request.QueryString("filtroRelacao")
                filtroProcPag = Request.QueryString("filtroProcPag")
                filtroDtPgto = Request.QueryString("filtroDtPgto")
            End If

            If tipoRelatorio = 2 Then
                ds = balRelPag.RelatorioRelacoesPagamento(idRelacaoPagamento)

                totalPagar = CDbl(ds.Tables(0).Compute("Sum(VALOR)", ""))
                qntdAjudasCusto = ds.Tables(0).Rows.Count
                dv = New DataView(ds.Tables(0))
                Dim dt As DataTable = dv.ToTable(True, "CPF_CNPJ")
                qntdPeritos = dt.Rows.Count

                nomeRelacaoPagamento = CStr(ds.Tables(0).Rows(0).Item(2))

                'CriarParametros(ds.Tables(0))
            Else
                ds = DirectCast(Session("RELAJUDACUSTOPERITO"), DataSet)

                totalPagar = CDbl(ds.Tables(0).Compute("Sum(VALOR)", ""))
                qntdAjudasCusto = ds.Tables(0).Rows.Count
                dv = New DataView(ds.Tables(0))
                Dim dt As DataTable = dv.ToTable(True, "COD_PERITO")
                qntdPeritos = dt.Rows.Count

                'CriarParametros(ds.Tables(0))
            End If

            If tipoRelatorio = 1 Then
                Dim parametros(12) As ReportParameter

                parametros(0) = New ReportParameter("UsuarioLogin", loginUsuario)
                parametros(1) = New ReportParameter("filtroNome", filtroNome)
                parametros(2) = New ReportParameter("filtroCpfCnpj", filtroCpfCnpj)
                parametros(3) = New ReportParameter("filtroProfissao", filtroProfissao)
                parametros(4) = New ReportParameter("filtroEspecialidade", filtroEspecialidade)
                parametros(5) = New ReportParameter("filtroOficio", filtroOficio)
                parametros(6) = New ReportParameter("filtroProcesso", filtroProcesso)
                parametros(7) = New ReportParameter("filtroRelacao", filtroRelacao)
                parametros(8) = New ReportParameter("filtroProcPag", filtroProcPag)
                parametros(9) = New ReportParameter("filtroDtPgto", filtroDtPgto)
                parametros(10) = New ReportParameter("qntdAjudasCusto", qntdAjudasCusto.ToString)
                parametros(11) = New ReportParameter("qntdPeritos", qntdPeritos.ToString)
                parametros(12) = New ReportParameter("totalPago", String.Format("R$ {0:N}", totalPagar))

                GeraRelatorio("RELATORIOAJUDACUSTOPERITO", "DataSet1", ds.Tables(0), parametros)
            ElseIf tipoRelatorio = 2 Then
                Dim parametros(4) As ReportParameter

                parametros(0) = New ReportParameter("UsuarioLogin", loginUsuario)
                parametros(1) = New ReportParameter("nomeRelacaoPagamento", nomeRelacaoPagamento)
                parametros(2) = New ReportParameter("qntdAjudasCusto", qntdAjudasCusto.ToString)
                parametros(3) = New ReportParameter("qntdPeritos", qntdPeritos.ToString)
                parametros(4) = New ReportParameter("totalPagar", String.Format("R$ {0:N}", totalPagar))

                GeraRelatorio("RELATORIORELACAOPAGAMENTO", "DataSet1", ds.Tables(0), parametros)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GeraRelatorio(ByVal arquivoRelatorio As String, ByVal nomeDataSet As String, ByVal dt As DataTable, ByVal parametros() As ReportParameter)
        Try
            Dim rptViewer As ReportViewer = New ReportViewer
            rptViewer.Reset()
            rptViewer.LocalReport.DataSources.Clear()
            rptViewer.LocalReport.DataSources.Add(New ReportDataSource(nomeDataSet, dt))
            rptViewer.ProcessingMode = ProcessingMode.Local
            rptViewer.LocalReport.ReportPath = String.Format("Relatorio\{0}.rdlc", arquivoRelatorio)
            rptViewer.LocalReport.SetParameters(parametros)
            rptViewer.LocalReport.Refresh()
            ExibirRelatorio(rptViewer.LocalReport, "PDF")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExibirRelatorio(ByVal report As LocalReport, ByVal tipoArquivo As String)
        Try
            Dim bytes As Byte() = Nothing
            bytes = report.Render(tipoArquivo, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Response.ContentType = String.Format("application/{0}", LCase(tipoArquivo))
            Response.AddHeader("content-length", bytes.Length.ToString)
            Response.BinaryWrite(bytes)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class