'------------------------------------------------
' 02/08/2007 11:45:47
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BALCOMARCA
    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosServentia(ByVal pCod_Orgao As Integer) As DataSet
        Dim ds As DataSet
        Dim oParam(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        'Dim sOrdenacao As String
        Try
            sd.Open()
            oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Orgao", OracleDbType.Int32, ParameterDirection.Input)
            oParam(1).Valor = pCod_Orgao
            ds = sd.CreateDataSet("ExibirDados_Serventia", oParam)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function


    'Public Function Listar(ByVal sFiltro As String, ByVal sOrdenacao As String) As EntCOMARCA()
    '    Dim lst() As EntCOMARCA = New EntCOMARCA() {}
    '    Dim dr As OracleDataReader
    '    Dim sSQL As String

    '    Try
    '        sFiltro = "%" + sFiltro + "%"
    '        sSQL = "select * from COMARCA where nome like ? " + IIf(sOrdenacao = String.Empty, String.Empty, " order by " + sOrdenacao)
    '        sd.Open()
    '        dr = sd.ExecuteReader(sSQL, sFiltro)
    '        While dr.Read
    '            Array.Resize(Of EntCOMARCA)(lst, lst.Length + 1)
    '            lst(lst.Length - 1) = GerarEntidade(dr)
    '        End While
    '        dr.Close()
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        sd.Close()
    '    End Try

    '    Return lst
    'End Function

    Public Function ExibirDadosSet(Optional ByVal sFiltro As Integer = 0) As DataSet
        Dim ds As DataSet
        Dim oParam(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        'Dim sOrdenacao As String
        Try
            sd.Open()
            oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Filtro", OracleDbType.Int32, ParameterDirection.Input)
            oParam(1).Valor = sFiltro
            ds = sd.CreateDataSet("ExibirDados_Comarca", oParam)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    Public Function ExibirDadosNurExt(ByVal pCod_Nur As Integer) As DataSet
        Dim ds As DataSet

        Dim ParametrosExibir(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo

        Try
            sd.Open()
            ParametrosExibir(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            ParametrosExibir(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("nCod_Nur", OracleDbType.Int32, ParameterDirection.Input)
            ParametrosExibir(1).Valor = pCod_Nur
            ds = sd.CreateDataSet("ExibirNUR", ParametrosExibir)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntCOMARCA
        Dim Ent As New EntCOMARCA

        Ent.CodCom = CInt(NVL(row("COD_COM"), 0))
        Ent.Nome = CStr(NVL(row("NOME"), String.Empty))
        Ent.NomeRedu = CStr(NVL(row("NOME_REDU"), String.Empty))
        Ent.CodNurc = CInt(NVL(row("COD_NURC"), 0))
        Ent.TipEntranc = CStr(NVL(row("TIP_ENTRANC"), String.Empty))
        Ent.IndSedeNurc = CStr(NVL(row("IND_SEDE_NURC"), String.Empty))
        Ent.CodComHll = CInt(NVL(row("COD_COM_HLL"), 0))
        Ent.CodRegiao = CInt(NVL(row("COD_REGIAO"), 0))
        Ent.CodTj = CStr(NVL(row("COD_TJ"), String.Empty))
        Ent.NumDiasAtrasoDo = CInt(NVL(row("NUM_DIAS_ATRASO_DO"), 0))
        Ent.IndDistOfic = CStr(NVL(row("IND_DIST_OFIC"), String.Empty))
        Ent.CodComPc = CStr(NVL(row("COD_COM_PC"), String.Empty))
        Ent.NomeDtl = CStr(NVL(row("NOME_DTL"), String.Empty))
        Ent.CodRegJud = CInt(NVL(row("COD_REG_JUD"), 0))
        Ent.CodComPrinc = CInt(NVL(row("COD_COM_PRINC"), 0))
        Ent.IndVaraFed = CStr(NVL(row("IND_VARA_FED"), String.Empty))
        Ent.NomeWeb = CStr(NVL(row("NOME_WEB"), String.Empty))
        Ent.NumOrdemDo = CInt(NVL(row("NUM_ORDEM_DO"), 0))
        Ent.CodComDo = CDec(NVL(row("COD_COM_DO"), 0D))
        Return Ent
    End Function

    ' Função utilizada pelo controle DGTECCodNome do ClientePadrao
    Public Function PesqPorDescr(ByVal str As String) As DataSet
        Return sd.CreateDataSet("Select COD_COM, NOME from COMARCA where NOME like ? ", str)
    End Function

End Class
