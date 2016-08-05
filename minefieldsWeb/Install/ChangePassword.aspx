<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="minefieldsWeb.Install.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Please, insert new password for tool's login</h1>
    <table>
        <tr>
            <td style="padding-right:10px"><asp:Label runat="server" Text="UserName" /></td>
            <td><asp:Label runat="server" Text="Minefields" /></td>
        </tr>
        <tr>
            <td style="padding-right:10px"><asp:Label runat="server" Text="Password" /></td>
            <td><asp:TextBox runat="server" ID="TxtPassword" TextMode="Password" /></td>
        </tr>
        <tr>
            <td colspan="2" align="right"><asp:Button runat="server" ID="BtnSave" Text="Save" OnClick="BtnSave_Click" /></td>
        </tr>
     </table>
    <div>
        <asp:TextBox runat="server" ID="TxtError" ReadOnly="true" Wrap="true" Visible="false" ForeColor="Red"></asp:TextBox>
    </div>
</asp:Content>
