Imports ServicoDadosODPNET
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager
Imports Oracle.DataAccess.Client


Public Class BalTelefone
    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ConsultarTelefone(ByVal pcod_perito As Long, ByVal pSeqTel As Integer, ByVal tipoPessoa As Integer) As EntTelefone
        Dim ds As DataSet
        Dim Ent As New EntTelefone

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.Consultar_Telefone", sd.CriaRefCursor, pcod_perito, pSeqTel, tipoPessoa)

            If ds.Tables(0).Rows.Count > 0 Then
                Ent = GerarEntidade(ds)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return Ent
    End Function

    Public Function ExibirDadosTelefone(ByVal pCod_Perito As Long, ByVal tipoPessoa As Integer) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Perito_Telefone", sd.CriaRefCursor, pCod_Perito, tipoPessoa)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Private Function GerarEntidade(ByVal Rss As DataSet) As EntTelefone
        Dim Ent As New EntTelefone

        For Each r As DataRow In Rss.Tables(0).Rows
            Ent.Cod_Tip_Tel = NVL(r("Cod_Tip_Tel"), 0)
            Ent.DDD = NVL(r("DDD"), "")
            Ent.Tel = NVL(r("NUM_TEL"), "")
            Ent.Ramal = NVL(r("NUM_RAMAL"), "")
            Ent.SeqTel = NVL(r("seq_tel"), 0)
        Next

        Return Ent
    End Function

    Public Sub Gravar(ByVal telefone As EntTelefone, ByVal perito As EntPERITO)
        Try
            If perito.TipoPessoa = 1 Then
                Gravar(telefone, perito.ID_PF, perito.Cod_Perito)
            Else
                If Not Validar(telefone.SeqTel, perito.Cod_Perito, perito.TipoPessoa) Then
                    sd.ExecutaProc("uc.Pericias_PKG.Inserir_Pessoajuridicatel", perito.ID_PJ, (RetornaUltimoSeqTel(perito.Cod_Perito, perito.TipoPessoa) + 1), _
                                   telefone.Cod_Tip_Tel, telefone.DDD, telefone.Tel, telefone.Ramal)
                Else
                    sd.ExecutaProc("uc.Pericias_PKG.Alterar_Pessoajuridicatel", perito.ID_PJ, telefone.SeqTel, _
                                   telefone.Cod_Tip_Tel, telefone.DDD, telefone.Tel, telefone.Ramal)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub Gravar(ByVal ent As EntTelefone, ByVal pidPF As Long, ByVal pcodigoPerito As Long)
        If Validar(ent.SeqTel, pcodigoPerito, 1) Then
            AlterarPessoaFisicaTelefone(ent, pidPF)
        Else
            InserirPessoaFisicaTelefone(ent, pidPF, pcodigoPerito)
        End If
    End Sub

    Private Function Validar(ByVal pSeqTel As Integer, ByVal pcodigoPerito As Int64, ByVal ptipoPessoa As Integer) As Boolean
        Try
            Dim dstel As DataSet = Nothing

            dstel = sd.ExecutaProcDS("NOVO_PERICIAS.Validar_Telefone", sd.CriaRefCursor, pcodigoPerito, pSeqTel, ptipoPessoa)

            If dstel.Tables(0).Rows.Count = 0 Then
                Return False
            Else
                If dstel.Tables(0).Rows(0).Item(0).Equals(DBNull.Value) Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

    End Function

    Private Sub AlterarPessoaFisicaTelefone(ByVal ent As EntTelefone, ByVal pID_PF As String)
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Alterar_PessoaFisicaTel", pID_PF, ent.SeqTel, ent.Cod_Tip_Tel, ent.DDD, ent.Tel, ent.Ramal)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub InserirPessoaFisicaTelefone(ByVal ent As EntTelefone, ByVal pID_PF As Long, ByVal codPerito As Long)
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Inserir_PessoaFisicaTel", pID_PF, RetornaUltimoSeqTel(codPerito, 1) + 1, ent.Cod_Tip_Tel, ent.DDD, ent.Tel, ent.Ramal)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub ExcluirPessoaFisicaTelefone(ByVal pID_PF As String, ByVal pSeq_Tel As Integer)
        Try
            sd.ExecutaProc("uc.Pericias_PKG.Excluir_PessoaFisicaTel", pID_PF, pSeq_Tel)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub Excluir(ByVal idPF As Int64, ByVal seqTel As Integer, ByVal codPerito As Long, ByVal tipoPessoa As Integer, ByVal idPJ As Long)
        Try
            If tipoPessoa = EntPERITO.Pessoa.Fisica Then
                ExcluirPessoaFisicaTelefone(idPF, seqTel)
            Else
                sd.ExecutaProc("uc.Pericias_PKG.Excluir_Pessoajuridicatel", idPJ, seqTel)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function RetornaUltimoSeqTel(ByVal codPerito As Long, ByVal tipoPessoa As Integer) As Integer
        Dim nSeq As Integer = 0

        Try
            nSeq = sd.ExecutaFunc("NOVO_PERICIAS.UltimoSeqTelefone", 50, codPerito, tipoPessoa)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return nSeq
    End Function
End Class
