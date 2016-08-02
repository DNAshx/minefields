<%@ Page Title="Install tool" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="minefieldsWeb.Install.Install" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>Install</h2>
    <h3>Install tool if it's not installed.</h3>
    <p>Insert SQL server connection string and click install.</p>
    <div>
        <asp:TextBox runat="server" ID="TxtConnString" />
        <asp:Button runat="server" ID="InstallBtn" OnClick="InstallBtn_Click" Text="Install" />
    </div>
    <div>
        <asp:TextBox runat="server" ID="TxtError" ReadOnly="true" Wrap="true" Visible="false" ForeColor="Red"></asp:TextBox>
    </div>
    <br />
</asp:Content>