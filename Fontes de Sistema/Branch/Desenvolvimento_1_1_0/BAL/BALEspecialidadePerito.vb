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
        Dim ds As DataSet
        Dim param(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("sID_PF", OracleDbType.Varchar2, ParameterDirection.Input)
        param(1).Valor = sID_PF

        sd.Open()
        ds = sd.CreateDataSet("ExibirDados_Esp_Perito", param)
        sd.Close()

        Return ds
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

        sd.Open()
        dr = sd.ExecuteReader("select data_exclusao from Especialidade_Perito where id_pf = ? and cod_profissao= ? and cod_especialidade = ?", _
                              ent.ID_PF, ent.COD_PROFISSAO, ent.COD_ESPECIALIDADE)

        If dr.Read Then
            sRetorno = dr.Item(0)
            sd.Close()
            If sRetorno = "" Or sRetorno = "01/01/1900" Then
                sd.Open()
                sd.ExecuteNonQuery("INSERIR_ESPECIALIDADE_PER", param)
                sd.Close()
            Else
                sMsg = "Não foi possível incluir a profissão por já existir como inativa."
            End If
        Else
            sd.Open()
            sd.ExecuteNonQuery("INSERIR_ESPECIALIDADE_PER", param)
            sd.Close()
        End If
        Return sMsg

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

        sd.Open()
        sd.ExecuteNonQuery("ALTERAR_ESPECIALIDADE_PER", param)
        sd.Close()

    End Sub

    Public Sub Excluir(ByVal nIdPF As Integer, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer)

        Dim param(2) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        param(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Especialidade", OracleDbType.Int32, ParameterDirection.Input)
        param(0).Valor = nCodEspecialidade

        param(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Profissao", OracleDbType.Int32, ParameterDirection.Input)
        param(1).Valor = nCodProfissao

        param(2) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nID_PF", OracleDbType.Int32, ParameterDirection.Input)
        param(2).Valor = nIdPF

        sd.Open()
        sd.ExecuteNonQuery("EXCLUIR_ESPECIALIDADE_PER", param)
        sd.Close()

    End Sub

    Public Function Validar(ByVal ent As EntEspecialidade_Perito) As Boolean

        Dim sRetorno As String = String.Empty
        sd.Open()
        sRetorno = sd.ExecutaFunc("Validar_Esp_Perito", 200, ent.ID_PF, ent.COD_PROFISSAO, ent.COD_ESPECIALIDADE)

        If sRetorno = "0" Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function Consultar(ByVal nIdPF As Long, ByVal nCodProfissao As Integer, ByVal nCodEspecialidade As Integer) As EntEspecialidade_Perito
        Dim dr As OracleDataReader
        Dim ent As New EntEspecialidade_Perito

        sd.Open()
        dr = sd.ExecuteReader("select * from especialidade_perito where ID_PF = ? and cod_profissao = ? and cod_especialidade = ? ", nIdPF, nCodProfissao, nCodEspecialidade)

        'If dr.Read Then
        '    ent = GerarEntidade(dr)
        'End If

        While dr.Read
            ent = GerarEntidade(dr)
        End While

        sd.Close()
        Return ent
    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntEspecialidade_Perito
        Dim ent As New EntEspecialidade_Perito

        'For Each r As DataRow In dr
        '    ent.ID_PF = NVL("ID_PF", 0)
        '    ent.COD_PROFISSAO = NVL("cod_profissao", 0)
        '    ent.COD_ESPECIALIDADE = NVL("cod_especialidade", 0)
        '    ent.COD_ORGAO_PER = NVL("COD_ORGAO_PER", 0)
        '    ent.UF = NVL("UF", String.Empty)
        '    ent.Num_Reg = NVL("NUM_REG", String.Empty)
        'Next

        '     For Each r As DataRow In dr
        ent.ID_PF = NVL(row("ID_PF"), 0)
        ent.COD_PROFISSAO = NVL(row("cod_profissao"), 0)
        ent.COD_ESPECIALIDADE = NVL(row("cod_especialidade"), 0)
        ent.COD_ORGAO_PER = NVL(row("COD_ORGAO_PER"), 0)
        ent.UF = NVL(row("UF"), String.Empty)
        ent.Num_Reg = NVL(row("NUM_REG"), String.Empty)
        '  Next

        Return ent
    End Function

End Class
