<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="minefieldsWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div runat="server" id="notInstalledDiv" class="jumbotron">        
        <h1>Please install tool first</h1>
        <p class="lead">Prepare connection string for connection to the SQL Server and install tool</p>
        <p><a href="Install/Install" class="btn btn-primary btn-lg">Install &raquo;</a></p>
    </div>    
    <div runat="server" id="installedDiv" class="jumbortron">
       <p class="lead"> Application Installed and ready to use.</p>
    </div>
    <div runat="server" id="administrationDiv"  class="jumbotron">
        <p><a href="User/AddUser" class="btn btn-primary btn-lg">Add User &raquo;</a></p>
    </div>

</asp:Content>
