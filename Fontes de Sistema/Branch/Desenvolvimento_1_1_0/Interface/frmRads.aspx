<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/PerMasterPage.master" CodeBehind="frmRads.aspx.vb" Inherits="Interface.WebForm1" 
    title="Rads" %>
<%@ Register assembly="ClienteWebPadrao" namespace="ClienteWebPadrao" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tela" runat="server">
    <form id="form1" runat="server">
    <cc1:DGTECHyperLink ID="DGTECHyperLink1" runat="server" 
        NavigateUrl="http://www.tjrj.jus.br/scripts/weblink.mgw?MGWLPN=DIGITAL1A&amp;PGM=WEBBCLE44&amp;PORTAL=1&amp;LAB=BIBxWEB&amp;AMB=INTRA&amp;SUMULAxTJ=&amp;TRIPA=198%5e2009%5e24&amp;PAL=&amp;JUR=ESTADUAL&amp;ANOX=2009&amp;TIPO=198&amp;ATO=24&amp;START=">[DGTECHyperLink1]</cc1:DGTECHyperLink>
    <br />
    <br />
    <br />
    </form>
</asp:Content>
