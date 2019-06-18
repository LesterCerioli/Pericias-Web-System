Partial Public Class frmDocsNecessarios
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TxtCpf.Attributes.Add("OnBlur", "validarcpf(this);")
        'TxtCpf.Attributes.Add("onkeyup", "javascript:maskCPF(this);")
        ''''TxtCpf.Attributes.Add("onblur", "javascript:validaCPF(this);")
        'txtCPF.Attributes.Add("onblur", "validacpf(txtCPF.value);")

    End Sub

    'Protected Sub DGTECButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DGTECButton1.Click
    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "validarcpf(frmaturoizacaoRec.txtCpf.value)", True)
    'Me.ClientScript.RegisterClientScriptBlock(Me.GetType(), "NovaJanela", "onblur="validacpf();",true)
    'End Sub
End Class