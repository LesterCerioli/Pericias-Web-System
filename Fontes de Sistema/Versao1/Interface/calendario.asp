<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="calendario.aspx.vb" Inherits="Interface.calendario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%
    Dim ano, mes, nav
    Dim nmes
    Dim dias, sem

    nav = Request.QueryString("nav")
    ano = CInt(Request.QueryString("ano"))
    mes = CInt(Request.QueryString("mes"))

    If nav = "P" And mes < 12 Then
        mes = mes + 1
    ElseIf nav = "P" And mes = 12 Then
        mes = 1
        ano = ano + 1
    ElseIf nav = "A" And mes > 1 Then
        mes = mes - 1
    ElseIf nav = "A" And mes = 1 Then
        mes = 12
        ano = ano - 1
    Else
        mes = 8
        ano = 2007
    End If

    If mes = 1 Then nmes = "janeiro"
    If mes = 2 Then nmes = "fevereiro"
    If mes = 3 Then nmes = "março"
    If mes = 4 Then nmes = "abril"
    If mes = 5 Then nmes = "maio"
    If mes = 6 Then nmes = "junho"
    If mes = 7 Then nmes = "julho"
    If mes = 8 Then nmes = "agosto"
    If mes = 9 Then nmes = "setembro"
    If mes = 10 Then nmes = "outubro"
    If mes = 11 Then nmes = "novembro"
    If mes = 12 Then nmes = "dezembro"


    dias = Day(DateAdd("d", -1, DateSerial(ano, mes + 1, 1)))
    sem = Weekday(DateAdd("d", -(Day("1/" & mes & "/" & ano) - 1), "1/" & mes & "/" & ano))
%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>calendario</title>
	<style>
		body { margin: 0px; }
		th, td { font-size: 8pt; }
	</style>
	<script language="javascript">
		function anterior() {
			f.nav.value="A";
			f.submit();
		}
		function proximo() {
			f.nav.value="P";
			f.submit();
		}
	</script>
</head>
<body>

    <form id="f" runat="server" method="get" action="#">
		<input type="hidden" name="nav" value="" />
		<input type="hidden" name="mes" value="<%=mes%>" />
		<input type="hidden" name="ano" value="<%=ano%>" />

        <div>
    
			<table cellpadding=0 cellspacing=0 border=1>
				<tr>
					<th><a href="javascript:anterior();"> << </a></th>
					<th colspan=5><%= nmes %> de <%= ano %></th>
					<th><a href="javascript:proximo();"> >> </a></th>
				</tr>
				<tr><th>DOM</th><th>SEG</th><th>TER</th><th>QUA</th><th>QUI</th><th>SEX</th><th>SAB</th></tr>
				<tr>
				<%
					dim d,ds,pos

					for d = 1 to dias
						if d = 1 then
							for pos = 1 to sem -1
								Response.Write("<td align='center'>&nbsp</td>")
							next
						End If

						if pos = 1 then
							Response.Write("<tr>")
						elseif pos = 8 then
							Response.Write("</tr>")
							Pos = 1
						end if

						Response.Write("<td align='center'>" & d & "</td>")

						pos = pos + 1

					next
					for d = pos to 7
						Response.Write("<td align='center'>&nbsp</td>")
					next

				%>
				</tr>
			</table>

        </div>

    </form>
</body>
</html>
