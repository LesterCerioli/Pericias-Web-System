Imports Entidade
Imports BAL
Imports System.Math

Public Class frmIndicadorDesempenho
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub BtnLimpar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLimpar.Click
        Try
            txtDataProtocoloInicial.Text = String.Empty
            txtDataProtocoloFinal.Text = String.Empty
            lblMedia.Text = "0"
            pnlMedia.Visible = False
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub BtnSair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSair.Click
        Try
            Response.Redirect("Principal.aspx")
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtDataProtocoloFinal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataProtocoloFinal.TextChanged
        Try
            Dim dataProtocoloInicial As Date
            Dim dataProtocoloFinal As Date
            Dim diasDiferenca As Long = 0
            Dim diasPeriodo As Long = 0
            Dim media As Double = 0D

            Dim balAjudaCusto As New BalAjudaCusto(GetUsuario)
            Dim listaAjudasCusto As List(Of AjudaCusto) = Nothing

            If String.IsNullOrEmpty(txtDataProtocoloInicial.Text) OrElse String.IsNullOrEmpty(txtDataProtocoloFinal.Text) Then
                MsgErro("Por favor, informe a data inicial ou final.")
                Exit Sub
            End If

            Try
                dataProtocoloInicial = CDate(txtDataProtocoloInicial.Text)
            Catch ex As Exception
                Throw New Exception("Data inicial inválida.")
            End Try

            Try
                dataProtocoloFinal = CDate(txtDataProtocoloFinal.Text)
            Catch ex As Exception
                Throw New Exception("Data final inválida.")
            End Try

            If dataProtocoloInicial >= dataProtocoloFinal Then
                MsgErro("A data de protocolo final deve ser maior que a data de protocolo inicial.")
                Exit Sub
            End If

            listaAjudasCusto = balAjudaCusto.ConsultarPagamentosLista(0, 0, 0, 0, 0, Nothing, txtDataProtocoloInicial.Text, txtDataProtocoloFinal.Text, Nothing, Nothing)

            If listaAjudasCusto.Count = 0 Then
                MsgErro("Não foram encontrados dados para o período informado.")
                Exit Sub
            End If

            diasPeriodo = (dataProtocoloFinal - dataProtocoloInicial).Days

            For Each ac As AjudaCusto In listaAjudasCusto
                diasDiferenca = (CDate(ac.DataProtocolo) - CDate(ac.DataRecebimento)).Days
                media = media + (diasDiferenca / diasPeriodo)
            Next

            lblMedia.Text = Round(media, 0).ToString

            pnlMedia.Visible = True
        Catch ex As Exception
            MsgErro(ex.Message)
        End Try
    End Sub

End Class