<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Enabled.aspx.cs" Inherits="minefieldsWeb.Install.Enabled" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron">        
        <h1>Enable/Disable Tool</h1>
        <p class="lead">Here you can <b>Enable</b> or <b>Disable</b> tool.</p>
        <p><asp:Button runat="server" ID="BtnEnableDisable" OnClick="BtnEnableDisable_Click" /></p>
    </div>  
</asp:Content>
