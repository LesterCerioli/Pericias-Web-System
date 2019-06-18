Imports BAL
Imports Entidade
Imports Utilitarios.DadosUtil

Public Class frmDetalhesPerito
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim codPerito As Long = CLng(Request.QueryString("codPerito"))
            Dim tipoPessoa As Integer = CInt(Request.QueryString("tipoPessoa"))
            Dim perito As EntPERITO = Nothing
            Dim balPerito As New BALPERITO(GetUsuario)

            perito = balPerito.ExibirDadosCadPeritoEnt("CODIGO", codPerito, "S", tipoPessoa)

            CarregarInformacoesPerito(perito)
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Private Sub CarregarInformacoesPerito(ByVal perito As EntPERITO)
        Try
            DadosPessoais(perito)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DadosPessoais(ByVal perito As EntPERITO)
        Try
            lblNomePerito.Text = perito.Nome
            lblCpfCnpj.Text = IIf(perito.TipoPessoa = EntPERITO.Pessoa.Fisica, perito.CPF, perito.CNPJ)
            lblStatusPerito.Text = perito.StatusAtual.Descricao
            imgFotoPerito.ImageUrl = ExibirArquivoGedFoto(perito.IDGED_Foto, "Imagem")

            Anotacoes(perito.Cod_Perito)

            Email(perito.EMAIL, perito.EMAIL1)

            Profissoes(perito.Cod_Perito)

            Telefone(perito.Cod_Perito, perito.TipoPessoa)

            Endereco(New Object() {perito.Nome_Logr, perito.Descr_Tip_Logr, perito.Num_Logr, _
                                    perito.Compl_Logr, perito.Descr_Bairro, perito.CEP, perito.Descr_Cidade, _
                                    perito.Sigla_UF},
                     New Object() {perito.Nome_Logr1, perito.Descr_Tip_Logr1, perito.Num_Logr1, _
                                    perito.Compl_Logr1, perito.Descr_Bairro1, perito.CEP1, perito.Descr_Cidade1, _
                                    perito.Sigla_UF1})


            InformacoesComplementares(perito.StatusAtual, perito.Data_Cadastramento, perito.DataMigracao)

            NursComarcas(perito.Cod_Perito)

            Documentos(perito.Cod_Perito)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Email(ByVal emailPrincipal As String, ByVal emailAlternativo As String)
        Try
            If Not String.IsNullOrEmpty(emailPrincipal) Then
                hlEmailPrincipal.NavigateUrl = String.Format("mailto:{0}", emailPrincipal)
                hlEmailPrincipal.Text = emailPrincipal
            End If

            If Not String.IsNullOrEmpty(emailAlternativo) Then
                hlEmailAlternativo.NavigateUrl = String.Format("mailto:{0}", emailAlternativo)
                hlEmailAlternativo.Text = emailAlternativo
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Telefone(ByVal codPerito As Long, ByVal tipoPessoa As Integer)
        Try
            Dim ds As DataSet = Nothing
            Dim balTelefone As New BalTelefone(GetUsuario)

            ds = balTelefone.ExibirDadosTelefone(codPerito, tipoPessoa)

            grdTelefone.DataSource = ds
            grdTelefone.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Endereco(ByVal enderecoResidencial As Object(),
                          ByVal enderecoComercial As Object())
        Try
            If enderecoResidencial.Length > 0 Then
                If Not String.IsNullOrEmpty(enderecoResidencial(1)) Then
                    lblTituloLogrResidencial.Text = CStr(enderecoResidencial(1)) + ":"
                    lblLogrResidencial.Text = CStr(enderecoResidencial(0))
                    lblNumeroResidencial.Text = CStr(enderecoResidencial(2))
                    lblComplementoResidencial.Text = CStr(enderecoResidencial(3))
                    lblBairroResidencial.Text = CStr(enderecoResidencial(4))
                    lblCEPResidencial.Text = CStr(enderecoResidencial(5))
                    lblCidadeResidencial.Text = CStr(enderecoResidencial(6))
                    lblUFResidencial.Text = CStr(enderecoResidencial(7))
                End If
            End If

            If enderecoComercial.Length > 0 Then
                If Not String.IsNullOrEmpty(enderecoComercial(1)) Then
                    lblTituloLogrComercial.Text = CStr(enderecoComercial(1)) + ":"
                    lblLogrComercial.Text = CStr(enderecoComercial(0))
                    lblNumeroComercial.Text = CStr(enderecoComercial(2))
                    lblComplementoComercial.Text = CStr(enderecoComercial(3))
                    lblBairroComercial.Text = CStr(enderecoComercial(4))
                    lblCEPComercial.Text = CStr(enderecoComercial(5))
                    lblCidadeComercial.Text = CStr(enderecoComercial(6))
                    lblUFComercial.Text = CStr(enderecoComercial(7))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InformacoesComplementares(ByVal statusAtual As TIPO_STATUS,
                                          ByVal dataCadastro As Date,
                                          ByVal dataMigracao As Date)
        Try
            If statusAtual.Codigo = 4 Then
                lblPeritoJuizo.Text = "Sim"
            Else
                lblPeritoJuizo.Text = "Não"
            End If

            lblDataCadastro.Text = dataCadastro.ToString("dd/MM/yyyy")

            If Not dataMigracao = Nothing Then
                lblDataMigração.Text = dataMigracao.ToString("dd/MM/yyyy")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Documentos(ByVal codPerito As Long)
        Try
            Dim listaDocumentos As List(Of DOCUMENTO) = Nothing
            Dim balDocumentos As New BALDOCUMENTO(GetUsuario)

            listaDocumentos = balDocumentos.Listar(codPerito)

            grdDoc.DataSource = listaDocumentos
            grdDoc.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub grdDoc_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDoc.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim urlDocGed As String = String.Empty
                Dim IdGed As String = String.Empty
                Dim nomeArquivo As String = String.Empty

                IdGed = CStr(e.Row.Cells(2).Text)
                nomeArquivo = CStr(e.Row.Cells(1).Text)
                urlDocGed = MontaURLDocumentoGED(IdGed)

                Dim linkDocumento As HyperLink = e.Row.FindControl("linkDocumento")
                linkDocumento.NavigateUrl = urlDocGed
                linkDocumento.Text = nomeArquivo

                e.Row.Cells(2).Visible = False
                e.Row.Cells(1).Visible = False
                e.Row.Cells(0).ColumnSpan = 2
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Anotacoes(ByVal codPerito As Long)
        Try
            Dim ds As DataSet = Nothing
            Dim balAnotacoes As New BALAnotacao(GetUsuario)


            Dim linhaAnotacao As HtmlTableRow = Nothing

            Dim lblAnotacao As Label = Nothing

            ds = balAnotacoes.Listar_Anotacoes_Perito(codPerito, 6) 'Anotações do tipo "OBSERVAÇÃO" '

            If ds.Tables(0).Rows.Count = 0 Then
                lblAnotacao = New Label
                lblAnotacao.ID = "lblAnotacao"
                lblAnotacao.Attributes.CssStyle.Add("margin-left", "30px")
                lblAnotacao.Text = "Não há anotações cadastradas para este perito."

                Dim colunaAnotacao = New HtmlTableCell
                colunaAnotacao.Controls.Add(lblAnotacao)

                linhaAnotacao = New HtmlTableRow
                linhaAnotacao.Cells.Add(colunaAnotacao)

                tblAnotacoes.Rows.Add(linhaAnotacao)
                Exit Sub
            End If


            For Each r As DataRow In ds.Tables(0).Rows
                linhaAnotacao = New HtmlTableRow

                Dim imgCollapsable = New Image
                imgCollapsable.ID = String.Format("imgCollapsable_{0}_{1}", CStr(r(0)), CStr(r(6)))
                imgCollapsable.ImageUrl = "~/Imagens/iconmonstr-arrow-69-24.png"
                imgCollapsable.Attributes.Add("estado", "retraido")
                imgCollapsable.Attributes.Add("OnClick", String.Format("Mudar_Imagem('imgCollapsable_{0}_{1}', 'pAnotacao_{0}_{1}')", CStr(r(0)), CStr(r(6))))

                Dim colunaAnotacao1 = New HtmlTableCell
                colunaAnotacao1.Controls.Add(imgCollapsable)

                Dim htmlGenerico As New HtmlGenericControl
                'htmlGenerico.InnerHtml = String.Format("{0}<p id='pAnotacao_{1}_{2}' estado='retraido' style='margin-left: 30px; max-width: 600px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap'><label id=lblAnotacao_{1}_{2}>{3}</label></p>", htmlGenerico.InnerHtml, CStr(r(0)), CStr(r(6)), CStr(r(1)), Now.Ticks.ToString)
                htmlGenerico.InnerHtml = String.Format("<p id='pAnotacao_{0}_{1}' estado='retraido' style='margin-left: 30px; max-width: 600px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap'><label id=lblAnotacao_{0}_{1}>{2}</label></p>", CStr(r(0)), CStr(r(6)), CStr(r(1)), Now.Ticks.ToString)
                'lblAnotacao = New Label
                'lblAnotacao.ID = String.Format("lblAnotacao_{0}_{1}", CStr(r(0)), CStr(r(6)))
                'lblAnotacao.Attributes.CssStyle.Add("margin-left", "30px")
                'lblAnotacao.Attributes.CssStyle.Add("max-width", "30px")
                'lblAnotacao.Attributes.CssStyle.Add("text-overflow", "...")
                'lblAnotacao.Attributes.CssStyle.Add("white-space", "nowrap")
                'lblAnotacao.Text = CStr(r(1))

                Dim colunaAnotacao = New HtmlTableCell
                colunaAnotacao.Controls.Add(htmlGenerico)

                linhaAnotacao.Cells.Add(colunaAnotacao1)

                linhaAnotacao.Cells.Add(colunaAnotacao)

                tblAnotacoes.Rows.Add(linhaAnotacao)
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Profissoes(ByVal codPerito As Long)
        Try
            Dim ds As DataSet = Nothing
            Dim balProfissaoEspecialidades As New BALEspecialidadePerito(GetUsuario)
            Dim htmlGenerico As New HtmlGenericControl

            Dim linhaProfissao As HtmlTableRow = Nothing
            Dim colunaNomeProfissao As HtmlTableCell = Nothing
            Dim colunaNomeEspecialidade As HtmlTableCell = Nothing
            Dim colunaSigla As HtmlTableCell = Nothing
            Dim colunaUF As HtmlTableCell = Nothing
            Dim colunaRegNum As HtmlTableCell = Nothing
            Dim lblProfissao As Label = Nothing
            Dim i As Long = 1

            ds = balProfissaoEspecialidades.ListarEspecialidadesPerito(CStr(codPerito))

            If ds.Tables(0).Rows.Count = 0 Then
                'htmlGenerico.InnerHtml = "<table><tr><td><p><label style='margin-left: 30px;'>Não há profissões cadastradas pare este perito.</label></p></td></tr><table>"
                lblProfissao = New Label
                lblProfissao.ID = "lblProfissao"
                lblProfissao.Attributes.CssStyle.Add("margin-left", "30px")
                lblProfissao.Text = "Não há profissões cadastradas pare este perito."

                colunaNomeProfissao = New HtmlTableCell
                colunaNomeProfissao.Controls.Add(lblProfissao)

                linhaProfissao = New HtmlTableRow
                linhaProfissao.Cells.Add(colunaNomeProfissao)
                tblProfissoes.Rows.Add(linhaProfissao)
                Exit Sub
            End If

            Dim nomeProfissao As String = Nothing

            For Each r As DataRow In ds.Tables(0).Rows
                If i = 1 Then
                    nomeProfissao = NVL(CStr(r(0)), String.Empty)

                    lblProfissao = New Label
                    lblProfissao.Text = String.Format("> {0}", nomeProfissao)

                    colunaNomeProfissao = New HtmlTableCell
                    colunaNomeProfissao.ColSpan = 4
                    colunaNomeProfissao.Controls.Add(lblProfissao)

                    linhaProfissao = New HtmlTableRow
                    linhaProfissao.Cells.Add(colunaNomeProfissao)

                    tblProfissoes.Rows.Add(linhaProfissao)

                    colunaNomeEspecialidade = New HtmlTableCell
                    colunaNomeEspecialidade.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(1), String.Empty)))
                    colunaSigla = New HtmlTableCell
                    colunaSigla.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(2), String.Empty)))
                    colunaUF = New HtmlTableCell
                    colunaUF.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(9), String.Empty)))
                    colunaRegNum = New HtmlTableCell
                    colunaRegNum.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(8), String.Empty)))

                    linhaProfissao = New HtmlTableRow
                    linhaProfissao.Cells.Add(colunaNomeEspecialidade)
                    linhaProfissao.Cells.Add(colunaSigla)
                    linhaProfissao.Cells.Add(colunaUF)
                    linhaProfissao.Cells.Add(colunaRegNum)

                    tblProfissoes.Rows.Add(linhaProfissao)
                    'htmlGenerico.InnerHtml = String.Format("{0}<tr><td colspan=4><label>> {1}</label></td></tr>", htmlGenerico.InnerHtml, nomeProfissao)
                Else
                    If nomeProfissao <> CStr(r(0)) Then
                        nomeProfissao = CStr(r(0))
                        lblProfissao = New Label
                        lblProfissao.Text = String.Format("> {0}", nomeProfissao)

                        colunaNomeProfissao = New HtmlTableCell
                        colunaNomeProfissao.ColSpan = 4
                        colunaNomeProfissao.Controls.Add(lblProfissao)

                        linhaProfissao = New HtmlTableRow
                        linhaProfissao.Cells.Add(colunaNomeProfissao)

                        tblProfissoes.Rows.Add(linhaProfissao)

                        colunaNomeEspecialidade = New HtmlTableCell
                        colunaNomeEspecialidade.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(1), String.Empty)))
                        colunaSigla = New HtmlTableCell
                        colunaSigla.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(2), String.Empty)))
                        colunaUF = New HtmlTableCell
                        colunaUF.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(9), String.Empty)))
                        colunaRegNum = New HtmlTableCell
                        colunaRegNum.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(8), String.Empty)))

                        linhaProfissao = New HtmlTableRow
                        linhaProfissao.Cells.Add(colunaNomeEspecialidade)
                        linhaProfissao.Cells.Add(colunaSigla)
                        linhaProfissao.Cells.Add(colunaUF)
                        linhaProfissao.Cells.Add(colunaRegNum)

                        tblProfissoes.Rows.Add(linhaProfissao)
                    Else
                        colunaNomeEspecialidade = New HtmlTableCell
                        colunaNomeEspecialidade.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(1), String.Empty)))
                        colunaSigla = New HtmlTableCell
                        colunaSigla.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(2), String.Empty)))
                        colunaUF = New HtmlTableCell
                        colunaUF.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(9), String.Empty)))
                        colunaRegNum = New HtmlTableCell
                        colunaRegNum.InnerHtml = String.Format("<label>{0}</label>", CStr(NVL(r(8), String.Empty)))

                        linhaProfissao = New HtmlTableRow
                        linhaProfissao.Cells.Add(colunaNomeEspecialidade)
                        linhaProfissao.Cells.Add(colunaSigla)
                        linhaProfissao.Cells.Add(colunaUF)
                        linhaProfissao.Cells.Add(colunaRegNum)

                        tblProfissoes.Rows.Add(linhaProfissao)
                    End If
                End If



                i = i + 1
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NursComarcas(ByVal codPerito As Long)
        Try
            Dim listaComarcasPerito As New List(Of EntCOMARCA)
            Dim listaNurs As List(Of EntNURC) = Nothing
            Dim balComarca As New BALCOMARCA(GetUsuario)
            Dim balNur As New BALNURC(GetUsuario)
            Dim htmlGenerico As New HtmlGenericControl
            Dim i As Long = 1

            Dim imgCollapsable As Image = Nothing
            Dim colunaImagemCollapsable As HtmlTableCell = Nothing
            Dim colunaNurComarca As HtmlTableCell = Nothing
            Dim linhaNurComarca As HtmlTableRow = Nothing

            listaNurs = balNur.ListarTodasNurcs

            listaComarcasPerito = balComarca.ListarComarcasPerito(codPerito)

            For Each nur As EntNURC In listaNurs
                'htmlGenerico.InnerHtml = String.Format("{0}<tr><td><img src='Imagens/iconmonstr-arrow-70-24.png' estado='expandido' onclick=Expandir_Retrair_Detalhes_Grid('{1}', 'imgCollapsable_Nur_{2}') id='imgCollapsable_Nur_{3}' /></td><td><label style='font-weight: bold;'>{4}</label></td></tr>", htmlGenerico.InnerHtml, nur.CodNurc.ToString, nur.CodNurc.ToString, nur.CodNurc.ToString, nur.DescrRes)
                'htmlGenerico.InnerHtml = String.Format("{0}<tr><td><img src='Imagens/iconmonstr-arrow-70-24.png' estado='expandido' id='imgCollapsable_Nur_{1}' onclick=Expandir_Retrair_Detalhes_Grid({2}, imgCollapsable_Nur_{3}) > </img></td><td><label style='font-weight: bold;'>{4}</label></td></tr>", htmlGenerico.InnerHtml, nur.CodNurc.ToString, nur.CodNurc.ToString, nur.CodNurc.ToString, nur.DescrRes)
                imgCollapsable = New Image
                imgCollapsable.ID = String.Format("imgCollapsable_Nur_{0}", nur.CodNurc.ToString)
                imgCollapsable.ImageUrl = "~/Imagens/iconmonstr-arrow-70-24.png"
                imgCollapsable.Attributes.Add("estado", "expandido")
                imgCollapsable.Attributes.Add("OnClick", String.Format("Expandir_Retrair_Detalhes_Grid('{0}', 'imgCollapsable_Nur_{0}')", nur.CodNurc.ToString))

                colunaImagemCollapsable = New HtmlTableCell
                colunaImagemCollapsable.Width = "30"
                colunaImagemCollapsable.Controls.Add(imgCollapsable)
                colunaNurComarca = New HtmlTableCell
                colunaNurComarca.InnerHtml = String.Format("<label style='font-weight: bold;'>{0}</label>", nur.DescrRes)

                linhaNurComarca = New HtmlTableRow
                linhaNurComarca.Cells.Add(colunaImagemCollapsable)
                linhaNurComarca.Cells.Add(colunaNurComarca)

                tblNursComarcas.Rows.Add(linhaNurComarca)

                For Each comarca As EntCOMARCA In listaComarcasPerito
                    If nur.CodNurc = comarca.CodNurc Then
                        colunaImagemCollapsable = New HtmlTableCell
                        colunaImagemCollapsable.Width = "30"
                        colunaNurComarca = New HtmlTableCell
                        colunaNurComarca.ID = String.Format("lnTbl_{0}_{1}", nur.CodNurc.ToString, comarca.CodCom.ToString)
                        colunaNurComarca.InnerHtml = String.Format("<label>{0}</label>", comarca.Nome)

                        linhaNurComarca = New HtmlTableRow
                        linhaNurComarca.Attributes.Add("pai", String.Format("lnTbl{0}", comarca.CodNurc))
                        linhaNurComarca.Cells.Add(colunaImagemCollapsable)
                        linhaNurComarca.Cells.Add(colunaNurComarca)
                        'htmlGenerico.InnerHtml = String.Format("{0}<tr><td></td><td id=lnTbl_{1}_{2} pai=lnTbl{3}><label>{4}</label></td></tr>", htmlGenerico.InnerHtml, comarca.CodNurc.ToString, comarca.CodCom.ToString, comarca.CodNurc.ToString, comarca.Nome)
                    End If

                    tblNursComarcas.Rows.Add(linhaNurComarca)
                Next
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class