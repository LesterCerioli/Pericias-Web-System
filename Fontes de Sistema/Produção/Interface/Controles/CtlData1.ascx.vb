Public Partial Class CtlData1

    Inherits System.Web.UI.UserControl

    'Public Event TextChanged()
    Public Event TextChanged As EventHandler

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    'Protected Overrides Sub OnInit(ByVal e As EventArgs)
    '    MyBase.OnInit(e)
    '    AddHandler txtCtlData.TextChanged, AddressOf txtCtlData.ToString
    'End Sub


    'Public Event EmployeeEdit As EventHandler

    'Protected Sub OnEmpEdit(ByVal e As EventArgs)
    '    RaiseEvent EmployeeEdit(Me, e)
    'End Sub
    'Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton.Click
    '    RaiseEvent EmployeeEdit(Me, e)

    '    EditEmployeeDetails()

    'End Sub



    'Visual Basic

    '    Sub TestEvents()
    '        Dim Obj As New Class1
    '        ' Associate an event handler with an event.
    '        AddHandler Obj.Ev_Event, AddressOf EventHandler
    '        ' Call the method to raise the event.
    '        Obj.CauseSomeEvent()
    '        ' Stop handling events.
    '        RemoveHandler Obj.Ev_Event, AddressOf EventHandler
    '        ' This event will not be handled.
    '        Obj.CauseSomeEvent()
    '    End Sub

    '    Sub EventHandler()
    '        ' Handle the event.
    '        MsgBox("EventHandler caught event.")
    '    End Sub

    '    Public Class Class1
    '        ' Declare an event.
    '        Public Event Ev_Event()
    '        Sub CauseSomeEvent()
    '            ' Raise an event.
    '            RaiseEvent Ev_Event()
    '        End Sub
    '    End Class

    Public Sub TxtCtlData_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCtlData.TextChanged
        'Public Event TitleChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim DataTeste As String
        Dim DataHoje As Date
        Dim DiaHoje As String
        Dim MesHoje As String
        Dim AnoHoje As String
        'Formato Final dd/mm/aaaa
        DataTeste = txtCtlData.Text
        DataHoje = Today()
        If DataTeste.Length = 5 Then
            DataTeste = DataTeste + "/" + (DataHoje.Year).ToString
        End If
        DiaHoje = (DataHoje.Day).ToString
        MesHoje = (DataHoje.Month).ToString
        If Len(DiaHoje) = 1 Then
            DiaHoje = "0" + DiaHoje
        End If
        If Len(MesHoje) = 1 Then
            MesHoje = "0" + MesHoje
        End If
        AnoHoje = (DataHoje.Year).ToString
        If UCase(txtCtlData.Text) = "H" Then
            txtCtlData.Text = DiaHoje + "/" + MesHoje + "/" + AnoHoje
            Exit Sub
        End If
        Select Case Len(DataTeste)
            Case 1
                DataTeste = "0" + DataTeste + "/" + MesHoje + "/" + AnoHoje
            Case 2
                DataTeste = DataTeste + "/" + MesHoje + "/" + AnoHoje
            Case 3
                DataTeste = "0" + Mid(DataTeste, 1, 1) + "/" + Mid(DataTeste, 2, 2) + "/" + AnoHoje
            Case 4
                DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 3, 2) + "/" + AnoHoje
            Case 5
                If Val(Mid(DataTeste, 4, 2)) < 50 Then
                    DataTeste = "0" + Mid(DataTeste, 1, 2) + "/0" + Mid(DataTeste, 2, 2) + "/20" + Mid(DataTeste, 4, 2)
                Else
                    DataTeste = "0" + Mid(DataTeste, 1, 2) + "/0" + Mid(DataTeste, 2, 2) + "/19" + Mid(DataTeste, 4, 2)
                End If
            Case 6
                If Val(Mid(DataTeste, 5, 2)) < 50 Then
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 3, 2) + "/20" + Mid(DataTeste, 5, 2)
                Else
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 3, 2) + "/19" + Mid(DataTeste, 5, 2)
                End If
            Case 8
                If Val(Mid(DataTeste, 7, 2)) < 50 Then
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 4, 2) + "/20" + Mid(DataTeste, 7, 2)
                Else
                    DataTeste = Mid(DataTeste, 1, 2) + "/" + Mid(DataTeste, 4, 2) + "/19" + Mid(DataTeste, 7, 2)
                End If
        End Select
        txtCtlData.Text = DataTeste
        Try
            If txtCtlData.Text <> "" Then
                If Not IsDate(CDate(txtCtlData.Text)) Then
                    MsgErroData()
                    txtCtlData.Text = ""
                    Exit Sub
                End If
            End If
        Catch
            MsgErroData()
            txtCtlData.Text = ""
            Exit Sub
        End Try
        txtCtlData.Text = FormatarData(txtCtlData.Text)
        RaiseEvent TextChanged(sender, e)
        'RaiseEvent TextChanged()
    End Sub
    Public Property Data() As String
        Get
            If Not (txtCtlData.Text) Is Nothing Then
                Return txtCtlData.Text
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            txtCtlData.Text = value
        End Set
    End Property

    Function FormatarData(ByVal NewDate As String) As String

        Dim Dia As String
        Dim Mes As String
        Dim Ano As String
        Dia = Mid(NewDate, 1, 2)
        Mes = Mid(NewDate, 4, 2)
        Ano = Trim(Mid(NewDate, 7, 4))
        If Mes.Length = 1 Then
            Mes = "0" + Mes
        End If
        If Dia.Length = 1 Then
            Dia = "0" + Dia
        End If
        If Ano.Length = 2 Then
            If Val(Ano) > 30 Then
                Ano = "19" + Ano
            Else
                Ano = "20" + Ano
            End If
        End If
        Return (Dia + "/" + Mes + "/" + Ano)
    End Function

    Public Sub MsgErroData()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "erro", "alert('Data Inválida');", True)
    End Sub

End Class