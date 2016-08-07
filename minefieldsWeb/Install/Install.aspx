<%@ Page Title="Install tool" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Install.aspx.cs" Inherits="minefieldsWeb.Install.Install" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>Install</h2>
    <h3>Install tool if it's not installed.</h3>
    <p>Insert SQL server connection string and click install.</p>
    <div>
        <table>
            <tr>
                <td style="padding-right:10px"> <asp:Label runat="server" Text="Server Name" /> </td>
                <td> <asp:TextBox runat="server" ID="TxtServerName" /> </td>
            </tr>
            <tr>
                <td style="padding-right:10px"> <asp:Label runat="server" Text="Database Name" /> </td>
                <td> <asp:TextBox runat="server" ID="TxtDataBase" Text="Minefields" ReadOnly="true" /> </td>
            </tr>
            <tr>
                <td style="padding-right:10px"><asp:Label runat="server" Text="Login" /></td>
                <td><asp:TextBox runat="server" ID="TxtLogin" Enabled="false" /></td>
            </tr>
            <tr>
                <td style="padding-right:10px"><asp:Label runat="server" Text="Password" /></td>
                <td><asp:TextBox runat="server" ID="TxtPassword" Enabled="false" TextMode="Password" /></td>
            </tr>
            <tr>                
                <td colspan="2"><asp:CheckBox runat="server" ID="ChkBxTrustConnection" Text="Windows Authorization" 
                    OnCheckedChanged="ChkBxTrustConnection_CheckedChanged" AutoPostBack="true"
                    Checked="true"/></td>
            </tr>
            <tr>
                <td colspan="2" style="padding-right:10px;padding-top:10px" align="right">
                    <asp:Button runat="server" ID="InstallBtn" OnClick="InstallBtn_Click" Text="Install" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:TextBox runat="server" ID="TxtError" ReadOnly="true" Wrap="true" Visible="false" ForeColor="Red"></asp:TextBox>
    </div>
    <br />
</asp:Content>