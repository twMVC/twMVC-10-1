<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WebForm_MultiLayers_ADONET.Category.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Details</h2>
    <fieldset>
        <legend>Category</legend>
        <div class="display-label">
            CategoryID
        </div>
        <div class="display-field">
            <asp:Label ID="Label_CategoryID" runat="server"></asp:Label>
        </div>
        <div class="display-label">
            CategoryName
        </div>
        <div class="display-field">
            <asp:TextBox ID="TextBox_CategoryName" runat="server"></asp:TextBox>
        </div>
        <div class="display-label">
            Description
        </div>
        <div class="display-field">
            <asp:TextBox ID="TextBox_Description" runat="server"></asp:TextBox>
        </div>
    </fieldset>
    <br />
    <asp:HyperLink ID="HyperLink_List" runat="server" NavigateUrl="List.aspx">Back to List</asp:HyperLink>    
</asp:Content>
