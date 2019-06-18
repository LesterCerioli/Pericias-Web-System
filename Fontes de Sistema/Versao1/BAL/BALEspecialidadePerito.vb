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

    Public Function ListarEspecialidadesPerito(ByVal sID_PF As String) As DataSet

        Dim param(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sID_PF", OracleDbType.Varchar2, ParameterDirection.Input)
        param(1).Valor = sID_PF

        Try

            sd.Open()

            Return sd.CreateDataSet("ExibirDados_Esp_Perito", param)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Gravar(ByVal ent As EntEspecialidade_Perito) As String
        Dim sMsg As String = String.Empty

        If Validar(ent) Then
            Alterar(ent)
        Else
            sMsg = Inserir(ent)
        End If
        Return sMsg
    End Function

    Public Function Inserir(ByVal ent As EntEspecialidade_Perito) As String

        Dim param(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim sMsg As String = String.Empty
        Dim dr As OracleDataReader
        Dim sRetorno As String = String.Empty

        Try

            param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
            param(0).Valor = ent.COD_ESPECIALIDADE

            param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
            param(1).Valor = ent.COD_PROFISSAO

            param(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
            param(2).Valor = ent.ID_PF

            param(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
            param(3).Valor = ent.COD_ORGAO_PER

            param(4) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_reg", OracleDbType.Varchar2, ParameterDirection.Input)
            param(4).Valor = ent.Num_Reg

            param(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
            param(5).Valor = ent.UF

            sd.Open()

            dr = sd.ExecuteReader("select data_exclusao from Especialidade_Perito where id_pf = ? and cod_profissao= ? and cod_especialidade = ?", _
                                  ent.ID_PF, ent.COD_PROFISSAO, ent.COD_ESPECIALIDADE)

            If dr.Read Then
                sRetorno = dr.Item(0)
                If sRetorno = "" Or sRetorno = "01/01/1900" Then
                    sd.ExecuteNonQuery("INSERIR_ESPECIALIDADE_PER", param)
                Else
                    sMsg = "Não foi possível incluir a profissão por já existir como inativa."
                End If
            Else
                sd.ExecuteNonQuery("INSERIR_ESPECIALIDADE_PER", param)
            End If

            Return sMsg

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Sub Alterar(ByVal ent As EntEspecialidade_Perito)


        Dim param(5) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        param(0).Valor = ent.COD_ESPECIALIDADE

        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        param(1).Valor = ent.COD_PROFISSAO

        param(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
        param(2).Valor = ent.ID_PF

        param(3) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao_Per", OracleDbType.Int32, ParameterDirection.Input)
        param(3).Valor = ent.COD_ORGAO_PER

        param(4) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sNum_reg", OracleDbType.Varchar2, ParameterDirection.Input)
        param(4).Valor = ent.Num_Reg

        param(5) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sUF", OracleDbType.Varchar2, ParameterDirection.Input)
        param(5).Valor = ent.UF

        Try

            sd.Open()

            sd.ExecuteNonQuery("ALTERAR_ESPECIALIDADE_PER", param)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Public Sub Excluir(ByVal nIdPF As Long, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer)

        Dim param(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        param(0).Valor = nCodEspecialidade

        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        param(1).Valor = nCodProfissao

        param(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int64, ParameterDirection.Input)
        param(2).Valor = nIdPF

        Try

            sd.Open()

            sd.ExecuteNonQuery("EXCLUIR_ESPECIALIDADE_PER", param)

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Sub

    Public Function Validar(ByVal ent As EntEspecialidade_Perito) As Boolean

        Dim sRetorno As String = String.Empty

        Try

            sd.Open()

            sRetorno = sd.ExecutaFunc("Validar_Esp_Perito", 200, ent.ID_PF, ent.COD_PROFISSAO, ent.COD_ESPECIALIDADE)

            If sRetorno = "0" Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Public Function Consultar(ByVal nIdPF As Long, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer) As EntEspecialidade_Perito

        Dim dr As OracleDataReader
        Dim ent As New EntEspecialidade_Perito

        Try

            sd.Open()

            dr = sd.ExecuteReader("select * from especialidade_perito where ID_PF = ? and cod_profissao = ? and cod_especialidade = ? ", nIdPF, nCodProfissao, nCodEspecialidade)

            While dr.Read
                ent = GerarEntidade(dr)
            End While

            Return ent

        Catch ex As Exception
            Throw ex
        Finally
            If sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntEspecialidade_Perito

        Dim ent As New EntEspecialidade_Perito

        ent.ID_PF = NVL(row("ID_PF"), 0)
        ent.COD_PROFISSAO = NVL(row("cod_profissao"), 0)
        ent.COD_ESPECIALIDADE = NVL(row("cod_especialidade"), 0)
        ent.COD_ORGAO_PER = NVL(row("COD_ORGAO_PER"), 0)
        ent.UF = NVL(row("UF"), String.Empty)
        ent.Num_Reg = NVL(row("NUM_REG"), String.Empty)

        Return ent

    End Function

End Class
