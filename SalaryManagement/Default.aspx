<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SalaryManagement._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Employees</h1>
        <div>
            Select file &nbsp;<asp:FileUpload ID="UploadEmployees" runat="server" /><br />
            <br />
            <asp:Button ID ="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" /><br />
            <br />
        </div>

        <asp:GridView ID="GridEmployees" AutoGenerateColumns="true" runat="server">
        </asp:GridView>
    </div>

</asp:Content>
