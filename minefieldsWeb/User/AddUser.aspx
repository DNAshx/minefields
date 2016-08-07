<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="minefieldsWeb.User.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../Styles/styles.css" />
    <asp:Label runat="server" ID="LblError" Visible="false" BackColor="LightGray" ForeColor="Red" Font-Bold="true"/>
    <h1>Add User</h1>
    <br />
    <h3>Enter user name in format <b>DOMAIN\LoginName</b></h3>
    <table>
        <tr>
            <td style="padding-right:10px; padding-left:10px"><asp:Label runat="server" Text="Login: " /></td>
            <td align="right"><asp:TextBox runat="server" ID="TxtUserName" /></td>
            
        </tr>
        <tr>
            <td colspan="2" align="right"><asp:Button runat="server" ID="BtnAddUser" Text="Add User" OnClick="BtnAddUser_Click" /></td>
        </tr>
        <tr>
        <td colspan="2"><h3>Users List</h3> </td>
         </tr>
       <tr>
           <td colspan="2">
                <asp:DataGrid runat="server" ID="DataGridUsers" DataKeyField="UserId" CssClass="Grid" PageSize="10" AllowPaging="True" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnDeleteCommand="Grid_DeleteCommand"
                    OnPageIndexChanged="Grid_PageIndexChanged">
                    <Columns>
                        <asp:BoundColumn HeaderText="ID" DataField="UserId"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="User Name" DataField="UserName"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="IsAllowed">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="ChkBxIsAllowed" Checked='<%# DataBinder.Eval(Container, "DataItem.IsAllowed") %>'/>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Admin">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="ChkBxAdmin" Checked='<%# DataBinder.Eval(Container, "DataItem.Admin") %>'/>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:ButtonColumn CommandName="Delete" HeaderText="Delete" Text="Delete"></asp:ButtonColumn>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <PagerStyle CssClass="GridPager" ForeColor="#333333" HorizontalAlign="Center" Mode="NumericPages" />
		            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
		            <ItemStyle CssClass="GridItem"></ItemStyle>
		            <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                </asp:DataGrid>
           </td>
      </tr>
        <tr>
            <td colspan="2" align="right">
                    <asp:Button runat="server" ID="BtnApply" Text="Apply Changes" OnClick="BtnApply_Click"/>
                </td>
            </tr>
        </table>
</asp:Content>
