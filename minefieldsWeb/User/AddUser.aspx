<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="minefieldsWeb.User.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Add User</h1>
    <br />
    <h3>To add new user, enter login click "Check" and then "Add".</h3>
    <table>
        <tr>
            <td style="padding-right:10px; padding-left:10px"><asp:Label runat="server" Text="Login: " /></td>
            <td><asp:TextBox runat="server" ID="TxtUserName" /></td>
        </tr>
        <tr>
            <td colspan="2" align="right"><asp:Button runat="server" ID="BtnAddUser" Text="Add User" OnClick="BtnAddUser_Click" /></td>
        </tr>
    </table>
</asp:Content>
