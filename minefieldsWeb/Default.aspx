<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="minefieldsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="PanelInstall">
        <h1>Please install tool first</h1>
        <p class="lead" style="padding-left:10px">Prepare connection string for connection to the SQL Server and install tool</p>
        <p style="padding-left:10px"><a href="Install/Install" class="btn btn-primary btn-lg">Install &raquo;</a></p>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="PanelInstalled" >
       <p class="lead" style="padding-left:10px"> Application Installed and ready to use.</p>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="PanelDisabled" Visible="false">
       <p class="lead" style="padding-left:10px"> <h3 style="padding-left:10px">Application <b>Disabled</b>, please click <a href="Install/Enabled">here</a> to enable or ask administrator if can't go there.</h3></p>
    </asp:Panel>
    <br />
    <asp:Panel runat="server" ID="PanelAddUser" >
        <p style="padding-left:10px"><a href="User/AddUser" class="btn btn-primary btn-lg">Add User &raquo;</a></p>
    </asp:Panel>

</asp:Content>
