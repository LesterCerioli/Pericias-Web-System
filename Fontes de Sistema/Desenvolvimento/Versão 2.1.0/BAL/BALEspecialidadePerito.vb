Imports ServicoDadosODPNET
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager
Imports Oracle.DataAccess.Client

Public Class BALEspecialidadePerito
    Inherits BaseBAL
    Public EntBal As New EntPERITO

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ListarEspecialidadesPerito(ByVal sCod_Perito As String) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.ExibirDados_Esp_Perito", sd.CriaRefCursor, sCod_Perito)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ListarProfissoesEspecialidadesPerito(ByVal sCod_Perito As Long) As DataSet
        Dim ds As DataSet = Nothing

        Try
            ds = sd.ExecutaProcDS("NOVO_PERICIAS.Listar_Especialidades_Perito", sd.CriaRefCursor, sCod_Perito)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function Gravar(ByVal ent As EntEspecialidade_Perito) As String
        Try
            Dim sMsg As String = String.Empty

            If Validar(ent) Then
                Alterar(ent)
            Else
                sMsg = Inserir(ent)
            End If
            Return sMsg
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function Inserir(ByVal ent As EntEspecialidade_Perito) As String
        Try
            Dim sMsg As String = String.Empty
            Dim dr As OracleDataReader
            Dim sRetorno As String = String.Empty


            sd.Open()
            dr = sd.ExecuteReader("select data_exclusao from Especialidade_Perito where cod_perito = ? and cod_profissao= ? and cod_especialidade = ?", _
                                  ent.CodigoPerito, ent.COD_PROFISSAO, ent.COD_ESPECIALIDADE)

            If dr.Read Then
                sRetorno = IIf(dr.Item(0).Equals(DBNull.Value), String.Empty, dr.Item(0))

                If sRetorno = "" Or sRetorno = "01/01/1900" Then
                    sd.ExecutaProc("NOVO_PERICIAS.INSERIR_ESPECIALIDADE_PER", ent.COD_ESPECIALIDADE, ent.COD_PROFISSAO, ent.ID_PF, ent.COD_ORGAO_PER, ent.Num_Reg, ent.UF, ent.CodigoPerito)
                Else
                    sMsg = "Não foi possível incluir a profissão por já existir como inativa."
                End If
            Else
                sd.ExecutaProc("NOVO_PERICIAS.INSERIR_ESPECIALIDADE_PER", ent.COD_ESPECIALIDADE, ent.COD_PROFISSAO, ent.ID_PF, ent.COD_ORGAO_PER, ent.Num_Reg, ent.UF, ent.CodigoPerito)
            End If

            Return sMsg
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Sub Alterar(ByVal ent As EntEspecialidade_Perito)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.ALTERAR_ESPECIALIDADE_PER", ent.COD_ESPECIALIDADE, ent.COD_PROFISSAO, ent.ID_PF, ent.COD_ORGAO_PER, ent.Num_Reg, ent.UF, ent.CodigoPerito)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Sub Excluir(ByVal nCodigoPerito As Int64, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer)
        Try
            sd.ExecutaProc("NOVO_PERICIAS.EXCLUIR_ESPECIALIDADE_PER", nCodEspecialidade, nCodProfissao, nCodigoPerito)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Sub

    Public Function Validar(ByVal ent As EntEspecialidade_Perito) As Boolean
        Try
            Dim sRetorno As String = String.Empty

            sRetorno = sd.ExecutaFunc("NOVO_PERICIAS.Validar_Especialidade_Perito", 200, ent.CodigoPerito, ent.COD_PROFISSAO, ent.COD_ESPECIALIDADE)

            If sRetorno = "0" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Public Function Consultar(ByVal ncod_perito As Long, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer) As EntEspecialidade_Perito
        Dim dr As OracleDataReader
        Dim ent As New EntEspecialidade_Perito

        Try
            sd.Open()

            dr = sd.ExecuteReader("select * from especialidade_perito where cod_perito = ? and cod_profissao = ? and cod_especialidade = ? ", ncod_perito, nCodProfissao, nCodEspecialidade)

            While dr.Read
                ent = GerarEntidade(dr)
            End While


            Return ent
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try
    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntEspecialidade_Perito
        Try
            Dim ent As New EntEspecialidade_Perito

            ent.ID_PF = NVL(row("ID_PF"), 0)
            ent.COD_PROFISSAO = NVL(row("cod_profissao"), 0)
            ent.COD_ESPECIALIDADE = NVL(row("cod_especialidade"), 0)
            ent.COD_ORGAO_PER = NVL(row("COD_ORGAO_PER"), 0)
            ent.UF = NVL(row("UF"), String.Empty)
            ent.Num_Reg = NVL(row("NUM_REG"), String.Empty)
            ent.CodigoPerito = NVL(row("COD_PERITO"), 0)

            Return ent
        Catch ex As Exception
            Throw ex
        End Try


    End Function

End Class
